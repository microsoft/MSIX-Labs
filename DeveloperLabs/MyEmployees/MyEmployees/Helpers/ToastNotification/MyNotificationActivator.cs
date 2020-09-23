// The GUID CLSID must be unique to your app. Create a new GUID if copying this code.
using System.Runtime.InteropServices;
namespace MyEmployees.Helpers.Toast
{
    [ClassInterface(ClassInterfaceType.None)]
    [ComSourceInterfaces(typeof(INotificationActivationCallback))]
    [Guid("2b6f0e1e-ec5e-41c8-8646-2602ebde053e"), ComVisible(true)]

    public class MyNotificationActivator : NotificationActivator
    {
        public override void OnActivated(string invokedArgs, NotificationUserInput userInput, string appUserModelId)
        {
            // TODO: Handle activation
        }
    }
}