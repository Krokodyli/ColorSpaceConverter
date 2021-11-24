using Microsoft.VisualStudio.TestTools.UnitTesting;
using ColorProfileConverter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Moq;

namespace ColorProfileConverter.Models.Tests
{
    [TestClass()]
    public class BitmapConverterTests
    {
        [TestMethod()]
        public void ConvertTest()
        {
            var whiteColor = Color.FromArgb(255, 255, 255);
            var blackColor = Color.FromArgb(0, 0, 0);

            var colorConverter = new Mock<ColorConverter>();
            colorConverter
                .Setup(conv => conv.Convert(blackColor))
                .Returns(whiteColor);

            var bitmap = generateBitmap(7, 13, blackColor);
            var bitmapConverter = new BitmapConverter(colorConverter.Object);


            var outputBitmap = bitmapConverter.Convert(bitmap);

            Assert.AreEqual(bitmap.Width, outputBitmap.Width);
            Assert.AreEqual(bitmap.Height, outputBitmap.Height);

            for (int y = 0; y < outputBitmap.Height; y++)
                for (int x = 0; x < outputBitmap.Width; x++)
                    Assert.AreEqual(outputBitmap.GetPixel(x, y), whiteColor);
        }

        private Bitmap generateBitmap(int width, int height, Color color)
        {
            var bitmap = new Bitmap(width, height);
            for (int y = 0; y < bitmap.Height; y++)
                for (int x = 0; x < bitmap.Width; x++)
                    bitmap.SetPixel(x, y, color);
            return bitmap;
        }
    }

}