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
    public partial class Form5 : Form
    {
        SqlConnection connection = new SqlConnection(connectDb.connectionString);
        SqlCommand command = new SqlCommand();


        Dictionary<string, int> subjectName_Id = new Dictionary<string, int>();
        Dictionary<string, int> topicName_Id = new Dictionary<string, int>();
        Dictionary<string, int> bookName_Id = new Dictionary<string, int>();
        Dictionary<string, int> difficultyName_Id = new Dictionary<string, int> { { "easy", 1 }, { "medium", 2 }, { "hard", 3 } };
        List<string> mcqChoices = new List<string> { "A", "B", "C", "D"};

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

        private List<string> GetTopics(int subjectId)
        {
            List<string> topicList = new List<string>();
            connection.Open();
            string query = "select * from Topic where SubjectId = @subjectid";
            if (bookName_Id.ContainsKey(comboBox2.Text))
            {
                query += " AND TopicId IN (SELECT TopicId FROM Book_Topic WHERE BookId = @bookid)";
            }
            command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@subjectid", subjectId);
            if (bookName_Id.ContainsKey(comboBox2.Text))
            {
                command.Parameters.AddWithValue("@bookid", bookName_Id[comboBox2.Text]);
            }
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int topicId = Convert.ToInt32(reader["TopicId"]);
                string topicName = reader["TopicName"].ToString();
                topicName_Id[topicName] = topicId;
                topicList.Add(topicName);
            }
            reader.Close();
            command.Dispose();
            connection.Close();
            return topicList;
        }

        private List<string> GetBooks(int subjectId)
        {
            List<string> bookList = new List<string>();
            connection.Open();
            string query = "select * from Book where SubjectId = " + subjectId;
            command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int topicId = Convert.ToInt32(reader["BookId"]);
                string topicName = reader["BookName"].ToString();
                bookName_Id[topicName] = topicId;
                bookList.Add(topicName);
            }
            reader.Close();
            command.Dispose();
            connection.Close();
            return bookList;
        }

        public Form5()
        {
            InitializeComponent();
        }
        private List<string> GetDifficultyList()
        {
            return new List<string> { "easy", "medium", "hard" };
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                comboBox5.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
            }
            else
            {
                comboBox5.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Form4 form4 = new Form4();
            //form4.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length == 0)
            {
                MessageBox.Show("Please fill out all the fields!");
                return;
            }
            if (!subjectName_Id.ContainsKey(comboBox1.Text))
            {
                MessageBox.Show("Please enter a valid subject!");
                return;
            }
            if (!topicName_Id.ContainsKey(comboBox3.Text))
            {
                MessageBox.Show("Please enter a valid topic!");
                return;
            }

            if (!difficultyName_Id.ContainsKey(comboBox4.Text))
            {
                MessageBox.Show("Please enter a valid difficulty!");
                return;
            }

            if (radioButton3.Checked)
            {
                if(textBox2.Text.Length == 0 || textBox3.Text.Length == 0 || textBox4.Text.Length == 0 || textBox5.Text.Length == 0)
                {
                    MessageBox.Show("Please enter all MCQ options!");
                    return;
                }
                if(!mcqChoices.Contains(comboBox5.Text))
                {
                    MessageBox.Show("Enter a valid answer!");
                    return;
                }
                InsertMCQ();
            }
            else
            {
                if (radioButton1.Checked)
                {
                    InsertQuestion("short");
                } 
                else if(radioButton2.Checked)
                {
                    InsertQuestion("long");
                }
            }

            MessageBox.Show("Success!");
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        private void InsertMCQ()
        {
            string content = textBox1.Text;
            int difficulty = difficultyName_Id[comboBox4.Text];
            int topicId = topicName_Id[comboBox3.Text];
            string optionA = textBox2.Text;
            string optionB = textBox3.Text;
            string optionC = textBox4.Text;
            string optionD = textBox5.Text;
            string answer = comboBox5.Text;

            connection.Open();
            string query = "EXEC InsertMCQ @content, @difficulty, @topicid, @optiona, @optionb, @optionc, @optiond, @answer";
            command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@content", content);
            command.Parameters.AddWithValue("@difficulty", difficulty);
            command.Parameters.AddWithValue("@topicid", topicId);
            command.Parameters.AddWithValue("@optiona", optionA);
            command.Parameters.AddWithValue("@optionb", optionB);
            command.Parameters.AddWithValue("@optionc", optionC);
            command.Parameters.AddWithValue("@optiond", optionD);
            command.Parameters.AddWithValue("@answer", answer);

            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();
        }

        private void InsertQuestion(string type)
        {
            string content = textBox1.Text;
            int difficulty = difficultyName_Id[comboBox4.Text];
            int topicId = topicName_Id[comboBox3.Text];

            connection.Open();
            string query = "EXEC InsertQuestion @topicid, @difficulty, @type, @content";
            command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@content", content);
            command.Parameters.AddWithValue("@difficulty", difficulty);
            command.Parameters.AddWithValue("@topicid", topicId);
            command.Parameters.AddWithValue("@type", type);

            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            List<string> subjectList = getSubjects();
            comboBox1.DataSource = subjectList;
            comboBox5.DataSource = mcqChoices;
            comboBox4.DataSource = GetDifficultyList();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text.Length <= 0)
            {
                comboBox3.Enabled = false;
                comboBox3.DataSource = null;
            }
            else
            {
                int subjectId = subjectName_Id[comboBox1.Text];
                List<string> topicList = GetTopics(subjectId);
                List<string> bookList = GetBooks(subjectId);

                comboBox3.Enabled = true;
                comboBox3.DataSource = topicList;
                comboBox3.SelectedIndex = -1;

                comboBox2.Enabled = true;
                comboBox2.DataSource = bookList;
                comboBox2.SelectedIndex = -1;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int subjectId = subjectName_Id[comboBox1.Text];
            List<string> topicList = GetTopics(subjectId);
            comboBox3.DataSource = topicList;
            comboBox3.SelectedIndex = -1;
           
        }
    }
}
