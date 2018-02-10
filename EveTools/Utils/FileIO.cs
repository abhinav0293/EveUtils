using System.Collections.Generic;
using System.IO;
using EveTools.DAO;
using EveTools.DataModels;
using EveTools.Views;
using Newtonsoft.Json;

namespace EveTools.Utils
{
    public class FileIO
    {
        public static string root = "C:\\ProgramData\\EveTools";

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
                App.eff = JsonConvert.DeserializeObject<Dictionary<string, double>>(File.ReadAllText(root + "\\eff.json"));
            }
            else
            {
                EffSet es = new EffSet();
                es.ShowDialog();
            }
        }

        public static void getManuList(List<string> manuList)
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

        public static void getInvList(List<string> invList)
        {
            if (File.Exists(root + "\\inv.json"))
            {
                List<string> li = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(root + "\\inv.json"));
                invList.AddRange(li.ToArray());
            }
            else
            {
                invList = Queries.getInstance().getInvList();
                HashSet<string> hs = new HashSet<string>();
                foreach (string s in invList)
                {
                    hs.Add(s);
                }
                invList.Clear();
                invList.AddRange(hs);
                string st = JsonConvert.SerializeObject(invList);
                File.WriteAllText(root + "\\inv.json", st);
            }
        }

        public static void saveEff()
        {
            File.WriteAllText(root+"\\eff.json", JsonConvert.SerializeObject(App.eff));
        }

        public static bool CheckExists(string name)
        {
            if (File.Exists(root + "\\Projects\\" + name + ".json"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void checkProjectsDir()
        {
            if (!Directory.Exists(root + "\\Projects"))
            {
                Directory.CreateDirectory(root + "\\Projects");
            }
        }

        public static void saveProject(Project proj)
        {
            File.WriteAllText(root + "\\Projects\\" + proj.name + ".json", JsonConvert.SerializeObject(proj));
        }

        public static Project loadProject(string name)
        {
            return JsonConvert.DeserializeObject<Project>(File.ReadAllText(root + "\\Projects\\" + name + ".json"));
            
        }

        public static List<string> getProjectList()
        {
            checkProjectsDir();
            List<string> projList = new List<string>();
            DirectoryInfo di = new DirectoryInfo(root + "\\Projects");
            foreach(FileInfo fi in di.EnumerateFiles())
            {
                projList.Add(fi.Name.Substring(0,fi.Name.IndexOf(".json")));
            }
            return projList;
        }
    }
}
