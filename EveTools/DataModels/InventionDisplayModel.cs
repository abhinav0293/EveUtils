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

        public InventionDisplayModel(Invention i)
        {
            this.i = i;
            createMainView();
            createOtherView();
            //createSkillsView();
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
    }
}
