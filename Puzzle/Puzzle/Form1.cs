using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzle
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection;

        public Form1()
        {
            InitializeComponent();
        }

        private async void enter_Click(object sender, EventArgs e)
        {
            string log = login_enter.Text;
            string pas = password_enter.Text;

            string connectionString = "Data Source=localhost;Initial Catalog=Puzzle;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();

            if (isUserExistsEnter())
            {
                UserMenu s = new UserMenu();
                s.Show();
                this.Hide();

            }
            
            //Gallery g = new Gallery();
            //g.Show();
        }

        public Boolean isUserExistsEnter()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM [User] WHERE login=@login AND password=@password", sqlConnection);
            command.Parameters.AddWithValue("login", login_enter.Text);
            command.Parameters.AddWithValue("password", password_enter.Text);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {

                return true;
            }
            else 
            {
                MessageBox.Show("Пользователь с таким логином и паролем не существует", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        private async void registration_Click(object sender, EventArgs e)
        {
            string log=login_reg.Text;
            string pas=password_reg.Text;
     

            if (log.Length < 4 || log.Length > 12)
            {
                MessageBox.Show("Логин должен содержать от 4 до 12 символов.","Ошибка регистрации", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }


            if (pas.Length < 5 || pas.Length > 10)
            {
                MessageBox.Show("Пароль должен содержать от 5 до 10 символов.", "Ошибка регистрации", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                string connectionString = "Data Source=localhost;Initial Catalog=Puzzle;Integrated Security=True";
                sqlConnection = new SqlConnection(connectionString);
                await sqlConnection.OpenAsync();


                SqlCommand sqlCommand = new SqlCommand("INSERT INTO [User] (login, password) VALUES(@login, @password)", sqlConnection);
                sqlCommand.Parameters.AddWithValue("login", login_reg.Text);
                sqlCommand.Parameters.AddWithValue("password", password_reg.Text);

                if (isUserExists())
                {
                    return;
                }

               else  if (sqlCommand.ExecuteNonQuery() == 1)
                {
                    UserMenu s = new UserMenu();
                    s.Show();
                    this.Hide();
                }
            }
 
        }
        public Boolean isUserExists()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM [User] WHERE login=@login", sqlConnection);
            command.Parameters.AddWithValue("login", login_reg.Text);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Такой логин уже существует. Придумайте новый.", "Ошибка регистрации", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
                return false;
        }

        private void button_admin_Click(object sender, EventArgs e)
        {
            string pas = "admin";

            if (password_admin.Text == pas)
            {
                AdminMenu s = new AdminMenu();
                s.Show();

                this.Hide();
            }
            else
            {
                password_admin.Text = "";
                MessageBox.Show("Пароль неверный. Попробуйте снова.", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
