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
using EveTools.Utils;
using EveTools.DataModels;

namespace EveTools.Views
{
    /// <summary>
    /// Interaction logic for ProjectCreator.xaml
    /// </summary>
    public partial class ProjectCreator : Window
    {
        public EntryPoint parent;
        public Blueprint bp;
        public ProjectCreator()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FileIO.checkProjectsDir();
            if (projName.Text == "" || projName.Text == null)
            {
                MessageBox.Show("Please Enter a Name", "Seriously??");
                return;
            }
            if (FileIO.CheckExists(projName.Text))
            {
                MessageBox.Show("Project of this name already exists", "Try Again");
                return;
            }
            parent.proj = new Project(bp, projName.Text);
            Close();
        }
    }
}
