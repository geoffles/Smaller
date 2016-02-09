using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using Smaller.Tasks;

namespace Smaller
{
    /// <summary>
    /// Provides bindable properties and commands for the NotifyIcon. In this sample, the
    /// view model is assigned to the NotifyIcon in XAML. Alternatively, the startup routing
    /// in App.xaml.cs could have created this view model, and assigned it to the NotifyIcon.
    /// </summary>
    public class NotifyIconViewModel
    {
        public ICommand ShowAbout
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecuteFunc = () => Application.Current.MainWindow == null,
                    CommandAction = () =>
                    {
                        Application.Current.MainWindow = new MainWindow();
                        Application.Current.MainWindow.Show();
                    }
                };
            }
        }

        /// <summary>
        /// Shows a window, if none is already open.
        /// </summary>
        public ICommand SmallifyCommand
        {
            get
            {
                return new SmallifyCommand();
            }
        }

        public ICommand TestTaskOutCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CommandAction = () =>
                    {
                        SampleTask();
                        SampleHistory();
                    }
                };
            }
        }

        public ICommand ViewTasksCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CommandAction = () =>
                    {
                        var window =
                            //Application.Current.MainWindow =
                            new TaskListWindow(TaskListWindowViewModel.From(JobRunner.GetAllTasks(), JobRunner.GetAllHistory()));
                        //Application.Current.MainWindow.Show();
                        window.Show();
                    }
                };
            }
        }


        private void SampleTask()
        {
            var tasks = new SmallerTaskList
                        {
                            Parameters = new List<Parameter>
                            {
                                new Parameter{Key = "ADMINISTRATOR", Value = "Andi"}
                            },
                            Tasks = new System.Collections.Generic.List<SmallerTaskBase>
                            {
                                new MailTask
                                {
                                    ScheduledDate = new DateTime(2016, 02, 10),
                                    Body = "Foo\r\nBar",
                                    Subject = "Bar",
                                    To = "aaa@bbb.com",
                                    Identifier = Guid.NewGuid().ToString()
                                }
                            }
                        };
                        var s = new System.Xml.Serialization.XmlSerializer(typeof (SmallerTaskList), new [] {typeof(SmallerTaskBase), typeof(MailTask)});
                        var m = new MemoryStream();
                        s.Serialize(m, tasks);

                        System.IO.File.WriteAllBytes("SampleTasks.xml", m.ToArray());
        }

        private void SampleHistory()
        {
            var tasks = new RunHistoryList()
            {
                new RunHistory
                {
                    Identifier = "Foo",
                    Result = "Success",
                    RunDate = DateTime.Now
                },
                new RunHistory
                {
                    Identifier = "Foo",
                    Result = "Success",
                    RunDate = DateTime.Now
                }

            };
            var s = new System.Xml.Serialization.XmlSerializer(typeof(RunHistoryList), new[] { typeof(RunHistory) });
            var m = new MemoryStream();
            s.Serialize(m, tasks);

            System.IO.File.WriteAllBytes("SampleHistory.xml", m.ToArray());
        }

        /// <summary>
        /// Shuts down the application.
        /// </summary>
        public ICommand ExitApplicationCommand
        {
            get
            {
                return new DelegateCommand {CommandAction = () => Application.Current.Shutdown()};
            }
        }
    }
}
