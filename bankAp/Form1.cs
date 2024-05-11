using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace bankAp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public String conStr = "Data Source=LAPTOP-L92RK5AB\\SQLEXPRESS;Initial Catalog=connection;Integrated Security=True";
        private void button2_Click(object sender, EventArgs e)
        {
            
            if (Check_id())
            {
                SqlConnection con = new SqlConnection(conStr);
                con.Open();
                String q = "insert into Table1(id,name)values('" + textBox1.Text.ToString() + "','" + textBox2.Text.ToString() + "')";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Kayıt Başarılı!");
            }
        }
        private bool Check_id()
        {
            SqlConnection con = new SqlConnection(conStr);
            String q = "SELECT * FROM Table1 Where id = '" + textBox1.Text.ToString() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(q, con);
            DataTable dtbl = new DataTable();
            sda.Fill(dtbl);
            if (dtbl.Rows.Count > 0)
            {
                MessageBox.Show("This Id Used Before!");
                return false;
            }
            else
            {                
                return true;             
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                String q = "SELECT * FROM Table1 Where id = '" + textBox1.Text.ToString() + "' AND name = '" + textBox2.Text.ToString() + "'";
                SqlDataAdapter sda = new SqlDataAdapter(q, con);
                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);
                if (dtbl.Rows.Count > 0)
                {
                    Form2 objm = new Form2(textBox1.Text.ToString());
                    this.Hide();
                    objm.Show();
                }
                else
                {
                    MessageBox.Show("Wrong ıd or password!");
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
