using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office.Word;
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
    public partial class NewPaper : Form
    {
        SqlConnection connection = new SqlConnection(connectDb.connectionString);
        SqlCommand command = new SqlCommand();

        List<int> mcqIds;
        List<int> questionIds;
        int subjectId;
        public NewPaper(List<int> questionIds, List<int> mcqIds, int subjectId)
        {
            InitializeComponent();
            this.questionIds = questionIds;
            this.mcqIds = mcqIds;
            this.subjectId = subjectId;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                MessageBox.Show("Please enter a name for your paper.");
                return;
            }
            string paperName = textBox1.Text;
            string dateTime;
            if(dateTimePicker1.Value == DateTime.MinValue)
            {
                dateTime = DateTime.Now.ToString();
            }
            else
            {
                dateTime = dateTimePicker1.Value.ToString();
            }

            try 
            {
                InsertPastPaper(paperName, dateTime);
                MessageBox.Show("Success!");
            }
            catch (Exception error)
            {
                MessageBox.Show("Could not insert past paper. " + error.Message);
            }
            
        }

        private void InsertPastPaper(string paperName, string dateTime)
        {
            connection.Open();

            string query1 = "INSERT INTO Past_Papers (Date, PaperName, SubjectId)" +
                "VALUES (@dateTime, @paperName, @subjectId)";
            command = new SqlCommand(query1, connection);
            command.Parameters.AddWithValue("@dateTime", dateTime);
            command.Parameters.AddWithValue("@paperName", paperName);
            command.Parameters.AddWithValue("@subjectId", subjectId);
            command.ExecuteNonQuery();
            command.Dispose();

            string query2 = "SELECT TOP 1 PaperID " +
                "FROM Past_Papers " +
                "ORDER BY PaperID DESC";
            command = new SqlCommand(query2, connection);
            object result = command.ExecuteScalar();
            connection.Close();

            int paperId = Convert.ToInt32(result);
            InsertPastPaper_Questions(paperId, questionIds);
            InsertPastPaper_MCQs(paperId, mcqIds);

        }

        private void InsertPastPaper_Questions(int paperId, List<int> questions)
        {
            for (int i = 0; i < questions.Count; i++)
            {
                connection.Open();

                string query = "INSERT INTO Paper_Question (PaperID, QuestionID)" +
                    "VALUES (@paperId, @questionId)";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@questionId", questions[i]);
                command.Parameters.AddWithValue("@paperId", paperId);
                command.ExecuteNonQuery();

                command.Dispose();
                connection.Close();
            }
        }
        private void InsertPastPaper_MCQs(int paperId, List<int> mcqs)
        {
            for (int i = 0; i < mcqs.Count; i++)
            {
                connection.Open();

                string query = "INSERT INTO Paper_MCQ (PaperID, MCQID)" +
                    "VALUES (@paperId, @mcqId)";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@mcqId", mcqs[i]);
                command.Parameters.AddWithValue("@paperId", paperId);
                command.ExecuteNonQuery();

                command.Dispose();
                connection.Close();
            }
        }
    }
}
