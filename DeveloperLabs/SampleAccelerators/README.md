## Overview
This section of MSIX Developer Labs provides a sample accelerator that can be used as a guide by developers to create Accelerators for their applications.  

## What are Accelerators?

An Accelerator provides an efficient way to capture important information regarding conversion of legacy applications to MSIX format
Read more about [Accelerators](https://learn.microsoft.com/en-us/windows/msix/toolkit/msix-toolkit-overview) here. 

## Sample Accelerator
When the given sample application "MyEmployees.msi" is packaged as an MSIX App using Msix Packaging tool, the logfile.txt is not being created.The logfile.txt tracks the events that occur during the usage of the application.
This happens in this partciluar case as the MSIX App does not have write access inside the installation folder "C:\Program Files\\WindowsApps\\MyEmployees_8h66172c634n0\".

To solve this problem, you can use the sample accelerator "Contoso.MyEmployees.yaml" during the conversion from MsixPackagingTool. 
This will automatically add the InstalledLocationVirtualization tag to the MSIX package manifest.
This redirects the log file writes into the install location to a safe location in the app data, where the application has write access.
You can find the log file for the newly generated MSIX package here, "%localappdata%\Packages\MyEmployees_8h66172c634n0\LocalCache\Local\VFS\C$\ Program Files (x86)\Contoso\MyEmployees

## Feedback

You can create an issue [here](https://github.com/microsoft/MSIX-Labs/issues).

