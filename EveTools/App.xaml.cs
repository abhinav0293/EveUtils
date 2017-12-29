﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using EveTools.Views;
using EveTools.DAO;
using Newtonsoft.Json;

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
        public static string root = "C:\\ProgramData\\EveTools";
        public static Dictionary<string, double> eff = new Dictionary<string, double>();
        public static InitPage ip;
        public static List<Color> colorList = new List<Color>();
        public static List<Color> innerList = new List<Color>();
        public static int currentColor = -1;
        public static int innerColor = -1;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
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

        public static void initData()
        {
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }
        }

        public static void initEff()
        {
            if (File.Exists(root + "\\eff.json"))
            {
                eff = JsonConvert.DeserializeObject<Dictionary<string, double>>(File.ReadAllText(root + "\\eff.json"));
            }
            else
            {
                eff.Add("spod", 0.0);
                eff.Add("gneiss", 0.0);
                eff.Add("crokite", 0.0);
                eff.Add("ark", 0.0);
                eff.Add("bistot", 0.0);
                eff.Add("ochre", 0.0);
                eff.Add("merc", 0.0);
                EffSet es = new EffSet();
                es.ShowDialog();
            }
        }

        public static void getManuList()
        {
            if (File.Exists(root + "\\manu.json"))
            {
                List<string> li = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(root + "\\manu.json"));
                manuList.AddRange(li.ToArray());
            }
            else
            {
                manuList = Queries.getInstance().getManuList();
                string s = JsonConvert.SerializeObject(manuList);
                File.WriteAllText(root + "\\manu.json", s);
            }
        }

        public static void getInvList()
        {
            if (File.Exists(root + "\\inv.json"))
            {
                List<string> li = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(root + "\\inv.json"));
                invList.AddRange(li.ToArray());
            }
            else
            {
                invList = Queries.getInstance().getInvList();
                string st = JsonConvert.SerializeObject(invList);
                File.WriteAllText(root + "\\inv.json", st);
            }
        }

        public static void saveEff()
        {
            File.WriteAllText("C:\\ProgramData\\EveTools\\eff.json", JsonConvert.SerializeObject(eff));
        }
    }
}
