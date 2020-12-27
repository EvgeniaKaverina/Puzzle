using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Puzzle
{
    class TriangularPictureBox : MyPictureBox
    {
        private bool left;//левые фрагмент
        public bool LeftGrag
        {
            set { left = value; }
            get { return left; }
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            using (var p = new GraphicsPath())
            {
                //Отрисовка треугольного фрагмента
                if (left)
                {
                    p.AddPolygon(new Point[] {
                new Point(0, 0),
                new Point(0, Height),
                new Point(Width, 0) });
                }
                else
                {
                    p.AddPolygon(new Point[] {
                new Point(Width, Height),
                new Point(0, Height),
                new Point(Width, 0) });
                }
                this.Region = new Region(p);
                base.OnPaint(pe);
            }
        }
    }
}
