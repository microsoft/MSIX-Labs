namespace ExportDataLibrary
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.EmployeeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Img = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Email = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Address = new System.Windows.Forms.DataGridViewLinkColumn();
            this.HrsPerWk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HourlyComp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AnnualComp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.employeeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importHRDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calculateAnnualCompensationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeBackgroundImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripUploadNewPicture = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripViewPicture = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSharePicture = new System.Windows.Forms.ToolStripMenuItem();
            this.exportEmployeeDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.employeeBindingSource)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AutoGenerateColumns = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EmployeeId,
            this.Img,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.Email,
            this.Address,
            this.HrsPerWk,
            this.HourlyComp,
            this.AnnualComp});
            this.dataGridView.DataSource = this.employeeBindingSource;
            this.dataGridView.Location = new System.Drawing.Point(12, 46);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(958, 379);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellContentClick);
            // 
            // EmployeeId
            // 
            this.EmployeeId.DataPropertyName = "EmployeeId";
            this.EmployeeId.HeaderText = "Id";
            this.EmployeeId.Name = "EmployeeId";
            this.EmployeeId.Width = 50;
            // 
            // Img
            // 
            this.Img.HeaderText = "Img";
            this.Img.MinimumWidth = 10;
            this.Img.Name = "Img";
            this.Img.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Img.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Img.Width = 50;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "FirstName";
            this.dataGridViewTextBoxColumn2.HeaderText = "FirstName";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "LastName";
            this.dataGridViewTextBoxColumn3.HeaderText = "LastName";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // Email
            // 
            this.Email.DataPropertyName = "Email";
            this.Email.HeaderText = "Email";
            this.Email.Name = "Email";
            this.Email.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Email.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Email.Width = 180;
            // 
            // Address
            // 
            this.Address.DataPropertyName = "Address";
            this.Address.HeaderText = "Address";
            this.Address.Name = "Address";
            this.Address.Width = 200;
            // 
            // HrsPerWk
            // 
            this.HrsPerWk.DataPropertyName = "HrsPerWk";
            this.HrsPerWk.HeaderText = "Hrs/Week";
            this.HrsPerWk.Name = "HrsPerWk";
            this.HrsPerWk.Width = 65;
            // 
            // HourlyComp
            // 
            this.HourlyComp.DataPropertyName = "HourlyComp";
            this.HourlyComp.HeaderText = "Hourly Comp.";
            this.HourlyComp.Name = "HourlyComp";
            this.HourlyComp.Width = 70;
            // 
            // AnnualComp
            // 
            this.AnnualComp.DataPropertyName = "AnnualComp";
            this.AnnualComp.HeaderText = "Annual Comp.";
            this.AnnualComp.Name = "AnnualComp";
            this.AnnualComp.Width = 80;
            // 
            // employeeBindingSource
            // 
            this.employeeBindingSource.DataSource = typeof(MyEmployees.Entities.Employee);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(982, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.checkForUpdatesToolStripMenuItem,
            this.importHRDataToolStripMenuItem,
            this.calculateAnnualCompensationToolStripMenuItem,
            this.changeBackgroundImageToolStripMenuItem,
            this.exportEmployeeDataToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.helpToolStripMenuItem.Text = "Menu";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.checkForUpdatesToolStripMenuItem.Text = "Check for updates";
            this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdatesToolStripMenuItem_Click);
            // 
            // importHRDataToolStripMenuItem
            // 
            this.importHRDataToolStripMenuItem.Name = "importHRDataToolStripMenuItem";
            this.importHRDataToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.importHRDataToolStripMenuItem.Text = "Import employee HR data";
            this.importHRDataToolStripMenuItem.Click += new System.EventHandler(this.importHRDataToolStripMenuItem_Click);
            // 
            // calculateAnnualCompensationToolStripMenuItem
            // 
            this.calculateAnnualCompensationToolStripMenuItem.Name = "calculateAnnualCompensationToolStripMenuItem";
            this.calculateAnnualCompensationToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.calculateAnnualCompensationToolStripMenuItem.Text = "Calculate annual compensation";
            this.calculateAnnualCompensationToolStripMenuItem.Click += new System.EventHandler(this.calculateAnnualCompensationToolStripMenuItem_Click);
            // 
            // changeBackgroundImageToolStripMenuItem
            // 
            this.changeBackgroundImageToolStripMenuItem.Name = "changeBackgroundImageToolStripMenuItem";
            this.changeBackgroundImageToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.changeBackgroundImageToolStripMenuItem.Text = "Change background image";
            this.changeBackgroundImageToolStripMenuItem.Click += new System.EventHandler(this.changeBackgroundImageToolStripMenuItem_Click);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripUploadNewPicture,
            this.toolStripViewPicture,
            this.toolStripSharePicture});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(178, 70);
            // 
            // toolStripUploadNewPicture
            // 
            this.toolStripUploadNewPicture.Name = "toolStripUploadNewPicture";
            this.toolStripUploadNewPicture.Size = new System.Drawing.Size(177, 22);
            this.toolStripUploadNewPicture.Text = "Upload new picture";
            this.toolStripUploadNewPicture.Click += new System.EventHandler(this.toolStripUploadNewPicture_Click);
            // 
            // toolStripViewPicture
            // 
            this.toolStripViewPicture.Name = "toolStripViewPicture";
            this.toolStripViewPicture.Size = new System.Drawing.Size(177, 22);
            this.toolStripViewPicture.Text = "View picture";
            this.toolStripViewPicture.Click += new System.EventHandler(this.toolStripViewPicture_Click);
            // 
            // toolStripSharePicture
            // 
            this.toolStripSharePicture.Name = "toolStripSharePicture";
            this.toolStripSharePicture.Size = new System.Drawing.Size(177, 22);
            this.toolStripSharePicture.Text = "Share picture";
            this.toolStripSharePicture.Click += new System.EventHandler(this.toolStripSharePicture_Click);
            // 
            // exportEmployeeDataToolStripMenuItem
            // 
            this.exportEmployeeDataToolStripMenuItem.Name = "exportEmployeeDataToolStripMenuItem";
            this.exportEmployeeDataToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.exportEmployeeDataToolStripMenuItem.Text = "Export employee data";
            this.exportEmployeeDataToolStripMenuItem.Click += new System.EventHandler(this.exportEmployeeDataToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 450);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "MyEmployees";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.employeeBindingSource)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.BindingSource employeeBindingSource;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripViewPicture;
        private System.Windows.Forms.ToolStripMenuItem toolStripUploadNewPicture;
        private System.Windows.Forms.ToolStripMenuItem toolStripSharePicture;
        private System.Windows.Forms.ToolStripMenuItem importHRDataToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmployeeId;
        private System.Windows.Forms.DataGridViewImageColumn Img;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewLinkColumn Email;
        private System.Windows.Forms.DataGridViewLinkColumn Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn HrsPerWk;
        private System.Windows.Forms.DataGridViewTextBoxColumn HourlyComp;
        private System.Windows.Forms.DataGridViewTextBoxColumn AnnualComp;
        private System.Windows.Forms.ToolStripMenuItem calculateAnnualCompensationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeBackgroundImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportEmployeeDataToolStripMenuItem;
    }
}

