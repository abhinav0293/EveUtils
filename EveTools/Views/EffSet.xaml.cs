using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace EveTools.Views
{
    /// <summary>
    /// Interaction logic for EffSet.xaml
    /// </summary>
    public partial class EffSet : Window
    {
        public EffSet()
        {
            InitializeComponent();
            if (App.eff != null)
            {
                spodeff.Text = Convert.ToString(App.eff["spod"]);
                gneisseff.Text = Convert.ToString(App.eff["gneiss"]);
                crokiteEff.Text = Convert.ToString(App.eff["crokite"]);
                bistotEff.Text = Convert.ToString(App.eff["bistot"]);
                arkonorEff.Text = Convert.ToString(App.eff["ark"]);
                ochreEff.Text = Convert.ToString(App.eff["ochre"]);
                merEff.Text = Convert.ToString(App.eff["merc"]);
            }
            else
            {
                spodeff.Text = "0";
                gneisseff.Text = "0";
                crokiteEff.Text = "0";
                bistotEff.Text = "0";
                arkonorEff.Text = "0";
                ochreEff.Text = "0";
                merEff.Text = "0";
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            double spod, gneiss, ochre, bistot, ark, crokite, merc;
            try
            {
                spod = Convert.ToDouble(spodeff.Text);
                if (spod >= 1 || spod <= 0)
                {
                    throw new Exception();
                }
            }
            catch
            {
                MessageBox.Show("Only Numeric Decimal Values Between 0 and 1 Allowed");
                spodeff.Focus();
                return;
            }

            try
            {
                gneiss = Convert.ToDouble(gneisseff.Text);
                if (gneiss >= 1 || gneiss <= 0)
                {
                    throw new Exception();
                }
            }
            catch
            {
                MessageBox.Show("Only Numeric Decimal Values Between 0 and 1 Allowed");
                gneisseff.Focus();
                return;
            }

            try
            {
                crokite = Convert.ToDouble(crokiteEff.Text);
                if (crokite >= 1 || crokite <= 0)
                {
                    throw new Exception();
                }
            }
            catch
            {
                MessageBox.Show("Only Numeric Decimal Values Between 0 and 1 Allowed");
                crokiteEff.Focus();
                return;
            }

            try
            {
                bistot = Convert.ToDouble(bistotEff.Text);
                if (bistot >= 1 || bistot <= 0)
                {
                    throw new Exception();
                }
            }
            catch
            {
                MessageBox.Show("Only Numeric Decimal Values Between 0 and 1 Allowed");
                bistotEff.Focus();
                return;
            }

            try
            {
                ark = Convert.ToDouble(arkonorEff.Text);
                if (ark >= 1 || ark <=0)
                {
                    throw new Exception();
                }
            }
            catch
            {
                MessageBox.Show("Only Numeric Decimal Values Between 0 and 1 Allowed");
                arkonorEff.Focus();
                return;
            }

            try
            {
                merc = Convert.ToDouble(merEff.Text);
                if (merc >= 1 || merc <= 0)
                {
                    throw new Exception();
                }
            }
            catch
            {
                MessageBox.Show("Only Numeric Decimal Values Between 0 and 1 Allowed");
                merEff.Focus();
                return;
            }

            try
            {
                ochre = Convert.ToDouble(ochreEff.Text);
                if (ochre >= 1)
                {
                    throw new Exception();
                }
            }
            catch
            {
                MessageBox.Show("Only Numeric Decimal Values Between 0 and 1 Allowed");
                ochreEff.Focus();
                return;
            }
            App.eff = new Dictionary<string, double>();
            App.eff.Clear();
            App.eff.Add("spod", spod);
            App.eff.Add("gneiss", gneiss);
            App.eff.Add("crokite", crokite);
            App.eff.Add("ark", ark);
            App.eff.Add("bistot", bistot);
            App.eff.Add("ochre", ochre);
            App.eff.Add("merc", merc);
            Close();
        }

        private void spodeff_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.SelectAll();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (App.eff == null)
            {
                MessageBox.Show("Please Set All Repoc Effeciencies Before Closing");
                e.Cancel = true;
            }
        }
    }
}
