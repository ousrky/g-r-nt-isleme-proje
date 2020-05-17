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
    class pdfAktar
    {
        /*     public static void pdfKaydet(DataGridView veriTablosu)
             {
                 try
                 {
                     PdfPTable pdfTablosu = new PdfPTable(veriTablosu.ColumnCount);
                     pdfTablosu.DefaultCell.Padding = 3;
                     pdfTablosu.WidthPercentage = 100;
                     pdfTablosu.HorizontalAlignment = Element.ALIGN_LEFT;
                     pdfTablosu.DefaultCell.BorderWidth = 1;
                     foreach (DataGridViewColumn sutun in veriTablosu.Columns)
                     {
                         PdfPCell pdfHucresi = new PdfPCell(new Phrase(sutun.HeaderText));
                         //     pdfHucresi.BackgroundColor = Color.LightGray;
                         pdfTablosu.AddCell(pdfHucresi);
                     }
                     foreach (DataGridViewRow satir in veriTablosu.Rows)
                     {
                         foreach (DataGridViewCell cell in satir.Cells)
                         {
                             pdfTablosu.AddCell(cell.Value.ToString());
                         }
                     }

                     SaveFileDialog dosyakaydet = new SaveFileDialog();
                     dosyakaydet.FileName = "projePDfDosyaAdı";
                     dosyakaydet.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
                     dosyakaydet.Filter = "PDF Dosyası|*.pdf";
                     if (dosyakaydet.ShowDialog() == DialogResult.OK)
                     {
                         using (FileStream stream = new FileStream(dosyakaydet.FileName, FileMode.Create))
                         {
                             Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                             PdfWriter.GetInstance(pdfDoc, stream);
                             pdfDoc.Open();
                             pdfDoc.Add(pdfTablosu);
                             pdfDoc.Close();
                             stream.Close();
                             MessageBox.Show("PDF dosyası başarıyla oluşturuldu!\n" + "Dosya Konumu: " + dosyakaydet.FileName, "İşlem Tamam");
                         }
                     }
                 }
                 catch (Exception hata)
                 {
                     MessageBox.Show(hata.Message);
                 }  */

    }
}

