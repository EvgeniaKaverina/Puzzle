using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzle
{
    class MyPictureBox:PictureBox
    {
        int index = 0;//Индекс фрагмента
        int imageIndex = 0;//Индекс картинки на данном фрагменте
        public int Index
        {
            get { return index; }
            set { index = value; }
        }
        public int ImageIndex
        {
            get { return imageIndex; }
            set { imageIndex = value; }
        }
        //Проверка на совпадение индекса фрагмента и индекса картинки
        public bool isMatch()
        {
            return (index == imageIndex);
        }
    }
}
