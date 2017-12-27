using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace EveTools.Tools
{
    public class ItemGenerator
    {

        private static Style tbStyle = new Style(typeof(TextBox));

        static ItemGenerator()
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

        public static Border generateHeader(string compName, int compCount)
        {
            //#E7E7DE
            Color grid = new Color();
            grid.A = 255;
            grid.R = 231;
            grid.G = 231;
            grid.B = 222;

            //#008891
            Color t = new Color();
            t.A = 255;
            t.R = 0;
            t.G = 136;
            t.B = 145;

            //#00587A
            Color t1 = new Color();
            t1.A = 255;
            t1.R = 0;
            t1.G = 88;
            t1.B = 122;

            //#F0A830
            Color f = new Color();
            f.A = 255;
            f.R = 240;
            f.G = 168;
            f.B = 48;

            SolidColorBrush fg = new SolidColorBrush(f);
            SolidColorBrush bg = new SolidColorBrush(grid);
            SolidColorBrush ba = new SolidColorBrush(t);
            SolidColorBrush bb = new SolidColorBrush(t1);

            Border b = new Border();
            b.BorderBrush = new SolidColorBrush(Colors.YellowGreen);
            b.Background = bb;
            b.BorderThickness = new Thickness(1);
            Grid inSp = new Grid();
            inSp.Background = bb;
            inSp.HorizontalAlignment = HorizontalAlignment.Left;
            inSp.VerticalAlignment = VerticalAlignment.Top;
            inSp.ColumnDefinitions.Add(new ColumnDefinition());
            inSp.ColumnDefinitions.Add(new ColumnDefinition());

            TextBox name = new TextBox();
            name.Text = compName;
            name.Style = tbStyle;
            name.Height = 50;
            name.Width = 370;
            Grid.SetColumn(name, 0);
            name.Background = ba;
            name.Foreground = fg;
            inSp.Children.Add(name);

            TextBox count = new TextBox();
            count.Text = string.Format("{0:0,0}", compCount);
            count.Style = tbStyle;
            count.Height = 50;
            count.Width = 210;
            count.HorizontalAlignment = HorizontalAlignment.Right;
            count.HorizontalContentAlignment = HorizontalAlignment.Right;
            Grid.SetColumn(count, 1);
            count.Foreground = fg;
            count.Background = bb;
            inSp.Children.Add(count);

            b.Child = inSp;
            return b;
        }
    }
}
