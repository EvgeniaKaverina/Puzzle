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
    public partial class CreatePuzzle : Form
    {
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

        private void create_puzzle_Click(object sender, EventArgs e)
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
                AdminMenu a = new AdminMenu();
                a.Show();
                this.Hide();
            }
        }

    }
}
