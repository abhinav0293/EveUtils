using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using EveTools.Views;
using EveTools.Utils;

namespace EveTools
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static List<string> auto = new List<string>();
        public static List<string> manuList = new List<string>();
        public static List<string> invList = new List<string>();
        
        public static Dictionary<string, double> eff;
        public static List<Color> colorList = new List<Color>();
        public static List<Color> innerList = new List<Color>();
        public static int currentColor = -1;
        public static int innerColor = -1;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            FileIO.initData();
            FileIO.getManuList(manuList);
            FileIO.getInvList(invList);
            colorList.Add(Colors.DodgerBlue);
            colorList.Add(Colors.CornflowerBlue);
            colorList.Add(Colors.SkyBlue);
            colorList.Add(Colors.SlateBlue);
            colorList.Add(Colors.Aqua);
            colorList.Add(Colors.SteelBlue);
            colorList.Add(Colors.LightBlue);
            colorList.Add(Colors.CadetBlue);

            innerList.Add(Colors.LimeGreen);
            innerList.Add(Colors.LawnGreen);
            innerList.Add(Colors.SeaGreen);
            innerList.Add(Colors.LightGreen);
            innerList.Add(Colors.PaleGreen);
            innerList.Add(Colors.SpringGreen);
            innerList.Add(Colors.YellowGreen);
            innerList.Add(Colors.Olive);
            // Create the startup window
            EntryPoint wnd = new EntryPoint();
            // Show the window
            wnd.Show();
        }

        public static Color getColor()
        {
            currentColor++;
            currentColor = currentColor % 8;
            return colorList[currentColor];
        }

        public static Color getInnerColor()
        {
            innerColor++;
            innerColor = innerColor % 8;
            return innerList[innerColor];
        }

        
    }
}