using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle
{
    class User
    {
        private string login;
        public User()
        {

        }
        public User(string login)
        {
            this.login = login;
        }
        public string Login
        {
            get { return login; }
            set { login = value; }
        }
    }
}
