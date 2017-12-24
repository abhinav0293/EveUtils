using System;
using System.Collections.Generic;
using EveTools.DataModels;

namespace EveTools.Tools
{
    class OreCalculator
    {

        #region init
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

        public OreCalculator(Mineral min)
        {
            tritanium = min.trit;
            pyerite = min.pyerite;
            mexallon = min.mexallon;
            isogen = min.isogen;
            nocxium = min.nocxium;
            zydrine = min.zydrine;
            megacyte = min.megacyte;
            spodEff = App.eff["spod"];
            gneissEff = App.eff["gneiss"];
            ochreEff = App.eff["ochre"];
            bistotEff = App.eff["bistot"];
            arkEff = App.eff["ark"];
            crokiteEff = App.eff["crokite"];
        }
        #endregion

        #region oreCalc
        public Ores calcOres()
        {
            calccrokite();
            calcbistot();
            calcarkonor();
            calcspod();
            calcgneiss();
            if (pyerite > 0)
            {
                calcPyreq();
            }
            calcochre();
            Ores ores = new Ores();
            ores.spod = (long)Math.Ceiling(spodumain);
            ores.gneiss = (long)Math.Ceiling(gneiss);
            ores.crokite = (long)Math.Ceiling(crokite);
            ores.bistot = (long)Math.Ceiling(bistot);
            ores.ark = (long)Math.Ceiling(arkonor);
            ores.ochre = (long)Math.Ceiling(ochre);
            return ores;
        }

        private void calcarkonor()
        {
            if (megacyte > 0)
            {
                double eff = arkEff;
                arkonor = (double)(Math.Ceiling(megacyte / (336 * eff)));
                tritanium -= (double)(Math.Floor(arkonor * 23100 * eff));
                mexallon -= (double)(Math.Floor(arkonor * 2625 * eff));
                megacyte -= (double)(Math.Floor(arkonor * 336 * eff));
            }
        }

        private void calcbistot()
        {
            if (zydrine > 0)
            {
                double eff = bistotEff;
                bistot = (double)(Math.Ceiling(zydrine / (473 * eff)));
                pyerite -= (double)(Math.Floor(bistot * 12600 * eff));
                zydrine -= (double)(Math.Ceiling(bistot * 473 * eff));
                megacyte -= (double)(Math.Ceiling(bistot * 105 * eff));
            }
        }

        private void calccrokite()
        {
            if (nocxium > 0)
            {
                double eff = crokiteEff;
                crokite = (double)(Math.Ceiling(nocxium / (798 * eff)));
                tritanium -= (double)(Math.Floor(crokite * 22050 * eff));
                nocxium -= (double)(Math.Floor(crokite * 798 * eff));
                zydrine -= (double)(Math.Floor(crokite * 142 * eff));
            }
        }

        private void calcspod()
        {
            if (tritanium > 0)
            {
                double eff = spodEff;
                spodumain = (double)(Math.Ceiling(tritanium / (58800 * eff)));
                tritanium -= (double)(Math.Floor(spodumain * 58800 * eff));
                pyerite -= (double)(Math.Floor(spodumain * 12653 * eff));
                mexallon -= (double)(Math.Floor(spodumain * 2205 * eff));
                isogen -= (double)(Math.Floor(spodumain * 473 * eff));
            }
        }

        private void calcgneiss()
        {
            if (mexallon > 0)
            {
                double eff = gneissEff;
                gneiss = (double)(Math.Ceiling(mexallon / (2520 * eff)));
                pyerite -= (double)(Math.Floor(gneiss * 2310 * eff));
                mexallon -= (double)(Math.Floor(gneiss * 2520 * eff));
                isogen -= (double)(Math.Floor(gneiss * 315 * eff));
            }

        }

        private void calcochre()
        {
            if (isogen > 0)
            {
                double eff = ochreEff;
                ochre = (double)(Math.Ceiling(isogen / (1600 * eff)));
                tritanium -= (double)(Math.Floor(ochre * eff * 10000));
                isogen -= (double)(Math.Floor(ochre * 1600 * eff));
                nocxium -= (double)(Math.Floor(ochre * eff * 120));
            }
        }

        private void calcPyreq()
        {
            double eff = spodEff;
            double sp = (double)(Math.Ceiling(pyerite / (12653 * eff)));
            tritanium -= (double)(Math.Floor(sp * 58800 * eff));
            pyerite -= (double)(Math.Floor(sp * 12563 * eff));
            mexallon -= (double)(Math.Floor(sp * 2205 * eff));
            isogen -= (double)(Math.Floor(sp * 473 * eff));
            spodumain += sp;
        }
        #endregion
    }
}
