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
            Ores ores = new Ores
            {
                spod = (long)Math.Ceiling(spodumain),
                gneiss = (long)Math.Ceiling(gneiss),
                crokite = (long)Math.Ceiling(crokite),
                bistot = (long)Math.Ceiling(bistot),
                ark = (long)Math.Ceiling(arkonor),
                ochre = (long)Math.Ceiling(ochre)
            };
            return ores;
        }

        private void calcarkonor()
        {
            if (megacyte > 0)
            {
                double eff = arkEff;
                arkonor = (double)(Math.Ceiling(megacyte / (352 * eff)));
                tritanium -= (double)(Math.Floor(arkonor * 24200 * eff));
                mexallon -= (double)(Math.Floor(arkonor * 2750 * eff));
                megacyte -= (double)(Math.Floor(arkonor * 352 * eff));
            }
        }

        private void calcbistot()
        {
            if (zydrine > 0)
            {
                double eff = bistotEff;
                bistot = (double)(Math.Ceiling(zydrine / (495 * eff)));
                pyerite -= (double)(Math.Floor(bistot * 13200 * eff));
                zydrine -= (double)(Math.Ceiling(bistot * 495 * eff));
                megacyte -= (double)(Math.Ceiling(bistot * 110 * eff));
            }
        }

        private void calccrokite()
        {
            if (nocxium > 0)
            {
                double eff = crokiteEff;
                crokite = (double)(Math.Ceiling(nocxium / (836 * eff)));
                tritanium -= (double)(Math.Floor(crokite * 23100 * eff));
                nocxium -= (double)(Math.Floor(crokite * 836 * eff));
                zydrine -= (double)(Math.Floor(crokite * 149 * eff));
            }
        }

        private void calcspod()
        {
            if (tritanium > 0)
            {
                double eff = spodEff;
                spodumain = (double)(Math.Ceiling(tritanium / (61600 * eff)));
                tritanium -= (double)(Math.Floor(spodumain * 61600 * eff));
                pyerite -= (double)(Math.Floor(spodumain * 13255 * eff));
                mexallon -= (double)(Math.Floor(spodumain * 2310 * eff));
                isogen -= (double)(Math.Floor(spodumain * 495 * eff));
            }
        }

        private void calcgneiss()
        {
            if (mexallon > 0)
            {
                double eff = gneissEff;
                gneiss = (double)(Math.Ceiling(mexallon / (2640 * eff)));
                pyerite -= (double)(Math.Floor(gneiss * 2420 * eff));
                mexallon -= (double)(Math.Floor(gneiss * 2640 * eff));
                isogen -= (double)(Math.Floor(gneiss * 330 * eff));
            }

        }

        private void calcochre()
        {
            if (isogen > 0)
            {
                double eff = ochreEff;
                ochre = (double)(Math.Ceiling(isogen / (1760 * eff)));
                tritanium -= (double)(Math.Floor(ochre * eff * 11000));
                isogen -= (double)(Math.Floor(ochre * 1760 * eff));
                nocxium -= (double)(Math.Floor(ochre * eff * 132));
            }
        }

        private void calcPyreq()
        {
            double eff = spodEff;
            double sp = (double)(Math.Ceiling(pyerite / (13255 * eff)));
            tritanium -= (double)(Math.Floor(sp * 61600 * eff));
            pyerite -= (double)(Math.Floor(sp * 12563 * eff));
            mexallon -= (double)(Math.Floor(sp * 2310 * eff));
            isogen -= (double)(Math.Floor(sp * 495 * eff));
            spodumain += sp;
        }
        #endregion
    }
}
