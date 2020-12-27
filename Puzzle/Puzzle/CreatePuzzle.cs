using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Puzzle
{
    public partial class CreatePuzzle : Form
    {
        string connectionString = "Data Source=localhost;Initial Catalog=Puzzle;Integrated Security=True";
        private SqlConnection sqlConnection;

        private GalleryForCreate gal;

        public CreatePuzzle()
        {
            InitializeComponent();

        }

        private void select_pict_Click(object sender, EventArgs e)
        {
            //открытие формы Галерея для выбора картинки для создания пазла
            gal = new GalleryForCreate(this);
            gal.Show();

        }
        /* Отображения названия картинки на форме
         */
        public void setTextToButton()
        {

            select_pict.Text = gal.getpicture_name();
        }
        private void back_Click(object sender, EventArgs e)
        {
            //возвращение к меню администратора
            AdminMenu s = new AdminMenu();
            s.Show();
            this.Hide();
        }

       
        private async void create_puzzle_Click(object sender, EventArgs e)
        {
            //Проверка на заполнение всех полей
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Выберите номер уровня");
            }
            else if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Выберите расположение фрагментов");
            }
            else if (select_pict.Text == "Выбрать картинку")
            {
                MessageBox.Show("Выберите картинку");
            }
            else
            {
                string pict = gal.getpicture_name();

                int number = Int32.Parse(comboBox1.SelectedItem.ToString());
              
                int loc = 0;
                if (comboBox2.SelectedItem.ToString() == "Лента")
                {
                    loc = 1;
                }
                else if (comboBox2.SelectedItem.ToString() == "Поле")
                {
                    loc = 0;
                }
                try
                {
                    //открытие соединения к БД
                    sqlConnection = new SqlConnection(connectionString);
                    await sqlConnection.OpenAsync();

                    //Добавление нового пазлав БД
                    SqlCommand command = new SqlCommand("INSERT INTO [Puzzles] (image, number_level, location) VALUES(@image, @number_level, @location)", sqlConnection);
                    command.Parameters.AddWithValue("image", pict);
                    command.Parameters.AddWithValue("number_level", number);
                    command.Parameters.AddWithValue("location", loc);
                    //await command.ExecuteNonQueryAsync();

                    if (isPuzzleExists())
                    {
                        return;
                    }
                    else if (command.ExecuteNonQuery() == 1)
                    {
                        //возвращение к меню администратора
                        AdminMenu menu = new AdminMenu();
                        menu.Show();
                        this.Hide();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка подключения к базе данных.");
                }
            }
        }
        /* Проверка на существовании пазла в БД
         */
        public Boolean isPuzzleExists()
        {
            string pict = gal.getpicture_name();
            int number = Int32.Parse(comboBox1.SelectedItem.ToString());


            SqlCommand command = new SqlCommand("SELECT * FROM [Puzzles] WHERE image=@image AND number_level=@number_level", sqlConnection);
            command.Parameters.AddWithValue("image", pict);
            command.Parameters.AddWithValue("number_level", number);


            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Такой пазл уже существует. Выберите другую картинку или уровень.", "Ошибка создания пазла", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
                return false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CreatePuzzle_Load(object sender, EventArgs e)
        {
            this.FormClosed += new FormClosedEventHandler(Form_Closed);
        }

        /* событие для закрытия формы
    */
        protected void Form_Closed(object sender, EventArgs e)
        { Application.Exit(); }
    }
}
