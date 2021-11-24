using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;
using System.Xml.Serialization;
using ColorProfileConverter.Commands;
using ColorProfileConverter.Data;
using ColorProfileConverter.Models;

namespace ColorProfileConverter.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {

        private ColorProfile sourceColorProfile, targetColorProfile;
        private Bitmap sourceImage, targetImage;
        private const String exampleBitmapPath = @"examplePhoto.jpg";
        private const String PredefinedColorProfilesConfigFilePath = @"predefinedProfiles.xml";
        public Dictionary<string, ColorProfile> PredefinedColorProfiles { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

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

            loadPredefinedProfiles();
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

        private void loadPredefinedProfiles()
        {
            var predefinedProfiles = new PredefinedColorProfiles();
            var path = PredefinedColorProfilesConfigFilePath;
            try
            {
                using (var reader = XmlReader.Create(path))
                {
                    var xmlSerializer = new XmlSerializer(typeof(PredefinedColorProfiles));
                    predefinedProfiles = (PredefinedColorProfiles)xmlSerializer.Deserialize(reader);
                }
            }
            catch (Exception)
            {
                this.PredefinedColorProfiles = new Dictionary<string, ColorProfile>();
                return;
            }
            this.PredefinedColorProfiles = predefinedProfiles.GetPredefinedProfiles();
        }

        public void LoadExampleBitmap()
        {
            try
            {
                SourceImage = new Bitmap(exampleBitmapPath);
            }
            catch (Exception)
            {
            }
        }
    }
}
