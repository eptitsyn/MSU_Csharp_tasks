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

namespace task19
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MediaElement_OnMediaEnded(object sender, RoutedEventArgs e)
        {
            //MainViewModel.
        }

        private void PlaylistList_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((MainViewModel)DataContext).PlayPauseCommand.Execute(this);
        }
    }
}