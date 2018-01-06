using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using EveTools.DataModels;
using EveTools.Tools;

namespace EveTools.Views
{
    /// <summary>
    /// Interaction logic for OreCalc.xaml
    /// </summary>
    public partial class OreCalc : Window
    {
        public Window parent = null;

        public OreCalc()
        {
            InitializeComponent();
            trit.Focus();
        }

        #region menu_items
        private void clear_click(object sender, RoutedEventArgs e)
        {
            trit.Text = "0";
            pyerite.Text = "0";
            mex.Text = "0";
            nocx.Text = "0";
            zyd.Text = "0";
            iso.Text = "0";
            mega.Text = "0";
            morp.Text = "0";
            spod.Text = "0";
            gneiss.Text = "0";
            ochre.Text = "0";
            crokite.Text = "0";
            bistot.Text = "0";
            arkonor.Text = "0";
            mercoxit.Text = "0";
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion

        #region textbox_event_handlers
        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            long l = 0;
            try
            {
                l = Convert.ToInt64(tb.Text);
            }
            catch
            {
                return;
            }
            tb.Text = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", l);
        }
        #endregion

        private void OreCalc_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            parent.Visibility = Visibility.Visible;
        }

        #region button_events
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EffSet effe = new EffSet();
            effe.ShowDialog();
        }

        private void calcOre(object sender, RoutedEventArgs e)
        {
            Mineral min = new Mineral();
            try
            {
                min.trit = Convert.ToInt64(trit.Text.Replace(",",""));
                if (min.trit < 0)
                {
                    throw new Exception();
                }
            }
            catch
            {
                showError(trit);
                return;
            }

            try
            {
                min.pyerite = Convert.ToInt64(pyerite.Text.Replace(",",""));
                if (min.pyerite < 0)
                {
                    throw new Exception();
                }
            }
            catch
            {
                showError(pyerite);
                return;
            }

            try
            {
                min.mexallon = Convert.ToInt64(mex.Text.Replace(",",""));
                if (min.mexallon < 0)
                {
                    throw new Exception();
                }
            }
            catch
            {
                showError(mex);
                return;
            }

            try
            {
                min.isogen = Convert.ToInt64(iso.Text.Replace(",",""));
                if (min.isogen < 0)
                {
                    throw new Exception();
                }
            }
            catch
            {
                showError(iso);
                return;
            }

            try
            {
                min.nocxium = Convert.ToInt64(nocx.Text.Replace(",",""));
                if (min.nocxium < 0)
                {
                    throw new Exception();
                }
            }
            catch
            {
                showError(nocx);
                return;
            }

            try
            {
                min.zydrine = Convert.ToInt64(zyd.Text.Replace(",",""));
                if (min.zydrine < 0)
                {
                    throw new Exception();
                }
            }
            catch
            {
                showError(zyd);
                return;
            }

            try
            {
                min.megacyte = Convert.ToInt64(mega.Text.Replace(",",""));
                if (min.megacyte < 0)
                {
                    throw new Exception();
                }
            }
            catch
            {
                showError(mega);
                return;
            }

            try
            {
                min.morphite = Convert.ToInt64(morp.Text.Replace(",",""));
                if (min.morphite < 0)
                {
                    throw new Exception();
                }
            }
            catch
            {
                showError(morp);
                return;
            }

            Ores o = new OreCalculator(min).calcOres();
            spod.Text = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", o.spod);
            gneiss.Text = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", o.gneiss);
            ochre.Text = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", o.ochre);
            crokite.Text = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", o.crokite);
            bistot.Text = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", o.bistot);
            arkonor.Text = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", o.ark);
            mercoxit.Text = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", o.mercox);
        }

        private void showError(TextBox tb)
        {
            MessageBox.Show("Mineral Value Must be a Non-Negative Whole Number","Error");
            tb.Focus();
        }
        #endregion
    }
}
