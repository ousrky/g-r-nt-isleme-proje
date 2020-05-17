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
    public partial class ogrenciekle : Form
    {
        public ogrenciekle()
        {
            InitializeComponent();
        }
               SqlConnection baglanti = new SqlConnection("Data Source=.; Initial Catalog=yoklama;Integrated Security=True;");

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            yetkiliekran yetkiliekrn = new yetkiliekran();
            yetkiliekrn.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
        string cinsiyet;
        private string cnsyt;

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text!="" && textBox2.Text!="" && textBox3.Text!="" && comboBox1.Text!="")
            {
                SqlConnection baglanti = new SqlConnection();
                baglanti.ConnectionString = "Data Source=.; Initial Catalog=yoklama;Integrated Security=True;";
                SqlCommand komut = new SqlCommand();
                komut.CommandText = "SELECT * FROM siniff where sinif_adi='" + comboBox1.Text + "'";
                komut.Connection = baglanti;
                komut.CommandType = CommandType.Text;
                int sinifid = 0;
                SqlDataReader dr;
                baglanti.Open();
                dr = komut.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    sinifid = Convert.ToInt32(dr.GetValue(0));
                }
                baglanti.Close();
                try
                {
                    baglanti.Open();
                    string text = "INSERT INTO ogrenci (ogrenci_adi,ogrenci_no,ogrenci_cinsiyet,ogrenci_tel,sinifid) VALUES ('" + textBox1.Text + "','" + Convert.ToUInt64(textBox2.Text) + "','" + cinsiyet + "','" + textBox3.Text + "','" + Convert.ToInt32(sinifid) + "')";
                    SqlCommand sorgu = new SqlCommand(text, baglanti);
                    sorgu.ExecuteNonQuery();
                    MessageBox.Show("Kayıt Eklendi");
                    baglanti.Close();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    comboBox1.Text = "";
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    button2.Enabled = false;
                    button1.Enabled = true;
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
            cinsiyet = "Kız";
        }

        private void ogrenciekle_Load(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection();
            baglanti.ConnectionString = "Data Source=.; Initial Catalog=yoklama;Integrated Security=True;";
            SqlCommand komut = new SqlCommand();
            komut.CommandText = "SELECT sinif_adi FROM siniff";
            komut.Connection = baglanti;
            komut.CommandType = CommandType.Text;
            SqlDataReader dr;
            baglanti.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr.GetValue(0));
                comboBox2.Items.Add(dr.GetValue(0));

            }
            baglanti.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection();
            baglanti.ConnectionString = "Data Source=.; Initial Catalog=yoklama;Integrated Security=True;";
            baglanti.Open();
            string sinif = "SELECT sinif_adi from siniff where id IN (SELECT sinifid from ogrenci where ogrenci_no='"+textBox7.Text+"')";
            SqlCommand kmt = new SqlCommand(sinif, baglanti);
            SqlDataAdapter daa = new SqlDataAdapter(kmt);
            SqlDataReader drr = kmt.ExecuteReader();
           if( drr.Read())
             comboBox2.Text=drr["sinif_adi"].ToString();
            baglanti.Close();


            baglanti.Open();
            string kayit = "SELECT * from ogrenci where ogrenci_no=@ogrenci_no";
            SqlCommand komut = new SqlCommand(kayit, baglanti);
            komut.Parameters.AddWithValue("@ogrenci_no", textBox7.Text);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                textBox6.Text = dr["ogrenci_adi"].ToString();
                textBox5.Text = dr["ogrenci_no"].ToString();
                if (radioButton3.Text == dr["ogrenci_cinsiyet"].ToString())
                {
                    radioButton3.Checked = true;
                    radioButton4.Checked = false;
                }
                else
                {
                    radioButton4.Checked = true;
                    radioButton3.Checked = false;
                }
                textBox4.Text = dr["ogrenci_tel"].ToString();
                button3.Enabled = true;
                button5.Enabled = false;
            }
            else
            {
                MessageBox.Show("Öğrenci Bulunamadı.");                
            }
            baglanti.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox6.Text != "" && textBox5.Text != "" && textBox4.Text != "" && comboBox2.Text != "")
            {
                SqlConnection baglanti = new SqlConnection();
                baglanti.ConnectionString = "Data Source=.; Initial Catalog=yoklama;Integrated Security=True;";
                SqlCommand komut = new SqlCommand();
                komut.CommandText = "SELECT * FROM siniff where sinif_adi='" + comboBox2.Text + "'";
                komut.Connection = baglanti;
                komut.CommandType = CommandType.Text;
                int sinifid = 0;
                SqlDataReader dr;
                baglanti.Open();
                dr = komut.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    sinifid = Convert.ToInt32(dr.GetValue(0));
                }
                baglanti.Close();

                try
                {
                    baglanti.Open();
                    string kayit = "UPDATE ogrenci SET ogrenci_adi=@ogrenciadi , ogrenci_no=@ogrencinoo , ogrenci_cinsiyet=@ogrencicinsiyet , ogrenci_tel=@ogrencitel , sinifid=@sinif where ogrenci_no=@ogrencino";
                    SqlCommand komutt = new SqlCommand(kayit, baglanti);
                    komutt.Parameters.AddWithValue("@ogrenciadi", textBox6.Text);
                    komutt.Parameters.AddWithValue("@ogrencinoo", textBox5.Text);
                    komutt.Parameters.AddWithValue("@ogrencino", textBox7.Text);
                    komutt.Parameters.AddWithValue("@ogrencicinsiyet", cnsyt);
                    komutt.Parameters.AddWithValue("@ogrencitel", textBox4.Text);
                    komutt.Parameters.AddWithValue("@sinif", sinifid);
                    komutt.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Öğrenci Bilgileri Güncellendi.");
                    textBox6.Text = "";
                    textBox5.Text = "";
                    textBox4.Text = "";
                    comboBox2.Text = "";
                    radioButton3.Checked = false;
                    radioButton4.Checked = false;
                    button5.Enabled = true;
                    button4.Enabled = false;
                }
                catch
                {

                    MessageBox.Show("Güncelleme işlemi Yapılamadı");
                }
            }
            else
            {
                MessageBox.Show("Lütfen bilgileri eksiksiz doldurun...", "EKSİK ALAN UYARISI", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            cnsyt = "Erkek";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            cnsyt = "Kız";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            yuz_ekle yuzekle = new yuz_ekle();
            yuzekle.textBox1.Text = textBox6.Text;
            yuzekle.txtFaceName.Text = textBox5.Text;
            yuzekle.Show();
            button4.Enabled = true;
            button3.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            yuz_ekle yuzekle = new yuz_ekle();
            yuzekle.textBox1.Text = textBox1.Text;
            yuzekle.txtFaceName.Text = textBox2.Text;
            yuzekle.Show();
            button2.Enabled = true;
            button1.Enabled = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection();
            baglanti.ConnectionString = "Data Source=.; Initial Catalog=yoklama;Integrated Security=True;";
            baglanti.Open();
            string sinif = "SELECT sinif_adi from siniff where id IN (SELECT sinifid from ogrenci where ogrenci_no='" + textBox8.Text + "')";
            SqlCommand kmt = new SqlCommand(sinif, baglanti);
            SqlDataAdapter daa = new SqlDataAdapter(kmt);
            SqlDataReader drr = kmt.ExecuteReader();
            if (drr.Read())
                comboBox3.Text = drr["sinif_adi"].ToString();
            baglanti.Close();


            baglanti.Open();
            string kayit = "SELECT * from ogrenci where ogrenci_no=@ogrenci_no";
            SqlCommand komut = new SqlCommand(kayit, baglanti);
            komut.Parameters.AddWithValue("@ogrenci_no", textBox8.Text);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read()) 
            {
                textBox11.Text = dr["ogrenci_adi"].ToString();
                textBox10.Text = dr["ogrenci_no"].ToString();
                if (radioButton5.Text == dr["ogrenci_cinsiyet"].ToString())
                {
                    radioButton5.Checked = true;
                    radioButton6.Checked = false;
                }
                else
                {
                    radioButton6.Checked = true;
                    radioButton5.Checked = false;
                }
                textBox9.Text = dr["ogrenci_tel"].ToString();
                //Datareader ile okunan verileri form kontrollerine aktardık.
            }
            else
                MessageBox.Show("Öğrenci Bulunamadı.");
            baglanti.Close();
            button7.Enabled = true;
            button6.Enabled = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection("Data Source=.; Initial Catalog=yoklama;Integrated Security=True;");
            if (textBox8.Text != "")
            {
                SqlCommand sorgu = new SqlCommand("DELETE From ogrenci where ogrenci_no=" + Convert.ToUInt64(textBox8.Text) + "", baglanti);

                try
                {
                    baglanti.Open();
                    sorgu.ExecuteNonQuery();
                    MessageBox.Show("Kayıt Silindi");
                    baglanti.Close();
                    textBox8.Text = "";
                    textBox11.Text = "";
                    textBox10.Text = "";
                    radioButton6.Checked = false;
                    radioButton5.Checked = false;
                    textBox9.Text = "";
                    comboBox3.Text = "";
                    button7.Enabled = false;
                    button6.Enabled = true;

                }
                catch
                {
                    textBox2.Text = "";
                    MessageBox.Show("Silme işlemi Yapılamadı");
                }
            }
            else
            {
                MessageBox.Show("Lütfen bilgileri eksiksiz doldurun...", "EKSİK ALAN UYARISI", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}
