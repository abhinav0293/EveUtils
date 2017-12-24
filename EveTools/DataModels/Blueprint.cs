using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveTools.DAO;
using EveTools.Tools;

namespace EveTools.DataModels
{
    public class Blueprint
    {
        public List<Component> components = new List<Component>();
        public List<Skill> skills = new List<Skill>();

        public Dictionary<string, PI> piList = null;
        public Dictionary<string, MoonGoo> mg = null;
        public Dictionary<string, Other> otherComps = null;

        public Ores ores = null;
        public Mineral mineral = null;

        public bool hasMineral = false;
        public bool hasMG = false;
        public bool hasPI = false;
        public bool hasOther = false;
        public long count = 1;

        public Blueprint(string name, int activity, long count)
        {
            this.count = count;
            components = Queries.getInstance().getCompList(name,activity);
            skills = Queries.getInstance().getBPSkills(name, activity);
            identifyComponents(activity);
            if (hasMineral)
            {
                ores = new OreCalculator(mineral).calcOres();
            }
        }

        private void identifyComponents(int activity)
        {
            foreach(Component comp in components)
            {
                if (comp.hasBlueprint && activity==1)
                {
                    getCompReq(comp);
                    continue;
                }
                else
                {
                    switch (Queries.getInstance().checkType(comp.compId))
                    {
                        case "Mineral":
                            addMineral(comp,count);
                            break;
                        case "PI":
                            addPI(comp, count);
                            break;
                        case "Moon":
                            addMoonGoo(comp,count);
                            break;
                        case "Other":
                            addOther(comp,count);
                            break;
                    }
                }
            }
        }

        private void getCompReq(Component comp)
        {
            Blueprint bp = new Blueprint(comp.compName, 1, count*comp.compCount);
            comp.bp = bp;
            if (bp.hasPI)
            {
                foreach(string p in bp.piList.Keys)
                {
                    PI pi = bp.piList[p];
                    Component c = new Component(pi.id, pi.name, pi.count, false);
                    addPI(c, comp.compCount);
                }
            }

            if (bp.hasMG)
            {
                foreach(string s in bp.mg.Keys)
                {
                    MoonGoo m = bp.mg[s];
                    Component c = new Component(m.id, m.name, m.count, false);
                    addMoonGoo(c, comp.compCount);
                }
            }

            if (bp.hasOther)
            {
                foreach(string s in bp.otherComps.Keys)
                {
                    Other o = bp.otherComps[s];
                    Component c = new Component(o.id, o.name, o.count, false);
                    addOther(c, comp.compCount);
                }
            }

            if (bp.hasMineral)
            {
                Mineral m = bp.mineral;
                if (!hasMineral)
                {
                    hasMineral = true;
                    mineral = new Mineral();
                }
                mineral.trit += m.trit;
                mineral.pyerite += m.pyerite;
                mineral.nocxium += m.nocxium;
                mineral.isogen += m.isogen;
                mineral.megacyte += m.megacyte;
                mineral.mexallon += m.mexallon;
                mineral.zydrine += m.zydrine;
                mineral.morphite += m.morphite;
            }
        }

        private void addMineral(Component comp, long multiplier)
        {
            if (!hasMineral)
            {
                hasMineral = true;
                mineral = new Mineral();
            }
            switch (comp.compName)
            {
                case "Tritanium":
                    mineral.trit += comp.compCount * multiplier;
                    break;
                case "Pyerite":
                    mineral.pyerite += comp.compCount * multiplier;
                    break;
                case "Mexallon":
                    mineral.mexallon += comp.compCount * multiplier;
                    break;
                case "Zydrine":
                    mineral.zydrine += comp.compCount * multiplier;
                    break;
                case "Megacyte":
                    mineral.megacyte += comp.compCount * multiplier;
                    break;
                case "Isogen":
                    mineral.isogen += comp.compCount * multiplier;
                    break;
                case "Nocxium":
                    mineral.nocxium += comp.compCount * multiplier;
                    break;
                case "Morphite":
                    mineral.morphite += comp.compCount * multiplier;
                    break;
            }
        }

        private void addPI(Component c, long multiplier)
        {
            if (!hasPI)
            {
                hasPI = true;
                piList = new Dictionary<string, PI>();
            }
            try
            {
                PI p = piList[c.compName];
                p.count += c.compCount * multiplier;
            }
            catch (KeyNotFoundException)
            {
                piList.Add(c.compName, new PI(c.compName, c.compId, c.compCount * multiplier));
            }
        }

        private void addMoonGoo(Component c, long multiplier)
        {
            if (!hasMG)
            {
                hasMG = true;
                mg = new Dictionary<string, MoonGoo>();
            }
            try
            {
                MoonGoo m = mg[c.compName];
                m.count += c.compCount * multiplier;
            }
            catch (KeyNotFoundException)
            {
                mg.Add(c.compName, new MoonGoo(c.compName, c.compId, c.compCount * multiplier));
            }
        }

        private void addOther(Component c, long multiplier)
        {
            if(!hasOther)
            {
                hasOther = true;
                otherComps = new Dictionary<string, Other>();
            }
            try
            {
                Other o = otherComps[c.compName];
                o.count += c.compCount * multiplier;
            }
            catch (KeyNotFoundException)
            {
                otherComps.Add(c.compName, new Other(c.compName, c.compId, c.compCount * multiplier));
            }
        }
    }
}
