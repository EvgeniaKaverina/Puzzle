using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Puzzle
{
    public partial class Gallery : Form
    {
        private PictureBox picSelected;
        public Gallery()
        {
            InitializeComponent();
            picSelected = null;
        }

        private void Gallery_Load(object sender, EventArgs e)
        {
           
     
            //запрос в БД SELECT
            for(int i = 0; i < 16; i++)
            {
                Bitmap bmp = new Bitmap(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"..\..\gallery\gallery1.jpg"));
                PictureBox tempPictureBox = new PictureBox();

                //generates a thumbnail image of specified size
           tempPictureBox.Image = bmp.GetThumbnailImage(100, 100,  new Image.GetThumbnailImageAbort(ThumbnailCallback),   IntPtr.Zero);
           //   tempPictureBox.ImageLocation = @"C:\\\Users\\Alsu\\Desktop\\7semester\Puzzle\Puzzle\Puzzle\gallery\gallery1.jpg";
                tempPictureBox.Size = new System.Drawing.Size(100, 100);
                tempPictureBox.Click += new EventHandler(this.tempPictureBox_Click);
                tempPictureBox.DoubleClick += new EventHandler(this.pictureBox_DoubleClick);
                flowLayoutPanel1.Controls.Add(tempPictureBox);
            }
            //запрос select из БД
           /* for (int i = 1; i <= 6; i++)
            {
                imageList1.Images.Add(Image.FromFile(@"C:\\\Users\\Alsu\\Desktop\\7semester\Puzzle\Puzzle\Puzzle\gallery\gallery"+i+".jpg"));
            }
            listView1.LargeImageList = imageList1;
      //      imageList1.Images.Add(Image.FromFile(@"C:\\\Users\\Alsu\\Desktop\\7semester\Puzzle\Puzzle\Puzzle\gallery\gallery1.jpg"));
            for (int j = 0; j < imageList1.Images.Count; j++)
            {
                ListViewItem item = new ListViewItem();
            //    item.Text = name[j];
                item.ImageIndex = j;
                listView1.Items.Add(item);
            }*/
        }
        private void tempPictureBox_Click(object sender, EventArgs e)
        {
            // Form form = new Form((Bitmap)((PictureBox)sender).Image);
            // Form Form2 = new Form((Bitmap)pictureBox1.Image);
            // Form2.ShowDialog();
            

            picSelected = (PictureBox)sender;
            //  PreviewPictureBox.Image = ((PictureBox)sender).Image;
        }
        public bool ThumbnailCallback()
        {
            return true;
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Файлы изображений | *.jpg";
            if (dialog.ShowDialog() != DialogResult.OK)
                return;
            Image image = Image.FromFile(dialog.FileName);
            PictureBox tempPictureBox = new PictureBox();

            //generates a thumbnail image of specified size
            
            tempPictureBox.Image = image.GetThumbnailImage(100, 100,
                                   new Image.GetThumbnailImageAbort(ThumbnailCallback),
                                   IntPtr.Zero);
            tempPictureBox.Size = new System.Drawing.Size(100, 100);
            tempPictureBox.Click += new EventHandler(this.tempPictureBox_Click);
            tempPictureBox.DoubleClick += new EventHandler(this.pictureBox_DoubleClick);
            flowLayoutPanel1.Controls.Add(tempPictureBox);
            /* try
             {
                 image = Image.FromFile(dialog.FileName);
             }
             catch (OutOfMemoryException ex)
             {
                 MessageBox.Show("Ошибка чтения картинки");
                 return;
             }
             //запрос INSERT в бд
             //перенести в другой метод
             imageList1.Images.Add(image);
             ListViewItem item = new ListViewItem();
             //    item.Text = name[j];
             item.ImageIndex =imageList1.Images.Count-1;
             listView1.Items.Add(item);*/
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            //запрос DELETE в бд
           
            flowLayoutPanel1.Controls.Remove((Control)picSelected);

        }
        private void pictureBox_DoubleClick(object sender, EventArgs e){
            //   picSelected = (PictureBox)sender;
         //rintDocument1.DocumentName =@"C:\\Users\\Alsu\\Desktop\\7semester\Puzzle\Puzzle\Puzzle\gallery\gallery1.jpg";
            //((PictureBox)sender).ImageLocation;
            // printDialog1.Document = printDocument1;

         // printPreviewDialog1.Document = printDocument1;
          //printPreviewDialog1.ShowDialog();
          string filename= @"C:\\Users\\Alsu\\Desktop\\7semester\Puzzle\Puzzle\Puzzle\gallery\gallery1.jpg";
            Picture picture = new Picture(filename);
            picture.ShowDialog();
        }
      
        

        private void flowLayoutPanel1_Scroll(object sender, ScrollEventArgs e)
        {

        }
    }
}
