using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
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

        public static Border generateHeader(string compName, long compCount, int offset, int level)
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

            Border b = new Border
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                BorderBrush = new SolidColorBrush(Colors.YellowGreen),
                Background = ba,
                BorderThickness = new Thickness(1),
                Width = 700 - (offset * (level-1)),
                //Margin = new Thickness(offset, 0, 0, 0)
            };
            Grid inSp = new Grid
            {
                Background = ba,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            };
            inSp.ColumnDefinitions.Add(new ColumnDefinition());
            ColumnDefinition cd1 = new ColumnDefinition();
            cd1.Width = new GridLength(230);
            inSp.ColumnDefinitions.Add(cd1);

            TextBox name = new TextBox
            {
                Text = compName,
                Style = tbStyle,
                Height = 30,
                Width = 502 - (offset * level),
                HorizontalAlignment = HorizontalAlignment.Left,
                HorizontalContentAlignment = HorizontalAlignment.Left,
                Background = ba,
                Foreground = fg
            };
            Grid.SetColumn(name, 0);
            inSp.Children.Add(name);

            TextBox count = new TextBox
            {
                Text = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", compCount),
                Style = tbStyle,
                Height = 30,
                Width = 230,
                HorizontalAlignment = HorizontalAlignment.Right,
                HorizontalContentAlignment = HorizontalAlignment.Right,
                Foreground = fg,
                Background = bb
            };
            Grid.SetColumn(count, 1);
            inSp.Children.Add(count);

            b.Child = inSp;
            return b;
        }

        public static Border generateRow(string compName, long compCount, int offset, int level, bool inside)
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
            int modifier = inside ? 0 : 20;
            Border b = new Border
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                BorderBrush = new SolidColorBrush(Colors.YellowGreen),
                Background = ba,
                BorderThickness = new Thickness(1),
                Width = 705 - (offset * (level - 1)) + modifier,
                Margin = new Thickness(offset + 3, 0, 0, 0)
            };
            Grid inSp = new Grid
            {
                Background = ba,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            };
            inSp.ColumnDefinitions.Add(new ColumnDefinition());
            ColumnDefinition cd1 = new ColumnDefinition
            {
                Width = new GridLength(230)
            };
            inSp.ColumnDefinitions.Add(cd1);

            TextBox name = new TextBox
            {
                Text = compName,
                Style = tbStyle,
                Height = 30,
                Width = 507 - (offset * level) + modifier,
                HorizontalAlignment = HorizontalAlignment.Left,
                HorizontalContentAlignment = HorizontalAlignment.Left,
                Background = ba,
                Foreground = fg
            };
            Grid.SetColumn(name, 0);
            inSp.Children.Add(name);

            TextBox count = new TextBox
            {
                Text = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", compCount),
                Style = tbStyle,
                Height = 30,
                Width = 230,
                HorizontalAlignment = HorizontalAlignment.Right,
                HorizontalContentAlignment = HorizontalAlignment.Right,
                Foreground = fg,
                Background = bb
            };
            Grid.SetColumn(count, 1);
            inSp.Children.Add(count);

            b.Child = inSp;
            return b;
        }

        public static Border generateSkillRow(string compName, long compCount, int offset, int level, bool inside)
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
            int modifier = inside ? 0 : 20;
            Border b = new Border
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                BorderBrush = new SolidColorBrush(Colors.YellowGreen),
                Background = ba,
                BorderThickness = new Thickness(1),
                Width = 705 - ((offset+17) * (level - 1)) + modifier,
                Margin = new Thickness(offset-5, 0, 0, 0)
            };
            Grid inSp = new Grid
            {
                Background = ba,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            };
            inSp.ColumnDefinitions.Add(new ColumnDefinition());
            ColumnDefinition cd1 = new ColumnDefinition
            {
                Width = new GridLength(230)
            };
            inSp.ColumnDefinitions.Add(cd1);

            TextBox name = new TextBox
            {
                Text = compName,
                Style = tbStyle,
                Height = 30,
                Width = 507 - ((offset) * level) + modifier,
                HorizontalAlignment = HorizontalAlignment.Left,
                HorizontalContentAlignment = HorizontalAlignment.Left,
                Background = ba,
                Foreground = fg
            };
            Grid.SetColumn(name, 0);
            inSp.Children.Add(name);

            TextBox count = new TextBox
            {
                Text = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", compCount),
                Style = tbStyle,
                Height = 30,
                Width = 230,
                HorizontalAlignment = HorizontalAlignment.Right,
                HorizontalContentAlignment = HorizontalAlignment.Right,
                Foreground = fg,
                Background = bb
            };
            Grid.SetColumn(count, 1);
            inSp.Children.Add(count);

            b.Child = inSp;
            return b;
        }
    }
}
