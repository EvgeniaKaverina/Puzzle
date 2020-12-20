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
    public partial class Info : Form
    {
        public Info()
        {
            InitializeComponent();
        }

        private void Info_Load(object sender, EventArgs e)
        {
            label1.Text= "Автоматизированную систему «Игра «Мир пазлов» разработали студентки группы 6415-020302D " +
                "Самарского университета Испухалеева Алсу Насепкалиевна и " +
                "Каверина Евгения Александровна, кафедра Программных систем.";
        }
    }
}
