using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Stock
{
    public partial class Stock : Form
    {
        public Stock()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void Stock_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadData1();
            TotalProduct();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            TotalProduct();
         }
        public void TotalProduct()
        {
            MySqlConnection con = new MySqlConnection("server = localhost; user id = root; database = stock; password= ratan");
            con.Open(); 
            MySqlDataAdapter adp = new MySqlDataAdapter("select count(DISTINCT ProductCode) from stock", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
           
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            MySqlConnection con = new MySqlConnection("server = localhost; user id = root; database = stock; password= admin");
            con.Open();
            MySqlDataAdapter adp = new MySqlDataAdapter("select ProductCode,ProductName from product", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            dataGridView1.Rows.Clear();  //before loading clear the record otherwise previous data will also come so i will become duplicate type
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["ProductCode"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["ProductName"].ToString();

            }

        }


        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
        }



        private void button1_Click(object sender, EventArgs e)
        {

            MySqlConnection con = new MySqlConnection("server = localhost; user id = root; database = stock; password= ratan");
            con.Open();
            string theDate = dateTimePicker1.Value.ToString("yyyy-MM-dd hh-mm-ss");

            var mysqlQuery = "";
            if (IfProductsExists(con, textBox1.Text))
            {
                mysqlQuery = @"Update stock set ProductName ='" + textBox2.Text + "', TransDate ='" + theDate + "', Quantity ='" +textBox3.Text+ "' where ProductCode ='" + textBox1.Text + "' ";
            }
            else
            {
                mysqlQuery = @"insert into stock values('" + textBox1.Text + "','" + textBox2.Text + "','" + theDate + "','" + textBox3.Text + "')";
            }
            MySqlCommand cmd = new MySqlCommand(mysqlQuery, con);
            cmd.ExecuteNonQuery();
            con.Close();
            LoadData();
            LoadData1();

        }
        private bool IfProductsExists(MySqlConnection con, string ProductCode)
        {
            MySqlDataAdapter adp = new MySqlDataAdapter("select 1 from stock where ProductCode='" + ProductCode + "'", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadData1();
        }

        public void LoadData1()
        {
            MySqlConnection con = new MySqlConnection("server = localhost; user id = root; database = stock; password= ratan");
            con.Open();
            MySqlDataAdapter adp = new MySqlDataAdapter("select * from stock", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            dataGridView2.Rows.Clear();  //before loading clear the record otherwise previous data will also come so i will become duplicate type
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView2.Rows.Add();
                dataGridView2.Rows[n].Cells[0].Value = item["ProductCode"].ToString();
                dataGridView2.Rows[n].Cells[1].Value = item["ProductName"].ToString();
                dataGridView2.Rows[n].Cells[2].Value = item["Quantity"].ToString();
                dataGridView2.Rows[n].Cells[3].Value = item["TransDate"].ToString();

            }
        }

    }
}
