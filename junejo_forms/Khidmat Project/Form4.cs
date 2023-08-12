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
    public partial class Form4 : Form
    {
        const string connectionString = @"Data Source=DESKTOP-PEGIUMG; Initial Catalog = khidmat_test1; Integrated Security = False; user id=Admin;password=Blaze30083;";
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand command = new SqlCommand();
        Dictionary<string, int> subjectName_Id = new Dictionary<string, int>();
        Dictionary<string, int> topicName_Id = new Dictionary<string, int>();   
        public Form4()
        {
            InitializeComponent();
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
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            form6.Show();
            this.Hide();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            List<string> subjectList = getSubjects();
            comboBox1.DataSource = subjectList;
            Search();
        }

        private void Search()
        {
            connection.Open();
            string query = "SELECT Content, Type, Diagram, TopicName, SubjectName " +
                "FROM Questions Q INNER JOIN Topic T ON Q.TopicId = T.TopicId " +
                "INNER JOIN Subject S ON T.SubjectId = S.SubjectId " +
                "WHERE 1=1 ";

            if(textBox1.Text.Length > 0)
            {
                query += @"AND Content LIKE '%' + @content + '%'";
            }
            if(comboBox1.Text.Length > 0)
            {
                query += "AND SubjectId = @subjectid";
            }
            if(comboBox3.Enabled && comboBox3.Text.Length > 0)
            {
                query += "AND TopicId = @topicid";
            }

            command = new SqlCommand(query, connection);
            com
            command.Parameters.AddWithValue("@subjectid", subjectName_Id[comboBox1.Text]);


            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            command.Dispose();
            connection.Close();
            dataGridView1.DataSource=dataTable;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text.Length > 0)
            {
                int subjectId = subjectName_Id[comboBox1.Text];
                List<string> topicList = GetTopics(subjectId);
                comboBox3.Enabled = true;
                comboBox3.DataSource = topicList;
            }
        }
    }
}
