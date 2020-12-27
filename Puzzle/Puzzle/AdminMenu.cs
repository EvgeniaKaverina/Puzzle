using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzle
{
    public partial class AdminMenu : Form
    {
        public AdminMenu()
        {
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(Form_Closed);
        }
        /* событие для закрытия формы
       */
        protected void Form_Closed(object sender, EventArgs e)
        { Application.Exit(); }

        private void button1_Click(object sender, EventArgs e)
        {
            //открытие формы настройки уровня сложности
            LevelSettings ls=new LevelSettings();
            ls.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //открытие формы информации о разработчиках
            Info info = new Info();
            info.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //открытие формы галереи
            Gallery g = new Gallery();
            g.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //открытие формы создания пазла
            CreatePuzzle p = new CreatePuzzle();
            p.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //открытие справочной информации
            Process.Start(Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), @"..\html\index.html"));
        }

        private void exit_admin_Click(object sender, EventArgs e)
        {
            //возвращение к форме входа в систему
            Form1 s = new Form1();
            s.Show();
            this.Hide();
        }
    }
}
