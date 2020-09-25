using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.ApplicationModel.AppExtensions;
using Windows.Storage;

namespace MyEmployees.Helpers
{
    public class ExtensionManager
    {
        private static AppExtensionCatalog _Catalog;
        static readonly string fileName = "image";
        static int imageNumber = 1;

        /// <summary>
        /// Retrieves the app extension specified by the input
        /// </summary>
        /// <param name="appExtensionName">Uniquely identifies a set of extensions within a namespace</param>
        /// <param name="appExtensionId">Uniquely identifies an extension within a set of extensions</param>
        /// <returns>An AppExtension that matches the input</returns>
        public static async Task<AppExtension> GetAppExtension(String appExtensionId, String appExtensionName)
        {
            AppExtension extension = await AppExtensionsHelper(appExtensionId, appExtensionName);
            return extension;
        }

        /// <summary>
        /// Loads an image file from an app extension and updates the UI with the asset 
        /// </summary>
        /// <param name="extension">Contains information about an AppExtension</param>
        public static async void ExecuteImageLoadScenario(AppExtension extension)
        {
            StorageFile file = null;
            file = await LoadAsset(extension);
            if (file != null)
            {
                ExportDataLibrary.Program._instance.Invoke(new MethodInvoker(delegate
                {
                    ExportDataLibrary.Program._instance.ChangeBackgroundImage(file.Path);
                    ExportDataLibrary.Program._instance.SaveBackgroundImage(file);
                }));
            }
        }

        /// <summary>
        /// Opens a catalog of extensions and retrieves the extension specified
        /// </summary>
        /// <param name="appExtensionName">The namespace of the app extension ex: "com.microsoft.contosoAssest"</param>
        /// <param name="appExtensionId">Uniquely identifies an extension within an extension namespace</param>
        /// <remarks>The namespace of the extension must be specified in the manifest of the host</remarks>
        /// <returns>A Uniquely identified extension within the app extension's namespace</returns>
        private static async Task<AppExtension> AppExtensionsHelper(String appExtensionId, String appExtensionName)
        {
            AppExtension extension = null;
            try
            {
                // Opens a catalog of extensions with the specified extension namespace 
                _Catalog = AppExtensionCatalog.Open(appExtensionName);
                IReadOnlyList<AppExtension> extensions = await _Catalog.FindAllAsync();
                foreach (AppExtension ext in extensions)
                {
                    if (ext.Id == appExtensionId)
                    {
                        extension = ext;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return extension;
        }

        /// <summary>
        /// Reads a file from the isolated storage of an app extension
        /// </summary>
        /// <param name="appExtension">Provides information about the specified app extension</param>
        /// <returns>A StorageFile object that represents an image asset</returns>
        private static async Task<StorageFile> LoadAsset(AppExtension appExtension)
        {
            StorageFile extensionAssetFile = null;
            StorageFolder folder = await appExtension.GetPublicFolderAsync();
            if (folder != null)
            {
                try
                {
                    extensionAssetFile = await folder.GetFileAsync(fileName + imageNumber + ".jpg");
                    imageNumber = (imageNumber % 3) +1 ;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return extensionAssetFile;
        }
    }
}
