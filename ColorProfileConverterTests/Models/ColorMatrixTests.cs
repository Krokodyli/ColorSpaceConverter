using Microsoft.VisualStudio.TestTools.UnitTesting;
using ColorProfileConverter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ColorProfileConverter.Models.Tests
{
    [TestClass()]
    public class ColorMatrixTests
    {
        private ColorProfile getColorProfile1()
        {
            var colorProfile1 = new ColorProfile();
            colorProfile1.RedX = 0.2;
            colorProfile1.GreenX = 0.5;
            colorProfile1.BlueX = 0.4;
            colorProfile1.RedY = 0.3;
            colorProfile1.GreenY = 0.1;
            colorProfile1.BlueY = 0.5;
            return colorProfile1;
        }

        private ColorMatrix getMatrix1()
            => new ColorMatrix(
                new double[,] {
                    { 0.2, 0.5, 0.4  },
                    { 0.3, 0.1, 0.5  },
                    { 0.5,  0.4, 0.1 }
                });

        private double getDeterminant1() => 0.1;

        private ColorMatrix getInverse1()
            => new ColorMatrix(
                new double[,]
                {
                    { -1.9, 1.1, 2.1 },
                    { 2.2, -1.8, 0.2 },
                    { 0.7,  1.7, -1.3 }
                });

        private double[] getCoefficients1()
            => new double[] { 1.0, 2.0, 3.0 };

        private ColorMatrix getMultipliedByCoefficients1()
            => new ColorMatrix(
                new double[,]
                {
                    { 0.2, 1.0, 1.2 },
                    { 0.3, 0.2, 1.5 },
                    { 0.5, 0.8, 0.3 }
                });

        private ColorProfile getColorProfile2()
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
            return profile;
        }

        private ColorMatrix getInverse2()
            => new ColorMatrix(
                new double[,]
                {
                    {  2.088, -0.991, -0.321 },
                    { -1.155,  2.236,  0.050 },
                    {  0.067, -0.245,  1.272 }
                });

        private ColorMatrix getMultipliedByCoefficients2()
            => new ColorMatrix(
                new double[,]
                {
                    {  0.64,  0.6, 0.45 },
                    {  0.33,  1.2, 0.18 },
                    {  0.03,  0.2, 2.37 }
                });




        private double delta = 0.0001;
        private double smallDelta = 0.001;

        [TestMethod()]
        public void colorProfileConstructorTest1()
        {
            var matrixActual = new ColorMatrix(getColorProfile1());
            var matrixExpected = getMatrix1();
            Assert.AreEqual(matrixExpected, matrixActual);
        }

        [TestMethod()]
        public void colorProfileSecondConstructorTest1()
        {
            var matrixActual = new ColorMatrix(getMatrix1());
            var matrixExpected = getMatrix1();
            Assert.AreEqual(matrixExpected, matrixActual);
        }


        [TestMethod()]
        public void GetDeterminantTest()
        {
            var matrix = getMatrix1();
            var det = getDeterminant1();
            Assert.AreEqual(det, matrix.GetDeterminant(), delta);
        }

        [TestMethod()]
        public void IsInvertibleTest()
        {
            Assert.AreEqual(true, getMatrix1().IsInvertible());
        }

        [TestMethod()]
        public void GetInverseTest()
        {
            var matrixActual = getMatrix1().GetInverse();
            var matrixExpected = getInverse1();
            Assert.IsNotNull(matrixActual);
            Assert.AreEqual(matrixExpected, matrixActual);
        }

        [TestMethod()]
        public void GetInverseTest2()
        {
            var matrixActual = new ColorMatrix(getColorProfile2()).GetInverse();
            var matrixExpected = getInverse2();
            Assert.IsNotNull(matrixActual);
            for (int i = 0; i < 9; i++)
                Assert.AreEqual(matrixExpected[i / 3, i % 3],
                                matrixActual[i / 3, i % 3],
                                smallDelta);
        }


        [TestMethod()]
        public void multiplyVerticalVectorTest()
        {
            double[] v = new double[3] { 0.5, 0.2, 0.4 };
            double[] expected = new double[3] { 0.36, 0.37, 0.37 };
            ColorMatrix matrix = getMatrix1();
            v = matrix * v;
            for (int i = 0; i < 3; i++)
                Assert.AreEqual(expected[i], v[i], delta);
        }

        [TestMethod()]
        public void multiplyMatrixColumnsByCoefficientsTest()
        {
            var matrix = getMatrix1();
            var expected = getMultipliedByCoefficients1();
            var coefficients = getCoefficients1();
            matrix.multiplyMatrixColumnsByCoefficients(coefficients);
            Assert.AreEqual(expected, matrix);
        }

        [TestMethod()]
        public void multiplyMatrixColumnsByCoefficientsTest2()
        {
            var actual = new ColorMatrix(getColorProfile2());
            actual.multiplyMatrixColumnsByCoefficients(getCoefficients1());
            var expected = getMultipliedByCoefficients2();
            Assert.AreEqual(expected, actual);
        }


        [TestMethod()]
        public void EqualsTest()
        {
            var matrix = getMatrix1();
            var changedMatrices = new ColorMatrix[9];
            Assert.IsTrue(matrix.Equals(matrix));
            for(int i = 0; i < 9; i++)
            {
                changedMatrices[i] = getMatrix1();
                changedMatrices[i][i / 3, i % 3] = 1000;
                Assert.IsFalse(matrix.Equals(changedMatrices[i]));
            }
            changedMatrices[0][0, 0] = matrix[0, 0];
            Assert.IsTrue(matrix.Equals(changedMatrices[0]));
        }
    }
}