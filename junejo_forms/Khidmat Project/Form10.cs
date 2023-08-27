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
    public partial class Form10 : Form
    {
        int bookId;
        string bookName;
        const string connectionString = @"Data Source =DESKTOP-PEGIUMG; Inital Catalog = khidmat_test1; Integrated Security = False; user id =Admin;password=Blaze30083";
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand command = new SqlCommand();
        Dictionary<string, int> subjectName_Id = new Dictionary<string, int>();

        public Form10(int bookId, string bookName)
        {
            InitializeComponent();
            this.bookId = bookId;
            this.bookName = bookName;
        }

        public void Form10_Load() 
        {
            textBox1.Text = bookName;

            List<string> subjectList = getSubjects();
            comboBox1.DataSource = subjectList;

            //getting the subject name
            string query = "SELECT Subject.SubjectName " +
                               "FROM Book " +
                               "INNER JOIN Subject ON Book.SubjectId = Subject.SubjectId " +
                               "WHERE Book.BookId = @BookId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@BookId", bookId);
            object result = command.ExecuteScalar();
            comboBox1.Text = result.ToString();
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
            Form8 form8 = new Form8();
            form8.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e) //Edit Button
        {
            string bookName = textBox1.Text.ToString();
            string subjectName = comboBox1.Text.ToString();
            if (bookName != null && subjectName != null)
            {
                string subjectId = subjectName_Id[subjectName].ToString();
                connection.Open();
                string query = "UPDATE Book SET SubjectID = @SubjectId, BookName = @BookName WHERE BookId = @BookId";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SubjectId", subjectId);
                command.Parameters.AddWithValue("@BookName", bookName);
                command.Parameters.AddWithValue("@BookId", this.bookId);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Book Added Sucessfully!");
            }
            else
            {
                MessageBox.Show("Please enter Book and Subject!");
            }

            //Form8 form8 = new Form8();
            //form8.Show();
            //this.Hide();
        }
    }
}
