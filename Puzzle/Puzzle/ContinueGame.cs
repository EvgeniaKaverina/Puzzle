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
using System.Collections;
using System.Data.SqlClient;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;

namespace Puzzle
{
    public partial class ContinueGame : Form
    {
        string connectionString = "Data Source=localhost;Initial Catalog=Puzzle;Integrated Security=True";
        private SqlConnection sqlConnection;
        
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
        int help_counter;
       // int number_level;
        bool location;

        MyPictureBox firstBox = null;
        MyPictureBox secondBox = null;
        MyPictureBox taleBox = null;
        //int numCols;
        //int numRows;
        //bool type;
        DateTime date;
        DateTime dateNow;
        Level numberLevel;
        Puzzle puzzle;
        User user;

        int count_points;
        public ContinueGame()
        {
            InitializeComponent();
        }
        public ContinueGame(string login)
        {
            InitializeComponent();
            user = new User();
            puzzle = new Puzzle();
            numberLevel = new Level();
            user.Login = login;
            dateNow = DateTime.Now;
            this.FormClosed += new FormClosedEventHandler(Form_Closed);

        }
        protected void Form_Closed(object sender, EventArgs e)
        { Application.Exit(); }
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
            command.Parameters.AddWithValue("login", user.Login);
            sqlConnection.Open();
            //  await command.ExecuteNonQueryAsync();

            byte[] array = (byte[])command.ExecuteScalar();
           
                stream = new MemoryStream(array, 0, array.Length);
                int[] newMatrix = (int[])formatter.Deserialize(stream);

                command = new SqlCommand("SELECT TOP 1 * FROM [Game] where login=@login and unfinished is not null Order by id_game DESC", sqlConnection);
                command.Parameters.AddWithValue("login", user.Login);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    id_game = Int32.Parse(Convert.ToString(reader["id_game"]));
                    id_puzzle = Int32.Parse(Convert.ToString(reader["id_puzzle"]));
                    count_points = Int32.Parse(Convert.ToString(reader["points"]));
                    date = DateTime.Parse(Convert.ToString(reader["time"]));
                    help_counter = Int32.Parse(Convert.ToString(reader["prompting"]));
                    // location = Boolean.Parse(Convert.ToString(reader["location"]));
                }
                reader.Close();

                command = new SqlCommand("SELECT * FROM [Puzzles] where id_puzzle=@id_puzzle", sqlConnection);
                command.Parameters.AddWithValue("id_puzzle", id_puzzle);


            
                SqlDataReader datareader = null;
                datareader = command.ExecuteReader();


                while (datareader.Read())
                {
                    numberLevel.Number = Int32.Parse(Convert.ToString(datareader["number_level"]));
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

            int unitX = groupBox1.Width / numberLevel.NumCols;
            int unitY = groupBox1.Height / numberLevel.NumRows;
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

                CreateBitmapImage(image, images, i, numberLevel.NumRows, numberLevel.NumCols, unitX, unitY);
                pictureBoxes[i].Location = new Point(unitX * (i % numberLevel.NumCols), unitY * (i / numberLevel.NumCols));
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

            int unitX = groupBox1.Width / numberLevel.NumCols;
            int unitY = groupBox1.Height / numberLevel.NumRows;
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

                CreateBitmapImage(image, images, i, numberLevel.NumRows, numberLevel.NumCols, unitX, unitY);
                pictureBoxOnTale[i].Location = new Point(unitX * (i % numberLevel.NumCols), unitY * (i / numberLevel.NumCols));
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
            if (IsGameExists())
            {
                if (numberLevel.Type)
                {
                    int[] matr = new int[level];
                    for (int i = 0; i < level; i++)
                    {
                        matr[i] = ((MyPictureBox)pictureBoxes[i]).ImageIndex;
                    }
                    date +=  DateTime.Now-dateNow;
                    MemoryStream stream = new MemoryStream();
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, matr);

                    sqlConnection = new SqlConnection(connectionString);
                    await sqlConnection.OpenAsync();

                    SqlCommand command = new SqlCommand("UPDATE  [Game] SET time=@time, points=@points, unfinished=@matrix, prompting=@help where id_game=@id_game", sqlConnection);
                    command.Parameters.AddWithValue("@id_game", id_game);
                    command.Parameters.AddWithValue("@points", count_points);
                    command.Parameters.AddWithValue("@time", date);
                    command.Parameters.AddWithValue("@help", help_counter);

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
                    date += DateTime.Now - dateNow;
                    MemoryStream stream = new MemoryStream();
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, matr);

                    sqlConnection = new SqlConnection(connectionString);
                    await sqlConnection.OpenAsync();

                    SqlCommand command = new SqlCommand("UPDATE  [Game] SET time=@time, points=@points, unfinished=@matrix, prompting=@help where id_game=@id_game", sqlConnection);
                    command.Parameters.AddWithValue("@id_game", id_game);
                    command.Parameters.AddWithValue("@points", count_points);
                    command.Parameters.AddWithValue("@time", date);
                    command.Parameters.AddWithValue("@help", help_counter);
                    command.Parameters.Add("@matrix", System.Data.SqlDbType.VarBinary);
                    command.Parameters["@matrix"].Value = stream.ToArray();


                    await command.ExecuteNonQueryAsync();
                }
            }
            //else
            //{
            //    if (numberLevel.Type)
            //    {
            //        int[] matr = new int[level];
            //        for (int i = 0; i < level; i++)
            //        {
            //            matr[i] = ((MyPictureBox)pictureBoxes[i]).ImageIndex;
            //        }
            //        MemoryStream stream = new MemoryStream();
            //        BinaryFormatter formatter = new BinaryFormatter();
            //        formatter.Serialize(stream, matr);

            //        sqlConnection = new SqlConnection(connectionString);
            //        await sqlConnection.OpenAsync();
            //        SqlCommand command = new SqlCommand("INSERT INTO  [Game]  (id_puzzle,login,time,points, unfinished,prompting)  VALUES(@id_puzzle,@user,@time, @points,@matrix,@help) ", sqlConnection);
            //        command.Parameters.AddWithValue("@id_puzzle", puzzle.ID);
            //        command.Parameters.AddWithValue("@user", user.Login);
            //        command.Parameters.AddWithValue("@id_game", id_game);
            //        command.Parameters.AddWithValue("@points", count_points);

            //        command.Parameters.AddWithValue("@time", DateTime.Now - date);
            //        command.Parameters.AddWithValue("@help", help_counter);

            //        command.Parameters.Add("@matrix", System.Data.SqlDbType.VarBinary);
            //        command.Parameters["@matrix"].Value = stream.ToArray();

            //        await command.ExecuteNonQueryAsync();
            //    }
            //    //треугольный на поле
            //    //треугольный на ленте
            //    else
            //    {
            //        int[] matr = new int[level * 2];
            //        for (int i = 0; i < level * 2; i += 2)
            //        {
            //            matr[i] = ((MyPictureBox)pictureBoxesTriangle[i / 2][0]).ImageIndex;
            //            matr[i + 1] = ((MyPictureBox)pictureBoxesTriangle[i / 2][1]).ImageIndex;
            //        }
            //        MemoryStream stream = new MemoryStream();
            //        BinaryFormatter formatter = new BinaryFormatter();
            //        formatter.Serialize(stream, matr);

            //        sqlConnection = new SqlConnection(connectionString);
            //        await sqlConnection.OpenAsync();

            //        SqlCommand command = new SqlCommand("INSERT INTO  [Game]  (id_puzzle,login,time,points, unfinished,prompting)  VALUES(@id_puzzle,@user,@time, @points,@matrix,@help) ", sqlConnection);
            //        command.Parameters.AddWithValue("@id_puzzle", puzzle.ID);
            //        command.Parameters.AddWithValue("@user", user.Login);
            //        command.Parameters.AddWithValue("@id_game", id_game);
            //        command.Parameters.AddWithValue("@points", count_points);

            //        command.Parameters.AddWithValue("@time", DateTime.Now - date);
            //        command.Parameters.AddWithValue("@help", help_counter);
            //        command.Parameters.Add("@matrix", System.Data.SqlDbType.VarBinary);
            //        command.Parameters["@matrix"].Value = stream.ToArray();


            //        await command.ExecuteNonQueryAsync();
            //    }
            //}
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

            int unitX = groupBox1.Width / numberLevel.NumCols;
            int unitY = groupBox1.Height / numberLevel.NumRows;
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

                CreateBitmapImage(image, images, i, numberLevel.NumRows, numberLevel.NumCols, unitX, unitY);
                pictureBoxes[i].Location = new Point(unitX * (i % numberLevel.NumCols), unitY * (i / numberLevel.NumCols));
                if (!groupBox1.Controls.Contains(pictureBoxes[i]))
                    groupBox1.Controls.Add(pictureBoxes[i]);
            }
            //  shuffle(ref indice);
            List<int> taleValues = (List<int>)matrix.ToList();
            for (int i = 0; i < level; i++)
            {
              //  taleValues[i] = -1;
                if (matrix[i] >= 0)
                {
                    pictureBoxes[i].Image = images[matrix[i]];
                    pictureBoxes[i].Click -= new EventHandler(OnClick);
                    pictureBoxes[i].Click += new EventHandler(OnPuzzleClick);
                    //    flowLayoutPanel1.Controls.Remove(pictureBoxOnTale[i]);
                }
                 ((MyPictureBox)pictureBoxes[i]).ImageIndex = matrix[i];
                taleValues[i] = matrix[i];
            }
            for (int i = 0; i < level; i++)
            {
                if (taleValues.IndexOf(i) == -1)
                {
                    pictureBoxOnTale[i].Image = images[i];
                    ((MyPictureBox)pictureBoxOnTale[i]).ImageIndex = i;
                   
                }
                else
                {
                    flowLayoutPanel1.Controls.Remove(pictureBoxOnTale[i]);
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
                ((MyPictureBox)sender).Click += new EventHandler(OnPuzzleClick);
                firstBox = null;
                taleBox = null;
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
                    setPointsToDB();
                    //   MessageBox.Show("Поздравляем, вы выиграли!\n" + "Количество набранных очков: " + count_points.ToString(),"Игра окончена",);
                    DialogResult dialogResult = MessageBox.Show("Поздравляем, вы выиграли!\n" + "Количество набранных очков: " + count_points.ToString(), "Игра окончена", MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    if (dialogResult == DialogResult.OK)
                    {

                        UserMenu userMenu = new UserMenu(user.Login);
                        userMenu.Show();
                        this.Hide();
                    }



                }
            }
        }
        private void getLevelSettings()
        {

            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM [Level] where number=@num", sqlConnection);
            command.Parameters.AddWithValue("num", numberLevel.Number);

            SqlDataReader reader = null;
            reader = command.ExecuteReader();


            while (reader.Read())
            {

                numberLevel.NumCols = Int32.Parse(Convert.ToString(reader["count_of_piece_horizontally"]));
                numberLevel.NumRows = Int32.Parse(Convert.ToString(reader["count_of_piece_vertically"]));
                numberLevel.Type = Boolean.Parse(Convert.ToString(reader["type_of_piece"]));
            }
            level = numberLevel.NumRows * numberLevel.NumCols;

        }

        private void ContinueGame_Load(object sender, EventArgs e)
        {

            groupBox1.Size = new System.Drawing.Size(600, 420);
            int[] matr = Continue();
            if (numberLevel.Type)
            {
                
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
            }
            else
            {
                int[][] matrix = new int[matr.Length/2][];
                for(int i = 0; i < matrix.Length; i++)
                {
                    matrix[i] = new int[2];
                    matrix[i][0] = matr[2*i];
                    matrix[i][1] = matr[2*i + 1];
                }
                numberLevel.NumCols /= 2;
                level /= 2;
                ShowImage();
                if (!location)
                {
                    createFragTriangle(matrix);
                }
                else
                {
                    createFragTaleTriangle();
                    createTriangleFragmentsOnField(matrix,matr);
                    
                }
            }
            help_lab.Text = "Количество подсказок: " + help_counter.ToString();
            //myTimer.Tick += new EventHandler(timer1_Tick);

            //// Sets the timer interval to 5 seconds.
            //myTimer.Interval = 5000;
            //myTimer.Start();
            timer1.Interval = 10;
            timer1.Tick += new EventHandler(timer1_Tick);

            ////Делаем таймер доступным
            //timer1.Enabled = true;
            //Запускаем таймер
            timer1.Start();

        }
       
        //static Timer myTimer = new Timer();
        //static int alarmCounter = 1;
        //static bool exitFlag = false;
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
            //не работает
            long tick = DateTime.Now.Ticks - dateNow.Ticks+date.Ticks ;

            //for (int delay = 0; delay < 1000; delay++) { tick++; }
            
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
               
                if (box1.ImageIndex != box1.Index && box2.ImageIndex != box2.Index)
                {
                    count_points -= 1;
                }
              

                if (count_points < 0)
                {
                    count_points = 0;
                }

           

                if (isFinished())
                {
                    //Останавливаем таймер
                    timer1.Stop();
                   
                    setPointsToDB();
                    DialogResult dialogResult = MessageBox.Show("Поздравляем, вы выиграли!\n" + "Количество набранных очков: " + count_points.ToString(), "Игра окончена", MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    if (dialogResult == DialogResult.OK)
                    {

                        UserMenu userMenu = new UserMenu(user.Login);
                        userMenu.Show();
                        this.Hide();
                    }
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
            if (!IsGameExists())
            {
                sqlConnection = new SqlConnection(connectionString);
                await sqlConnection.OpenAsync();
                date += DateTime.Now - dateNow;
                SqlCommand command = new SqlCommand("INSERT INTO  [Game]  (id_puzzle,login,time,points, unfinished,prompting)  VALUES(@id_puzzle,@user,@time, @points,@matrix,@help)", sqlConnection);
                command.Parameters.AddWithValue("@id_puzzle", puzzle.ID);
                command.Parameters.AddWithValue("@user", user.Login);
                command.Parameters.AddWithValue("@id_game", id_game);
                command.Parameters.AddWithValue("@points", count_points);
                command.Parameters.AddWithValue("@time",  date);
                command.Parameters.AddWithValue("@help", DBNull.Value);
                command.Parameters.Add("@matrix", System.Data.SqlDbType.VarBinary).Value = DBNull.Value;
                await command.ExecuteNonQueryAsync();

                command = new SqlCommand("SELECT TOP 1* FROM [Game] ORDER BY id_game DESC ", sqlConnection);

                SqlDataReader reader = null;
                reader = command.ExecuteReader();


                while (reader.Read())
                {

                    id_game = Int32.Parse(Convert.ToString(reader["id_game"]));

                }
            }
            else
            {
                sqlConnection = new SqlConnection(connectionString);
                await sqlConnection.OpenAsync();

                SqlCommand command = new SqlCommand("UPDATE  [Game] SET time=@time, points=@points, unfinished=@matrix,prompting=@help where id_game=@id_game", sqlConnection);
                command.Parameters.AddWithValue("@id_game", id_game);
                command.Parameters.AddWithValue("@points", count_points);
                command.Parameters.AddWithValue("@time", DateTime.Now - date);
                command.Parameters.AddWithValue("@help", DBNull.Value);

                command.Parameters.Add("@matrix", System.Data.SqlDbType.VarBinary).Value = DBNull.Value;
                await command.ExecuteNonQueryAsync();
            }
        }
        private bool IsGameExists()
        {
            if (id_game == -1)
            {
                return false;
            }
            else return true;
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
        private void exit_Click_1(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(" Хотите сохранить текущий результат?", "Пазл не сохранен!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            if (dialogResult == DialogResult.Yes)
            {
                SavePuzzzle();

            }
            //else
            //{
            //    deleteGame();
            //}
            UserMenu userMenu = new UserMenu(user.Login);
            userMenu.Show();
            this.Hide();
        }
        //удаление игры
        private async void deleteGame()
        {
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();

            SqlCommand command = new SqlCommand("DELETE FROM  [Game]  where id_game=@id_game", sqlConnection);
            command.Parameters.AddWithValue("@id_game", id_game);

            await command.ExecuteNonQueryAsync();
        }

        //подсказка
        private void help_Click(object sender, EventArgs e)
        {
            if (help_counter > 0)
            {
                //лента прямоугольные
                if (location && numberLevel.Type)
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
                                help_lab.Text = "Количество подсказок: " + help_counter.ToString();
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
                                help_lab.Text = "Количество подсказок: " + help_counter.ToString();
                                return;
                            }
                        }
                    }
                    else MessageBox.Show("Выберите фрагмент");

                }
                //поле прямоугольные
                else if (!location && numberLevel.Type)
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
                                help_lab.Text = "Количество подсказок: " + help_counter.ToString();
                                return;
                            }
                        }

                    }
                    else MessageBox.Show("Выберите фрагмент");

                }
                //лента треугольные
                else if (puzzle.Location && !numberLevel.Type)
                {
                    if (taleBox != null)
                    {
                        for (int i = 0; i < level; i++)
                        {

                            if (((TriangularPictureBox)taleBox).LeftGrag)
                            {
                                if (taleBox.ImageIndex == Math.Abs(((MyPictureBox)pictureBoxesTriangle[i][1]).Index))
                                {
                                    if (((MyPictureBox)pictureBoxesTriangle[i][1]).ImageIndex == -150)

                                    {
                                        ((MyPictureBox)pictureBoxesTriangle[i][1]).ImageIndex = taleBox.ImageIndex;
                                        pictureBoxesTriangle[i][1].Image = imagesTriangle[taleBox.ImageIndex][1];

                                        flowLayoutPanel1.Controls.Remove(taleBox);
                                        pictureBoxesTriangle[i][1].Click += new EventHandler(OnPuzzleClick);
                                    }
                                    else SwitchImageTriangle(taleBox, (MyPictureBox)pictureBoxesTriangle[i][1], 1);
                                    isFinishedTriangle();
                                    help_counter--;
                                    help_lab.Text = "Количество подсказок: " + help_counter.ToString();

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
                                        pictureBoxesTriangle[i][0].Image = imagesTriangle[taleBox.ImageIndex][0];

                                        flowLayoutPanel1.Controls.Remove(taleBox);
                                        pictureBoxesTriangle[i][0].Click += new EventHandler(OnPuzzleClick);
                                    }
                                    else SwitchImageTriangle(taleBox, (MyPictureBox)pictureBoxesTriangle[i][0], 0);
                                    isFinishedTriangle();
                                    help_counter--;
                                    help_lab.Text = "Количество подсказок: " + help_counter.ToString();
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
                                    help_lab.Text = "Количество подсказок: " + help_counter.ToString();
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
                                    help_lab.Text = "Количество подсказок: " + help_counter.ToString();
                                    return;
                                }
                            }

                        }

                    }
                    else MessageBox.Show("Выберите фрагмент");
                }
                //поле треугольные
                else if (!puzzle.Location && !numberLevel.Type)
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
                                    help_lab.Text = "Количество подсказок: " + help_counter.ToString();
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
                                    help_lab.Text = "Количество подсказок: " + help_counter.ToString();
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
        private void createFragTriangle(int[][]matrix)
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

            int unitX = groupBox1.Width / numberLevel.NumCols;
            int unitY = groupBox1.Height / numberLevel.NumRows;
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



                CreateBitmapImageTriangle(image, imagesTriangle, i, 0, numberLevel.NumRows, numberLevel.NumCols, unitX, unitY);
                CreateBitmapImageTriangle(image, imagesTriangle, i, 1, numberLevel.NumRows, numberLevel.NumCols, unitX, unitY);
                pictureBoxesTriangle[i][0].Location = new Point(unitX * (i % numberLevel.NumCols), unitY * (i / numberLevel.NumCols));
                pictureBoxesTriangle[i][1].Location = new Point(unitX * (i % numberLevel.NumCols), unitY * (i / numberLevel.NumCols));
                if (!groupBox1.Controls.Contains(pictureBoxesTriangle[i][0]))
                    groupBox1.Controls.Add(pictureBoxesTriangle[i][0]);
                if (!groupBox1.Controls.Contains(pictureBoxesTriangle[i][1]))
                    groupBox1.Controls.Add(pictureBoxesTriangle[i][1]);
            }
           
            for (int i = 0; i < level; i++)
            {

                pictureBoxesTriangle[i][0].Image = imagesTriangle[matrix[i][0]][0];
                ((MyPictureBox)pictureBoxesTriangle[i][0]).ImageIndex = matrix[i][0];

            }
          
            for (int i = 0; i < level; i++)
            {

                pictureBoxesTriangle[i][1].Image = imagesTriangle[matrix[i][1]][1];
                ((MyPictureBox)pictureBoxesTriangle[i][1]).ImageIndex = matrix[i][1];
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

                if (Math.Abs(box1.ImageIndex) != Math.Abs(box1.Index) && Math.Abs(box2.ImageIndex) != Math.Abs(box2.Index))
                {
                    count_points -= 1;
                }
                //count_points = count_points - 1;

                if (count_points < 0)
                {
                    count_points = 0;
                }
                //points.Text = "Количество очков: " + count_points.ToString();
                if (isFinishedTriangle())
                {
                    timer1.Stop();
                    //  MessageBox.Show("Поздравляем, вы выиграли!\n" + "Количество набранных очков: " + count_points.ToString());
                    setPointsToDB();
                    DialogResult dialogResult = MessageBox.Show("Поздравляем, вы выиграли!\n" + "Количество набранных очков: " + count_points.ToString(), "Игра окончена", MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    if (dialogResult == DialogResult.OK)
                    {

                        UserMenu userMenu = new UserMenu(user.Login);
                        userMenu.Show();
                        this.Hide();
                    }
                }
            }
        }
        private bool isFinishedTriangle()
        {
            for (int i = 0; i < level; i++)
            {
                if (Math.Abs(((MyPictureBox)pictureBoxesTriangle[i][0]).ImageIndex) == Math.Abs(((MyPictureBox)pictureBoxesTriangle[i][0]).Index))
                {
                    //if (pictureBoxesTriangle[i][0].Enabled)
                    //{
                    //    Bitmap btm = (Bitmap)pictureBoxesTriangle[i][0].Image;

                    //}
                    pictureBoxesTriangle[i][0].Enabled = false;

                }
                if (Math.Abs(((MyPictureBox)pictureBoxesTriangle[i][1]).ImageIndex) == Math.Abs(((MyPictureBox)pictureBoxesTriangle[i][1]).Index))
                {
                    //if (pictureBoxesTriangle[i][1].Enabled)
                    //{
                    //    Bitmap btm = (Bitmap)pictureBoxesTriangle[i][1].Image;

                    //}
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

        private void createTriangleFragmentsOnField(int[][] matrix,int[] matr)
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
            int unitX = groupBox1.Width / numberLevel.NumCols;
            int unitY = groupBox1.Height / numberLevel.NumRows;
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

                pictureBoxesTriangle[i][0].Location = new Point(unitX * (i % numberLevel.NumCols), unitY * (i / numberLevel.NumCols));
                pictureBoxesTriangle[i][1].Location = new Point(unitX * (i % numberLevel.NumCols), unitY * (i / numberLevel.NumCols));
                if (!groupBox1.Controls.Contains(pictureBoxesTriangle[i][0]))
                    groupBox1.Controls.Add(pictureBoxesTriangle[i][0]);
                if (!groupBox1.Controls.Contains(pictureBoxesTriangle[i][1]))
                    groupBox1.Controls.Add(pictureBoxesTriangle[i][1]);
            }

            //for (int i = 0; i < level; i++)
            //{

            //    ((MyPictureBox)pictureBoxesTriangle[i][0]).ImageIndex = -150;
            //    ((MyPictureBox)pictureBoxesTriangle[i][1]).ImageIndex = -150;
            //}
            List<int> taleValuesLeft = new List<int>();
            List<int> taleValuesRight = new List<int>();
            for (int i = 0; i < level; i++)
            {
             
                if (matrix[i][0] >= 0)
                {
                    pictureBoxesTriangle[i][0].Image = imagesTriangle[matrix[i][0]][0];
                    pictureBoxesTriangle[i][0].Click -= new EventHandler(OnClickTriangle);
                    pictureBoxesTriangle[i][0].Click += new EventHandler(OnPuzzleClickTriangle);
                    //    flowLayoutPanel1.Controls.Remove(pictureBoxOnTale[i]);
                }
                ((MyPictureBox)pictureBoxesTriangle[i][0]).ImageIndex = matrix[i][0];
                taleValuesLeft.Add(matrix[i][0]);
                if (matrix[i][1] >= 0)
                {
                    pictureBoxesTriangle[i][1].Image = imagesTriangle[matrix[i][1]][1];
                    pictureBoxesTriangle[i][1].Click -= new EventHandler(OnClickTriangle);
                    pictureBoxesTriangle[i][1].Click += new EventHandler(OnPuzzleClickTriangle);

                }
                 ((MyPictureBox)pictureBoxesTriangle[i][1]).ImageIndex = matrix[i][1];
                taleValuesRight.Add( matrix[i][1]);
            }

            for (int i = 0; i < level; i++)
            {

                if (taleValuesLeft.IndexOf(i) == -1)
                {

                    pictureBoxesTriangleOnTale[i][0].Image = imagesTriangle[i][0];
                    ((MyPictureBox)pictureBoxesTriangleOnTale[i][0]).ImageIndex = i;

                }
                else
                {
                    flowLayoutPanel1.Controls.Remove(pictureBoxesTriangleOnTale[i][0]);
                }
                if (taleValuesRight.IndexOf(i) == -1)
                {

                    pictureBoxesTriangleOnTale[i][1].Image = imagesTriangle[i][1];
                    ((MyPictureBox)pictureBoxesTriangleOnTale[i][1]).ImageIndex = i;

                }
                else
                {
                    flowLayoutPanel1.Controls.Remove(pictureBoxesTriangleOnTale[i][1]);
                }
            }
        }
        private void createFragTaleTriangle()//лента 
        {
            if (pic != null)
            {
                //flowLayoutPanel1.Controls.Remove(pic);
                pic.Dispose();
                pic = null;

            }
            if (pictureBoxesTriangleOnTale == null)
            {
                pictureBoxesTriangleOnTale = new PictureBox[level][];

                imagesTriangle = new Image[level][];
            }

            int unitX = groupBox1.Width / numberLevel.NumCols;
            int unitY = groupBox1.Height / numberLevel.NumRows;
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

                CreateBitmapImageTriangleOnTale(image, imagesTriangle, i, 0, numberLevel.NumRows, numberLevel.NumCols, unitX, unitY);
                CreateBitmapImageTriangleOnTale(image, imagesTriangle, i, 1, numberLevel.NumRows, numberLevel.NumCols, unitX, unitY);
                pictureBoxesTriangleOnTale[i][0].Location = new Point(unitX * (i % numberLevel.NumCols), unitY * (i / numberLevel.NumCols));
                pictureBoxesTriangleOnTale[i][1].Location = new Point(unitX * (i % numberLevel.NumCols), unitY * (i / numberLevel.NumCols));
                if (!flowLayoutPanel1.Controls.Contains(pictureBoxesTriangleOnTale[i][0]))
                    flowLayoutPanel1.Controls.Add(pictureBoxesTriangleOnTale[i][0]);
                if (!flowLayoutPanel1.Controls.Contains(pictureBoxesTriangleOnTale[i][1]))
                    flowLayoutPanel1.Controls.Add(pictureBoxesTriangleOnTale[i][1]);
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
                    setPointsToDB();
                    //    MessageBox.Show("Поздравляем, вы выиграли!\n" + "Количество набранных очков: " + count_points.ToString());
                    DialogResult dialogResult = MessageBox.Show("Поздравляем, вы выиграли!\n" + "Количество набранных очков: " + count_points.ToString(), "Игра окончена", MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    if (dialogResult == DialogResult.OK)
                    {

                        UserMenu userMenu = new UserMenu(user.Login);
                        userMenu.Show();
                        this.Hide();
                    }
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void info_Click(object sender, EventArgs e)
        {
            Process.Start(Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), @"..\html\index.html"));
        }
    }
}
