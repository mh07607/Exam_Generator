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

namespace Khidmat_Project
{
    public partial class Form8 : Form
    {
        const string connectionString = @"Data Source =DESKTOP-PEGIUMG; Inital Catalog = khidmat_test1; Integrated Security = False; user id =Admin;password=Blaze30083";
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand command = new SqlCommand();
        Dictionary<string, int> subjectName_Id = new Dictionary<string, int>();
        public Form8()
        {
            InitializeComponent();
        }
        
        private void Form8_Load(object sender, EventArgs e)
        {
            List<string> subjectList = getSubjects();
            comboBox1.DataSource = subjectList;
        }

        private List<string> getSubjects()
        {
            List<string> subjectList = new List<string>();

            connection.Open();
            string query = "select * from Subject";
            command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int subjectId = Convert.ToInt32(reader["SubjectId"]);
                string subjectName = reader["SubjectName"].ToString();

                subjectName_Id[subjectName] = subjectId;

                subjectList.Add(subjectName);
            }

            reader.Close();
            command.Dispose();
            connection.Close();
            return subjectList;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form9 form9 = new Form9();
            form9.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e) //Edit Button 
        {
            Form10 form10 = new Form10();
            form10.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e) //Filter Button
        {

        }

        private void button1_Click(object sender, EventArgs e) //Delete Button
        {
            if (dataGridView1.SelectedCells.Count == 0)
            {
                MessageBox.Show("Please select a topic first.");
            }
            else
            {
                DataGridViewCell selectedCell = dataGridView1.SelectedCells[0];
                int selectedRowIndex = selectedCell.RowIndex;
                int bookId = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells[0].Value);

                //Deleting Book_Topic
                connection.Open();
                string query = "DELETE FROM Book_Topic WHERE BookId = @BookId";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TopicId", bookId);
                command.ExecuteNonQuery();
                connection.Close();

                //Deleting Book
                connection.Open();
                string query2 = "DELETE FROM Book WHERE BookId = @BookId";
                command = new SqlCommand(query2, connection);
                command.Parameters.AddWithValue("@BookId", bookId);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Book Sucessfuly Deleted");
            }
        }
    }

}
