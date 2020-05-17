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
    public partial class ders : Form
    {
        public ders()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=.; Initial Catalog=yoklama;Integrated Security=True;");


        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Ders_Load(object sender, EventArgs e)
        {
            listBox1.Items.Add("Öğretmen No      " + " Öğretmen Adı");

            SqlCommand comlist = new SqlCommand();
            comlist.CommandText = "SELECT * FROM ogretmen";
            comlist.Connection = baglanti;
            comlist.CommandType = CommandType.Text;

            SqlDataReader listoku;
            baglanti.Open();
            listoku = comlist.ExecuteReader();
            while (listoku.Read())
            {
                listBox1.Items.Add(listoku.GetValue(3).ToString() + "                " + listoku.GetValue(1).ToString());
            }
            baglanti.Close();

            SqlCommand comsinif = new SqlCommand();
            comsinif.CommandText = "SELECT sinif_adi FROM siniff";
            comsinif.Connection = baglanti;
            comsinif.CommandType = CommandType.Text;

            SqlDataReader comoku;
            baglanti.Open();
            comoku = comsinif.ExecuteReader();
            while (comoku.Read())
            {
                comboBox2.Items.Add(comoku.GetValue(0));
                comboBox5.Items.Add(comoku.GetValue(0));
                comboBox4.Items.Add(comoku.GetValue(0));
                comboBox8.Items.Add(comoku.GetValue(0));
            }
            baglanti.Close();

            SqlCommand comogrt = new SqlCommand();
            comogrt.CommandText = "SELECT ogretmen_adi FROM ogretmen";
            comogrt.Connection = baglanti;
            comogrt.CommandType = CommandType.Text;

            SqlDataReader ogtoku;
            baglanti.Open();
            ogtoku = comogrt.ExecuteReader();
            while (ogtoku.Read())
            {
                comboBox1.Items.Add(ogtoku.GetValue(0));
                comboBox3.Items.Add(ogtoku.GetValue(0));
            }
            baglanti.Close();

            SqlCommand dersek = new SqlCommand();
            dersek.CommandText = "SELECT DISTINCT ders_adi FROM ders";
            dersek.Connection = baglanti;
            dersek.CommandType = CommandType.Text;

            SqlDataReader deroku;
            baglanti.Open();
            deroku = dersek.ExecuteReader();
            while (deroku.Read())
            {
                comboBox9.Items.Add(deroku.GetValue(0).ToString());
                comboBox10.Items.Add(deroku.GetValue(0).ToString());
            }
            baglanti.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && comboBox2.Text != "" && comboBox1.Text != "")
            {
                SqlCommand kmtsnfid = new SqlCommand();
                kmtsnfid.CommandText = "SELECT * FROM siniff where sinif_adi='" + comboBox2.Text + "'";
                kmtsnfid.Connection = baglanti;
                kmtsnfid.CommandType = CommandType.Text;
                int sinifid = 0;
                SqlDataReader snfidoku;
                baglanti.Open();
                snfidoku = kmtsnfid.ExecuteReader();
                snfidoku.Read();
                if (snfidoku.HasRows)
                {
                    sinifid = Convert.ToInt32(snfidoku.GetValue(0));
                }
                baglanti.Close();

                SqlCommand kmtogtno = new SqlCommand();
                kmtogtno.CommandText = "SELECT * FROM ogretmen where ogretmen_adi='" + comboBox1.Text + "'";
                kmtogtno.Connection = baglanti;
                kmtogtno.CommandType = CommandType.Text;
                int ogretmenno = 0;
                SqlDataReader ogtnooku;
                baglanti.Open();
                ogtnooku = kmtogtno.ExecuteReader();
                ogtnooku.Read();
                if (ogtnooku.HasRows)
                {
                    ogretmenno = Convert.ToInt32(ogtnooku.GetValue(3));
                }
                baglanti.Close();

                try
                {

                    baglanti.Open();
                    string text = "INSERT INTO ders (ders_adi,ders_kodu,ogretmen_no,sinif_id) VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + Convert.ToUInt32(ogretmenno) + "','" + sinifid.ToString() + "')";
                    SqlCommand sorgu = new SqlCommand(text, baglanti);
                    sorgu.ExecuteNonQuery();
                    MessageBox.Show("Ders Eklendi");
                    baglanti.Close();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    comboBox1.Text = "";
                    comboBox2.Text = "";

                }
                catch
                {

                    MessageBox.Show("Ekleme işlemi Yapılamadı");
                }
            }
        }
        int sinifid = 0;
        private void Button2_Click(object sender, EventArgs e)
        {

            SqlCommand kmtsnfid = new SqlCommand();
            kmtsnfid.CommandText = "SELECT * FROM siniff where sinif_adi='" + comboBox5.Text + "'";
            kmtsnfid.Connection = baglanti;
            kmtsnfid.CommandType = CommandType.Text;
            
            SqlDataReader snfidoku;
            baglanti.Open();
            snfidoku = kmtsnfid.ExecuteReader();
            snfidoku.Read();
            if (snfidoku.HasRows)
            {
                sinifid = Convert.ToInt32(snfidoku.GetValue(0));
            }
            baglanti.Close();

            baglanti.Open();
            string sinif = "SELECT sinif_adi from siniff where id='" + sinifid + "'";
            SqlCommand kmt = new SqlCommand(sinif, baglanti);
            SqlDataAdapter daa = new SqlDataAdapter(kmt);
            SqlDataReader drr = kmt.ExecuteReader();
            if (drr.Read())
                comboBox4.Text = drr["sinif_adi"].ToString();
            baglanti.Close();

            baglanti.Open();
            string ogret = "SELECT ogretmen_adi from ogretmen where ogretmen_no IN (SELECT ogretmen_no from ders where ders_adi='" + comboBox9.Text + "' and sinif_id='" + sinifid + "')";
            SqlCommand ogkmt = new SqlCommand(ogret, baglanti);
            SqlDataAdapter daogt = new SqlDataAdapter(ogkmt);
            SqlDataReader drogt = ogkmt.ExecuteReader();
            if (drogt.Read())
                comboBox3.Text = drogt["ogretmen_adi"].ToString();
            baglanti.Close();

            baglanti.Open();
            string kayit = "SELECT * from ders where ders_adi='"+comboBox9.Text+"' and sinif_id='"+sinifid.ToString()+"'";
            //öğrencino parametresine bağlı olarak öğrenci bilgilerini çeken sql kodu
            SqlCommand komut = new SqlCommand(kayit, baglanti);
            //öğrencino parametremize textbox'dan girilen değeri aktarıyoruz.
            SqlDataAdapter da = new SqlDataAdapter(komut);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read()) //Sadece tek bir kayıt döndürüleceği için while kullanmadım.
            {
                textBox3.Text = dr["ders_adi"].ToString();
                textBox4.Text = dr["ders_kodu"].ToString();             
                //Datareader ile okunan verileri form kontrollerine aktardık.
            }
            else
                MessageBox.Show("Ders Bulunamadı.");
            baglanti.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            SqlCommand snfkomut = new SqlCommand();
            snfkomut.CommandText = "SELECT * FROM siniff where sinif_adi='" + comboBox4.Text + "'";
            snfkomut.Connection = baglanti;
            snfkomut.CommandType = CommandType.Text;
            int sinifidd = 0;
            SqlDataReader snfdr;
            baglanti.Open();
            snfdr = snfkomut.ExecuteReader();
            snfdr.Read();
            if (snfdr.HasRows)
            {
                sinifidd = Convert.ToInt32(snfdr.GetValue(0));
            }
            baglanti.Close();

            SqlCommand ogtkomut = new SqlCommand();
            ogtkomut.CommandText = "SELECT * FROM ogretmen where ogretmen_adi='" + comboBox3.Text + "'";
            ogtkomut.Connection = baglanti;
            ogtkomut.CommandType = CommandType.Text;
            int ogtno = 0;
            SqlDataReader ogtdr;
            baglanti.Open();
            ogtdr = ogtkomut.ExecuteReader();
            ogtdr.Read();
            if (ogtdr.HasRows)
            {
                ogtno = Convert.ToInt32(ogtdr.GetValue(3));
            }
            baglanti.Close();

            try
            {

                baglanti.Open();
                string kayit = "UPDATE ders SET ders_adi=@dersadi , ders_kodu=@derskodu , ogretmen_no=@ogretmeno , sinif_id=@sinifid where ders_adi='"+comboBox9.Text+"' and sinif_id='"+sinifid+"'";
                // öğrenciler tablomuzun ilgili alanlarını değiştirecek olan güncelleme sorgusu.
                SqlCommand komutt = new SqlCommand(kayit, baglanti);
                //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
                komutt.Parameters.AddWithValue("@dersadi", textBox3.Text);
                komutt.Parameters.AddWithValue("@derskodu", textBox4.Text);
                komutt.Parameters.AddWithValue("@ogretmeno", ogtno);
                komutt.Parameters.AddWithValue("@sinifid", sinifidd);
                //Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.
                komutt.ExecuteNonQuery();
                //Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
                baglanti.Close();
                MessageBox.Show("Ders Bilgileri Güncellendi.");
                comboBox9.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                comboBox3.Text = "";
                comboBox4.Text = "";
                comboBox5.Text = "";
            }
            catch
            {

                MessageBox.Show("Güncelleme işlemi Yapılamadı");
            }
        }

        private void ComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Button5_Click(object sender, EventArgs e)
        {

            SqlCommand kmtsnfid = new SqlCommand();
            kmtsnfid.CommandText = "SELECT * FROM siniff where sinif_adi='" + comboBox8.Text + "'";
            kmtsnfid.Connection = baglanti;
            kmtsnfid.CommandType = CommandType.Text;

            SqlDataReader snfidoku;
            baglanti.Open();
            snfidoku = kmtsnfid.ExecuteReader();
            snfidoku.Read();
            if (snfidoku.HasRows)
            {
                sinifid = Convert.ToInt32(snfidoku.GetValue(0));
            }
            baglanti.Close();

            baglanti.Open();
            string sinif = "SELECT sinif_adi from siniff where id='" + sinifid + "'";
            SqlCommand kmt = new SqlCommand(sinif, baglanti);
            SqlDataAdapter daa = new SqlDataAdapter(kmt);
            SqlDataReader drr = kmt.ExecuteReader();
            if (drr.Read())
                comboBox6.Text = drr["sinif_adi"].ToString();
            baglanti.Close();

            baglanti.Open();
            string ogret = "SELECT ogretmen_adi from ogretmen where ogretmen_no IN (SELECT ogretmen_no from ders where ders_adi='" + comboBox10.Text + "' and sinif_id='" + sinifid + "')";
            SqlCommand ogkmt = new SqlCommand(ogret, baglanti);
            SqlDataAdapter daogt = new SqlDataAdapter(ogkmt);
            SqlDataReader drogt = ogkmt.ExecuteReader();
            if (drogt.Read())
                comboBox7.Text = drogt["ogretmen_adi"].ToString();
            baglanti.Close();

            baglanti.Open();
            string kayit = "SELECT * from ders where ders_adi='" + comboBox10.Text + "' and sinif_id='" + sinifid.ToString() + "'";
            //öğrencino parametresine bağlı olarak öğrenci bilgilerini çeken sql kodu
            SqlCommand komut = new SqlCommand(kayit, baglanti);
            //öğrencino parametremize textbox'dan girilen değeri aktarıyoruz.
            SqlDataAdapter da = new SqlDataAdapter(komut);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read()) //Sadece tek bir kayıt döndürüleceği için while kullanmadım.
            {
                textBox7.Text = dr["ders_adi"].ToString();
                textBox6.Text = dr["ders_kodu"].ToString();
                //Datareader ile okunan verileri form kontrollerine aktardık.
            }
            else
                MessageBox.Show("Ders Bulunamadı.");
            baglanti.Close();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            SqlCommand sorgu = new SqlCommand("Delete From ders where ders_adi='"+ comboBox10.Text+"' and sinif_id='"+sinifid+"'", baglanti);

            try
            {
                baglanti.Open();
                sorgu.ExecuteNonQuery();
                MessageBox.Show("Kayıt Silindi");
                baglanti.Close();
                comboBox10.Text = "";
                textBox7.Text = "";
                textBox6.Text = "";
                comboBox6.Text = "";
                comboBox7.Text = "";
                comboBox8.Text = "";
            }
            catch
            {
                textBox2.Text = "";
                MessageBox.Show("Silme işlemi Yapılamadı");
            }
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            yetkiliekran yet = new yetkiliekran();
            yet.Show();
            this.Hide();
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}
