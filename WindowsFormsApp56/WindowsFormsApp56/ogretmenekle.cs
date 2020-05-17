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
    public partial class ogretmenekle : Form
    {
        public ogretmenekle()
        {
            InitializeComponent();
        }
        string cinsiyet;
       SqlConnection baglanti = new SqlConnection("Data Source=.; Initial Catalog=yoklama;Integrated Security=True;");
        private string cnsyt;
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox4.Text != "" && textBox3.Text != "")
            {
                SqlConnection baglanti = new SqlConnection();
                baglanti.ConnectionString = "Data Source=.; Initial Catalog=yoklama;Integrated Security=True;";
                try
                {

                    baglanti.Open();
                    string text = "INSERT INTO ogretmen (ogretmen_adi,ogretmen_cinsiyet,ogretmen_no,sifre,mail) VALUES ('" + textBox1.Text + "','" + cinsiyet + "','" + Convert.ToInt32(textBox3.Text) + "','" + textBox4.Text + "','" + textBox12.Text + "')";
                    SqlCommand sorgu = new SqlCommand(text, baglanti);
                    sorgu.ExecuteNonQuery();                    
                    MessageBox.Show("Kayıt Eklendi");
                    baglanti.Close();
                    textBox1.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox12.Text = "";
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                }
                catch
                {
                    MessageBox.Show("Ekleme işlemi Yapılamadı");
                }
            }
            else
            {
                MessageBox.Show("Lütfen bilgileri eksiksiz doldurun...", "EKSİK ALAN UYARISI", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            cinsiyet = "Erkek";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            cinsiyet = "Kadın";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                SqlCommand sorgu = new SqlCommand("Delete From ogretmen where ogretmen_no=" + Convert.ToInt32(textBox2.Text) + "", baglanti);
                try
                {
                    baglanti.Open();
                    sorgu.ExecuteNonQuery();
                    MessageBox.Show("Kayıt Silindi");
                    baglanti.Close();
                    textBox2.Text = "";
                    textBox11.Text = "";
                    textBox10.Text = "";
                    textBox14.Text = "";
                    radioButton6.Checked = false;
                    radioButton5.Checked = false;
                    textBox9.Text = "";
                }
                catch
                {
                    MessageBox.Show("Silme işlemi Yapılamadı");
                }
             }
            else
            {
                MessageBox.Show("Gerekli Alanları Dolduruz....");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox8.Text != "" && textBox7.Text != "" && textBox6.Text != "")
            {
                try
                {
                    baglanti.Open();
                    string kayit = "UPDATE ogretmen SET ogretmen_adi='"+ textBox8.Text + "' , ogretmen_no='"+ textBox7.Text + "' , ogretmen_cinsiyet='"+ cnsyt + "' , sifre='"+ textBox6.Text + "' , mail='"+ textBox13.Text + "'  where ogretmen_no='"+ textBox5.Text + "'";
                    SqlCommand komutt = new SqlCommand(kayit, baglanti);
                    komutt.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Öğretmen Bilgileri Güncellendi.");
                    textBox8.Text = "";
                    textBox7.Text = "";
                    textBox6.Text = "";
                    textBox13.Text = "";
                    radioButton3.Checked = false;
                    radioButton4.Checked = false;
                }
                catch
                {
                    MessageBox.Show("Güncelleme işlemi Yapılamadı");

                }
            }
            else
            {
                MessageBox.Show("Gerekli Alanları Dolduruz....");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            yetkiliekran yetkiliekrn = new yetkiliekran();
            yetkiliekrn.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection();
            baglanti.ConnectionString = "Data Source=.; Initial Catalog=yoklama;Integrated Security=True;";
            baglanti.Open();
            string kayit = "SELECT * from ogretmen where ogretmen_no=@ogretmen_no";
            SqlCommand komut = new SqlCommand(kayit, baglanti);
            komut.Parameters.AddWithValue("@ogretmen_no", textBox5.Text);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read()) 
            {
                textBox8.Text = dr["ogretmen_adi"].ToString();
                textBox7.Text = dr["ogretmen_no"].ToString();
                if (radioButton3.Text == dr["ogretmen_cinsiyet"].ToString())
                {
                    radioButton3.Checked = true;
                    radioButton4.Checked = false;
                }
                else
                {
                    radioButton4.Checked = true;
                    radioButton3.Checked = false;
                }
                textBox6.Text = dr["sifre"].ToString();
                textBox13.Text = dr["mail"].ToString();
            }
            else
                MessageBox.Show("Öğretmen Bulunamadı.");
            baglanti.Close();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            cnsyt = "Erkek";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            cnsyt = "Kadın";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection();
            baglanti.ConnectionString = "Data Source=.; Initial Catalog=yoklama;Integrated Security=True;";
            baglanti.Open();
            string kayit = "SELECT * from ogretmen where ogretmen_no=@ogretmen_no";
            SqlCommand komut = new SqlCommand(kayit, baglanti);
            komut.Parameters.AddWithValue("@ogretmen_no", textBox2.Text);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read()) 
            {
                textBox11.Text = dr["ogretmen_adi"].ToString();
                textBox10.Text = dr["ogretmen_no"].ToString();
                if (radioButton5.Text == dr["ogretmen_cinsiyet"].ToString())
                {
                    radioButton5.Checked = true;
                    radioButton6.Checked = false;
                }
                else
                {
                    radioButton4.Checked = true;
                    radioButton6.Checked = false;
                }
                textBox9.Text = dr["sifre"].ToString();
                textBox14.Text = dr["mail"].ToString();
            }
            else
                MessageBox.Show("Öğretmen Bulunamadı.");
            baglanti.Close();
        }
    }
}
