using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorProfileConverter.ViewModels
{
    internal class MainWindowViewModel
    {
        public ColorProfile SourceColorProfile { get; set; }
        public ColorProfile TargetColorProfile { get; set; }

        public Bitmap SourceImage { get; set; }
        public Bitmap TargetImage { get; set; }

        public MainWindowViewModel()
        {
            SourceColorProfile = new ColorProfile();
            TargetColorProfile = new ColorProfile();
        }
    }
}
