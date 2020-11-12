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
    public partial class Picture : Form
    {
        public Picture()
        {
            InitializeComponent();
        }
        public Picture(string filename)
        {
            InitializeComponent();
            Image image = Image.FromFile(filename);
            pictureBox1.Image= image.GetThumbnailImage(600, 420,
                                   new Image.GetThumbnailImageAbort(ThumbnailCallback),
                                   IntPtr.Zero);
       //   pictureBox1.Size = new System.Drawing.Size(600, 420);
        }
        public bool ThumbnailCallback()
        {
            return true;
        }
        private void Picture_Load(object sender, EventArgs e)
        {

        }
    }
}
