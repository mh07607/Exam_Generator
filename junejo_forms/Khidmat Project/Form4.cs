using System;
using System.Collections;
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
    public partial class Form4 : Form
    {
        const string connectionString = @"Data Source=DESKTOP-PEGIUMG; Initial Catalog = khidmat_test1; Integrated Security = False; user id=Admin;password=Blaze30083;";
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand command = new SqlCommand();


        Dictionary<string, int> subjectName_Id = new Dictionary<string, int>();
        Dictionary<string, int> topicName_Id = new Dictionary<string, int>();
        Dictionary<string, int> difficultyName_Id = new Dictionary<string, int> { {"easy", 1}, {"medium", 2 }, {"hard", 3 } };
        List<string> typeList = new List<string> { "short", "long" };
    public Form4()
        {
            InitializeComponent();
        }

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
            string query = "select * from Topic where SubjectId = " + subjectId;
            command = new SqlCommand(query, connection);
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
            //this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            form6.Show();
            //this.Hide();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            List<string> subjectList = getSubjects();
            List<string> difficultyList = GetDifficultyList();


            comboBox1.DataSource = subjectList;
            comboBox4.DataSource = difficultyList;
            comboBox5.DataSource = typeList;
            
            comboBox1.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;

            Search();
        }

        private void Search()
        {
            connection.Open();
            string query = "SELECT Content, Type, Difficulty, Diagram, TopicName, SubjectName " +
                    "FROM Questions Q INNER JOIN Topic T ON Q.TopicId = T.TopicId " +
                    "INNER JOIN Subject S ON T.SubjectId = S.SubjectId " +
                    "WHERE 1=1 ";

            if (radioButton2.Checked)
            {
                query = "SELECT Content, Difficulty, OptionA, OptionB, OptionC, OptionD, Answer, TopicName, SubjectName " +
                    "FROM MCQs Q INNER JOIN Topic T ON Q.TopicId = T.TopicId " +
                    "INNER JOIN Subject S ON T.SubjectId = S.SubjectId " +
                    "WHERE 1=1 ";
            }

            if(textBox1.Text.Length > 0)
            {
                query += @" AND Content LIKE '%' + @content + '%' ";
            }
            if(comboBox1.Text.Length > 0)
            {
                if (!subjectName_Id.ContainsKey(comboBox1.Text))
                {
                    MessageBox.Show("The given subject doesn't exist! Please add it first.");
                }
                else
                {
                    query += " AND S.SubjectId = @subjectid ";
                }
            }
            if(comboBox3.Enabled && comboBox3.Text.Length > 0)
            {
                if (!topicName_Id.ContainsKey(comboBox3.Text))
                {
                    MessageBox.Show("The given topic doesn't exist! Please add it first.");
                }
                else
                {
                    query += " AND T.TopicId = @topicid ";
                }
            }

            if (comboBox4.Text.Length > 0)
            {
                if (!difficultyName_Id.ContainsKey(comboBox4.Text))
                {
                    MessageBox.Show(comboBox4.Text + " is not a valid difficulty!");
                }
                else
                {
                    query += " AND Difficulty = @difficulty ";
                }
            }

            if(comboBox5.Text.Length > 0)
            {
                if (!typeList.Contains(comboBox5.Text))
                {
                    MessageBox.Show(comboBox5.Text + " is not a valid type!");
                }
                else
                {
                    query += " AND Type = @type ";
                }
            }

            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@content", textBox1.Text);
            if (subjectName_Id.ContainsKey(comboBox1.Text))
            {
               command.Parameters.AddWithValue("@subjectid", subjectName_Id[comboBox1.Text]);    
            }
            if (topicName_Id.ContainsKey(comboBox3.Text))
            {
               command.Parameters.AddWithValue("@topicid", topicName_Id[comboBox3.Text]);
            }
            if (difficultyName_Id.ContainsKey(comboBox4.Text))
            {
               command.Parameters.AddWithValue("@difficulty", difficultyName_Id[comboBox4.Text]);
            }
            if(typeList.Contains(comboBox5.Text))
            {
                command.Parameters.AddWithValue("@type", comboBox5.Text);
            }

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            command.Dispose();
            connection.Close();
            dataGridView1.DataSource=dataTable;
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
                comboBox3.Enabled = true;
                comboBox3.DataSource = topicList;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            {
                comboBox5.Enabled = true;
                comboBox5.DataSource = typeList;
                comboBox5.SelectedIndex = -1;
            } else
            {
                comboBox5.Enabled = false;
                comboBox5.DataSource = null;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
