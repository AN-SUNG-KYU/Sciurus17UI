using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciurus17.MyLibrary.Math
{
    public class Matrix
    {
        private double[,] matrix;
        public int Row { get; }
        public int Column { get; }
        public int Size { get { return Row * Column; } }
        public double this[int row, int col]
        {

            set { matrix[row, col] = value; }
            get { return matrix[row, col]; }
        }

        public Matrix T { get { return Transposed(); } }



        public Matrix(double[,] array2D)
        {
            matrix = array2D;
            Row = array2D.GetLength(0);
            Column = array2D.GetLength(1);
        }
        public Matrix(int row, int col)
        {
            matrix = new double[row, col];
            Row = row;
            Column = col;
        }

        public static Matrix operator +(Matrix left, Matrix right)
        {
            if (left.Row != right.Row | left.Column != right.Column) throw new ArithmeticException();

            var mat = new double[left.Row, left.Column];
            for (int i = 0; i < left.Row; i++)
            {
                for (int j = 0; j < left.Column; j++)
                {
                    mat[i, j] = left[i, j] + right[i, j];
                }
            }
            return new Matrix(mat);
        }

        public static Matrix operator -(Matrix left, Matrix right)
        {
            if (left.Row != right.Row | left.Column != right.Column) throw new ArithmeticException();

            var mat = new double[left.Row, left.Column];
            for (int i = 0; i < left.Row; i++)
            {
                for (int j = 0; j < left.Column; j++)
                {
                    mat[i, j] = left[i, j] - right[i, j];
                }
            }
            return new Matrix(mat);
        }

        public static Matrix operator *(Matrix left, Matrix right)
        {
            if (left.Column != right.Row) throw new ArithmeticException();

            var mat = new double[left.Row, right.Column];
            for (int i = 0; i < left.Row; i++)
            {
                for (int j = 0; j < right.Column; j++)
                {
                    for (int k = 0; k < left.Column; k++) mat[i, j] += left[i, k] * right[k, j];
                }
            }
            return new Matrix(mat);
        }

        public static Matrix Identity(int dimension)
        {
            var mat = new double[dimension, dimension];
            for (int i = 0; i < dimension; i++) mat[i, i] = 1.0;
            return new Matrix(mat);
        }

        public void Show()
        {
            Console.Write("[");
            for (int i = 0; i < Row; i++)
            {
                Console.Write("[");
                for (int j = 0; j < Column; j++) Console.Write("{0},", this[i, j]);
                if (i != Row - 1) Console.Write("]\n");
            }
            Console.Write("]]\n");
        }

        public Matrix Transposed()
        {
            var mat = new double[Column, Row];
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Column; j++) mat[j, i] = matrix[i, j];
            }
            return new Matrix(mat);
        }

    }
}
