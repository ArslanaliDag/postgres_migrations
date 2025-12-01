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

        private Label section4Label;
        private TextBox txtSql;

        private Button btnInsertTemplate;
        private Panel buttonPanel;
        private Button btnCreate;
        private Button btnPreview;
        private Button btnClear;
        private Button btnExit;

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
            components = new System.ComponentModel.Container();

            // Form properties
            this.Text = "Database Migration Creator v2.0";
            this.Width = 1000;
            this.Height = 820;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new System.Drawing.Font("Segoe UI", 10);
            this.BackColor = System.Drawing.Color.White;

            // Title
            titleLabel = new Label();
            titleLabel.Location = new System.Drawing.Point(20, 12);
            titleLabel.Size = new System.Drawing.Size(940, 30);
            titleLabel.Text = "CREATE DATABASE MIGRATION (Multi-Schema Support)";
            titleLabel.Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold);
            titleLabel.ForeColor = System.Drawing.Color.DarkBlue;
            titleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(titleLabel);

            // Separator
            separator1 = new Label();
            separator1.Location = new System.Drawing.Point(20, 50);
            separator1.Size = new System.Drawing.Size(940, 2);
            separator1.BorderStyle = BorderStyle.FixedSingle;
            separator1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(separator1);

            // SECTION 1: Schema
            labelSchema = new Label();
            labelSchema.Location = new System.Drawing.Point(20, 70);
            labelSchema.Size = new System.Drawing.Size(200, 25);
            labelSchema.Text = "Target Schema:";
            labelSchema.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            labelSchema.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.Controls.Add(labelSchema);

            comboSchema = new ComboBox();
            comboSchema.Location = new System.Drawing.Point(220, 70);
            comboSchema.Size = new System.Drawing.Size(200, 25);
            comboSchema.DropDownStyle = ComboBoxStyle.DropDownList;
            comboSchema.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            comboSchema.SelectedIndexChanged += comboSchema_SelectedIndexChanged;
            this.Controls.Add(comboSchema);

            labelCustomSchema = new Label();
            labelCustomSchema.Location = new System.Drawing.Point(430, 70);
            labelCustomSchema.Size = new System.Drawing.Size(100, 25);
            labelCustomSchema.Text = "Custom schema:";
            labelCustomSchema.Visible = false;
            labelCustomSchema.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.Controls.Add(labelCustomSchema);

            txtCustomSchema = new TextBox();
            txtCustomSchema.Location = new System.Drawing.Point(540, 70);
            txtCustomSchema.Size = new System.Drawing.Size(150, 25);
            txtCustomSchema.Visible = false;
            txtCustomSchema.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(txtCustomSchema);

            // SECTION 2: Migration info
            section2Label = new Label();
            section2Label.Location = new System.Drawing.Point(20, 110);
            section2Label.Size = new System.Drawing.Size(300, 25);
            section2Label.Text = "2. Migration Description";
            section2Label.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            section2Label.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.Controls.Add(section2Label);

            labelName = new Label();
            labelName.Location = new System.Drawing.Point(40, 145);
            labelName.Size = new System.Drawing.Size(150, 25);
            labelName.Text = "Migration Name:";
            labelName.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.Controls.Add(labelName);

            txtName = new TextBox();
            txtName.Location = new System.Drawing.Point(200, 145);
            txtName.Size = new System.Drawing.Size(400, 25);
            txtName.Font = new System.Drawing.Font("Consolas", 10);
            txtName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(txtName);

            nameHint = new Label();
            nameHint.Location = new System.Drawing.Point(610, 145);
            nameHint.Size = new System.Drawing.Size(300, 25);
            nameHint.Text = "English letters and underscores only";
            nameHint.ForeColor = System.Drawing.Color.Gray;
            nameHint.Font = new System.Drawing.Font("Segoe UI", 9);
            nameHint.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(nameHint);

            labelAuthor = new Label();
            labelAuthor.Location = new System.Drawing.Point(40, 185);
            labelAuthor.Size = new System.Drawing.Size(150, 25);
            labelAuthor.Text = "Author:";
            labelAuthor.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.Controls.Add(labelAuthor);

            txtAuthor = new TextBox();
            txtAuthor.Location = new System.Drawing.Point(200, 185);
            txtAuthor.Size = new System.Drawing.Size(400, 25);
            txtAuthor.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtAuthor.Font = new System.Drawing.Font("Segoe UI", 10);
            this.Controls.Add(txtAuthor);

            labelComment = new Label();
            labelComment.Location = new System.Drawing.Point(40, 225);
            labelComment.Size = new System.Drawing.Size(150, 25);
            labelComment.Text = "Comment (notes):";
            labelComment.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.Controls.Add(labelComment);

            txtComment = new TextBox();
            txtComment.Location = new System.Drawing.Point(200, 225);
            txtComment.Size = new System.Drawing.Size(650, 25);
            txtComment.Font = new System.Drawing.Font("Segoe UI", 10);
            txtComment.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(txtComment);

            // SECTION 3: Migration Type
            section3Label = new Label();
            section3Label.Location = new System.Drawing.Point(20, 265);
            section3Label.Size = new System.Drawing.Size(300, 25);
            section3Label.Text = "3. Migration Type";
            section3Label.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            section3Label.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.Controls.Add(section3Label);

            comboType = new ComboBox();
            comboType.Location = new System.Drawing.Point(40, 300);
            comboType.Size = new System.Drawing.Size(800, 30);
            comboType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(comboType);

            // SECTION 4: SQL
            section4Label = new Label();
            section4Label.Location = new System.Drawing.Point(20, 340);
            section4Label.Size = new System.Drawing.Size(300, 25);
            section4Label.Text = "4. SQL Migration Code";
            section4Label.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            section4Label.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.Controls.Add(section4Label);

            txtSql = new TextBox();
            txtSql.Location = new System.Drawing.Point(40, 375);
            txtSql.Size = new System.Drawing.Size(910, 200);
            txtSql.Multiline = true;
            txtSql.ScrollBars = ScrollBars.Both;
            txtSql.AcceptsTab = true;
            txtSql.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(txtSql);

            // Insert Template button
            btnInsertTemplate = new Button();
            btnInsertTemplate.Location = new System.Drawing.Point(40, 585);
            btnInsertTemplate.Size = new System.Drawing.Size(150, 30);
            btnInsertTemplate.Text = "Insert Template";
            btnInsertTemplate.BackColor = System.Drawing.Color.LightGray;
            btnInsertTemplate.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            btnInsertTemplate.Click += btnInsertTemplate_Click;
            this.Controls.Add(btnInsertTemplate);

            // Buttons panel (bottom)
            buttonPanel = new Panel();
            buttonPanel.Location = new System.Drawing.Point(20, 630);
            buttonPanel.Size = new System.Drawing.Size(930, 60);
            buttonPanel.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            this.Controls.Add(buttonPanel);

            btnCreate = new Button();
            btnCreate.Location = new System.Drawing.Point(0, 10);
            btnCreate.Size = new System.Drawing.Size(150, 40);
            btnCreate.Text = "Create Migration";
            btnCreate.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            btnCreate.BackColor = System.Drawing.Color.ForestGreen;
            btnCreate.ForeColor = System.Drawing.Color.White;
            btnCreate.Click += btnCreate_Click;
            buttonPanel.Controls.Add(btnCreate);

            btnPreview = new Button();
            btnPreview.Location = new System.Drawing.Point(160, 10);
            btnPreview.Size = new System.Drawing.Size(150, 40);
            btnPreview.Text = "Preview";
            btnPreview.BackColor = System.Drawing.Color.SteelBlue;
            btnPreview.ForeColor = System.Drawing.Color.White;
            btnPreview.Click += btnPreview_Click;
            buttonPanel.Controls.Add(btnPreview);

            btnClear = new Button();
            btnClear.Location = new System.Drawing.Point(320, 10);
            btnClear.Size = new System.Drawing.Size(150, 40);
            btnClear.Text = "Clear";
            btnClear.BackColor = System.Drawing.Color.OrangeRed;
            btnClear.ForeColor = System.Drawing.Color.White;
            btnClear.Click += btnClear_Click;
            buttonPanel.Controls.Add(btnClear);

            btnExit = new Button();
            btnExit.Location = new System.Drawing.Point(780, 10);
            btnExit.Size = new System.Drawing.Size(150, 40);
            btnExit.Text = "Exit";
            btnExit.BackColor = System.Drawing.Color.DarkRed;
            btnExit.ForeColor = System.Drawing.Color.White;
            btnExit.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            btnExit.Click += btnExit_Click;
            buttonPanel.Controls.Add(btnExit);
        }
    }
}

