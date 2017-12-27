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

        public ManufactureDisplayModel(Blueprint blueprint, int offset, int level)
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
                Foreground = new SolidColorBrush(Colors.Black),
                HorizontalAlignment = HorizontalAlignment.Left
            };
            skills.Background = new SolidColorBrush(Colors.Aqua);
            skills.HorizontalAlignment = HorizontalAlignment.Left;
            skills.Width = 760;
            skills.Header = tb;
        }

        private void createViews()
        {
            createOverview();
        }

        private void createOverview()
        {
            overview = new Expander();
            TextBox tb = new TextBox
            {
                Text = "Overview",
                Style = tbStyle,
                Width = 720 - (offset * (level - 1)),
                FontSize = 20,
                Foreground = new SolidColorBrush(Colors.Black),
                HorizontalAlignment = HorizontalAlignment.Left
            };
            overview.Background = new SolidColorBrush(Colors.Aqua);
            overview.Header = tb;
            overview.Width = 750 - (offset * (level - 1));
            overview.HorizontalAlignment = HorizontalAlignment.Left;
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Vertical;
            overview.Content = sp;
        }
    }
}
