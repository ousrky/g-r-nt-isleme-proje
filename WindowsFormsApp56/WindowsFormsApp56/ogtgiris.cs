using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp56
{

    public partial class ogtgiris : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public ogtgiris()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            anaekran anakrn = new anaekran();
            anakrn.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //checkBox işaretli ise
            if (checkBox1.Checked)
            {
                //karakteri göster.
                textBox2.PasswordChar = '\0';
            }
            //değilse karakterlerin yerine * koy.
            else
            {
                textBox2.PasswordChar = '*';
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {              
                try
                {                  
                    con = new SqlConnection("Data Source=.; Initial Catalog=yoklama;Integrated Security=True;");
                    cmd = new SqlCommand();
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM ogretmen where ogretmen_no='" + textBox1.Text + "' AND sifre='" + textBox2.Text + "'";
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        ogretmenekran ogrtmnekrn = new ogretmenekran();                      
                        ogrtmnekrn.ogretmen_no = textBox1.Text;
                        ogrtmnekrn.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı Adı veya Şifre yanlıştır.");
                    }
                }
                catch
                {
                    MessageBox.Show("Kullanıcı Adı veya Şifre yanlıştır.");
                }
                con.Close();
            }
            else
            {
                MessageBox.Show("Gerekli Alanları Doldurunuz...","BOŞ ALAN UYARISI",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
    }
}
