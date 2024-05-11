using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bankAp
{
    public partial class Form4 : Form
    {
        public int newkredi = 0;
        public string Id = "";
        String conStr = "Data Source=LAPTOP-L92RK5AB\\SQLEXPRESS;Initial Catalog=connection;Integrated Security=True";
        SqlConnection con;
        public Form4(string id)
        {
            Id = id;
            InitializeComponent();
            con = new SqlConnection(conStr);
            con.Open();
            if (con.State == ConnectionState.Open)
            {
                String q = "SELECT kullanılabilir FROM Kredi WHERE id = @id"; 
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);
                if (dtbl.Rows.Count > 0)
                {
                    // Display money in the text box
                    textBox1.Text = dtbl.Rows[0]["kullanılabilir"].ToString();
                }
                else
                {
                    MessageBox.Show("User not found!");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.Text.ToString().Equals("Öde"))
            {
                bool b = Check_ode();
                if (b)
                {
                    string updateQuery = "UPDATE Kredi SET kullanılabilir = @kullanılabilir WHERE id = @id";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                    updateCmd.Parameters.AddWithValue("@kullanılabilir", newkredi);
                    updateCmd.Parameters.AddWithValue("@id", Id);                    

                    int rowsAffected = updateCmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Kredi kaydı güncellendi.");
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı bulunamadı veya güncelleme yapılamadı!");
                    }
                }             
                //Form1 form1 = new Form1();
                //this.Hide();
                //form1.Show();
            }
            else if (button4.Text.ToString().Equals("Çek"))
            {
                bool b = Check_cek();
                if (b)
                {
                    string updateQuery = "UPDATE Kredi SET kullanılabilir = @kullanılabilir WHERE id = @id";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                    updateCmd.Parameters.AddWithValue("@kullanılabilir", newkredi);
                    updateCmd.Parameters.AddWithValue("@id", Id);

                    int rowsAffected = updateCmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        //MessageBox.Show("Kredi kaydı güncellendi.");
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı bulunamadı veya güncelleme yapılamadı!");
                    }
                }
                Form1 form2 = new Form1();
                this.Hide();
                form2.Show();
            }
        }
        private bool Check_ode()
        {
            String q = "SELECT kullanılabilir FROM Kredi WHERE id = @Id";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@Id", Id);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dtbl = new DataTable();
            sda.Fill(dtbl);

            String q2 = "SELECT maxmoney FROM Kredi WHERE id = @Id";
            SqlCommand cmd2 = new SqlCommand(q2, con);
            cmd2.Parameters.AddWithValue("@Id", Id);
            SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
            DataTable dtbl2 = new DataTable();
            sda2.Fill(dtbl2);
            int a = Convert.ToInt32(dtbl.Rows[0]["kullanılabilir"].ToString());
            int b = Convert.ToInt32(dtbl2.Rows[0]["maxmoney"].ToString());
            int c = Convert.ToInt32(textBox2.Text.ToString());

            String q1 = "SELECT money FROM Money WHERE id = @Id";
            SqlCommand cmd1 = new SqlCommand(q1, con);
            cmd1.Parameters.AddWithValue("@Id", Id);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dtbl1 = new DataTable();
            sda1.Fill(dtbl1);
            int new_money = Convert.ToInt32(dtbl1.Rows[0]["money"].ToString()) - c;
            if (new_money > 0)
            {
                string updateQuery2 = "UPDATE Money SET money = @money WHERE id = @Id";
                SqlCommand updateCmd2 = new SqlCommand(updateQuery2, con);
                updateCmd2.Parameters.AddWithValue("@money", new_money);
                updateCmd2.Parameters.AddWithValue("@Id", Id);
                int rowsAffected = updateCmd2.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Bakiye güncellendi. Yeni bakiye: " + new_money.ToString());
                }
                else
                {
                    MessageBox.Show("Kullanıcı bulunamadı veya güncelleme yapılamadı!");
                }
            }
            if (a < b && (b - a) > c)
            {
                newkredi = a + c;

                textBox3.Visible = true;
                label2.Visible = true; 
                textBox3.Text = (b - newkredi).ToString();
                return true;
            }
            else 
            { 
                return false;
            }
            
        }
        private bool Check_cek()
        {
            String q = "SELECT kullanılabilir FROM Kredi WHERE id = @Id";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@Id", Id);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dtbl = new DataTable();
            sda.Fill(dtbl);

            String q2 = "SELECT maxmoney FROM Kredi WHERE id = @Id";
            SqlCommand cmd2 = new SqlCommand(q2, con);
            cmd2.Parameters.AddWithValue("@Id", Id);
            SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
            DataTable dtbl2 = new DataTable();
            sda2.Fill(dtbl2);
            int a = Convert.ToInt32(dtbl.Rows[0]["kullanılabilir"].ToString());
            int b = Convert.ToInt32(dtbl2.Rows[0]["maxmoney"].ToString());
            int c = Convert.ToInt32(textBox2.Text.ToString());

            String q1 = "SELECT money FROM Money WHERE id = @Id";
            SqlCommand cmd1 = new SqlCommand(q1, con);
            cmd1.Parameters.AddWithValue("@Id", Id);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dtbl1 = new DataTable();
            sda1.Fill(dtbl1);
            int new_money = Convert.ToInt32(dtbl1.Rows[0]["money"].ToString()) + c;         

            if ((a - c) > 0)
            {
                string updateQuery2 = "UPDATE Money SET money = @money WHERE id = @Id";
                SqlCommand updateCmd2 = new SqlCommand(updateQuery2, con);
                updateCmd2.Parameters.AddWithValue("@money", new_money);
                updateCmd2.Parameters.AddWithValue("@Id", Id);
                int rowsAffected = updateCmd2.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Bakiye güncellendi. Yeni bakiye: " + new_money.ToString());
                }
                else
                {
                    MessageBox.Show("Kullanıcı bulunamadı veya güncelleme yapılamadı!");
                }
                newkredi = a - c;
                return true;
            }
            else
            {
                return false;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            button4.Text = "Çek";
            textBox2.Visible = true;
            button4.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button4.Text = "Öde";
            textBox2.Visible = true;
            button4.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            MessageBox.Show("Yeniden Giriş Yapmanız Gerekmektedir!");
            Form1 form2 = new Form1();
            this.Hide();
            form2.Show();
        }
    }
}
