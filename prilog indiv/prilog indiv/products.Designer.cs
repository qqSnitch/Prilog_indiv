﻿namespace prilog_indiv
{
    partial class products
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridView1 = new DataGridView();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            ((System.ComponentModel.ISupportInitialize)DataGridView1).BeginInit();
            SuspendLayout();
            // 
            // DataGridView1
            // 
            DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridView1.Location = new Point(162, 12);
            DataGridView1.Name = "DataGridView1";
            DataGridView1.RowHeadersWidth = 51;
            DataGridView1.Size = new Size(615, 426);
            DataGridView1.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(12, 12);
            button1.Name = "button1";
            button1.Size = new Size(144, 29);
            button1.TabIndex = 1;
            button1.Text = "Добавить товар";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(12, 64);
            button2.Name = "button2";
            button2.Size = new Size(144, 29);
            button2.TabIndex = 2;
            button2.Text = "Удалить товар";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(12, 109);
            button3.Name = "button3";
            button3.Size = new Size(144, 30);
            button3.TabIndex = 3;
            button3.Text = "Изменить товар";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // products
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(DataGridView1);
            Name = "products";
            Text = "products";
            ((System.ComponentModel.ISupportInitialize)DataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView DataGridView1;
        private Button button1;
        private Button button2;
        private Button button3;
    }
}