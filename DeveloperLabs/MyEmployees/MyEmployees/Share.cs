using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;

namespace MyEmployees.Helpers
{
    class Share
    {
        public static readonly string imageFileName = "ShareImage.png";

        /// <summary>
        /// Adds a DataRequested event handler to be called whenever a user invokes share
        /// </summary>
        /// <param name="hwnd">The main window handle of the associated process</param>
        public static void RegisterForSharing(IntPtr hwnd)
        {
            var dataTransferManager = ShareDataTransferManager.GetForWindow(hwnd);
            dataTransferManager.DataRequested += DataTransferManager_DataRequested;
        }

        /// <summary>
        /// Gets an image to the share file in Png format  
        /// </summary>
        /// <returns>A StorageFile object that contains the share image</returns>
        private static async Task<StorageFile> GetShareFileAsync()
        {
            StorageFile sharefile = null;
            var localFolder = ApplicationData.Current.LocalFolder;
            if (File.Exists(Path.Combine(localFolder.Path, imageFileName)))
            {
                // Reads in the imageFile and stores the file in an object
                sharefile = await localFolder.GetFileAsync(imageFileName);
            }
            return sharefile;
        }

        /// <summary>
        /// Creates a DataRequested event handler to be called whenever a user invokes share
        /// </summary>
        private static async void DataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs e)
        {
            var defferal = e.Request.GetDeferral();
            DataRequest request = e.Request;
            request.Data.Properties.Title = "Share From MyEmployees";
            StorageFile shareImageFile = await GetShareFileAsync();
            Scenarios.shareImage = null;
            if (shareImageFile != null)
            {
                IEnumerable<IStorageItem> transferData = new IStorageItem[] { shareImageFile };
                request.Data.SetStorageItems(transferData);
                request.Data.SetText("Sharing this image");
            }
            else
            {
                MessageBox.Show("The image file cannot be opened");
            }
            defferal.Complete();
        }
    }
}
