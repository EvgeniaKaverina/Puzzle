using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle
{
    class Game
    {
        private long id;
        private long puzzle;
        private string user;
        private DateTime time;
        private int points;
        private int[] unfinished;
        private int prompting;
        public Game() { }
        public Game(long id, long puzzle,string user)
        {
            this.id = id;
            this.puzzle = puzzle;
            this.user = user;
        }
        public long ID
        {
            get { return id; }
            set { id = value; }
        }
        public long Puzzle
        {
            get { return puzzle; }
            set { puzzle = value; }
        }
        public string User
        {
            get { return user; }
            set { user = value; }
        }
        public DateTime Time
        {
            get { return time; }
            set { time = value; }
        }
        public int Points
        {
            get { return points; }
            set { points = value; }
        }
        public int[] Unfinished
        {
            get { return unfinished; }
            set { unfinished = value; }
        }
        public int Prompting
        {
            get { return prompting; }
            set { prompting = value; }
        }
    }
}
