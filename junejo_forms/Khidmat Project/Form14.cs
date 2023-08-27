using DocumentFormat.OpenXml.Office.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xceed.Words.NET;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Khidmat_Project
{
    public partial class Form14 : Form
    {        
        SqlConnection connection = new SqlConnection(connectDb.connectionString);
        SqlCommand command = new SqlCommand();

        Dictionary<string, int> subjectName_Id = new Dictionary<string, int>();

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

        public Form14()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void Form14_Load(object sender, EventArgs e)
        {
            List<string> subjectList = getSubjects();
            comboBox1.DataSource = subjectList;

            comboBox1.SelectedIndex = -1;
            Search();
        }

        private void Search()
        {
            connection.Open();
            string query = "SELECT PaperID, S.SubjectId, PaperName, S.SubjectName, Date " +
                "FROM Past_Papers PP INNER JOIN Subject S on S.SubjectId = PP.SubjectId ";

            if (textBox1.Text.Length > 0)
            {
                query += @" AND PaperName LIKE '%' + @paperName + '%' ";
            }
            if (comboBox1.Text.Length > 0)
            {
                if (!subjectName_Id.ContainsKey(comboBox1.Text))
                {
                    MessageBox.Show("The given subject doesn't exist! Please add it first.");
                }
                else
                {
                    query += " AND S.SubjectId = @subjectId ";
                }
            }

            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@paperName", textBox1.Text);
            if (subjectName_Id.ContainsKey(comboBox1.Text))
            {
                command.Parameters.AddWithValue("@subjectId", subjectName_Id[comboBox1.Text]);
            }
            
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            command.Dispose();
            connection.Close();
            dataGridView1.DataSource = dataTable;
            dataGridView1.Columns[0].Visible = false;
        }


        //muzzzzzzzzzzzzzzzzzzz
        private void DeletePastPapers()
        {

        }

        private void InsertMCQInDocx(DocX doc, int paperId)
        {
            var placeholder = "{mcqs}";
            var paragraph = doc.Paragraphs.FirstOrDefault(p => p.Text.Contains(placeholder));

            if (paragraph == null)
            {
                MessageBox.Show("Your template is faulty, there is no '{mcqs}' placeholder to place the MCQs.");
                return;
            }

            var mcqList = doc.AddList();

            
            connection.Open();
            string query = "SELECT * FROM MCQs " +
                "WHERE MCQID IN (SELECT MCQID " +
                "FROM Paper_MCQ " +
                "WHERE PaperId = @paperId)";
            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@paperId", paperId);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int value1 = reader.GetInt32(0); //MCQID
                string value2 = reader.GetString(1); //Content
                string value3 = reader.GetString(4); //Option A
                string value4 = reader.GetString(5); //Option B
                string value5 = reader.GetString(6); //Option C
                string value6 = reader.GetString(7); //Option D

                doc.AddListItem(mcqList, value2);
                doc.AddListItem(mcqList, value3, 1);
                doc.AddListItem(mcqList, value4, 1);
                doc.AddListItem(mcqList, value5, 1);
                doc.AddListItem(mcqList, value6, 1);                    
            }
            reader.Close();
            command.Dispose();
            connection.Close();
            

            var index = doc.Paragraphs.IndexOf(paragraph);
            paragraph.InsertListAfterSelf(mcqList);
            //doc.Paragraphs.Remove();
        }

        private void InsertQuestionInDocx(DocX doc, int paperId, string type)
        {
            var placeholder = "{"+type+"}";
            var paragraph = doc.Paragraphs.FirstOrDefault(p => p.Text.Contains(placeholder));

            if (paragraph == null)
            {
                MessageBox.Show("Your template is faulty, there is no '{"+type+"}' placeholder to place the MCQs.");
                return;
            }

            var questionList = doc.AddList();


            connection.Open();
            string query = "SELECT * FROM Questions " +
                "WHERE QuestionID IN (SELECT QuestionID " +
                "FROM Paper_Question " +
                "WHERE PaperId = @paperId) AND Type = @type";
            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@paperId", paperId);
            command.Parameters.AddWithValue("@type", type);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int value1 = reader.GetInt32(0); //QuestionID
                string value2 = reader.GetString(4); //Content


                doc.AddListItem(questionList, value2);
            }
            reader.Close();
            command.Dispose();
            connection.Close();


            var index = doc.Paragraphs.IndexOf(paragraph);
            paragraph.InsertListAfterSelf(questionList);
            //doc.Paragraphs.Remove();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Select a row to view it!");
                return;
            } else if (dataGridView1.SelectedRows.Count > 1)
            {
                MessageBox.Show("You can only view one row at a time!");
                return;
            }

            int paperId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);

            string docxFilePath = @"C:\Users\Arsalan\Downloads\exams\exam-template.docx"; // Path for the Word document

            using (DocX doc = DocX.Load(docxFilePath))
            {
                var placeholder = "{mcqs}";
                var paragraph = doc.Paragraphs.FirstOrDefault(p => p.Text.Contains(placeholder));

                string paperInfo = dataGridView1.SelectedRows[0].Cells["SubjectName"].Value.ToString()
                    + ", " + dataGridView1.SelectedRows[0].Cells["PaperName"].Value.ToString()
                    +", " + dataGridView1.SelectedRows[0].Cells["Date"].Value.ToString();
                paragraph.InsertParagraphBeforeSelf(paperInfo);

                InsertMCQInDocx(doc, paperId);
                InsertQuestionInDocx(doc, paperId, "short");
                InsertQuestionInDocx(doc, paperId, "long");               

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Word Document|*.docx";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var savePath = saveFileDialog.FileName;
                        try
                        {
                            doc.SaveAs(savePath);
                            Process.Start(savePath);
                        }
                        catch (Exception error)
                        {
                            MessageBox.Show("An error occurred while saving the document: " + error.Message);
                        }

                    }
                    else
                    {
                        Console.WriteLine("User canceled the save operation.");
                    }

                    MessageBox.Show("Past Paper generated successfully", "Success");                    
                }
            }



        }
    }
}
