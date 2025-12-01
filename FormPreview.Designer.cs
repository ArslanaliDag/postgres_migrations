using System.Windows.Forms;

namespace PostgresMigrations
{
    partial class FormPreview
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        /// 

        private TextBox txtPreview;
        private Button btnOk;

        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.Text = "Migration Preview";
            this.Width = 850;
            this.Height = 640;
            this.StartPosition = FormStartPosition.CenterParent;

            txtPreview = new TextBox();
            txtPreview.Location = new System.Drawing.Point(10, 10);
            txtPreview.Size = new System.Drawing.Size(810, 540);
            txtPreview.Multiline = true;
            txtPreview.ScrollBars = ScrollBars.Both;
            txtPreview.Font = new System.Drawing.Font("Consolas", 9);
            txtPreview.ReadOnly = true;
            txtPreview.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(txtPreview);

            btnOk = new Button();
            btnOk.Location = new System.Drawing.Point((this.ClientSize.Width - 100) / 2, 560);
            btnOk.Size = new System.Drawing.Size(100, 30);
            btnOk.Text = "OK";
            btnOk.Anchor = AnchorStyles.Bottom;
            btnOk.Click += (s, e) => { this.DialogResult = DialogResult.OK; this.Close(); };
            this.Controls.Add(btnOk);
        }
    }
}