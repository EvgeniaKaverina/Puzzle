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
        // private GalleryForCreate gal;
        DateTime date; 

        public Game()
        {
            InitializeComponent();
        }

        Image image;
        PictureBox pic = null;
        PictureBox[] pictureBoxes = null;
        Image[] images = null;
        int level = 8;

        MyPictureBox firstBox = null;
        MyPictureBox secondBox = null;

        private void createFrag()
        {
          //запрос в бд
          //треугольные фрагменты
          //сохранение пазла в бд (незаконченного)
          //лента для пазлов
            if (pic != null)
            {
                groupBox1.Controls.Remove(pic);
                pic.Dispose();
                pic = null;

            }
            if (pictureBoxes == null)
            {
                pictureBoxes = new PictureBox[level];
                images = new Image[level];
            }
            int numCol = 2;
            int numRows = 4;
            int unitX = groupBox1.Width / numCol;
            int unitY = groupBox1.Height / numRows;
            int[] indice = new int[level];
            for(int i = 0; i < level; i++)
            {
                indice[i] = i;
                if (pictureBoxes[i] == null)
                {
                    pictureBoxes[i] = new MyPictureBox();
                    pictureBoxes[i].Click += new EventHandler(OnPuzzleClick);
                    pictureBoxes[i].BorderStyle = BorderStyle.Fixed3D;
                }
                pictureBoxes[i].Width = unitX;
                pictureBoxes[i].Height = unitY;

                ((MyPictureBox)pictureBoxes[i]).Index = i;

                CreateBitmapImage(image, images,i, numRows, numCol, unitX, unitY);
                pictureBoxes[i].Location = new Point(unitX * (i % numCol), unitY * (i / numCol));
                if (!groupBox1.Controls.Contains(pictureBoxes[i]))
                    groupBox1.Controls.Add(pictureBoxes[i]);
            }
            shuffle(ref indice);
            for(int i = 0; i < level; i++)
            {
                pictureBoxes[i].Image = images[indice[i]];
                ((MyPictureBox)pictureBoxes[i]).ImageIndex = indice[i];
            }

        }

        private void createFragTale()//лента 
        {
            if (pic != null)
            {
                flowLayoutPanel1.Controls.Remove(pic);
                pic.Dispose();
                pic = null;

            }
            if (pictureBoxes == null)
            {
                pictureBoxes = new PictureBox[level];
                images = new Image[level];
            }
            int numCol = 4;
            int numRows = 2;
            int unitX = groupBox1.Width / numCol;
            int unitY = groupBox1.Height / numRows;
            flowLayoutPanel1.Height = unitY + 50;
            int[] indice = new int[level];
            for (int i = 0; i < level; i++)
            {
                indice[i] = i;
                if (pictureBoxes[i] == null)
                {
                    pictureBoxes[i] = new MyPictureBox();
                    pictureBoxes[i].Click += new EventHandler(OnPuzzleClickTale);
                    pictureBoxes[i].BorderStyle = BorderStyle.Fixed3D;
                   // pictureBoxes[i].Size = new System.Drawing.Size(unitX, unitY);
                }
                pictureBoxes[i].Width = unitX;
                pictureBoxes[i].Height = unitY;

                ((MyPictureBox)pictureBoxes[i]).Index = i;

                CreateBitmapImage(image, images, i, numRows, numCol, unitX, unitY);
                pictureBoxes[i].Location = new Point(unitX * (i % numCol), unitY * (i / numCol));
                if (!flowLayoutPanel1.Controls.Contains(pictureBoxes[i]))
                    flowLayoutPanel1.Controls.Add(pictureBoxes[i]);
            }
            shuffle(ref indice);
            for (int i = 0; i < level; i++)
            {
                pictureBoxes[i].Image = images[indice[i]];
                ((MyPictureBox)pictureBoxes[i]).ImageIndex = indice[i];
            }

        }

        private void OnPuzzleClickTale(object sender, EventArgs e)
        {
            if (firstBox == null)
            {
                firstBox = (MyPictureBox)sender;
                firstBox.BorderStyle = BorderStyle.FixedSingle;
            }
            else if (secondBox == null)
            {
                secondBox = (MyPictureBox)sender;
                firstBox.BorderStyle = BorderStyle.Fixed3D;
                secondBox.BorderStyle = BorderStyle.FixedSingle;
               // SwitchImage(firstBox, secondBox);
                firstBox = null;
                secondBox = null;
            }
        }

        private void OnPuzzleClick(object sender, EventArgs e)//createFrag
        {
            if (firstBox == null)
            {
                firstBox = (MyPictureBox)sender;
                firstBox.BorderStyle = BorderStyle.FixedSingle;
            }
            else if (secondBox == null)
            {
                secondBox = (MyPictureBox)sender;
                firstBox.BorderStyle = BorderStyle.Fixed3D;
                secondBox.BorderStyle = BorderStyle.FixedSingle;
                SwitchImage(firstBox, secondBox);
                firstBox = null;
                secondBox = null;
            }
           
           // ((MyPictureBox)sender).BorderStyle = BorderStyle.FixedSingle;
        }
        private void SwitchImage(MyPictureBox box1,MyPictureBox box2)//createFrag
        {
            int tmp = box2.ImageIndex;
            box2.Image = images[box1.ImageIndex];
            box2.ImageIndex = box1.ImageIndex;
            box1.Image = images[tmp];
            box1.ImageIndex = tmp;
            if (isFinished())
            {
                //Останавливаем таймер
                timer1.Stop();
                MessageBox.Show("Well done!");
                //ShowImage();
            }
        }
        private bool isFinished()//createFrag
        {
            for(int i = 0; i < level; i++)
            {
                if(((MyPictureBox)pictureBoxes[i]).ImageIndex== ((MyPictureBox)pictureBoxes[i]).Index)
                {
                    pictureBoxes[i].Enabled = false;
                    pictureBoxes[i].Click -= null;
                }
            }
            for(int i = 0; i < level; i++)
            {
                if (((MyPictureBox)pictureBoxes[i]).ImageIndex != ((MyPictureBox)pictureBoxes[i]).Index)
                    return false;
            }
            return true;
        }
        private void CreateBitmapImage(Image image,Image[] images, int index, int numRows, int numCols, int unitX,int unitY)
        {
            images[index] = new Bitmap(unitX, unitY);
            Graphics og = Graphics.FromImage(images[index]);
            og.Clear(Color.White);

            og.DrawImage(image,
                new Rectangle(0, 0, unitX, unitY),
                //что-то не так
                new Rectangle(unitX * (index % numCols), unitY * (index / numCols), unitX, unitY),
                GraphicsUnit.Pixel);
            og.Flush();
        }

        private void shuffle(ref int[] array)
        {
            Random rnd = new Random();
            int n = array.Length;
            while (n > 1)
            {
                int k = rnd.Next(n);
                n--;
                int temp = array[n];
                array[n] = array[k];
                array[k] = temp;

            }
        }

        private void Game_Load(object sender, EventArgs e)
        {
            date = DateTime.Now;
           Timer timer = new Timer();
            timer1.Interval = 10;
            timer1.Tick += new EventHandler(timer1_Tick);
            
            ////Делаем таймер доступным
            //timer1.Enabled = true;
            //Запускаем таймер
            timer1.Start();
           

            groupBox1.Size = new System.Drawing.Size(600, 420);
          //  flowLayoutPanel1.Size = new System.Drawing.Size(600, 420);
            //image = Image.FromFile();
            CreateBitmapImage();
            ShowImage();
            createFrag();
            //createFragTale();
        }
        private void ShowImage()
        {
            image = CreateBitmapImage();
            if (pic == null)
            {
                pic = new PictureBox();
                pic.Height = groupBox1.Height;
                pic.Width = groupBox1.Width;
              //  groupBox1.Controls.Add(pic);

                //pic.Height = flowLayoutPanel1.Height;
                //pic.Width = flowLayoutPanel1.Width;
                //flowLayoutPanel1.Controls.Add(pic);

            }
            pic.Image = image;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            long tick = DateTime.Now.Ticks - date.Ticks;
            DateTime stopWatch = new DateTime();

            stopWatch = stopWatch.AddTicks(tick);
            time.Text = String.Format("{0:mm:ss}", stopWatch);
        }

        private void view_pic_Click(object sender, EventArgs e)
        {
            //Picture p = new Picture();
            //указать путь к выбранной пользователем картинке
            Picture picture = new Picture(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"..\..\gallery\cat4.jpg"));
            picture.ShowDialog();
            //p.Show();
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

            Image img = Image.FromFile(@"..\..\gallery\cat4.jpg");

            Bitmap objBmImage = new Bitmap(groupBox1.Width, groupBox1.Height);
            //Bitmap objBmImage = new Bitmap(flowLayoutPanel1.Width, flowLayoutPanel1.Height);
            Graphics objGraphics = Graphics.FromImage(objBmImage);
            objGraphics.Clear(Color.White);
            objGraphics.DrawImage(img, new Rectangle(0, 0, groupBox1.Width, groupBox1.Height));
            //objGraphics.DrawImage(img, new Rectangle(0, 0, flowLayoutPanel1.Width, flowLayoutPanel1.Height));
            objGraphics.Flush();

            return objBmImage;           
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        
    }
}
