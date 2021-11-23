using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using ColorProfileConverter.Models;

namespace ColorProfileConverter
{
    /// <summary>
    /// Interaction logic for ColorProfilePicker.xaml
    /// </summary>
    public partial class ColorProfilePicker : UserControl
    {   
        public ColorProfilePicker()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label",
                typeof(String),
                typeof(ColorProfilePicker));

        [Bindable(true)]
        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);

        }

        public static readonly DependencyProperty ColorProfileProperty =
            DependencyProperty.Register("ColorProfile",
                typeof(ColorProfile),
                typeof(ColorProfilePicker));

        [Bindable(true)]
        public ColorProfile ColorProfile
        {
            get => (ColorProfile)GetValue(ColorProfileProperty);
            set => SetValue(ColorProfileProperty, value);
        }

    }
}
