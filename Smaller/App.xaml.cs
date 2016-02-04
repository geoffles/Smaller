using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Hardcodet.Wpf.TaskbarNotification;

namespace Smaller
{
    /// <summary>
    /// Simple application. Check the XAML for comments.
    /// </summary>
    public partial class App : Application
    {
        private Mutex _lock;
        
        private TaskbarIcon notifyIcon;

        protected override void OnStartup(StartupEventArgs e)
        {
            bool mutexIsAvailable = false;
            
            try
            {
                _lock = new Mutex(true, "Smaller.Singleton");
                mutexIsAvailable = _lock.WaitOne(1, false); // wait only 1 ms
            }
            catch (AbandonedMutexException)
            {
                //In case the previous instance did not correctly release the mutex, ignore the error.
                mutexIsAvailable = true;
            }

            if (!mutexIsAvailable)
            {
                RunJobController.Instance.TriggerRunJobs();
                Current.Shutdown();
                return;
            }
            else
            {
                base.OnStartup(e);

                //create the notifyicon (it's a resource declared in NotifyIconResources.xaml
                notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");

                RunJobController.Instance.TriggerRunJobs();
            }
        }

        

        protected override void OnExit(ExitEventArgs e)
        {
            notifyIcon.Dispose(); //the icon would clean up automatically, but this is cleaner
            _lock.ReleaseMutex();

            _lock.Dispose();
            base.OnExit(e);
        }
    }
}
