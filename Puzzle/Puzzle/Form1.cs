using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LevelSettings ls = new LevelSettings();
            ls.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string log=textBox3.Text;
            string pas=textBox4.Text;

           

            if (log.Length < 4)
            {
                MessageBox.Show("Введите логин не менее 4 символов.");

            }
            
            if (log.Length > 12)
            {
                MessageBox.Show("Логин содержит слишком много символов.");
            }

            if (pas.Length < 5)
            {
                MessageBox.Show("Придумайте пароль, содержащий не менее 5 символов.");
            }
            if (pas.Length > 10)
            {
                MessageBox.Show("Длина пароля должна быть менее 10 символов.");
            }


            else
            {
                AdminMenu s = new AdminMenu();
                s.Show();

                this.Hide();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string pas = "admin";

            if (textBox5.Text == pas)
            {
                AdminMenu s = new AdminMenu();
                s.Show();

                this.Hide();
            }
            else
            {
                textBox5.Text = "";
                MessageBox.Show("Пароль неверный. Попробуйте снова.");
            }

        }
    }
}
