using Microsoft.Win32;
using MyEmployees.Entities;
using MyEmployees.Helpers;
using MyEmployees.PluginInterface;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.ApplicationModel;
using Windows.Foundation.Collections;
using Windows.Storage;

namespace ExportDataLibrary
{
    public partial class Form1 : Form
    {
        Config config;
        IPlugin plugin;
        Logger logger;
        // Stores the path of a local file containing the new package
        public static readonly string inputPackageUri = "c:\\temp\\MyEmployees.Package.msixbundle";
        // Stores the path of a local file containing the version data of the new package
        public static readonly string inputPackageVersionUri = "c:\\temp\\version.txt";
        static readonly string hourlyCompFileName = "HourlyCompData.txt";
        static readonly string hoursWorkedFileName = "HoursWorkedData.txt";
        static readonly string annualCompFileName = "annualCompData.txt";
        static readonly string backgroundImageFileName = "AppExtensionAsset";
        static readonly int imgColumn = 1;
        static readonly int emailColumn = 4;
        static readonly int addressColumn = 5;
        static int rowClicked = 0;
        StorageFile imgFile = null;
        StorageFolder HrData = null;
        public static string[] employeeHourlyComp;
        public static string[] employeeHoursWorked;

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
            Scenarios.InitiateBackgroundCheck();
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
                            Email = reader[3].ToString(),
                            Address = "One Microsoft Way, Redmond, WA",
                            HourlyComp = 0,
                            HrsPerWk = 0,
                            AnnualComp = 0
                        };

                        employeeBindingSource.Add(employee);
                    }
                }
            }
            dataGridView.DataSource = employeeBindingSource;
            LoadNewEmployees();
            LoadEmployeePictures();
            LoadHrData();
            LoadEmployeeAnnualCompData();
            LoadBackgroundImage();
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

        public void LoadNewEmployees()
        {
            try
            {
                string path = ApplicationData.Current.LocalFolder.Path + "\\Downloadtemp.CSV";
                var file = File.OpenText(path);
                var reader = new CsvHelper.CsvReader(file);
                while (reader.Read())
                {
                    Employee employee = new Employee
                    {
                        EmployeeId = int.Parse(reader[0].ToString()),
                        FirstName = reader[1].ToString(),
                        LastName = reader[2].ToString(),
                        Email = reader[3].ToString(),
                        Address = "One Microsoft Way, Redmond, WA"
                    };
                    employeeBindingSource.Add(employee);
                }
                dataGridView.DataSource = employeeBindingSource;
                file.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scenarios.InitiateAppUpdate();
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            rowClicked = e.RowIndex;

            // Checks if the cell clicked is an image cell
            if (e.ColumnIndex == imgColumn)
            {
                this.contextMenuStrip.Show(Cursor.Position);
            }

            // Checks if the cell clicked is an email cell
            if (e.ColumnIndex == emailColumn)
            {
                String email = dataGridView.Rows[rowClicked].Cells[emailColumn].Value.ToString();
                Scenarios.LaunchMailApp(email);
            }

            // Checks if the cell clicked is an address cell
            if (e.ColumnIndex == addressColumn)
            {
                String address = dataGridView.Rows[rowClicked].Cells[addressColumn].Value.ToString();
                Scenarios.LaunchMapsApp(address);
            }
        }

        private async void UploadAndSaveImageAsync()
        {
            String employeeId = rowClicked.ToString();
            var localFolder = ApplicationData.Current.LocalFolder;
            try
            {
                StorageFile file = await imgFile.CopyAsync(localFolder, employeeId + ".jpg", NameCollisionOption.ReplaceExisting);
                dataGridView.Rows[rowClicked].Cells[imgColumn].Value = Image.FromFile(file.Path);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public async void LoadEmployeePictures()
        {
            IReadOnlyList<StorageFile> files = await ApplicationData.Current.LocalFolder.GetFilesAsync();
            foreach (StorageFile file in files)
            {
                String[] fileName = file.Name.Split('.');
                int id;
                if (int.TryParse(fileName[0], out id))
                {
                    dataGridView.Rows[id].Cells[imgColumn].Value = Image.FromFile(file.Path);
                }
            }
        }

        private async void toolStripViewPicture_Click(object sender, EventArgs e)
        {
            String employeeId = rowClicked.ToString();
            var localFolder = ApplicationData.Current.LocalFolder;
            try

            {
                var file = await localFolder.GetFileAsync(employeeId + ".jpg");
                Scenarios.LaunchPhotosApp(file);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void toolStripUploadNewPicture_Click(object sender, EventArgs e)
        {
            imgFile = await Scenarios.PickFileAsync();
            if (imgFile != null)
            {
                UploadAndSaveImageAsync();
            }
        }

        private void toolStripSharePicture_Click(object sender, EventArgs e)
        {
            Scenarios.shareImage = (Image)dataGridView.Rows[rowClicked].Cells[imgColumn].Value;
            if (Scenarios.shareImage != null)
            {
                Scenarios.SetShareFileAsync();
                Scenarios.InitiateShare();
            }
            else
            {
                MessageBox.Show("The image is empty");
            }
        }

        private string[] LoadEmployeeHourlyCompData(StorageFolder HrData)
        {
            return File.ReadAllLines(Path.Combine(HrData.Path, hourlyCompFileName));
        }

        private string[] LoadEmployeeHoursWorkedData(StorageFolder HrData)
        {
            return File.ReadAllLines(Path.Combine(HrData.Path, hoursWorkedFileName));
        }

        private void UpdateEmployeeHourlyComp(String[] hourlyComp)
        {
            int index = 0;
            foreach (Employee e in employeeBindingSource)
            {
                e.HourlyComp = Convert.ToInt32(hourlyComp[index]);
                index++;
            }
        }

        private void UpdateEmployeeHoursWorked(String[] hoursWorked)
        {
            int index = 0;
            foreach (Employee e in employeeBindingSource)
            {
                e.HrsPerWk = Convert.ToInt32(hoursWorked[index]);
                index++;
            }
        }

        private async void SaveHrData()
        {
            try
            {
                StorageFile hoursWorkedFile = await HrData.GetFileAsync(hoursWorkedFileName);
                await hoursWorkedFile.CopyAsync(ApplicationData.Current.LocalFolder);
                StorageFile hourlyCompFile = await HrData.GetFileAsync(hourlyCompFileName);
                await hourlyCompFile.CopyAsync(ApplicationData.Current.LocalFolder);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void LoadHrData()
        {
            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                employeeHourlyComp = LoadEmployeeHourlyCompData(localFolder);
                UpdateEmployeeHourlyComp(employeeHourlyComp);
                employeeHoursWorked = LoadEmployeeHoursWorkedData(localFolder);
                UpdateEmployeeHoursWorked(employeeHoursWorked);
                this.Refresh();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private async void importHRDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HrData = await Scenarios.LoadDataFromOptionalPackageAsync();
            if (HrData != null)
            {
                employeeHourlyComp = LoadEmployeeHourlyCompData(HrData);
                UpdateEmployeeHourlyComp(employeeHourlyComp);
                employeeHoursWorked = LoadEmployeeHoursWorkedData(HrData);
                UpdateEmployeeHoursWorked(employeeHoursWorked);
                SaveHrData();
                this.Refresh();
            }
        }

        private void UpdateEmployeeAnnualCompData(ValueSet annualComp)
        {
            int index = 0;
            foreach (Employee e in employeeBindingSource)
            {
                e.AnnualComp = Convert.ToInt32(annualComp[e.EmployeeId.ToString()]);
                index++;
            }
        }

        private void LoadEmployeeAnnualCompData()
        {
            try
            {
                String[] annualCompData = File.ReadAllLines(Path.Combine(ApplicationData.Current.LocalFolder.Path, annualCompFileName));
                foreach (Employee e in employeeBindingSource)
                {
                    e.AnnualComp = Convert.ToInt32(annualCompData[e.EmployeeId - 1]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private async void SaveEmployeeAnnualCompData(ValueSet annualComp)
        {
            StorageFile annualCompFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(annualCompFileName, CreationCollisionOption.ReplaceExisting);
            int index = 0;
            string[] annualCompData = new string[employeeBindingSource.Count];
            foreach (Employee e in employeeBindingSource)
            {
                int employeeAnnualComp = Convert.ToInt32(annualComp[e.EmployeeId.ToString()]);
                annualCompData[index] = employeeAnnualComp.ToString();
                index++;
            }
            File.WriteAllLines(annualCompFile.Path, annualCompData);
        }

        private async void calculateAnnualCompensationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (employeeHourlyComp != null && employeeHoursWorked != null)
            {
                ValueSet annualComp = await Scenarios.CallAppServiceAsync("MyEmployeesCalcService_rv8ym4y7mg4aw", "com.microsoft.AnnualCompCalculator", employeeBindingSource.Count);
                if (annualComp != null)
                {
                    UpdateEmployeeAnnualCompData(annualComp);
                    SaveEmployeeAnnualCompData(annualComp);
                    this.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Please get the data from the optional package first");
            }
        }

        public async void SaveBackgroundImage(StorageFile appExtensionAsset)
        {
            try
            {
                var localFolder = ApplicationData.Current.LocalFolder;
                await appExtensionAsset.CopyAsync(localFolder, backgroundImageFileName, NameCollisionOption.ReplaceExisting);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void LoadBackgroundImage()
        {
            try
            {
                string backgroundImagePath = ApplicationData.Current.LocalFolder.Path + "\\" + backgroundImageFileName;
                ChangeBackgroundImage(backgroundImagePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void ChangeBackgroundImage(String backgroundImagePath)
        {
            Image image = Image.FromFile(backgroundImagePath);
            this.BackgroundImage = image;
        }

        private void changeBackgroundImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scenarios.InitiateAndExecuteAppExtensions();
        }
    }
}
