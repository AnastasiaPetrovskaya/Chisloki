using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetrovskayaMatrix
{
    class Methods
    {
        // метод верхней релаксации
        public static Vector Relax(Matrix A, Vector b, out int countIterations, double epsilon = 1E-9)
        {
            b = A.Transpose().MultOnVect(b);
            A = A.Transpose().MultMatrix(A);
            //double tau = 2 / (A.Norm + 1);

            Matrix M = A.GenMUpRelax();
            double tau = Math.Abs((1 - M.Norm) / A.Norm);
            Matrix Tmp = M.MultMatrix(A.MultOnNum(tau * (-1)));
            countIterations = 0;
            Vector X1 = new Vector(b.VectorGetSet);
            Vector X2 = X1;
            while (countIterations <= 1000000) // i - максимально допустимое количество итераций
            {
                X2 = M.Inverse().MultOnVect(Tmp.MultOnVect(X1).SumVector(b.MultOnNum(tau)));
                // проверка разности межну решением на предыдущей итерации и на текущей  
                double delta = 0;
                for(int j = 0; j < X2.VectorGetSet.GetLength(0); j++)
			    {
			        delta += (X2.VectorGetSet[j] - X1.VectorGetSet[j])*(X2.VectorGetSet[j] - X1.VectorGetSet[j]);
                    j ++;
			    } 
                if (Math.Sqrt(delta) < epsilon) // если заданная точность достигнута
                {
                    return  X2;
                }
                else
                {
                    X1 = X2;
                    countIterations++;
                }   
            }
            throw new Exception("метод верхних релаксаций не сошелся");
        }


        // модифицированный метод простой итерации
        public static Vector ModifiedIteration(Matrix A, Vector b,out int countIterations, double epsilon )
        {
            b = A.Transpose().MultOnVect(b);
            A = A.Transpose().MultMatrix(A);
            countIterations = 0;
            Matrix D = A.GenDModified();
            Vector X1 = new Vector(b.VectorGetSet);
            Vector X2 = new Vector(X1.VectorGetSet);
            //double tau =  0.5 / (A.Norm + 1);
            double tau = Math.Abs((1 - D.Norm) / A.Norm);
            Matrix inv = D.Inverse();
            bool flag ;
            while (countIterations <= 1000000) // i - максимально допустимое количество итераций
            {
                flag = true;
                //X2 = inv.MultOnVect(A.MultOnVect(X1).MultOnNum(-1).SumVector(b)).MultOnNum(tau).SumVector(X1);
                X2 = inv.MultOnVect(D.SumMatrixes(A.MultOnNum(-tau)).MultOnVect(X1).SumVector(b.MultOnNum(tau)));
                // проверка разности межну решением на предыдущей итерации и на текущей  
                for (int j = 0; j < X1.VectorGetSet.Length; j++)
                {
                    if (Math.Abs(X1.SumVector(X2.MultOnNum(-1)).VectorGetSet[j]) > epsilon) flag = false;
                }
                
                if (flag) // если заданная точность достигнута
                {
                    return X2;
                }
                else
                {
                    Array.Copy(X2.VectorGetSet, X1.VectorGetSet, X1.VectorGetSet.Length); ;
                    //if (countIterations % 500 == 0)
                        //System.Windows.Forms.MessageBox.Show(X1.toString());
                    countIterations++;
                }
            }
            throw new Exception("модифицированный метод простой итерации не сошелся");
        }

    }
}
