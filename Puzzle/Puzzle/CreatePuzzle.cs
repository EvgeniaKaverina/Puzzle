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
    }
}
