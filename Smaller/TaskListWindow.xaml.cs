﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Smaller
{
    /// <summary>
    /// Interaction logic for TaskList.xaml
    /// </summary>
    public partial class TaskListWindow : Window
    {
        private TaskListWindowViewModel Model = null;
        public TaskListWindow()
        {
            InitializeComponent();
        }

        public TaskListWindow(TaskListWindowViewModel vm) : this()
        {
            Model = vm;
            this.DataContext = Model;
        }
    }
}
