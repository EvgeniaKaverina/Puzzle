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
            gal= new GalleryForCreate();
           gal.Show();
        }

        private void back_Click(object sender, EventArgs e)
        {
            AdminMenu s = new AdminMenu();
            s.Show();
            this.Hide();
        }

        private int[,] create_rect(string pict, int num_rows, int num_cols)
        {
            Bitmap bm = new Bitmap(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\gallery\", pict));

            int wid = (int)600 / num_rows;
            int hgt = (int)420 / num_cols;
            int[,] matrix=new int[num_rows, num_cols]; 

            // Начнем расщепление растрового изображения.
            //string piece_name =
            //    Path.GetFileNameWithoutExtension(txtFile.Text);
        Bitmap piece = new Bitmap(wid, hgt);
            Rectangle dest_rect = new Rectangle(0, 0, wid, hgt);
            using (Graphics gr = Graphics.FromImage(piece))
            {

                Rectangle source_rect = new Rectangle(0, 0, wid, hgt);
                for (int row = 0; row < num_rows; row++)
                {
                    source_rect.X = 0;
                    for (int col = 0; col < num_cols; col++)
                    {
                        // Скопируем фрагмент изображения.
                        gr.DrawImage(bm, dest_rect, source_rect,
                            GraphicsUnit.Pixel);

                        // Сохраним кусок.
                        string filename = pict +
                            row.ToString("00") +
                            col.ToString("00") + ".jpg";
                        piece.Save(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\fragm\", filename), ImageFormat.Jpeg);

                        //matr

                        //matrix = new int[num_rows, num_cols];
                         
                        for (int i = 0; i < num_rows; i++)
                        {
                            for (int j = 0; j < num_cols; j++)
                            {
                                matrix[i, j] = matrix[row, col];
                                
                            }
                           
                        }
                        

                        // Переместимся к следующему столбцу.
                        source_rect.X += wid;
                    }
                    source_rect.Y += hgt;
                }
                
              
            }
            return matrix; // вернуть матрицу

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

                int num_rows = 4;
                int num_cols = 4;

                //вернуть матрицу

                //запрос к бд все сохранить

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
