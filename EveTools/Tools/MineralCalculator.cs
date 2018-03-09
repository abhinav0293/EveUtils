using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveTools.DataModels;

namespace EveTools.Tools
{
    public class MineralCalculator
    {
        Ores ores;
        double tritanium = 0;
        double pyerite = 0;
        double mexallon = 0;
        double isogen = 0;
        double nocxium = 0;
        double zydrine = 0;
        double megacyte = 0;
        public double arkonor = 0;
        public double bistot = 0;
        public double crokite = 0;
        public double gneiss = 0;
        public double spodumain = 0;
        public double ochre = 0;
        double spodEff = 0;
        double gneissEff = 0;
        double ochreEff = 0;
        double crokiteEff = 0;
        double bistotEff = 0;
        double arkEff = 0;

        public MineralCalculator(Ores ores)
        {
            arkonor = ores.ark;
            bistot = ores.bistot;
            crokite = ores.crokite;
            ochre = ores.ochre;
            gneiss = ores.gneiss;
            spodumain = ores.spod;
            spodEff = App.eff["spod"];
            gneissEff = App.eff["gneiss"];
            ochreEff = App.eff["ochre"];
            bistotEff = App.eff["bistot"];
            arkEff = App.eff["ark"];
            crokiteEff = App.eff["crokite"];
        }

        public Mineral calcMinerals()
        {
            Mineral min = new Mineral();
            tritanium -= (double)(Math.Floor(arkonor * 24200 * arkEff));
            mexallon -= (double)(Math.Floor(arkonor * 2750 * arkEff));
            megacyte -= (double)(Math.Floor(arkonor * 352 * arkEff));

            pyerite -= (double)(Math.Floor(bistot * 13200 * bistotEff));
            zydrine -= (double)(Math.Ceiling(bistot * 495 * bistotEff));
            megacyte -= (double)(Math.Ceiling(bistot * 110 * bistotEff));

            tritanium -= (double)(Math.Floor(crokite * 23100 * crokiteEff));
            nocxium -= (double)(Math.Floor(crokite * 836 * crokiteEff));
            zydrine -= (double)(Math.Floor(crokite * 149 * crokiteEff));

            tritanium -= (double)(Math.Floor(spodumain * 61600 * spodEff));
            pyerite -= (double)(Math.Floor(spodumain * 13255 * spodEff));
            mexallon -= (double)(Math.Floor(spodumain * 2310 * spodEff));
            isogen -= (double)(Math.Floor(spodumain * 495 * spodEff));

            pyerite -= (double)(Math.Floor(gneiss * 2420 * gneissEff));
            mexallon -= (double)(Math.Floor(gneiss * 2640 * gneissEff));
            isogen -= (double)(Math.Floor(gneiss * 330 * gneissEff));

            tritanium -= (double)(Math.Floor(ochre * ochreEff * 11000));
            isogen -= (double)(Math.Floor(ochre * 1760 * ochreEff));
            nocxium -= (double)(Math.Floor(ochre * ochreEff * 132));

            min.trit = Math.Abs((long)tritanium);
            min.pyerite = Math.Abs((long)pyerite);
            min.mexallon = Math.Abs((long)mexallon);
            min.isogen = Math.Abs((long)isogen);
            min.nocxium = Math.Abs((long)nocxium);
            min.zydrine = Math.Abs((long)zydrine);
            min.megacyte = Math.Abs((long)megacyte);

            return min;
        }
    }
}
