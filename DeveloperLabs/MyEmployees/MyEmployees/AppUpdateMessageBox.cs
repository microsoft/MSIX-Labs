using System;
using System.Drawing;
using System.Windows.Forms;
using Windows.ApplicationModel;

namespace MyEmployees
{
    public partial class AppUpdateMessageBox : Form
    {
        public static string errorText;
        static readonly string onFailure = "The update has failed to download.";
        public static string onFailureSuggestion = " Please try updating the app from 'Menu' -> 'check for updates' ";
        Label message = new Label();
        ProgressBar progressBar = new ProgressBar();
        public Button updateNowButton = new Button();
        Button updateLaterButton = new Button();

        public AppUpdateMessageBox(string title, string body, string button1 = null, string button2 = null)
        {
            this.updateNowButton.Click += new System.EventHandler(this.updateNowButton_Click);
            this.updateLaterButton.Click += new System.EventHandler(this.updateLaterButton_Click);
            this.ClientSize = new System.Drawing.Size(300, 110);
            this.BackColor = Control.DefaultBackColor;
            this.Text = title;

            if (button1 != null)
            {
                updateNowButton.Location = new System.Drawing.Point(100, 82);
                updateNowButton.Size = new System.Drawing.Size(90, 23);
                updateNowButton.Text = button1;
                updateNowButton.BackColor = Control.DefaultBackColor;
                this.Controls.Add(updateNowButton);
            }

            if (button2 != null)
            {
                updateLaterButton.Location = new System.Drawing.Point(195, 82);
                updateLaterButton.Size = new System.Drawing.Size(90, 23);
                updateLaterButton.Text = button2;
                updateLaterButton.BackColor = Control.DefaultBackColor;
                this.Controls.Add(updateLaterButton);
            }

            progressBar.Location = new System.Drawing.Point(40, 40);
            progressBar.Size = new System.Drawing.Size(200, 20);
            this.Load += ProgressBarPackageCatalog_Load;
            this.progressBar.Visible = false;
            this.Controls.Add(progressBar);

            message.Location = new System.Drawing.Point(20, 10);
            message.MaximumSize = new Size(275 , 90);
            message.AutoSize = true;
            message.Text = body;
            this.ShowIcon = false;
            this.Controls.Add(message);
        }

        private void updateLaterButton_Click(object sender, System.EventArgs e)
        { 
            Close();
        }

        public void updateNowButton_Click(object sender, System.EventArgs e)
        {
            this.progressBar.Visible = true;
            int status = Helpers.AppUpdate.UpdateNowHelper();
            if (status == 1)
            {
                updateLaterButton.Text = "Ok";
                updateNowButton.Visible = false;
                this.progressBar.Visible = false;
                this.message.Text = onFailure + errorText + onFailureSuggestion;
            }
        }

        public void ProgressBarPackageCatalog_Load(object sender, EventArgs e)
        {
            // Listen for package update event
            PackageCatalog packageCatalog = PackageCatalog.OpenForCurrentUser();
            packageCatalog.PackageUpdating += OnPackageUpdating;
        }

        private void OnPackageUpdating(object sender, PackageUpdatingEventArgs args)
        {
            if (args.IsComplete)
            {
                this.Close();
            }
            // Increment progressBar in sync with the progress of the update
            progressBar.Value = (int)args.Progress;
        }
    }
}