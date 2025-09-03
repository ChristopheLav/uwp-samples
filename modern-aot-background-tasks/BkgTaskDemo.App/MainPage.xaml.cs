using BkgTaskDemo.Task.RuntimeComponent;
using System;
using Windows.ApplicationModel.Background;
using Windows.UI.Xaml.Controls;

namespace BkgTaskDemoApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a <see cref="Frame">.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public async void RegisterButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            const string taskName = "ToasterBackgroundTask";
            var taskType = typeof(ToasterBackgroundTask);

            foreach (var task in BackgroundTaskRegistration.AllTasks)
            {
                if (task.Value.Name == taskName)
                {
                    task.Value.Unregister(true);
                }
            }

            var access = await BackgroundExecutionManager.RequestAccessAsync();
            if (access == BackgroundAccessStatus.DeniedByUser || access == BackgroundAccessStatus.Unspecified)
            {
                return;
            }

            var builder = new BackgroundTaskBuilder
            {
                Name = taskName,
                TaskEntryPoint = taskType.FullName
                //"RuntimeComponent1.ToasterBackgroundTask" //taskType.FullName
            };


            builder.SetTrigger(new TimeTrigger(15, false));
            builder.Register();
        }

        public void RunButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var task = new ToasterBackgroundTask();
            task.SendToast();
        }
    }
}
