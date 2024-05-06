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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuanLiNhaSach.View.Staff
{
    /// <summary>
    /// Interaction logic for MainStaffWindow.xaml
    /// </summary>
    public partial class MainStaffWindow : Window
    {
        public MainStaffWindow()
        {
            InitializeComponent();
        }

        private void Overlay_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BeginStoryboard((Storyboard)Resources["MenuClose"]);
        }

        private void AdminWD_Closed(object sender, System.EventArgs e)
        {
            this.Owner.Visibility = Visibility.Visible;
        }

       
    }
}
