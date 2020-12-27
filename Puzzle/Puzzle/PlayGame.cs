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
using System.Diagnostics;

namespace Puzzle
{
    public partial class PlayGame : Form
    {
        string connectionString = "Data Source=localhost;Initial Catalog=Puzzle;Integrated Security=True";
        private SqlConnection sqlConnection;
        DateTime date; 

        public PlayGame()
        {
            InitializeComponent();
        }

        public PlayGame(string picture_name, int number, string user)
        {
            InitializeComponent();
            numberLevel = new Level();
            this.user = new User();
            puzzle = new Puzzle();
     
            puzzle.Image = picture_name;
            puzzle.Level = number;
            numberLevel.Number = number;
            this.user.Login = user;
            id_game = -1;
            this.FormClosed += new FormClosedEventHandler(Form_Closed);
        }
        /* Событие для закрытия приложения
        */
        protected void Form_Closed(object sender, EventArgs e)
        { Application.Exit(); }

        Image image;
        PictureBox pic = null;
        //PictureBox для прямоугольных фрагментов на поле
        MyPictureBox[] pictureBoxes = null;
        //PictureBox для прямоугольных фрагментов на ленте
        MyPictureBox[] pictureBoxOnTale = null;
        //массив прямоугольных фрагментов
        Image[] images = null;


        //PictureBox для треугольных фрагментов на поле
        PictureBox[][] pictureBoxesTriangle = null;
        //PictureBox для треугольных фрагментов на ленте
        PictureBox[][] pictureBoxesTriangleOnTale = null;
        //массив треугольных фрагментов
        Image[][] imagesTriangle = null;

        int id_game;
        int countofFragments;
  
        //Вспомогательные pictureBox для выделения картинок
        MyPictureBox firstBox = null;
        MyPictureBox secondBox = null;
        MyPictureBox taleBox = null;


         Level numberLevel;
        Puzzle puzzle;
        User user;

        int count_points;//количество очков

        /*создание прямоугольных фрагментов на поле*/
        private void createFrag()
        {
     
            if (pic != null)
            {
                groupBox1.Controls.Remove(pic);
                pic.Dispose();
                pic = null;

            }
            if (pictureBoxes == null)
            {
                pictureBoxes = new MyPictureBox[countofFragments];
                images = new Image[countofFragments];
            }
            int unitX = groupBox1.Width / numberLevel.NumCols;
            int unitY = groupBox1.Height / numberLevel.NumRows;
            int[] indice = new int[countofFragments];
            for(int i = 0; i < countofFragments; i++)
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
                //присваивание индекса pictureBox
                ((MyPictureBox)pictureBoxes[i]).Index = i;

                CreateBitmapImage(image, images,i, numberLevel.NumRows, numberLevel.NumCols, unitX, unitY);
                pictureBoxes[i].Location = new Point(unitX * (i % numberLevel.NumCols), unitY * (i / numberLevel.NumCols));
                if (!groupBox1.Controls.Contains(pictureBoxes[i]))
                    groupBox1.Controls.Add(pictureBoxes[i]);
            }
            //перемешивание фрагментов
            shuffle(ref indice);
            for(int i = 0; i < countofFragments; i++)
            {
                //присваивание картинок и индексов картинок фрагментам
                pictureBoxes[i].Image = images[indice[i]];
                ((MyPictureBox)pictureBoxes[i]).ImageIndex = indice[i];
            }

        }
        /*создание прямоугольных фрагментов на поле при игре на ленте*/
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
                pictureBoxes = new MyPictureBox[countofFragments];
             
            }
           
            int unitX = groupBox1.Width / numberLevel.NumCols;
            int unitY = groupBox1.Height / numberLevel.NumRows;
            int[] indice = new int[countofFragments];
            for (int i = 0; i < countofFragments; i++)
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

              
                pictureBoxes[i].Location = new Point(unitX * (i % numberLevel.NumCols), unitY * (i / numberLevel.NumCols));
                if (!groupBox1.Controls.Contains(pictureBoxes[i]))
                    groupBox1.Controls.Add(pictureBoxes[i]);
            }
            for (int i = 0; i < countofFragments; i++)
            {
              
                ((MyPictureBox)pictureBoxes[i]).ImageIndex = -1;
            }

        }
        /*создание прямоугольных фрагментов на ленте*/
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
                pictureBoxOnTale = new MyPictureBox[countofFragments];
                images = new Image[countofFragments];
            }

            int unitX = groupBox1.Width / numberLevel.NumCols;
            int unitY = groupBox1.Height / numberLevel.NumRows;
            flowLayoutPanel1.Height = unitY + 50;
            int[] indice = new int[countofFragments];
            for (int i = 0; i < countofFragments; i++)
            {
                indice[i] = i;
                if (pictureBoxOnTale[i] == null)
                {
                    pictureBoxOnTale[i] = new MyPictureBox();
                    pictureBoxOnTale[i].Click += new EventHandler(OnPuzzleClickTale);
                    pictureBoxOnTale[i].BorderStyle = BorderStyle.Fixed3D;
                   
                }
                pictureBoxOnTale[i].Width = unitX;
                pictureBoxOnTale[i].Height = unitY;

                ((MyPictureBox)pictureBoxOnTale[i]).Index = -1;

                CreateBitmapImage(image, images, i, numberLevel.NumRows, numberLevel.NumCols, unitX, unitY);
                pictureBoxOnTale[i].Location = new Point(unitX * (i % numberLevel.NumCols), unitY * (i / numberLevel.NumCols));
                if (!flowLayoutPanel1.Controls.Contains(pictureBoxOnTale[i]))
                    flowLayoutPanel1.Controls.Add(pictureBoxOnTale[i]);
            }
            shuffle(ref indice);
            for (int i = 0; i < countofFragments; i++)
            {
                pictureBoxOnTale[i].Image = images[indice[i]];
                ((MyPictureBox)pictureBoxOnTale[i]).ImageIndex = indice[i];
            }

        }
        /*Событие при выборе фрагмента на ленте*/
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
        //Перетаскивание фрагмента с ленты на поле
        private void SwitchFieldAndTale(MyPictureBox tale, MyPictureBox box)
        {
            if (box.ImageIndex == -1)
            {
                box.Image = images[tale.ImageIndex];
                box.ImageIndex = tale.ImageIndex;
                flowLayoutPanel1.Controls.Remove(tale);
                //Проверка на окончание игры
                if (isFinished())
                {
                    //Останавливаем таймер
                    timer1.Stop();
                    setPointsToDB();
                
                    DialogResult dialogResult = MessageBox.Show("Поздравляем, вы выиграли!\n" +"Количество набранных очков: " + count_points.ToString(), "Игра окончена", MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    if (dialogResult == DialogResult.OK)
                    {

                        UserMenu userMenu = new UserMenu(user.Login);
                        userMenu.Show();
                        this.Hide();
                    }
                   
                   

                }
            }
        }
        //Событие при выборе фрагмента на поле
        private void OnClick(object sender, EventArgs e)
        {
            
            firstBox = (MyPictureBox)sender;
            firstBox.BorderStyle = BorderStyle.FixedSingle;
            if (taleBox != null)
            {
                SwitchFieldAndTale(taleBox, firstBox);
         
                firstBox.Click -= new EventHandler(OnClick);
                ((MyPictureBox)sender).Click +=new  EventHandler(OnPuzzleClick);
                firstBox = null;
                taleBox = null;
            }
        }
        //Событие при выборе фрагмента на поле
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
        }

        /*Обмен картинок на поле*/
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
                               
                if (box1.ImageIndex != box1.Index && box2.ImageIndex != box2.Index)
                {
                    count_points -= 1;
                }
               

                if (count_points<0)
                {
                    count_points = 0;
                }
          //Проверка на завершение игры
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
                }
            }
        }
        //Метод проверки завершения игры
        private bool isFinished()
        {
            for(int i = 0; i < countofFragments; i++)
            {
                if(((MyPictureBox)pictureBoxes[i]).ImageIndex== ((MyPictureBox)pictureBoxes[i]).Index)
                {
                    if (pictureBoxes[i].Enabled)
                    {
                        Bitmap btm = (Bitmap)pictureBoxes[i].Image;

                    }
                    pictureBoxes[i].Enabled = false;
 

                }
            }
            //Проверка на совпадение индекса picturebox и индекса картинки
            for(int i = 0; i < countofFragments; i++)
            {
                if (((MyPictureBox)pictureBoxes[i]).ImageIndex != ((MyPictureBox)pictureBoxes[i]).Index)
                    return false;
            }
            return true;
        }
        //Отрисовка фрагмента
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
        //Перемешивание фрагментов
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
       
        //сохранение очков при завершении
        private async void setPointsToDB()
        {
            if (!IsGameExists())
            {
                try
                {
                    sqlConnection = new SqlConnection(connectionString);
                    await sqlConnection.OpenAsync();

                    SqlCommand command = new SqlCommand("INSERT INTO  [Game]  (id_puzzle,login,time,points, unfinished,prompting)  VALUES(@id_puzzle,@user,@time, @points,@matrix,@help)", sqlConnection);
                    command.Parameters.AddWithValue("@id_puzzle", puzzle.ID);
                    command.Parameters.AddWithValue("@user", user.Login);
                    command.Parameters.AddWithValue("@id_game", id_game);
                    command.Parameters.AddWithValue("@points", count_points);
                    command.Parameters.AddWithValue("@time", DateTime.Now - date);
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
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка подключения к базе данных.");
                }
            }
            else
            {
                try {
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
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка подключения к базе данных.");
                }
            }
        }
       //Провека на существование игры 
        private bool IsGameExists()
        {
            if (id_game == -1)
            {
                return false;
            }
            else return true;
        }
        private void Game_Load(object sender, EventArgs e)
        {
            //размер groupBox
            groupBox1.Size = new System.Drawing.Size(600, 420);
            CreateBitmapImage();
            ShowImage();
            getLevelSettings();

            getPuzzle();
  
            if (puzzle.Location && numberLevel.Type)
            {
                createFragmentsOnField();
                createFragTale();
            }else if (!puzzle.Location && numberLevel.Type)
            {
                createFrag();
                flowLayoutPanel1.Visible = false;
            }
            else if (puzzle.Location && !numberLevel.Type)
            {
                numberLevel.NumCols = numberLevel.NumCols / 2;
                countofFragments = numberLevel.NumRows * numberLevel.NumCols;
                createTriangleFragmentsOnField();
                createFragTaleTriangle();
            }
            else if (!puzzle.Location && !numberLevel.Type)
            {
                numberLevel.NumCols = numberLevel.NumCols / 2;
                countofFragments = numberLevel.NumRows * numberLevel.NumCols;
                createFragTriangle();
                flowLayoutPanel1.Visible = false;
            }
            //Таймер
            date = DateTime.Now;
            Timer timer = new Timer();
            timer1.Interval = 10;
            timer1.Tick += new EventHandler(timer1_Tick);

           
            //Запускаем таймер
            timer1.Start();

            //Присваивание количество подсказок
            help_counter = 3;
            help_lab.Text = "Количество подсказок: " + help_counter.ToString();

            //Очки
            count_points = 20 * numberLevel.Number;
         

        }
        //загрузка картинки из БД
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
        //Обработчик события таймер
        private void timer1_Tick(object sender, EventArgs e)
        {
            long tick = DateTime.Now.Ticks - date.Ticks;
            DateTime stopWatch = new DateTime();

            stopWatch = stopWatch.AddTicks(tick);
            time.Text = String.Format("{0:mm:ss}", stopWatch);
            
            
        }

        private void view_pic_Click(object sender, EventArgs e)
        {
           
            //Открытие выбранной пользователем картинки
            Picture picture = new Picture(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"..\..\gallery\"+ puzzle.Image));
            picture.ShowDialog();
          
        }
        //Создание картинки
        private Bitmap CreateBitmapImage()
        {

            Image img = Image.FromFile(@"..\..\gallery\"+ puzzle.Image);

            Bitmap objBmImage = new Bitmap(groupBox1.Width, groupBox1.Height);
         
            Graphics objGraphics = Graphics.FromImage(objBmImage);
            objGraphics.Clear(Color.White);
            objGraphics.DrawImage(img, new Rectangle(0, 0, groupBox1.Width, groupBox1.Height));
       
            objGraphics.Flush();

            return objBmImage;           
        }
        //Запрос в БД для получения информации об Уровне сложности
        private void getLevelSettings()
        {
            try
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
                countofFragments = numberLevel.NumRows * numberLevel.NumCols;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения к базе данных.");
            }
        }
        //Запрос в БД для получения информации о пазле
        private void getPuzzle()
        {
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM [Puzzles] where number_level=@num and image=@pic_name", sqlConnection);
                command.Parameters.AddWithValue("num", numberLevel.Number);
                command.Parameters.AddWithValue("pic_name", puzzle.Image);

                SqlDataReader reader = null;
                reader = command.ExecuteReader();


                while (reader.Read())
                {

                    puzzle.ID = Int32.Parse(Convert.ToString(reader["id_puzzle"]));
                    puzzle.Location = Boolean.Parse(Convert.ToString(reader["location"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения к базе данных.");
            }

        }

        //Сохранение незаконченного пазла для возможности продложения
        private async void SavePuzzzle()
        {
            try
            {
                //прямоугольный на поле
                //прямоугольный на ленте
                if (IsGameExists())
                {
                    if (numberLevel.Type)
                    {
                        int[] matr = new int[countofFragments];
                        for (int i = 0; i < countofFragments; i++)
                        {
                            matr[i] = ((MyPictureBox)pictureBoxes[i]).ImageIndex;
                        }
                        MemoryStream stream = new MemoryStream();
                        BinaryFormatter formatter = new BinaryFormatter();
                        formatter.Serialize(stream, matr);

                        sqlConnection = new SqlConnection(connectionString);
                        await sqlConnection.OpenAsync();

                        SqlCommand command = new SqlCommand("UPDATE  [Game] SET time=@time, points=@points, unfinished=@matrix, prompting=@help where id_game=@id_game", sqlConnection);
                        command.Parameters.AddWithValue("@id_game", id_game);
                        command.Parameters.AddWithValue("@points", count_points);
                        command.Parameters.AddWithValue("@time", DateTime.Now - date);
                        command.Parameters.AddWithValue("@help", help_counter);

                        command.Parameters.Add("@matrix", System.Data.SqlDbType.VarBinary);
                        command.Parameters["@matrix"].Value = stream.ToArray();

                        await command.ExecuteNonQueryAsync();
                    }
                    //треугольный на поле
                    //треугольный на ленте
                    else
                    {
                        int[] matr = new int[countofFragments * 2];
                        for (int i = 0; i < countofFragments * 2; i += 2)
                        {
                            matr[i] = ((MyPictureBox)pictureBoxesTriangle[i / 2][0]).ImageIndex;
                            matr[i + 1] = ((MyPictureBox)pictureBoxesTriangle[i / 2][1]).ImageIndex;
                        }
                        MemoryStream stream = new MemoryStream();
                        BinaryFormatter formatter = new BinaryFormatter();
                        formatter.Serialize(stream, matr);

                        sqlConnection = new SqlConnection(connectionString);
                        await sqlConnection.OpenAsync();

                        SqlCommand command = new SqlCommand("UPDATE  [Game] SET time=@time, points=@points, unfinished=@matrix, prompting=@help where id_game=@id_game", sqlConnection);
                        command.Parameters.AddWithValue("@id_game", id_game);
                        command.Parameters.AddWithValue("@points", count_points);
                        command.Parameters.AddWithValue("@time", DateTime.Now - date);
                        command.Parameters.AddWithValue("@help", help_counter);
                        command.Parameters.Add("@matrix", System.Data.SqlDbType.VarBinary);
                        command.Parameters["@matrix"].Value = stream.ToArray();


                        await command.ExecuteNonQueryAsync();
                    }
                }
                else
                {
                    if (numberLevel.Type)
                    {
                        int[] matr = new int[countofFragments];
                        for (int i = 0; i < countofFragments; i++)
                        {
                            matr[i] = ((MyPictureBox)pictureBoxes[i]).ImageIndex;
                        }
                        MemoryStream stream = new MemoryStream();
                        BinaryFormatter formatter = new BinaryFormatter();
                        formatter.Serialize(stream, matr);

                        sqlConnection = new SqlConnection(connectionString);
                        await sqlConnection.OpenAsync();
                        SqlCommand command = new SqlCommand("INSERT INTO  [Game]  (id_puzzle,login,time,points, unfinished,prompting)  VALUES(@id_puzzle,@user,@time, @points,@matrix,@help)", sqlConnection);
                        command.Parameters.AddWithValue("@id_puzzle", puzzle.ID);
                        command.Parameters.AddWithValue("@user", user.Login);
                        command.Parameters.AddWithValue("@id_game", id_game);
                        command.Parameters.AddWithValue("@points", count_points);

                        command.Parameters.AddWithValue("@time", DateTime.Now - date);
                        command.Parameters.AddWithValue("@help", help_counter);

                        command.Parameters.Add("@matrix", System.Data.SqlDbType.VarBinary);
                        command.Parameters["@matrix"].Value = stream.ToArray();

                        await command.ExecuteNonQueryAsync();
                    }
                    //треугольный на поле
                    //треугольный на ленте
                    else
                    {
                        int[] matr = new int[countofFragments * 2];
                        for (int i = 0; i < countofFragments * 2; i += 2)
                        {
                            matr[i] = ((MyPictureBox)pictureBoxesTriangle[i / 2][0]).ImageIndex;
                            matr[i + 1] = ((MyPictureBox)pictureBoxesTriangle[i / 2][1]).ImageIndex;
                        }
                        MemoryStream stream = new MemoryStream();
                        BinaryFormatter formatter = new BinaryFormatter();
                        formatter.Serialize(stream, matr);

                        sqlConnection = new SqlConnection(connectionString);
                        await sqlConnection.OpenAsync();

                        SqlCommand command = new SqlCommand("INSERT INTO  [Game]  (id_puzzle,login,time,points, unfinished,prompting)  VALUES(@id_puzzle,@user,@time, @points,@matrix,@help) ", sqlConnection);
                        command.Parameters.AddWithValue("@id_puzzle", puzzle.ID);
                        command.Parameters.AddWithValue("@user", user.Login);
                        command.Parameters.AddWithValue("@id_game", id_game);
                        command.Parameters.AddWithValue("@points", count_points);

                        command.Parameters.AddWithValue("@time", DateTime.Now - date);
                        command.Parameters.AddWithValue("@help", help_counter);
                      
                        command.Parameters.Add("@matrix", System.Data.SqlDbType.VarBinary);
                        command.Parameters["@matrix"].Value = stream.ToArray();


                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения к базе данных.");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SavePuzzzle();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        //Создание треугольных фрагментов
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
                pictureBoxesTriangle = new PictureBox[countofFragments][];
                imagesTriangle = new Image[countofFragments][];
            }
           
            int unitX = groupBox1.Width / numberLevel.NumCols;
            int unitY = groupBox1.Height / numberLevel.NumRows;
            int[] indice = new int[countofFragments];
            for (int i = 0; i < countofFragments; i++)
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
            //Перемешивание индексов
            shuffle(ref indice);
            for (int i = 0; i < countofFragments; i++)
            {

                pictureBoxesTriangle[i][0].Image = imagesTriangle[indice[i]][0];
                ((MyPictureBox)pictureBoxesTriangle[i][0]).ImageIndex = indice[i];

            }
            shuffle(ref indice);
            for (int i = 0; i < countofFragments; i++)
            {

                pictureBoxesTriangle[i][1].Image = imagesTriangle[indice[i]][1];
                ((MyPictureBox)pictureBoxesTriangle[i][1]).ImageIndex = indice[i];
            }

        }
        //Обработчик события при нажатии на фрагмент на поле
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

          
        }
        //Своп фрагментов на поле
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
                    //уменьшение количества очков при неправильной перестановки фрагмента
                    count_points -= 1;
                }
            

                if (count_points < 0)
                {
                    count_points = 0;
                }
            
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
        //Проверка на завершение игры
        private bool isFinishedTriangle()
        {
            for (int i = 0; i < countofFragments; i++)
            {
                if (Math.Abs(((MyPictureBox)pictureBoxesTriangle[i][0]).ImageIndex) == Math.Abs(((MyPictureBox)pictureBoxesTriangle[i][0]).Index))
                {
                 
                    pictureBoxesTriangle[i][0].Enabled = false;
                  
                }
                if (Math.Abs(((MyPictureBox)pictureBoxesTriangle[i][1]).ImageIndex) == Math.Abs(((MyPictureBox)pictureBoxesTriangle[i][1]).Index))
                {
                    
                    pictureBoxesTriangle[i][1].Enabled = false;
                  
                }
            }
            //Сравнение индексов фрагментов и индексов картинок
            for (int i = 0; i < countofFragments; i++)
            {
                if (Math.Abs(((MyPictureBox)pictureBoxesTriangle[i][0]).ImageIndex) != Math.Abs(((MyPictureBox)pictureBoxesTriangle[i][0]).Index))
                    return false;

                if (Math.Abs(((MyPictureBox)pictureBoxesTriangle[i][1]).ImageIndex) != Math.Abs(((MyPictureBox)pictureBoxesTriangle[i][1]).Index))
                    return false;
            }
            return true;
        }
        //создание картинок фрагментов для треугольных пазлов
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
        //создание картинок фрагментов для треугольных пазлов
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
        //создание треугольных фрагментов на поле
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
                pictureBoxesTriangle = new PictureBox[countofFragments][];
                //        imagesTriangle = new Image[level][];
            }
            int unitX = groupBox1.Width / numberLevel.NumCols;
            int unitY = groupBox1.Height / numberLevel.NumRows;
            int[] indice = new int[countofFragments];
            for (int i = 0; i < countofFragments; i++)
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

            for (int i = 0; i < countofFragments; i++)
            {

                ((MyPictureBox)pictureBoxesTriangle[i][0]).ImageIndex = -150;
                ((MyPictureBox)pictureBoxesTriangle[i][1]).ImageIndex = -150;
            }

        }
        //Создание треугольных фрагментов на ленте
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
                pictureBoxesTriangleOnTale = new PictureBox[countofFragments][];

                imagesTriangle = new Image[countofFragments][];
            }

            int unitX = groupBox1.Width / numberLevel.NumCols;
            int unitY = groupBox1.Height / numberLevel.NumRows;
            flowLayoutPanel1.Height = unitY + 50;
            int[] indice = new int[countofFragments];
            for (int i = 0; i < countofFragments; i++)
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
            //перемешивание фрагментов
            shuffle(ref indice);
            for (int i = 0; i < countofFragments; i++)
            {
                pictureBoxesTriangleOnTale[i][0].Image = imagesTriangle[indice[i]][0];
                ((MyPictureBox)pictureBoxesTriangleOnTale[i][0]).ImageIndex = indice[i];
            }
            shuffle(ref indice);
            for (int i = 0; i < countofFragments; i++)
            {
                pictureBoxesTriangleOnTale[i][1].Image = imagesTriangle[indice[i]][1];
                ((MyPictureBox)pictureBoxesTriangleOnTale[i][1]).ImageIndex = indice[i];
            }

        }
        //Перемещение треугольного фрагмента с ленты на поле
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
        //Обработчик события при выборе фрагмента на поле
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
        //обработчик события при нажатии на кнопку ВЗЯТЬ ПОДСКАЗКУ
        private void help_Click(object sender, EventArgs e)
        {
            if (help_counter > 0)
            {
                //лента прямоугольные
                if (puzzle.Location && numberLevel.Type)
                {
                    if (taleBox != null)
                    {
                        for (int i = 0; i < countofFragments; i++)
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
                        for (int i = 0; i < countofFragments; i++)
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
                else if (!puzzle.Location && numberLevel.Type)
                {
                    if (firstBox != null)
                    {
                        for (int i = 0; i < countofFragments; i++)
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
                        for (int i = 0; i < countofFragments; i++)
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
                        for (int i = 0; i < countofFragments; i++)
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
                        for (int i = 0; i < countofFragments; i++)
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
        //обработчик события при нажатии на кнопку ВЫЙТИ
        private void exit_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(" Хотите сохранить текущий результат?", "Пазл не сохранен!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            if (dialogResult == DialogResult.Yes)
            {
                SavePuzzzle();
               
            }
            //возвращение к меню
           
            UserMenu userMenu = new UserMenu(user.Login);
            userMenu.Show();
            this.Hide();
        }

        private void time_Click(object sender, EventArgs e)
        {

        }
        //отображение справочной информации
        private void info_Click(object sender, EventArgs e)
        {
            Process.Start(Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), @"..\html\index.html"));
        }

        //Отображение формы рейтинга 
        private void rating_Click(object sender, EventArgs e)
        {
            Rating rating = new Rating();
            rating.Show();
        }
    }
}
