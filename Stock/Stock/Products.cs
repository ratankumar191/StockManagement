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
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Products_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            LoadData();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection("server = localhost; user id = root; database = stock; password= ratan");
            con.Open();
            int status = 0;
            if (comboBox1.SelectedIndex == 0)
            {
                status = 1;
            }
            else
            {
                status = 0;
            }



            var mysqlQuery = "";
            if (IfProductsExists(con, textBox1.Text ))
            {
                mysqlQuery = "Update product set ProductName ='"+textBox2.Text+"', ProductStatus ='"+status+"' where ProductCode ='"+textBox1.Text+"' ";
            }
            else
            {
                mysqlQuery = @"insert into product values('" + textBox1.Text + "','" + textBox2.Text + "','" + status + "')";
            }
            MySqlCommand cmd = new MySqlCommand(mysqlQuery, con);
            cmd.ExecuteNonQuery();
            con.Close();

            // reading the data from database
            LoadData();
        }



        private bool IfProductsExists(MySqlConnection con, string ProductCode )
        {
            MySqlDataAdapter adp = new MySqlDataAdapter("select 1 from product where ProductCode='"+ProductCode+"'", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            if(dt.Rows.Count>0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        } 




        public void LoadData()
        {
            MySqlConnection con = new MySqlConnection("server = localhost; user id = root; database = stock; password= admin");
            MySqlDataAdapter adp = new MySqlDataAdapter("select * from product", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            dataGridView1.Rows.Clear();  //before loading clear the record
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["ProductCode"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["ProductName"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["ProductStatus"].ToString();

                
                /*if ((bool)item["ProductStatus"])
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Active";
                } 
                else
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Deactive";
                }*/
                

            }
        }



        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
           // comboBox1.SelectedText = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            if (dataGridView1.SelectedRows[0].Cells[2].Value.ToString()=="1")
            {
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                comboBox1.SelectedIndex = 1;
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection("server = localhost; user id = root; database = stock; password= ratan");
            var mysqlQuery = "";
            if (IfProductsExists(con, textBox1.Text))
            {
                con.Open();
                mysqlQuery = "delete from product where ProductCode ='" + textBox1.Text + "' ";
                MySqlCommand cmd = new MySqlCommand(mysqlQuery, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                MessageBox.Show("Record Not Exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

            // reading the data from database
            LoadData();
        }
    }
}
