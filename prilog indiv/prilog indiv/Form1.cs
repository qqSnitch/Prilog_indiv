using Microsoft.Office.Interop.Excel;
using Microsoft.VisualBasic.ApplicationServices;
using Npgsql;
using System.ComponentModel;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using OfficeOpenXml;
using System.IO;
using System;
using Series = System.Windows.Forms.DataVisualization.Charting.Series;
namespace prilog_indiv
{
    public partial class Form1 : Form
    {
        public NpgsqlConnection con;
        System.Data.DataTable dt = new System.Data.DataTable();
        DataSet ds = new DataSet();

        public void MyLoad()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            con = new NpgsqlConnection("Server=localhost;Port=5432;UserID=postgres;Password=12345;Database=indiv");
            con.Open();
        }
        public Form1()
        {
            InitializeComponent();
            MyLoad();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            products f = new products(con);
            f.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            orders f = new orders(con);
            f.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bill f = new bill(con);
            f.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            client f = new client(con);
            f.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            String sql = @"SELECT 
                            o.order_id as ID,
                            c.name as Имя,
                            o.order_date as Дата,
                            o.total_amount as Сумма
                        FROM orders o
                        JOIN clients c ON o.client_id = c.client_id;";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];
            ExcelPackage.License.SetNonCommercialPersonal("Kirill");
            using (var package = new ExcelPackage())  
            {
                var worksheet = package.Workbook.Worksheets.Add("Data");

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1].Value = dt.Columns[i].ColumnName;
                }

                for (int row = 0; row < dt.Rows.Count; row++)
                {
                    for (int col = 0; col < dt.Columns.Count; col++)
                    {
                        worksheet.Cells[row + 2, col + 1].Value = dt.Rows[row][col];
                    }
                }

                var file = new FileInfo(@"D:\Export.xlsx");
                package.SaveAs(file);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DateTime date1 = dateTimePicker1.Value.Date;
            DateTime date2 = dateTimePicker2.Value.Date;

            string sql = @"
                        SELECT 
                        COUNT(invoice_id) AS invoice_count,
                        SUM(total_amount) AS total_sum
                        FROM invoices
                        WHERE invoice_date BETWEEN @date1 AND @date2";

            using (NpgsqlCommand command = new NpgsqlCommand(sql, con))
            {
                command.Parameters.AddWithValue("@date1", date1);
                command.Parameters.AddWithValue("@date2", date2);

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(command);
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    chart1.Series.Clear();

                    Series seriesSum = new Series("Сумма заказов");
                    seriesSum.ChartType = SeriesChartType.Column;
                    seriesSum.IsValueShownAsLabel = true;
                    seriesSum.Color = Color.SteelBlue;

                    Series seriesDeliveries = new Series("Количество доставок");
                    seriesDeliveries.ChartType = SeriesChartType.Column;
                    seriesDeliveries.IsValueShownAsLabel = true;
                    seriesDeliveries.Color = Color.Orange;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string monthLabel = date1.AddMonths(i).ToString("MMM yyyy");
                        decimal sum = Convert.ToDecimal(dt.Rows[i]["total_sum"]);
                        int deliveries = Convert.ToInt32(dt.Rows[i]["invoice_count"]);

                        seriesSum.Points.AddXY(monthLabel, sum);
                        seriesDeliveries.Points.AddXY(monthLabel, deliveries);
                    }

                    chart1.Series.Add(seriesSum);
                    chart1.Series.Add(seriesDeliveries);

                    chart1.ChartAreas[0].AxisX.Title = "Месяц";
                    chart1.ChartAreas[0].AxisY.Title = "Сумма / Количество";
                    chart1.ChartAreas[0].AxisX.Interval = 1;
                    chart1.ChartAreas[0].AxisY2.Enabled = AxisEnabled.False;
                    chart1.Legends[0].Enabled = true;
                    chart1.Titles.Clear();
                    chart1.Titles.Add($"Отчет за период с {date1:dd.MM.yyyy} по {date2:dd.MM.yyyy}");
                }
                else
                {
                    MessageBox.Show("Нет данных для отображения за выбранный период");
                }
            }
        }


        
    }
}
