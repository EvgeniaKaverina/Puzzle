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
            gal= new GalleryForCreate(this);
           gal.Show();
            
        }
        public void setTextToButton()
        {
            select_pict.Text = gal.getpicture_name();
        }
        private void back_Click(object sender, EventArgs e)
        {
            AdminMenu s = new AdminMenu();
            s.Show();
            this.Hide();
        }

       // private async void create_rect(string pict, int num_rows, int num_cols, int id)
       // {
       //     Bitmap bm = new Bitmap(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\gallery\", pict));

       //     int wid = (int)600 / num_cols;
       //     int hgt = (int)420 / num_rows;
 

       //     // Начнем расщепление растрового изображения.

       //     Bitmap piece = new Bitmap(wid, hgt);
       //     Rectangle dest_rect = new Rectangle(0, 0, wid, hgt);
       //     using (Graphics gr = Graphics.FromImage(piece))
       //     {
       //         sqlConnection = new SqlConnection(connectionString);
       //         await sqlConnection.OpenAsync();
       //         Rectangle source_rect = new Rectangle(0, 0, wid, hgt);
       //         for (int row = 0; row < num_rows; row++)
       //         {
       //             source_rect.X = 0;
       //             for (int col = 0; col < num_cols; col++)
       //             {
       //                 // Скопируем фрагмент изображения.
       //                 gr.DrawImage(bm, dest_rect, source_rect,
       //                     GraphicsUnit.Pixel);

       //                 // Сохраним кусок.
                  
       //                 string filename = pict +id+
       //                     row.ToString("00") +
       //                     col.ToString("00") + ".jpg";
       //                 //      matrix[row, col] = filename;

       //                 //запись в бд
                       
       //                 SqlCommand command = new SqlCommand("INSERT INTO [Fragment] (name_fragment, id_puzzle) VALUES(@frag,@id)",sqlConnection);
       //                 command.Parameters.AddWithValue("frag", filename);
       //                 command.Parameters.AddWithValue("id", id);

       //                 await command.ExecuteNonQueryAsync();
                       

       //                 piece.Save(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\fragm\", filename), ImageFormat.Jpeg);
                        

       //                 // Переместимся к следующему столбцу.
       //                 source_rect.X += wid;
       //             }
       //             source_rect.Y += hgt;
       //         }

       //         sqlConnection.Close();
       //     }
       ////     return matrix; // вернуть матрицу

       // }
       // private async void create_trian(string pict, int num_rows, int num_cols, int id)
       // {
       //     Bitmap bm = new Bitmap(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\gallery\", pict));

       //     int wid = (int)600 / num_cols;
       //     int hgt = (int)420 / num_rows;


       //     // Начнем расщепление растрового изображения.

       //     Bitmap piece = new Bitmap(wid, hgt);
       //     Rectangle dest_rect = new Rectangle(0, 0, wid, hgt);
       //     using (Graphics gr = Graphics.FromImage(piece))
       //     {
       //         sqlConnection = new SqlConnection(connectionString);
       //         await sqlConnection.OpenAsync();
                
       //         Rectangle source_rect = new Rectangle(0, 0, wid, hgt);
       //         for (int row = 0; row < num_rows; row++)
       //         {
       //             source_rect.X = 0;
       //             for (int col = 0; col < num_cols; col++)
       //             {
       //                 // Скопируем фрагмент изображения.
       //                 gr.DrawImage(bm, dest_rect, source_rect,
       //                     GraphicsUnit.Pixel);

       //                 for(int j = 0; j < hgt; j++)
       //                 {
       //                     for(int i = 0; i < wid/hgt*j; i++)
       //                     {
       //                         piece.SetPixel(i, j, Color.Transparent);
       //                     }
       //                 }
                       
                       
       //                 // Сохраним кусок.
                        

       //                 string filename = pict + id +row.ToString("00") + col.ToString("00")+"01" + ".jpg";
              
       //                 SqlCommand command = new SqlCommand("INSERT INTO [Fragment] (name_fragment, id_puzzle) VALUES(@frag,@id)", sqlConnection);
       //                 command.Parameters.AddWithValue("frag", filename);
       //                 command.Parameters.AddWithValue("id", id);

       //                 await command.ExecuteNonQueryAsync();


       //                 piece.Save(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\fragm\", filename), ImageFormat.Jpeg);

       //                 for (int j = 0; j < hgt; j++)
       //                 {
       //                     for (int i = wid / hgt * j; i < wid ; i++)
       //                     {
       //                         piece.SetPixel(i, j, Color.Transparent);
       //                     }
       //                 }


       //                 // Сохраним кусок.


       //                 string filename2 = pict + id + row.ToString("00") + col.ToString("00") + "02" + ".jpg";

       //                command = new SqlCommand("INSERT INTO [Fragment] (name_fragment, id_puzzle) VALUES(@frag,@id)", sqlConnection);
       //                 command.Parameters.AddWithValue("frag", filename2);
       //                 command.Parameters.AddWithValue("id", id);

       //                 await command.ExecuteNonQueryAsync();


       //                 piece.Save(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\fragm\", filename2), ImageFormat.Jpeg);


       //                 // Переместимся к следующему столбцу.
       //                 source_rect.X += wid;
       //             }
       //             source_rect.Y += hgt;
       //         }

       //         sqlConnection.Close();
       //     }

       // }
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
            else if (select_pict.Text == "Выбрать картинку")
            {
                MessageBox.Show("Выберите картинку");
            }
            else
            {
                string pict = gal.getpicture_name();


                //запрос к бд все сохранить

                int number = Int32.Parse(comboBox1.SelectedItem.ToString());
                //  string loc=comboBox2.SelectedItem.ToString();
                int loc=0;
                if (comboBox2.SelectedItem.ToString() == "Лента")
                {
                    loc = 1;
                }
                else if (comboBox2.SelectedItem.ToString() == "Поле")
                {
                    loc = 0;
                }
                sqlConnection = new SqlConnection(connectionString);
                await sqlConnection.OpenAsync();

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
                    //await command.ExecuteNonQueryAsync();
                    AdminMenu menu = new AdminMenu();
                    menu.Show();
                    this.Close();
                }
            }
        }
        public Boolean isPuzzleExists()
        {
            string pict = gal.getpicture_name();
            int number = Int32.Parse(comboBox1.SelectedItem.ToString());

            SqlCommand command = new SqlCommand("SELECT * FROM [Puzzles] WHERE image=@image AND number_level=@number_level", sqlConnection);
            command.Parameters.AddWithValue("image", pict);
            command.Parameters.AddWithValue("number_level", number );


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
    }
}
