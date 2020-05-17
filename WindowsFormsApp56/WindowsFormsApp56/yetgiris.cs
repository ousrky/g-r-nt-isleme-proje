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

namespace WindowsFormsApp56
{
    public partial class yetgiris : Form
    {
        public yetgiris()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
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
                    cmd.CommandText = "SELECT * FROM yetkili where yetkili_no='" + textBox1.Text + "' AND parola='" + textBox2.Text + "'";
                    dr = cmd.ExecuteReader();
                    if(dr.Read())
                    {
                        yetkiliekran yetkiliekrn = new yetkiliekran();
                        yetkiliekrn.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı Adı veya Şifre yanlıştır.");
                    }
                }
                catch
                {
                    MessageBox.Show("Kullanıcı adını ve şifrenizi kontrol ediniz....");
                }
                con.Close();
            }
            else
            {
                MessageBox.Show("Lütfen Gerekli Alanları Doldurunuz...");
            }
        }
    }
}
