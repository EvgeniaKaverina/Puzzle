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
    public partial class Rating : Form
    {
        public Rating()
        {
            InitializeComponent();
            LoadData();
            LoadDataTime();
        
        }

        private void LoadData()
        {
            string connectionString = "Data Source=localhost;Initial Catalog=Puzzle;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            sqlConnection.Open();

            SqlCommand command = new SqlCommand(" SELECT TOP 10 login, SUM(points)  FROM [Game] WHERE unfinished IS NULL GROUP BY login ORDER BY SUM(points) DESC", sqlConnection);

            SqlDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[2]);

                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
            }
            reader.Close();
            sqlConnection.Close();

            foreach (string[] s in data)
                dataGridView1.Rows.Add(s);

        }

        private void LoadDataTime()
        {
            string connectionString = "Data Source=localhost;Initial Catalog=Puzzle;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            

            for (int i = 1; i <= 5; i++)
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(" SELECT TOP 3 [Game].login, [Game].time, [Puzzles].number_level  FROM [Game] " +
                    " INNER JOIN [Puzzles] ON [Game].id_puzzle = [Puzzles].id_puzzle WHERE unfinished IS NULL and number_level=@i ORDER BY time", sqlConnection);
                command.Parameters.AddWithValue("i", i);

                // time.Text = String.Format("{0:mm:ss}", stopWatch);
                SqlDataReader reader = command.ExecuteReader();

                List<string[]> data = new List<string[]>();

                while (reader.Read())
                {
                    data.Add(new string[3]);

                    data[data.Count - 1][0] = reader[0].ToString();
                    DateTime time = DateTime.Parse(Convert.ToString(reader[1]));
                    data[data.Count - 1][1] = String.Format("{0:mm:ss}", time);
                    data[data.Count - 1][2] = reader[2].ToString();
                }
                reader.Close();
                sqlConnection.Close();

                foreach (string[] s in data)
                    dataGridView2.Rows.Add(s);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
