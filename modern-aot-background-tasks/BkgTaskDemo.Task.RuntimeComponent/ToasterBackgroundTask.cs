using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace BkgTaskDemo.Task.RuntimeComponent
{
    public sealed partial class ToasterBackgroundTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();
            try
            {
                SendToast();
            }
            finally
            {
                deferral.Complete();
            }
        }

        public void SendToast()
        {
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText02);
            XmlNodeList textElements = toastXml.GetElementsByTagName("text");
            textElements[0].AppendChild(toastXml.CreateTextNode("A toast example"));
            textElements[1].AppendChild(toastXml.CreateTextNode("You've changed timezones!"));
            ToastNotification notification = new(toastXml);
            ToastNotificationManager.CreateToastNotifier().Show(notification);
        }
    }
}
