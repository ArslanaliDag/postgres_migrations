using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScintillaNET;

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
            comboSchema.Items.AddRange(new object[] { "policyregistry", "users_schema", "public", "custom" });
            comboSchema.SelectedIndex = 0;
            comboType.Items.AddRange(new object[] {
                "CREATE TABLE - Create new table",
                "ALTER TABLE - Modify table",
                "CREATE FUNCTION - Create function",
                "CREATE INDEX - Create index",
                "CREATE SCHEMA - Create schema",
                "DATA MIGRATION - Data migration",
                "CUSTOM - Custom SQL" });
            comboType.SelectedIndex = 0;

            txtAuthor.Text = CurrentUser;
            txtName.Text = "add_new_column";
            txtComment.Text = "Added new field for storing information";

            // Configure Scintilla for SQL syntax highlighting
            ConfigureScintillaEditor(txtSqlUp);
            ConfigureScintillaEditor(txtSqlDown);

            // Set default SQL content
            txtSqlUp.Text = @"-- Write UP migration here
-- Example: CREATE TABLE {schema}.new_table (...);";

            txtSqlDown.Text = @"-- Write DOWN migration here
-- Example: DROP TABLE {schema}.new_table;";
        }

        private void ConfigureScintillaEditor(Scintilla scintilla)
        {
            // Reset default styles
            scintilla.StyleResetDefault();

            // Configure basic editor settings
            scintilla.Lexer = Lexer.Sql;
            scintilla.Margins[0].Width = 16; // Line number margin
            scintilla.Margins[0].Type = MarginType.Number;

            // Configure SQL lexer styles
            scintilla.Styles[Style.Sql.Identifier].ForeColor = System.Drawing.Color.Black;
            scintilla.Styles[Style.Sql.String].ForeColor = System.Drawing.Color.DarkRed;
            scintilla.Styles[Style.Sql.QuotedIdentifier].ForeColor = System.Drawing.Color.DarkGreen;
            scintilla.Styles[Style.Sql.Comment].ForeColor = System.Drawing.Color.Green;
            scintilla.Styles[Style.Sql.CommentLine].ForeColor = System.Drawing.Color.Green;
            scintilla.Styles[Style.Sql.CommentDoc].ForeColor = System.Drawing.Color.Green;
            scintilla.Styles[Style.Sql.Number].ForeColor = System.Drawing.Color.DarkOrange;
            scintilla.Styles[Style.Sql.Word].ForeColor = System.Drawing.Color.Blue;
            scintilla.Styles[Style.Sql.Word2].ForeColor = System.Drawing.Color.DarkBlue;
            scintilla.Styles[Style.Sql.Operator].ForeColor = System.Drawing.Color.DarkMagenta;

            // Enable line wrapping
            scintilla.WrapMode = WrapMode.Word;

            // Show line numbers
            scintilla.Margins[1].Width = 0; // Disable fold margin

            // Set font
            scintilla.Font = new System.Drawing.Font("Consolas", 10);

            // Enable code folding
            scintilla.SetProperty("fold", "1");
            scintilla.SetProperty("fold.compact", "1");
            scintilla.SetProperty("fold.sql", "1");

            // Set folding markers
            scintilla.Markers[Marker.Folder].Symbol = MarkerSymbol.BoxPlus;
            scintilla.Markers[Marker.FolderOpen].Symbol = MarkerSymbol.BoxMinus;
            scintilla.Markers[Marker.FolderEnd].Symbol = MarkerSymbol.BoxPlusConnected;
            scintilla.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            scintilla.Markers[Marker.FolderOpenMid].Symbol = MarkerSymbol.BoxMinusConnected;
            scintilla.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            scintilla.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

            // Auto-indentation settings
            scintilla.IndentationGuides = IndentView.LookBoth;
            scintilla.TabWidth = 4;
            scintilla.UseTabs = false; // Используем пробелы вместо табуляции
            scintilla.IndentWidth = 4;

            // Configure auto-indentation
            scintilla.AutoCIgnoreCase = true;

            // Braces highlighting
            scintilla.Styles[Style.BraceLight].BackColor = System.Drawing.Color.LightGray;
            scintilla.Styles[Style.BraceLight].ForeColor = System.Drawing.Color.Black;
            scintilla.Styles[Style.BraceBad].ForeColor = System.Drawing.Color.Red;

            // Selection color
            scintilla.SetSelectionBackColor(true, System.Drawing.Color.LightSteelBlue);

            // Caret settings
            scintilla.CaretForeColor = System.Drawing.Color.Black;
            scintilla.CaretLineVisible = true;
            scintilla.CaretLineBackColor = System.Drawing.Color.FromArgb(240, 240, 255);

            // Enable right margin at 80 chars
            scintilla.Margins[2].Width = 1;
            scintilla.Margins[2].Type = MarginType.Color;
            scintilla.Margins[2].BackColor = System.Drawing.Color.LightGray;

            // Set right margin at 120 chars
            scintilla.Margins[3].Width = 1;
            scintilla.Margins[3].Type = MarginType.Color;
            scintilla.Margins[3].BackColor = System.Drawing.Color.FromArgb(255, 200, 200);

            // Set SQL keywords
            string sqlKeywords =
                "SELECT INSERT UPDATE DELETE CREATE ALTER DROP TRUNCATE TABLE " +
                "FROM WHERE AND OR NOT LIKE IN BETWEEN IS NULL " +
                "ORDER BY GROUP BY HAVING JOIN INNER LEFT RIGHT FULL OUTER " +
                "ON AS CASE WHEN THEN ELSE END " +
                "UNION INTERSECT EXCEPT DISTINCT ALL " +
                "VALUES SET INTO " +
                "BEGIN END COMMIT ROLLBACK SAVEPOINT " +
                "FUNCTION PROCEDURE TRIGGER VIEW INDEX SEQUENCE " +
                "PRIMARY KEY FOREIGN KEY REFERENCES CONSTRAINT " +
                "INTEGER VARCHAR TEXT CHAR BOOLEAN DATE TIMESTAMP NUMERIC DECIMAL " +
                "TRUE FALSE NULL " +
                "IF EXISTS IF NOT EXISTS " +
                "CASCADE RESTRICT";

            scintilla.SetKeywords(0, sqlKeywords);

            // Set PostgreSQL specific keywords
            string sqlKeywords2 =
                "SERIAL BIGSERIAL JSONB UUID " +
                "CURRENT_TIMESTAMP CURRENT_DATE CURRENT_TIME " +
                "NOW() " +
                "RETURNS LANGUAGE PLPGSQL " +
                "EXECUTE RAISE NOTICE EXCEPTION " +
                "DO $$ $$ " +
                "CONCURRENTLY " +
                "ADD COLUMN DROP COLUMN RENAME COLUMN " +
                "ALTER COLUMN TYPE " +
                "WITHOUT TIME ZONE WITH TIME ZONE " +
                "DEFAULT NOT NULL UNIQUE CHECK " +
                "COMMENT ON TABLE COMMENT ON COLUMN " +
                "GRANT REVOKE";

            scintilla.SetKeywords(1, sqlKeywords2);

            // Set margins for better appearance
            scintilla.Margins[0].Width = 50;
            scintilla.Margins[0].Sensitive = true;
            scintilla.Margins[0].Type = MarginType.Number;

            // Show current line indicator
            scintilla.CaretLineVisible = true;
            scintilla.CaretLineBackColor = System.Drawing.Color.FromArgb(255, 255, 240);

            // Show white space (optional)
            scintilla.ViewWhitespace = WhitespaceMode.Invisible;

            // Enable scrolling
            scintilla.ScrollWidth = 1;
            scintilla.ScrollWidthTracking = true;

            // Enable auto-completion (optional)
            scintilla.AutoCChooseSingle = true;
            scintilla.AutoCIgnoreCase = true;
            scintilla.AutoCMaxHeight = 10;

            // Enable right-click context menu
            var contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Cut", null, (s, e) => scintilla.Cut());
            contextMenu.Items.Add("Copy", null, (s, e) => scintilla.Copy());
            contextMenu.Items.Add("Paste", null, (s, e) => scintilla.Paste());
            contextMenu.Items.Add("-");
            contextMenu.Items.Add("Select All", null, (s, e) => scintilla.SelectAll());
            contextMenu.Items.Add("-");
            contextMenu.Items.Add("Comment Line", null, (s, e) => CommentLine(scintilla));
            contextMenu.Items.Add("Uncomment Line", null, (s, e) => UncommentLine(scintilla));
            contextMenu.Items.Add("-");
            contextMenu.Items.Add("Format SQL", null, (s, e) => FormatSql(scintilla));

            scintilla.ContextMenuStrip = contextMenu;

            // Add key handler for auto-indentation
            scintilla.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    // Get current line
                    int currentLine = scintilla.LineFromPosition(scintilla.CurrentPosition);
                    string lineText = scintilla.Lines[currentLine].Text;

                    // Count leading spaces/tabs
                    int indentLevel = 0;
                    foreach (char c in lineText)
                    {
                        if (c == ' ' || c == '\t')
                            indentLevel++;
                        else
                            break;
                    }

                    // Insert new line with same indentation
                    scintilla.ReplaceSelection("\n" + new string(' ', indentLevel));
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Tab && !e.Shift)
                {
                    // Insert 4 spaces on Tab
                    scintilla.ReplaceSelection(new string(' ', 4));
                    e.Handled = true;
                }
            };
        }

        private void CommentLine(Scintilla scintilla)
        {
            int startPos = scintilla.SelectionStart;
            int endPos = scintilla.SelectionEnd;

            scintilla.ReplaceSelection("-- " + scintilla.GetTextRange(startPos, endPos - startPos));
        }

        private void UncommentLine(Scintilla scintilla)
        {
            string selectedText = scintilla.SelectedText;
            if (selectedText.StartsWith("-- "))
            {
                scintilla.ReplaceSelection(selectedText.Substring(3));
            }
            else if (selectedText.StartsWith("--"))
            {
                scintilla.ReplaceSelection(selectedText.Substring(2));
            }
        }

        private void FormatSql(Scintilla scintilla)
        {
            // Basic SQL formatting - you can enhance this with a proper SQL formatter
            string text = scintilla.Text;

            // Format common SQL patterns
            text = text.Replace("SELECT ", "\nSELECT ")
                      .Replace(" FROM ", "\nFROM ")
                      .Replace(" WHERE ", "\nWHERE ")
                      .Replace(" AND ", "\n    AND ")
                      .Replace(" OR ", "\n    OR ")
                      .Replace(" ORDER BY ", "\nORDER BY ")
                      .Replace(" GROUP BY ", "\nGROUP BY ")
                      .Replace(" HAVING ", "\nHAVING ")
                      .Replace(" INSERT INTO ", "\nINSERT INTO ")
                      .Replace(" VALUES ", "\nVALUES ")
                      .Replace(" UPDATE ", "\nUPDATE ")
                      .Replace(" SET ", "\nSET ")
                      .Replace(" DELETE FROM ", "\nDELETE FROM ")
                      .Replace(" CREATE TABLE ", "\nCREATE TABLE ")
                      .Replace(" ALTER TABLE ", "\nALTER TABLE ")
                      .Replace(" DROP TABLE ", "\nDROP TABLE ");

            scintilla.Text = text;
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
                if (char.IsLetterOrDigit(c) || c == '_')
                    sb.Append(c);
                else
                    sb.Append('_');
            }
            return sb.ToString();
        }

        //private string GetMigrationFileName(string name, string schema)
        //{
        //    string date = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        //    string safeName = GetSafeName(name);
        //    string safeSchema = GetSafeName(schema);
        //    //return $"{date}_{safeSchema}_{safeName}.sql";
        //    return $"{date}_{safeSchema}_{safeName}"; // Без .sql в конце
        //}

        private string GetMigrationFileName(string name, string schema)
        {
            string date = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string safeName = GetSafeName(name);

            // Формат: yyyyMMdd_HHmmss_migrationname
            return $"{date}_{safeName}";
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
        SELECT 1 
        FROM {placeholder}.target_table tt
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
                MessageBox.Show("Please select or enter a schema!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var template = GetSqlTemplateByType(
                comboType.SelectedItem?.ToString() ?? "CUSTOM",
                target);

            template = template.Replace("{schema}", target);

            txtSqlUp.Text = template;
            txtSqlDown.Text = "-- Write DOWN migration here\n-- Inverse of UP script";
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            string migrationName = txtName.Text.Trim();
            if (string.IsNullOrWhiteSpace(migrationName))
            {
                MessageBox.Show("Please enter migration name!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string targetSchema = GetTargetSchema();
            if (string.IsNullOrWhiteSpace(targetSchema))
            {
                MessageBox.Show("Please select or enter a schema!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string baseName = GetMigrationFileName(migrationName, targetSchema)
                .Replace(".sql", "");
            string fileUp = baseName + "_UP.sql";
            string fileDown = baseName + "_DOWN.sql";

            string author = txtAuthor.Text.Trim();
            string comment = txtComment.Text.Trim();
            string sqlUp = txtSqlUp.Text;
            string sqlDown = txtSqlDown.Text;

            var sb = new StringBuilder();
            sb.AppendLine("=============================================");
            sb.AppendLine(" MIGRATION PREVIEW (UP/DOWN)");
            sb.AppendLine("=============================================");
            sb.AppendLine($"UP File: {fileUp}");
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

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string migrationName = txtName.Text.Trim();
            if (string.IsNullOrWhiteSpace(migrationName))
            {
                MessageBox.Show("Please enter migration name!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string targetSchema = GetTargetSchema();
            if (string.IsNullOrWhiteSpace(targetSchema))
            {
                MessageBox.Show("Please select or enter a schema!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string baseName = GetMigrationFileName(migrationName, targetSchema)
                .Replace(".sql", "");
            string upFile = Path.Combine(PendingPath, baseName + "_UP.sql");
            string downFile = Path.Combine(PendingPath, baseName + "_DOWN.sql");

            string author = txtAuthor.Text.Trim();
            if (string.IsNullOrWhiteSpace(author)) author = CurrentUser;

            string comment = txtComment.Text.Trim();
            if (string.IsNullOrWhiteSpace(comment)) comment = $"Migration: {migrationName} for schema: {targetSchema}";

            string sqlUp = txtSqlUp.Text.Trim();
            string sqlDown = txtSqlDown.Text.Trim();

            if (string.IsNullOrWhiteSpace(sqlUp))
            {
                MessageBox.Show("UP SQL is empty!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                // Build UP migration content
                var sbUp = new StringBuilder();
                sbUp.AppendLine($"-- {baseName}_UP.sql");
                sbUp.AppendLine($"-- Schema: {targetSchema}");
                sbUp.AppendLine($"-- Author: {author}");
                sbUp.AppendLine($"-- Date: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
                sbUp.AppendLine($"-- Type: {comboType.SelectedItem}");
                sbUp.AppendLine($"-- Description: {comment}");
                sbUp.AppendLine();
                sbUp.AppendLine("DO $$");
                sbUp.AppendLine("DECLARE");
                sbUp.AppendLine("    start_time TIMESTAMP;");
                sbUp.AppendLine($"    migration_id TEXT := '{baseName}_UP';");
                sbUp.AppendLine($"    migration_desc TEXT := '{EscapeForSqlLiteral(comment)}';");
                sbUp.AppendLine($"    target_schema TEXT := '{EscapeForSqlLiteral(targetSchema)}';");
                sbUp.AppendLine("BEGIN");
                sbUp.AppendLine("    start_time := clock_timestamp();");
                sbUp.AppendLine("    RAISE NOTICE 'Applying UP migration % to schema: %', migration_id, target_schema;");
                sbUp.AppendLine();
                sbUp.AppendLine("    -- === UP SQL MIGRATION CODE ===");
                sbUp.AppendLine();
                sbUp.AppendLine(sqlUp);
                sbUp.AppendLine();
                sbUp.AppendLine("    -- === END UP SQL CODE ===");
                sbUp.AppendLine();
                sbUp.AppendLine("    INSERT INTO migrations.db_migrations (");
                sbUp.AppendLine("        migration_name,");
                sbUp.AppendLine("        schema_name,");
                sbUp.AppendLine("        notes,");
                sbUp.AppendLine("        execution_time_ms");
                sbUp.AppendLine("    ) VALUES (");
                sbUp.AppendLine("        migration_id,");
                sbUp.AppendLine("        target_schema,");
                sbUp.AppendLine("        migration_desc || ' (UP)',");
                sbUp.AppendLine("        EXTRACT(EPOCH FROM (clock_timestamp() - start_time)) * 1000");
                sbUp.AppendLine("    );");
                sbUp.AppendLine();
                sbUp.AppendLine("    RAISE NOTICE 'UP migration % for schema % completed successfully', migration_id, target_schema;");
                sbUp.AppendLine();
                sbUp.AppendLine("EXCEPTION");
                sbUp.AppendLine("    WHEN others THEN");
                sbUp.AppendLine("        RAISE EXCEPTION 'Error in UP migration % for schema %: %', migration_id, target_schema, SQLERRM;");
                sbUp.AppendLine("END $$;");

                // Build DOWN migration content
                var sbDown = new StringBuilder();
                sbDown.AppendLine($"-- {baseName}_DOWN.sql");
                sbDown.AppendLine($"-- Schema: {targetSchema}");
                sbDown.AppendLine($"-- Author: {author}");
                sbDown.AppendLine($"-- Date: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
                sbDown.AppendLine($"-- Type: {comboType.SelectedItem}");
                sbDown.AppendLine($"-- Description: {comment} (ROLLBACK)");
                sbDown.AppendLine();
                sbDown.AppendLine("DO $$");
                sbDown.AppendLine("DECLARE");
                sbDown.AppendLine("    start_time TIMESTAMP;");
                sbDown.AppendLine($"    migration_id TEXT := '{baseName}_DOWN';");
                sbDown.AppendLine($"    migration_desc TEXT := '{EscapeForSqlLiteral(comment)}';");
                sbDown.AppendLine($"    target_schema TEXT := '{EscapeForSqlLiteral(targetSchema)}';");
                sbDown.AppendLine("BEGIN");
                sbDown.AppendLine("    start_time := clock_timestamp();");
                sbDown.AppendLine("    RAISE NOTICE 'Applying DOWN migration (rollback) % to schema: %', migration_id, target_schema;");
                sbDown.AppendLine();
                sbDown.AppendLine("    -- === DOWN SQL MIGRATION CODE (ROLLBACK) ===");
                sbDown.AppendLine();
                sbDown.AppendLine(sqlDown);
                sbDown.AppendLine();
                sbDown.AppendLine("    -- === END DOWN SQL CODE ===");
                sbDown.AppendLine();
                sbDown.AppendLine("    -- Insert rollback record");
                sbDown.AppendLine("    INSERT INTO migrations.db_migrations (");
                sbDown.AppendLine("        migration_name,");
                sbDown.AppendLine("        schema_name,");
                sbDown.AppendLine("        notes,");
                sbDown.AppendLine("        execution_time_ms,");
                sbDown.AppendLine("        success");
                sbDown.AppendLine("    ) VALUES (");
                sbDown.AppendLine("        migration_id,");
                sbDown.AppendLine("        target_schema,");
                sbDown.AppendLine("        migration_desc || ' (DOWN/ROLLBACK applied)',");
                sbDown.AppendLine("        EXTRACT(EPOCH FROM (clock_timestamp() - start_time)) * 1000,");
                sbDown.AppendLine("        true"); // Можете установить false если хотите отметить как неудачную операцию
                sbDown.AppendLine("    );");
                sbDown.AppendLine();
                sbDown.AppendLine("    RAISE NOTICE 'DOWN migration (rollback) % for schema % completed', migration_id, target_schema;");
                sbDown.AppendLine();
                sbDown.AppendLine("EXCEPTION");
                sbDown.AppendLine("    WHEN others THEN");
                sbDown.AppendLine("        RAISE EXCEPTION 'Error in DOWN migration % for schema %: %', migration_id, target_schema, SQLERRM;");
                sbDown.AppendLine("END $$;");

                // Save files
                File.WriteAllText(upFile, sbUp.ToString(), Encoding.UTF8);
                File.WriteAllText(downFile, sbDown.ToString(), Encoding.UTF8);

                // Save schema config if not exists
                string schemaConfigPath = Path.Combine(SchemasPath, $"{GetSafeName(targetSchema)}.txt");
                if (!File.Exists(schemaConfigPath))
                {
                    var cfgSb = new StringBuilder();
                    cfgSb.AppendLine($"Schema: {targetSchema}");
                    cfgSb.AppendLine($"Created: {DateTime.Now:yyyy-MM-dd}");
                    cfgSb.AppendLine($"Description: Database schema for {targetSchema}");
                    cfgSb.AppendLine($"Last migration: {baseName}");
                    cfgSb.AppendLine($"Migration count: 1");
                    File.WriteAllText(schemaConfigPath, cfgSb.ToString(), Encoding.UTF8);
                }
                else
                {
                    // Update existing config
                    var lines = File.ReadAllLines(schemaConfigPath).ToList();
                    bool foundLastMigration = false;
                    bool foundMigrationCount = false;

                    for (int i = 0; i < lines.Count; i++)
                    {
                        if (lines[i].StartsWith("Last migration:"))
                        {
                            lines[i] = $"Last migration: {baseName}";
                            foundLastMigration = true;
                        }
                        else if (lines[i].StartsWith("Migration count:"))
                        {
                            if (int.TryParse(lines[i].Split(':')[1].Trim(), out int count))
                            {
                                lines[i] = $"Migration count: {count + 1}";
                            }
                            foundMigrationCount = true;
                        }
                    }

                    if (!foundLastMigration)
                        lines.Add($"Last migration: {baseName}");
                    if (!foundMigrationCount)
                        lines.Add("Migration count: 1");

                    File.WriteAllLines(schemaConfigPath, lines, Encoding.UTF8);
                }

                MessageBox.Show(
                    $"Migration created successfully!\n\n" +
                    $"UP File: {Path.GetFileName(upFile)}\n" +
                    $"DOWN File: {Path.GetFileName(downFile)}\n" +
                    $"Schema: {targetSchema}\n" +
                    $"Folder: {PendingPath}\n\n" +
                    $"Next steps:\n" +
                    $"1. Ensure migrations schema exists\n" +
                    $"2. Apply UP migration in DBeaver or psql\n" +
                    $"3. Check migrations.db_migrations table\n" +
                    $"4. DOWN migration is for rollback only",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Open folder and UP file in notepad
                try
                {
                    Process.Start(new ProcessStartInfo("explorer.exe", $"\"{PendingPath}\"")
                    {
                        UseShellExecute = true
                    });

                    // Open only UP file (DOWN is for rollback)
                    //Process.Start(new ProcessStartInfo("notepad.exe", $"\"{upFile}\"")
                    //{
                    //    UseShellExecute = true
                    //});
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Migration created error!:\n{ex.Message}", "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving files:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string EscapeForSqlLiteral(string input)
        {
            if (input == null) return "";

            // Экранирование одинарных кавычек для SQL
            return input.Replace("'", "''");
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
CREATE INDEX IF NOT EXISTS idx_migrations_schema ON migrations.db_migrations(schema_name);

-- 4. Create comment
COMMENT ON SCHEMA migrations IS 'Миграции';
COMMENT ON TABLE migrations.db_migrations IS 'Центральная таблица со всеми миграциями в базе';

RAISE NOTICE 'Migrations system initialized successfully!';
";

            string initPath = Path.Combine(MigrationsPath, "init_migrations_system.sql");
            try
            {
                File.WriteAllText(initPath, initScript, Encoding.UTF8);
                MessageBox.Show(
                    $"Initialization script generated!\n\nFile: {initPath}\n\n" +
                    "Run this script ONCE in your database to set up the migrations system.",
                    "Init Script Generated",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                try
                {
                    Process.Start(new ProcessStartInfo("notepad.exe", $"\"{initPath}\"")
                    {
                        UseShellExecute = true
                    });
                }
                catch { }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating init script:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show($"Unable to load template: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion
    }
}