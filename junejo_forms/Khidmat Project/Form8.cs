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
    public partial class Form8 : Form
    {
        const string connectionString = @"Data Source=DESKTOP-PEGIUMG; Initial Catalog = khidmat_test1; Integrated Security = False; user id=Admin;password=Blaze30083;";
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand command = new SqlCommand();


        Dictionary<string, int> subjectName_Id = new Dictionary<string, int>();
        Dictionary<string, int> topicName_Id = new Dictionary<string, int>();


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
            command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@subjectid", subjectId);
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

        public Form8()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form9 form9 = new Form9();
            form9.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form10 form10 = new Form10();
            form10.Show();
            this.Hide();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            List<string> subjectList = getSubjects();
            comboBox1.DataSource = subjectList;

            comboBox1.SelectedIndex = -1;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
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

                comboBox3.Enabled = true;
                comboBox3.DataSource = topicList;
                comboBox3.SelectedIndex = -1;
            }
        }

        private void Search()
        {
            connection.Open();
            string query = "SELECT BookId, BookName, TopicName " +
                " FROM Book B INNER JOIN Book_Topic BT on B.BookId = BT.BookId " +
                " INNER JOIN Topic T on BT.TopicId = T.TopicId " +
                " WHERE 1=1 ";
            command = new SqlCommand(query, connection);

            if (textBox1.Text.Length > 0)
            {
                query += @" AND BookName LIKE '%' + @bookname + '%' ";
            }
            if (comboBox1.Text.Length > 0)
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
            if (comboBox3.Enabled && comboBox3.Text.Length > 0)
            {
                if (!topicName_Id.ContainsKey(comboBox3.Text))
                {
                    MessageBox.Show("The given topic doesn't exist! Please add it first.");
                }
                else
                {
                    query += " AND BT.TopicId = @topicid ";
                }
            }

            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@bookname", textBox1.Text);
            if (subjectName_Id.ContainsKey(comboBox1.Text))
            {
                command.Parameters.AddWithValue("@subjectid", subjectName_Id[comboBox1.Text]);
            }
            if (topicName_Id.ContainsKey(comboBox3.Text))
            {
                command.Parameters.AddWithValue("@topicid", topicName_Id[comboBox3.Text]);
            }

            connection.Close();
        }
    }
}
