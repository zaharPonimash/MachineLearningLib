﻿using MachineLearningLib;
using ru.aryumin.MachineLearningLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples.cs
{
    class Program
    {
        static double[] xVals = { 1.0, 2.0, 3.0, 4.0, 5.0 };
        static double[] yVals = { 1.0, 2.0, 2.8, 3.75, 4.95 };

        static void Main(string[] args)
        {
            //getting matrix for parabola regression (order n)
            Matrix mParabolaRegression = Matrix.GetMatrixFromTXT("data\\parabola_regression.txt", '\t');
            NOrderSimpleParabolaRegression nospr = new NOrderSimpleParabolaRegression();
            Matrix z = nospr.GetRegressionCoefficients(mParabolaRegression, 2);
            double yVal = nospr.GetYForVectorX(z, 84.0);


            //getting matrix from file
            Matrix mFromFile = Matrix.GetMatrixFromTXT("data\\regress_data.txt", '\t');

            //Muliple Linear Regression
            var mlr = new MultipleLinearRegression();
            int[] rows = Enumerable.Range(0, mFromFile.matrixBase.GetLength(0))
                        .Select(i => i)
                        .ToArray();
            Matrix bVector = mlr.GetBCoefficientsForMatrix(mFromFile.GetMatrixPart(rows, new int[] { 1, 2, 3 }), 
                mFromFile.GetMatrixPart(rows, new int[] { 0 }));
            //getting y for x0 = 1, x2 = 81, x3 = 259
            double y = mlr.GetYForVectorXs(bVector, new int[] { 1, 81, 259 });

            Console.WriteLine(mFromFile + "\n");
            Console.WriteLine("y for x0 = 1, x1 = 81, x2 = 259 = " + y);

            double[,] a = new double[3,2];
            a[0, 0] = 2;
            a[0, 1] = 6;
            a[1, 0] = 7;
            a[1, 1] = 3;
            a[2, 0] = 5;
            a[2, 1] = 2;

            double[,] b = new double[2, 3];
            b[0, 0] = 1;
            b[0, 1] = 7;
            b[0, 2] = 3;
            b[1, 0] = 2;
            b[1, 1] = 5;
            b[1, 2] = 6;

            //double[,] sqvArr = new double[2, 2];
            //sqvArr[0, 0] = 1;
            //sqvArr[0, 1] = 7;
            //sqvArr[1, 0] = 3;
            //sqvArr[1, 1] = 2;

            double[,] sqvArr = new double[3, 3];
            sqvArr[0, 0] = -1;
            sqvArr[0, 1] = -2;
            sqvArr[0, 2] = 2;
            sqvArr[1, 0] = 2;
            sqvArr[1, 1] = 1;
            sqvArr[1, 2] = 1;
            sqvArr[2, 0] = 3;
            sqvArr[2, 1] = 4;
            sqvArr[2, 2] = 5;

            Matrix matrixA = new Matrix(a);
            Matrix matrixB = new Matrix(b);
            Matrix matrixSkv = new Matrix(sqvArr);

            //Invert matrix
            Matrix invertedSkv = matrixSkv.Invert();

            //determinant of matrix
            double determinant = matrixSkv.GetDeterminant();

            //Transporate matrix
            Console.WriteLine(matrixA);
            Console.WriteLine("\n");
            matrixA = matrixA.Transpose();
            Console.WriteLine(matrixA);

            //Multipling to scalar
            Console.WriteLine(matrixA);
            matrixA = matrixA.MultiplyToScalar(10);
            Console.WriteLine(matrixA);

            Matrix matrixC = Matrix.MultiplyMatrices(matrixA, matrixB);
            Console.WriteLine("Multiply marix\n\n{0}\n\nto matrix\n\n{1}\n\nIt gives:\n\n{2}\n\n"
                , matrixA, matrixB, matrixC);



            //Calculate simple linear regression
            SimpleLinearRegression slr = new SimpleLinearRegression(xVals, yVals);
            double yPredicted = slr.PredictY(4);
            Console.WriteLine(yPredicted);

            Console.ReadLine();
        }
    }
}
