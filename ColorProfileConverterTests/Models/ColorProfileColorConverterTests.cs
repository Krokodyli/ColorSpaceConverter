using Microsoft.VisualStudio.TestTools.UnitTesting;
using ColorProfileConverter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.VisualStudio.TestPlatform.Utilities;

namespace ColorProfileConverter.Models.Tests
{
    [TestClass()]
    public class ColorProfileColorConverterTests
    {

        [TestMethod()]
        public void SameProfileColorConversionWithNoGamma()
        {
            var color = Color.FromArgb(184, 213, 21);
            var profile = getSRPGProfile();
            profile.Gamma = 1;
            var converter = new ColorProfileColorConverter(profile, profile);
            var convertedColor = converter.Convert(color);

            Assert.AreEqual(color, convertedColor);
        }

        [TestMethod()]
        public void SameProfileColorConversionWithGammaEqual2()
        {
            var color = Color.FromArgb(125, 125, 125);
            var profile = getSRPGProfile();
            profile.Gamma = 2;
            var converter = new ColorProfileColorConverter(profile, profile);
            var convertedColor = converter.Convert(color);

            Assert.AreEqual(color, convertedColor);
        }

        [TestMethod()]
        public void SameProfileColorConversionWithGammaEqualOneThird()
        {
            var tolerance = 3;
            var color = Color.FromArgb(200, 125, 124);
            var profile = getSRPGProfile();
            profile.Gamma = 0.33;
            var converter = new ColorProfileColorConverter(profile, profile);
            var convertedColor = converter.Convert(color);

            var error = Math.Abs(color.R - convertedColor.R);
            error += Math.Abs(color.G - convertedColor.G);
            error += Math.Abs(color.B - convertedColor.B);

            ConsoleOutput.Instance.WriteLine($"Error = {error}", OutputLevel.Information);

            Assert.IsTrue(error <= tolerance, $"Error is too big (Error={error})");
        }

        [TestMethod()]
        public void TestFromProjectExample()
        {
            var color = new double[3] { 0.299, 0.16, 0.0656 };
            var expectedColor = Color.FromArgb(215, 35, 67);
            var profile = getSRPGProfile();
            var converter = new ColorProfileColorConverter(profile, profile);
            var converterColor = converter.ConvertXYZColorToTargetProfile(color);
            Assert.AreEqual(expectedColor, converterColor);
        }

        private ColorProfile getWideGamutProfile()
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

        private ColorProfile getSRPGProfile()
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

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CtorShouldThrowExceptionWhenMatrixIsNotInvertible()
        {
            var profile = new ColorProfile();
            profile.Gamma = 2;
            profile.RedX = profile.BlueX = profile.GreenX = 0.1;
            profile.RedY = profile.BlueY = profile.GreenY = 0.3;

            var converter = new ColorProfileColorConverter(profile, profile);
        }
    }
}