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
    public partial class ogrgiris : Form
    {
        public ogrgiris()
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                try
                {
                    con = new SqlConnection("Data Source=.; Initial Catalog=yoklama;Integrated Security=True;");
                    cmd = new SqlCommand();
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM ogrenci where ogrenci_no='" + textBox1.Text + "'";
                    dr = cmd.ExecuteReader();
                    if(dr.Read())
                    {
                        ogrenciekran ogrenciekrn = new ogrenciekran();
                        ogrenciekrn.ogrenci_no = textBox1.Text;
                        ogrenciekrn.label2.Text = textBox1.Text;
                        ogrenciekrn.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Öğrenci Numaranızı Kontrol Ediniz...");
                    }
                    con.Close();
                }
                catch
                {
                    MessageBox.Show("Öğrenci Numaranızı Kontrol Ediniz...");
                }
            }
            else
            {
                MessageBox.Show("Gerekli Alanı Doldurunuz...");
            }
        }
    }
}
