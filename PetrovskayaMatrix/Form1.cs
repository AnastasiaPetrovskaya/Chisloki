using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

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
                Matrix A = GenerateMatrix(size);
                Vector b = GenerateVector(size);
                for (int i = 0; i<size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        matrixForm[j, i].Value = A.MatrixGetSet[j, i];
                    }
                    vectorForm[0, i].Value = b.VectorGetSet[i];
                }
                
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


        private void buttonSolve_Click(object sender, EventArgs e) // нажатие на кнопку "Решить"
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
                TimeSpan ts = swatch.Elapsed;
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:0000}",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds / 10);
                labelTime.Text = "Время, затраченное на решение: " + elapsedTime;
                labelCountIterations.Text = "Количество выполненых итераций: " + countIterations.ToString();
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
                TimeSpan ts = swatch.Elapsed;
                resForm.RowCount = vect1.VectorGetSet.GetLength(0);
                for (int i = 0; i < vect1.VectorGetSet.GetLength(0); i++)// заполнение результата на форме
                {
                    
                    resForm[0, i].Value = res.VectorGetSet[i];
                    
                }

                string elapsedTime = ts.TotalMilliseconds.ToString();
                labelTime.Text = "Время, затраченное на решение: " + elapsedTime;
                labelCountIterations.Text = "Количество выполненых итераций: " + countIterations.ToString();
                labelTime.Visible = true;
                labelCountIterations.Visible = true;
            }
            resForm.AutoResizeColumns();
            resForm.AutoResizeRows();
        }

        
        private Matrix GenerateMatrix(int size) //  генерация матриц
        {
            bool flag = false;
            Matrix A = new Matrix(size);
            do
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
                        A.MatrixGetSet[i, j] = elem;
                        // теперь меняем элемент на главной диагонали , чтобы выполнялось условие диагонального преобладания
                        if (j == size - 1)
                        {
                            A.MatrixGetSet[i, i] = summ * 1.3;
                        }
                    }
                }
                Matrix M = A.GenDModified();
                Vector E = new Vector(size);
                for (int j = 0; j < size; j++)
                {
                    E.VectorGetSet[j] = 1;
                }
                Vector tmp = M.MultOnNum(2).SumMatrixes(A.MultOnNum(-1)).MultOnVect(E);
                double scalMultiply = 0;
                for (int j = 0; j < size; j++)
                {
                    scalMultiply += E.VectorGetSet[j] * tmp.VectorGetSet[j];
                }
                if (scalMultiply > 0)
                    flag = true;
            } while (flag == false);
            return A;
        }



        private Vector GenerateVector(int size) //  генерация вектора
        {
            Random x = new Random(); // создание экземпляра класса рандом
            Vector resV = new Vector(size);
            for (int i = 0; i< size; i++)
            {
                resV.VectorGetSet[i] = x.Next(-9, 10);
            }
            return resV;
        }


        private void buttonCompare_Click(object sender, EventArgs e) // сравнить два метода
        {
            double[,] tmCnt = new double[11,10];
            int[,] itCnt = new int[11, 10];
            double eps;
            try
            {
                eps = Convert.ToDouble(textBoxEps.Text);
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show("Вы ввели недопустимое число в поле для введения погрешности.",
                    "Ошибка", System.Windows.Forms.MessageBoxButtons.OK);
                return;
            }
            for (int i = 3; i < 14 ; i++)// цикл по размерностям матриц
            {
                for (int j = 1; j< 11; j++) // цикл по матрицам (по 10 матриц) каждой размерности
                {
                    Matrix A = GenerateMatrix(i);
                    Vector b = GenerateVector(i);
                    Vector res;
                    int countIterations = 0;
                    eps = Convert.ToDouble(textBoxEps.Text);
                    System.Diagnostics.Stopwatch swatch = new System.Diagnostics.Stopwatch();
                    swatch.Start();
                    if (j == 1 && i == 3)
                    {
                        try
                        {
                            res = Methods.Relax(A, b, out countIterations, eps);
                        }
                        catch (Exception err)
                        {
                            System.Windows.Forms.MessageBox.Show(err.Message, "Ошибка", System.Windows.Forms.MessageBoxButtons.OK);
                            return;
                        }
                    }
                    swatch.Stop();
                    swatch.Restart();
                    try
                    {
                        res = Methods.Relax(A, b, out countIterations, eps);
                    }
                    catch (Exception err)
                    {
                        System.Windows.Forms.MessageBox.Show(err.Message, "Ошибка", System.Windows.Forms.MessageBoxButtons.OK);
                        return;
                    }
                    swatch.Stop();
                    TimeSpan ts = swatch.Elapsed;
                    double tm = ts.TotalMilliseconds;
                    int it = countIterations;
                    tmCnt[i - 3, j - 1] = Math.Round(tm,4);
                    itCnt[i - 3, j - 1] = it;
                }
            }
            // отрисовка графиков


            Microsoft.Office.Interop.Excel.Application application = new Microsoft.Office.Interop.Excel.Application();
            application.Workbooks.Add(Type.Missing);
            Worksheet sheet = (Worksheet)application.Sheets[1];


            for (int i = 1; i <= 11; i++)
            {
                double pointTm = 0;
                for (int j = 0; j <= 9; j++ )
                {
                    pointTm += tmCnt[i-1, j];
                }
                pointTm = pointTm / 10;
                sheet.Cells[i, 1] = i;
                sheet.Cells[i, 2] = pointTm;
            }

            ChartObjects xlCharts = (ChartObjects)sheet.ChartObjects(Type.Missing);
            ChartObject myChart = (ChartObject)xlCharts.Add(110, 0, 350, 250);
            Chart chart = myChart.Chart;
            SeriesCollection seriesCollection = (SeriesCollection)chart.SeriesCollection(Type.Missing);
            Series series = seriesCollection.NewSeries();
            series.XValues = sheet.get_Range("A1", "A10");
            series.Values = sheet.get_Range("B1", "B10");
            chart.ChartType = XlChartType.xlXYScatterSmooth;
            application.Visible = true;

            //System.Drawing.Pen myPen;
            //myPen = new System.Drawing.Pen(System.Drawing.Color.Red);
            //System.Drawing.Graphics formGraphics = this.CreateGraphics();
            ////formGraphics.DrawLine(myPen, 0, 0, 200, 200);
            ////myPen.Dispose();
            ////formGraphics.Dispose();

            //SolidBrush brush = new SolidBrush(Color.Brown);
            //formGraphics.FillRectangle(brush, new System.Drawing.Rectangle(10, 800, 100, 100));
            //formGraphics.FillRectangle(brush, 10, 800, 100, 100);

            //System.Drawing.Point[] posTm = new System.Drawing.Point[11];

            //int p = 0;
            //for (int i = 0; 1 < 12; i++)
            //{
            //    for (j = )
            //    posTm[i] = new System.Drawing.Point(i+1,)
            //}

            //formGraphics.DrawCurve(myPen, posTm);

            //pictureBox1.BackColor = Color.White;
            //pictureBox1.Visible = true;

            //// Connect the Paint event of the PictureBox to the event handler method.
            //pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
        }

        
        

        
        
    }
}
