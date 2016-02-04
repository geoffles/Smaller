using System;
using System.Windows;
using System.Windows.Input;

namespace Smaller
{
    /// <summary>
    /// Simplistic delegate command for the demo.
    /// </summary>
    public class SmallifyCommand : ICommand
    {
        //public Action<string> CommandAction { get; set; }
        public Func<bool> CanExecuteFunc { get; set; }

        public void Execute(object parameter)
        {
            RunJobController.Instance.TriggerRunJobs();
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteFunc == null;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}