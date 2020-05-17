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
    public partial class ogrenciekran : Form
    {
        public ogrenciekran()
        {
            InitializeComponent();
        }
        public string ogrenci_no;
        public string listedersid;
        public string dersid;

        SqlConnection baglanti = new SqlConnection("Data Source=.; Initial Catalog=yoklama;Integrated Security=True;");
        private void ogrenciekran_Load(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand();
            komut.CommandText = "SELECT ders_adi from ders where sinif_id IN (SELECT sinifid FROM ogrenci where  ogrenci_no='"+ ogrenci_no +"')";
            komut.Connection = baglanti;
            komut.CommandType = CommandType.Text;
            SqlDataReader dr;
            baglanti.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["ders_adi"]);
            }
            baglanti.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ogrgiris ogrgiris = new ogrgiris();
            ogrgiris.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("Lütfen bilgileri eksiksiz doldurun...", "EKSİK ALAN UYARISI", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                dersid = comboBox1.Text;
                baglanti.Open();
                SqlCommand derskmt = new SqlCommand();
                derskmt.Connection = baglanti;
                derskmt.CommandText = "SELECT * FROM ders where ders_adi='" + dersid + "'";
                SqlDataReader dersoku = derskmt.ExecuteReader();
                if (dersoku.Read())
                {
                    listedersid = dersoku.GetValue(0).ToString();
                }
                baglanti.Close();
                baglanti.Open();
                string listekayit = "SELECT devamsizlik.ogrenci_no AS[Öğrenci no], ogrenci.ogrenci_adi As[Oğrenci adi], hafta1 AS[1.Hafta], hafta2 AS[2.Hafta], hafta3 AS[3.Hafta], hafta4 AS[4.Hafta], hafta5 AS[5.Hafta], hafta6 AS[6.Hafta], hafta7 AS[7.Hafta], hafta8 AS[8.Hafta], hafta9 AS[9.Hafta], hafta10 AS[10.Hafta], hafta11 AS[11.Hafta], hafta12 AS[12.Hafta], hafta13 AS[13.Hafta], hafta14 AS[14.Hafta] FROM devamsizlik INNER JOIN ogrenci ON devamsizlik.ogrenci_no = ogrenci.ogrenci_no where devamsizlik.ogrenci_no='" + label2.Text+"' and ders_id =" + listedersid;
                SqlCommand listekmt = new SqlCommand(listekayit, baglanti);
                SqlDataAdapter liste = new SqlDataAdapter(listekmt);
                DataTable listeoku = new DataTable();
                liste.Fill(listeoku);
                dataGridView1.DataSource = listeoku;
                baglanti.Close();
            }
        }
    }
}
