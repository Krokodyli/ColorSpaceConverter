using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorProfileConverter.ViewModels;

namespace ColorProfileConverter.Models
{
    public class BitmapConverter
    {
        ColorConverter converter;
       
        public BitmapConverter(ColorConverter converter)
        {
            this.converter = converter;
        }

        public Bitmap Convert(Bitmap sourceImage)
        {
            Bitmap targetImage = new Bitmap(sourceImage.Width,
                                            sourceImage.Height);

            for(int y = 0; y < sourceImage.Height; y++)
            {
                for(int x = 0; x < sourceImage.Width; x++)
                {
                    var oldColor = sourceImage.GetPixel(x, y);
                    var newColor = converter.Convert(oldColor);
                    targetImage.SetPixel(x, y, newColor);
                }
            }

            return targetImage;
        }
    }
}
