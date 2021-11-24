using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class ColorProfilePicker 
        : UserControl, INotifyPropertyChanged
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
            set {
                SetValue(ColorProfileProperty, value);
                UpdateColorProfile();
            }
        }

        public static readonly DependencyProperty PredefinedColorProfilesProperty =
            DependencyProperty.Register("PredefinedColorProfiles",
                typeof(Dictionary<String, ColorProfile>),
                typeof(ColorProfilePicker));

        [Bindable(true)]
        public Dictionary<String, ColorProfile> PredefinedColorProfiles
        {
            get => (Dictionary<String, ColorProfile>)GetValue(PredefinedColorProfilesProperty);
            set {
                SetValue(PredefinedColorProfilesProperty, value);

                if (value != null && value.Count > 0)
                    ChosenPredefinedColorProfile = value.Keys.First();

                UpdateColorProfile();
            }
        }

        public String chosenPredefinedColorProfile;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChange([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public String ChosenPredefinedColorProfile 
        {
            get => chosenPredefinedColorProfile;
            set
            {
                chosenPredefinedColorProfile = value;
                UpdateColorProfile();
                NotifyPropertyChange(nameof(ChosenPredefinedColorProfile));
            }
        }

        public void UpdateColorProfile()
        {
            if (ColorProfile != null && PredefinedColorProfiles != null)
            {
                if (chosenPredefinedColorProfile == null
                    && PredefinedColorProfiles.Count > 0)
                {
                    chosenPredefinedColorProfile = PredefinedColorProfiles.Keys.First();
                    NotifyPropertyChange(nameof(ChosenPredefinedColorProfile));
                }
                if (PredefinedColorProfiles.ContainsKey(chosenPredefinedColorProfile))
                {
                    var chosenColorProfile = PredefinedColorProfiles[chosenPredefinedColorProfile];
                    ColorProfile.SetSamePropertiesAs(chosenColorProfile);
                }
            }


        }
    }
}
