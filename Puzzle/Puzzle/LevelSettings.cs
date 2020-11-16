using System;
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
        private int level=0;
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
            if (level== 0)
            {
                MessageBox.Show("Выберите номер уровня");
               
            }
            else if(comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Выберите вид фрагментов");
            }
            else
            {
                string connectionString = "Data Source=localhost;Initial Catalog=Puzzle;Integrated Security=True";
                sqlConnection = new SqlConnection(connectionString);
                await sqlConnection.OpenAsync();
               
                SqlCommand sqlCommand = new SqlCommand("SELECT number FROM Level WHERE number=@num",sqlConnection);
                sqlCommand.Parameters.AddWithValue("num", level);

                SqlDataReader reader = null;
                reader = await sqlCommand.ExecuteReaderAsync();
                string n="";
                while(await reader.ReadAsync())
                {
                    n = reader["number"].ToString();
                }
                if (n != "")
                {
                    reader.Close();
                    DialogResult dialogResult= MessageBox.Show("Этот уровень существует. Хотите его изменить?", "Уровень существует!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    if (dialogResult== DialogResult.Yes)
                    {
                        SqlCommand command = new SqlCommand("UPDATE [Level] set count_of_piece_horizontally=@count_horizon, count_of_piece_vertically=@count_vertical, type_of_piece=@type where number=@number", sqlConnection);
                        command.Parameters.AddWithValue("number", level);
                        command.Parameters.AddWithValue("count_horizon", numericUpDown2.Value);
                        command.Parameters.AddWithValue("count_vertical", numericUpDown1.Value);
                        command.Parameters.AddWithValue("type", comboBox1.SelectedItem.ToString());
                        await command.ExecuteNonQueryAsync();
                    }
                }
                else
                {
                    reader.Close();
                    SqlCommand command = new SqlCommand("INSERT INTO [Level] (number, count_of_piece_horizontally,count_of_piece_vertically, type_of_piece) VALUES(@number, @count_horizon,@count_vertical, @type)", sqlConnection);
                    command.Parameters.AddWithValue("number", level);
                    command.Parameters.AddWithValue("count_horizon", numericUpDown2.Value);
                    command.Parameters.AddWithValue("count_vertical", numericUpDown1.Value);
                    command.Parameters.AddWithValue("type", comboBox1.SelectedItem.ToString());
                    await command.ExecuteNonQueryAsync();
                }
                AdminMenu am = new AdminMenu();
                am.Show();
                Hide();
            }
           
           
          
        }
    }
}
