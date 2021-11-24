using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ColorProfileConverter.Models;

namespace ColorProfileConverter.Models
{
    public class ColorProfileColorConverter : ColorConverter
    {
        private double sourceGamma, targetGamma;

        private ColorMatrix transformationMatrixFromSource;
        private ColorMatrix transformationMatrixToTarget;

        public ColorProfileColorConverter(ColorProfile sourceProfile,
                                          ColorProfile targetProfile)
        {
            sourceGamma = sourceProfile.Gamma;
            targetGamma = targetProfile.Gamma;
            transformationMatrixFromSource
                = GenerateTransformationMatrix(sourceProfile);
            transformationMatrixToTarget
                = GenerateTransformationMatrix(targetProfile).GetInverse();
        }

        public Color Convert(Color color)
        {
            var xyzColor = ConvertSourceProfileColorToXYZColor(color);
            return ConvertXYZColorToTargetProfile(xyzColor);
        }

        public double[] ConvertSourceProfileColorToXYZColor(Color color)
        {
            double[] colorVector = colorToVectorRGB(color);

            colorVector = applyGammaTransformation(colorVector, sourceGamma);

            colorVector = applyTransformationMatrix(colorVector, 
                transformationMatrixFromSource);

            return colorVector;
        }

        public Color ConvertXYZColorToTargetProfile(double[] colorVector)
        {
            colorVector = applyTransformationMatrix(colorVector,
                transformationMatrixToTarget);

            colorVector = applyGammaTransformation(colorVector, 1 / targetGamma);

            return vectorRGB2Color(colorVector);
        }

        public double[] applyTransformationMatrix(double[] colorVector, 
                                                  ColorMatrix transformationMatrix)
        {
            return normalizeColorVector(transformationMatrix * colorVector);
        }

        private double[] applyGammaTransformation(double[] colorVector, double gamma)
        {
            for (int i = 0; i < 3; i++)
                colorVector[i] = Math.Pow(colorVector[i], gamma);
            return colorVector;
        }

        private ColorMatrix GenerateTransformationMatrix(ColorProfile profile)
        {
            double[] whitePoint = new double[3]
            {
                profile.WhiteX / profile.WhiteY,                        // Xw
                1.0,                                                    // Yw
                (1 - profile.WhiteX - profile.WhiteY)/profile.WhiteY    // Zw
            };

            var colorMatrix = new ColorMatrix(profile);
            var inverse = colorMatrix.GetInverse();
            var S = inverse * whitePoint;

            colorMatrix.multiplyMatrixColumnsByCoefficients(S);

            return colorMatrix;
        }

        private double[] colorToVectorRGB(Color color)
        {
            return new double[]
            {
                (double)color.R/255.0,
                (double)color.G/255.0,
                (double)color.B/255.0,
            };
        }
        private Color vectorRGB2Color(double[] color)
        {
            if (color.Length != 3)
                throw new ArgumentException();
            return Color.FromArgb(
                (int)(color[0] * 255),
                (int)(color[1] * 255),
                (int)(color[2] * 255)
            );
        }

        private double[] normalizeColorVector(double[] colorVector)
        {
            for (int i = 0; i < 3; i++)
                colorVector[i] = Math.Max(Math.Min(colorVector[i], 1.0), 0.0);
            return colorVector;
        }
    }

}
