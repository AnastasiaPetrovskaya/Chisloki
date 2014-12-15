namespace PetrovskayaMatrix
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.matrixForm = new System.Windows.Forms.DataGridView();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.sizeBox = new System.Windows.Forms.TextBox();
            this.vectorForm = new System.Windows.Forms.DataGridView();
            this.writeBox = new System.Windows.Forms.CheckBox();
            this.resForm = new System.Windows.Forms.DataGridView();
            this.buttonSolve = new System.Windows.Forms.Button();
            this.radioButtonIteration = new System.Windows.Forms.RadioButton();
            this.radioButtonRelax = new System.Windows.Forms.RadioButton();
            this.textBoxEps = new System.Windows.Forms.TextBox();
            this.labelEps = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.labelCountIterations = new System.Windows.Forms.Label();
            this.buttonCompare = new System.Windows.Forms.Button();
            this.performanceCounter1 = new System.Diagnostics.PerformanceCounter();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.matrixForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vectorForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.performanceCounter1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // matrixForm
            // 
            this.matrixForm.AllowUserToAddRows = false;
            this.matrixForm.AllowUserToOrderColumns = true;
            this.matrixForm.BackgroundColor = System.Drawing.SystemColors.Control;
            this.matrixForm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.matrixForm.ColumnHeadersVisible = false;
            this.matrixForm.Location = new System.Drawing.Point(60, 228);
            this.matrixForm.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.matrixForm.Name = "matrixForm";
            this.matrixForm.RowHeadersVisible = false;
            this.matrixForm.RowTemplate.Height = 24;
            this.matrixForm.Size = new System.Drawing.Size(750, 781);
            this.matrixForm.TabIndex = 0;
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(1402, 324);
            this.buttonCreate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(150, 36);
            this.buttonCreate.TabIndex = 1;
            this.buttonCreate.Text = "Создать";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // sizeBox
            // 
            this.sizeBox.Location = new System.Drawing.Point(1671, 220);
            this.sizeBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sizeBox.MaxLength = 5;
            this.sizeBox.Name = "sizeBox";
            this.sizeBox.Size = new System.Drawing.Size(70, 31);
            this.sizeBox.TabIndex = 2;
            this.sizeBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.sizeBox_KeyPress);
            // 
            // vectorForm
            // 
            this.vectorForm.AllowUserToAddRows = false;
            this.vectorForm.AllowUserToOrderColumns = true;
            this.vectorForm.BackgroundColor = System.Drawing.SystemColors.Control;
            this.vectorForm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.vectorForm.ColumnHeadersVisible = false;
            this.vectorForm.Location = new System.Drawing.Point(883, 228);
            this.vectorForm.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.vectorForm.Name = "vectorForm";
            this.vectorForm.RowHeadersVisible = false;
            this.vectorForm.RowTemplate.Height = 24;
            this.vectorForm.Size = new System.Drawing.Size(139, 781);
            this.vectorForm.TabIndex = 3;
            // 
            // writeBox
            // 
            this.writeBox.AutoSize = true;
            this.writeBox.Location = new System.Drawing.Point(1403, 264);
            this.writeBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.writeBox.Name = "writeBox";
            this.writeBox.Size = new System.Drawing.Size(150, 29);
            this.writeBox.TabIndex = 4;
            this.writeBox.Text = "Заполнить";
            this.writeBox.UseVisualStyleBackColor = true;
            // 
            // resForm
            // 
            this.resForm.BackgroundColor = System.Drawing.SystemColors.Control;
            this.resForm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resForm.ColumnHeadersVisible = false;
            this.resForm.Location = new System.Drawing.Point(1103, 228);
            this.resForm.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.resForm.Name = "resForm";
            this.resForm.RowHeadersVisible = false;
            this.resForm.RowTemplate.Height = 24;
            this.resForm.Size = new System.Drawing.Size(242, 780);
            this.resForm.TabIndex = 6;
            // 
            // buttonSolve
            // 
            this.buttonSolve.Location = new System.Drawing.Point(1408, 633);
            this.buttonSolve.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonSolve.Name = "buttonSolve";
            this.buttonSolve.Size = new System.Drawing.Size(150, 53);
            this.buttonSolve.TabIndex = 7;
            this.buttonSolve.Text = "Решить";
            this.buttonSolve.UseVisualStyleBackColor = true;
            this.buttonSolve.Click += new System.EventHandler(this.buttonSolve_Click);
            // 
            // radioButtonIteration
            // 
            this.radioButtonIteration.AutoSize = true;
            this.radioButtonIteration.Location = new System.Drawing.Point(1408, 518);
            this.radioButtonIteration.Name = "radioButtonIteration";
            this.radioButtonIteration.Size = new System.Drawing.Size(502, 29);
            this.radioButtonIteration.TabIndex = 11;
            this.radioButtonIteration.TabStop = true;
            this.radioButtonIteration.Text = "Модифицированный метод простой итерации";
            this.radioButtonIteration.UseVisualStyleBackColor = true;
            // 
            // radioButtonRelax
            // 
            this.radioButtonRelax.AutoSize = true;
            this.radioButtonRelax.Location = new System.Drawing.Point(1408, 575);
            this.radioButtonRelax.Name = "radioButtonRelax";
            this.radioButtonRelax.Size = new System.Drawing.Size(319, 29);
            this.radioButtonRelax.TabIndex = 12;
            this.radioButtonRelax.TabStop = true;
            this.radioButtonRelax.Text = "Метод верхней релаксации";
            this.radioButtonRelax.UseVisualStyleBackColor = true;
            // 
            // textBoxEps
            // 
            this.textBoxEps.Location = new System.Drawing.Point(1584, 396);
            this.textBoxEps.Name = "textBoxEps";
            this.textBoxEps.Size = new System.Drawing.Size(157, 31);
            this.textBoxEps.TabIndex = 13;
            this.textBoxEps.Text = "1E-6";
            // 
            // labelEps
            // 
            this.labelEps.AutoSize = true;
            this.labelEps.Location = new System.Drawing.Point(1403, 399);
            this.labelEps.Name = "labelEps";
            this.labelEps.Size = new System.Drawing.Size(150, 25);
            this.labelEps.TabIndex = 14;
            this.labelEps.Text = "Погрешность:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(801, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(250, 25);
            this.label2.TabIndex = 15;
            this.label2.Text = "Решение системы Ax=b";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(227, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 25);
            this.label3.TabIndex = 16;
            this.label3.Text = "Матрица A";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(909, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 25);
            this.label4.TabIndex = 17;
            this.label4.Text = "Вектор b";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1165, 151);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 25);
            this.label5.TabIndex = 18;
            this.label5.Text = "Вектор x";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1397, 223);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(258, 25);
            this.label6.TabIndex = 19;
            this.label6.Text = "Размерность матрицы A";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1403, 463);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 25);
            this.label1.TabIndex = 20;
            this.label1.Text = "Метод решения";
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Location = new System.Drawing.Point(1403, 730);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(335, 25);
            this.labelTime.TabIndex = 21;
            this.labelTime.Text = "Время затраченное на решение";
            this.labelTime.Visible = false;
            // 
            // labelCountIterations
            // 
            this.labelCountIterations.AutoSize = true;
            this.labelCountIterations.Location = new System.Drawing.Point(1403, 787);
            this.labelCountIterations.Name = "labelCountIterations";
            this.labelCountIterations.Size = new System.Drawing.Size(173, 25);
            this.labelCountIterations.TabIndex = 22;
            this.labelCountIterations.Text = "Число итераций";
            this.labelCountIterations.Visible = false;
            // 
            // buttonCompare
            // 
            this.buttonCompare.Location = new System.Drawing.Point(1631, 633);
            this.buttonCompare.Name = "buttonCompare";
            this.buttonCompare.Size = new System.Drawing.Size(258, 53);
            this.buttonCompare.TabIndex = 23;
            this.buttonCompare.Text = "Сравнительный анализ";
            this.buttonCompare.UseVisualStyleBackColor = true;
            this.buttonCompare.Click += new System.EventHandler(this.buttonCompare_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(75, 1111);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(149, 187);
            this.pictureBox1.TabIndex = 24;
            this.pictureBox1.TabStop = false;
            
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2674, 1429);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttonCompare);
            this.Controls.Add(this.labelCountIterations);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelEps);
            this.Controls.Add(this.textBoxEps);
            this.Controls.Add(this.radioButtonRelax);
            this.Controls.Add(this.radioButtonIteration);
            this.Controls.Add(this.buttonSolve);
            this.Controls.Add(this.resForm);
            this.Controls.Add(this.writeBox);
            this.Controls.Add(this.vectorForm);
            this.Controls.Add(this.sizeBox);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(this.matrixForm);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.matrixForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vectorForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resForm)).EndInit();
           
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView matrixForm;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.TextBox sizeBox;
        private System.Windows.Forms.DataGridView vectorForm;
        private System.Windows.Forms.CheckBox writeBox;
        private System.Windows.Forms.DataGridView resForm;
        private System.Windows.Forms.Button buttonSolve;
        private System.Windows.Forms.RadioButton radioButtonIteration;
        private System.Windows.Forms.RadioButton radioButtonRelax;
        private System.Windows.Forms.TextBox textBoxEps;
        private System.Windows.Forms.Label labelEps;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Label labelCountIterations;
        private System.Windows.Forms.Button buttonCompare;
        private System.Diagnostics.PerformanceCounter performanceCounter1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

