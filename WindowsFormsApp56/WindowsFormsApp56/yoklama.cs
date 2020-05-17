using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp56
{
    public partial class yoklama : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=.; Initial Catalog=yoklama;Integrated Security=True;");
        string sinifid;
        string dersid;
        int hafta = 0;
        public yoklama()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }
        public string yuz;
        private async void btnEgit_Click_1(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    if (!recognition.SaveTrainingData(pictureBox2.Image, txtFaceName.Text)) MessageBox.Show("Hata", "Profil alınırken beklenmeyen bir hata oluştu.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Thread.Sleep(100);
                    lblEgitilenAdet.Text = (i + 1) + " adet profil.";
                }


                recognition = null;
                train = null;

                recognition = new BusinessRecognition("C:\\", "Faces", "yuz.xml");
                train = new Classifier_Train("C:\\", "Faces", "yuz.xml");
            });
        }

        BusinessRecognition recognition = new BusinessRecognition("C:\\", "Faces", "yuz.xml");
        Classifier_Train train = new Classifier_Train("C:\\", "Faces", "yuz.xml");

        private void yoklama_Load(object sender, EventArgs e)
        {

            if (label6.Text == "hafta1")
                hafta = 1;
            else if (label6.Text == "hafta2")
                hafta = 2;
            else if (label6.Text == "hafta3")
                hafta = 3;
            else if (label6.Text == "hafta4")
                hafta = 4;
            else if (label6.Text == "hafta5")
                hafta = 5;
            else if (label6.Text == "hafta6")
                hafta = 6;
            else if (label6.Text == "hafta7")
                hafta = 7;
            else if (label6.Text == "hafta8")
                hafta = 8;
            else if (label6.Text == "hafta9")
                hafta = 9;
            else if (label6.Text == "hafta10")
                hafta = 10;
            else if (label6.Text == "hafta11")
                hafta = 11;
            else if (label6.Text == "hafta12")
                hafta = 12;
            else if (label6.Text == "hafta13")
                hafta = 13;
            else if (label6.Text == "hafta14")
                hafta = 14;

            label2.Text = txtFaceName.Text;
            Capture capture = new Capture();
            capture.Start();
            capture.ImageGrabbed += (a, b) =>
            {
                var image = capture.RetrieveBgrFrame();
                var grayimage = image.Convert<Gray, byte>();
                HaarCascade haaryuz = new HaarCascade("haarcascade_frontalface_alt2.xml");
                MCvAvgComp[][] Yuzler = grayimage.DetectHaarCascade(haaryuz, 1.2, 5, HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(15, 15));
                MCvFont font = new MCvFont(FONT.CV_FONT_HERSHEY_COMPLEX, 0.5, 0.5);
                foreach (MCvAvgComp yuz in Yuzler[0])
                {
                    var sadeyuz = grayimage.Copy(yuz.rect).Convert<Gray, byte>().Resize(100, 100, INTER.CV_INTER_CUBIC);
                    //Resimler aynı boyutta olmalıdır. O yüzden Resize ile yeniden boyutlandırma yapılmıştır. Aksi taktirde Classifier_Train sınıfının 245. satırında hata alınacaktır.
                    pictureBox2.Image = sadeyuz.ToBitmap();
                    if (train != null)
                        if (train.IsTrained)
                        {
                            string name = train.Recognise(sadeyuz);
                            int match_value = (int)train.Get_Eigen_Distance;
                            image.Draw(name + " ", ref font, new Point(yuz.rect.X - 2, yuz.rect.Y - 2), new Bgr(Color.LightGreen));
                            label5.Text = name;
                        }
                    image.Draw(yuz.rect, new Bgr(Color.Red), 2);
                }
                yuz = label5.Text;
                pictureBox1.Image = image.ToBitmap();
            };

            baglanti.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = baglanti;
            cmd.CommandText = "SELECT * FROM siniff where sinif_adi='" + label4.Text + "'";
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                sinifid = dr.GetValue(0).ToString();
            }
            baglanti.Close();

            baglanti.Open();
            SqlCommand derskmt = new SqlCommand();
            derskmt.Connection = baglanti;
            derskmt.CommandText = "SELECT * FROM ders where ders_adi='" + label3.Text + "'";
            SqlDataReader dersoku = derskmt.ExecuteReader();
            if (dersoku.Read())
            {
                dersid = dersoku.GetValue(0).ToString();
            }
            baglanti.Close();

            baglanti.Open();
            string kayit = "SELECT ogrenci_no AS [Öğrenci Numarası], ogrenci_adi AS[Öğrenci Adı] FROM ogrenci where sinifid='" + sinifid + "'";
            SqlCommand kmt = new SqlCommand(kayit, baglanti);
            SqlDataAdapter dada = new SqlDataAdapter(kmt);
            DataTable dtdt = new DataTable();
            dada.Fill(dtdt);
            dataGridView1.DataSource = dtdt;
            baglanti.Close();

            yoklamaliste liste = new yoklamaliste();
            liste.listesinifid = sinifid;
            liste.listedersid = dersid;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            ogretmenekran ekran = new ogretmenekran();
            ekran.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult mesaj = new DialogResult();
            mesaj = MessageBox.Show("Öğrenci numarasının doğru olduğundan emin misiniz?", "UYARI İŞLEMİ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (mesaj == DialogResult.Yes)
            {
                SqlConnection baglan = new SqlConnection();
                baglan.ConnectionString = "Data Source=.; Initial Catalog=yoklama;Integrated Security=True;";
                baglanti.Open();
                SqlCommand haftaoku = new SqlCommand();
                haftaoku.Connection = baglanti;
                haftaoku.CommandText = "SELECT * FROM devamsizlik where ogrenci_no='" + label5.Text + "'";
                SqlDataReader drrr = haftaoku.ExecuteReader();
                if (drrr.Read())
                {
                    SqlCommand komut = new SqlCommand();
                    komut.CommandText = "update devamsizlik set " + label6.Text + " = 'var' where ogrenci_no = " + label5.Text;
                    komut.Connection = baglan;
                    komut.CommandType = CommandType.Text;
                    baglan.Open();
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Yoklama Alındı.");
                    baglan.Close();

                    baglan.Open();
                    string kayit = "SELECT ogrenci_no AS [Öğrenci Numarası], ogrenci_adi AS[Öğrenci Adı] FROM ogrenci where sinifid='" + sinifid + "' and ogrenci_no='" + label5.Text + "'";
                    SqlCommand kmt = new SqlCommand(kayit, baglan);
                    SqlDataAdapter dada = new SqlDataAdapter(kmt);
                    DataTable dtdt = new DataTable();
                    dada.Fill(dtdt);
                    dataGridView2.DataSource = dtdt;
                    baglan.Close();
                }
                else
                {
                    SqlCommand komut = new SqlCommand();
                    komut.CommandText = "INSERT INTO devamsizlik (ders_id, sinif_id, saat, ogrenci_no, tarih,devam_durumu, hafta1) VALUES ('" + dersid + "', '" + sinifid + "', '" + DateTime.Now.ToLongTimeString() + "', '" + yuz + "', '" + DateTime.Now.ToShortDateString() + "','%0', 'var')";
                    komut.Connection = baglan;
                    komut.CommandType = CommandType.Text;
                    baglan.Open();
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Yoklama Alındı.");
                    baglan.Close();

                    baglan.Open();
                    string kayit = "SELECT ogrenci_no AS [Öğrenci Numarası], ogrenci_adi AS[Öğrenci Adı] FROM ogrenci where sinifid='" + sinifid + "' and ogrenci_no='" + label5.Text + "'";
                    SqlCommand kmt = new SqlCommand(kayit, baglan);
                    SqlDataAdapter dada = new SqlDataAdapter(kmt);
                    DataTable dtdt = new DataTable();
                    dada.Fill(dtdt);
                    dataGridView2.DataSource = dtdt;
                    baglan.Close();
                }
                baglanti.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection baglan = new SqlConnection();
            baglan.ConnectionString = "Data Source=.; Initial Catalog=yoklama;Integrated Security=True;";

            baglanti.Open();
            DialogResult mesaj = new DialogResult();
            mesaj = MessageBox.Show("Yoklamayı bitirmek istediğinizden emin misiniz?", "YOKLAMA BİTİRME İŞLEMİ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (mesaj == DialogResult.Yes)
            {
                if (label6.Text == "hafta1")
                {
                    SqlCommand haftaa = new SqlCommand();
                    haftaa.Connection = baglanti;
                    haftaa.CommandText = "Select * from ogrenci where ogrenci_no NOT IN(Select ogrenci_no from devamsizlik where sinif_id='" + sinifid + "' and ders_id='" + dersid + "') and sinifid='" + sinifid + "'";
                    SqlDataReader haftadr = haftaa.ExecuteReader();
                    while (haftadr.Read())
                    {
                        SqlCommand komut = new SqlCommand();
                        komut.CommandText = "INSERT INTO devamsizlik (ders_id, sinif_id, saat, ogrenci_no, tarih,devam_durumu, hafta1) VALUES ('" + dersid + "', '" + sinifid + "', '" + DateTime.Now.ToLongTimeString() + "', '" + haftadr.GetValue(2).ToString() + "', '" + DateTime.Now.ToShortDateString() + "','%100', 'yok')";
                        komut.Connection = baglan;
                        komut.CommandType = CommandType.Text;
                        baglan.Open();
                        komut.ExecuteNonQuery();
                        baglan.Close();
                    }
                    MessageBox.Show("Yoklama İşlemi Tamamlandı.");
                }
                else
                {
                    SqlCommand haftaoku = new SqlCommand();
                    haftaoku.Connection = baglanti;
                    haftaoku.CommandText = "SELECT * FROM devamsizlik where sinif_id='" + sinifid + "' and ders_id='" + dersid + "' and " + label6.Text + " is NULL";
                    SqlDataReader drrr = haftaoku.ExecuteReader();
                    while (drrr.Read())
                    {
                        SqlCommand komut = new SqlCommand();
                        komut.CommandText = "update devamsizlik set " + label6.Text + " = 'yok' where " + label6.Text + " is null";
                        komut.Connection = baglan;
                        komut.CommandType = CommandType.Text;
                        baglan.Open();
                        komut.ExecuteNonQuery();
                        
                        baglan.Close();
                    }
                   
                    baglanti.Close();

                    baglanti.Open();
                    SqlCommand devamoku = new SqlCommand();
                    haftaoku.Connection = baglanti;
                    haftaoku.CommandText = "SELECT * FROM devamsizlik where sinif_id='" + sinifid + "' and ders_id='" + dersid + "' ";
                    SqlDataReader devamdr = haftaoku.ExecuteReader();


                    while (devamdr.Read())//satır sayısı kadar dönücek.
                    {
                        int okunanyok = 0;
                        int devam_durumu = 0;
                        for (int i = 0; i < 21; i++)//sütun sayısı indexleri 0 dan 19 a kadar olduğu için.
                        {
                            if (devamdr.GetValue(i).ToString() == "yok")//okunan sütunların içindeki değer var ise buraya giriyor.get value sütun içindeki değeri almamızı sağlıyor.
                            {
                                okunanyok++;
                            }
                        }
                        devam_durumu = (okunanyok * 100) / hafta;
                        SqlCommand komutum = new SqlCommand();
                        komutum.CommandText = "update devamsizlik set devam_durumu='%" + devam_durumu.ToString() + "' where id='" + devamdr.GetValue(0).ToString() + "'"; //where şartında id=okunansatirdaki id ye eşitse 
                        komutum.Connection = baglan;
                        komutum.CommandType = CommandType.Text;
                        baglan.Open();
                        komutum.ExecuteNonQuery();
                        baglan.Close();
                    }
                    baglanti.Close();
                }
                MessageBox.Show("Yoklama Alınma İşlemi Tamamlandı.");

                Application.Exit();

            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
