﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Puzzle
{
    public partial class UserMenu : Form
    {
        string connectionString = "Data Source=localhost;Initial Catalog=Puzzle;Integrated Security=True";
        private SqlConnection sqlConnection;
        public UserMenu()
        {
            InitializeComponent();
        }

        string login;
        public UserMenu(string login)
        {
            InitializeComponent();
            this.login = login;
        }
       //Добавить переходы между формами

        private void buttonNewGame_Click(object sender, EventArgs e)
        {
            UserChoosingPuzzle ch = new UserChoosingPuzzle(login);
            ch.Show();
            this.Close();
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            sqlConnection = new SqlConnection(connectionString);
            //   await sqlConnection.OpenAsync();

            SqlCommand command = new SqlCommand("SELECT TOP 1 unfinished FROM [Game] where login=@login and unfinished is not null Order by id_game DESC", sqlConnection);
            command.Parameters.AddWithValue("login", login);
            sqlConnection.Open();
            //  await command.ExecuteNonQueryAsync();

            byte[] array = (byte[])command.ExecuteScalar();
            if (array != null)
            {
                ContinueGame continueGame = new ContinueGame(login);
                continueGame.Show();
                this.Close();
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Нет сохраненных игр. Хотите начать новую игру?", "Все игры оконченны", MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                if (dialogResult == DialogResult.OK)
                {
                    UserChoosingPuzzle userChoosingPuzzle = new UserChoosingPuzzle(login);
                    userChoosingPuzzle.Show();
                    this.Close();

                }
               
            }
        }

        private void buttonRating_Click(object sender, EventArgs e)
        {

        }

        private void buttonAboutCreators_Click(object sender, EventArgs e)
        {

        }

        private void buttonAboutGame_Click(object sender, EventArgs e)
        {
            Process.Start(Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), @"..\html\index.html"));
        }

        private void back_user_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }
    }
}
