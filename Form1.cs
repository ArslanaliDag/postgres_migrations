using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PostgresMigrations
{
    public partial class Form1 : Form
    {
        // Config paths (kept identical to PowerShell defaults)
        private readonly string MigrationsPath = @"D:\database_migrations";
        private readonly string PendingPath;
        private readonly string TemplatesPath;
        private readonly string SchemasPath;
        private readonly string CurrentUser;

        public Form1()
        {
            InitializeComponent();

            PendingPath = Path.Combine(MigrationsPath, "pending");
            TemplatesPath = Path.Combine(MigrationsPath, "templates");
            SchemasPath = Path.Combine(MigrationsPath, "schemas");
            CurrentUser = Environment.UserName;

            LoadTemplates();

            // Ensure folders
            Directory.CreateDirectory(MigrationsPath);
            Directory.CreateDirectory(PendingPath);
            Directory.CreateDirectory(TemplatesPath);
            Directory.CreateDirectory(SchemasPath);

            // Initialize UI defaults
            comboSchema.Items.AddRange(new object[] { "policyregistry", "users_schema", "public", "custom" });
            comboSchema.SelectedIndex = 0;

            comboType.Items.AddRange(new object[] {
                "CREATE TABLE - Create new table",
                "ALTER TABLE - Modify table",
                "CREATE FUNCTION - Create function",
                "CREATE INDEX - Create index",
                "CREATE SCHEMA - Create schema",
                "DATA MIGRATION - Data migration",
                "CUSTOM - Custom SQL"
            });
            comboType.SelectedIndex = 0;

            txtAuthor.Text = CurrentUser;
            txtName.Text = "add_new_column";
            txtComment.Text = "Added new field for storing information";
            txtSql.Font = new System.Drawing.Font("Consolas", 10);
            txtSql.Text = @"-- Write your SQL code here
-- Use schema prefix for all objects: {schema}.table_name
-- Example: CREATE TABLE IF NOT EXISTS {schema}.table_name (...)";
        }

        // Load template files into combo
        private void LoadTemplates()
        {
            try
            {
                var files = Directory.GetFiles(TemplatesPath, "*.sql");
                if (files.Length > 0)
                {
                    comboTemplates.Items.Clear();
                    comboTemplates.Items.Add("-- Select template --");
                    foreach (var f in files)
                    {
                        comboTemplates.Items.Add(Path.GetFileName(f));
                    }
                    comboTemplates.SelectedIndex = 0;
                    comboTemplates.Visible = true;
                    labelLoadTemplate.Visible = true;
                    btnLoadTemplate.Visible = true;
                }
                else
                {
                    comboTemplates.Visible = false;
                    labelLoadTemplate.Visible = false;
                    btnLoadTemplate.Visible = false;
                }
            }
            catch
            {
                // ignore template loading errors
            }
        }

        #region Helpers

        private string GetTargetSchema()
        {
            var sel = comboSchema.SelectedItem?.ToString() ?? "public";
            if (sel == "custom")
            {
                return txtCustomSchema.Text.Trim();
            }
            return sel;
        }

        private string GetSafeName(string name)
        {
            var sb = new StringBuilder();
            foreach (char c in name)
            {
                if (char.IsLetterOrDigit(c) || c == '_') sb.Append(c);
                else sb.Append('_');
            }
            return sb.ToString();
        }

        private string GetMigrationFileName(string name, string schema)
        {
            string date = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string safeName = GetSafeName(name);
            string safeSchema = GetSafeName(schema);
            return $"{date}_{safeSchema}_{safeName}.sql";
        }

        // Template text provider similar to PowerShell Get-SQLTemplate
        private string GetSqlTemplateByType(string typeLabel, string schema)
        {
            string placeholder = "{schema}";
            if (typeLabel.Contains("CREATE TABLE"))
            {
                return $@"-- Create new table in {schema} schema
CREATE TABLE IF NOT EXISTS {placeholder}.table_name (
    id SERIAL PRIMARY KEY,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Comments
COMMENT ON TABLE {placeholder}.table_name IS 'Table description';
";
            }
            if (typeLabel.Contains("ALTER TABLE"))
            {
                return $@"-- Add new column to table in {schema} schema
ALTER TABLE IF EXISTS {placeholder}.table_name 
ADD COLUMN IF NOT EXISTS new_column VARCHAR(255);

-- Create index
CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_table_name_new_column 
ON {placeholder}.table_name(new_column);

-- Add comment
COMMENT ON COLUMN {placeholder}.table_name.new_column IS 'Column description';
";
            }
            if (typeLabel.Contains("CREATE FUNCTION"))
            {
                return $@"-- Create function in {schema} schema
CREATE OR REPLACE FUNCTION {placeholder}.function_name(param1 INTEGER)
RETURNS TABLE(column1 INTEGER, column2 VARCHAR) AS $$
BEGIN
    RETURN QUERY
    SELECT id, name
    FROM {placeholder}.some_table
    WHERE id = param1;
END;
$$ LANGUAGE plpgsql;

-- Function comment
COMMENT ON FUNCTION {placeholder}.function_name(INTEGER) IS 'Function description';
";
            }
            if (typeLabel.Contains("CREATE INDEX"))
            {
                return $@"-- Create index CONCURRENTLY (without table lock) in {schema} schema
CREATE INDEX CONCURRENTLY IF NOT EXISTS {placeholder}.idx_table_name_column 
ON {placeholder}.table_name(column_name);

-- Partial index
CREATE INDEX CONCURRENTLY IF NOT EXISTS {placeholder}.idx_table_name_active 
ON {placeholder}.table_name(id) 
WHERE is_active = true;
";
            }
            if (typeLabel.Contains("CREATE SCHEMA"))
            {
                return $@"-- Create new schema
CREATE SCHEMA IF NOT EXISTS {placeholder};

-- Grant permissions if needed
GRANT USAGE ON SCHEMA {placeholder} TO application_user;
GRANT CREATE ON SCHEMA {placeholder} TO application_user;
";
            }
            if (typeLabel.Contains("DATA MIGRATION"))
            {
                return $@"-- Data migration between tables in {schema} schema
DO $$
DECLARE
    rows_affected INTEGER;
BEGIN
    -- Insert data with existence check
    INSERT INTO {placeholder}.target_table (column1, column2)
    SELECT source_column1, source_column2
    FROM {placeholder}.source_table st
    WHERE NOT EXISTS (
        SELECT 1 FROM {placeholder}.target_table tt 
        WHERE tt.column1 = st.source_column1
    );
    
    GET DIAGNOSTICS rows_affected = ROW_COUNT;
    RAISE NOTICE 'Migrated % records', rows_affected;
END $$;
";
            }
            // Default / custom
            return $@"-- Your SQL code for {schema} schema
-- Use {placeholder} prefix for all objects
-- Example: CREATE TABLE IF NOT EXISTS {placeholder}.table_name (...)

DO $$
BEGIN
    -- Your code with error handling
    RAISE NOTICE 'Migration started for schema: {placeholder}';
    
    -- Execute operations
    
    RAISE NOTICE 'Migration completed successfully';
EXCEPTION
    WHEN others THEN
        RAISE EXCEPTION 'Error: %', SQLERRM;
END $$;
";
        }

        #endregion

        #region Event Handlers (buttons + combos)

        private void comboSchema_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isCustom = comboSchema.SelectedItem?.ToString() == "custom";
            labelCustomSchema.Visible = isCustom;
            txtCustomSchema.Visible = isCustom;
        }

        private void btnInsertTemplate_Click(object sender, EventArgs e)
        {
            string target = GetTargetSchema();
            if (string.IsNullOrWhiteSpace(target))
            {
                MessageBox.Show("Please select or enter a schema!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var template = GetSqlTemplateByType(comboType.SelectedItem?.ToString() ?? "CUSTOM", target);
            template = template.Replace("{schema}", target);
            txtSql.Text = template;
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            string migrationName = txtName.Text.Trim();
            if (string.IsNullOrWhiteSpace(migrationName))
            {
                MessageBox.Show("Please enter migration name!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string targetSchema = GetTargetSchema();
            if (string.IsNullOrWhiteSpace(targetSchema))
            {
                MessageBox.Show("Please select or enter a schema!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string fileName = GetMigrationFileName(migrationName, targetSchema);
            string author = txtAuthor.Text.Trim();
            string comment = txtComment.Text.Trim();
            string sql = txtSql.Text.Trim();

            // Build preview content (matches PowerShell preview structure)
            var sb = new StringBuilder();
            sb.AppendLine("=============================================");
            sb.AppendLine("MIGRATION PREVIEW");
            sb.AppendLine("=============================================");
            sb.AppendLine($"File Name: {fileName}");
            sb.AppendLine($"Target Schema: {targetSchema}");
            sb.AppendLine($"Author: {author}");
            sb.AppendLine($"Comment: {comment}");
            sb.AppendLine($"Type: {comboType.SelectedItem}");
            sb.AppendLine("=============================================");
            sb.AppendLine("FILE CONTENT:");
            sb.AppendLine("=============================================");
            sb.AppendLine();
            sb.AppendLine($"-- {fileName}");
            sb.AppendLine($"-- Schema: {targetSchema}");
            sb.AppendLine($"-- Author: {author}");
            sb.AppendLine($"-- Date: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
            sb.AppendLine($"-- Type: {comboType.SelectedItem}");
            sb.AppendLine();
            sb.AppendLine("DO $$");
            sb.AppendLine("DECLARE");
            sb.AppendLine("    start_time TIMESTAMP;");
            sb.AppendLine($"    migration_id TEXT := '{fileName.Replace(".sql", string.Empty)}';");
            sb.AppendLine($"    migration_desc TEXT := '{EscapeForSqlLiteral(comment)}';");
            sb.AppendLine($"    target_schema TEXT := '{EscapeForSqlLiteral(targetSchema)}';");
            sb.AppendLine("BEGIN");
            sb.AppendLine("    start_time := clock_timestamp();");
            sb.AppendLine("    RAISE NOTICE 'Applying migration % to schema: %', migration_id, target_schema;");
            sb.AppendLine();
            sb.AppendLine("    -- === SQL MIGRATION CODE ===");
            sb.AppendLine();
            sb.AppendLine(sql);
            sb.AppendLine();
            sb.AppendLine("    -- === END SQL CODE ===");
            sb.AppendLine();
            sb.AppendLine("    INSERT INTO migrations.db_migrations (");
            sb.AppendLine("        migration_name,");
            sb.AppendLine("        schema_name,");
            sb.AppendLine("        notes,");
            sb.AppendLine("        execution_time_ms");
            sb.AppendLine("    ) VALUES (");
            sb.AppendLine("        migration_id,");
            sb.AppendLine("        target_schema,");
            sb.AppendLine("        migration_desc,");
            sb.AppendLine("        EXTRACT(EPOCH FROM (clock_timestamp() - start_time)) * 1000");
            sb.AppendLine("    );");
            sb.AppendLine();
            sb.AppendLine("    RAISE NOTICE 'Migration % for schema % completed successfully', migration_id, target_schema;");
            sb.AppendLine();
            sb.AppendLine("EXCEPTION");
            sb.AppendLine("    WHEN others THEN");
            sb.AppendLine("        RAISE EXCEPTION 'Error in migration % for schema %: %', migration_id, target_schema, SQLERRM;");
            sb.AppendLine("END $$;");
            sb.AppendLine("=============================================");
            sb.AppendLine($"File will be saved to: {Path.Combine(PendingPath, fileName)}");
            sb.AppendLine("=============================================");

            using (var preview = new FormPreview(sb.ToString()))
            {
                preview.ShowDialog(this);
            }
        }

        private string EscapeForSqlLiteral(string input)
        {
            if (input == null) return "";
            return input.Replace("'", "''");
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string migrationName = txtName.Text.Trim();
            if (string.IsNullOrWhiteSpace(migrationName))
            {
                MessageBox.Show("Please enter migration name!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string targetSchema = GetTargetSchema();
            if (string.IsNullOrWhiteSpace(targetSchema))
            {
                MessageBox.Show("Please select or enter a schema!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string fileName = GetMigrationFileName(migrationName, targetSchema);
            string filePath = Path.Combine(PendingPath, fileName);

            string author = txtAuthor.Text.Trim();
            if (string.IsNullOrWhiteSpace(author)) author = CurrentUser;

            string comment = txtComment.Text.Trim();
            if (string.IsNullOrWhiteSpace(comment)) comment = $"Migration: {migrationName} for schema: {targetSchema}";

            string sqlCode = txtSql.Text.Trim();
            if (string.IsNullOrWhiteSpace(sqlCode))
            {
                var res = MessageBox.Show("SQL code is empty. Create migration without SQL code?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.No) return;
            }

            // Build migration content (same structure as preview)
            var sb = new StringBuilder();
            sb.AppendLine($"-- {fileName}");
            sb.AppendLine($"-- Schema: {targetSchema}");
            sb.AppendLine($"-- Author: {author}");
            sb.AppendLine($"-- Date: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
            sb.AppendLine($"-- Type: {comboType.SelectedItem}");
            sb.AppendLine();
            sb.AppendLine("DO $$");
            sb.AppendLine("DECLARE");
            sb.AppendLine("    start_time TIMESTAMP;");
            sb.AppendLine($"    migration_id TEXT := '{fileName.Replace(".sql", string.Empty)}';");
            sb.AppendLine($"    migration_desc TEXT := '{EscapeForSqlLiteral(comment)}';");
            sb.AppendLine($"    target_schema TEXT := '{EscapeForSqlLiteral(targetSchema)}';");
            sb.AppendLine("BEGIN");
            sb.AppendLine("    start_time := clock_timestamp();");
            sb.AppendLine("    RAISE NOTICE 'Applying migration % to schema: %', migration_id, target_schema;");
            sb.AppendLine();
            sb.AppendLine("    -- === SQL MIGRATION CODE ===");
            sb.AppendLine();
            sb.AppendLine(sqlCode);
            sb.AppendLine();
            sb.AppendLine("    -- === END SQL CODE ===");
            sb.AppendLine();
            sb.AppendLine("    INSERT INTO migrations.db_migrations (");
            sb.AppendLine("        migration_name,");
            sb.AppendLine("        schema_name,");
            sb.AppendLine("        notes,");
            sb.AppendLine("        execution_time_ms");
            sb.AppendLine("    ) VALUES (");
            sb.AppendLine("        migration_id,");
            sb.AppendLine("        target_schema,");
            sb.AppendLine("        migration_desc,");
            sb.AppendLine("        EXTRACT(EPOCH FROM (clock_timestamp() - start_time)) * 1000");
            sb.AppendLine("    );");
            sb.AppendLine();
            sb.AppendLine("    RAISE NOTICE 'Migration % for schema % completed successfully', migration_id, target_schema;");
            sb.AppendLine();
            sb.AppendLine("EXCEPTION");
            sb.AppendLine("    WHEN others THEN");
            sb.AppendLine("        RAISE EXCEPTION 'Error in migration % for schema %: %', migration_id, target_schema, SQLERRM;");
            sb.AppendLine("END $$;");

            try
            {
                File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);

                // Save schema config if not exists
                string schemaConfigPath = Path.Combine(SchemasPath, $"{GetSafeName(targetSchema)}.txt");
                if (!File.Exists(schemaConfigPath))
                {
                    var cfgSb = new StringBuilder();
                    cfgSb.AppendLine($"Schema: {targetSchema}");
                    cfgSb.AppendLine($"Created: {DateTime.Now:yyyy-MM-dd}");
                    cfgSb.AppendLine($"Description: Database schema for {targetSchema}");
                    File.WriteAllText(schemaConfigPath, cfgSb.ToString(), Encoding.UTF8);
                }

                MessageBox.Show($"Migration created successfully!\n\nFile: {fileName}\nSchema: {targetSchema}\nFolder: {PendingPath}\n\nNext steps:\n1. Ensure migrations schema exists\n2. Apply in DBeaver or psql\n3. Check migrations.db_migrations table", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Open folder and file in notepad
                try
                {
                    Process.Start(new ProcessStartInfo("explorer.exe", $"\"{PendingPath}\"") { UseShellExecute = true });
                    Process.Start(new ProcessStartInfo("notepad.exe", $"\"{filePath}\"") { UseShellExecute = true });
                }
                catch
                {
                    // ignore process start errors
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving file:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtSql.Text = @"-- Write your SQL code here
-- Use schema prefix for all objects: {schema}.table_name
-- Example: CREATE TABLE IF NOT EXISTS {schema}.table_name (...)";
            txtComment.Text = "Added new field for storing information";
            comboSchema.SelectedIndex = 0;
            comboType.SelectedIndex = 0;
            txtCustomSchema.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnGenerateInit_Click(object sender, EventArgs e)
        {
            string initScript = @"-- =============================================
-- INITIALIZATION SCRIPT FOR MIGRATIONS SYSTEM
-- =============================================

-- 1. Create migrations schema
CREATE SCHEMA IF NOT EXISTS migrations;

-- 2. Create central migrations table
CREATE TABLE IF NOT EXISTS migrations.db_migrations (
    id SERIAL PRIMARY KEY,
    migration_name VARCHAR(255) NOT NULL,
    schema_name VARCHAR(50) NOT NULL,
    applied_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    applied_by VARCHAR(100) DEFAULT CURRENT_USER,
    execution_time_ms INTEGER,
    checksum VARCHAR(64),
    success BOOLEAN DEFAULT TRUE,
    notes TEXT,
    
    -- Unique constraint for migration name and schema
    CONSTRAINT unique_migration_schema UNIQUE (migration_name, schema_name)
);

-- 3. Create indexes for performance
CREATE INDEX IF NOT EXISTS idx_migrations_schema 
ON migrations.db_migrations(schema_name);

-- 4. Create comment
COMMENT ON SCHEMA migrations IS 'Миграции';
COMMENT ON TABLE migrations.db_migrations IS 'Центральная таблица со всеми миграциями в базе';

RAISE NOTICE 'Migrations system initialized successfully!';";

            string initPath = Path.Combine(MigrationsPath, "init_migrations_system.sql");
            try
            {
                File.WriteAllText(initPath, initScript, Encoding.UTF8);
                MessageBox.Show($"Initialization script generated!\n\nFile: {initPath}\n\nRun this script ONCE in your database to set up the migrations system.", "Init Script Generated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                try
                {
                    Process.Start(new ProcessStartInfo("notepad.exe", $"\"{initPath}\"") { UseShellExecute = true });
                }
                catch { }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating init script:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoadTemplate_Click(object sender, EventArgs e)
        {
            if (comboTemplates.SelectedIndex > 0)
            {
                string selected = comboTemplates.SelectedItem.ToString();
                string path = Path.Combine(TemplatesPath, selected);
                try
                {
                    string content = File.ReadAllText(path, Encoding.UTF8);
                    txtSql.Text = content;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Unable to load template: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion
    }
}
