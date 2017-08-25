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
    public partial class Products_Report : Form
    {
        public Products_Report()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            LoadData();
        }

        private void Products_Report_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadDate();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            LoadDate();
        }

        public void LoadData()
        {
            MySqlConnection con = new MySqlConnection("server = localhost; user id = root; database = stock; password= ratan");
            con.Open();
            MySqlDataAdapter adp = new MySqlDataAdapter("select ProductCode, ProductName from product", con);
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

        public void LoadDate()
        {
            label3.Text = DateTime.Now.ToShortDateString();
        }
    }


}
