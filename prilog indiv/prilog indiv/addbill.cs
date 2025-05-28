using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace prilog_indiv
{
    public partial class addbill : Form
    {
        public NpgsqlConnection con;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        public addbill(NpgsqlConnection con)
        {
            InitializeComponent();
            this.con = con;
            Update();
        }
        public void Update()
        {
            String sql = @"SELECT 
                            o.order_id,
                            c.name,
                            o.order_date
                        FROM orders o
                        JOIN clients c ON o.client_id = c.client_id;";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];
            DataGridView1.DataSource = dt;
            DataGridView1.Columns[0].HeaderText = "ID заказа";
            DataGridView1.Columns[1].HeaderText = "ФИО";
            DataGridView1.Columns[2].HeaderText = "Дата";
            DataGridView1.SelectionChanged += SelectionChanged;
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        private void SelectionChanged(object sender, EventArgs e)
        {
            if (DataGridView1.CurrentRow == null ||
                DataGridView1.CurrentRow.IsNewRow) return;

            int orderId = Convert.ToInt32(DataGridView1.CurrentRow.Cells["order_id"].Value);
            LoadOrderItems(orderId);
        }
        public class ProductItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string DisplayText { get; set; }
            public override string ToString()
            {
                return DisplayText;
            }

        }
        private void LoadOrderItems(int orderId)
        {
            checkedListBox1.Items.Clear();

            try
            {
                string sql = @"SELECT p.product_id, p.name
                          FROM order_items oi
                          JOIN products p ON oi.product_id = p.product_id
                          WHERE oi.order_id = @orderId";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, con))
                {
                    command.Parameters.AddWithValue("@orderId", orderId);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
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
                                DisplayText = displayText
                            };

                            // Добавляем в CheckedListBox
                            checkedListBox1.Items.Add(item, isChecked: true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки товаров: {ex.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime dateOnly = dateTimePicker1.Value.Date;
            int id = (int)DataGridView1.CurrentRow.Cells["order_id"].Value;

            NpgsqlCommand command = new NpgsqlCommand("INSERT INTO invoices (invoice_date, order_id) VALUES (:invoice_date,:order_id) RETURNING invoice_id", con);
            command.Parameters.AddWithValue("invoice_date", dateOnly);
            command.Parameters.AddWithValue("order_id", id);
            
            int newId = Convert.ToInt32(command.ExecuteScalar());
            foreach (var item in checkedListBox1.CheckedItems)
            {
                dynamic product = item;

                string sql = @"INSERT INTO invoice_items 
                                         (invoice_id, product_id) 
                                         VALUES (:invoice_id, :productId)"
                    ;

                using (NpgsqlCommand com = new NpgsqlCommand(sql, con))
                {
                    com.Parameters.AddWithValue("@invoice_id", newId);
                    com.Parameters.AddWithValue("@productId", product.Id);

                    com.ExecuteNonQuery();
                }
            }
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
