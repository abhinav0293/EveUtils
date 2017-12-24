﻿using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Shapes;
using EveTools.DataModels;

namespace EveTools.Views
{
    /// <summary>
    /// Interaction logic for EntryPoint.xaml
    /// </summary>
    public partial class EntryPoint : Window
    {
        public volatile bool set = false;
        public EntryPoint()
        {
            InitPage ip = new InitPage();
            App.ip = ip;
            ip.parent = this;
            ip.ShowDialog();
            while (!set)
            {

            }
            InitializeComponent();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (jobType.SelectedIndex == 0)
                App.auto = App.manuList;
            else
                App.auto = App.invList;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.saveEff();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            stackingPanel.Children.Clear();
            Blueprint bp = new Blueprint(Actb.SelectedItem.ToString(),(jobType.SelectedIndex==0)?1:8,1);
            BPDisplayModel bpd = new BPDisplayModel(bp,40,1);
            List<Expander> exp = bpd.getViews();
            foreach(Expander ex in exp)
            {
                //ex.IsExpanded = true;
                stackingPanel.Children.Add(ex);
            }
        }

        #region menu_iems
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            EffSet es = new EffSet();
            es.ShowDialog();
        }
        #endregion
    }
}