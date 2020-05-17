using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using System.Data.SqlClient;

namespace WindowsFormsApp56
{
    public partial class raporlama : Form
    {
        string DosyaYolu;
        public raporlama()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosya = new OpenFileDialog();
            dosya.Title = "Rapor";
            dosya.ShowDialog();
            DosyaYolu = dosya.FileName;
            label3.Text = "Dosya Eklendi";
        }

        private void button2_Click(object sender, EventArgs e)
        {          
            try
            {
                 string kime = comboBox1.Text;
                 string konu = textBox2.Text;
                string icerik = textBox3.Text;
                SmtpClient mailClient = new SmtpClient("smtp.live.com", 587); //Bu kısma port nosunu yazın
                NetworkCredential cred = new NetworkCredential("pokemon_cagatay_364@hotmail.com", "2C2A4G2A8T2A9Y");
                mailClient.Credentials = cred;

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("pokemon_cagatay_364@hotmail.com");
                mail.Subject = "Başlık";
                mail.IsBodyHtml = true;
                mail.Body = "";
                mailClient.EnableSsl = true;
                mail.To.Add(kime);
                mail.Subject = konu;
                mail.Body = icerik;
                mail.Attachments.Add(new Attachment(DosyaYolu));      
                mailClient.Send(mail);
                MessageBox.Show("Mail başarıyla gönderilmiştir.!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Bir sorun oluştu.");
            }
        }

        private void raporlama_Load(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection();
            baglanti.ConnectionString = "Data Source=.; Initial Catalog=yoklama;Integrated Security=True;";
            SqlCommand komut = new SqlCommand();
            komut.CommandText = "SELECT mail FROM ogretmen";
            komut.Connection = baglanti;
            komut.CommandType = CommandType.Text;

            SqlDataReader dr;
            baglanti.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["mail"]);
            }
            baglanti.Close();
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
    }

