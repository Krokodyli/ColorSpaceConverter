using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorProfileConverter.Models
{
    public class ColorMatrix : IEquatable<ColorMatrix>
    {
        private double[,] matrix;

        public ColorMatrix()
        {
            matrix = new double[3, 3];
            for(int r = 0; r < 3; r++)
                for (int c = 0; c < 3; c++)
                    matrix[r, c] = 0.0;
        }

        public ColorMatrix(double[,] matrixToClone)
        {
            if (matrixToClone == null
                || matrixToClone.GetLength(0) != 3
                || matrixToClone.GetLength(1) != 3
                || matrixToClone.Length != 9)
                throw new ArgumentException();

            matrix = new double[3, 3];
            for (int r = 0; r < 3; r++)
                for (int c = 0; c < 3; c++)
                    matrix[r, c] = matrixToClone[r, c];
        }

        public ColorMatrix(ColorMatrix matrixToClone)
            : this(matrixToClone.matrix) { }

        public ColorMatrix(ColorProfile profile)
        {
            matrix = new double[3, 3];
            matrix[0, 0] = profile.RedX;
            matrix[1, 0] = profile.RedY;
            matrix[0, 1] = profile.GreenX;
            matrix[1, 1] = profile.GreenY;
            matrix[0, 2] = profile.BlueX;
            matrix[1, 2] = profile.BlueY;
            for (int c = 0; c < 3; c++)
                matrix[2, c] = 1 - matrix[0, c] - matrix[1, c];
        }

        public double GetDeterminant()
        {
            return matrix[0, 0] * get2x2Determinant(0, 0)
                 - matrix[0, 1] * get2x2Determinant(0, 1) 
                 + matrix[0, 2] * get2x2Determinant(0, 2);
        }

        public bool IsInvertible() => GetDeterminant() != 0;

        public ColorMatrix GetInverse()
        {
            if (!IsInvertible())
                throw new DivideByZeroException();

            var inverse = new ColorMatrix();
            var det = GetDeterminant();
            
            for(int r = 0; r < 3; r++)
            {
                for(int c = 0; c < 3; c++)
                {
                    inverse[r, c] = get2x2Determinant(c, r) / det;
                    if ((r + c) % 2 == 1)
                        inverse[r, c] *= -1;
                }
            }

            return inverse;
        }

        public static double[] operator*(ColorMatrix matrix, double[] v)
        {
            if (v.Length != 3)
                throw new ArgumentException();

            var newV = new double[3] { 0f, 0f, 0f };
            for (int r = 0; r < 3; r++)
                for (int i = 0; i < 3; i++)
                    newV[r] += matrix[r, i] * v[i];

            return newV;
        }

        public void multiplyMatrixColumnsByCoefficients(double[] v)
        {
            if (v.Length != 3)
                throw new ArgumentException();

            for (int c = 0; c < 3; c++)
                for (int i = 0; i < 3; i++)
                    matrix[i, c] *= v[c];
        }


        public double this[int r, int c]
        {
            get => matrix[r, c];
            set => matrix[r, c] = value;
        }

        private double get2x2Determinant(int r, int c)
        {
            int r1 = (r + 2) % 3, r2 = (r + 1) % 3;
            int c1 = (c + 2) % 3, c2 = (c + 1) % 3;
            int tmp;
            if(r1 > r2)
            {
                tmp = r1;
                r1 = r2;
                r2 = tmp;
            }
            if(c1 > c2)
            {
                tmp = c1;
                c1 = c2;
                c2 = tmp;
            }
            return matrix[r1, c1] * matrix[r2, c2] - matrix[r1, c2] * matrix[r2, c1];
        }

        public bool Equals(ColorMatrix other)
        {
            var tolerance = 0.0001f;
            for (int r = 0; r < 3; r++)
                for (int c = 0; c < 3; c++)
                    if (Math.Abs(matrix[r, c]-other.matrix[r, c]) > tolerance)
                        return false;
            return true;
        }
        public override bool Equals(object obj)
        {
            return this.Equals(obj as ColorMatrix);
        }

        public override int GetHashCode()
        {
            int hashCode = matrix[0, 0].GetHashCode();
            for(int i = 1; i < 9; i++)
            {
                hashCode *= 17;
                hashCode += matrix[i / 3, i % 3].GetHashCode();
            }
            return hashCode;
        }
    }
}
