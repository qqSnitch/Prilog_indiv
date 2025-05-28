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
    public partial class addclient : Form
    {
        public NpgsqlConnection con;
        public addclient(NpgsqlConnection con)
        {
            InitializeComponent();
            this.con = con;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO Clients (name, address, age, phone) VALUES (:name,:address, :age, :phone)", con);
                command.Parameters.AddWithValue("name", textBox1.Text);
                command.Parameters.AddWithValue("address", textBox2.Text);
                command.Parameters.AddWithValue("age", Convert.ToDouble(textBox3.Text));
                command.Parameters.AddWithValue("phone", textBox4.Text);
                command.ExecuteNonQuery();
                Close();
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
