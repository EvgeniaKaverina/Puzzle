﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzle
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection;

        public Form1()
        {
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(Form_Closed);
        }
        /* событие для закрытия формы
         */
        protected void Form_Closed(object sender, EventArgs e)
        { Application.Exit(); }
        /* обработчик события нажатия на кнопку ВХОД
                */
        private void enter_Click(object sender, EventArgs e)
        {
            string log = login_enter.Text;
            string pas = password_enter.Text;

            string connectionString = "Data Source=localhost;Initial Catalog=Puzzle;Integrated Security=True";
              sqlConnection = new SqlConnection(connectionString);
            try
            {
                //откывается соединение к БД
                sqlConnection.Open();
                if (isUserExistsEnter())
                {
                    UserMenu s = new UserMenu(log);
                    s.Show();
                    this.Hide();

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка подключения к базе данных.");
            }
           
        }
        /* Проверка на существование пользователя
         */ 
        public Boolean isUserExistsEnter()
        {
            SqlCommand command = null;
            try
            {
                //запрос в БД
                command = new SqlCommand("SELECT * FROM [User] WHERE login=@login AND password=@password", sqlConnection);
                command.Parameters.AddWithValue("login", login_enter.Text);
                command.Parameters.AddWithValue("password", password_enter.Text);
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();

                adapter.SelectCommand = command;
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                {

                    return true;
                }
                else
                {
                    MessageBox.Show("Введен неверный логин или пароль. Попробуйте ввести данные снова!", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
           
        }
        /*Обработчик события нажатия на кнопку РЕГИСТРАЦИЯ 
         
         */
        private async void registration_Click(object sender, EventArgs e)
        {
            string log=login_reg.Text;
            string pas=password_reg.Text;
     
            //проверка длины логина
            if (log.Length < 4 || log.Length > 12)
            {
                MessageBox.Show("Логин должен содержать от 4 до 12 символов.","Ошибка регистрации", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            //проверка длины пароля
            else if (pas.Length < 5 || pas.Length > 10)
            {
                MessageBox.Show("Пароль должен содержать от 5 до 10 символов.", "Ошибка регистрации", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //проверка логина
            else if (!char.IsLetter(log[0]))
            {
                MessageBox.Show("Логин не должен начинаться с цифры", "Ошибка регистрации", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                SqlCommand sqlCommand=null;
                try
                {
                    //открытие соединения к БД
                    string connectionString = "Data Source=localhost;Initial Catalog=Puzzle;Integrated Security=True";
                    sqlConnection = new SqlConnection(connectionString);
                    await sqlConnection.OpenAsync();

                    //запрос в БД на запись пользователя
                    sqlCommand = new SqlCommand("INSERT INTO [User] (login, password) VALUES(@login, @password)", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("login", log);
                    sqlCommand.Parameters.AddWithValue("password", pas);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Ошибка подключения к базе данных.");
                }
                if (isUserExists())
                {
                    return;
                }

               else  if (sqlCommand.ExecuteNonQuery() == 1)
                {
                    //переход к меню пользователя
                    UserMenu s = new UserMenu(log);
                    s.Show();
                    this.Hide();
                }
            }
        }
        /*Проверка на существование пользователя в БД
         * true- такой пользователь существует
         * false- такого пользователя нет в БД
         */
        public Boolean isUserExists()
        {
            SqlCommand command = null;
            try
            {
                command = new SqlCommand("SELECT * FROM [User] WHERE login=@login", sqlConnection);
                command.Parameters.AddWithValue("login", login_reg.Text);

               
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Такой логин уже существует. Придумайте новый.", "Ошибка регистрации", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
                return false;
        }
        /*Обработчик события нажатия на кнопку ВОЙТИ КАК АДМИНИСТРАТОР 
         */
        private void button_admin_Click(object sender, EventArgs e)
        {
            string pas = "admin";
            //проверка пароля
            if (password_admin.Text == pas)
            {
                AdminMenu s = new AdminMenu();
                s.Show();

                this.Hide();
            }
            else
            {
                password_admin.Text = "";
                MessageBox.Show("Пароль неверный. Попробуйте снова.", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void login_enter_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
