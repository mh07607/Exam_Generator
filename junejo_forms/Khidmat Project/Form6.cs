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
    public partial class Form6 : Form
    {
        const string connectionString = @"Data Source=DESKTOP-PEGIUMG; Initial Catalog = khidmat_test1; Integrated Security = False; user id=Admin;password=Blaze30083;";
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand command = new SqlCommand();


        Dictionary<string, int> subjectName_Id = new Dictionary<string, int>();
        Dictionary<string, int> topicName_Id = new Dictionary<string, int>();
        Dictionary<string, int> bookName_Id = new Dictionary<string, int>();

        Dictionary<string, int> difficultyName_Id = new Dictionary<string, int> { { "easy", 1 }, { "medium", 2 }, { "hard", 3 } };
        List<string> mcqChoices = new List<string> { "A", "B", "C", "D" };

        int questionID;
        bool isMcq;

        private List<string> GetDifficultyList()
        {
            return new List<string> { "easy", "medium", "hard" };
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

        public Form6(int questionId, bool isMCQ)
        {
            InitializeComponent();
            questionID = questionId;
            isMcq = isMCQ;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
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

            if (isMcq)
            {
                if (textBox2.Text.Length == 0 || textBox3.Text.Length == 0 || textBox4.Text.Length == 0 || textBox5.Text.Length == 0)
                {
                    MessageBox.Show("Please enter all MCQ options!");
                    return;
                }
                if (!mcqChoices.Contains(comboBox5.Text))
                {
                    MessageBox.Show("Enter a valid answer!");
                    return;
                }
                UpdateMCQ();
            }
            else
            {
                if (radioButton1.Checked)
                {
                    UpdateQuestion("short");
                }
                else if (radioButton2.Checked)
                {
                    UpdateQuestion("long");
                }
            }
            this.Hide();
        }

        private void UpdateQuestion(string type)
        {
            string content = textBox1.Text;
            int difficulty = difficultyName_Id[comboBox4.Text];
            int topicId = topicName_Id[comboBox3.Text];

            connection.Open();
            string query = "EXEC UpdateQuestion @questionid, @content, @difficulty, @topicid, @type";
            command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@content", content);
            command.Parameters.AddWithValue("@difficulty", difficulty);
            command.Parameters.AddWithValue("@topicid", topicId);
            command.Parameters.AddWithValue("@type", type);
            command.Parameters.AddWithValue("@questionid", questionID);

            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();
        }
        private void UpdateMCQ()
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
            string query = "EXEC UpdateMCQ @mcqid, @content, @difficulty, @topicid, @optiona, @optionb, @optionc, @optiond, @answer";
            command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@content", content);
            command.Parameters.AddWithValue("@difficulty", difficulty);
            command.Parameters.AddWithValue("@topicid", topicId);
            command.Parameters.AddWithValue("@optiona", optionA);
            command.Parameters.AddWithValue("@optionb", optionB);
            command.Parameters.AddWithValue("@optionc", optionC);
            command.Parameters.AddWithValue("@optiond", optionD);
            command.Parameters.AddWithValue("@answer", answer);
            command.Parameters.AddWithValue("@mcqid", questionID);

            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            if (isMcq)
            {
                comboBox5.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
            }
            else
            {
                comboBox5.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                radioButton3.Enabled = false;
            }


            List<string> subjectList = getSubjects();
            comboBox1.DataSource = subjectList;
            List<string> topicList = GetTopics(subjectName_Id[comboBox1.Text]);
            comboBox3.DataSource = topicList;
            comboBox5.DataSource = mcqChoices;
            comboBox4.DataSource = GetDifficultyList();
            if (isMcq)
            {
                AutofillMCQData();
            }
            else
            {
                AutofillQuestionData();
            }
            
        }   

        private void AutofillQuestionData()
        {
            connection.Open();

            string query = "SELECT * " +
                "FROM Questions Q INNER JOIN Topic T ON Q.TopicId = T.TopicId " +
                "INNER JOIN Subject S ON S.SubjectId = T.SubjectId " +
                "WHERE Q.QuestionID = @questionid";

            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@questionid", questionID);

            string subject, topic, content, type;
            int difficulty;

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read()) // Move to the first row
            {
                subject = reader.GetString(reader.GetOrdinal("SubjectName"));
                topic = reader.GetString(reader.GetOrdinal("TopicName"));
                content = reader.GetString(reader.GetOrdinal("Content"));
                difficulty = reader.GetInt32(reader.GetOrdinal("Difficulty"));
                type= reader.GetString(reader.GetOrdinal("Type"));
            }
            else
            {
                MessageBox.Show("The selected question doesn't exist anymore!");
                return;
            }

            command.Dispose();
            connection.Close();

            List<string> subjectList = (List<string>)comboBox1.DataSource;
            List<string> topicList = (List<string>)comboBox3.DataSource;

            textBox1.Text = content;
            comboBox1.SelectedIndex = subjectList.IndexOf(subject);
            comboBox3.SelectedIndex = topicList.IndexOf(topic);
            comboBox4.SelectedIndex = difficulty-1;
            if(type == "short")
            {
                radioButton1.Checked = true;
            } 
            else
            {
                radioButton2.Checked = true;
            }
            
        }

        private void AutofillMCQData()
        {
            connection.Open();

            string query = "SELECT * " +
                "FROM MCQs Q INNER JOIN Topic T ON Q.TopicId = T.TopicId " +
                "INNER JOIN Subject S ON S.SubjectId = T.SubjectId " +
                "WHERE Q.MCQId = @questionid";

            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@questionid", questionID);

            string subject, topic, content, optionA, optionB, optionC, optionD, answer;
            int difficulty;

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read()) // Move to the first row
            {
                subject = reader.GetString(reader.GetOrdinal("SubjectName"));
                topic = reader.GetString(reader.GetOrdinal("TopicName"));
                content = reader.GetString(reader.GetOrdinal("Content"));
                difficulty = reader.GetInt32(reader.GetOrdinal("Difficulty"));
                optionA = reader.GetString(reader.GetOrdinal("OptionA"));
                optionB = reader.GetString(reader.GetOrdinal("OptionB"));
                optionC = reader.GetString(reader.GetOrdinal("OptionC"));
                optionD = reader.GetString(reader.GetOrdinal("OptionD"));
                answer = reader.GetString(reader.GetOrdinal("Answer"));

            }
            else
            {
                MessageBox.Show("The selected question doesn't exist anymore!");
                return;
            }

            command.Dispose();
            connection.Close();

            List<string> subjectList = (List<string>)comboBox1.DataSource;
            List<string> topicList = (List<string>)comboBox3.DataSource;

            textBox1.Text = content;
            textBox2.Text = optionA;
            textBox3.Text = optionB;
            textBox4.Text = optionC;
            textBox5.Text = optionD;
            radioButton3.Checked = true;

            comboBox1.SelectedIndex = subjectList.IndexOf(subject);
            comboBox3.SelectedIndex = topicList.IndexOf(topic);
            comboBox4.SelectedIndex = difficulty-1;
            comboBox5.SelectedIndex = mcqChoices.IndexOf(answer);
        }

        private void label3_Click(object sender, EventArgs e)
        {

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
                //List<string> bookList = GetBooks(subjectId);

                comboBox3.Enabled = true;
                comboBox3.DataSource = topicList;
                comboBox3.SelectedIndex = -1;

                //comboBox2.Enabled = true;
                //comboBox2.DataSource = bookList;
                //comboBox2.SelectedIndex = -1;
            }
        }
    }
}
