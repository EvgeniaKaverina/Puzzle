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
        /*Вывод информации о разработчиках*/
        private void Info_Load(object sender, EventArgs e)
        {
            label1.Text= "Самарский Университет";
            label2.Text = "Кафедра программных систем";
            label3.Text = "Курсовой проект по дисциплине  «Программная инженерия»";
            label4.Text = "Тема проекта «Автоматизированная система «Игра «Puzzle» с функциями администратора»";
            label5.Text = "Разработчики: студентки группы 6415-020302D";
            label6.Text = "Каверина Евгения, Испухалеева Алсу";
            label7.Text = "Самара 2020";


        }
    }
}
