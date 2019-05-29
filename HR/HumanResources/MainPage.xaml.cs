using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Popups;
using System.Diagnostics;
using System.Reflection;
using Windows.Storage;
using Newtonsoft.Json;
using System.Globalization;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Windows.Management.Deployment;
using System.Net;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HumanResources
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private PackageCatalog packageCatalog;
        private Grid currentGrid;
        private EmployeeData approvals;

        public MainPage()
        {
            this.InitializeComponent();

            ApplicationView.PreferredLaunchViewSize = new Size(1200, 850);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            Loaded += new RoutedEventHandler(page_Loaded);
        }

        public void page_Loaded(object sender, RoutedEventArgs e)
        {
            var currentAppPackage = Package.Current;
            foreach (var package in currentAppPackage.Dependencies)
            {
                if (package.IsOptional && package.Id.FamilyName.Contains("Approvals"))
                {
                    OpTitle.Visibility = Visibility.Visible;
                    ToApprovals.Visibility = Visibility.Visible;
                }
                else if (package.IsOptional && package.Id.FamilyName.Contains("Employees"))
                {
                    OpTitle.Visibility = Visibility.Visible;
                    ToEmployees.Visibility = Visibility.Visible;
                }
            }

            PackageVersion packageVersion = currentAppPackage.Id.Version;
            appVersion.Text = string.Format("{0}.{1}.{2}.{3}", packageVersion.Major, packageVersion.Minor, packageVersion.Build, packageVersion.Revision);

            currentGrid = HomeGrid;

            HookupCatalog();
        }

        private void HookupCatalog()
        {
            try
            {
                packageCatalog = PackageCatalog.OpenForCurrentPackage();
                packageCatalog.PackageInstalling += Catalog_PackageInstalling;
            }
            catch (Exception ex)
            {
                PopupUI("Unable to setup deployment event handlers. {" + ex.InnerException + "}");
            }
        }

        private async void Catalog_PackageInstalling(PackageCatalog sender, PackageInstallingEventArgs args)
        {
            if (args.Progress == 100 && args.IsComplete && args.Package.IsOptional && args.Package.Id.FamilyName.Contains("Approvals"))
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    OpTitle.Visibility = Visibility.Visible;
                    ToApprovals.Visibility = Visibility.Visible;
                });
            }
            else if ((args.Progress == 100 && args.IsComplete && args.Package.IsOptional && args.Package.Id.FamilyName.Contains("Employees")))
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    OpTitle.Visibility = Visibility.Visible;
                    ToEmployees.Visibility = Visibility.Visible;
                });
            }
        }

        private async void PopupUI(string text)
        {
            await new MessageDialog(text).ShowAsync();
        }

        private void BackTapped(object sender, TappedRoutedEventArgs e)
        {
            HomeGrid.Visibility = Visibility.Visible;
            currentGrid.Visibility = Visibility.Collapsed;
            currentGrid = HomeGrid;
        }

        private async void ToTimeTapped(object sender, TappedRoutedEventArgs e)
        {
            currentGrid.Visibility = Visibility.Collapsed;
            TimeGrid.Visibility = Visibility.Visible;
            currentGrid = TimeGrid;

            var employeefile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///employees.json"));
            var json = await FileIO.ReadTextAsync(employeefile);
            EmployeeData main = JsonConvert.DeserializeObject<EmployeeData>(json);

            TotalVacation.Text = main.Employees[0].Vacation;
            TotalSickLeave.Text = main.Employees[0].SickLeave;

            var textboxes = new List<TextBox>();
            FindChildren(textboxes, TimeGrid);
            foreach (var box in textboxes)
            {
                box.Text = "";
            }
        }

        private void TimeSubmitTapped(object sender, TappedRoutedEventArgs e)
        {
            var textboxes = new List<TextBox>();
            FindChildren(textboxes, TimeGrid);
            int vacation = 0;
            int sickLeave = 0;
            foreach (var box in textboxes)
            {
                if ((string)box.Tag == "Vacation")
                {
                    if (box.Text != "")
                    {
                        if (Int32.TryParse(box.Text, out int i))
                        {
                            vacation += i;
                        }
                    }
                    box.Text = "";
                } else if ((string)box.Tag == "SickLeave")
                {
                    if (box.Text != "")
                    {
                        if (Int32.TryParse(box.Text, out int i))
                        {
                            sickLeave += i;
                        }
                    }
                    box.Text = "";
                }
            }

            TotalVacation.Text = (Int32.Parse(TotalVacation.Text) - vacation).ToString();
            TotalSickLeave.Text = (Int32.Parse(TotalSickLeave.Text) - sickLeave).ToString();
        }

        internal static void FindChildren<T>(List<T> results, DependencyObject startNode) where T : DependencyObject
        {
            int count = VisualTreeHelper.GetChildrenCount(startNode);
            for (int i = 0; i < count; i++)
            {
                DependencyObject current = VisualTreeHelper.GetChild(startNode, i);
                if ((current.GetType()).Equals(typeof(T)) || (current.GetType().GetTypeInfo().IsSubclassOf(typeof(T))))
                {
                    T asType = (T)current;
                    results.Add(asType);
                }
                FindChildren<T>(results, current);
            }
        }


        private async void ToRewardsTapped(object sender, TappedRoutedEventArgs e)
        {
            currentGrid.Visibility = Visibility.Collapsed;
            RewardsGrid.Visibility = Visibility.Visible;
            currentGrid = RewardsGrid;

            var employeefile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///employees.json"));
            var json = await FileIO.ReadTextAsync(employeefile);
            EmployeeData main = JsonConvert.DeserializeObject<EmployeeData>(json);

            CultureInfo us = new CultureInfo("en-US");
            int salary = Int32.Parse(main.Employees[0].Salary);
            int salaryIncrease = Int32.Parse(main.Employees[0].SalaryIncrease);
            int bonus = Int32.Parse(main.Employees[0].Bonus);
            int stock = Int32.Parse(main.Employees[0].Stock);

            CurrentSalary.Text = salary.ToString("C0", us);
            SalaryIncrease.Text = salaryIncrease.ToString("C0", us);
            NewSalary.Text = (salary + salaryIncrease).ToString("C0", us);
            Bonus.Text = bonus.ToString("C0", us);
            Stock.Text = bonus.ToString("C0", us);
            PrevBonus.Text = Int32.Parse(main.Employees[0].PrevBonus).ToString("C0", us);
            PrevStock.Text = Int32.Parse(main.Employees[0].PrevStock).ToString("C0", us);
            TotalRewards.Text = (salary + bonus + stock).ToString("C0", us);
        }

        private async void ToApprovalsTapped(object sender, TappedRoutedEventArgs e)
        {
            var currentAppPackage = Package.Current;
            foreach (var package in currentAppPackage.Dependencies)
            {
                if (package.IsOptional && package.Id.FamilyName.Contains("Approvals"))
                {
                    currentGrid.Visibility = Visibility.Collapsed;
                    ApprovalsGrid.Visibility = Visibility.Visible;
                    currentGrid = ApprovalsGrid;

                    StorageFolder installFolder = package.InstalledLocation;
                    StorageFile employeefile = await installFolder.GetFileAsync("employees.json");
                    var json = await FileIO.ReadTextAsync(employeefile);
                    approvals = JsonConvert.DeserializeObject<EmployeeData>(json);

                    ApprovalsListView.ItemsSource = approvals.Employees;
                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int index = ApprovalsListView.Items.IndexOf(item);

            string[] resources = Regex.Split(approvals.Employees[index].Approved, @"\W+");
            if (((sender as ComboBox).SelectedItem as ComboBoxItem).Content as string != "")
            {
                var resourcesList = resources.OfType<string>().ToList();
                resourcesList.Add(((sender as ComboBox).SelectedItem as ComboBoxItem).Content as string);
                resources = resourcesList.Distinct().ToArray();
                approvals.Employees[index].Approved = String.Join(", ", resources);
            }
        }

        private async void ToEmployeesTapped(object sender, TappedRoutedEventArgs e)
        {
            var currentAppPackage = Package.Current;
            foreach (var package in currentAppPackage.Dependencies)
            {
                if (package.IsOptional && package.Id.FamilyName.Contains("Employees"))
                {
                    currentGrid.Visibility = Visibility.Collapsed;
                    EmployeesGrid.Visibility = Visibility.Visible;
                    currentGrid = EmployeesGrid;

                    StorageFolder installFolder = package.InstalledLocation;
                    StorageFile employeefile = await installFolder.GetFileAsync("employees.json");
                    var json = await FileIO.ReadTextAsync(employeefile);
                    var employees = JsonConvert.DeserializeObject<EmployeeData>(json);

                    CultureInfo us = new CultureInfo("en-US");
                    foreach(Employee employee in employees.Employees)
                    {
                        employee.Salary = Int32.Parse(employee.Salary).ToString("C0", us);
                    }
                    EmployeesListView.ItemsSource = employees.Employees;

                }
            }
        }

        private void EmployeesSubmitTapped(object sender, TappedRoutedEventArgs e)
        {
            var textboxes = new List<TextBox>();
            FindChildren(textboxes, EmployeesGrid);
            foreach (var box in textboxes)
            {
                box.Text = "";
            }
        }

        private async void CheckUpdate(object sender, TappedRoutedEventArgs e)
        {
            WebClient client = new WebClient();
            Stream stream = client.OpenRead("https://trial3.azurewebsites.net/HRApp/Version.txt");
            StreamReader reader = new StreamReader(stream);
            var newVersion = new Version(await reader.ReadToEndAsync());
            Package package = Package.Current;
            PackageVersion packageVersion = package.Id.Version;
            var currentVersion = new Version(string.Format("{0}.{1}.{2}.{3}", packageVersion.Major, packageVersion.Minor, packageVersion.Build, packageVersion.Revision));
            if (newVersion.CompareTo(currentVersion) > 0)
            {
                var messageDialog = new MessageDialog("Found an update.");
                messageDialog.Commands.Add(new UICommand(
                    "Update",
                    new UICommandInvokedHandler(this.CommandInvokedHandler)));
                messageDialog.Commands.Add(new UICommand(
                    "Close",
                    new UICommandInvokedHandler(this.CommandInvokedHandler)));
                messageDialog.DefaultCommandIndex = 0;
                messageDialog.CancelCommandIndex = 1;
                await messageDialog.ShowAsync();
            } else
            {
                var messageDialog = new MessageDialog("Did not find an update.");
                await messageDialog.ShowAsync();
            }
        }

        private async void CommandInvokedHandler(IUICommand command)
        {
            if (command.Label == "Update")
            {
                PackageManager packagemanager = new PackageManager();
                await packagemanager.UpdatePackageAsync(
                    new Uri("https://trial3.azurewebsites.net/HRApp/HRApp.msix"),
                    null,
                    DeploymentOptions.ForceApplicationShutdown
                );
            }
        }
    }

    public class Employee : INotifyPropertyChanged
    {
        private string _Approved;
        public string Name { get; set; }
        public string Vacation { get; set; }
        public string SickLeave { get; set; }
        public string Salary { get; set; }
        public string SalaryIncrease { get; set; }
        public string PrevBonus { get; set; }
        public string PrevStock { get; set; }
        public string Stock { get; set; }
        public string Bonus { get; set; }
        public string Approved
        {
            get { return _Approved; }
            set
            {
                _Approved = value;
                RaisePropertyChanged("Approved");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }

    public class EmployeeData
    {
        public List<Employee> Employees { get; set; }
    }
}
