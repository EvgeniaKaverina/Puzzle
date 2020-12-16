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
    public partial class UserMenu : Form
    {
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
            ContinueGame continueGame = new ContinueGame(login);
            continueGame.Show();
            this.Close();
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
