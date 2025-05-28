using Microsoft.VisualBasic;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace prilog_indiv
{
    public partial class addorder : Form
    {
        public NpgsqlConnection con;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        public addorder(NpgsqlConnection con)
        {
            InitializeComponent();
            this.con = con;
            Update();
        }
        private class ClientItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public override string ToString() => Name;
        }
        public class ProductItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string Unit { get; set; }
            public string DisplayText { get; set; }

            // Переопределяем ToString для корректного отображения в CheckedListBox
            public override string ToString()
            {
                return DisplayText;
            }
        }
        public void Update()
        {
            comboBox1.DisplayMember = "Name"; // Отображаемое поле
            comboBox1.ValueMember = "Id";     // Скрытое значение (ID)
            String sql = "SELECT client_id, name FROM clients ORDER BY name";
            NpgsqlCommand command1 = new NpgsqlCommand(sql, con);
            using (var reader = command1.ExecuteReader())
            {
                while (reader.Read())
                {
                    comboBox1.Items.Add(new ClientItem
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    });
                }
            }
            sql = "SELECT product_id, name, price, unit FROM products ORDER BY name";
            NpgsqlCommand command2 = new NpgsqlCommand(sql, con);
            using (NpgsqlDataReader reader = command2.ExecuteReader())
            {
                while (reader.Read())
                {
                    // Создаем строку для отображения
                    string displayText = $"{reader["name"]}";

                    // Создаем объект с полной информацией о товаре
                    ProductItem item = new ProductItem
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Price = reader.GetDecimal(2),
                        Unit = reader.GetString(3),
                        DisplayText = displayText
                    };

                    // Добавляем в CheckedListBox
                    checkedListBox1.Items.Add(item, isChecked: false);
                }
            }
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Выберите клиента из списка!");
                return;
            }
            DateTime dateOnly = dateTimePicker1.Value.Date;
            // Получаем ID выбранного клиента
            dynamic selectedItem = comboBox1.SelectedItem;
            int clientId = selectedItem.Id;
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO Orders (client_id, order_date,total_amount) VALUES (:client_id,:order_date, 0) RETURNING order_id", con);
                command.Parameters.AddWithValue("client_id", clientId);
                command.Parameters.AddWithValue("order_date", dateOnly);
                command.Parameters.AddWithValue("total_amount", Convert.ToDouble(0));

                int newOrderId = Convert.ToInt32(command.ExecuteScalar());
                foreach (var item in checkedListBox1.CheckedItems)
                {
                    string quantity = Interaction.InputBox("Введите количество:", "Ввод количества", "1");
                    dynamic product = item;

                    string sql = @"INSERT INTO order_items 
                                         (order_id, product_id) 
                                         VALUES (:orderId, :productId)"
                        ;

                    using (NpgsqlCommand com = new NpgsqlCommand(sql, con))
                    {
                        com.Parameters.AddWithValue("@orderId", newOrderId);
                        com.Parameters.AddWithValue("@productId", product.Id);

                        com.ExecuteNonQuery();
                    }
                }
            }
            catch { }
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
