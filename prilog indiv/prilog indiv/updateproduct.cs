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
    public partial class updateproduct : Form
    {
        public NpgsqlConnection con;
        public int id;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();

        public updateproduct(NpgsqlConnection con,int id)
        {
            InitializeComponent();
            this.con = con;
            this.id = id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("UPDATE products SET name = @name, price = @price, unit = @unit " +
                    "WHERE product_id = @id", con);
                command.Parameters.AddWithValue("name", textBox1.Text);
                command.Parameters.AddWithValue("price", Convert.ToDouble(textBox2.Text));
                command.Parameters.AddWithValue("unit", textBox3.Text);
                command.Parameters.AddWithValue("@id", id);
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
