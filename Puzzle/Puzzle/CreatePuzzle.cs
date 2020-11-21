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

namespace Puzzle
{
    public partial class CreatePuzzle : Form
    {
        string connectionString = "Data Source=localhost;Initial Catalog=Puzzle;Integrated Security=True";
        private SqlConnection sqlConnection;

        private GalleryForCreate gal = new GalleryForCreate();

        public CreatePuzzle()
        {
            InitializeComponent();
        }

        private void select_pict_Click(object sender, EventArgs e)
        {
           GalleryForCreate gal= new GalleryForCreate();
           gal.Show();
        }

        private void back_Click(object sender, EventArgs e)
        {
            AdminMenu s = new AdminMenu();
            s.Show();
            this.Hide();
        }

        private async void create_puzzle_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Выберите номер уровня");
            }
            else if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Выберите расположение фрагментов");
            }

            else
            {
                string pict = gal.getpicture_name();
                //sqlConnection = new SqlConnection(connectionString);
                //await sqlConnection.OpenAsync();



                //SqlCommand command = new SqlCommand("INSERT INTO [Puzzles] (image, number_level, location) VALUES(@image, @number_level, @location)", sqlConnection);
                ////command.Parameters.AddWithValue("image", comboBox1.Text);
                //command.Parameters.AddWithValue("number_level", comboBox1.SelectedItem);
                //command.Parameters.AddWithValue("location", comboBox2.SelectedText);

                //await command.ExecuteNonQueryAsync();

                AdminMenu a = new AdminMenu();
                a.Show();
                this.Hide();
            }
        }

    }
}
