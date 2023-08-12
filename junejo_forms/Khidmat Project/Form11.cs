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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Khidmat_Project
{

    public partial class Form11 : Form
    {
        const string connectionString = @"Data Source =DESKTOP-PEGIUMG; Inital Catalog = khidmat_test1; Integrated Security = False; user id =Admin;password=Blaze30083";
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand command = new SqlCommand();
        Dictionary<string, int> subjectName_Id = new Dictionary<string, int>();
        Dictionary<string, int> BookName_Id = new Dictionary<string, int>();

        public Form11()
        {
            InitializeComponent();
        }

        private void Form11_Load(object sender, EventArgs e)
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

        private void button5_Click(object sender, EventArgs e)
        {
            Form13 form13 = new Form13();
            form13.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form13 form13 = new Form13();
            form13.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e) //Search Button
        {
            string topicName = textBox1.Text.ToString();
            string query;
            if(topicName == null)
            {
                query = "SELECT TopicName FROM Topic"; //Showing all topics
            }
            else
            {
                query = "SELECT TopicName FROM Topic WHERE TopicName LIKE %" + @topicName + "%"; //Showing topics based on entered data
            }
            connection.Open();
            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@topicName", topicName);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            command.Dispose(); 
            connection.Close();
            dataGridView1.DataSource = dataTable;
        }

        private void button6_Click(object sender, EventArgs e) //Filter Button
        {
            string topicName = textBox1.Text.ToString();
            string subjectName = comboBox1.Text.ToString();
            string bookName = comboBox3.Text.ToString();


            string query = "SELECT TopicName FROM Topic WHERE 1=1 ";
            
            if(topicName != null)
            {
                query = query + " AND TopicName LIKE %" + "@topicName" + "%";
                command.Parameters.AddWithValue("@topicName", topicName);
            }
            if(subjectName != null)
            {
                query = query + " AND SubjectId = @SubjectID ";
                command.Parameters.AddWithValue("@SubjectID", subjectName_Id[subjectName]);
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) //Filling the book comboBox
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
            comboBox3.DataSource = BookList;
        }
    }
}
