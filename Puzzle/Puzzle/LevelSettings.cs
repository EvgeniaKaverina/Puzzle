﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzle
{
    public partial class LevelSettings : Form
    {
        private int level;
        SqlConnection sqlConnection;
        public LevelSettings()
        {
            InitializeComponent();
        }

        private void LevelSettings_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            level = 1;
         
        }

        private void button2_Click(object sender, EventArgs e)
        {
            level = 2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            level = 3;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            level = 4;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            level = 5;
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            string connectionString= @"Data Source=localhost;Initial Catalog=Puzzle;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();
            SqlCommand command = new SqlCommand("INSERT INTO [Level] (number, count_of_piece_horizontally,count_of_piece_vertically, type_of_piece) VALUES(@number, @count_horizon,@count_vertical, @type)",sqlConnection);
            command.Parameters.AddWithValue("number", level);
            command.Parameters.AddWithValue("count_horizon",numericUpDown2.Value);
            command.Parameters.AddWithValue("count_vertical", numericUpDown1.Value);
            command.Parameters.AddWithValue("type", comboBox1.SelectedItem.ToString());
            await command.ExecuteNonQueryAsync();
            //проверить и вернуться в меню
            //ошибка?
            //редактировать?
        }
    }
}