﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace Puzzle
{
    public partial class Game : Form
    {
        string connectionString = "Data Source=localhost;Initial Catalog=Puzzle;Integrated Security=True";
        private SqlConnection sqlConnection;
        DateTime date; 

        public Game()
        {
            InitializeComponent();
        }

        public Game(string picture_name, int number, string user)
        {
            InitializeComponent();
            this.picture_name = picture_name;
            this.number_level = number;
            this.user = user;
        }
        string user;
        Image image;
        PictureBox pic = null;
        PictureBox[] pictureBoxes = null;
        PictureBox[] pictureBoxOnTale = null;
        Image[] images = null;

        PictureBox[][] pictureBoxesTriangle = null;
        Image[][] imagesTriangle = null;

        int level;
        string picture_name;
        int number_level;

        MyPictureBox firstBox = null;
        MyPictureBox secondBox = null;
        MyPictureBox taleBox = null;
        int numCols;
        int numRows;
        bool type;

        private void createFrag()
        {
          //запрос в бд
          //треугольные фрагменты
          //сохранение пазла в бд (незаконченного)
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
            int unitX = groupBox1.Width / numCols;
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

                CreateBitmapImage(image, images,i, numRows, numCols, unitX, unitY);
                pictureBoxes[i].Location = new Point(unitX * (i % numCols), unitY * (i / numCols));
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
        private void createFragmentsOnField()
        {
      
            if (pic != null)
            {
                groupBox1.Controls.Remove(pic);
                pic.Dispose();
                pic = null;

            }
            if (pictureBoxes == null)
            {
                pictureBoxes = new PictureBox[level];
              //  images = new Image[level];
            }
            int unitX = groupBox1.Width / numCols;
            int unitY = groupBox1.Height / numRows;
            int[] indice = new int[level];
            for (int i = 0; i < level; i++)
            {
                indice[i] = i;
                if (pictureBoxes[i] == null)
                {
                    pictureBoxes[i] = new MyPictureBox();
                    pictureBoxes[i].Click += new EventHandler(OnClick);
                    pictureBoxes[i].BorderStyle = BorderStyle.Fixed3D;
                }
                pictureBoxes[i].Width = unitX;
                pictureBoxes[i].Height = unitY;

                ((MyPictureBox)pictureBoxes[i]).Index = i;

                //    CreateBitmapImage(image, images, i, numRows, numCols, unitX, unitY);
                pictureBoxes[i].Location = new Point(unitX * (i % numCols), unitY * (i / numCols));
                if (!groupBox1.Controls.Contains(pictureBoxes[i]))
                    groupBox1.Controls.Add(pictureBoxes[i]);
            }
            for (int i = 0; i < level; i++)
            {
              
                ((MyPictureBox)pictureBoxes[i]).ImageIndex = -1;
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
            if (pictureBoxOnTale == null)
            {
                pictureBoxOnTale = new PictureBox[level];
                images = new Image[level];
            }

            int unitX = groupBox1.Width / numCols;
            int unitY = groupBox1.Height / numRows;
            flowLayoutPanel1.Height = unitY + 50;
            int[] indice = new int[level];
            for (int i = 0; i < level; i++)
            {
                indice[i] = i;
                if (pictureBoxOnTale[i] == null)
                {
                    pictureBoxOnTale[i] = new MyPictureBox();
                    pictureBoxOnTale[i].Click += new EventHandler(OnPuzzleClickTale);
                    pictureBoxOnTale[i].BorderStyle = BorderStyle.Fixed3D;
                   // pictureBoxes[i].Size = new System.Drawing.Size(unitX, unitY);
                }
                pictureBoxOnTale[i].Width = unitX;
                pictureBoxOnTale[i].Height = unitY;

                ((MyPictureBox)pictureBoxOnTale[i]).Index = -1;

                CreateBitmapImage(image, images, i, numRows, numCols, unitX, unitY);
                pictureBoxOnTale[i].Location = new Point(unitX * (i % numCols), unitY * (i / numCols));
                if (!flowLayoutPanel1.Controls.Contains(pictureBoxOnTale[i]))
                    flowLayoutPanel1.Controls.Add(pictureBoxOnTale[i]);
            }
            shuffle(ref indice);
            for (int i = 0; i < level; i++)
            {
                pictureBoxOnTale[i].Image = images[indice[i]];
                ((MyPictureBox)pictureBoxOnTale[i]).ImageIndex = indice[i];
            }

        }

        private void OnPuzzleClickTale(object sender, EventArgs e)
        {
           
            
            if (taleBox != null)
            {
                
                taleBox.BorderStyle = BorderStyle.Fixed3D;
                //firstBox = (MyPictureBox)sender;
                //firstBox.BorderStyle = BorderStyle.FixedSingle;
                //SwitchFieldAndTale(taleBox, firstBox);
                //taleBox = null;
                //firstBox = null;
                taleBox = (MyPictureBox)sender;
                taleBox.BorderStyle = BorderStyle.FixedSingle;
            }
            else
            {
                taleBox = (MyPictureBox)sender;
                taleBox.BorderStyle = BorderStyle.FixedSingle;
            }

            
            
        }

        private void SwitchFieldAndTale(MyPictureBox tale, MyPictureBox box)
        {
            if (box.ImageIndex == -1)
            {
                box.Image = images[tale.ImageIndex];
                box.ImageIndex = tale.ImageIndex;
                flowLayoutPanel1.Controls.Remove(tale);
                if (isFinished())
                {
                    //Останавливаем таймер
                    timer1.Stop();
                    MessageBox.Show("Well done!");

                }
            }
        }
        private void OnClick(object sender, EventArgs e)
        {
            
            firstBox = (MyPictureBox)sender;
            firstBox.BorderStyle = BorderStyle.FixedSingle;
            if (taleBox != null)
            {
                SwitchFieldAndTale(taleBox, firstBox);
                //  firstBox.Click -= null;
                firstBox.Click -= new EventHandler(OnClick);
                ((MyPictureBox)sender).Click +=new  EventHandler(OnPuzzleClick);
                firstBox = null;
                taleBox = null;
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
                taleBox = null;
            }


           
           // ((MyPictureBox)sender).BorderStyle = BorderStyle.FixedSingle;
        }
        private void SwitchImage(MyPictureBox box1,MyPictureBox box2)//createFrag
        {
            if (box1.ImageIndex != -1 && box2.ImageIndex != -1)
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
        }
        private bool isFinished()//createFrag
        {
            for(int i = 0; i < level; i++)
            {
                if(((MyPictureBox)pictureBoxes[i]).ImageIndex== ((MyPictureBox)pictureBoxes[i]).Index)
                {
                    if (pictureBoxes[i].Enabled)
                    {
                        Bitmap btm = (Bitmap)pictureBoxes[i].Image;
                        pictureBoxes[i].Image = ChangeBrightness(btm, 0.7f);
                    }
                    pictureBoxes[i].Enabled = false;
                  //  pictureBoxes[i].Click -= null;

                   

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
            getLevelSettings();
            //размер groupBox
            groupBox1.Size = new System.Drawing.Size(600, 420);
            //  flowLayoutPanel1.Size = new System.Drawing.Size(600, 420);
            //image = Image.FromFile();
            
            CreateBitmapImage();
            ShowImage();
            //  createFrag();

            //createFragTale();
            //createFragmentsOnField();

            numRows = numRows / 2;
            level = numRows * numCols;
            createFragTriangle();

            date = DateTime.Now;
            Timer timer = new Timer();
            timer1.Interval = 10;
            timer1.Tick += new EventHandler(timer1_Tick);

            ////Делаем таймер доступным
            //timer1.Enabled = true;
            //Запускаем таймер
            timer1.Start();

        }
        private void ShowImage()
        {
            image = CreateBitmapImage();
            if (pic == null)
            {
                pic = new PictureBox();
                pic.Height = groupBox1.Height;
                pic.Width = groupBox1.Width;
           

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
            Picture picture = new Picture(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"..\..\gallery\"+picture_name));
            picture.ShowDialog();
            //p.Show();
        }

        private Bitmap CreateBitmapImage()
        {
          

            Image img = Image.FromFile(@"..\..\gallery\"+picture_name);

            Bitmap objBmImage = new Bitmap(groupBox1.Width, groupBox1.Height);
            //Bitmap objBmImage = new Bitmap(flowLayoutPanel1.Width, flowLayoutPanel1.Height);
            Graphics objGraphics = Graphics.FromImage(objBmImage);
            objGraphics.Clear(Color.White);
            objGraphics.DrawImage(img, new Rectangle(0, 0, groupBox1.Width, groupBox1.Height));
            //objGraphics.DrawImage(img, new Rectangle(0, 0, flowLayoutPanel1.Width, flowLayoutPanel1.Height));
            objGraphics.Flush();

            return objBmImage;           
        }

        private void getLevelSettings()
        {
           
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM [Level] where number=@num", sqlConnection);
            command.Parameters.AddWithValue("num", number_level);
          
            SqlDataReader reader = null;
            reader =  command.ExecuteReader();
            

            while ( reader.Read())
            {
    
                numCols = Int32.Parse(Convert.ToString(reader["count_of_piece_horizontally"]));
                numRows= Int32.Parse(Convert.ToString(reader["count_of_piece_vertically"]));
                type = Boolean.Parse(Convert.ToString(reader["type_of_piece"]));
            }
            level = numRows * numCols;

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        public static Image ChangeBrightness(Bitmap image, float brightness)
        {
            ImageAttributes imageAttributes = new ImageAttributes();
            int width = image.Width;
            int height = image.Height;

            float[][] colorMatrixElements = {
            new float[] { brightness, 0, 0, 0, 0},
            new float[] {0, brightness, 0, 0, 0},
            new float[] {0, 0, brightness, 0, 0},
            new float[] {0, 0, 0, 1, 0},
            new float[] {0, 0, 0, 0, 1}
             };

            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

            imageAttributes.SetColorMatrix(
                colorMatrix,
                ColorMatrixFlag.Default,
                ColorAdjustType.Bitmap);
            Graphics graphics = Graphics.FromImage(image);

            graphics.DrawImage(image, new Rectangle(0, 0, width, height), 0, 0, width,
                height,
                GraphicsUnit.Pixel,
                imageAttributes);
            return image;
        }
        private void createFragTriangle()
        {

            if (pic != null)
            {
                groupBox1.Controls.Remove(pic);
                pic.Dispose();
                pic = null;

            }
            if (pictureBoxesTriangle == null)
            {
                pictureBoxesTriangle = new PictureBox[level][];
                imagesTriangle = new Image[level][];
            }
           
            int unitX = groupBox1.Width / numCols;
            int unitY = groupBox1.Height / numRows;
            int[] indice = new int[level];
            for (int i = 0; i < level; i++)
            {
                indice[i] = i;
                if (pictureBoxesTriangle[i] == null)
                {
                    pictureBoxesTriangle[i] = new TriangularPictureBox[2];
                    pictureBoxesTriangle[i][0] = new TriangularPictureBox();
                    pictureBoxesTriangle[i][1] = new TriangularPictureBox();
                    imagesTriangle[i] = new Image[2];

                    pictureBoxesTriangle[i][0].Click += new EventHandler(OnPuzzleClickTriangle);
                    pictureBoxesTriangle[i][0].BorderStyle = BorderStyle.Fixed3D;
                    pictureBoxesTriangle[i][1].Click += new EventHandler(OnPuzzleClickTriangle);
                    pictureBoxesTriangle[i][1].BorderStyle = BorderStyle.Fixed3D;

                    ((TriangularPictureBox)pictureBoxesTriangle[i][0]).LeftGrag = false;
                    ((TriangularPictureBox)pictureBoxesTriangle[i][1]).LeftGrag = true;
                }
                pictureBoxesTriangle[i][0].Width = unitX;
                pictureBoxesTriangle[i][0].Height = unitY;
                pictureBoxesTriangle[i][1].Width = unitX;
                pictureBoxesTriangle[i][1].Height = unitY;

                ((MyPictureBox)pictureBoxesTriangle[i][0]).Index = i;
                ((MyPictureBox)pictureBoxesTriangle[i][1]).Index = -i;



                CreateBitmapImageTriangle(image, imagesTriangle, i, 0, numRows, numCols, unitX, unitY);
                CreateBitmapImageTriangle(image, imagesTriangle, i, 1, numRows, numCols, unitX, unitY);
                pictureBoxesTriangle[i][0].Location = new Point(unitX * (i % numCols), unitY * (i / numCols));
                pictureBoxesTriangle[i][1].Location = new Point(unitX * (i % numCols), unitY * (i / numCols));
                if (!groupBox1.Controls.Contains(pictureBoxesTriangle[i][0]))
                    groupBox1.Controls.Add(pictureBoxesTriangle[i][0]);
                if (!groupBox1.Controls.Contains(pictureBoxesTriangle[i][1]))
                    groupBox1.Controls.Add(pictureBoxesTriangle[i][1]);
            }
            shuffle(ref indice);
            for (int i = 0; i < level; i++)
            {

                pictureBoxesTriangle[i][0].Image = imagesTriangle[indice[i]][0];
                ((MyPictureBox)pictureBoxesTriangle[i][0]).ImageIndex = indice[i];

            }
            shuffle(ref indice);
            for (int i = 0; i < level; i++)
            {

                pictureBoxesTriangle[i][1].Image = imagesTriangle[indice[i]][1];
                ((MyPictureBox)pictureBoxesTriangle[i][1]).ImageIndex = indice[i];
            }

        }
        private void OnPuzzleClickTriangle(object sender, EventArgs e)
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
                if (secondBox.Index <= 0 && firstBox.Index <= 0)
                    SwitchImageTriangle(firstBox, secondBox, 0);
                else if (secondBox.Index >= 0 && firstBox.Index >= 0)
                    SwitchImageTriangle(firstBox, secondBox, 1);
                firstBox = null;
                secondBox = null;
            }

            // ((MyPictureBox)sender).BorderStyle = BorderStyle.FixedSingle;
        }
        private void SwitchImageTriangle(MyPictureBox box1, MyPictureBox box2, int i)
        {
            int tmp = box2.ImageIndex;
            box2.Image = imagesTriangle[box1.ImageIndex][i];
            box2.ImageIndex = box1.ImageIndex;
            box1.Image = imagesTriangle[tmp][i];
            box1.ImageIndex = tmp;
            if (isFinishedTriangle())
            {
                timer1.Stop();
                MessageBox.Show("Well done!");
                //ShowImage();
            }
        }
        private bool isFinishedTriangle()
        {
            for (int i = 0; i < level; i++)
            {
                if (Math.Abs(((MyPictureBox)pictureBoxesTriangle[i][0]).ImageIndex) == Math.Abs(((MyPictureBox)pictureBoxesTriangle[i][0]).Index))
                {
                    if (pictureBoxesTriangle[i][0].Enabled)
                    {
                        Bitmap btm = (Bitmap)pictureBoxesTriangle[i][0].Image;
                        pictureBoxesTriangle[i][0].Image = ChangeBrightness(btm, 0.7f);
                    }
                    pictureBoxesTriangle[i][0].Enabled = false;
                  
                }
                if (Math.Abs(((MyPictureBox)pictureBoxesTriangle[i][1]).ImageIndex) == Math.Abs(((MyPictureBox)pictureBoxesTriangle[i][1]).Index))
                {
                    if (pictureBoxesTriangle[i][1].Enabled)
                    {
                        Bitmap btm = (Bitmap)pictureBoxesTriangle[i][1].Image;
                        pictureBoxesTriangle[i][1].Image = ChangeBrightness(btm, 0.7f);
                    }
                    pictureBoxesTriangle[i][1].Enabled = false;
                  
                }
            }
            for (int i = 0; i < level; i++)
            {
                if (Math.Abs(((MyPictureBox)pictureBoxesTriangle[i][0]).ImageIndex) != Math.Abs(((MyPictureBox)pictureBoxesTriangle[i][0]).Index))
                    return false;

                if (Math.Abs(((MyPictureBox)pictureBoxesTriangle[i][1]).ImageIndex) != Math.Abs(((MyPictureBox)pictureBoxesTriangle[i][1]).Index))
                    return false;
            }
            return true;
        }
        private void CreateBitmapImageTriangle(Image image, Image[][] images, int index, int j, int numRows, int numCols, int unitX, int unitY)
        {
            images[index][j] = new Bitmap(unitX, unitY);
            Graphics og = Graphics.FromImage(images[index][j]);
            og.Clear(Color.White);

            og.DrawImage(image,

          new Rectangle(0, 0, unitX, unitY),

          new Rectangle(unitX * (index % numCols), unitY * (index / numCols), unitX, unitY),
          GraphicsUnit.Pixel);

            GraphicsPath path = new GraphicsPath();
            path.AddLines(new Point[] {
                new Point(0, 0),
                new Point(0, unitY),
                new Point(unitX, 0) });
            path.CloseAllFigures();
            pictureBoxesTriangle[index][j].Image = images[index][j];
            Graphics g = pictureBoxesTriangle[index][j].CreateGraphics();
            g.Clip = new Region(path);
            g.DrawImage(pictureBoxesTriangle[index][j].Image, new Point(0, 0));
            g.Flush();
        }



    }
}
