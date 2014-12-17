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
                swatch.Restart();
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
                string elapsedTime = ts.TotalMilliseconds.ToString();
                labelTime.Text = "Время, затраченное на решение: " + elapsedTime;
                labelCountIterations.Text = "Количество выполненых итераций: " + countIterations.ToString();
                labelTime.Visible = true;
                labelCountIterations.Visible = true;
            }
            else if (radioButtonRelax.Checked) // если пользователь выбрал метод верхних релаксаций
            {
                System.Diagnostics.Stopwatch swatch = new System.Diagnostics.Stopwatch();
                Vector res;
                try
                {
                    swatch.Restart();
                    res = Methods.Relax(matr1, vect1, out countIterations, eps);
                    swatch.Stop();
                }
                catch (Exception err)
                {
                    System.Windows.Forms.MessageBox.Show(err.Message, "Ошибка", System.Windows.Forms.MessageBoxButtons.OK);
                    return;
                }
                GC.Collect();
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
                            A.MatrixGetSet[i, i] = summ * 1.5;
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
                    //flag = true;
                
                {
                    double fl=0;
                    for (int i = 0; i<size ;i++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            if (!(j == i))
                                fl += A.MatrixGetSet[i, j] * A.MatrixGetSet[i, j] / (A.MatrixGetSet[i, i] * A.MatrixGetSet[i, i]);
                        }
                    }
                    if (fl < 1)
                        flag = true;
                }
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
            double[,] tmCntIt = new double[12,10];
            int[,] itCntIt = new int[12, 10];
            double[,] tmCntRel = new double[12, 10];
            int[,] itCntRel = new int[12, 10];
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
            for (int i = 2; i < 14 ; i++)// цикл по размерностям матриц
            {
                for (int j = 1; j< 11; j++) // цикл по матрицам (по 10 матриц) каждой размерности
                {
                    Matrix A = GenerateMatrix(i);
                    Vector b = GenerateVector(i);
                    Vector res;
                    int countIterations = 0;
                    eps = Convert.ToDouble(textBoxEps.Text);
                    System.Diagnostics.Stopwatch swatch = new System.Diagnostics.Stopwatch();
                    countIterations = 0;
                    swatch.Restart();
                    try
                    {
                        res = Methods.Relax(A, b, out countIterations, eps);
                    }
                    catch (Exception err)
                    {
                        j--;
                        //System.Windows.Forms.MessageBox.Show(err.Message, "Ошибка", System.Windows.Forms.MessageBoxButtons.OK);
                        //return;
                        continue;
                    }
                    swatch.Stop();
                    TimeSpan ts = swatch.Elapsed;
                    double tm = ts.TotalMilliseconds;
                    int it = countIterations;
                    tmCntRel[i - 2, j - 1] = Math.Round(tm,4);
                    itCntRel[i - 2, j - 1] = it;
                    countIterations=0;
                    swatch.Restart();
                    try
                    {
                        res = Methods.ModifiedIteration(A, b, out countIterations, eps);
                    }
                    catch (Exception err)
                    {
                        j--;
                        //System.Windows.Forms.MessageBox.Show(err.Message, "Ошибка", System.Windows.Forms.MessageBoxButtons.OK);
                        //return;
                        continue;
                    }
                    swatch.Stop();
                    ts = swatch.Elapsed;
                    tm = ts.TotalMilliseconds;
                    it = countIterations;
                    tmCntIt[i - 2, j - 1] = Math.Round(tm, 4);
                    itCntIt[i - 2, j - 1] = it;
                }
            }
            // отрисовка графиков в Excel


            Microsoft.Office.Interop.Excel.Application application = new Microsoft.Office.Interop.Excel.Application();
            application.Workbooks.Add(Type.Missing);
            Worksheet sheet = (Worksheet)application.Sheets[1];

            // время(релаксации)
            for (int i = 1; i <= 11; i++)
            {
                double pointTm = 0;
                for (int j = 0; j <= 9; j++ )
                {
                    pointTm += tmCntRel[i, j];
                }
                pointTm = pointTm / 10;
                sheet.Cells[i, 1] = i+2;
                sheet.Cells[i, 2] = pointTm;
            }
            ChartObjects xlCharts = (ChartObjects)sheet.ChartObjects(Type.Missing);
            ChartObject myChart = (ChartObject)xlCharts.Add(110, 0, 350, 250);
            myChart.Name = "Зависимость времени от размерности матрицы";
            Chart chart = myChart.Chart;
            SeriesCollection seriesCollection = (SeriesCollection)chart.SeriesCollection(Type.Missing);
            Series series = seriesCollection.NewSeries();
            series.XValues = sheet.get_Range("A1", "A11");
            series.Values = sheet.get_Range("B1", "B11");
            series.Name = "Зависимость времени от размерности матрицы (метод верхней релаксации)";
            chart.ChartType = XlChartType.xlXYScatterSmooth;
            // итерации(релаксации)
            for (int i = 1; i <= 11; i++)
            {
                double pointTm = 0;
                for (int j = 0; j <= 9; j++)
                {
                    pointTm += itCntRel[i , j];
                }
                pointTm = pointTm / 10;
                sheet.Cells[i, "L"] = i+2;
                sheet.Cells[i, "M"] = pointTm;
            }
            //ChartObjects xlCharts = (ChartObjects)sheet.ChartObjects(Type.Missing);
            ChartObject myChart2 = (ChartObject)xlCharts.Add(680, 0, 350, 250);
            myChart2.Name = "Зависимость количества итераций от размерности матрицы";
            Chart chart2 = myChart2.Chart;
            SeriesCollection seriesCollection2 = (SeriesCollection)chart2.SeriesCollection(Type.Missing);
            Series series2 = seriesCollection2.NewSeries();
            series2.XValues = sheet.get_Range("L1", "L11");
            series2.Values = sheet.get_Range("M1", "M11");
            series2.Name = "Зависимость количества итераций от размерности матрицы (метод верхней релаксации)";
            chart2.ChartType = XlChartType.xlXYScatterSmooth;

            // время (итерации)
            for (int i = 1; i <= 11; i++)
            {
                double pointTm = 0;
                for (int j = 0; j <= 9; j++)
                {
                    pointTm += tmCntIt[i , j];
                }
                pointTm = pointTm / 10;
                sheet.Cells[i+21, "A"] = i+2;
                sheet.Cells[i+21, "B"] = pointTm;
            }
            //ChartObjects xlCharts = (ChartObjects)sheet.ChartObjects(Type.Missing);
            ChartObject myChart3 = (ChartObject)xlCharts.Add(110, 300, 350, 250);
            myChart3.Name = "Зависимость времени от размерности матрицы";
            Chart chart3 = myChart3.Chart;
            SeriesCollection seriesCollection3 = (SeriesCollection)chart3.SeriesCollection(Type.Missing);
            Series series3 = seriesCollection3.NewSeries();
            series3.XValues = sheet.get_Range("A22", "A32");
            series3.Values = sheet.get_Range("B22", "B32");
            series3.Name = "Зависимость времени от размерности матрицы (модифицированный метод простой итерации)";
            chart3.ChartType = XlChartType.xlXYScatterSmooth;

            // итерации(итерации)
            for (int i = 1; i <= 11; i++)
            {
                double pointTm = 0;
                for (int j = 0; j <= 9; j++)
                {
                    pointTm += itCntIt[i , j];
                }
                pointTm = pointTm / 10;
                sheet.Cells[i+21, "L"] = i+2;
                sheet.Cells[i+21, "M"] = pointTm;
            }
            //ChartObjects xlCharts = (ChartObjects)sheet.ChartObjects(Type.Missing);
            ChartObject myChart4 = (ChartObject)xlCharts.Add(680, 300, 350, 250);
            myChart4.Name = "Зависимость количества ";
            Chart chart4 = myChart4.Chart;
            SeriesCollection seriesCollection4 = (SeriesCollection)chart4.SeriesCollection(Type.Missing);
            Series series4 = seriesCollection4.NewSeries();
            series4.XValues = sheet.get_Range("L22", "L32");
            series4.Values = sheet.get_Range("M22", "M32");
            series4.Name = "Зависимость количества итераций от размерности матрицы (модифицированный метод простой итерации)";
            
            
            chart4.ChartType = XlChartType.xlXYScatterSmooth;



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
