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
