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

namespace EveTools.Views
{
    /// <summary>
    /// Interaction logic for ProjectSelector.xaml
    /// </summary>
    public partial class ProjectSelector : Window
    {
        public EntryPoint parent;
        public bool ok = true;
        public ProjectSelector()
        {
            InitializeComponent();
            List<string> projList = FileIO.getProjectList();
            if (projList.Count == 0)
            {
                MessageBox.Show("You Don't Have Any Saved Projects", "Putting The Cart Before The Horse?");
                ok = false;
            }
            foreach(string s in projList)
            {
                projectList.Items.Add(s);
            }
        }

        private void selected(object sender, RoutedEventArgs e)
        {
            string s = projectList.SelectedValue.ToString();
            parent.proj = FileIO.loadProject(s);
            Close();
        }
    }
}
