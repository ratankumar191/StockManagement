using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
           
        }

          private void label2_Click(object sender, EventArgs e)
          {

          }

          private void textBox1_TextChanged(object sender, EventArgs e)
          {

          }

          private void button1_Click(object sender, EventArgs e)
          {
            textBox1.Text = "";
            textBox2.Clear();
            textBox1.Focus();
        }

          private void button2_Click(object sender, EventArgs e)
          {
            //to do validation from database 
            MySqlConnection con=new MySqlConnection("server = localhost; user id = root; database = stock; password= admin");
            con.Open();
            MySqlCommand cmd = new MySqlCommand("select * from login where username ='"+textBox1.Text+"' and password='"+textBox2.Text+"'",con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            if (dt.Rows.Count == 1)
            {
                this.Hide();
                StockMain main = new StockMain();
                main.Show();
            }
            else
            {
                MessageBox.Show("Invalid username & password","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                button1_Click(sender, e); // after clicking ok textbox will be reset
            }
            con.Close();
          }
       
    }
}
