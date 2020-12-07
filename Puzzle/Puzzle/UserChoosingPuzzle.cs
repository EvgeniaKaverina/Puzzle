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
    public partial class UserChoosingPuzzle : CreatePuzzle
    {
        private GalleryForCreate gal;
        //string connectionString = "Data Source=localhost;Initial Catalog=Puzzle;Integrated Security=True";
        //private SqlConnection sqlConnection;

        public UserChoosingPuzzle()
        {
            InitializeComponent();
        }

        private void back_Click(object sender, EventArgs e)
        {
            UserMenu u = new UserMenu();
            u.Show();
            this.Hide();
        }

        private void select_img_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Выберите номер уровня");
            }
            else {
                int number = Int32.Parse(comboBox1.SelectedItem.ToString());
                gal = new GalleryForCreate(this,number);
                gal.Show();
            }
        }

        private void play_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Выберите номер уровня");
            }
            else
            {

                Game g = new Game();
                g.Show();
                this.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
