using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle
{
    class Puzzle
    {
        private long id;//Идентификатор пазла
        private int level;//Номер уровня сложности пазла
        private string image;//название изображения
        private bool location;//место расположения фрагментов: 1-на ленте; 0 - на поле
        public Puzzle() { }
        public Puzzle(int id,int level,string image,bool location)
        {
            this.id = id;
            this.image = image;
            this.level = level;
            this.location = location;
        }
        public long ID
        {
            get { return id; }
            set { id = value; }
        }
        public int Level
        {
            get { return level; }
            set { level = value; }
        }
        public string Image
        {
            get { return image; }
            set { image = value; }
        }
        public bool Location
        {
            get { return location; }
            set { location = value; }
        }
    }
}
