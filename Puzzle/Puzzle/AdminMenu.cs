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
        protected void Form_Closed(object sender, EventArgs e)
        { Application.Exit(); }
        private void button1_Click(object sender, EventArgs e)
        {
            LevelSettings ls=new LevelSettings();
            ls.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Gallery g = new Gallery();
            g.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CreatePuzzle p = new CreatePuzzle();
            p.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Process.Start(Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), @"..\html\index.html"));
        }

        private void exit_admin_Click(object sender, EventArgs e)
        {
            Form1 s = new Form1();
            s.Show();
            this.Hide();
        }
    }
}
