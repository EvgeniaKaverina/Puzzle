﻿using System;
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
    public partial class UserMenu : Form
    {
        public UserMenu()
        {
            InitializeComponent();
        }
    
       //Добавить переходы между формами

        private void buttonNewGame_Click(object sender, EventArgs e)
        {
            UserChoosingPuzzle ch = new UserChoosingPuzzle();
            ch.Show();
            this.Close();
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {

        }

        private void buttonRating_Click(object sender, EventArgs e)
        {

        }

        private void buttonAboutCreators_Click(object sender, EventArgs e)
        {

        }

        private void buttonAboutGame_Click(object sender, EventArgs e)
        {

        }

        private void back_user_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }
    }
}
