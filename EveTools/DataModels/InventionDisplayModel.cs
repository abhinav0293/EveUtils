using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using EveTools.Tools;

namespace EveTools.DataModels
{
    public class InventionDisplayModel
    {
        public Invention i;
        public Expander main;
        public Expander skills;
        public Expander other;
        private static Style tbStyle = new Style(typeof(TextBox));
        int offset = 20;
        int level = 1;

        public InventionDisplayModel(Invention i)
        {
            this.i = i;
            createMainView();
            createOtherView();
            createSkillsView();
        }

        static InventionDisplayModel()
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

        private void createMainView()
        {
            Color f = new Color();
            f.A = 255;
            f.R = 240;
            f.G = 168;
            f.B = 48;
            Color t = new Color();
            t.A = 255;
            t.R = 0;
            t.G = 136;
            t.B = 145;

            SolidColorBrush fg = new SolidColorBrush(f);
            SolidColorBrush ba = new SolidColorBrush(t);

            main = new Expander();
            TextBox tb = new TextBox
            {
                Text = "Blueprint Required",
                Style = tbStyle,
                Width = 690,
                FontSize = 20,
                Foreground = new SolidColorBrush(Colors.Black),
                HorizontalAlignment = HorizontalAlignment.Left
            };
            main.Background = new SolidColorBrush(Colors.CornflowerBlue);
            main.Header = tb;
            main.Content = new Border
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                BorderBrush = new SolidColorBrush(Colors.YellowGreen),
                BorderThickness = new Thickness(1),
                Margin = new Thickness(25, 0, 0, 0),
                Child = new TextBox
                {
                    Text = i.in_bp,
                    Style = tbStyle,
                    Width = 722,
                    FontSize = 20,
                    Background = ba,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Foreground = fg
                }
            };
            
        }

        private void createOtherView()
        {
            other = new Expander();
            TextBox tb = new TextBox
            {
                Text = "Items Required",
                Style = tbStyle,
                Width = 717,
                FontSize = 20,
                Foreground = new SolidColorBrush(Colors.Black)
            };
            other.Background = new SolidColorBrush(Colors.LightBlue);
            other.Header = tb;
            other.Width = 755;
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Vertical;
            other.Content = sp;
            foreach (Other p in i.reqs)
            {
                Border b = ItemGenerator.generateRow(p.name, p.count, 20, 1, false);
                sp.Children.Add(b);
            }
        }

        private void createSkillsView()
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

            foreach (Skill s in i.reqSkills)
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
                    foreach (Skill sk in s.reqSkills)
                    {
                        if (sk.hasReqSkills)
                        {
                            sksp.Children.Add(createInnerSkill(sk, level + 1, offset - 10));
                        }
                        else
                        {
                            Border br = ItemGenerator.generateRow(sk.skillName, sk.level, offset, level + 1, false);
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

            Border b = ItemGenerator.generateHeader(s.skillName, s.level, offset + 10, level);
            Expander e = new Expander
            {
                Header = b,
                Width = 748 - (offset * level),
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness((offset * level), 0, 0, 0),
                Background = new SolidColorBrush(App.getInnerColor()),
                Content = sp
            };
            foreach (Skill sk in s.reqSkills)
            {
                if (sk.hasReqSkills)
                {
                    sp.Children.Add(createInnerSkill(sk, level + 1, offset));
                }
                else
                {
                    Border br = ItemGenerator.generateSkillRow(s.skillName, s.level, offset + 20, level, false);
                    sp.Children.Add(br);
                }
            }
            return e;
        }
    }
}
