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
    public partial class Stock_Report : Form
    {
        public Stock_Report()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }


        private void label7_Click(object sender, EventArgs e)
        {
            LoadDate();    
        }
        public void LoadDate()
        {
        label7.Text = DateTime.Now.ToShortDateString();
        }



        private void Stock_Report_Load(object sender, EventArgs e)
        {
            LoadDate();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            
            MySqlConnection con = new MySqlConnection("server = localhost; user id = root; database = stock; password= admin");
            con.Open();
            string theDate1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string theDate2 = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            MySqlDataAdapter adp = new MySqlDataAdapter("select ProductCode, ProductName, Quantity from stock where TransDate between '"+theDate1+"' and '"+theDate2+"'", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            dataGridView1.Rows.Clear();  //before loading clear the record otherwise previous data will also come so i will become duplicate type
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["ProductCode"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["ProductName"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["Quantity"].ToString();

            }
            label8Date();
            label9Date();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            label8Date();
        }

        public void label8Date()
        {
            label8.Text = dateTimePicker1.Value.ToString();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            label9Date();
        }
        public void label9Date()
        {
            label9.Text = dateTimePicker2.Value.ToString();
        }
    }
}
