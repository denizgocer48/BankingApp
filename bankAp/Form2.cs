using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using ZXing;

namespace bankAp
{
    public partial class Form2 : Form
    {
        String conStr = "Data Source=LAPTOP-L92RK5AB\\SQLEXPRESS;Initial Catalog=connection;Integrated Security=True";
        SqlConnection con;
        public string Id = "";
        public Form2(string id)
        {
            InitializeComponent();
            Id = id; 
            // Open SQL connection
            con = new SqlConnection(conStr);
            con.Open();
            if (con.State == ConnectionState.Open)
            {
                String q = "SELECT money FROM Money WHERE id = @id";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);
                if (dtbl.Rows.Count > 0)
                {
                    // Display money in the text box
                    textBox1.Text = dtbl.Rows[0]["money"].ToString();
                }
                else
                {
                    MessageBox.Show("User not found!");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 frm = new Form3();
            this.Hide();
            frm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(Id);
            this.Hide();
            form4.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 frm = new Form3();
            this.Hide();
            frm.Show();
        }
    }
}
