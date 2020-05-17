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
    public partial class Form1 : Form
    {
        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }
        string yuz;
        private async void btnEgit_Click(object sender, EventArgs e)
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
        private void Form1_Load(object sender, EventArgs e)
        {
            
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
                            label1.Text = name;
                        }
                    image.Draw(yuz.rect, new Bgr(Color.Red), 2);
                }
                yuz = label1.Text;
                pictureBox1.Image = image.ToBitmap();
            };
            
        }

        private void btnEgitimSil_Click(object sender, EventArgs e)
        {
            recognition.DeleteTrains();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show(yuz);
        }
    }
}
