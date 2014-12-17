using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetrovskayaMatrix
{
    public class MethodStatistic
    {
        private int iterationsCount;
        private double time;
        private Vector delta;
        //свойства
        public int IterationsCount
        {
            get { return this.iterationsCount; }
        }
        public double Time
        {
            get { return time;}
        }
        public Vector Delta
        {
            get { return delta; }
        }
        //конструктор
        public MethodStatistic(int iterationsCount, double tm, Vector dl)
        {
            this.iterationsCount = iterationsCount;
            this.time = tm;
            this.delta = 
        }
    }
    class Methods
    {
        // метод верхней релаксации
        public static Vector Relax(Matrix A, Vector b, out int countIterations, double epsilon = 1E-9)
        {
            b = A.Transpose().MultOnVect(b);
            A = A.Transpose().MultMatrix(A);
            //double tau = 2 / (A.Norm * 1.1);
            Matrix M = A.GenMUpRelax();
            //double n = M.SumMatrixes(A.MultOnNum(tau * (-1))).Norm;
            double tau = (Math.Abs((1 - M.Norm) / A.Norm))*0.9;
            double n = M.SumMatrixes(A.MultOnNum(tau * (-1))).Norm;
            Matrix Tmp = M.MultMatrix(A.MultOnNum(tau * (-1)));
            countIterations = 0;
            //Matrix M = A.GenMUpRelax();
            Vector X1 = new Vector(b.VectorGetSet);
            Vector X2 = X1;
            Matrix inv = M.Inverse();
            while (countIterations <= 1000000) // i - максимально допустимое количество итераций
            {
                X2 = inv.MultOnVect(M.SumMatrixes(A.MultOnNum(-tau)).MultOnVect(X1).SumVector(b.MultOnNum(tau)));
                // проверка разности межну решением на предыдущей итерации и на текущей  
                double delta = 0;
                for(int j = 0; j < X2.VectorGetSet.GetLength(0); j++)
			    {
			        delta += (X2.VectorGetSet[j] - X1.VectorGetSet[j])*(X2.VectorGetSet[j] - X1.VectorGetSet[j]);
                    
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
            
            double tau = 1;//2 / (A.Norm * 1.1);
            
            //double tau = Math.Abs(( D.Norm - 1) / A.Norm);
            double n = D.SumMatrixes(A.MultOnNum(tau * (-1))).Norm;
            Matrix inv = D.Inverse();
            while (countIterations <= 1000000) // i - максимально допустимое количество итераций
            {
                //X2 = inv.MultOnVect(A.MultOnVect(X1).MultOnNum(-1).SumVector(b)).MultOnNum(tau).SumVector(X1);
                X2 = inv.MultOnVect(D.SumMatrixes(A.MultOnNum(-tau)).MultOnVect(X1).SumVector(b.MultOnNum(tau)));
                // проверка разности межну решением на предыдущей итерации и на текущей  
                double delta = 0;
                for (int j = 0; j < X2.VectorGetSet.GetLength(0); j++)
                {
                    delta += (X2.VectorGetSet[j] - X1.VectorGetSet[j]) * (X2.VectorGetSet[j] - X1.VectorGetSet[j]);
                }
                if (Math.Sqrt(delta) < epsilon) // если заданная точность достигнута
                {
                    return X2;
                }
                else
                {
                    X1 = X2;
                    countIterations++;
                }   
            }
            throw new Exception("модифицированный метод простой итерации не сошелся");
        }

    }
}
