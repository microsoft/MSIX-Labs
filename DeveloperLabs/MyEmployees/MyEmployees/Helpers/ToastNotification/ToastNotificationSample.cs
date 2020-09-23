using Microsoft.Toolkit.Uwp.Notifications;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace MyEmployees.Helpers.Toast
{
    class ToastNotificationSample
    {
        const string aumid = "MyEmployees_rv8ym4y7mg4aw!App";
        const string updateAvailable = "There is an update available.\nPlease click 'Menu' -> 'check for updates'to update.";
        public static void ImplementToastNotification()
        {
            // Register AUMID and COM server (for MSIX/sparse package apps, this no-ops)
            DesktopNotificationManagerCompat.RegisterAumidAndComServer<MyNotificationActivator>(aumid);

            // Construct the visuals of the toast (using Notifications library)
            ToastContent toastContent = new ToastContent()
            {
                // Arguments when the user taps body of toast
                Launch = "app-defined-string",

                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
            {
                new AdaptiveText()
                {
                    Text = updateAvailable
                },
            }
                    }
                }
            };
            // Create the XML document (BE SURE TO REFERENCE WINDOWS.DATA.XML.DOM)
            var doc = new XmlDocument();
            doc.LoadXml(toastContent.GetContent());
            // Create the toast notification and show it
            var toast = new ToastNotification(doc);
            DesktopNotificationManagerCompat.CreateToastNotifier().Show(toast);
        }
    }
}
