using Microsoft.Win32;
using MyEmployees.Entities;
using MyEmployees.PluginInterface;
using Newtonsoft.Json;
using NLog;
using System;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.ApplicationModel;

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
            CheckKioskMode();
            //await CheckForUpdates();
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
            string EmpdbPath = $"{result.Substring(0, index)}\\Employees.db";
            string EmpFinancedbPath = $"{result.Substring(0, index)}\\EmployeesFinance.db";
            bool reademp = true; // whether to read employees.db or not

            SQLiteConnection Empconnection = new SQLiteConnection($"Data Source= {EmpdbPath}");
            SQLiteConnection EmpFinanceconnection = new SQLiteConnection($"Data Source= {EmpFinancedbPath}");
            using (SQLiteCommand command = new SQLiteCommand(EmpFinanceconnection))
            {
                // open connection with Employees.db if present
                SQLiteCommand empcommand = new SQLiteCommand(Empconnection);
                Empconnection.Open();
                empcommand.CommandText = "SELECT * FROM Employees";
                SQLiteDataReader empreader = null;
                try
                {
                    empreader = empcommand.ExecuteReader();
                }
                catch
                {
                    reademp = false;
                }

                EmpFinanceconnection.Open();
                command.CommandText = "SELECT * FROM EmployeesFinance";
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Employee employee = new Employee
                        {
                            EmployeeId = int.Parse(reader[0].ToString()),
                            Email = reader[1].ToString(),
                            Role = reader[2].ToString(),
                            AnnualCompensation = double.Parse(reader[3].ToString()),
                            AnnualBonus = double.Parse(reader[4].ToString())
                        };
                        // if reading employees.db too
                        if (reademp == true && empreader.Read())
                        {
                            if(employee.EmployeeId == int.Parse(empreader[0].ToString()))
                            {
                                employee.FirstName = empreader[1].ToString();
                                employee.LastName = empreader[2].ToString();
                            }
                        }

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
