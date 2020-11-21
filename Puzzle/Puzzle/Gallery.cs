using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace Puzzle
{
    public partial class Gallery : Form
    {
        private PictureBox picSelected;
        string connectionString = "Data Source=localhost;Initial Catalog=Puzzle;Integrated Security=True";
        private SqlConnection sqlConnection;
        public Gallery()
        {
            InitializeComponent();
            picSelected = null;
        }

        private async void Gallery_Load(object sender, EventArgs e)
        {
             //запрос в БД SELECT
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();
            SqlCommand command = new SqlCommand("SELECT name_picture FROM GalleryImage", sqlConnection);
            SqlDataReader reader = null;
            reader= await command.ExecuteReaderAsync();
            while(await reader.ReadAsync())
            //for(int i = 0; i < 16; i++)
            {
                string filename = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\gallery\", reader["name_picture"].ToString());
                Bitmap bmp = new Bitmap(filename);
                PictureBox tempPictureBox = new PictureBox();

                //generates a thumbnail image of specified size
                tempPictureBox.Image = bmp.GetThumbnailImage(200, 140,  new Image.GetThumbnailImageAbort(ThumbnailCallback),   IntPtr.Zero);
        
                tempPictureBox.Size = new System.Drawing.Size(200, 140);
                string[] p=filename.Split('\\');
                tempPictureBox.Name = p[p.Length-1];
                tempPictureBox.Click += new EventHandler(this.tempPictureBox_Click);
                tempPictureBox.DoubleClick += new EventHandler(this.pictureBox_DoubleClick);
                flowLayoutPanel1.Controls.Add(tempPictureBox);
            }
                    
        }

        //выделение картинки
        //сделать фокус на картинку!!!!!!!!!
        private void tempPictureBox_Click(object sender, EventArgs e)
        {
            picSelected = (PictureBox)sender;

         //   picSelected.Focus();
        
        }
        public bool ThumbnailCallback()
        {
            return true;
        }
        
        //добавление картинки
        private async void buttonAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Файлы изображений | *.jpg";
            if (dialog.ShowDialog() != DialogResult.OK)
                return;
            string filename = dialog.FileName;
            //if (File.Exists(Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), @"..\gallery\", filename)))
            //{
            //    MessageBox.Show("Изображение с таким названием существует");

            //}
            //else
            //{
                Image image = Image.FromFile(dialog.FileName);

                PictureBox tempPictureBox = new PictureBox();

                //generates a thumbnail image of specified size

                tempPictureBox.Image = image.GetThumbnailImage(200, 140,
                                       new Image.GetThumbnailImageAbort(ThumbnailCallback),
                                       IntPtr.Zero);
                tempPictureBox.Size = new System.Drawing.Size(200, 140);
                string[] p = filename.Split('\\');
                tempPictureBox.Name = p[p.Length - 1];
                tempPictureBox.Click += new EventHandler(this.tempPictureBox_Click);
                tempPictureBox.DoubleClick += new EventHandler(this.pictureBox_DoubleClick);
                flowLayoutPanel1.Controls.Add(tempPictureBox);
                //   File.Copy(filename, Path.Combine(Directory.GetCurrentDirectory(), @"..\..\gallery\",new FileInfo(tempPictureBox.Name).Name));

                //приведение к одному размеру
                Bitmap bmp = new Bitmap(600, 420);
                bmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    Rectangle src = new Rectangle(0, 0, image.Width, image.Height);

                    Rectangle dst = new Rectangle(0, 0, bmp.Width, bmp.Height);

                    g.DrawImage(image, dst, src, GraphicsUnit.Pixel);
                    bmp.Save(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\gallery\", tempPictureBox.Name), ImageFormat.Jpeg);

                //}

                sqlConnection = new SqlConnection(connectionString);
                await sqlConnection.OpenAsync();
                SqlCommand command = new SqlCommand("INSERT INTO [GalleryImage] (name_picture) VALUES(@picture)", sqlConnection);
                command.Parameters.AddWithValue("picture", tempPictureBox.Name);

                await command.ExecuteNonQueryAsync();

            }
        }
        //удаление картинки
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Remove((Control)picSelected);
            picSelected.Image = null;

            deleteFromGallery();

            deleteFromBD();
        }

        private void deleteFromGallery()
        {
            while (true)
            {
                try
                {
                    if (File.Exists(Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), @"..\gallery\", picSelected.Name)))
                    {
                        File.Delete(Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), @"..\gallery\", picSelected.Name));
                        return;
                    }
                }
                catch (IOException)
                {
                    MessageBox.Show("Error");
                }
            }
        }
        private async void deleteFromBD()
        {
            //запрос DELETE в бд
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();
            SqlCommand command = new SqlCommand("DELETE FROM [GalleryImage] WHERE name_picture=@picture", sqlConnection);
            command.Parameters.AddWithValue("picture", picSelected.Name);

            await command.ExecuteNonQueryAsync();
        }

        //увеличение картинки
        private void pictureBox_DoubleClick(object sender, EventArgs e) {
           
      
            Picture picture = new Picture(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"..\..\gallery\", picSelected.Name));
            picture.ShowDialog();

        }
      
        

        private void flowLayoutPanel1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
