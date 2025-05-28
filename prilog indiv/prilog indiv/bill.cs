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
    public partial class bill : Form
    {
        public NpgsqlConnection con;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        DataSet ds2 = new DataSet();
        public bill(NpgsqlConnection con)
        {
            InitializeComponent();
            this.con = con;
            Update();
        }
        public void Update()
        {
            String sql = @"SELECT 
                            i.invoice_id,
                            i.invoice_date,
                            c.name,
                            c.address,
                            p.name,
                            p.price
                        FROM 
                            invoices i
                        JOIN 
                            orders o ON i.order_id = o.order_id
                        JOIN 
                            clients c ON o.client_id = c.client_id
                        JOIN 
                            invoice_items ii ON i.invoice_id = ii.invoice_id
                        JOIN 
                            products p ON ii.product_id = p.product_id";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];
            DataGridView1.DataSource = dt;
            DataGridView1.Columns[0].HeaderText = "ID";
            DataGridView1.Columns[1].HeaderText = "Дата";
            DataGridView1.Columns[2].HeaderText = "ФИО";
            DataGridView1.Columns[3].HeaderText = "Адрес";
            DataGridView1.Columns[4].HeaderText = "Название";
            DataGridView1.Columns[5].HeaderText = "Цена";
            this.StartPosition = FormStartPosition.CenterScreen;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            addbill f = new addbill(con);
            f.ShowDialog();
            Update();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = (int)DataGridView1.CurrentRow.Cells["invoice_id"].Value;
            NpgsqlCommand command = new NpgsqlCommand("Delete from invoices where invoice_id =:id", con);
            command.Parameters.AddWithValue("id", id);
            command.ExecuteNonQuery();
            Update();
        }
    }
}
