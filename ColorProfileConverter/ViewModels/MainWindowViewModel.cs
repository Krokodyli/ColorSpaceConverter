using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ColorProfileConverter.Commands;

using ColorProfileConverter.Models;

namespace ColorProfileConverter.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private ColorProfile sourceColorProfile, targetColorProfile;

        private Bitmap sourceImage, targetImage;
        public ICommand LoadImageCommand { get; set; }
        public ICommand SaveImageCommand { get; set; }
        public ICommand GenerateTargetImageCommand { get; set; }

        public MainWindowViewModel()
        {
            SourceColorProfile = new ColorProfile();
            TargetColorProfile = new ColorProfile();
            LoadImageCommand = new LoadImageCommand(this);
            SaveImageCommand = new SaveImageCommand(this);
            GenerateTargetImageCommand = new GenerateTargetImageCommand(this);

            SourceColorProfile.Gamma = 2.2;
            SourceColorProfile.WhiteX = 0.3127;
            SourceColorProfile.WhiteY = 0.329;
            SourceColorProfile.RedX = 0.64;
            SourceColorProfile.RedY = 0.33;
            SourceColorProfile.GreenX = 0.3;
            SourceColorProfile.GreenY = 0.6;
            SourceColorProfile.BlueX = 0.15;
            SourceColorProfile.BlueY = 0.06;

            TargetColorProfile.Gamma = 1.2;
            TargetColorProfile.WhiteX = 0.3457;
            TargetColorProfile.WhiteY = 0.3585;
            TargetColorProfile.RedX = 0.7347;
            TargetColorProfile.RedY = 0.2653;
            TargetColorProfile.GreenX = 0.1152;
            TargetColorProfile.GreenY = 0.8264;
            TargetColorProfile.BlueX = 0.1566;
            TargetColorProfile.BlueY = 0.0177;
        }

        public ColorProfile SourceColorProfile
        {
            get => sourceColorProfile;
            set { sourceColorProfile = value; OnPropertyChanged(); }
        }
        public ColorProfile TargetColorProfile
        {
            get => targetColorProfile;
            set { targetColorProfile = value; OnPropertyChanged(); }
        }

        public Bitmap SourceImage 
        {
            get => sourceImage;
            set { sourceImage = value; OnPropertyChanged(); }
        }

        public Bitmap TargetImage
        {
            get => targetImage;
            set { targetImage = value; OnPropertyChanged(); }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
