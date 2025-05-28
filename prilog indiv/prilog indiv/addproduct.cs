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
    public partial class addproduct : Form
    {
        public NpgsqlConnection con;

        public addproduct(NpgsqlConnection con)
        {
            InitializeComponent();
            this.con = con;

        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO Products (name, price, unit) VALUES (:name,:price, :unit)", con);
                command.Parameters.AddWithValue("name", textBox1.Text);
                command.Parameters.AddWithValue("price", Convert.ToDouble(textBox2.Text));
                command.Parameters.AddWithValue("unit", textBox3.Text);
                command.ExecuteNonQuery();
                Close();
            }
            catch { }
        }

        private void buttonNo_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
