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
    public partial class Form12 : Form
    {
        const string connectionString = @"Data Source =DESKTOP-PEGIUMG; Inital Catalog = khidmat_test1; Integrated Security = False; user id =Admin;password=Blaze30083";
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand command = new SqlCommand();
        Dictionary<string, int> subjectName_Id = new Dictionary<string, int>();
        Dictionary<string, int> BookName_Id = new Dictionary<string, int>();

        public Form12()
        {
            InitializeComponent();
        }

        private void Form12_Load(object sender, EventArgs e)
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
            Form11 form11 = new Form11();
            form11.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string topicName = textBox1.Text.ToString();
            string subjectName = textBox1.Text.ToString();
            if (topicName != null && subjectName != null)
            {
                string subjectId = subjectName_Id[subjectName].ToString();
                connection.Open();
                string query = "exex InsertTopic @SubjectId, @TopicName";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SubjectId", subjectId);
                command.Parameters.AddWithValue("@TopicName", topicName);
                command.ExecuteNonQuery();
                connection.Close();

                //You may want to comment out this part while debugging
                //Adding the books, if theres no books added in the listBox this loop doesn't excute
                //Since you said "make it so that the book entry is optional"
                foreach (var item in listBox1.Items)
                {
                    string BookName = item.ToString();
                    string BookId = BookName_Id[BookName].ToString();
                    connection.Open();
                    string query2 = "exex InsertBookTopic @BookId, @TopicId";
                    command = new SqlCommand(query2, connection);
                    command.Parameters.AddWithValue("@BookId", BookId);
                    //How do we get TopicId when the topic was freshly created?
                    //command.Parameters.AddWithValue("@TopicId", TopicId) 
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                MessageBox.Show("Topic Added Sucessfully");
            }
            if (topicName == null)
            {
                MessageBox.Show("Please Insert Topic Name");
            }
            if (subjectName == null)
            {
                MessageBox.Show("Please Select a Subject");
            }

            /*Form11 form11 = new Form11();
            form11.Show();
            this.Hide();*/
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Updating the entries in comboBox2
            List<string> BookList = new List<string>();

            string subjectSelcted = comboBox1.Text.ToString();
            string id = subjectName_Id[subjectSelcted].ToString();
            connection.Open();
            string query = "select BookName, BookID from Book where SubjectID = " + id;
            command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int BookId = Convert.ToInt32(reader["BookId"]);
                string BookName = reader["BookName"].ToString();

                subjectName_Id[BookName] = BookId;

                BookList.Add(BookName);
            }
            reader.Close();
            command.Dispose();
            connection.Close();
            comboBox2.DataSource = BookList;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string bookSelected = comboBox2.Text.ToString();
            if (listBox1.Items.Contains(bookSelected) == false && bookSelected != null)
            {
                listBox1.Items.Add(bookSelected);
            }
            if (bookSelected != null)
            {
                MessageBox.Show("Please select a book");
            }
            else
            {
                MessageBox.Show("This book has been already been selected");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string bookSelected = listBox1.Text.ToString();
            listBox1.Items.Remove(bookSelected);
        }
    }
}
