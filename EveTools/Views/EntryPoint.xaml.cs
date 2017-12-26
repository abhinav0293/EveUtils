using System;
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
            Blueprint bp = new Blueprint(Actb.SelectedItem.ToString(), (jobType.SelectedIndex==0)?1:8,Convert.ToInt32(runs.Text), bm, cm);
            bp.getSkills(Actb.SelectedItem.ToString(), (jobType.SelectedIndex == 0) ? 1 : 8);
            BPDisplayModel bpd = new BPDisplayModel(bp,40,1);
            List<Expander> exp = bpd.getViews();
            foreach(Expander exi in exp)
            {
                stackingPanel.Children.Add(exi);
            }
            Expander ex = bpd.getSkills();
            ex.Background = new SolidColorBrush(Colors.SlateBlue);
            ex.Content = bpd.createSkills(bp.skills, 1);
            stackingPanel.Children.Add(ex);
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
