using System;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
using Windows.Foundation.Collections;

namespace MyAppService
{
    public sealed class AnnualCompCalculator : IBackgroundTask
    {
        private BackgroundTaskDeferral backgroundTaskDeferral;
        private AppServiceConnection appServiceconnection;

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            // Gets a deferral so that the service isn't terminated
            this.backgroundTaskDeferral = taskInstance.GetDeferral();

            // Associates a cancellation handler with the background task
            taskInstance.Canceled += OnTaskCanceled;

            // Retrieves the app service connection and sets up a listener for incoming app service requests
            var details = taskInstance.TriggerDetails as AppServiceTriggerDetails;
            appServiceconnection = details.AppServiceConnection;
            appServiceconnection.RequestReceived += OnRequestReceived;
        }

        private ValueSet CalculateEmployeeAnnualCompData(string[] hourlyCompData, string[] hoursWorkedData, int numberOfEmployees)
        {
            ValueSet annualCompData = new ValueSet();
            for (int i = 0; i < numberOfEmployees; i++)
            {
                String employeeId = (i + 1).ToString();
                int weeklyComp = (Convert.ToInt32(hoursWorkedData[i]) * Convert.ToInt32(hourlyCompData[i]));
                int annualComp = weeklyComp * 4 * 12;
                annualCompData.Add(employeeId, annualComp);
            }
            return annualCompData;
        }

        private async void OnRequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
        {
            // Gets a deferral because we use an awaitable API below to respond to the message
            // and we don't want this call to get canceled while we are waiting
            var messageDeferral = args.GetDeferral();
            ValueSet message = args.Request.Message;
            string[] hourlyCompData = (string[])message["EmployeeHoursWorkedData"];
            string[] hoursWorkedData = (string[])message["EmployeeHourlyCompData"];
            int numberOfEmployees = (int)message["numberOfEmployees"];
            try
            {
                ValueSet annualCompData = CalculateEmployeeAnnualCompData(hourlyCompData, hoursWorkedData, numberOfEmployees);
                // Return the data to the caller.
                await args.Request.SendResponseAsync(annualCompData);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                // Completes the deferral so that the platform knows that we're done responding to the app service call
                messageDeferral.Complete();
            }
        }

        private void OnTaskCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            if (this.backgroundTaskDeferral != null)
            {
                // Completes the service deferral
                this.backgroundTaskDeferral.Complete();
            }
        }
    }
}