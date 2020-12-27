using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle
{
    class Level
    {
        private int number;//номер уровня сложности
        private int numRows;//количество фрагментов по вертикали
        private int numCols;//количество фрагментов по горизонтали
        private bool type;//тип фрагментов

        public Level()
        {

        }
        public Level(int number, int numCols,int numRows,bool type)
        {
            this.number = number;
            this.numCols = numCols;
            this.numRows = numRows;
            this.type = type;
        }
        public int Number
        {
            get { return number; }
            set { number = value; }
        }
        public int NumCols
        {
            get { return numCols; }
            set { numCols = value; }
        }
        public int NumRows
        {
            get { return numRows; }
            set { numRows = value; }
        }
        public bool Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
