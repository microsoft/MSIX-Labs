﻿using Microsoft.Win32;
using MyEmployees.Entities;
using MyEmployees.Helpers;
using MyEmployees.PluginInterface;
using Newtonsoft.Json;
using NLog;
using System;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Management.Deployment;

namespace ExportDataLibrary
{
    public partial class Form1 : Form
    {
        Config config;
        IPlugin plugin;
        Logger logger;


        public Form1()
        {
            InitializeComponent();
            var logManager = LogManager.LoadConfiguration("NLog.config");
            logger = logManager.GetCurrentClassLogger();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadConfig();
            LoadData();
            AddAppInstaller(getAppInstallerUri());
            CheckKioskMode();
            //await CheckForUpdates();
        }

        // Retrieves the URI of the .appinstaller which is embedded in the application (MyEmployees) package
        public static string getAppInstallerUri()
        {
            string uriPath = null;
            try
            {
                uriPath = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\..\\Update.appinstaller";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return uriPath;
        }

        // If there is not an associated .appinstaller file with the package,
        // this function adds the .appinstaller referred to by inputPackageUri
        public static void AddAppInstaller(String inputPackageUri)
        {
            AppInstallerInfo info = Package.Current.GetAppInstallerInfo();
            if (info == null && inputPackageUri != null)
            {
                // Register the active instance of an application for restart
                uint res = RelaunchHelper.RegisterApplicationRestart(null, RelaunchHelper.RestartFlags.NONE);
                
                Uri packageUri = new Uri(inputPackageUri);
                PackageManager packageManager = new PackageManager();
                IAsyncOperationWithProgress<DeploymentResult, DeploymentProgress> deploymentOperation = null;
                try
                {
                    deploymentOperation = packageManager.AddPackageByAppInstallerFileAsync(
                            packageUri,
                            AddPackageByAppInstallerOptions.ForceTargetAppShutdown,
                            null);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                // Listen to the event that the deployment is complete
                ManualResetEvent opCompletedEvent = new ManualResetEvent(false);
                deploymentOperation.Completed = (depProgress, status) => { opCompletedEvent.Set(); };
                opCompletedEvent.WaitOne();

                // Check the status of the operation
                if (deploymentOperation.Status == AsyncStatus.Error)
                {
                    DeploymentResult deploymentResult = deploymentOperation.GetResults();
                    Console.WriteLine("Error code: {0}", deploymentOperation.ErrorCode);
                    Console.WriteLine("Error text: {0}", deploymentResult.ErrorText);

                }
                else if (deploymentOperation.Status == AsyncStatus.Canceled)
                {
                    Console.WriteLine("Association canceled");
                }
                else if (deploymentOperation.Status == AsyncStatus.Completed)
                {
                    Console.WriteLine("Association succeeded");
                }
                else
                {
                    Console.WriteLine("Association status unknown");
                }
            }
        }

        private async Task CheckForUpdates()
        {
            var result = await Package.Current.CheckUpdateAvailabilityAsync();
            if (result.Availability == PackageUpdateAvailability.Available)
            {
                MessageBox.Show("There's a new update! Restart your app to install it");
            }
        }

        private void CheckKioskMode()
        {
            var regKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Contoso\\MyEmployees");
            if (regKey != null)
            {
                var kioskMode = regKey.GetValue("KioskMode");
                if (kioskMode != null)
                {
                    string isKioskModeEnabled = kioskMode.ToString().ToLowerInvariant();
                    if (isKioskModeEnabled == "true")
                    {
                        menuStrip1.Visible = false;
                        logger.Log(LogLevel.Info, "Kiosk mode enabled");
                    }
                }
            }
        }

        private void LoadConfig()
        {
            string path = $"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}\\Contoso\\MyEmployees\\config.json";
            if (File.Exists(path))
            {
                logger.Log(LogLevel.Info, "Custom config file is available");
                string json = File.ReadAllText(path);
                config = JsonConvert.DeserializeObject<Config>(json);

                if (!config.IsCheckForUpdatesEnabled)
                { 
                    logger.Log(LogLevel.Info, "Check for updates disabled");
                }
            }
            else
            {
                logger.Log(LogLevel.Info, "Custom config file isn't available");
            }
            try
            {
                string dllPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}\\Contoso\\MyEmployees\\Plugins\\ExportDataLibrary.dll";
                plugin = LoadAssembly(dllPath);
                logger.Log(LogLevel.Info, "Export data plugin available");
            }
            catch (Exception)
            {
                logger.Log(LogLevel.Info, "Export data plugin isn't available");
            }
        }

        private void LoadData()
        {
            string result = Assembly.GetExecutingAssembly().Location;
            int index = result.LastIndexOf("\\");
            string dbPath = $"{result.Substring(0, index)}\\Employees.db";

            SQLiteConnection connection = new SQLiteConnection($"Data Source= {dbPath}");
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = "SELECT * FROM Employees";
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Employee employee = new Employee
                        {
                            EmployeeId = int.Parse(reader[0].ToString()),
                            FirstName = reader[1].ToString(),
                            LastName = reader[2].ToString(),
                            Email = reader[3].ToString()
                        };

                        employeeBindingSource.Add(employee);
                    }
                }
            }

            dataGridView1.DataSource = employeeBindingSource;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm;
            if (config != null)
            {
                aboutForm = new AboutForm(config.About.CompanyName, config.About.SupportLink, config.About.SupportMail);
            }
            else
            {
                aboutForm = new AboutForm();
            }

            aboutForm.ShowDialog();
        }

        private void exportAsCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV|*.csv";
            saveFileDialog.Title = "Save a CSV file";
            saveFileDialog.ShowDialog();

            bool isFileSaved = plugin.Execute(employeeBindingSource.List, saveFileDialog.FileName);
            if (isFileSaved)
            {
                MessageBox.Show("The CSV file has been exported with success");
            }
            else
            {
                MessageBox.Show("The export operation has failed");
            }
        }

        private IPlugin LoadAssembly(string assemblyPath)
        {
            string assembly = Path.GetFullPath(assemblyPath);
            Assembly ptrAssembly = Assembly.LoadFile(assembly);
            foreach (Type item in ptrAssembly.GetTypes())
            {
                if (!item.IsClass) continue;
                if (item.GetInterfaces().Contains(typeof(IPlugin)))
                {
                    return (IPlugin)Activator.CreateInstance(item);
                }
            }
            throw new Exception("Invalid DLL, Interface not found!");
        }
    }
}
