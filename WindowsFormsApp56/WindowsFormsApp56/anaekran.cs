using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp56
{
    public partial class anaekran : Form
    {
        public anaekran()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ogtgiris ogtgrs = new ogtgiris();
            ogtgrs.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ogrgiris ogrgrs = new ogrgiris();
            ogrgrs.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            yetgiris yetgrs = new yetgiris();
            yetgrs.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}
