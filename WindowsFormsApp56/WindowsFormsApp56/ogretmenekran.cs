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
    public partial class ogretmenekran : Form
    {
        public ogretmenekran()
        {
            InitializeComponent();
        }
        public string ogretmen_no;
        private void ogretmenekran_Load(object sender, EventArgs e)
        {
            comboBox3.Items.Add("hafta1");
            comboBox3.Items.Add("hafta2");
            comboBox3.Items.Add("hafta3");
            comboBox3.Items.Add("hafta4");
            comboBox3.Items.Add("hafta5");
            comboBox3.Items.Add("hafta6");
            comboBox3.Items.Add("hafta7");
            comboBox3.Items.Add("hafta8");
            comboBox3.Items.Add("hafta9");
            comboBox3.Items.Add("hafta10");
            comboBox3.Items.Add("hafta11");
            comboBox3.Items.Add("hafta12");
            comboBox3.Items.Add("hafta13");
            comboBox3.Items.Add("hafta14");

            SqlConnection baglanti = new SqlConnection();
            baglanti.ConnectionString = "Data Source=.; Initial Catalog=yoklama;Integrated Security=True;";
            SqlCommand komut = new SqlCommand();
            komut.CommandText = "SELECT ders_adi FROM ders where ogretmen_no='"+ ogretmen_no + "' group by ders_adi";
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox2.Text="";
            SqlConnection baglanti = new SqlConnection();
            baglanti.ConnectionString = "Data Source=.; Initial Catalog=yoklama;Integrated Security=True;";
            SqlCommand komut = new SqlCommand();
            komut.CommandText = "SELECT sinif_adi from siniff where id IN (SELECT sinif_id FROM ders where ders_adi= '"+comboBox1.Text+"' AND ogretmen_no='"+ogretmen_no+"')";
            komut.Connection = baglanti;
            komut.CommandType = CommandType.Text;

            SqlDataReader dr;
            baglanti.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr["sinif_adi"]);
            }
            baglanti.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ogtgiris ogtgrs = new ogtgiris();
            ogtgrs.Show();
            this.Hide();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "")
            {
                yoklama yoklama = new yoklama();
                yoklama.label3.Text = comboBox1.Text;
                yoklama.label4.Text = comboBox2.Text;
                yoklama.label6.Text = comboBox3.Text;
                yoklama.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Lütfen bilgileri eksiksiz doldurun...", "EKSİK ALAN UYARISI", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            raporlama rapor = new raporlama();
            rapor.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "" && comboBox2.Text != "")
            {
                yoklamaliste liste = new yoklamaliste();
                liste.sinifid = comboBox2.Text;
                liste.dersid = comboBox1.Text;
                liste.Show();
            }
            else
            {
                MessageBox.Show("Lütfen bilgileri eksiksiz doldurun...", "EKSİK ALAN UYARISI", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }  
            
                     
        }
    }
}

