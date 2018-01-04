using System;
using System.Collections.Generic;
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
        public double bme = 0.0;
        public double cme = 0.0;
        public double sme = 0.0;
        public string desc = "";
        public string name = "";

        public Blueprint(string name, int activity, long count, double sme, double bme, double cme)
        {
            this.bme = 1.0 - bme;
            this.cme = 1.0 - cme;
            this.sme = 1.0 - sme;
            this.count = count;
            this.name = name;
            components = Queries.getInstance().getCompList(name,activity);
            desc = Queries.getInstance().getItemDesc(name);

            identifyComponents(activity);
            if (hasMineral)
            {
                ores = new OreCalculator(mineral).calcOres();
            }
        }

        public void getSkills()
        {
            skills = Queries.getInstance().getBPSkills(name, 1);
        }

        private void identifyComponents(int activity)
        {
            foreach(Component comp in components)
            {
                comp.compCount = (long)Math.Ceiling(comp.compCount * sme);
                comp.compCount = (long)Math.Ceiling(comp.compCount * bme * count);
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
                            addMineral(comp);
                            break;
                        case "PI":
                            addPI(comp);
                            break;
                        case "Moon":
                            addMoonGoo(comp);
                            break;
                        case "Other":
                            addOther(comp);
                            break;
                    }
                }
            }
        }

        private void getCompReq(Component comp)
        {
            Blueprint bp = new Blueprint(comp.compName, 1, comp.compCount, 1.0-sme, 1.0-cme,1.0-cme);
            comp.bp = bp;
            if (bp.hasPI)
            {
                foreach(string p in bp.piList.Keys)
                {
                    PI pi = bp.piList[p];
                    Component c = new Component(pi.id, pi.name, pi.count, false);
                    addPI(c);
                }
            }

            if (bp.hasMG)
            {
                foreach(string s in bp.mg.Keys)
                {
                    MoonGoo m = bp.mg[s];
                    Component c = new Component(m.id, m.name, m.count, false);
                    addMoonGoo(c);
                }
            }

            if (bp.hasOther)
            {
                foreach(string s in bp.otherComps.Keys)
                {
                    Other o = bp.otherComps[s];
                    Component c = new Component(o.id, o.name, o.count, false);
                    addOther(c);
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

        private void addMineral(Component comp)
        {
            if (!hasMineral)
            {
                hasMineral = true;
                mineral = new Mineral();
            }
            switch (comp.compName)
            {
                case "Tritanium":
                    mineral.trit += comp.compCount;
                    break;
                case "Pyerite":
                    mineral.pyerite += comp.compCount;
                    break;
                case "Mexallon":
                    mineral.mexallon += comp.compCount;
                    break;
                case "Zydrine":
                    mineral.zydrine += comp.compCount;
                    break;
                case "Megacyte":
                    mineral.megacyte += comp.compCount;
                    break;
                case "Isogen":
                    mineral.isogen += comp.compCount;
                    break;
                case "Nocxium":
                    mineral.nocxium += comp.compCount;
                    break;
                case "Morphite":
                    mineral.morphite += comp.compCount;
                    break;
            }
        }

        private void addPI(Component c)
        {
            if (!hasPI)
            {
                hasPI = true;
                piList = new Dictionary<string, PI>();
            }
            try
            {
                PI p = piList[c.compName];
                p.count += c.compCount;
            }
            catch (KeyNotFoundException)
            {
                piList.Add(c.compName, new PI(c.compName, c.compId, c.compCount));
            }
        }

        private void addMoonGoo(Component c)
        {
            if (!hasMG)
            {
                hasMG = true;
                mg = new Dictionary<string, MoonGoo>();
            }
            try
            {
                MoonGoo m = mg[c.compName];
                m.count += c.compCount;
            }
            catch (KeyNotFoundException)
            {
                mg.Add(c.compName, new MoonGoo(c.compName, c.compId, c.compCount));
            }
        }

        private void addOther(Component c)
        {
            if(!hasOther)
            {
                hasOther = true;
                otherComps = new Dictionary<string, Other>();
            }
            try
            {
                Other o = otherComps[c.compName];
                o.count += c.compCount;
            }
            catch (KeyNotFoundException)
            {
                otherComps.Add(c.compName, new Other(c.compName, c.compId, c.compCount));
            }
        }
    }
}
