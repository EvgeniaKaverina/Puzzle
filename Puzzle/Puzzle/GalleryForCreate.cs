using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace Puzzle
{
    public partial class GalleryForCreate : Form
    {
        private PictureBox picSelected;
        string connectionString = "Data Source=localhost;Initial Catalog=Puzzle;Integrated Security=True";
        private SqlConnection sqlConnection;

        private string picture_name;

        public GalleryForCreate()
        {
            InitializeComponent();
            picSelected = null;
        }

        private async void GalleryForCreate_Load(object sender, EventArgs e)
        {
            //запрос в БД SELECT
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();
            SqlCommand command = new SqlCommand("SELECT name_picture FROM GalleryImage", sqlConnection);
            SqlDataReader reader = null;
            reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            //for(int i = 0; i < 16; i++)
            {
                string filename = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\gallery\", reader["name_picture"].ToString());
                Bitmap bmp = new Bitmap(filename);
                PictureBox tempPictureBox = new PictureBox();

                //generates a thumbnail image of specified size
                tempPictureBox.Image = bmp.GetThumbnailImage(200, 140, new Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero);

                tempPictureBox.Size = new System.Drawing.Size(200, 140);
                string[] p = filename.Split('\\');
                tempPictureBox.Name = p[p.Length - 1];
                tempPictureBox.Click += new EventHandler(this.tempPictureBox_Click);
                tempPictureBox.DoubleClick += new EventHandler(this.pictureBox_DoubleClick);
                flowLayoutPanel1.Controls.Add(tempPictureBox);
            }

        }
        //выделение картинки
        private void tempPictureBox_Click(object sender, EventArgs e)
        {
            picSelected = (PictureBox)sender;
            picture_name = picSelected.Name;
            //   picSelected.Focus();

        }
        public string getpicture_name()
        {
            return picture_name;
        }
        public bool ThumbnailCallback()
        {
            return true;
        }

        //увеличение картинки
        private void pictureBox_DoubleClick(object sender, EventArgs e)
        {
            Picture picture = new Picture(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"..\..\gallery\", picSelected.Name));
            picture.ShowDialog();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (picSelected == null)
            {
                MessageBox.Show("Изображение не выбрано");
            }
            else
            {
                CreatePuzzle c = new CreatePuzzle();
                c.Show();
                this.Hide();
            }
        }
    }
}
