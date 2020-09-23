using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;

namespace MyEmployees.Helpers
{
    /// <remarks>
    /// Code from https://github.com/microsoft/Windows-classic-samples/blob/master/Samples/ShareSource/wpf/DataTransferManagerHelper.cs
    /// </reamrks>
    class ShareDataTransferManager
    {
        static readonly Guid _dtm_iid = new Guid(0xa5caee9b, 0x8708, 0x49d1, 0x8d, 0x36, 0x67, 0xd2, 0x5a, 0x8d, 0xa0, 0x0c);

        /// <summary>
        /// Enables access to DataTransferManager methods in desktop apps
        /// </summary>
        /// <returns>An IDataTransferManagerInterop instance that implements the activation factory interface for the specified Windows Runtime type</returns>
        static IDataTransferManagerInterop DataTransferManagerInterop
        {
            get
            {
                return (IDataTransferManagerInterop)WindowsRuntimeMarshal.GetActivationFactory(typeof(DataTransferManager));
            }
        }

        /// <summary>
        /// Gets the DataTransferManager object that is associated with the active window
        /// </summary>
        /// <param name="hwnd">The main window handle of the associated process</param>
        /// <returns>The DataTransferManager object associated with the current window</returns>
        public static DataTransferManager GetForWindow(IntPtr hwnd)
        {
            return DataTransferManagerInterop.GetForWindow(hwnd, _dtm_iid);
        }

        /// <summary>
        /// Programmatically initiates the user interface for sharing content with another app
        /// </summary>
        /// <param name="hwnd">The main window handle of the associated process</param>
        public static void ShowShareUIForWindow(IntPtr hwnd)
        {
            DataTransferManagerInterop.ShowShareUIForWindow(hwnd);
        }

        [ComImport, Guid("3A3DCD6C-3EAB-43DC-BCDE-45671CE800C8")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        interface IDataTransferManagerInterop
        {
            DataTransferManager GetForWindow([In] IntPtr appWindow, [In] ref Guid riid);
            void ShowShareUIForWindow(IntPtr appWindow);
        }
    }
}
