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
using static System.Windows.Forms.DataFormats;

namespace prilog_indiv
{
    public partial class products : Form
    {
        public NpgsqlConnection con;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();

        public products(NpgsqlConnection con)
        {
            InitializeComponent();
            this.con = con;
            Update();
        }

        public void Update()
        {
            String sql = "Select * from Products";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];
            DataGridView1.DataSource = dt;
            DataGridView1.Columns[0].HeaderText = "Номер";
            DataGridView1.Columns[1].HeaderText = "Наименование";
            DataGridView1.Columns[2].HeaderText = "Цена";
            DataGridView1.Columns[3].HeaderText = "Ед изменения";
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addproduct f = new addproduct(con);
            f.ShowDialog();
            Update();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = (int)DataGridView1.CurrentRow.Cells["product_id"].Value;
            NpgsqlCommand command = new NpgsqlCommand("Delete from Products where product_id =:id", con);
            command.Parameters.AddWithValue("id", id);
            0command.ExecuteNonQuery();
            Update();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = (int)DataGridView1.CurrentRow.Cells["product_id"].Value;
            updateproduct f = new updateproduct(con,id);
            f.ShowDialog();
            Update();
        }
    }
}
