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
using EveTools.Utils;
using EveTools.Tools;

namespace EveTools.Views
{
    /// <summary>
    /// Interaction logic for ProjectViewer.xaml
    /// </summary>
    public partial class ProjectViewer : Window
    {
        public EntryPoint parent;
        public ProjectViewer(string name)
        {
            proj = FileIO.loadProject(name);
            InitializeComponent();
            update();
        }

        private void update()
        {
            updateMinerals();
            updateOres();
            if (saved)
            {
                saveMenu.IsEnabled = false;
            }
            else
            {
                saveMenu.IsEnabled = true;
            }

            if (proj.rollback==null || proj.rollback.Count == 0)
            {
                undoMenu.IsEnabled = false;
            }
            else
            {
                undoMenu.IsEnabled = true;
            }

            if (proj.redo==null || proj.redo.Count == 0)
            {
                redoMenu.IsEnabled = false;
            }
            else
            {
                redoMenu.IsEnabled = true;
            }
        }

        private void updateOres()
        {
            initSpod.Content = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", proj.original.ores.spod);
            currentSpod.Content = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", proj.current.ores.spod);

            initGneiss.Content = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", proj.original.ores.gneiss);
            currentGneiss.Content = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", proj.current.ores.gneiss);

            initOchre.Content = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", proj.original.ores.ochre);
            currentOchre.Content = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", proj.current.ores.ochre);

            initCrokite.Content = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", proj.original.ores.crokite);
            currentCrokite.Content = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", proj.current.ores.crokite);

            initBistot.Content = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", proj.original.ores.bistot);
            currentBistot.Content = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", proj.current.ores.bistot);

            initArk.Content = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", proj.original.ores.ark);
            currentArk.Content = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", proj.current.ores.ark);
        }

        private void updateMinerals()
        {
            initTrit.Content = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", proj.original.mineral.trit);
            currentTrit.Content = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", proj.current.mineral.trit);

            initPy.Content = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", proj.original.mineral.pyerite);
            currentPy.Content = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", proj.current.mineral.pyerite);

            initMex.Content = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", proj.original.mineral.mexallon);
            currentMex.Content = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", proj.current.mineral.mexallon);

            initIso.Content = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", proj.original.mineral.isogen);
            currentIso.Content = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", proj.current.mineral.isogen);

            initNoc.Content = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", proj.original.mineral.nocxium);
            currentNoc.Content = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", proj.current.mineral.nocxium);

            initZyd.Content = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", proj.original.mineral.zydrine);
            currentZyd.Content = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", proj.current.mineral.zydrine);

            initMeg.Content = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", proj.original.mineral.megacyte);
            currentMeg.Content = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", proj.current.mineral.megacyte);
        }

        public ProjectViewer(bool saveState, Project proj)
        {
            this.proj = proj;
            saved = false;
            InitializeComponent();
            update();
        }
        private bool saved = true;
        private Project proj;
        private void closeCheck(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!saved)
            {
                MessageBoxResult result =  MessageBox.Show("Do You Want To Save The Project(You Won't Be Able To Rollback Changes)", "Save??", MessageBoxButton.YesNoCancel);
                if(result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }

                if(result == MessageBoxResult.Yes)
                {
                    FileIO.saveProject(proj);
                }
            }
            parent.Visibility = Visibility.Visible;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do You Want To Save The Project(You Won't Be Able To Rollback Changes)", "Save??", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Cancel)
            {
                return;
            }

            if (result == MessageBoxResult.Yes)
            {
                FileIO.saveProject(proj);
            }
            saved = true;
            proj.rollback = null;
            proj.redo = null;
            update();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            Blueprint bpc = proj.getLastChange();
            if (bpc != null)
            {
                proj.current = bpc;
                update();
                saved = false;
            }
        }

        private void Redo_Click(object sender, RoutedEventArgs e)
        {
            Blueprint bpc = proj.getLastRollback();
            if (bpc != null)
            {
                proj.current = bpc;
                update();
                saved = false;
            };
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            proj.addToRollbackQueue();
            saved = false;
            proj.reset();
            update();
        }

        private void updateMineral(object sender, RoutedEventArgs e)
        {
            proj.addToRollbackQueue();
            try
            {
                long trit = Convert.ToInt64(updateTrit.Text.Replace(",", ""));
                proj.current.mineral.trit -= trit;

                long py = Convert.ToInt64(updatePy.Text.Replace(",", ""));
                proj.current.mineral.pyerite -= py;

                long mex = Convert.ToInt64(updateMex.Text.Replace(",", ""));
                proj.current.mineral.mexallon -= mex;

                long iso = Convert.ToInt64(updateIso.Text.Replace(",", ""));
                proj.current.mineral.isogen -= iso;

                long noc = Convert.ToInt64(updateNoc.Text.Replace(",", ""));
                proj.current.mineral.nocxium -= noc;

                long zyd = Convert.ToInt64(updateZyd.Text.Replace(",", ""));
                proj.current.mineral.zydrine -= zyd;

                long meg = Convert.ToInt64(updateMeg.Text.Replace(",", ""));
                proj.current.mineral.megacyte -= meg;

                proj.current.ores = new OreCalculator(proj.current.mineral).calcOres();
                update();
                updateTrit.Text = "0";
                updatePy.Text = "0";
                updateMex.Text = "0";
                updateIso.Text = "0";
                updateNoc.Text = "0";
                updateZyd.Text = "0";
                updateMeg.Text = "0";
                saved = false;
            }
            catch
            {
                MessageBox.Show("Check Your Input Values", "Error");
                proj.current = proj.discardChange();
            }
        }

        private void textSet(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", tb.Text);
        }

        private void updateOre(object sender, RoutedEventArgs e)
        {
            proj.addToRollbackQueue();
            Ores ores = null;
            try
            {
                ores = new Ores()
                {
                    spod = Convert.ToInt64(updateSpod.Text.Replace(",", "")),
                    gneiss = Convert.ToInt64(updateGneiss.Text.Replace(",", "")),
                    crokite = Convert.ToInt64(updateCrokite.Text.Replace(",", "")),
                    ochre = Convert.ToInt64(updateOchre.Text.Replace(",", "")),
                    bistot = Convert.ToInt64(updateBistot.Text.Replace(",", "")),
                    ark = Convert.ToInt64(updateArk.Text.Replace(",", ""))
                };
            }
            catch
            {
                MessageBox.Show("Check Your Input Values", "Error");
                proj.current = proj.discardChange();
                return;
            }

            Mineral mins = new MineralCalculator(ores).calcMinerals();
            proj.current.mineral.trit -= mins.trit;
            proj.current.mineral.pyerite -= mins.pyerite;
            proj.current.mineral.mexallon -= mins.mexallon;
            proj.current.mineral.isogen -= mins.isogen;
            proj.current.mineral.nocxium -= mins.nocxium;
            proj.current.mineral.zydrine -= mins.zydrine;
            proj.current.mineral.megacyte -= mins.megacyte;
            proj.current.ores = new OreCalculator(proj.current.mineral).calcOres();
            update();
            updateSpod.Text = "0";
            updateGneiss.Text = "0";
            updateOchre.Text = "0";
            updateCrokite.Text = "0";
            updateBistot.Text = "0";
            updateArk.Text = "0";
            saved = false;
        }
    }
}
