using System;
using System.Collections.Generic;
using System.ComponentModel;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace WindowsFormsApp56
{
    public partial class yoklamaliste : Form
    {
        public yoklamaliste()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=.; Initial Catalog=yoklama;Integrated Security=True;");
        public string listesinifid;
        public string listedersid;
        public string sinifid;
        public string dersid;
        private void yoklamaliste_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = baglanti;
            cmd.CommandText = "SELECT * FROM siniff where sinif_adi='" + sinifid + "'";
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                listesinifid = dr.GetValue(0).ToString();
            }
            baglanti.Close();

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
            string listekayit = "SELECT devamsizlik.ogrenci_no AS[Öğrenci no], ogrenci.ogrenci_adi As[Oğrenci adi],devam_durumu As[Devam Durumu] , hafta1 AS[1.Hafta], hafta2 AS[2.Hafta], hafta3 AS[3.Hafta], hafta4 AS[4.Hafta], hafta5 AS[5.Hafta], hafta6 AS[6.Hafta], hafta7 AS[7.Hafta], hafta8 AS[8.Hafta], hafta9 AS[9.Hafta], hafta10 AS[10.Hafta], hafta11 AS[11.Hafta], hafta12 AS[12.Hafta], hafta13 AS[13.Hafta], hafta14 AS[14.Hafta] FROM devamsizlik INNER JOIN ogrenci ON devamsizlik.ogrenci_no = ogrenci.ogrenci_no where sinif_id = '" + listesinifid + "' and ders_id =" + listedersid;
            SqlCommand listekmt = new SqlCommand(listekayit, baglanti);
            SqlDataAdapter liste = new SqlDataAdapter(listekmt);
            System.Data.DataTable listeoku = new System.Data.DataTable();
            liste.Fill(listeoku);
            dataGridView1.DataSource = listeoku;
            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            raporlama rapor = new raporlama();
            rapor.Show();
        }

        private void button1_Click(object sender, EventArgs e) //datagridwievi excele aktarma
        {
            Excel.Application excel = new Excel.Application();
            excel.Visible = true;
            object Missing = Type.Missing;
            Workbook workbook = excel.Workbooks.Add(Missing);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
            int StartCol = 1;
            int StartRow = 1;
            for (int j = 0; j < dataGridView1.Columns.Count; j++)
            {
                Range myRange = (Range)sheet1.Cells[StartRow, StartCol + j];
                myRange.Value2 = dataGridView1.Columns[j].HeaderText;
            }
            StartRow++;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    Range myRange = (Range)sheet1.Cells[StartRow + i, StartCol + j];
                    myRange.Value2 = dataGridView1[j, i].Value == null ? "" : dataGridView1[j, i].Value;
                    myRange.Select();
                }
            }
        }
        private void PictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
          
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            ogretmenekran ekr = new ogretmenekran();
            ekr.Show();
            this.Hide();
        }
    }
}

