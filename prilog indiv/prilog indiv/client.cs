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
    public partial class client : Form
    {
        public NpgsqlConnection con;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        public client(NpgsqlConnection con)
        {
            InitializeComponent();
            this.con = con;
            Update();
        }
        public void Update()
        {
            String sql = "Select * from Clients";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];
            DataGridView1.DataSource = dt;
            DataGridView1.Columns[0].HeaderText = "Номер";
            DataGridView1.Columns[1].HeaderText = "ФИО";
            DataGridView1.Columns[2].HeaderText = "Адрес";
            DataGridView1.Columns[3].HeaderText = "Возраст";
            DataGridView1.Columns[4].HeaderText = "Телефон";
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addclient f = new addclient(con);
            f.ShowDialog();
            Update();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = (int)DataGridView1.CurrentRow.Cells["client_id"].Value;
            NpgsqlCommand command = new NpgsqlCommand("Delete from Clients where client_id =:id", con);
            command.Parameters.AddWithValue("id", id);
            command.ExecuteNonQuery();
            Update();
        }
    }
}
