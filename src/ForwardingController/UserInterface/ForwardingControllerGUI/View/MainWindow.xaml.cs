﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ForwardingControllerGUI.ViewModel;
using Ninject;

namespace ForwardingControllerGUI.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel VM { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SpTopMenu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 2)
                WindowState = (WindowState)(WindowState == 0 ? 2 : 0);

            DragMove();
        }
    }
}