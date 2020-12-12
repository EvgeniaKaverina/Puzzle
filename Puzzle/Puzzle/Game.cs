using System;
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
using System.Runtime.Serialization.Formatters.Binary;

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
        MyPictureBox[] pictureBoxes = null;
        MyPictureBox[] pictureBoxOnTale = null;
        Image[] images = null;

        PictureBox[][] pictureBoxesTriangle = null;
        PictureBox[][] pictureBoxesTriangleOnTale = null;
        Image[][] imagesTriangle = null;

        int id_puzzle;
        int level;
        string picture_name;
        int number_level;
        bool location;

        MyPictureBox firstBox = null;
        MyPictureBox secondBox = null;
        MyPictureBox taleBox = null;
        int numCols;
        int numRows;
        bool type;

        int count_points;

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
                pictureBoxes = new MyPictureBox[level];
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
                pictureBoxes = new MyPictureBox[level];
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
                pictureBoxOnTale = new MyPictureBox[level];
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

                //проверка на правильность и уменьшение очков 
                //снимает очки, когда пытаешься поменять с фиксированным фрагментом два раза
                for (int i = 0; i < level; i++)
                {
                    if (firstBox.ImageIndex == ((MyPictureBox)pictureBoxes[i]).Index)
                    {
                        count_points = count_points - 1;
                    }

                }
                //count_points = count_points - 1;

                if (count_points<0)
                {
                    count_points = 0;
                }
              
                points.Text = "Количество очков: " + count_points.ToString();
                

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
                   //     pictureBoxes[i].Image = ChangeBrightness(btm, 0.7f);
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
            //размер groupBox
            groupBox1.Size = new System.Drawing.Size(600, 420);
            CreateBitmapImage();
            ShowImage();
            getLevelSettings();
            getPuzzle();
            if (location && type)
            {
                createFragmentsOnField();
                createFragTale();
            }else if (!location && type)
            {
                createFrag();
                flowLayoutPanel1.Visible = false;
            }
            else if (location && !type)
            {
                numRows = numRows / 2;
                level = numRows * numCols;
                createTriangleFragmentsOnField();
                createFragTaleTriangle();
            }
            else if (!location && !type)
            {
                numRows = numRows / 2;
                level = numRows * numCols;
                createFragTriangle();
                flowLayoutPanel1.Visible = false;
            }

            date = DateTime.Now;
            Timer timer = new Timer();
            timer1.Interval = 10;
            timer1.Tick += new EventHandler(timer1_Tick);

            ////Делаем таймер доступным
            //timer1.Enabled = true;
            //Запускаем таймер
            timer1.Start();

            
            count_points = 30 * number_level;
            points.Text = "Количество очков: " + count_points.ToString();

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

        private void getPuzzle()
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM [Puzzles] where number_level=@num and image=@pic_name", sqlConnection);
            command.Parameters.AddWithValue("num", number_level);
            command.Parameters.AddWithValue("pic_name", picture_name);

            SqlDataReader reader = null;
            reader = command.ExecuteReader();


            while (reader.Read())
            {

                id_puzzle=Int32.Parse(Convert.ToString(reader["id_puzzle"]));
                location = Boolean.Parse(Convert.ToString(reader["location"]));
            }
           
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //прямоугольный на поле
            //прямоугольный на ленте
            if (type)
            {
                int[] matr = new int[level];
                for (int i = 0; i < level; i++)
                {
                    matr[i] = ((MyPictureBox)pictureBoxes[i]).ImageIndex;
                }
                MemoryStream stream = new MemoryStream();
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, matr);

                sqlConnection = new SqlConnection(connectionString);
                await sqlConnection.OpenAsync();

                SqlCommand command = new SqlCommand("INSERT INTO [Puzzles] (id_puzzle, login,time,points, unfinished) VALUES(@id_puzzle, @login, @time, @points,@matrix) ", sqlConnection);
                command.Parameters.AddWithValue("@id_puzzle", id_puzzle);
                command.Parameters.AddWithValue("@login", user);
                command.Parameters.AddWithValue("@points", id_puzzle);//добавить очки
                command.Parameters.AddWithValue("@time", timer1);//как брать время?

                command.Parameters.Add("@matrix", System.Data.SqlDbType.VarBinary);
                command.Parameters["@matrix"].Value = stream.ToArray();

                await command.ExecuteNonQueryAsync();
            }
            //треугольный на поле
            //треугольный на ленте
            else
            {
                int[] matr = new int[level * 2];
                for (int i = 0; i < level * 2; i += 2)
                {
                    matr[i] = ((MyPictureBox)pictureBoxesTriangle[i][0]).ImageIndex;
                    matr[i + 1] = ((MyPictureBox)pictureBoxesTriangle[i][1]).ImageIndex;
                }
                MemoryStream stream = new MemoryStream();
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, matr);

                sqlConnection = new SqlConnection(connectionString);
                await sqlConnection.OpenAsync();

                SqlCommand command = new SqlCommand("INSERT INTO [Puzzles] (id_puzzle, login,time,points, unfinished) VALUES(@id_puzzle, @login, @time, @points,@matrix) ", sqlConnection);
                command.Parameters.AddWithValue("@id_puzzle", id_puzzle);
                command.Parameters.AddWithValue("@login", user);
                command.Parameters.AddWithValue("@points", id_puzzle);//добавить очки
                command.Parameters.AddWithValue("@time", timer1);//как брать время?

                command.Parameters.Add("@matrix", System.Data.SqlDbType.VarBinary);
                command.Parameters["@matrix"].Value = stream.ToArray();

                await command.ExecuteNonQueryAsync();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        //public static Image ChangeBrightness(Bitmap image, float brightness)
        //{
        //    ImageAttributes imageAttributes = new ImageAttributes();
        //    int width = image.Width;
        //    int height = image.Height;

        //    float[][] colorMatrixElements = {
        //    new float[] { brightness, 0, 0, 0, 0},
        //    new float[] {0, brightness, 0, 0, 0},
        //    new float[] {0, 0, brightness, 0, 0},
        //    new float[] {0, 0, 0, 1, 0},
        //    new float[] {0, 0, 0, 0, 1}
        //     };

        //    ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

        //    imageAttributes.SetColorMatrix(
        //        colorMatrix,
        //        ColorMatrixFlag.Default,
        //        ColorAdjustType.Bitmap);
        //    Graphics graphics = Graphics.FromImage(image);

        //    graphics.DrawImage(image, new Rectangle(0, 0, width, height), 0, 0, width,
        //        height,
        //        GraphicsUnit.Pixel,
        //        imageAttributes);
        //    return image;
        //}
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
            if (box1.ImageIndex != -150 && box2.ImageIndex != -150)
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
                    //    pictureBoxesTriangle[i][0].Image = ChangeBrightness(btm, 0.3f);
                    }
                    pictureBoxesTriangle[i][0].Enabled = false;
                  
                }
                if (Math.Abs(((MyPictureBox)pictureBoxesTriangle[i][1]).ImageIndex) == Math.Abs(((MyPictureBox)pictureBoxesTriangle[i][1]).Index))
                {
                    if (pictureBoxesTriangle[i][1].Enabled)
                    {
                        Bitmap btm = (Bitmap)pictureBoxesTriangle[i][1].Image;
                    //    pictureBoxesTriangle[i][1].Image = ChangeBrightness(btm, 0.3f);
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

        private void CreateBitmapImageTriangleOnTale(Image image, Image[][] images, int index, int j, int numRows, int numCols, int unitX, int unitY)
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
            pictureBoxesTriangleOnTale[index][j].Image = images[index][j];
            Graphics g = pictureBoxesTriangleOnTale[index][j].CreateGraphics();
            g.Clip = new Region(path);
            g.DrawImage(pictureBoxesTriangleOnTale[index][j].Image, new Point(0, 0));
            g.Flush();
        }

        private void createTriangleFragmentsOnField()
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
                //        imagesTriangle = new Image[level][];
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
                    //    imagesTriangle[i] = new Image[2];

                    pictureBoxesTriangle[i][0].Click += new EventHandler(OnClickTriangle);
                    pictureBoxesTriangle[i][0].BorderStyle = BorderStyle.Fixed3D;
                    pictureBoxesTriangle[i][1].Click += new EventHandler(OnClickTriangle);
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

                pictureBoxesTriangle[i][0].Location = new Point(unitX * (i % numCols), unitY * (i / numCols));
                pictureBoxesTriangle[i][1].Location = new Point(unitX * (i % numCols), unitY * (i / numCols));
                if (!groupBox1.Controls.Contains(pictureBoxesTriangle[i][0]))
                    groupBox1.Controls.Add(pictureBoxesTriangle[i][0]);
                if (!groupBox1.Controls.Contains(pictureBoxesTriangle[i][1]))
                    groupBox1.Controls.Add(pictureBoxesTriangle[i][1]);
            }

            for (int i = 0; i < level; i++)
            {

                ((MyPictureBox)pictureBoxesTriangle[i][0]).ImageIndex = -150;
                ((MyPictureBox)pictureBoxesTriangle[i][1]).ImageIndex = -150;
            }

        }
        private void createFragTaleTriangle()//лента 
        {
            if (pic != null)
            {
                flowLayoutPanel1.Controls.Remove(pic);
                pic.Dispose();
                pic = null;

            }
            if (pictureBoxesTriangleOnTale == null)
            {
                pictureBoxesTriangleOnTale = new PictureBox[level][];

                imagesTriangle = new Image[level][];
            }

            int unitX = groupBox1.Width / numCols;
            int unitY = groupBox1.Height / numRows;
            flowLayoutPanel1.Height = unitY + 50;
            int[] indice = new int[level];
            for (int i = 0; i < level; i++)
            {
                indice[i] = i;
                if (pictureBoxesTriangleOnTale[i] == null)
                {
                    pictureBoxesTriangleOnTale[i] = new TriangularPictureBox[2];
                    pictureBoxesTriangleOnTale[i][0] = new TriangularPictureBox();
                    pictureBoxesTriangleOnTale[i][1] = new TriangularPictureBox();
                    imagesTriangle[i] = new Image[2];

                    pictureBoxesTriangleOnTale[i][0].Click += new EventHandler(OnPuzzleClickTale);
                    pictureBoxesTriangleOnTale[i][1].Click += new EventHandler(OnPuzzleClickTale);
                    pictureBoxesTriangleOnTale[i][0].BorderStyle = BorderStyle.Fixed3D;
                    pictureBoxesTriangleOnTale[i][1].BorderStyle = BorderStyle.Fixed3D;
                    ((TriangularPictureBox)pictureBoxesTriangleOnTale[i][0]).LeftGrag = false;
                    ((TriangularPictureBox)pictureBoxesTriangleOnTale[i][1]).LeftGrag = true;
                    // pictureBoxes[i].Size = new System.Drawing.Size(unitX, unitY);
                }

                pictureBoxesTriangleOnTale[i][0].Width = unitX;
                pictureBoxesTriangleOnTale[i][0].Height = unitY;
                pictureBoxesTriangleOnTale[i][1].Width = unitX;
                pictureBoxesTriangleOnTale[i][1].Height = unitY;

                ((MyPictureBox)pictureBoxesTriangleOnTale[i][0]).Index = i;
                ((MyPictureBox)pictureBoxesTriangleOnTale[i][1]).Index = -i;

                CreateBitmapImageTriangleOnTale(image, imagesTriangle, i, 0, numRows, numCols, unitX, unitY);
                CreateBitmapImageTriangleOnTale(image, imagesTriangle, i, 1, numRows, numCols, unitX, unitY);
                pictureBoxesTriangleOnTale[i][0].Location = new Point(unitX * (i % numCols), unitY * (i / numCols));
                pictureBoxesTriangleOnTale[i][1].Location = new Point(unitX * (i % numCols), unitY * (i / numCols));
                if (!flowLayoutPanel1.Controls.Contains(pictureBoxesTriangleOnTale[i][0]))
                    flowLayoutPanel1.Controls.Add(pictureBoxesTriangleOnTale[i][0]);
                if (!flowLayoutPanel1.Controls.Contains(pictureBoxesTriangleOnTale[i][1]))
                    flowLayoutPanel1.Controls.Add(pictureBoxesTriangleOnTale[i][1]);
            }
            shuffle(ref indice);
            for (int i = 0; i < level; i++)
            {
                pictureBoxesTriangleOnTale[i][0].Image = imagesTriangle[indice[i]][0];
                ((MyPictureBox)pictureBoxesTriangleOnTale[i][0]).ImageIndex = indice[i];
            }
            shuffle(ref indice);
            for (int i = 0; i < level; i++)
            {
                pictureBoxesTriangleOnTale[i][1].Image = imagesTriangle[indice[i]][1];
                ((MyPictureBox)pictureBoxesTriangleOnTale[i][1]).ImageIndex = indice[i];
            }

        }
        private void SwitchFieldAndTaleTriangle(MyPictureBox tale, MyPictureBox box)
        {
            if (box.ImageIndex == -150)
            {
                if (box.Index <= 0 && tale.Index <= 0)
                {
                    box.Image = imagesTriangle[tale.ImageIndex][0];
                    box.ImageIndex = tale.ImageIndex;
                    flowLayoutPanel1.Controls.Remove(tale);
                }
                else if (box.Index >= 0 && tale.Index >= 0)
                {
                    box.Image = imagesTriangle[tale.ImageIndex][1];
                    box.ImageIndex = tale.ImageIndex;
                    flowLayoutPanel1.Controls.Remove(tale);
                }

                if (isFinishedTriangle())
                {
                    //Останавливаем таймер
                       timer1.Stop();
                    MessageBox.Show("Well done!");

                }
            }
        }
        private void OnClickTriangle(object sender, EventArgs e)
        {

            firstBox = (MyPictureBox)sender;
            firstBox.BorderStyle = BorderStyle.FixedSingle;
            if (taleBox != null)
            {
                SwitchFieldAndTaleTriangle(taleBox, firstBox);
                //  firstBox.Click -= null;
                firstBox.Click -= new EventHandler(OnClickTriangle);
                ((MyPictureBox)sender).Click += new EventHandler(OnPuzzleClickTriangle);
                firstBox = null;
                taleBox = null;
            }
        }
        //подсказка
        int help_counter = 3;
        private void help_Click(object sender, EventArgs e)
        {
            if (help_counter > 0)
            {
                //лента прямоугольные
                if (location && type)
                {
                    if (taleBox != null)
                    {
                        for (int i = 0; i < level; i++)
                        {
                            if (taleBox.ImageIndex == ((MyPictureBox)pictureBoxes[i]).Index)
                            {
                                if (((MyPictureBox)pictureBoxes[i]).ImageIndex == -1)

                                {
                                    ((MyPictureBox)pictureBoxes[i]).ImageIndex = taleBox.ImageIndex;
                                    pictureBoxes[i].Image = images[taleBox.ImageIndex];

                                    flowLayoutPanel1.Controls.Remove(taleBox);
                                    pictureBoxes[i].Click += new EventHandler(OnPuzzleClick);
                                }
                                else SwitchImage(taleBox, pictureBoxes[i]);
                                taleBox = null;
                                isFinished();
                                help_counter--;
                                return;
                            }
                        }
                    }
                    else if (firstBox != null)
                    {
                        for (int i = 0; i < level; i++)
                        {
                            if (firstBox.ImageIndex == ((MyPictureBox)pictureBoxes[i]).Index)
                            {
                                SwitchImage(firstBox, pictureBoxes[i]);
                                isFinished();
                                firstBox = null;
                                help_counter--;
                                return;
                            }
                        }
                    }
                    else MessageBox.Show("Выберите фрагмент");

                }
                //поле прямоугольные
                else if (!location && type)
                {
                    if (firstBox != null)
                    {
                        for (int i = 0; i < level; i++)
                        {
                            if (firstBox.ImageIndex == ((MyPictureBox)pictureBoxes[i]).Index)
                            {
                                SwitchImage(firstBox, pictureBoxes[i]);
                                firstBox = null;
                                isFinished();
                                help_counter--;
                                return;
                            }
                        }

                    }
                    else MessageBox.Show("Выберите фрагмент");

                }
                //лента треугольные
                else if (location && !type)
                {
                    if (taleBox != null)
                    {
                        for (int i = 0; i < level; i++)
                        {

                            if (((TriangularPictureBox)firstBox).LeftGrag)
                            {
                                if (taleBox.ImageIndex == Math.Abs(((MyPictureBox)pictureBoxesTriangle[i][1]).Index))
                                {
                                    if (((MyPictureBox)pictureBoxesTriangle[i][1]).ImageIndex == -150)

                                    {
                                        ((MyPictureBox)pictureBoxesTriangle[i][1]).ImageIndex = taleBox.ImageIndex;
                                        pictureBoxesTriangle[i][1].Image = images[taleBox.ImageIndex];

                                        flowLayoutPanel1.Controls.Remove(taleBox);
                                        pictureBoxesTriangle[i][1].Click += new EventHandler(OnPuzzleClick);
                                    }
                                    else SwitchImageTriangle(taleBox, (MyPictureBox)pictureBoxesTriangle[i][1], 1);
                                    isFinishedTriangle();
                                    help_counter--;
                                    return;
                                }
                            }
                            else
                            {
                                if (taleBox.ImageIndex == Math.Abs(((MyPictureBox)pictureBoxesTriangle[i][0]).Index))
                                {
                                    if (((MyPictureBox)pictureBoxesTriangle[i][0]).ImageIndex == -150)

                                    {
                                        ((MyPictureBox)pictureBoxesTriangle[i][0]).ImageIndex = taleBox.ImageIndex;
                                        pictureBoxesTriangle[i][0].Image = images[taleBox.ImageIndex];

                                        flowLayoutPanel1.Controls.Remove(taleBox);
                                        pictureBoxesTriangle[i][0].Click += new EventHandler(OnPuzzleClick);
                                    }
                                    else SwitchImageTriangle(taleBox, (MyPictureBox)pictureBoxesTriangle[i][0], 0);
                                    isFinishedTriangle();
                                    help_counter--;
                                    return;
                                }
                            }
                        }
                    }
                    else if (firstBox != null)
                    {
                        for (int i = 0; i < level; i++)
                        {
                            if (((TriangularPictureBox)firstBox).LeftGrag)
                            {
                                if (firstBox.ImageIndex == Math.Abs(((MyPictureBox)pictureBoxesTriangle[i][1]).Index))
                                {
                                    SwitchImageTriangle(firstBox, (MyPictureBox)pictureBoxesTriangle[i][1], 1);
                                    firstBox = null;
                                    help_counter--;
                                    return;
                                }
                            }
                            else
                            {
                                if (firstBox.ImageIndex == Math.Abs(((MyPictureBox)pictureBoxesTriangle[i][0]).Index))
                                {
                                    SwitchImageTriangle(firstBox, (MyPictureBox)pictureBoxesTriangle[i][0], 0);
                                    firstBox = null;
                                    help_counter--;
                                    return;
                                }
                            }

                        }

                    }
                    else MessageBox.Show("Выберите фрагмент");
                }
                //поле треугольные
                else if (!location && !type)
                {
                    if (firstBox != null)
                    {
                        for (int i = 0; i < level; i++)
                        {
                            if (((TriangularPictureBox)firstBox).LeftGrag)
                            {
                                if (firstBox.ImageIndex == Math.Abs(((MyPictureBox)pictureBoxesTriangle[i][1]).Index))
                                {
                                    SwitchImageTriangle(firstBox, (MyPictureBox)pictureBoxesTriangle[i][1], 1);
                                    firstBox = null;
                                    help_counter--;
                                    return;
                                }
                            }
                            else
                            {
                                if (firstBox.ImageIndex == Math.Abs(((MyPictureBox)pictureBoxesTriangle[i][0]).Index))
                                {
                                    SwitchImageTriangle(firstBox, (MyPictureBox)pictureBoxesTriangle[i][0], 0);
                                    firstBox = null;
                                    help_counter--;
                                    return;
                                }
                            }

                        }

                    }
                    else MessageBox.Show("Выберите фрагмент");
                }
            }
            else MessageBox.Show("У вас закончились подсказки!");
        }
    }
}
