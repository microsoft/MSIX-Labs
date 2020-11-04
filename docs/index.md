# MSIX Labs for Developers

MSIX Labs for Developers is a hands-on labs experience to help developers learn how to modernize their desktop apps by taking advantage of MSIX. These labs have been carefully designed to implement one feature per exercise, intentionally delivering byte sized information, and eventually building up your toolset so you can give your desktop app a true makeover!

The labs start with the desktop app 'MyEmployees', which you will learn to package using the MSIX packaging format, and in consecutive exercises implement Windows 10 features in the packaged app. The labs will not walk through step by step by adding lines of code. They are sample based, so will add features and help you understand the mechanics behind what made the feature work.


## Related training material
If you are new to MSIX or unfamiliar with the concepts, we recommend watching the training videos prior to performing the labs. The MSIX overview video talks through the core concepts of MSIX.

* [MSIX Training Overview](https://www.microsoft.com/en-us/videoplayer/embed/RE3ig2l)
* [MSIX Training for Developers](https://www.microsoft.com/en-us/videoplayer/embed/RE3i5DH) 
* [MSIX Training Evolving Enhancing Desktop Apps v2](https://www.microsoft.com/en-us/videoplayer/embed/RE3iiD5)


## Pre-requisites

Ensure that your device has these prerequisites installed before you begin the labs:

- Windows 10, version 1903 (build 18362) or a later version
- Visual Studio 2019 or Visual Studio 2017 v15.5+ (since these versions contain the MSIX Packaging Project). Make sure you install the following workloads and optional features with Visual Studio:
  - .NET Desktop development
  - Universal Windows Platform development
  - Windows 10 SDK
- [.NET Core 3 SDK](https://dotnet.microsoft.com/download/dotnet-core) (install the latest version) (You can also do this by running the command '*winget install Microsoft.dotnet*' from your terminal.)
- [GitHub Desktop Client](https://desktop.github.com/) (You can also do this by running the command '*winget install GitHub.GitHubDesktop*' from your terminal.)
- [Activate Developer Mode](https://docs.microsoft.com/en-us/windows/apps/get-started/enable-your-device-for-development) on your device to be able to run the labs samples.

## Setup your lab environment

1. Clone the [MSIX-Labs](https://github.com/Microsoft/msix-labs) repository to your computer. You can do this either from the repository home page from your browser or by using the GitHub Desktop Client.
2. Open File Explorer and navigate to the folder path **MSIX-Labs\DeveloperLabs\MyEmployees** in your cloned MSIX-Labs repository. Pin the current location to Quick Access by selecting the 'Pin to Quick access' button in the ribbon. Right click on MyEmployees.sln and select 'Microsoft Visual Studio 2019' from the 'Open with' submenu.
3. To navigate between branches, you can use the Team Explorer in Visual Studio or use GitHub Desktop. We will be using GitHub Desktop.

## Labs structure

The branch **dev-labs-myemployees** is the first branch in the series and contains the basic Desktop app MyEmployees. The labs contain 11 exercises, each exercise adds a new feature to the MyEmployees app. The branches named **'dev-labs-exercise-*'** contain the code at the end of each exercise with the feature added.

For example, the branch **dev-labs-exercise-1-appupdate** contains the update feature implemented in exercise 1, the branch **dev-labs-exercise-2-backgroundtask** contains the background task feature implemented in exercise 2 on top of the update feature and so on.

The master branch contains all the features and you can checkout the master branch to see what the MyEmployees app looks like with all the modern features added. Additionally, the table below lists the corresponding branch for each feature and contains its GitHub hyperlink.

| Repository Branch Content                  | Repository Branch Name and Link                              |
| ------------------------------------------ | ------------------------------------------------------------ |
| Basic Desktop app: MyEmployees             | [dev-labs-myemployees](https://github.com/microsoft/MSIX-Labs/tree/dev-labs-myemployees) |
| Exercise 1: App update                     | [dev-labs-exercise-1-appupdate](https://github.com/microsoft/MSIX-Labs/tree/dev-labs-exercise-1-appupdate) |
| Exercise 2: Background Task                | [dev-labs-exercise-2-backgroundtask](https://github.com/microsoft/MSIX-Labs/tree/dev-labs-exercise-2-backgroundtask) |
| Exercise 3: Toast Notification             | [dev-labs-exercise-3-toast](https://github.com/microsoft/MSIX-Labs/tree/dev-labs-exercise-3-toast) |
| Exercise 4: Background Transfer            | [dev-labs-exercise-4-bgtransfer](https://github.com/microsoft/MSIX-Labs/tree/dev-labs-exercise-4-bgtransfer) |
| Exercise 5: Picker                         | [dev-labs-exercise-5-picker](https://github.com/microsoft/MSIX-Labs/tree/dev-labs-exercise-5-picker) |
| Exercise 6: Launcher                       | [dev-labs-exercise-6-launcher](https://github.com/microsoft/MSIX-Labs/tree/dev-labs-exercise-6-launcher) |
| Exercise 7: Share                          | [dev-labs-exercise-7-share](https://github.com/microsoft/MSIX-Labs/tree/dev-labs-exercise-7-share) |
| Exercise 8: Optional Package               | [dev-labs-exercise-8-optionalpackage](https://github.com/microsoft/MSIX-Labs/tree/dev-labs-exercise-8-optionalpackage) |
| Exercise 9: App Service                    | [dev-labs-exercise-9-appservice](https://github.com/microsoft/MSIX-Labs/tree/dev-labs-exercise-9-appservice) |
| Exercise 10: App Extension                 | [dev-labs-exercise-10-appextension](https://github.com/microsoft/MSIX-Labs/tree/dev-labs-exercise-10-appextension) |
| Exercise 11: WinRT Component               | [dev-labs-exercise-11-winrtcomponent](https://github.com/microsoft/MSIX-Labs/tree/dev-labs-exercise-11-winrtcomponent) |
| MyEmployees with all modern features added | [master](https://github.com/microsoft/MSIX-Labs/tree/master) |

## MyEmployees app setup

MyEmployees is a basic Desktop app that displays employee records and their details. To run the basic app sample:

1. Checkout branch 'dev-labs-myemployees'. From your GitHub Desktop Client, click on the 'Current branch' dropdown menu, and select the branch 'dev-labs-myemployees'.

2. From the pinned 'MyEmployees' folder in File Explorer, open MyEmployees.sln using Visual Studio 2019.

   **Note:** In the Visual Studio Solution Explorer pane, the project 'MyEmployees' is a Win32 app. The project 'MyEmployees.Package' is a [Windows Application Packaging Project](https://docs.microsoft.com/en-us/windows/msix/desktop/desktop-to-uwp-packaging-dot-net) that enables the app to be packaged as an MSIX and be modernized by taking advantage of Windows 10 features. For more information, refer to the [MSIX documentation - Package a desktop or UWP app in Visual Studio](https://docs.microsoft.com/en-us/windows/msix/package/packaging-uwp-apps).

3. Expand 'MyEmployees.Package' from the Solution Explorer. Right-click on 'Package.appxmanifest' and select 'View Designer' from the drop-down menu. Select the 'Packaging' tab within the designer view.

   **Note:** The 'Package.appxmanifest' file is the MyEmployees app's manifest file formatted as XML. The fields shown in the 'Packaging' tab describes the package when it is deployed.

4. To build the solution, in the Standard Toolbar Options, select Configuration - Debug, Platform - x64 and Project - MyEmployees.Package. Click 'Build Solution' from the main menu Build dropdown.

   <img src="Images/buildconfig.PNG" alt="build configuration" width=90%>

5. To run the application you can choose either of the following options:

      1. Start debugging by clicking on 'Local Machine' or select F5.

      2. Package the app and then install it on your local machine.

            1. Right click on the MyEmployees.Package project -> Publish -> Create App Packages. A window titled 'Create App Packages' will pop up.

            2. In the 'Select distribution method' settings, select the 'Sideloading' radio button, uncheck 'Enable automatic updates'. Click on 'Next'.

            3. In the 'Select signing method' settings, select the 'Yes, select a certificate' radio button, and click the 'Create' option. Fill in the 'Publisher Common Name' as 'Contoso' and create a password if you wish. You can also choose a Timestamp server if you wish. Click on 'Next'. 

                  If you get an error that says 'Certificate not found', just remove the current certificate and try step 3.

            4. In the 'Select and configure packages' settings, you can choose to either generate an app bundle or a package. For the purpose of these labs, let's create a package. Choose 'Never' in the dropdown menu for 'Generate app bundle'. Select x64 checkbox for 'Select the packages to create and the solution configuration mappings'. Click on 'Create'.

            5. In the 'Finished creating package' pop-up, click on the 'Output location' and open the package 'MyEmployees.Package_1.0.0.0_x64_Debug_Test'. Double click on the .msix package to install the app.

                  **Note:** If the certificate you used to sign the app is not trusted by your machine, you will not be able to install the package. 

                  To install the certificate on your local machine, right click on the .msix package and select 'Properties'. In the 'Digital Signatures' tab, select the certificate and click on 'Details'. In the 'Digital Signature Details' window, click on 'View Certificate', and then 'Install Certificate'. In the 'Certificate Import Wizard', check the 'Store Location' as 'Local Machine'. Click on 'Next'. Select the 'Place all certificates in the following store' radio button and browse to 'Trusted People'. Click on 'Next', and then 'Finish'. 

                  Now that you've installed the certificate, try installing the app.

            6. Launch the app.

6. Open Task Manager from the Windows Start Menu and click on 'More Details'. The 'Processes' tab shows the running app processes and their device resource usage. Notice the 'MyEmployees' app details here.

   In the 'Details' tab in Task Manager, notice the Package name next to MyEmployees.exe. Try changing the 'Package display name' in the MyEmployees Package.appxmanifest file and see if it gets reflected in the Task Manager!

7. Whether you chose to debug or package the app and install the MSIX on your local machine, the app gets installed. Click on the Windows start menu and search for MyEmployees, right click on the MyEmployees app and select 'App settings'. Notice the app details like Publisher and Version that were specified in the appxmanifest file. The version for the basic MyEmployees app should be 1.0.0.0.

8. To stop running the application, simply exit it.

      If you were debugging, you can also stop debugging by pressing Shift+F5 or clicking the red square 'Stop Debugging' option on the Application Insights Toolbar.

9. Cleanup:

      To uninstall the app, click on the Windows start menu and search for MyEmployees, right click on the MyEmployees app and select 'Uninstall'.

      Discard any changes made to this local branch if you are just trying out the features. From GitHub Desktop Client, from the Branch menu, select 'Discard all changes'.

10. Check out the video at the end of each exercise to see how to run the sample and how the feature works and looks like.

<video width="100%" controls>
  <source src="DemoVideos/myemployees.mp4" type="video/mp4">
  Your browser does not support the video tag.
</video>

## Exercise 1: App Update

### What does this feature do?

The app update feature checks whether there is a newer version of the app available at the specified location in the code by comparing version data, offers options to update the app now or later, and handles cancellation of the update in case of failure.

### What is the magic sauce here?

The Scenario.cs file contains the functions for all feature scenarios added in these exercises. The function InitiateAppUpdate() is responsible for checking for the update and initiating the process.

```
/// <summary>
/// Initiates the scenario app update
/// </summary>
public static void InitiateAppUpdate()
{
	bool checkForUpdate = AppUpdate.CheckforUpdate();
    if (checkForUpdate)
    {
    	AppUpdateHelper(updateAvailable, updateNowbutton_text, updateLaterbutton_text);
    }
    else
    {
    	MessageBox.Show(upToDate, appName, MessageBoxButtons.OK);
    }
}
```

The function AddPackageAsync is responsible for updating the application and relaunching it. This update is performed when the latest available version is larger than the app version. 

```
deploymentOperation = packageManager.AddPackageAsync(packageUri, null, DeploymentOptions.ForceApplicationShutdown);
```

The following code snippet listens for the package update event, updates the progress bar and handles completion. It also handles the cancellation of the update upon failure and displays the error message.

```
PackageCatalog packageCatalog = PackageCatalog.OpenForCurrentUser();
packageCatalog.PackageUpdating += OnPackageUpdating;
```

### How do I run this sample?

1. Checkout branch 'dev-labs-exercise-1-appupdate' from your GitHub Desktop Client.
2. Use the steps listed in the MyEmployees app setup section to build and run the application. Again, you can choose to either debug and run the application from Visual Studio or publish it as an MSIX package and install it.
3. Notice that the Version in the MyEmployees App Settings is now 1.0.0.1
4. Create a text file in 'C:\temp', name it version.txt and write 2.0.0.1 in it. This file is referenced by the MyEmployees app for the latest version available. In our case, since the latest version i.e. 2.0.0.1 is greater than the app version i.e. 1.0.0.1, the app will want to update itself.
5. Now publish another version of the MyEmployees app from Visual Studio, but edit the version number to 2.0.0.1, and save that to 'C:\temp'. Rename it to MyEmployees.Package.msixbundle. Note that this version should be a .msixbundle file, since the app update feature code looks for a .msixbundle file to update the app.
6. Test out the update feature in the MyEmployees app by selecting Menu -> Check for updates. Try installing the update, wait for the app to update and restart, and verify the Version in the MyEmployees App Settings.
7. Cleanup your environment as specified in the [MyEmployees app setup](#MyEmployees-app-setup) section.

<video width="100%" controls>
  <source src="DemoVideos/appupdate.mp4" type="video/mp4">
  Your browser does not support the video tag.
</video>

## Exercise 2: Background Task

### What does this feature do?

This sample configures a background task for the MyEmployees app that gets triggered every 15 minutes to check for an app update. If an update is available, it configures a box on the MyEmployees app window that says 'Update Now' to signal that an app update is available to the user.

### What is the magic sauce here?

The function InitiateBackgroundCheck() in Scenario.cs initiates and registers and sets the trigger for the background task which is referenced in the MyEmployees appxmanifest file. The [out-of-process task](https://docs.microsoft.com/en-us/windows/uwp/launch-resume/create-and-register-a-background-task) runs in the background, and when triggered, it calls the code to check for an available update, if yes, it pops up a UI box with the option for the user to update the app now or later.

```
/// <summary>
/// Initiates the scenario background task
/// </summary>
public static void InitiateBackgroundCheck()
{
    // For permission reasons, StoreVersionData is needed to be called for a file that is stored locally. It is not required to call 		StoreVersionData for web server locations
    StoreVersionData();
    BackgroundUpdateSample.BackgroundTaskImplementation();
}
```

This is the entry point for the background task in the appxmanifest file.

```
<Extension Category="windows.backgroundTasks" EntryPoint="BackgroundUpdate.BackgroundUpdateTask">
<BackgroundTasks>
<Task Type="systemEvent" />
</BackgroundTasks>
</Extension>
```

### How do I run this sample?

1. Checkout branch 'dev-labs-exercise-2-backgroundtask' from your GitHub Desktop Client.

2. In Visual Studio, open the BackgroundUpdateSample.cs file in MyEmployees -> Helpers, and comment the line:

   ```
   static MaintenanceTrigger trigger = new MaintenanceTrigger(15, true);
   ```

   Uncomment the line:

   ```
   static SystemTrigger trigger = new SystemTrigger(SystemTriggerType.TimeZoneChange, true);
   ```

   We are doing this only for testing purposes so that we can trigger the background task immediately by changing the system time zone manually, instead of waiting 15 minutes for it to happen using the MaintenanceTrigger.

3. Use the steps listed in the MyEmployees app setup section to build and run the application. Again, you can choose to either debug and run the application from Visual Studio or publish it as an MSIX package and install it.

4. Notice that the Version in the MyEmployees App Settings is now 1.0.0.2

5. Create a text file in 'C:\temp', name it version.txt and write 2.0.0.2 in it. This file is referenced by the MyEmployees app for the latest version available. In our case, since the latest version i.e. 2.0.0.2 is greater than the app version i.e. 1.0.0.2, the app will want to update itself.

6. Now publish another version of the MyEmployees app from Visual Studio, but edit the version number to 2.0.0.2, and save that to 'C:\temp'. Rename it to MyEmployees.Package.msixbundle. Note that this version should be a .msixbundle file, since the app update feature code looks for a .msixbundle file to update the app.

7. Go to Date and time settings from the Windows start menu and change the time zone. This should trigger the background task, and when it detects an available update, an 'Update Now' box pops up on the MyEmployees app window.

8. Test out the update feature in the MyEmployees app by clicking on the 'Update Now' box. Try installing the update, wait for the app to update and restart, and verify the Version in the MyEmployees App Settings.

9. Cleanup your environment as specified in the [MyEmployees app setup](#MyEmployees-app-setup) section.

<video width="100%" controls>
  <source src="DemoVideos/backgroundtask.mp4" type="video/mp4">
  Your browser does not support the video tag.
</video>

## Exercise 3: Toast Notification

### What does this feature do?

This sample makes the background task feature more elegant by configuring toast notifications for updates. That means whenever the background task detects an available update, it sends out a toast notification to the Windows notification panel to signal that an app update is available to the user.

### What is the magic sauce here?

The function ImplementToastNotification() implements the toast notification and gets called after the background task is completed.

```
public static void ImplementToastNotification()
{
    ...
    // Construct the visuals of the toast (using Notifications library)
    ToastContent toastContent = new ToastContent()
    {
    ...
    // Create the toast notification and show it
    var toast = new ToastNotification(doc);
    DesktopNotificationManagerCompat.CreateToastNotifier().Show(toast);
}
```

### How do I run this sample?

1. Checkout branch 'dev-labs-exercise-3-toast' from your GitHub Desktop Client.

2. In Visual Studio, open the BackgroundUpdateSample.cs file in MyEmployees -> Helpers, and comment the line:

   ```
   static MaintenanceTrigger trigger = new MaintenanceTrigger(15, true);
   ```

   Uncomment the line:

   ```
   static SystemTrigger trigger = new SystemTrigger(SystemTriggerType.TimeZoneChange, true);
   ```

   We are doing this only for testing purposes so that we can trigger the background task immediately by changing the system time zone manually, instead of waiting 15 minutes for it to happen using the MaintenanceTrigger.

3. Use the steps listed in the MyEmployees app setup section to build and run the application. Again, you can choose to either debug and run the application from Visual Studio or publish it as an MSIX package and install it.

4. Notice that the Version in the MyEmployees App Settings is now 1.0.0.3

5. Create a text file in 'C:\temp', name it version.txt and write 2.0.0.3 in it. This file is referenced by the MyEmployees app for the latest version available. In our case, since the latest version i.e. 2.0.0.3 is greater than the app version i.e. 1.0.0.3, the app will want to update itself.

6. Now publish another version of the MyEmployees app from Visual Studio, but edit the version number to 2.0.0.3, and save that to 'C:\temp'. Rename it to MyEmployees.Package.msixbundle. Note that this version should be a .msixbundle file, since the app update feature code looks for a .msixbundle file to update the app.

7. Go to Date and time settings from the Windows start menu and change the time zone. This should trigger the background task, and when it detects an available update, a toast notification pops up in the Windows notification side panel. This notifies the user that an update is available and reminds them to do that from the app menu.

8. Test out the update feature in the MyEmployees app by clicking on MyEmployees Menu -> Check for updates and selecting 'Update now'. Wait for the app to update and restart, and verify the Version in the MyEmployees App Settings.

9. Cleanup your environment as specified in the [MyEmployees app setup](#MyEmployees-app-setup) section.

<video width="100%" controls>
  <source src="DemoVideos/toast.mp4" type="video/mp4">
  Your browser does not support the video tag.
</video>

## Exercise 4: Background Transfer

### What does this feature do?

The background transfer feature enables the MyEmployees app to download new employee records in the background, while the app can still be used. The employee records in the app window are updated once the background transfer is complete. This transfer is triggered automatically whenever new employee records are present by the same background task that is also used to detect app updates.

### What is the magic sauce here?

The function DownloadNewEmployeesRecordsAsync() is called when the background task is executed, which creates a new BackgroundDownloader object and updates the employee records.

```
/// <summary>
/// Creates a download operation and initiates the download from a web server
/// </summary>
private static async Task DownloadNewEmployeesRecordsAsync()
{
    Uri source = new Uri("https://contososoftwaremyemp.blob.core.windows.net/$web/EmployeeData.csv");
    var localFolder = ApplicationData.Current.LocalFolder;
    // Creates or replaces DownloadTemp.CSV in the ApplicationData's local folder
    StorageFile file = await localFolder.CreateFileAsync("DownloadTemp.CSV", CreationCollisionOption.ReplaceExisting);
    BackgroundDownloader downloader = new BackgroundDownloader();
    DownloadOperation download = downloader.CreateDownload(source, file);
    await download.StartAsync();
}
```

### How do I run this sample?

1. Checkout branch 'dev-labs-exercise-4-bgtransfer' from your GitHub Desktop Client.

2. In Visual Studio, open the BackgroundUpdateSample.cs file in MyEmployees -> Helpers, and comment the line:

   ```
   static MaintenanceTrigger trigger = new MaintenanceTrigger(15, true);
   ```

   Uncomment the line:

   ```
   static SystemTrigger trigger = new SystemTrigger(SystemTriggerType.TimeZoneChange, true);
   ```

   We are doing this only for testing purposes so that we can trigger the background task immediately by changing the system time zone manually, instead of waiting 15 minutes for it to happen using the MaintenanceTrigger.

3. Use the steps listed in the MyEmployees app setup section to build and run the application. Again, you can choose to either debug and run the application from Visual Studio or publish it as an MSIX package and install it.

4. Notice that the Version in the MyEmployees App Settings is now 1.0.0.4

5. Go to Date and time settings from the Windows start menu and change the time zone. This should trigger the background task, and when it detects new employee records, they are automatically fetched using the background transfer feature and updated in the app window.

6. Cleanup your environment as specified in the [MyEmployees app setup](#MyEmployees-app-setup) section.

<video width="100%" controls>
  <source src="DemoVideos/bgtransfer.mp4" type="video/mp4">
  Your browser does not support the video tag.
</video>

## Exercise 5: Picker

### What does this feature do?

The picker feature modernizes the app even further by enabling users to pick a profile picture for each employee record and displays it in the app!

### What is the magic sauce here?

The function PickFileAsync() in Scenario.cs sets up the file picker to allow the user to select and upload an image file for each employee.

```
/// <summary>
/// Pops up a file picker that allows the user to pick a single file
/// </summary>
/// <returns>A StorageFile object that represents the file the user picked</returns>
public static async Task<StorageFile> PickFileAsync()
{
    // Creates the picker object and sets some of its properties
    FileOpenPicker openPicker = new FileOpenPicker();
    openPicker.FileTypeFilter.Add(".jpg");
    openPicker.FileTypeFilter.Add(".jpeg");
    openPicker.FileTypeFilter.Add(".png");
    openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
    // Assigns the window handle to the file pickers' UI
    IInitializeWithWindow initWindow = (IInitializeWithWindow)(object)openPicker;
    initWindow.Initialize(GetMainWindowHandle());
    // Opens the file picker for the user to pick a single file
    StorageFile file = await openPicker.PickSingleFileAsync();
    return file;
}
```

### How do I run this sample?

1. Checkout branch 'dev-labs-exercise-5-picker' from your GitHub Desktop Client.
2. Use the steps listed in the MyEmployees app setup section to build and run the application. Again, you can choose to either debug and run the application from Visual Studio or publish it as an MSIX package and install it.
3. Notice that the Version in the MyEmployees App Settings is now 1.0.0.5
4. Click on the corresponding 'Img' column for the employee record you wish to update, select 'Upload new picture' and pick the image from the File Explorer window. See it get reflected in the MyEmployees app window.
5. Cleanup your environment as specified in the [MyEmployees app setup](#MyEmployees-app-setup) section.

<video width="100%" controls>
  <source src="DemoVideos/picker.mp4" type="video/mp4">
  Your browser does not support the video tag.
</video>

## Exercise 6: Launcher

### What does this feature do?

This feature configures launchers in the MyEmployees app for the Photos app (to view employee profile picture), Mail app (for mailto options) and  Bing maps (to view employee location on the map, get directions etc.).

### What is the magic sauce here?

In the Scenarios.cs file, the functions LaunchMailApp(), LaunchPhotosApp() and LaunchMapsApp() are responsible for setting up launchers at different points in the MyEmployees app. For example, the function LaunchMailApp() is triggered through a click event on the employee email.

```
/// <summary>
/// Launches the default email app and creates a new message with the specified email address
/// </summary>
/// <param name="email">The specified email address</param>
public static async void LaunchMailApp(string email)
{
    String path = "mailto:" + email;
    var success = await Windows.System.Launcher.LaunchUriAsync(new Uri(path));
    if (!success)
    {
    	MessageBox.Show("The mail uri launcher has failed");
    }
}
```

### How do I run this sample?

1. Checkout branch 'dev-labs-exercise-6-launcher' from your GitHub Desktop Client.
2. Use the steps listed in the MyEmployees app setup section to build and run the application. Again, you can choose to either debug and run the application from Visual Studio or publish it as an MSIX package and install it.
3. Notice that the Version in the MyEmployees App Settings is now 1.0.0.6
4. Click on any employee 'Img -> View picture' to launch the Photos app. Click on any employee Email to launch the Mail app. Click on any employee Address to launch Bing Maps.
5. Cleanup your environment as specified in the [MyEmployees app setup](#MyEmployees-app-setup) section.

<video width="100%" controls>
  <source src="DemoVideos/launcher.mp4" type="video/mp4">
  Your browser does not support the video tag.
</video>

## Exercise 7: Share

### What does this feature do?

This feature configures the share source feature for the MyEmployees app, so a user can share an employee profile image using any app of their choice.

### What is the magic sauce here?

The function InitiateShare() in Scenarios.cs file registers the app as a share source and also sets up the UI for sharing the employee picture.

```
/// <summary>
/// Initiates the scenario share and pops up the standard share UI
/// </summary>
public static void InitiateShare()
{
	Share.RegisterForSharing(GetMainWindowHandle());
	ShareDataTransferManager.ShowShareUIForWindow(GetMainWindowHandle());
}
```

### How do I run this sample?

1. Checkout branch 'dev-labs-exercise-7-share' from your GitHub Desktop Client.
2. Use the steps listed in the MyEmployees app setup section to build and run the application. Again, you can choose to either debug and run the application from Visual Studio or publish it as an MSIX package and install it.
3. Notice that the Version in the MyEmployees App Settings is now 1.0.0.7
4. Click on any employee 'Img -> Share picture' to launch the share window and choose any app on your device to share the picture.
5. Cleanup your environment as specified in the [MyEmployees app setup](#MyEmployees-app-setup) section.

<video width="100%" controls>
  <source src="DemoVideos/share.mp4" type="video/mp4">
  Your browser does not support the video tag.
</video>

## Exercise 8: Optional Package

### What does this feature do?

Optional packages contain content that can be integrated with a main package. These are useful for downloadable content (DLC), dividing a large app for size restraints, or for shipping any additional content separate from your original app. In this exercise we want to add more functionality to the MyEmployees app and allow it to import HR data for the employee records. Instead of bloating the base app, we decide to create an optional package called 'MyEmployeesHR' which will seamlessly plug into the base app, and allows us to componentize the MyEmployees app. This has a few benefits like being able to customize this optional package outside of the main app, monetize it separately, the freedom to choose among distribution methods, for example, the Windows Store, and the Windows platform already knows how to install the package and manage it.

### What is the magic sauce here?

The optional package project 'OptionalPackage' is a separate UWP app project that is added as a dependent on the main MyEmployees project in the optional package's appxmanifest file.

```
<uap3:MainPackageDependency Name="MyEmployees" />
```

The function LoadDataFromOptionalPackageAsync() implements the main functionality of the optional package i.e. importing HR data, by calling LoadHrData(), which the updates the employee records with the data.

```
/// <summary>
/// Searches for an optional package in the main package dependencies
/// </summary>
/// <returns>The HrData folder shared from the optional package or null if there is no optional package</returns>
public static async Task<StorageFolder> LoadDataFromOptionalPackageAsync()
{
    foreach (var package in Windows.ApplicationModel.Package.Current.Dependencies)
    {
        if (package.IsOptional)
        {
        	return await LoadHrData(package);
        }
    }
    MessageBox.Show("Please install the optional package. (Refer to the readme for further instructions)");
    return null;
}

/// <summary>
/// Retrieves the HrData folder from the optional package
/// </summary>
/// <param name="package">The optional package that contains the HrData folder</param>
/// <returns>The HrData folder shared from the optional package</returns>
public static async Task<StorageFolder> LoadHrData(Windows.ApplicationModel.Package package)
{
    StorageFolder appInstalledFolder = package.InstalledLocation;
    return await appInstalledFolder.GetFolderAsync("HrData");
}
```

### How do I run this sample?

1. Checkout branch 'dev-labs-exercise-8-optionalpackage' from your GitHub Desktop Client.

2. Use the steps listed in the MyEmployees app setup section to build and run the application. Again, you can choose to either debug and run the application from Visual Studio or publish it as an MSIX package and install it.

3. Now publish the optional package (project name 'OptionalPackage') from Visual Studio, and install that too. Optional packages are published as appx packages. Note that the main package should be installed before the optional package.

   If you chose to run the MyEmployees app via debug in the previous step, you can deploy the optional package from Visual Studio by right clicking on the project 'OptionalPackage' and selecting 'Deploy'.

4. Test out the feature added by the optional package in the MyEmployees app by clicking on MyEmployees Menu -> 'Import employee HR data' and verify that the compensation fields got populated. If you close and restart the MyEmployees app, this data still persists (This is only if you installed the app, not via debug). Observe that the Annual compensation field is still 0s for all employees, and this is because we will extend the app with a compensation calculator in the next exercise!

5. Bonus: Wait for 15 minutes and see if the app updates employee records and pulls in new ones! Try importing HR data for these records too!

6. Notice that the Version in the MyEmployees App Settings is now 1.0.0.8. Scroll down the App Settings window and you will find the optional package 'MyEmployeesHRPackage' under 'App add-ons & downloadable content'.

7. Cleanup your environment as specified in the [MyEmployees app setup](#MyEmployees-app-setup) section.

<video width="100%" controls>
  <source src="DemoVideos/optionalpackage.mp4" type="video/mp4">
  Your browser does not support the video tag.
</video>

## Exercise 9: App  Service

### What does this feature do?

App services are UWP apps that provide services to other packaged apps. They are analogous to web services, on a device. An app service runs as a background task in the host app and can provide its service to other apps. For example, an app service might provide a bar code scanner service that other apps could use. Or perhaps an Enterprise suite of apps has a common spell checking app service that is available to the other apps in the suite. App services let developers create UI-less services that apps can call on the same device, and starting with Windows 10, version 1607, on remote devices. Starting in Windows 10, version 1607, app services that run in the same process as the host app can be created. This exercise focuses on creating and consuming an app service that runs in a separate background process. This exercise sample configures an app service called 'MyEmployeesCalcService' to calculate the annual compensation for employees and is used by the MyEmployees app to populate annual compensation in the employee records.

### What is the magic sauce here?

The app service project 'MyEmployeesCalcService' is a separate UWP app project, and 'MyAppService' is a Windows Runtime Component, that serves as the calculator service. This service cannot directly be referenced by the MyEmployees app, so the 'MyEmployeesCalcService' serves as the provider to link MyEmployees to the calc service via the appxmanifest file.

```
<uap:Extension Category="windows.appService" EntryPoint="MyAppService.AnnualCompCalculator">
<uap3:AppService Name="com.microsoft.AnnualCompCalculator" uap4:SupportsMultipleInstances="true"/>
</uap:Extension>
```

 The function CallAppServiceAsync() is where the magic happens. This function establishes a connection to the app service, which is running in the background, and passes it employee information like hours worked, hourly compensation, and receives the total compensation calculated by the service in an asynchronous way.

```
/// <summary>
/// Establishes a connection to an AppService and calls the service
/// </summary>
/// <param name="packageFamilyName">The app service provider's package family name</param>
/// <param name="appServiceName">The app service name defined in the app service provider's Package.appxmanifest file</param>
/// <returns>A response message from the AppService</returns>
public static async Task<ValueSet> CallAppServiceAsync(string packageFamilyName, string appServiceName, int numberOfEmployees)
{
    AppServiceConnection appService = null;
    if (appService == null)
    {
        appService = new AppServiceConnection();
        ValueSet message = new ValueSet();
        appService.AppServiceName = appServiceName;
        appService.PackageFamilyName = packageFamilyName;
        message.Add("EmployeeHoursWorkedData", Form1.employeeHoursWorked);
        message.Add("EmployeeHourlyCompData", Form1.employeeHourlyComp);
        message.Add("numberOfEmployees", numberOfEmployees);
        var status = await appService.OpenAsync();
        if (status == AppServiceConnectionStatus.Success)
        {
            AppServiceResponse response = await appService.SendMessageAsync(message);
            return response.Message;
        }
        else
        {
        	MessageBox.Show(status.ToString());
    	}
	}
	return null;
}
```

### How do I run this sample?

1. Checkout branch 'dev-labs-exercise-9-appservice' from your GitHub Desktop Client.

2. Publish the app packages:

   1. First, publish the app service (project name 'MyEmployeesCalcService') from within Visual Studio. Then, make sure that the app service Package family name is accurately referenced in the MyEmployees app code.

      1. In the MyEmployeesCalcService project's Package.appxmanifest file, under Packaging tab, copy the value for 'Package family name'.

      2. Now in MyEmployees project, file Form1.cs, function calculateAnnualCompensationToolStripMenuItem_Click(), verify that the first argument in CallAppServiceAsync() is same as the package family name value you copied in the previous step.

      In the example below, it is "MyEmployeesCalcService_rv8ym4y7mg4aw".

      ```
      private async void calculateAnnualCompensationToolStripMenuItem_Click(object sender, EventArgs e)
      {
          if (employeeHourlyComp != null && employeeHoursWorked != null)
          {
              ValueSet annualComp = await Scenarios.CallAppServiceAsync("MyEmployeesCalcService_rv8ym4y7mg4aw",
              "com.microsoft.AnnualCompCalculator", employeeBindingSource.Count);
              ...
          }
          ...
      }
      ```

      

   2. Then publish the MyEmployees package and the optional package (project name 'OptionalPackage') from Visual Studio, just like in the previous exercise.

   3. Then publish the MyEmployees package.

3. Install the MyEmployees main package first, then the optional package and app service.

4. Import the employee HR data in the MyEmployees app by clicking on MyEmployees Menu -> 'Import employee HR data' and verify that the compensation fields got populated. Now test out the app service by clicking on the MyEmployees Menu -> 'Calculate annual compensation' and verify that the field 'Annual Comp.' got populated.

5. Bonus: Wait for 15 minutes and see if the app updates employee records and pulls in new ones! Try importing HR data and calculating annual compensation for these records too!

6. Notice that the Version in the MyEmployees App Settings is now 1.0.0.9. Also notice that the app service 'MyEmployeesCalcService' shows up in the Windows Start Menu.

7. Cleanup your environment as specified in the [MyEmployees app setup](#MyEmployees-app-setup) section. Don't forget to uninstall the app service too.

<video width="100%" controls>
  <source src="DemoVideos/appservice.mp4" type="video/mp4">
  Your browser does not support the video tag.
</video>

## Exercise 10: App Extension

### What does this feature do?

In Windows 10, app extensions provide functionality similar to what plug-ins, add-ins, and add-ons do on other platforms. They are UWP apps or packaged desktop apps that have an extension declaration that allows them to share content and deployment events with a host app. An extension app can provide multiple extensions. App extensions are similar to optional packages with a key difference in the level of trust - App extensions are meant for 3rd parties to build add-on experiences to your application which the app may not trust fully while optional packages run with the identity of the base app. So when the MyEmployees app is declared as an extension host, it creates an opportunity to develop an ecosystem around the app in which other developers can enhance it through extensions. For example, Microsoft Office extensions, Visual Studio extensions, browser extensions, etc. create richer experiences for the base apps.

In this exercise, the app extension called 'MyEmployeesImageExtension' allows the MyEmployees app user to set app background images through a simple app menu option.

### What is the magic sauce here?

The project 'MyEmployeesImageExtension' is a UWP project that is declared as an app extension in its appxmanifest.

```
<uap3:Extension Category="windows.appExtension">
<uap3:AppExtension Name="com.microsoft.contosoassetext"
Id="BackgroundImage"
DisplayName="Background Image"
Description ="This extension provides image assets"
PublicFolder="Public">
</uap3:AppExtension>
```

The MyEmployees app is registered as an app extension host in its appxmanifest so that it can be aware of the app extension.

```
<uap3:Extension Category="windows.appExtensionHost">
<uap3:AppExtensionHost>
<uap3:Name>com.microsoft.contosoassetext</uap3:Name>
</uap3:AppExtensionHost>
</uap3:Extension>
```

The function InitiateAndExecuteAppExtensions() serves as an entry point and executes the scenario by calling ExecuteImageLoadScenario() to load the image and set it as the MyEmployees background.

```
/// <summary>
/// Initiates and executes the scenario app extensions
/// </summary>
public static async void InitiateAndExecuteAppExtensions()
{
    AppExtension extension = null;
    extension = await ExtensionManager.GetAppExtension(appExtensionId, appExtensionName);
    if (extension == null)
    {
    	MessageBox.Show("Please install the app extension (Refer to the readme for further instructions) ");
    }
    else
    {
    	ExtensionManager.ExecuteImageLoadScenario(extension);
    }
}
```

### How do I run this sample?

1. Checkout branch 'dev-labs-exercise-10-appextension' from your GitHub Desktop Client.

2. Use the steps listed in the MyEmployees app setup section to build and run the application. Again, you can choose to either debug and run the application from Visual Studio or publish it as an MSIX package and install it.

3. Now publish the app extension (project name 'MyEmployeesImageExtension') from Visual Studio, and install that too. App extensions are published as appx packages. Note that the main package should be installed before the app extension.

   If you chose to run the MyEmployees app via debug in the previous step, you can deploy the app extension from Visual Studio by right clicking on the project 'MyEmployeesImageExtension' and selecting 'Deploy'.

4. You will need to install the optional package and app service as explained in previous exercises to see all the features work together, although the app extension feature is not coupled with those, and will work independently as long as the main MyEmployees app is installed.

5. Notice that the Version in the MyEmployees App Settings is now 1.0.0.10

6. Click on the MyEmployees 'Menu -> Change background image' option and notice the background image has changed in the app window.

7. Cleanup your environment as specified in the [MyEmployees app setup](#MyEmployees-app-setup) section. Don't forget to uninstall the app service and app extension too.

<video width="100%" controls>
  <source src="DemoVideos/appextension.mp4" type="video/mp4">
  Your browser does not support the video tag.
</video>

## Exercise 11: Using a WinRT Component

### What does this feature do?

A Windows Runtime component is a self-contained software module that can be authored, referenced, and used with any Windows Runtime language (including C#, C++/WinRT, Visual Basic, JavaScript, and C++/CX). This exercise sample creates a Windows Runtime component (project 'RuntimeComponent') that is used in the MyEmployees app and enables the MyEmployees user to export employee data to a csv file.

### What is the magic sauce here?

The WinRT Component project 'RuntimeComponent' implements the export data functionality. When this project is built, it produces a .winmd file, which is referenced by MyEmployees in its .csproj file. The MyEmployees project has been ported to .NET Core to be able to use the WinRT component.

The function ExportData() implements the meat of the functionality.

```
/// <summary>
/// Calls an API from the WinRT Component, which exports employee hr data to a specified file
/// </summary>
/// <param name="data">The data that is passed in to the WinRT API</param>
public static async void ExportData(IList data)
{
    try
    {
        StorageFile file = await PickCsvFileAsync();
        if (file != null && RuntimeComponent.Class1.ExportData(data, file.Path))
        {
        	MessageBox.Show("The file was successfully saved");
        }
    }
    catch (Exception e)
    {
        MessageBox.Show(e.Message);
    }
}
```

### How do I run this sample?

1. Checkout branch 'dev-labs-exercise-11-winrtcomponent' from your GitHub Desktop Client.
2. Use the steps listed in the MyEmployees app setup section to build and run the application. Again, you can choose to either debug and run the application from Visual Studio or publish it as an MSIX package and install it.
3. You will need to install the optional package, app service and app extension as explained in previous exercises to see all the features work together, although the WinRT component is not coupled with those, and will work independently as long as the main MyEmployees app is installed.
4. Notice that the Version in the MyEmployees App Settings is now 1.0.0.11
5. To test the WinRT component, create a csv file in 'C:\temp' and name it 'emp.csv'. Click on the MyEmployees 'Menu -> Export employee data' option and select the file emp.csv you just created. A pop up dialog box indicates that the file was successfully saved. Verify that the file emp.csv has all the employee records.
6. Cleanup your environment as specified in the [MyEmployees app setup](#MyEmployees-app-setup) section.

<video width="100%" controls>
  <source src="DemoVideos/winrt.mp4" type="video/mp4">
  Your browser does not support the video tag.
</video>

## Conclusion

We hope that you enjoyed working through these lab exercises and learnt more about modernizing your Windows app! We recommend the following channels to stay engaged with the MSIX team:

- [Open an issue related to MSIX labs](https://github.com/microsoft/MSIX-Labs/issues)
- [MSIX Tech Community](https://techcommunity.microsoft.com/t5/msix/ct-p/MSIX)
- [MSIX Documentation](https://docs.microsoft.com/en-us/windows/msix/overview)