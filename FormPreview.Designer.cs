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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPreview));
            this.txtPreview = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtPreview
            // 
            this.txtPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPreview.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtPreview.Location = new System.Drawing.Point(10, 10);
            this.txtPreview.Multiline = true;
            this.txtPreview.Name = "txtPreview";
            this.txtPreview.ReadOnly = true;
            this.txtPreview.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtPreview.Size = new System.Drawing.Size(810, 579);
            this.txtPreview.TabIndex = 0;
            // 
            // FormPreview
            // 
            this.ClientSize = new System.Drawing.Size(834, 601);
            this.Controls.Add(this.txtPreview);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormPreview";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Migration Preview";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}