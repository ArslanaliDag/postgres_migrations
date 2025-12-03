using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;

namespace PostgresMigrations
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private Label titleLabel;
        private Label separator1;

        private Label labelSchema;
        private ComboBox comboSchema;
        private Label labelCustomSchema;
        private TextBox txtCustomSchema;

        private Label section2Label;
        private Label labelName;
        private TextBox txtName;
        private Label nameHint;
        private Label labelAuthor;
        private TextBox txtAuthor;
        private Label labelComment;
        private TextBox txtComment;

        private Label section3Label;
        private ComboBox comboType;

        private Label section4UpLabel;
        private TextBox txtSqlUp;

        private Label section4DownLabel;
        private TextBox txtSqlDown;

        private Button btnInsertTemplate;
        private Panel buttonPanel;
        private Button btnCreate;
        private Button btnPreview;
        private Button btnClear;

        private Label labelLoadTemplate;
        private ComboBox comboTemplates;
        private Button btnLoadTemplate;
        private Button btnGenerateInit;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.titleLabel = new System.Windows.Forms.Label();
            this.separator1 = new System.Windows.Forms.Label();
            this.labelSchema = new System.Windows.Forms.Label();
            this.comboSchema = new System.Windows.Forms.ComboBox();
            this.labelCustomSchema = new System.Windows.Forms.Label();
            this.txtCustomSchema = new System.Windows.Forms.TextBox();
            this.section2Label = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.nameHint = new System.Windows.Forms.Label();
            this.labelAuthor = new System.Windows.Forms.Label();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.labelComment = new System.Windows.Forms.Label();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.section3Label = new System.Windows.Forms.Label();
            this.comboType = new System.Windows.Forms.ComboBox();
            this.btnInsertTemplate = new System.Windows.Forms.Button();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.btnGenerateInit = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.labelLoadTemplate = new System.Windows.Forms.Label();
            this.comboTemplates = new System.Windows.Forms.ComboBox();
            this.btnLoadTemplate = new System.Windows.Forms.Button();
            this.section4UpLabel = new System.Windows.Forms.Label();
            this.txtSqlUp = new System.Windows.Forms.TextBox();
            this.section4DownLabel = new System.Windows.Forms.Label();
            this.txtSqlDown = new System.Windows.Forms.TextBox();
            this.buttonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.DarkBlue;
            this.titleLabel.Location = new System.Drawing.Point(20, 12);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(940, 30);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "CREATE DATABASE MIGRATION";
            // 
            // separator1
            // 
            this.separator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.separator1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.separator1.Location = new System.Drawing.Point(20, 50);
            this.separator1.Name = "separator1";
            this.separator1.Size = new System.Drawing.Size(940, 2);
            this.separator1.TabIndex = 1;
            // 
            // labelSchema
            // 
            this.labelSchema.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.labelSchema.Location = new System.Drawing.Point(20, 70);
            this.labelSchema.Name = "labelSchema";
            this.labelSchema.Size = new System.Drawing.Size(200, 25);
            this.labelSchema.TabIndex = 2;
            this.labelSchema.Text = "Target Schema:";
            // 
            // comboSchema
            // 
            this.comboSchema.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSchema.Location = new System.Drawing.Point(200, 70);
            this.comboSchema.Name = "comboSchema";
            this.comboSchema.Size = new System.Drawing.Size(178, 25);
            this.comboSchema.TabIndex = 3;
            this.comboSchema.SelectedIndexChanged += new System.EventHandler(this.comboSchema_SelectedIndexChanged);
            // 
            // labelCustomSchema
            // 
            this.labelCustomSchema.Location = new System.Drawing.Point(392, 70);
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
            this.txtCustomSchema.Location = new System.Drawing.Point(507, 69);
            this.txtCustomSchema.Name = "txtCustomSchema";
            this.txtCustomSchema.Size = new System.Drawing.Size(150, 25);
            this.txtCustomSchema.TabIndex = 5;
            this.txtCustomSchema.Visible = false;
            // 
            // section2Label
            // 
            this.section2Label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.section2Label.Location = new System.Drawing.Point(20, 110);
            this.section2Label.Name = "section2Label";
            this.section2Label.Size = new System.Drawing.Size(300, 25);
            this.section2Label.TabIndex = 6;
            this.section2Label.Text = "2. Migration Description";
            // 
            // labelName
            // 
            this.labelName.Location = new System.Drawing.Point(40, 145);
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
            this.txtName.Location = new System.Drawing.Point(200, 145);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(400, 23);
            this.txtName.TabIndex = 8;
            // 
            // nameHint
            // 
            this.nameHint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nameHint.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nameHint.ForeColor = System.Drawing.Color.Gray;
            this.nameHint.Location = new System.Drawing.Point(610, 145);
            this.nameHint.Name = "nameHint";
            this.nameHint.Size = new System.Drawing.Size(300, 25);
            this.nameHint.TabIndex = 9;
            this.nameHint.Text = "English letters and underscores only";
            // 
            // labelAuthor
            // 
            this.labelAuthor.Location = new System.Drawing.Point(40, 185);
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
            this.txtAuthor.Location = new System.Drawing.Point(200, 185);
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.Size = new System.Drawing.Size(400, 25);
            this.txtAuthor.TabIndex = 11;
            // 
            // labelComment
            // 
            this.labelComment.Location = new System.Drawing.Point(40, 225);
            this.labelComment.Name = "labelComment";
            this.labelComment.Size = new System.Drawing.Size(150, 25);
            this.labelComment.TabIndex = 12;
            this.labelComment.Text = "Comment (notes):";
            // 
            // txtComment
            // 
            this.txtComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComment.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtComment.Location = new System.Drawing.Point(200, 225);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(650, 25);
            this.txtComment.TabIndex = 13;
            // 
            // section3Label
            // 
            this.section3Label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.section3Label.Location = new System.Drawing.Point(20, 265);
            this.section3Label.Name = "section3Label";
            this.section3Label.Size = new System.Drawing.Size(300, 25);
            this.section3Label.TabIndex = 14;
            this.section3Label.Text = "3. Migration Type";
            // 
            // comboType
            // 
            this.comboType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboType.Location = new System.Drawing.Point(40, 300);
            this.comboType.Name = "comboType";
            this.comboType.Size = new System.Drawing.Size(800, 25);
            this.comboType.TabIndex = 15;
            // 
            // btnInsertTemplate
            // 
            this.btnInsertTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnInsertTemplate.BackColor = System.Drawing.Color.LightGray;
            this.btnInsertTemplate.Location = new System.Drawing.Point(40, 745);
            this.btnInsertTemplate.Name = "btnInsertTemplate";
            this.btnInsertTemplate.Size = new System.Drawing.Size(150, 30);
            this.btnInsertTemplate.TabIndex = 18;
            this.btnInsertTemplate.Text = "Insert Template";
            this.btnInsertTemplate.UseVisualStyleBackColor = false;
            this.btnInsertTemplate.Click += new System.EventHandler(this.btnInsertTemplate_Click);
            // 
            // buttonPanel
            // 
            this.buttonPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonPanel.Controls.Add(this.btnGenerateInit);
            this.buttonPanel.Controls.Add(this.btnCreate);
            this.buttonPanel.Controls.Add(this.btnPreview);
            this.buttonPanel.Controls.Add(this.btnClear);
            this.buttonPanel.Location = new System.Drawing.Point(0, 793);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(984, 67);
            this.buttonPanel.TabIndex = 19;
            // 
            // btnGenerateInit
            // 
            this.btnGenerateInit.BackColor = System.Drawing.Color.DarkOrange;
            this.btnGenerateInit.ForeColor = System.Drawing.Color.White;
            this.btnGenerateInit.Location = new System.Drawing.Point(521, 9);
            this.btnGenerateInit.Name = "btnGenerateInit";
            this.btnGenerateInit.Size = new System.Drawing.Size(150, 40);
            this.btnGenerateInit.TabIndex = 0;
            this.btnGenerateInit.Text = "Generate Init Script";
            this.btnGenerateInit.UseVisualStyleBackColor = false;
            this.btnGenerateInit.Click += new System.EventHandler(this.btnGenerateInit_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.BackColor = System.Drawing.Color.ForestGreen;
            this.btnCreate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCreate.ForeColor = System.Drawing.Color.White;
            this.btnCreate.Location = new System.Drawing.Point(40, 9);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(150, 40);
            this.btnCreate.TabIndex = 0;
            this.btnCreate.Text = "Create Migration";
            this.btnCreate.UseVisualStyleBackColor = false;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.BackColor = System.Drawing.Color.SteelBlue;
            this.btnPreview.ForeColor = System.Drawing.Color.White;
            this.btnPreview.Location = new System.Drawing.Point(200, 9);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(150, 40);
            this.btnPreview.TabIndex = 1;
            this.btnPreview.Text = "Preview";
            this.btnPreview.UseVisualStyleBackColor = false;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.OrangeRed;
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(360, 9);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(150, 40);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // labelLoadTemplate
            // 
            this.labelLoadTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLoadTemplate.Location = new System.Drawing.Point(218, 746);
            this.labelLoadTemplate.Name = "labelLoadTemplate";
            this.labelLoadTemplate.Size = new System.Drawing.Size(104, 30);
            this.labelLoadTemplate.TabIndex = 0;
            this.labelLoadTemplate.Text = "Load template:";
            this.labelLoadTemplate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboTemplates
            // 
            this.comboTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboTemplates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTemplates.Location = new System.Drawing.Point(328, 750);
            this.comboTemplates.Name = "comboTemplates";
            this.comboTemplates.Size = new System.Drawing.Size(300, 25);
            this.comboTemplates.TabIndex = 1;
            // 
            // btnLoadTemplate
            // 
            this.btnLoadTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadTemplate.BackColor = System.Drawing.Color.LightBlue;
            this.btnLoadTemplate.Location = new System.Drawing.Point(643, 747);
            this.btnLoadTemplate.Name = "btnLoadTemplate";
            this.btnLoadTemplate.Size = new System.Drawing.Size(100, 30);
            this.btnLoadTemplate.TabIndex = 2;
            this.btnLoadTemplate.Text = "Load";
            this.btnLoadTemplate.UseVisualStyleBackColor = false;
            this.btnLoadTemplate.Click += new System.EventHandler(this.btnLoadTemplate_Click);
            // 
            // section4UpLabel
            // 
            this.section4UpLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.section4UpLabel.Location = new System.Drawing.Point(20, 340);
            this.section4UpLabel.Name = "section4UpLabel";
            this.section4UpLabel.Size = new System.Drawing.Size(300, 25);
            this.section4UpLabel.TabIndex = 0;
            this.section4UpLabel.Text = "4. SQL Migration Code (UP)";
            // 
            // txtSqlUp
            // 
            this.txtSqlUp.AcceptsTab = true;
            this.txtSqlUp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSqlUp.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtSqlUp.Location = new System.Drawing.Point(40, 368);
            this.txtSqlUp.Multiline = true;
            this.txtSqlUp.Name = "txtSqlUp";
            this.txtSqlUp.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSqlUp.Size = new System.Drawing.Size(910, 166);
            this.txtSqlUp.TabIndex = 1;
            // 
            // section4DownLabel
            // 
            this.section4DownLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.section4DownLabel.Location = new System.Drawing.Point(20, 547);
            this.section4DownLabel.Name = "section4DownLabel";
            this.section4DownLabel.Size = new System.Drawing.Size(300, 25);
            this.section4DownLabel.TabIndex = 2;
            this.section4DownLabel.Text = "5. SQL Migration Code (DOWN)";
            // 
            // txtSqlDown
            // 
            this.txtSqlDown.AcceptsTab = true;
            this.txtSqlDown.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSqlDown.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtSqlDown.Location = new System.Drawing.Point(40, 576);
            this.txtSqlDown.Multiline = true;
            this.txtSqlDown.Name = "txtSqlDown";
            this.txtSqlDown.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSqlDown.Size = new System.Drawing.Size(910, 160);
            this.txtSqlDown.TabIndex = 3;
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(984, 860);
            this.Controls.Add(this.txtCustomSchema);
            this.Controls.Add(this.comboSchema);
            this.Controls.Add(this.labelCustomSchema);
            this.Controls.Add(this.section4UpLabel);
            this.Controls.Add(this.txtSqlUp);
            this.Controls.Add(this.section4DownLabel);
            this.Controls.Add(this.txtSqlDown);
            this.Controls.Add(this.labelLoadTemplate);
            this.Controls.Add(this.comboTemplates);
            this.Controls.Add(this.btnLoadTemplate);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.separator1);
            this.Controls.Add(this.labelSchema);
            this.Controls.Add(this.section2Label);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.nameHint);
            this.Controls.Add(this.labelAuthor);
            this.Controls.Add(this.txtAuthor);
            this.Controls.Add(this.labelComment);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.section3Label);
            this.Controls.Add(this.comboType);
            this.Controls.Add(this.btnInsertTemplate);
            this.Controls.Add(this.buttonPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Database Migration Creator v1.0";
            this.buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}

