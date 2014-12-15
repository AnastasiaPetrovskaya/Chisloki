using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PetrovskayaMatrix
{
    class Matrix
    {
        double[,] matrix; //сама матрицы 
        //double determine = 1;
        double norm = 0; 

        // конструктор
        public Matrix(int size)
        {
            matrix = new double[size, size];
            //determine = 0;
        }

        public Matrix(double[,] readMatrix)
        {
            matrix = (double[,])readMatrix.Clone();
            //Determine();
            NormCalc();
        }
        // конструктор

        // свойства
        public double[,]  MatrixGetSet
        {
            get { return matrix;}
            set { matrix = value;}
        }
        public double Norm
        {
            get { return norm; }
        }

       /* public Matrix(int size1, int size2, int disp, bool symetric)
        {
            matrix = new double[size1,size2];
            matrix.GetLength(0) = size1;
            matrix.GetLength(0) = size2;
            Random x = new Random();
            ResizeArray<double>(ref matrix, matrix.GetLength(0), matrix.GetLength(0));
            if (symetric)
                for (int i = 0; i < matrix.GetLength(0); i++)
                    for (int j = 0; j < matrix.GetLength(0); j++)
                        matrix[i, j] = x.Next(-disp + 1, disp);
            else
                for (int i = 0; i < matrix.GetLength(0); i++)
                    for (int j = i; j < matrix.GetLength(0); j++)
                    {
                        matrix[i, j] = x.Next(-disp + 1, disp);
                        matrix[j, i] = matrix[i, j];
                    }
            determine = Determine();
        }*/

        private void NormCalc()//считаем норму матрицы
        {
            norm = 0;
            foreach (var element in matrix)
                norm += Math.Abs(element)*Math.Abs(element);
            norm = Math.Sqrt(norm);
            //for (int i = 0; i < matrix.GetLength(0); i++)
            //   for (int j = 0; j < matrix.GetLength(0); j++)
            //      norm += Math.Abs(matrix[i, j]);
        }

        

        public void SwapRows(int row1, int row2)
        {
            double help;
            for (int i = 0; i <= matrix.GetLength(0); i++)
            {
                help = matrix[row1, i];
                matrix[row1, i] = matrix[row2, i];
                matrix[row2, i] = help;
            }
        }

        public void SwapCols(int col1, int col2)
        {
            double help;
            for (int i = 0; i <= matrix.GetLength(0); i++)
            {
                help = matrix[i, col1];
                matrix[i, col1] = matrix[i, col2];
                matrix[i, col2] = help;
            }
        }

        public Matrix Transpose()// транспонирование матрицы
        {
            Matrix tranMatrix = new Matrix(matrix.GetLength(0));
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(0); j++)
                    tranMatrix.matrix[j, i] = this.matrix[i, j];
            return tranMatrix;
        }

        public Matrix Inverse() // обратная матрица
        {
            Matrix helper = new Matrix(this.matrix);
            Matrix inverse = new Matrix(matrix.GetLength(0));
            for (int k = 0; k < this.matrix.GetLength(0); k++)
            {
                for (int i = 0; i < inverse.matrix.GetLength(0); i++)
                    for (int j = 0; j < inverse.matrix.GetLength(0); j++)
                    {
                        if ((i == k) && (j == k)) inverse.matrix[i, j] = 1 / helper.matrix[i, j];
                        if ((i == k) && (j != k)) inverse.matrix[i, j] = -helper.matrix[i, j] / helper.matrix[k, k];
                        if ((i != k) && (j == k)) inverse.matrix[i, j] = helper.matrix[i, k] / helper.matrix[k, k];
                        if ((i != k) && (j != k)) inverse.matrix[i, j] = helper.matrix[i, j] - helper.matrix[k, j] * helper.matrix[i, k] / helper.matrix[k, k];
                    }
                Array.Copy(inverse.matrix, helper.matrix, inverse.matrix.Length);
            }
            return inverse;
        }

        public Matrix MultOnNum(double num)
        {
            Matrix multOnNum = new Matrix(matrix.GetLength(0));
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(0); j++)
                    multOnNum.matrix[i, j] = this.matrix[i, j] * num;
           // multOnNum.Determine();
            multOnNum.NormCalc();
            return multOnNum;
        }

        //private void Determine() // детерминант матрицы
        //{
        //    double[,] tmp = (double[,])matrix.Clone();
        //    for(int i = 0; i < tmp.GetLength(0); i++)
        //    {
        //        for(int j = 0; j < tmp.GetLength(0); j++)
        //        {

        //        }
        //    }
        //    double[,] helpMatr = new double[this.matrix.GetLength(0), this.matrix.GetLength(0)];
        //    Array.Copy(matrix, helpMatr, this.matrix.Length);
        //    double det = 1;
        //    double b;
        //    for (int i = 0; i < matrix.GetLength(0); i++) // цикл по строкам
        //    {
        //        for (int j = i + 1; j < matrix.GetLength(0); j++) // цикл по элементам строки начиная со следующего после диагонального
        //        {
        //            if (helpMatr[i, i] == 0) { this.determine = 0; return; }
        //            b = helpMatr[j, i] / helpMatr[i, i];
        //            for (int k = i; k < matrix.GetLength(0); k++)
        //                helpMatr[j, k] -= helpMatr[i, k] * b;
        //        }
        //        det *= helpMatr[i, i];
        //    }
        //    this.determine = det;
        //}

        public Matrix SumMatrixes(Matrix secondMatr)
        {
            Matrix sumMatr = new Matrix(this.matrix.GetLength(0));
            for (int i = 0; i < this.matrix.GetLength(0); i++)
                for (int j = 0; j < secondMatr.matrix.GetLength(0); j++)
                    sumMatr.matrix[i, j] = this.matrix[i, j] + secondMatr.matrix[i, j];
          //  sumMatr.Determine();
            sumMatr.NormCalc();
            return sumMatr;
        }

        public Matrix MultMatrix(Matrix secondMatr)
        {
            Matrix multMatr = new Matrix(this.matrix.GetLength(0));
            Parallel.For(0, this.matrix.GetLength(0), i =>
            {
                for (int j = 0; j < this.matrix.GetLength(0); ++j)
                    for (int k = 0; k < secondMatr.matrix.GetLength(0); ++k)
                        multMatr.matrix[i, j] += this.matrix[i, k] * secondMatr.matrix[k, j];
            });
          //  multMatr.Determine();
            multMatr.NormCalc();
            return multMatr;
        }

        public Vector MultOnVect(Vector vect)
        {
            Vector newVect = new Vector(vect.VectorGetSet.Length);
            for (int i = 0; i < vect.VectorGetSet.Length; i++) // цикл по строкам
                for (int j = 0; j < matrix.GetLength(0); j++)
                    newVect.VectorGetSet[i] += matrix[i, j] * vect.VectorGetSet[j];
            return newVect;
        }

        public Matrix GenDModified() // диагональная матрица от исходной
        {
            Matrix M = new Matrix(this.matrix.GetLength(0));
            for (int i = 0; i < matrix.GetLength(0); i++)
                M.matrix[i, i] = this.matrix[i, i];
            return M;
        }

        public Matrix GenMUpRelax() // матрица M для метода верхней релаксации
        {
            
            Matrix D = GenDModified();
            Matrix A1 = new Matrix(this.matrix.GetLength(0));
            for (int i = 1; i < this.matrix.GetLength(0) - 1; i++)
                for (int j = 0; j < i; j++)
                    A1.matrix[i, j] = this.matrix[i, j];
            double tau = 2 / (this.norm + 1);
            //double tau = Math.Abs((1 - M.Norm) / A.Norm);
            Matrix M = D.SumMatrixes(A1.MultOnNum(tau));
            return M;
        }

        public Matrix GenBMatrix() // матрица B 
        {
            Matrix B = new Matrix(this.matrix.GetLength(0));
            for (int i = 0; i < this.matrix.GetLength(0); i++)
                for (int j = 0; j < this.matrix.GetLength(0); j++)
                    if (i != j) B.matrix[i, j] = -this.matrix[i, j] / this.matrix[i, i];
                    else B.matrix[i, j] = 0;
            return B;
        }

        public Matrix GenA1Matrix() // матрица A1 которая входит в M  для метода в верхней релаксации
        {
            Matrix A1 = new Matrix(this.matrix.GetLength(0));
            for (int i = 1; i < this.matrix.GetLength(0); i++)
                for (int j = 0; j < i; j++)
                    A1.matrix[i, j] = this.matrix[i, j];
            return A1;
        }

        public bool DiagDominate() // проверка на диагональное доминирование 
        {
            double temp = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    if (i != j) temp += Math.Abs(matrix[i, j]);
                }
                if (!(Math.Abs(matrix[i, i]) > temp)) return false;
                temp = 0;
            }
            return true;
        }

        //public double CalcTauD(Matrix A)
        //{

        //}
     
    }
}
