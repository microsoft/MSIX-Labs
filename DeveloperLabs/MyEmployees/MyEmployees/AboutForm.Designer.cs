namespace ExportDataLibrary
{
    partial class AboutForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.linkCompanyWebsite = new System.Windows.Forms.LinkLabel();
            this.linkSupportMail = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompanyName.Location = new System.Drawing.Point(12, 9);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(262, 23);
            this.lblCompanyName.TabIndex = 0;
            this.lblCompanyName.Text = "Company Name v4.0";
            this.lblCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // linkCompanyWebsite
            // 
            this.linkCompanyWebsite.Location = new System.Drawing.Point(13, 50);
            this.linkCompanyWebsite.Name = "linkCompanyWebsite";
            this.linkCompanyWebsite.Size = new System.Drawing.Size(261, 23);
            this.linkCompanyWebsite.TabIndex = 1;
            this.linkCompanyWebsite.TabStop = true;
            this.linkCompanyWebsite.Text = "Website";
            this.linkCompanyWebsite.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // linkSupportMail
            // 
            this.linkSupportMail.Location = new System.Drawing.Point(12, 86);
            this.linkSupportMail.Name = "linkSupportMail";
            this.linkSupportMail.Size = new System.Drawing.Size(262, 38);
            this.linkSupportMail.TabIndex = 2;
            this.linkSupportMail.TabStop = true;
            this.linkSupportMail.Text = "E-mail";
            this.linkSupportMail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 129);
            this.Controls.Add(this.linkSupportMail);
            this.Controls.Add(this.linkCompanyWebsite);
            this.Controls.Add(this.lblCompanyName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AboutForm";
            this.Text = "About";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblCompanyName;
        private System.Windows.Forms.LinkLabel linkCompanyWebsite;
        private System.Windows.Forms.LinkLabel linkSupportMail;
    }
}