using System;
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
using System.Windows.Shapes;
using EveTools.DataModels;
using EveTools.Utils;

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
        }

        public ProjectViewer(bool saveState, Project proj)
        {
            this.proj = proj;
            saved = saveState;
            InitializeComponent();
        }
        private bool saved = true;
        private Project proj;
        private void closeCheck(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!saved)
            {
                MessageBoxResult result =  MessageBox.Show("Do You Want To Save The Project", "Save??", MessageBoxButton.YesNoCancel);
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
    }
}
