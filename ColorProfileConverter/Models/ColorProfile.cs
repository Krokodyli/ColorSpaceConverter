using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ColorProfileConverter.Models
{
    public class ColorProfile : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private double redX, redY;
        private double greenX, greenY;
        private double blueX, blueY;
        private double whiteX, whiteY;
        private double gamma;

        public ColorProfile() { }

        public double RedX { get { return redX; } set { redX = value; OnPropertyChanged();  } }
        public double RedY { get { return redY; } set { redY = value; OnPropertyChanged();  } }
        public double GreenX { get { return greenX; } set { greenX = value; OnPropertyChanged();  } }
        public double GreenY { get { return greenY; } set { greenY = value; OnPropertyChanged();  } }
        public double BlueX { get { return blueX; } set { blueX = value; OnPropertyChanged();  } }
        public double BlueY { get { return blueY; } set { blueY = value; OnPropertyChanged();  } }
        public double WhiteX { get { return whiteX; } set { whiteX = value; OnPropertyChanged();  } }
        public double WhiteY { get { return whiteY; } set { whiteY = value; OnPropertyChanged();  } }
        public double Gamma { get { return gamma; } set { gamma = value; OnPropertyChanged();  } }


        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
