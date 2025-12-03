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

            // Ensure folders
            Directory.CreateDirectory(MigrationsPath);
            Directory.CreateDirectory(PendingPath);
            Directory.CreateDirectory(TemplatesPath);
            Directory.CreateDirectory(SchemasPath);

            LoadTemplates();

            // Initialize UI defaults
            comboSchema.Items.AddRange(new object[] {
        "policyregistry",
        "users_schema",
        "public",
        "custom"
    });
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

            // NEW — apply same font for both text areas
            var monofont = new System.Drawing.Font("Consolas", 10);
            txtSqlUp.Font = monofont;
            txtSqlDown.Font = monofont;

            // NEW — default UP/DOWN content
            txtSqlUp.Text =
        @"-- Write UP migration here
-- Example: CREATE TABLE {schema}.new_table (...);";

            txtSqlDown.Text =
        @"-- Write DOWN migration here
-- Example: DROP TABLE {schema}.new_table;";
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

            txtSqlUp.Text = template;
            txtSqlDown.Text = "-- Write DOWN migration here\n-- Inverse of UP script";
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

            string baseName = GetMigrationFileName(migrationName, targetSchema)
                .Replace(".sql", "");

            string fileUp = baseName + "_UP.sql";
            string fileDown = baseName + "_DOWN.sql";

            string author = txtAuthor.Text.Trim();
            string comment = txtComment.Text.Trim();

            string sqlUp = txtSqlUp.Text.Trim();
            string sqlDown = txtSqlDown.Text.Trim();

            var sb = new StringBuilder();
            sb.AppendLine("=============================================");
            sb.AppendLine("         MIGRATION PREVIEW (UP/DOWN)");
            sb.AppendLine("=============================================");
            sb.AppendLine($"UP File:   {fileUp}");
            sb.AppendLine($"DOWN File: {fileDown}");
            sb.AppendLine($"Schema: {targetSchema}");
            sb.AppendLine($"Author: {author}");
            sb.AppendLine($"Comment: {comment}");
            sb.AppendLine("=============================================");

            sb.AppendLine("\n========== UP SQL ==========\n");
            sb.AppendLine(sqlUp);

            sb.AppendLine("\n========== DOWN SQL ==========\n");
            sb.AppendLine(sqlDown);

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

            string baseName = GetMigrationFileName(migrationName, targetSchema)
                .Replace(".sql", "");

            string upFile = Path.Combine(PendingPath, baseName + "_UP.sql");
            string downFile = Path.Combine(PendingPath, baseName + "_DOWN.sql");

            string author = txtAuthor.Text.Trim();
            string comment = txtComment.Text.Trim();

            string sqlUp = txtSqlUp.Text.Trim();
            string sqlDown = txtSqlDown.Text.Trim();

            if (string.IsNullOrWhiteSpace(sqlUp))
            {
                MessageBox.Show("UP SQL is empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(sqlDown))
            {
                var r = MessageBox.Show(
                    "DOWN script is empty. Continue?",
                    "Warning",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (r == DialogResult.No) return;
            }

            try
            {
                File.WriteAllText(upFile, sqlUp, Encoding.UTF8);
                File.WriteAllText(downFile, sqlDown, Encoding.UTF8);

                MessageBox.Show(
                    $"Migration created!\n\nUP: {upFile}\nDOWN: {downFile}",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                Process.Start(new ProcessStartInfo("explorer.exe", $"\"{PendingPath}\"") { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving files:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtSqlUp.Text = @"-- Write UP migration here";
            txtSqlDown.Text = @"-- Write DOWN migration here";
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
                    txtSqlUp.Text = content;
                    txtSqlDown.Text = "-- Write DOWN migration here";
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
