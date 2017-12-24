using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using EveTools.DAO;

namespace EveTools.Tools
{
    public class GenerateHeader
    {
        private static Style tbStyle = new Style(typeof(TextBox));

        static GenerateHeader()
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

        public static Border createHeader(string name, long count, int offset, int level)
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
            b.BorderThickness = new Thickness(0.5);
            //b.Margin = new Thickness(offset, 0, 0, 0);
            Grid inSp = new Grid();
            inSp.Background = bb;
            inSp.HorizontalAlignment = HorizontalAlignment.Left;
            inSp.VerticalAlignment = VerticalAlignment.Top;
            ColumnDefinition cd = new ColumnDefinition();
            cd.Width = new GridLength(500 - offset);
            inSp.ColumnDefinitions.Add(cd);
            ColumnDefinition cd1 = new ColumnDefinition();
            cd1.Width = new GridLength(230);
            inSp.ColumnDefinitions.Add(cd1);

            TextBox tb1 = new TextBox();
            tb1.Style = tbStyle;
            tb1.Text = name;
            tb1.Background = ba;
            tb1.Foreground = fg;
            tb1.Height = 30;
            tb1.VerticalAlignment = VerticalAlignment.Center;
            tb1.HorizontalAlignment = HorizontalAlignment.Left;
            tb1.Width = 500-offset;
            tb1.FontSize = 20;
            tb1.Margin = new Thickness(2+offset, 0, 0, 0);
            Grid.SetColumn(tb1, 0);
            tb1.FontWeight = FontWeights.Bold;
            inSp.Children.Add(tb1);

            TextBox tb2 = new TextBox();
            tb2.Style = tbStyle;
            tb2.Text = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", count) + " ";
            tb2.Height = 30;
            tb2.VerticalAlignment = VerticalAlignment.Center;
            tb2.HorizontalAlignment = HorizontalAlignment.Left;
            tb2.Width = 230;
            tb2.FontSize = 20;
            tb2.FontWeight = FontWeights.Bold;
            Grid.SetColumn(tb2, 1);
            tb2.TextAlignment = TextAlignment.Right;
            tb1.Margin = new Thickness(0, 0, 2, 0);
            tb2.Background = bb;
            tb2.Foreground = fg;
            inSp.Children.Add(tb2);
            b.Child = inSp;
            return b;
        }

        public static Border createColumn(string name, long count, int offset, int level)
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
            b.BorderThickness = new Thickness(0.5);
            b.Margin = new Thickness(offset, 0, 2, 0);
            Grid inSp = new Grid();
            inSp.Background = bb;
            inSp.HorizontalAlignment = HorizontalAlignment.Left;
            inSp.VerticalAlignment = VerticalAlignment.Top;
            ColumnDefinition cd = new ColumnDefinition();
            cd.Width = new GridLength(500 - ((offset * level)-5));
            inSp.ColumnDefinitions.Add(cd);
            ColumnDefinition cd1 = new ColumnDefinition();
            cd1.Width = new GridLength(230);
            inSp.ColumnDefinitions.Add(cd1);

            TextBox tb1 = new TextBox();
            tb1.Style = tbStyle;
            tb1.Text = name;
            tb1.Background = ba;
            tb1.Foreground = fg;
            tb1.Height = 30;
            tb1.VerticalAlignment = VerticalAlignment.Center;
            tb1.HorizontalAlignment = HorizontalAlignment.Left;
            tb1.Width = 500 - ((offset * level) - 5);
            tb1.FontSize = 20;
            tb1.Margin = new Thickness(2 + offset, 0, 0, 0);
            Grid.SetColumn(tb1, 0);
            tb1.FontWeight = FontWeights.Bold;
            inSp.Children.Add(tb1);

            TextBox tb2 = new TextBox();
            tb2.Style = tbStyle;
            tb2.Text = string.Format(CultureInfo.GetCultureInfo("en-US").NumberFormat, "{0:0,0}", count) + " ";
            tb2.Height = 30;
            tb2.VerticalAlignment = VerticalAlignment.Center;
            tb2.HorizontalAlignment = HorizontalAlignment.Left;
            tb2.Width = 230;
            tb2.FontSize = 20;
            tb2.FontWeight = FontWeights.Bold;
            Grid.SetColumn(tb2, 1);
            tb2.TextAlignment = TextAlignment.Right;
            tb1.Margin = new Thickness(0, 0, 2, 0);
            tb2.Background = bb;
            tb2.Foreground = fg;
            inSp.Children.Add(tb2);
            b.Child = inSp;
            return b;
        }
    }
}
