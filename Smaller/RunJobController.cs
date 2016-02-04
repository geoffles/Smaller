using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Threading;

namespace Smaller
{
    public class RunJobController
    {
        private  static readonly EventWaitHandle _showSignal = new EventWaitHandle(false, EventResetMode.AutoReset, "Smaller.ShowSignal");

        public void TriggerRunJobs()
        {
            _showSignal.Set();
        }

        private RunJobController()
        {
            StartListenForJobs();
        }

        private static readonly RunJobController _instance = new RunJobController();

        public static RunJobController Instance
        {
            get { return _instance; }
        }

        private void RunJobs()
        {
            Action x = () =>
            {
                //if (Application.Current.MainWindow == null)
                //{
                //    Application.Current.MainWindow = new MainWindow();
                //    Application.Current.MainWindow.Show();
                //}

                new JobRunner().Run();
            };

            Application.Current.Dispatcher.Invoke(x, DispatcherPriority.Send);
        }

        private void StartListenForJobs()
        {
            new Thread(() =>
            {
                while (true)
                {
                    _showSignal.WaitOne();

                    RunJobs();
                }
            }).Start();
        }
    }
}
