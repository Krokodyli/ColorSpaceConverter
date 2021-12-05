using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
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

        public Bitmap Convert2(Bitmap sourceImage)
        {
            Bitmap targetImage = (Bitmap)sourceImage.Clone();
            unsafe
            {
                var imgRectangle = new System.Drawing.Rectangle(0, 0, targetImage.Width,
                    targetImage.Height);
                BitmapData imgData = targetImage.LockBits(imgRectangle, ImageLockMode.ReadWrite,
                    targetImage.PixelFormat);
                int bytesPerPixel = Bitmap.GetPixelFormatSize(targetImage.PixelFormat) / 8;
                int height = imgData.Height;
                int widthInBytes = imgData.Width * bytesPerPixel;
                byte *ptrFirstPixel = (byte*)imgData.Scan0;
                Parallel.For(0, height, y =>
                {
                    byte* currentLine = ptrFirstPixel + y * imgData.Stride;
                    for(int x = 0; x < widthInBytes; x += bytesPerPixel)
                    {
                        var color = Color.FromArgb(
                            currentLine[x],
                            currentLine[x + 1],
                            currentLine[x + 2]);

                        var newColor = converter.Convert(color);

                        currentLine[x] = (byte)newColor.R;
                        currentLine[x+1] = (byte)newColor.G;
                        currentLine[x+2] = (byte)newColor.B;
                    }
                });
                targetImage.UnlockBits(imgData);
            }
            return targetImage;
        }

        public Bitmap Convert(Bitmap sourceImage)
        {
            return Convert2(sourceImage);
            /*
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
            */
        }
    }
}
