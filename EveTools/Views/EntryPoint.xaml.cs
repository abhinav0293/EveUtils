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
using EveTools.DataModels;
using EveTools.DAO;

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
            if (jobType.SelectedIndex == 0)
            {
                manufacturingJob();
            }
            else
            {
                inventionJob();
            }
        }

        #region job_type
        private void manufacturingJob()
        {
            double sme = 0;
            switch (rigs.SelectedIndex)
            {
                case 0:
                    sme = 0;
                    break;
                case 1:
                    sme = 0.01;
                    break;
                case 2:
                    sme = 0.02;
                    break;
            }
            double bm = Convert.ToDouble(bme.SelectedValue) / 100;
            string s = cme.SelectedValue.ToString();
            double cm;
            if (s.Equals("Same"))
            {
                cm = bm;
            }
            else
            {
                cm = Convert.ToDouble(cme.SelectedValue) / 100;
            }
            Blueprint bp = new Blueprint(Actb.SelectedItem.ToString(), 1, Convert.ToInt32(runs.Text), sme, bm, cm);
            bp.getSkills(Actb.SelectedItem.ToString(), (jobType.SelectedIndex == 0) ? 1 : 8);
            ManufactureDisplayModel bpd = new ManufactureDisplayModel(bp, 40, 1);
            stackingPanel.Children.Add(bpd.overview);
            
        }

        private void inventionJob()
        {
            InventionDisplayModel idm = new InventionDisplayModel(new Invention(Queries.getInstance().getItemId(Actb.SelectedItem.ToString()),Convert.ToInt32(runs.Text)));
            stackingPanel.Children.Add(idm.main);
            stackingPanel.Children.Add(idm.other);
            stackingPanel.Children.Add(idm.skills);
        }
        #endregion
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
