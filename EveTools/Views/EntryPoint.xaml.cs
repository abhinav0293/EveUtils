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
using EveTools.DataModels;
using EveTools.DAO;

namespace EveTools.Views
{
    /// <summary>
    /// Interaction logic for EntryPoint.xaml
    /// </summary>
    public partial class EntryPoint : Window
    {

        #region init_close
        private Blueprint bp;
        InventionDisplayModel idm;

        public EntryPoint()
        {
            App.initEff();
            InitializeComponent();
            outputReset();
            inputReset();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.saveEff();
        }
        #endregion

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
                    sme = 0.02;
                    break;
                case 2:
                    sme = 0.042;
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
            bp = new Blueprint(Actb.SelectedItem.ToString(), 1, Convert.ToInt32(runs.Text), sme, bm, cm);
            bp.getSkills();
            ManufactureDisplayModel bpd = new ManufactureDisplayModel(bp, 20, 1, false);
            List<Expander> list = bpd.getViews();
            foreach(Expander e in list)
            {
                stackingPanel.Children.Add(e);
            }
            
        }

        private void inventionJob()
        {
            idm = new InventionDisplayModel(new Invention(Queries.getInstance().getItemId(Actb.SelectedItem.ToString()),Convert.ToInt32(runs.Text)));
            stackingPanel.Children.Add(idm.main);
            stackingPanel.Children.Add(idm.other);
            stackingPanel.Children.Add(idm.skills);
        }
        #endregion

        #region menu_iems
        private void clear_click(object sender, RoutedEventArgs e)
        {
            outputReset();
            inputReset();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            EffSet es = new EffSet();
            es.ShowDialog();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            OreCalc ore = new OreCalc
            {
                parent = this
            };
            Visibility = Visibility.Hidden;
            ore.Show();
        }
        #endregion

        #region reset
        private void outputReset()
        {
            stackingPanel.Children.Clear();
            itemDesc.Visibility = Visibility.Hidden;
            copyButton.Visibility = Visibility.Hidden;
            projectButton.Visibility = Visibility.Hidden;
        }

        private void inputReset()
        {
            jobType.SelectedIndex = 0;
            Actb.SelectedItem = "";
            runs.Text = "1";
            bme.SelectedIndex = 0;
            cme.SelectedIndex = 0;
            rigs.SelectedIndex = 0;
        }
        #endregion

        #region buttons
        private void itemDesc_Click(object sender, RoutedEventArgs e)
        {
            if (bp != null)
            {
                MessageBox.Show(bp.desc, Actb.SelectedItem.ToString());
            }
            else
            {
                MessageBox.Show(Queries.getInstance().getItemDesc(idm.i.product), idm.i.product);
            }
        }

        private void copy_click(object sender, RoutedEventArgs e)
        {
            string st = "";
            if (stackingPanel.Children.Count > 0)
            {
                if (bp != null)
                {
                    st = createClipboardDataManu();
                }
                else
                {
                    st = createClipboardDataInv();
                }
            }
            Clipboard.SetText(st);
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            outputReset();
            bp = null;
            idm = null;
            App.innerColor = -1;
            App.currentColor = -1;
            if (Actb.SelectedItem == null)
            {
                MessageBox.Show("Type in the Search Box and Select an Item form the Drop Down before pressing the Search Button\n\n\n\n(Going by the fact that this had to be explained to you i'm guessing you're the type that buys Toilet Paper with Instructions to use it Printed on it)", "Error");
                return;
            }
            if (jobType.SelectedIndex == 0)
            {
                manufacturingJob();
                projectButton.Visibility = Visibility.Visible;
            }
            else
            {
                inventionJob();
            }
            copyButton.Visibility = Visibility.Visible;
            itemDesc.Visibility = Visibility.Visible;
        }
        #endregion

        #region misc_tools
        private string createClipboardDataInv()
        {
            Invention i = idm.i;

            string st = i.in_bp.Count == 1 ? "Blueprint Required" : "Relic Required(Any One)" + Environment.NewLine;
            foreach(string s in i.in_bp)
            {
                st += s + Environment.NewLine;
            }

            st += Environment.NewLine + "Other:" + Environment.NewLine;
            foreach (Other o in i.reqs)
            {
                st += o.name + ":\t\t\t" + o.count + Environment.NewLine;
            }

            return st;
        }

        private string createClipboardDataManu()
        {
            string st = "Overview:" + Environment.NewLine;
            foreach(Component c in bp.components)
            {
                st += c.compName + "\t\t\t" + c.compCount + Environment.NewLine;
            }

            if (bp.hasMineral)
            {
                st += Environment.NewLine + "Minerals:" + Environment.NewLine;
                st += "Tritanium:\t\t\t" + bp.mineral.trit + Environment.NewLine;
                st += "Pyerite:\t\t\t" + bp.mineral.pyerite + Environment.NewLine;
                st += "Mexallon:\t\t\t" + bp.mineral.mexallon + Environment.NewLine;
                st += "Isogen:\t\t\t\t" + bp.mineral.isogen + Environment.NewLine;
                st += "Nocxium:\t\t\t" + bp.mineral.nocxium + Environment.NewLine;
                st += "Zydrine:\t\t\t" + bp.mineral.zydrine + Environment.NewLine;
                st += "Megacyte:\t\t\t" + bp.mineral.megacyte + Environment.NewLine;
                st += "Morphite:\t\t\t" + bp.mineral.morphite + Environment.NewLine;
                st += Environment.NewLine + "Ores:" + Environment.NewLine;
                st += "Spodumain:\t\t\t" + bp.ores.spod + Environment.NewLine;
                st += "Gneiss:\t\t\t\t" + bp.ores.gneiss + Environment.NewLine;
                st += "Crokite:\t\t\t" + bp.ores.crokite + Environment.NewLine;
                st += "Ochre:\t\t\t\t" + bp.ores.ochre + Environment.NewLine;
                st += "Bistot:\t\t\t\t" + bp.ores.bistot + Environment.NewLine;
                st += "Arkonor:\t\t\t" + bp.ores.ark + Environment.NewLine;
                st += "Mercoxit:\t\t\t" + bp.ores.mercox + Environment.NewLine;
            }

            if (bp.hasPI)
            {
                st += Environment.NewLine + "PI:" + Environment.NewLine;
                foreach(string s in bp.piList.Keys)
                {
                    PI pi = bp.piList[s];
                    st += pi.name + ":\t\t\t" + pi.count + Environment.NewLine;
                }
            }

            if (bp.hasMG)
            {
                st += Environment.NewLine + "Moon Goo:" + Environment.NewLine;
                foreach (string s in bp.mg.Keys)
                {
                    MoonGoo pi = bp.mg[s];
                    st += pi.name + ":\t\t\t" + pi.count + Environment.NewLine;
                }
            }

            if (bp.hasOther)
            {
                st += Environment.NewLine + "Other:" + Environment.NewLine;
                foreach (string s in bp.otherComps.Keys)
                {
                    Other pi = bp.otherComps[s];
                    st += pi.name + ":\t\t\t" + pi.count + Environment.NewLine;
                }
            }
            return st;
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (jobType.SelectedIndex == 0)
                App.auto = App.manuList;
            else
                App.auto = App.invList;
        }
        #endregion

    }
}
