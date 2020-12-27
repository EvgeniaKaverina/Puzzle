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
using System.IO;
using System.Diagnostics;

namespace Puzzle
{
    public partial class UserChoosingPuzzle : Form
    {
        private GalleryForCreate gal;
  

        public UserChoosingPuzzle()
        {
            InitializeComponent();
        }
        public UserChoosingPuzzle(string login)
        {
            InitializeComponent();
            this.login = login;
            this.FormClosed += new FormClosedEventHandler(Form_Closed);
        }
        string login;
        int number;
        string picture;
        private void back_Click(object sender, EventArgs e)
        {
            //возвращение к меню пользователя
            UserMenu u = new UserMenu();
            u.Show();
            this.Hide();
        }
        /* Событие для закрытия приложения
         */
        protected void Form_Closed(object sender, EventArgs e)
        { Application.Exit(); }

        /*Обработчик события для выбора картинки из списка доступных*/
        private void select_img_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Выберите номер уровня");
            }
            else {
                number = Int32.Parse(comboBox1.SelectedItem.ToString());
                //открытие формы Галерея
                gal = new GalleryForCreate(this,number);
                gal.Show();
            }
        }
        //отображение имени картинки 
        public void setTextToButton()
        {
            picture= gal.getpicture_name();
            select_img.Text = picture;
        }
        /* Переход к началу игры*/
        private void play_Click(object sender, EventArgs e)
        {
            //проверка на заполнение всех полей
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Выберите номер уровня");
            }
            else if(select_img.Text== "Выбрать картинку")
            {
                MessageBox.Show("Выберите картинку");
            }
            else
            {

                PlayGame g = new PlayGame(picture,number,login);
                g.Show();
                this.Hide();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void info_Click(object sender, EventArgs e)
        {
            //отображение справочной информации
            Process.Start(Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), @"..\html\index.html"));
        }

        private void UserChoosingPuzzle_Load(object sender, EventArgs e)
        {

        }
    }
}
