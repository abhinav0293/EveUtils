using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EveTools.Tools;

namespace EveTools.DataModels
{
    class ManufactureDisplayModel
    {

        #region member_variables
        private static Style tbStyle = new Style(typeof(TextBox));
        public Blueprint blueprint;
        public Expander overview;
        public Expander pi;
        public Expander other;
        public Expander mg;
        public Expander minerals;
        public Expander ores;
        public Expander skills;
        public int offset;
        public int level;
        public bool inside = false;
        #endregion

        static ManufactureDisplayModel()
        {
            tbStyle.Setters.Add(new Setter(Control.BackgroundProperty, null));
            tbStyle.Setters.Add(new Setter(Control.BorderBrushProperty, null));
            tbStyle.Setters.Add(new Setter(Control.BorderThicknessProperty, new Thickness(0)));
            tbStyle.Setters.Add(new Setter(Control.PaddingProperty, new Thickness(0)));
            tbStyle.Setters.Add(new Setter(System.Windows.Controls.Primitives.TextBoxBase.IsReadOnlyProperty, true));
            tbStyle.Setters.Add(new Setter(Control.IsTabStopProperty, true));
            tbStyle.Setters.Add(new Setter(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Stretch));
            tbStyle.Setters.Add(new Setter(UIElement.SnapsToDevicePixelsProperty, true));
            tbStyle.Setters.Add(new Setter(Control.VerticalContentAlignmentProperty, VerticalAlignment.Center));
            tbStyle.Setters.Add(new Setter(TextBox.TextWrappingProperty, TextWrapping.Wrap));
            tbStyle.Setters.Add(new Setter(Control.FontWeightProperty, FontWeights.Bold));
            tbStyle.Setters.Add(new Setter(Control.FontSizeProperty, 18.0));
            Trigger tb = new Trigger();
            tb.SetValue(UIElement.IsEnabledProperty, false);
            //tb.Setters.Add(new Setter(BackgroundProperty, null));
            tbStyle.Triggers.Add(tb);
        }

        public ManufactureDisplayModel(Blueprint blueprint, int offset, int level, bool inside)
        {
            this.level = level;
            this.blueprint = blueprint;
            this.offset = offset;
            this.inside = inside;
            createViews();
        }

        private void createViews()
        {
            createOverview();
            if (blueprint.hasMineral)
            {
                createMineralView();
                createOresView();
            }
            if (blueprint.hasPI)
                createPIView();
            if (blueprint.hasMG)
                createMGView();
            if (blueprint.hasOther)
                createOtherView();
            createSkillView();
        }

        private void createOverview()
        {
            TextBox tb = new TextBox
            {
                Text = "Overview",
                Style = tbStyle,
                Width = 725 - (offset * (level - 1) * 2),
                FontSize = 20,
                Foreground = new SolidColorBrush(Colors.Black),
                //Background = new SolidColorBrush(Colors.Wheat),
                HorizontalAlignment = HorizontalAlignment.Left
            };

            StackPanel sp = new StackPanel
            {
                Orientation = Orientation.Vertical
            };

            overview = new Expander
            {
                Background = new SolidColorBrush(App.getColor()),
                Header = tb,
                Width = 755 - (offset * (level - 1) * 2),
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness((offset * (level - 1)), 0, 0, 0),
                Content = sp
            };

            if (inside)
            {
                overview.IsExpanded = true;
            }
            List<Border> exp = new List<Border>();
            foreach (Component c in blueprint.components)
            {
                if (c.hasBlueprint)
                {
                    Border b = ItemGenerator.generateHeader(c.compName, c.compCount, offset, level);
                    Expander e = new Expander
                    {
                        Header = b,
                        Width = 748 - (offset * level),
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = new Thickness((offset * level)+2, 0, 0, 0),
                        Background = new SolidColorBrush(App.getInnerColor())
                    };

                    ManufactureDisplayModel inbpd = new ManufactureDisplayModel(c.bp, offset, level + 1, true);
                    Expander ex = inbpd.getViews()[0];
                    e.Content = ex;
                    sp.Children.Add(e);
                }
                else
                {
                    exp.Add(ItemGenerator.generateRow(c.compName, c.compCount, offset, level, inside));
                }
            }
            for (int x = 0; x < exp.Count; x++)
            {
                Border en = exp[0];
                exp.Remove(en);
                sp.Children.Add(en);
                x--;
            }
        }

        private void createMineralView()
        {
            TextBox tb = new TextBox
            {
                Text = "Minerals",
                Style = tbStyle,
                Width = 725 - (offset * (level - 1) * 2),
                FontSize = 20,
                Foreground = new SolidColorBrush(Colors.Black),
                //Background = new SolidColorBrush(Colors.Wheat),
                HorizontalAlignment = HorizontalAlignment.Left
            };

            StackPanel sp = new StackPanel
            {
                Orientation = Orientation.Vertical
            };

            minerals = new Expander
            {
                Background = new SolidColorBrush(App.getColor()),
                Header = tb,
                Width = 755 - (offset * (level - 1) * 2),
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness((offset * (level - 1)), 0, 0, 0),
                Content = sp
            };

            String st = "";
            if (blueprint.mineral.trit > 0)
            {
                Border b = ItemGenerator.generateRow("Tritanium", blueprint.mineral.trit, offset, level, inside);
                sp.Children.Add(b);
                st += "Tritanium:\t\t" + blueprint.mineral.trit + Environment.NewLine;
            }
            if (blueprint.mineral.pyerite > 0)
            {
                Border b = ItemGenerator.generateRow("Pyerite", blueprint.mineral.pyerite, offset, level, inside);
                sp.Children.Add(b);
                st += "Pyerite:\t\t" + blueprint.mineral.pyerite + Environment.NewLine;
            }
            if (blueprint.mineral.mexallon > 0)
            {
                Border b = ItemGenerator.generateRow("Mexallon", blueprint.mineral.mexallon, offset, level, inside);
                sp.Children.Add(b);
                st += "Mexallon:\t\t" + blueprint.mineral.mexallon + Environment.NewLine;
            }
            if (blueprint.mineral.isogen > 0)
            {
                Border b = ItemGenerator.generateRow("Isogen", blueprint.mineral.isogen, offset, level, inside);
                sp.Children.Add(b);
                st += "Isogen:\t\t\t" + blueprint.mineral.isogen + Environment.NewLine;
            }
            if (blueprint.mineral.nocxium > 0)
            {
                Border b = ItemGenerator.generateRow("Nocxium", blueprint.mineral.nocxium, offset, level, inside);
                sp.Children.Add(b);
                st += "Nocxium:\t\t" + blueprint.mineral.nocxium + Environment.NewLine;
            }
            if (blueprint.mineral.zydrine > 0)
            {
                Border b = ItemGenerator.generateRow("Zydrine", blueprint.mineral.zydrine, offset, level, inside);
                sp.Children.Add(b);
                st += "Zydrine:\t\t" + blueprint.mineral.zydrine + Environment.NewLine;
            }
            if (blueprint.mineral.megacyte > 0)
            {
                Border b = ItemGenerator.generateRow("Megacyte", blueprint.mineral.megacyte, offset, level, inside);
                sp.Children.Add(b);
                st += "Megacyte:\t\t" + blueprint.mineral.megacyte + Environment.NewLine;
            }
            if (blueprint.mineral.morphite > 0)
            {
                Border b = ItemGenerator.generateRow("Morphite", blueprint.mineral.morphite, offset, level, inside);
                sp.Children.Add(b);
                st += "Morphite:\t\t" + blueprint.mineral.morphite + Environment.NewLine;
            }
            Button butt = new Button
            {
                Height = 30,
                Width = 140,
                //#FF955353
                Background = new SolidColorBrush(Color.FromArgb(255, 149, 83, 83)),
                //#FFE3EE00
                Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)),
                FontSize = 14,
                FontWeight = FontWeights.Bold,
                Content = "Copy To ClipBoard"
            };
            butt.Click += (s, e) => Clipboard.SetText(st);
            sp.Children.Add(butt);
        }

        private void createOresView()
        {
            TextBox tb = new TextBox
            {
                Text = "Ores",
                Style = tbStyle,
                Width = 725 - (offset * (level - 1) * 2),
                FontSize = 20,
                Foreground = new SolidColorBrush(Colors.Black),
                //Background = new SolidColorBrush(Colors.Wheat),
                HorizontalAlignment = HorizontalAlignment.Left
            };

            StackPanel sp = new StackPanel
            {
                Orientation = Orientation.Vertical
            };

            ores = new Expander
            {
                Background = new SolidColorBrush(App.getColor()),
                Header = tb,
                Width = 755 - (offset * (level - 1) * 2),
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness((offset * (level - 1)), 0, 0, 0),
                Content = sp
            };
            
            string st = "";
            if (blueprint.ores.spod > 0)
            {
                Border b = ItemGenerator.generateRow("Spodumain", blueprint.ores.spod, offset, level, inside);
                sp.Children.Add(b);
                st += "Spodumain:\t\t" + blueprint.ores.spod + Environment.NewLine;
            }
            if (blueprint.ores.gneiss > 0)
            {
                Border b = ItemGenerator.generateRow("Gneiss", blueprint.ores.gneiss, offset, level, inside);
                sp.Children.Add(b);
                st += "Gneiss:\t\t\t" + blueprint.ores.gneiss + Environment.NewLine;
            }
            if (blueprint.ores.crokite > 0)
            {
                Border b = ItemGenerator.generateRow("Crokite", blueprint.ores.crokite, offset, level, inside);
                sp.Children.Add(b);
                st += "Crokite:\t\t" + blueprint.ores.crokite + Environment.NewLine;
            }
            if (blueprint.ores.ochre > 0)
            {
                Border b = ItemGenerator.generateRow("Ochre", blueprint.ores.ochre, offset, level, inside);
                sp.Children.Add(b);
                st += "Ochre:\t\t\t" + blueprint.ores.ochre + Environment.NewLine;
            }
            if (blueprint.ores.bistot > 0)
            {
                Border b = ItemGenerator.generateRow("Bistot", blueprint.ores.bistot, offset, level, inside);
                sp.Children.Add(b);
                st += "Bistot:\t\t\t" + blueprint.ores.bistot + Environment.NewLine;
            }
            if (blueprint.ores.ark > 0)
            {
                Border b = ItemGenerator.generateRow("Arkonor", blueprint.ores.ark, offset, level, inside);
                sp.Children.Add(b);
                st += "Arkonor:\t\t" + blueprint.ores.ark + Environment.NewLine;
            }
            if (blueprint.ores.mercox > 0)
            {
                Border b = ItemGenerator.generateRow("Mercoxit", blueprint.ores.mercox, offset, level, inside);
                sp.Children.Add(b);
                st += "Mercoxit:\t\t" + blueprint.ores.mercox + Environment.NewLine;
            }
            Button butt = new Button();
            butt.Height = 30;
            butt.Width = 140;
            //#FF955353
            butt.Background = new SolidColorBrush(Color.FromArgb(255, 149, 83, 83));
            //#FFE3EE00
            butt.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            butt.FontSize = 14;
            butt.FontWeight = FontWeights.Bold;
            butt.Content = "Copy To ClipBoard";
            butt.Click += (s, e) => Clipboard.SetText(st);
            sp.Children.Add(butt);
        }

        private void createPIView()
        {
            StackPanel sp = new StackPanel
            {
                Orientation = Orientation.Vertical
            };

            TextBox tb = new TextBox
            {
                Text = "PI",
                Style = tbStyle,
                Width = 725 - (offset * (level - 1) * 2),
                FontSize = 20,
                Foreground = new SolidColorBrush(Colors.Black),
                //Background = new SolidColorBrush(Colors.Wheat),
                HorizontalAlignment = HorizontalAlignment.Left
            };

            pi = new Expander
            {
                Background = new SolidColorBrush(App.getColor()),
                Header = tb,
                Width = 755 - (offset * (level - 1) * 2),
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness((offset * (level - 1)), 0, 0, 0),
                Content = sp
            };

            foreach (string pi in blueprint.piList.Keys)
            {
                PI p = blueprint.piList[pi];
                Border b = ItemGenerator.generateRow(p.name, p.count, offset, level, inside);
                sp.Children.Add(b);
            }
        }

        private void createMGView()
        {
            StackPanel sp = new StackPanel
            {
                Orientation = Orientation.Vertical
            };

            TextBox tb = new TextBox
            {
                Text = "Moon Materials",
                Style = tbStyle,
                Width = 725 - (offset * (level - 1) * 2),
                FontSize = 20,
                Foreground = new SolidColorBrush(Colors.Black),
                //Background = new SolidColorBrush(Colors.Wheat),
                HorizontalAlignment = HorizontalAlignment.Left
            };

            mg = new Expander
            {
                Background = new SolidColorBrush(App.getColor()),
                Header = tb,
                Width = 755 - (offset * (level - 1) * 2),
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness((offset * (level - 1)), 0, 0, 0),
                Content = sp
            };

            foreach (string pi in blueprint.mg.Keys)
            {
                MoonGoo p = blueprint.mg[pi];
                Border b = ItemGenerator.generateRow(p.name, p.count, offset, level, inside);
                sp.Children.Add(b);
            }
        }

        private void createOtherView()
        {
            StackPanel sp = new StackPanel
            {
                Orientation = Orientation.Vertical
            };

            TextBox tb = new TextBox
            {
                Text = "Other Requirements",
                Style = tbStyle,
                Width = 725 - (offset * (level - 1) * 2),
                FontSize = 20,
                Foreground = new SolidColorBrush(Colors.Black),
                //Background = new SolidColorBrush(Colors.Wheat),
                HorizontalAlignment = HorizontalAlignment.Left
            };

            other = new Expander
            {
                Background = new SolidColorBrush(App.getColor()),
                Header = tb,
                Width = 755 - (offset * (level - 1) * 2),
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness((offset * (level - 1)), 0, 0, 0),
                Content = sp
            };

            foreach (string pi in blueprint.otherComps.Keys)
            {
                Other p = blueprint.otherComps[pi];
                Border b = ItemGenerator.generateRow(p.name, p.count, offset, level, inside);
                sp.Children.Add(b);
            }
        }

        private void createSkillView()
        {
            StackPanel sp = new StackPanel
            {
                Orientation = Orientation.Vertical
            };

            TextBox tb = new TextBox
            {
                Text = "Skills Required",
                Style = tbStyle,
                Width = 725 - (offset * (level - 1) * 2),
                FontSize = 20,
                Foreground = new SolidColorBrush(Colors.Black),
                //Background = new SolidColorBrush(Colors.Wheat),
                HorizontalAlignment = HorizontalAlignment.Left
            };

            skills = new Expander
            {
                Background = new SolidColorBrush(App.getColor()),
                Header = tb,
                Width = 755 - (offset * (level - 1) * 2),
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness((offset * (level - 1)), 0, 0, 0),
                Content = sp
            };

            blueprint.getSkills();
            foreach(Skill s in blueprint.skills)
            {
                if (s.hasReqSkills)
                {
                    StackPanel sksp = new StackPanel
                    {
                        Orientation = Orientation.Vertical
                    };

                    Border b = ItemGenerator.generateHeader(s.skillName, s.level, offset, level);
                    Expander e = new Expander
                    {
                        Header = b,
                        Width = 748 - (offset * level),
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = new Thickness((offset * level) + 2, 0, 0, 0),
                        Background = new SolidColorBrush(App.getInnerColor()),
                        Content = sksp
                    };
                    sp.Children.Add(e);
                    foreach(Skill sk in s.reqSkills)
                    {
                        if (sk.hasReqSkills)
                        {
                            sksp.Children.Add(createInnerSkill(sk, level + 1, offset-10));
                        }
                        else
                        {
                            Border br = ItemGenerator.generateRow(sk.skillName, sk.level, offset, level+1, false);
                            br.Margin = new Thickness(23, 0, 0, 0);
                            sksp.Children.Add(br);
                        }
                    }
                }
                else
                {
                    Border b = ItemGenerator.generateRow(s.skillName, s.level, offset, level, false);
                    sp.Children.Add(b);
                }
            }
        }

        private Expander createInnerSkill(Skill s, int level, int offset)
        {
            StackPanel sp = new StackPanel
            {
                Orientation = Orientation.Vertical
            };

            Border b = ItemGenerator.generateHeader(s.skillName, s.level, offset+10, level);
            Expander e = new Expander
            {
                Header = b,
                Width = 748 - (offset * level),
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness((offset * level), 0, 0, 0),
                Background = new SolidColorBrush(App.getInnerColor()),
                Content = sp
            };
            foreach(Skill sk in s.reqSkills)
            {
                if (sk.hasReqSkills)
                {
                    sp.Children.Add(createInnerSkill(sk, level + 1, offset));
                }
                else
                {
                    Border br = ItemGenerator.generateSkillRow(s.skillName, s.level, offset+20, level, false);
                    sp.Children.Add(br);
                }
            }
            return e;
        }

        public List<Expander> getViews()
        {
            List<Expander> e = new List<Expander>
            {
                overview
            };

            if (blueprint.hasMineral)
            {
                e.Add(minerals);
                e.Add(ores);
            }

            if (blueprint.hasPI)
            {
                e.Add(pi);
            }

            if (blueprint.hasMG)
            {
                e.Add(mg);
            }

            if (blueprint.hasOther)
            {
                e.Add(other);
            }

            e.Add(skills);
            return e;
        }
    }
}
