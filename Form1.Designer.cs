using System.Windows.Forms;
using ScintillaNET;

namespace PostgresMigrations
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private Label labelSchema;
        private ComboBox comboSchema;
        private Label labelCustomSchema;
        private TextBox txtCustomSchema;
        private Label section2Label;
        private Label labelName;
        private TextBox txtName;
        private Label labelAuthor;
        private TextBox txtAuthor;
        private Label labelComment;
        private TextBox txtComment;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.labelSchema = new System.Windows.Forms.Label();
            this.comboSchema = new System.Windows.Forms.ComboBox();
            this.labelCustomSchema = new System.Windows.Forms.Label();
            this.txtCustomSchema = new System.Windows.Forms.TextBox();
            this.section2Label = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.labelAuthor = new System.Windows.Forms.Label();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.labelComment = new System.Windows.Forms.Label();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtSqlUp = new ScintillaNET.Scintilla();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtSqlDown = new ScintillaNET.Scintilla();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.createMigrationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createMirgationFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previewToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.generateSchemaInitScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelSchema
            // 
            this.labelSchema.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.labelSchema.Location = new System.Drawing.Point(20, 45);
            this.labelSchema.Name = "labelSchema";
            this.labelSchema.Size = new System.Drawing.Size(200, 25);
            this.labelSchema.TabIndex = 2;
            this.labelSchema.Text = "1. Target Schema:";
            // 
            // comboSchema
            // 
            this.comboSchema.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSchema.Location = new System.Drawing.Point(200, 45);
            this.comboSchema.Name = "comboSchema";
            this.comboSchema.Size = new System.Drawing.Size(178, 25);
            this.comboSchema.TabIndex = 3;
            this.comboSchema.SelectedIndexChanged += new System.EventHandler(this.comboSchema_SelectedIndexChanged);
            // 
            // labelCustomSchema
            // 
            this.labelCustomSchema.Location = new System.Drawing.Point(392, 45);
            this.labelCustomSchema.Name = "labelCustomSchema";
            this.labelCustomSchema.Size = new System.Drawing.Size(114, 25);
            this.labelCustomSchema.TabIndex = 4;
            this.labelCustomSchema.Text = "Custom schema:";
            this.labelCustomSchema.Visible = false;
            // 
            // txtCustomSchema
            // 
            this.txtCustomSchema.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCustomSchema.Location = new System.Drawing.Point(507, 44);
            this.txtCustomSchema.Name = "txtCustomSchema";
            this.txtCustomSchema.Size = new System.Drawing.Size(150, 25);
            this.txtCustomSchema.TabIndex = 5;
            this.txtCustomSchema.Visible = false;
            // 
            // section2Label
            // 
            this.section2Label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.section2Label.Location = new System.Drawing.Point(20, 85);
            this.section2Label.Name = "section2Label";
            this.section2Label.Size = new System.Drawing.Size(300, 25);
            this.section2Label.TabIndex = 6;
            this.section2Label.Text = "2. Migration Description";
            // 
            // labelName
            // 
            this.labelName.Location = new System.Drawing.Point(40, 120);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(150, 25);
            this.labelName.TabIndex = 7;
            this.labelName.Text = "Migration Name:";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtName.Location = new System.Drawing.Point(200, 120);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(457, 23);
            this.txtName.TabIndex = 8;
            // 
            // labelAuthor
            // 
            this.labelAuthor.Location = new System.Drawing.Point(40, 160);
            this.labelAuthor.Name = "labelAuthor";
            this.labelAuthor.Size = new System.Drawing.Size(150, 25);
            this.labelAuthor.TabIndex = 10;
            this.labelAuthor.Text = "Author:";
            // 
            // txtAuthor
            // 
            this.txtAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAuthor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAuthor.Location = new System.Drawing.Point(200, 160);
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.Size = new System.Drawing.Size(457, 25);
            this.txtAuthor.TabIndex = 11;
            // 
            // labelComment
            // 
            this.labelComment.Location = new System.Drawing.Point(40, 200);
            this.labelComment.Name = "labelComment";
            this.labelComment.Size = new System.Drawing.Size(150, 25);
            this.labelComment.TabIndex = 12;
            this.labelComment.Text = "Comment:";
            // 
            // txtComment
            // 
            this.txtComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComment.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtComment.Location = new System.Drawing.Point(200, 200);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(457, 25);
            this.txtComment.TabIndex = 13;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.tabControl1.Location = new System.Drawing.Point(0, 240);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(984, 469);
            this.tabControl1.TabIndex = 20;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtSqlUp);
            this.tabPage1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(976, 439);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "3. SQL Migration Code (UP)";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtSqlUp
            // 
            this.txtSqlUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSqlUp.Location = new System.Drawing.Point(3, 3);
            this.txtSqlUp.Name = "txtSqlUp";
            this.txtSqlUp.Size = new System.Drawing.Size(970, 433);
            this.txtSqlUp.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtSqlDown);
            this.tabPage2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(976, 439);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "4. SQL Migration Code (DOWN)";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtSqlDown
            // 
            this.txtSqlDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSqlDown.Location = new System.Drawing.Point(3, 3);
            this.txtSqlDown.Name = "txtSqlDown";
            this.txtSqlDown.Size = new System.Drawing.Size(970, 433);
            this.txtSqlDown.TabIndex = 4;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createMigrationToolStripMenuItem,
            this.previewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(984, 24);
            this.menuStrip1.TabIndex = 21;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // createMigrationToolStripMenuItem
            // 
            this.createMigrationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createMirgationFilesToolStripMenuItem,
            this.previewToolStripMenuItem1,
            this.generateSchemaInitScriptToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.createMigrationToolStripMenuItem.Name = "createMigrationToolStripMenuItem";
            this.createMigrationToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.createMigrationToolStripMenuItem.Text = "File";
            // 
            // createMirgationFilesToolStripMenuItem
            // 
            this.createMirgationFilesToolStripMenuItem.Name = "createMirgationFilesToolStripMenuItem";
            this.createMirgationFilesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.createMirgationFilesToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.createMirgationFilesToolStripMenuItem.Text = "Create migrations files";
            this.createMirgationFilesToolStripMenuItem.Click += new System.EventHandler(this.createMirgationFilesToolStripMenuItem_Click);
            // 
            // previewToolStripMenuItem1
            // 
            this.previewToolStripMenuItem1.Name = "previewToolStripMenuItem1";
            this.previewToolStripMenuItem1.Size = new System.Drawing.Size(259, 22);
            this.previewToolStripMenuItem1.Text = "Preview migration";
            this.previewToolStripMenuItem1.Visible = false;
            this.previewToolStripMenuItem1.Click += new System.EventHandler(this.previewToolStripMenuItem1_Click);
            // 
            // generateSchemaInitScriptToolStripMenuItem
            // 
            this.generateSchemaInitScriptToolStripMenuItem.Name = "generateSchemaInitScriptToolStripMenuItem";
            this.generateSchemaInitScriptToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D)));
            this.generateSchemaInitScriptToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.generateSchemaInitScriptToolStripMenuItem.Text = "Generate schema init script";
            this.generateSchemaInitScriptToolStripMenuItem.Click += new System.EventHandler(this.generateSchemaInitScriptToolStripMenuItem_Click);
            // 
            // previewToolStripMenuItem
            // 
            this.previewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem});
            this.previewToolStripMenuItem.Name = "previewToolStripMenuItem";
            this.previewToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.previewToolStripMenuItem.Text = "Edit";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 712);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(984, 22);
            this.statusStrip1.TabIndex = 22;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(256, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(984, 734);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtCustomSchema);
            this.Controls.Add(this.comboSchema);
            this.Controls.Add(this.labelCustomSchema);
            this.Controls.Add(this.labelSchema);
            this.Controls.Add(this.section2Label);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.labelAuthor);
            this.Controls.Add(this.txtAuthor);
            this.Controls.Add(this.labelComment);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Database Migration Creator v1.1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private TabControl tabControl1;
        private TabPage tabPage1;
        private Scintilla txtSqlUp;
        private TabPage tabPage2;
        private Scintilla txtSqlDown;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem createMigrationToolStripMenuItem;
        private ToolStripMenuItem createMirgationFilesToolStripMenuItem;
        private ToolStripMenuItem previewToolStripMenuItem1;
        private ToolStripMenuItem generateSchemaInitScriptToolStripMenuItem;
        private ToolStripMenuItem previewToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripMenuItem clearToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem exitToolStripMenuItem;
    }
}