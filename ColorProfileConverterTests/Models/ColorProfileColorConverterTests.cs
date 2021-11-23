using Microsoft.VisualStudio.TestTools.UnitTesting;
using ColorProfileConverter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ColorProfileConverter.Models.Tests
{
    [TestClass()]
    public class ColorProfileColorConverterTests
    {
        [TestMethod()]
        public void GenerateTransformationMatrixTest()
        {
            var profile = get_SRGB_D65_profile();
            var converter = new ColorProfileColorConverter(profile, profile);
            var actual = converter.GenerateTransformationMatrix(profile);
            var expected = get_SRGB_D65_Transformation_Matrix();
            for (int i = 0; i < 9; i++)
                Assert.AreEqual(expected[i / 3, i % 3], actual[i / 3, i % 3], 0.001);
        }


        private ColorProfile getWideGamut()
        {
            var profile = new ColorProfile();
            profile.RedX = 0.7347;
            profile.RedY = 0.2653;
            profile.GreenX = 0.1152;
            profile.GreenY = 0.8264;
            profile.BlueX = 0.1566;
            profile.BlueY = 0.0177;
            profile.WhiteX = 0.3457;
            profile.WhiteY = 0.3585;
            profile.Gamma = 2.2;
            return profile;
        }

        private ColorProfile get_SRGB_D65_profile()
        {
            var profile = new ColorProfile();
            profile.RedX = 0.64;
            profile.RedY = 0.33;
            profile.GreenX = 0.3;
            profile.GreenY = 0.6;
            profile.BlueX = 0.15;
            profile.BlueY = 0.06;
            profile.WhiteX = 0.3127;
            profile.WhiteY = 0.3290;
            profile.Gamma = 2.2;
            return profile;
        }

        private ColorMatrix get_SRGB_D65_Transformation_Matrix()
            => new ColorMatrix(new double[3, 3]
               {
                { 0.4124,  0.3576,  0.1805 },
                { 0.2126,  0.7152,  0.0722 },
                { 0.0193,  0.1192,  0.9505 }
               });

        [TestMethod()]
        public void colorConvertToTargetTest()
        {
            var profile = get_SRGB_D65_profile();
            var converter = new ColorProfileColorConverter(profile, profile);

            var xyzVector = new double[3]
            {
                0.2990,
                0.1600,
                0.0656
            };

            var expected = Color.FromArgb(215, 35, 67);
            var actual = converter.colorConvertToTarget(xyzVector);

            Assert.AreEqual(expected, actual);
        }
    }
}