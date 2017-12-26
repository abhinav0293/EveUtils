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
    public class BPDisplayModel
    {
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

        static BPDisplayModel()
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

        public BPDisplayModel(Blueprint blueprint, int offset, int level)
        {
            this.level = level;
            this.blueprint = blueprint;
            this.offset = offset;
            createViews();
            skills = new Expander();
            TextBox tb = new TextBox
            {
                Text = "Skills Required",
                Style = tbStyle,
                Width = 717,
                FontSize = 20,
                Foreground = new SolidColorBrush(Colors.Black)
            };
            skills.Background = new SolidColorBrush(Colors.Aqua);
            skills.Header = tb;
        }

        private void createViews()
        {
            createOverview();
            if (blueprint.hasMineral)
            {
                createMineralView();
                createOresView();
            }
            if(blueprint.hasPI)
                createPIView();
            if(blueprint.hasMG)
                createMGView();
            if(blueprint.hasOther)
                createOtherView();
        }

        public StackPanel createSkills(List<Skill> skill, int level)
        {
            StackPanel sp = new StackPanel();
            foreach(Skill s in skill)
            {
                if (s.hasReqSkills)
                {
                    Border b = GenerateHeader.createHeader(s.skillName, s.level, offset, level);
                    Expander exp = new Expander
                    {
                        Header = b
                    };
                    exp.Content = createSkills(s.reqSkills, level + 1);
                    exp.Margin = new Thickness(20, 0, 0, 0);
                    sp.Children.Add(exp);
                }
                else
                {
                    Border b = GenerateHeader.GenerateSkillColumn(s.skillName, s.level, offset + 5, level);
                    sp.Children.Add(b);
                }

            }
            return sp;
        }

        public Expander getSkills()
        {
            return skills;
        }

        private void createOverview()
        {
            overview = new Expander();
            TextBox tb = new TextBox
            {
                Text = "Overview",
                Style = tbStyle,
                Width = 717,
                FontSize = 20,
                Foreground = new SolidColorBrush(Colors.Black)
            };
            overview.Background = new SolidColorBrush(Colors.Aqua);
            overview.Header = tb;
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Vertical;
            overview.Content = sp;
            foreach (Component c in blueprint.components)
            {
                if (c.hasBlueprint)
                {
                    Border b = GenerateHeader.createHeader(c.compName, c.compCount, offset, level);
                    Expander exp = new Expander
                    {
                        Header = b
                    };
                    StackPanel insp = new StackPanel();
                    BPDisplayModel inbpd = new BPDisplayModel(c.bp, offset, level + 1);
                    foreach(Expander e in inbpd.getViews())
                    {
                        e.Margin = new Thickness(20, 0, 0, 0);
                        insp.Children.Add(e);
                    }
                    exp.Content = insp;
                    exp.Margin = new Thickness(20, 0, 0, 0);
                    sp.Children.Add(exp);
                }
                else
                {
                    Border b = GenerateHeader.createColumn(c.compName, c.compCount, offset+5, level);
                    sp.Children.Add(b);
                }
                
            }
        }

        public List<Expander> getViews()
        {
            List<Expander> views = new List<Expander>();

            views.Add(overview);

            if (blueprint.hasMineral)
            { 
                views.Add(minerals);
                views.Add(ores);
            }
            if (blueprint.hasPI)
            {
                views.Add(pi);
            }
            if (blueprint.hasMG)
            {
                views.Add(mg);
            }
            if (blueprint.hasOther)
            {
                views.Add(other);
            }

            return views;
        }

        private void createMineralView()
        {
            minerals = new Expander();
            TextBox tb = new TextBox
            {
                Text = "Minerals",
                Style = tbStyle,
                Width = 717,
                FontSize = 20,
                Foreground = new SolidColorBrush(Colors.Black)
            };
            minerals.Background = new SolidColorBrush(Colors.LightSteelBlue);
            minerals.Header = tb;
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Vertical;
            minerals.Content = sp;
            if (blueprint.mineral.trit > 0)
            {
                Border b = GenerateHeader.createColumn("Tritanium", blueprint.mineral.trit, offset + 5, level);
                sp.Children.Add(b);
            }
            if (blueprint.mineral.pyerite > 0)
            {
                Border b = GenerateHeader.createColumn("Pyerite", blueprint.mineral.pyerite, offset + 5, level);
                sp.Children.Add(b);
            }
            if (blueprint.mineral.mexallon > 0)
            {
                Border b = GenerateHeader.createColumn("Mexallon", blueprint.mineral.mexallon, offset + 5, level);
                sp.Children.Add(b);
            }
            if (blueprint.mineral.isogen > 0)
            {
                Border b = GenerateHeader.createColumn("Isogen", blueprint.mineral.isogen, offset + 5, level);
                sp.Children.Add(b);
            }
            if (blueprint.mineral.nocxium > 0)
            {
                Border b = GenerateHeader.createColumn("Nocxium", blueprint.mineral.nocxium, offset + 5, level);
                sp.Children.Add(b);
            }
            if (blueprint.mineral.zydrine > 0)
            {
                Border b = GenerateHeader.createColumn("Zydrine", blueprint.mineral.zydrine, offset + 5, level);
                sp.Children.Add(b);
            }
            if (blueprint.mineral.megacyte > 0)
            {
                Border b = GenerateHeader.createColumn("Megacyte", blueprint.mineral.megacyte, offset + 5, level);
                sp.Children.Add(b);
            }
            if (blueprint.mineral.morphite > 0)
            {
                Border b = GenerateHeader.createColumn("Morphite", blueprint.mineral.morphite, offset + 5, level);
                sp.Children.Add(b);
            }
            Button butt = new Button();
            butt.Height = 30;
            butt.Width = 40;
            butt.Content = "Copy To ClipBoard";
        }

        private void createOresView()
        {
            ores = new Expander();
            TextBox tb = new TextBox
            {
                Text = "Ores",
                Style = tbStyle,
                Width = 717,
                FontSize = 20,
                Foreground = new SolidColorBrush(Colors.Black)
            };
            ores.Background = new SolidColorBrush(Colors.DeepSkyBlue);
            ores.Header = tb;
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Vertical;
            ores.Content = sp;
            if (blueprint.ores.spod > 0)
            {
                Border b = GenerateHeader.createColumn("Spodumain", blueprint.ores.spod, offset + 5, level);
                sp.Children.Add(b);
            }
            if (blueprint.ores.gneiss > 0)
            {
                Border b = GenerateHeader.createColumn("Gneiss", blueprint.ores.gneiss, offset + 5, level);
                sp.Children.Add(b);
            }
            if (blueprint.ores.crokite>0)
            {
                Border b = GenerateHeader.createColumn("Crokite", blueprint.ores.crokite, offset + 5, level);
                sp.Children.Add(b);
            }
            if (blueprint.ores.ochre > 0)
            {
                Border b = GenerateHeader.createColumn("Ochre", blueprint.ores.ochre, offset + 5, level);
                sp.Children.Add(b);
            }
            if (blueprint.ores.bistot > 0)
            {
                Border b = GenerateHeader.createColumn("Bistot", blueprint.ores.bistot, offset + 5, level);
                sp.Children.Add(b);
            }
            if (blueprint.ores.ark > 0)
            {
                Border b = GenerateHeader.createColumn("Arkonor", blueprint.ores.ark, offset + 5, level);
                sp.Children.Add(b);
            }
            if (blueprint.ores.mercox > 0)
            {
                Border b = GenerateHeader.createColumn("Mecoxit", blueprint.ores.mercox, offset + 5, level);
                sp.Children.Add(b);
            }
        }

        private void createPIView()
        {
            pi = new Expander();
            TextBox tb = new TextBox
            {
                Text = "PI",
                Style = tbStyle,
                Width = 717,
                FontSize = 20,
                Foreground = new SolidColorBrush(Colors.Black)
            };
            pi.Background = new SolidColorBrush(Colors.CadetBlue);
            pi.Header = tb;
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Vertical;
            pi.Content = sp;
            foreach (string pi in blueprint.piList.Keys)
            {
                PI p = blueprint.piList[pi];
                Border b = GenerateHeader.createColumn(p.name, p.count, offset + 5, level);
                sp.Children.Add(b);
            }
        }

        private void createMGView()
        {
            mg = new Expander();
            TextBox tb = new TextBox
            {
                Text = "Moon Goo",
                Style = tbStyle,
                Width = 717,
                FontSize = 20,
                Foreground = new SolidColorBrush(Colors.Black)
            };
            mg.Background = new SolidColorBrush(Colors.CornflowerBlue);
            mg.Header = tb;
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Vertical;
            mg.Content = sp;
            foreach (string pi in blueprint.mg.Keys)
            {
                MoonGoo p = blueprint.mg[pi];
                Border b = GenerateHeader.createColumn(p.name, p.count, offset + 5, level);
                sp.Children.Add(b);
            }
        }

        private void createOtherView()
        {
            other = new Expander();
            TextBox tb = new TextBox
            {
                Text = "Other",
                Style = tbStyle,
                Width = 717,
                FontSize = 20,
                Foreground = new SolidColorBrush(Colors.Black)
            };
            other.Background = new SolidColorBrush(Colors.LightBlue);
            other.Header = tb;
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Vertical;
            other.Content = sp;
            foreach (string pi in blueprint.otherComps.Keys)
            {
                Other p = blueprint.otherComps[pi];
                Border b = GenerateHeader.createColumn(p.name, p.count, offset + 5, level);
                sp.Children.Add(b);
            }
        }
    }
}
