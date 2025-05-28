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

namespace prilog_indiv
{
    public partial class orders : Form
    {
        public NpgsqlConnection con;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        DataSet ds2 = new DataSet();
        public orders(NpgsqlConnection con)
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
                            o.order_date,
                            o.total_amount
                        FROM orders o
                        JOIN clients c ON o.client_id = c.client_id;";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];
            DataGridView1.DataSource = dt;
            DataGridView1.Columns[0].HeaderText = "ID";
            DataGridView1.Columns[1].HeaderText = "ФИО";
            DataGridView1.Columns[2].HeaderText = "Дата";
            DataGridView1.Columns[3].HeaderText = "Сумма";
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addorder f = new addorder(con);
            f.ShowDialog();
            Update();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = (int)DataGridView1.CurrentRow.Cells["order_id"].Value;
            NpgsqlCommand command = new NpgsqlCommand("Delete from Orders where order_id =:id", con);
            command.Parameters.AddWithValue("id", id);
            command.ExecuteNonQuery();
            Update();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = (int)DataGridView1.CurrentRow.Cells["order_id"].Value;
            String sql = @"SELECT p.name, p.price FROM order_items oi JOIN products p ON oi.product_id = p.product_id WHERE order_id = @id;";
            NpgsqlCommand command = new NpgsqlCommand(sql, con);
            command.Parameters.AddWithValue("@id", id);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(command);
            ds2.Reset();
            da.Fill(ds2);
            dt = ds2.Tables[0];
            DataGridView2.DataSource = dt;
            DataGridView2.Columns[0].HeaderText = "Название";
            DataGridView2.Columns[1].HeaderText = "Цена";
            this.StartPosition = FormStartPosition.CenterScreen;
        }
    }
}
