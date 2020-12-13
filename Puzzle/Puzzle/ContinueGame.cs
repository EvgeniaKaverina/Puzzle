using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.Serialization.Formatters.Binary;

namespace Puzzle
{
    public partial class ContinueGame : Form
    {
        string connectionString = "Data Source=localhost;Initial Catalog=Puzzle;Integrated Security=True";
        private SqlConnection sqlConnection;
        string user;
        Image image;
        PictureBox pic = null;
        MyPictureBox[] pictureBoxes = null;
        MyPictureBox[] pictureBoxOnTale = null;
        Image[] images = null;

        PictureBox[][] pictureBoxesTriangle = null;
        PictureBox[][] pictureBoxesTriangleOnTale = null;
        Image[][] imagesTriangle = null;

        int id_game;
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
        DateTime date;
   

        int count_points;
        public ContinueGame()
        {
            InitializeComponent();
        }
        public ContinueGame(string login)
        {
            InitializeComponent();
            user = login;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SavePuzzzle();
        }
        private int[] Continue()
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            sqlConnection = new SqlConnection(connectionString);
            //   await sqlConnection.OpenAsync();

            SqlCommand command = new SqlCommand("SELECT TOP 1 unfinished FROM [Game] where login=@login and unfinished is not null Order by id_game DESC", sqlConnection);
            command.Parameters.AddWithValue("login", user);
            sqlConnection.Open();
            //  await command.ExecuteNonQueryAsync();

            byte[] array = (byte[])command.ExecuteScalar();
            stream = new MemoryStream(array, 0, array.Length);
            int[] newMatrix = (int[])formatter.Deserialize(stream);

            command = new SqlCommand("SELECT TOP 1 * FROM [Game] where login=@login Order by id_game DESC", sqlConnection);
            command.Parameters.AddWithValue("login", user);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {

                id_puzzle = Int32.Parse(Convert.ToString(reader["id_puzzle"]));
                count_points = Int32.Parse(Convert.ToString(reader["points"]));
                date = DateTime.Parse(Convert.ToString(reader["time"]));
                // location = Boolean.Parse(Convert.ToString(reader["location"]));
            }
            reader.Close();

            command = new SqlCommand("SELECT * FROM [Puzzles] where id_puzzle=@id_puzzle", sqlConnection);
            command.Parameters.AddWithValue("id_puzzle", id_puzzle);


            SqlDataReader datareader = null;
            datareader = command.ExecuteReader();


            while (datareader.Read())
            {
                number_level = Int32.Parse(Convert.ToString(datareader["number_level"]));
                location = Boolean.Parse(Convert.ToString(datareader["location"]));
                picture_name = Convert.ToString(datareader["image"]);
            }

            getLevelSettings();
            return newMatrix;
            
        }
        private void createFragContinue(int[] matrix)
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
                images = new Image[level];
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
                    pictureBoxes[i].Click += new EventHandler(OnPuzzleClick);
                    pictureBoxes[i].BorderStyle = BorderStyle.Fixed3D;
                }
                pictureBoxes[i].Width = unitX;
                pictureBoxes[i].Height = unitY;

                ((MyPictureBox)pictureBoxes[i]).Index = i;

                CreateBitmapImage(image, images, i, numRows, numCols, unitX, unitY);
                pictureBoxes[i].Location = new Point(unitX * (i % numCols), unitY * (i / numCols));
                if (!groupBox1.Controls.Contains(pictureBoxes[i]))
                    groupBox1.Controls.Add(pictureBoxes[i]);
            }
            //  shuffle(ref indice);
            for (int i = 0; i < level; i++)
            {
                pictureBoxes[i].Image = images[matrix[i]];
                ((MyPictureBox)pictureBoxes[i]).ImageIndex = matrix[i];
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
        private async void SavePuzzzle()
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

                SqlCommand command = new SqlCommand("UPDATE  [Game] SET time=@time, points=@points, unfinished=@matrix where id_game=@id_game", sqlConnection);
                command.Parameters.AddWithValue("@id_game", id_game);
                command.Parameters.AddWithValue("@points", count_points);
                command.Parameters.AddWithValue("@time", DateTime.Now - date);

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
                    matr[i] = ((MyPictureBox)pictureBoxesTriangle[i / 2][0]).ImageIndex;
                    matr[i + 1] = ((MyPictureBox)pictureBoxesTriangle[i / 2][1]).ImageIndex;
                }
                MemoryStream stream = new MemoryStream();
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, matr);

                sqlConnection = new SqlConnection(connectionString);
                await sqlConnection.OpenAsync();

                SqlCommand command = new SqlCommand("UPDATE  [Game] SET time=@time, points=@points, unfinished=@matrix where id_game=@id_game", sqlConnection);
                command.Parameters.AddWithValue("@id_game", id_game);
                command.Parameters.AddWithValue("@points", count_points);
                command.Parameters.AddWithValue("@time", DateTime.Now - date);

                command.Parameters.Add("@matrix", System.Data.SqlDbType.VarBinary);
                command.Parameters["@matrix"].Value = stream.ToArray();

                await command.ExecuteNonQueryAsync();
            }
        }
        private void createFragContinueOnTale(int[] matrix)
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
                
                images = new Image[level];
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
                    pictureBoxes[i].Click += new EventHandler(OnPuzzleClick);
                    pictureBoxes[i].BorderStyle = BorderStyle.Fixed3D;
                }
                pictureBoxes[i].Width = unitX;
                pictureBoxes[i].Height = unitY;

                ((MyPictureBox)pictureBoxes[i]).Index = i;

                CreateBitmapImage(image, images, i, numRows, numCols, unitX, unitY);
                pictureBoxes[i].Location = new Point(unitX * (i % numCols), unitY * (i / numCols));
                if (!groupBox1.Controls.Contains(pictureBoxes[i]))
                    groupBox1.Controls.Add(pictureBoxes[i]);
            }
            //  shuffle(ref indice);
         
            for (int i = 0; i < level; i++)
            {
                if (matrix[i] >= 0)
                {
                    pictureBoxes[i].Image = images[matrix[i]];
                    ((MyPictureBox)pictureBoxes[i]).ImageIndex = matrix[i];
                    flowLayoutPanel1.Controls.Remove(pictureBoxOnTale[i]);
                }
                else
                {
                    pictureBoxOnTale[i].Image = images[matrix[i]];
                    ((MyPictureBox)pictureBoxOnTale[i]).ImageIndex = matrix[i];
                }
            }
        }
        private void getLevelSettings()
        {

            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM [Level] where number=@num", sqlConnection);
            command.Parameters.AddWithValue("num", number_level);

            SqlDataReader reader = null;
            reader = command.ExecuteReader();


            while (reader.Read())
            {

                numCols = Int32.Parse(Convert.ToString(reader["count_of_piece_horizontally"]));
                numRows = Int32.Parse(Convert.ToString(reader["count_of_piece_vertically"]));
                type = Boolean.Parse(Convert.ToString(reader["type_of_piece"]));
            }
            level = numRows * numCols;

        }

        private void ContinueGame_Load(object sender, EventArgs e)
        {
           
            groupBox1.Size = new System.Drawing.Size(600, 420);
            int[] matr=Continue();
            ShowImage();
            if (!location)
            {
                createFragContinue(matr);
            }
            else
            {
                createFragTale();
                createFragContinueOnTale(matr);
                
            }
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
        private Bitmap CreateBitmapImage()
        {

            Image img = Image.FromFile(@"..\..\gallery\" + picture_name);

            Bitmap objBmImage = new Bitmap(groupBox1.Width, groupBox1.Height);
            //Bitmap objBmImage = new Bitmap(flowLayoutPanel1.Width, flowLayoutPanel1.Height);
            Graphics objGraphics = Graphics.FromImage(objBmImage);
            objGraphics.Clear(Color.White);
            objGraphics.DrawImage(img, new Rectangle(0, 0, groupBox1.Width, groupBox1.Height));
            //objGraphics.DrawImage(img, new Rectangle(0, 0, flowLayoutPanel1.Width, flowLayoutPanel1.Height));
            objGraphics.Flush();

            return objBmImage;
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

            //указать путь к выбранной пользователем картинке
            Picture picture = new Picture(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"..\..\gallery\" + picture_name));
            picture.ShowDialog();

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
        private void SwitchImage(MyPictureBox box1, MyPictureBox box2)//createFrag
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
                //for (int i = 0; i < level; i++)
                //{
                //    if (firstBox.ImageIndex == ((MyPictureBox)pictureBoxes[i]).Index)
                //    {
                //        count_points = count_points - 1;
                //    }

                //}
                if (box1.ImageIndex != box1.Index && box2.ImageIndex != box2.Index)
                {
                    count_points -= 1;
                }
                //count_points = count_points - 1;

                if (count_points < 0)
                {
                    count_points = 0;
                }

               // points.Text = "Количество очков: " + count_points.ToString();


                if (isFinished())
                {
                    //Останавливаем таймер
                    timer1.Stop();
                    MessageBox.Show("Well done!");
                    setPointsToDB();
                    //ShowImage();
                }
            }
        }
        private bool isFinished()//createFrag
        {
            for (int i = 0; i < level; i++)
            {
                if (((MyPictureBox)pictureBoxes[i]).ImageIndex == ((MyPictureBox)pictureBoxes[i]).Index)
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
            for (int i = 0; i < level; i++)
            {
                if (((MyPictureBox)pictureBoxes[i]).ImageIndex != ((MyPictureBox)pictureBoxes[i]).Index)
                    return false;
            }
            return true;
        }
        //сохранение очков при завершении
        private async void setPointsToDB()
        {
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();

            SqlCommand command = new SqlCommand("UPDATE  [Game] SET time=@time, points=@points, unfinished=@matrix where id_game=@id_game", sqlConnection);
            command.Parameters.AddWithValue("@id_game", id_game);
            command.Parameters.AddWithValue("@points", count_points);
            command.Parameters.AddWithValue("@time", DateTime.Now - date);
            //  command.Parameters.AddWithValue("@matrix", DBNull.Value);
            //  command.Parameters["@matrix"].Value = System.Data.SqlTypes.SqlBinary.Null;
            command.Parameters.Add("@matrix", System.Data.SqlDbType.VarBinary).Value = DBNull.Value;
            await command.ExecuteNonQueryAsync();
        }
        private void CreateBitmapImage(Image image, Image[] images, int index, int numRows, int numCols, int unitX, int unitY)
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

       
    }
}
