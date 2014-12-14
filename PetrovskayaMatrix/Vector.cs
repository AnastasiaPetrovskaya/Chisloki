using System;
using System.Collections.Generic;
using System.Text;

namespace PetrovskayaMatrix
{
    class Vector
    {
        double[] vector;
        int vectSize = 0;
        //свойства
        public double[] VectorGetSet
        {
            set { vector = value; }
            get { return vector; }
        }

        public Vector(int size)
        {
            vector = new double[size];
            vectSize = size;
        }

        public Vector(double[] readVect)
        {
            vector = new double[readVect.Length]; 
            Array.Copy(readVect, vector, readVect.Length);
            vectSize = readVect.Length;
        }
        

        public Vector SumVector(Vector secondVect) // метод суммирования данного вектора с другим возвращает вектор
        {
            Vector sumVect = new Vector(Math.Max(this.vectSize, secondVect.vectSize));
            for (int i = 0; i < sumVect.vectSize; i++)
                sumVect.vector[i] = this.vector[i] + secondVect.vector[i];
            return sumVect;
        }

        public double ScalarMultVector(Vector secondVector)
        {
            double res = 0;
            for (int i = 0; i < Math.Min(this.vectSize, secondVector.vectSize); i++)
                res += this.vector[i] * secondVector.vector[i];
            return res;
        }

        public Vector MultOnNum(double num) // произведение вектора на число
        {
            Vector vectMultOnNum = new Vector(this.vectSize);
            for (int i = 0; i < vectMultOnNum.vectSize; i++)
                vectMultOnNum.vector[i] = this.vector[i] * num;
            return vectMultOnNum;
        }

        internal string toString()
        {
            string ans = "";
            for (int i = 0; i < vector.Length; i++)
                ans += vector[i].ToString() + "\n";
            return ans;
        }
    }
}
