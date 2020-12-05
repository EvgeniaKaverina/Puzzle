using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzle
{
    public partial class Game : Form
    {
        private GalleryForCreate gal;
        public Game()
        {
            InitializeComponent();
        }

        Image image;
        PictureBox pic = null;

        private void Game_Load(object sender, EventArgs e)
        {
            //image = Image.FromFile();
            //CreateBitmapImage();
            image = CreateBitmapImage();
            if (pic == null)
            {
                pic = new PictureBox();
                pic.Height = groupBox1.Height;
                pic.Width = groupBox1.Width;
                groupBox1.Controls.Add(pic);
            }
            pic.Image = image;
        }
        private Bitmap CreateBitmapImage()
        {
           // string img = gal.getpicture_name();

            //if (pic == null)
            //{
            //    Bitmap bm = new Bitmap(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\gallery\", pict));
            //    pic = new PictureBox();
            //    pic.Height = groupBox1.Height;
            //    pic.Width = groupBox1.Width;
            //    groupBox1.Controls.Add(pic);
            //}
            //pic.Image = image;

            Image img = Image.FromFile(@"..\..\gallery\about.jpg");


            Bitmap objBmImage = new Bitmap(groupBox1.Width, groupBox1.Height);
            Graphics objGraphics = Graphics.FromImage(objBmImage);
            objGraphics.Clear(Color.White);
            objGraphics.DrawImage(img, new Rectangle(0, 0, groupBox1.Width, groupBox1.Height));
            objGraphics.Flush();

            return objBmImage;
            
        }
    }
}
