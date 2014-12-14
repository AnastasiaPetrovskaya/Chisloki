using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PetrovskayaMatrix
{
    public partial class Form1 : Form
    {
        // --------------------------- обработка событий формы
        public Form1()
        {
            InitializeComponent();
        }

        public void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonCreate_Click(object sender, EventArgs e) // обработка события нажатия на кнопку "Создать"
        {
           
            int size = vectorForm.RowCount = matrixForm.RowCount = matrixForm.ColumnCount = Convert.ToInt32(sizeBox.Text);
            if (writeBox.Checked) // если отмечен чекбокс "Заполнить", то заполняем матрицу рандомными числами но должно выполнятся условие
                // диагонального преобладания
            {    
                bool flag = false;
                do
                {
                    Matrix A = GenerateMatrix(size);
                    Matrix M = A.GenDModified();
                    Vector E = new Vector(size);
                    for (int j = 0 ; j < size; j++)
                    {
                        E.VectorGetSet[j] = 1;
                    }
                    Vector tmp = M.MultOnNum(2).SumMatrixes(A.MultOnNum(-1)).MultOnVect(E);
                    double scalMultiply = 0;
                    for (int j = 0; j < size; j ++)
                    {
                        scalMultiply += E.VectorGetSet[j]*tmp.VectorGetSet[j];
                    }
                    if (scalMultiply > 0)
                        flag = true;
                }
                while (flag == false);
            }
            // размер сетки под содержание
            matrixForm.AutoResizeColumns(); 
            matrixForm.AutoResizeRows();
            vectorForm.AutoResizeColumns();
            vectorForm.AutoResizeRows();
        }

        private void sizeBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }


        private void buttonSolve_Click(object sender, EventArgs e)
        {
            double[,] readMatr = new double[matrixForm.RowCount, matrixForm.ColumnCount];
            double[] readVect = new double[vectorForm.RowCount];
            for (int i = 0; i < matrixForm.RowCount; i++)
            {
                readVect[i] = Convert.ToDouble(vectorForm.Rows[i].Cells[0].Value);
                for (int j = 0; j < matrixForm.ColumnCount; j++)
                    readMatr[i, j] = Convert.ToDouble(matrixForm.Rows[i].Cells[j].Value);
            }
            Matrix matr1 = new Matrix(readMatr);
            Vector vect1 = new Vector(readVect);
            double eps;
            try
            {
                eps = Convert.ToDouble(textBoxEps.Text);
            }
            catch(Exception err)
            {
                System.Windows.Forms.MessageBox.Show("Вы ввели недопустимое число в поле для введения погрешности.",
                    "Ошибка", System.Windows.Forms.MessageBoxButtons.OK );
                return;
            }
            int countIterations;
            if (radioButtonIteration.Checked)// если пользователь выбрал модифицированный метод простой итерации
            {
                System.Diagnostics.Stopwatch swatch = new System.Diagnostics.Stopwatch();
                swatch.Start();
                Vector res;
                try 
                {
                    res = Methods.ModifiedIteration(matr1, vect1, out countIterations, eps);
                }
                catch (Exception err)
                {
                    System.Windows.Forms.MessageBox.Show(err.Message, "Ошибка", System.Windows.Forms.MessageBoxButtons.OK);
                    return;
                }
                swatch.Stop();
                resForm.RowCount = vect1.VectorGetSet.GetLength(0);
                for (int i = 0; i < vect1.VectorGetSet.GetLength(0); i++)// заполнение результата на форме
                {
                    resForm[0, i].Value = res.VectorGetSet[i];
                }
                labelTime.Text = "Время, затраченное на решение" + swatch.Elapsed;
                labelCountIterations.Text = "Количество выполненых итераций " + countIterations.ToString();
                labelTime.Visible = true;
                labelCountIterations.Visible = true;
            }
            else if (radioButtonRelax.Checked) // если пользователь выбрал метод верхних релаксаций
            {
                System.Diagnostics.Stopwatch swatch = new System.Diagnostics.Stopwatch();
                swatch.Start();
                Vector res;
                try
                {
                    res = Methods.Relax(matr1, vect1, out countIterations, eps);
                }
                catch (Exception err)
                {
                    System.Windows.Forms.MessageBox.Show(err.Message, "Ошибка", System.Windows.Forms.MessageBoxButtons.OK);
                    return;
                }
                swatch.Stop();
                resForm.RowCount = vect1.VectorGetSet.GetLength(0);
                for (int i = 0; i < vect1.VectorGetSet.GetLength(0); i++)// заполнение результата на форме
                {
                    
                    resForm[0, i].Value = res.VectorGetSet[i];
                    
                }
                labelTime.Text = "Время, затраченное на решение" + swatch.Elapsed;
                labelCountIterations.Text = "Количество выполненых итераций " + countIterations.ToString();
                labelTime.Visible = true;
                labelCountIterations.Visible = true;
            }
            resForm.AutoResizeColumns();
            resForm.AutoResizeRows();
        }

        
        private Matrix GenerateMatrix(int size)
        {
            Random x = new Random(); // создание экземпляра класса рандом
            for (int i = 0; i < size; i++) // цикл по строкам
            {
                double summ = 0;// переменная для подсчета суммы элементов в строке
                int elem;
                for (int j = 0; j < size; j++) // цикл по столбцам
                {
                    elem = x.Next(-9, 10);
                    summ += Math.Abs(elem);
                    matrixForm[i, j].Value = elem;
                    // теперь меняем элемент на главной диагонали , чтобы выполнялось условие диагонального преобладания
                    if (j == size - 1)
                    {
                        matrixForm[i, i].Value = summ * 1.3;
                    }
                }
                vectorForm[0, i].Value = x.Next(-9, 10);
            }
            double[,] readMatr = new double[matrixForm.RowCount, matrixForm.ColumnCount];
            for (int i = 0; i < matrixForm.RowCount; i++)
            {
                for (int j = 0; j < matrixForm.ColumnCount; j++)
                    readMatr[i, j] = Convert.ToDouble(matrixForm.Rows[i].Cells[j].Value);
            }
            Matrix matr1 = new Matrix(readMatr);
            return matr1;
        }

        
        // --------------------------- с этого места начинается описание рабочих методов
    }
}
