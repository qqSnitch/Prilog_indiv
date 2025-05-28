namespace prilog_indiv
{
    partial class orders
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
            button1 = new Button();
            button2 = new Button();
            DataGridView1 = new DataGridView();
            DataGridView2 = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            button3 = new Button();
            ((System.ComponentModel.ISupportInitialize)DataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DataGridView2).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(28, 45);
            button1.Name = "button1";
            button1.Size = new Size(212, 29);
            button1.TabIndex = 0;
            button1.Text = "Добавить новый заказ";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(28, 80);
            button2.Name = "button2";
            button2.Size = new Size(212, 29);
            button2.TabIndex = 1;
            button2.Text = "Удалить заказ";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // DataGridView1
            // 
            DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridView1.Location = new Point(272, 45);
            DataGridView1.Name = "DataGridView1";
            DataGridView1.RowHeadersWidth = 51;
            DataGridView1.Size = new Size(381, 393);
            DataGridView1.TabIndex = 2;
            // 
            // DataGridView2
            // 
            DataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridView2.Location = new Point(673, 45);
            DataGridView2.Name = "DataGridView2";
            DataGridView2.RowHeadersWidth = 51;
            DataGridView2.Size = new Size(377, 393);
            DataGridView2.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(429, 20);
            label1.Name = "label1";
            label1.Size = new Size(58, 20);
            label1.TabIndex = 4;
            label1.Text = "Заказы";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(803, 20);
            label2.Name = "label2";
            label2.Size = new Size(123, 20);
            label2.TabIndex = 5;
            label2.Text = "Товары в заказе";
            // 
            // button3
            // 
            button3.Location = new Point(28, 155);
            button3.Name = "button3";
            button3.Size = new Size(212, 31);
            button3.TabIndex = 6;
            button3.Text = "Показать товары";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // orders
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1087, 450);
            Controls.Add(button3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(DataGridView2);
            Controls.Add(DataGridView1);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "orders";
            Text = "orders";
            ((System.ComponentModel.ISupportInitialize)DataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)DataGridView2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private DataGridView DataGridView1;
        private DataGridView DataGridView2;
        private Label label1;
        private Label label2;
        private Button button3;
    }
}