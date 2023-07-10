using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Khidmat_UI
{
    public partial class NewExam : Form
    {
        //Arsalan laptopdb: DESKTOP-PEGIUMG\YEET
        //Arsalan pcdb: DESKTOP-6N9R52E\SQLEXPRESS

        const string connectionString = @"Data Source=DESKTOP-PEGIUMG\YEET; Initial Catalog = khidmat_test; Integrated Security = True";
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand command = new SqlCommand();

        public NewExam()
        {
            InitializeComponent();
            comboBox1.DataSource = new List<string>();
        }

        private List<string> getSubjects()
        {
            List<string> subjectList = new List<string>();

            connection.Open();
            string query = "select SubjectName from Subject";
            command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string subjectName = reader["SubjectName"].ToString();
                subjectList.Add(subjectName);
            }

            reader.Close();
            command.Dispose();
            connection.Close();
            return subjectList;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<string> subjectList = getSubjects();
            comboBox1.DataSource = subjectList.ToArray();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string texContent = GenerateTexContent();

            string outputDirectory = @"C:\Users\Arsalan\Downloads";

            string outputFileName = "exam.tex";

            // Combine the output directory and file name to create the full file path
            string outputPath = Path.Combine(outputDirectory, outputFileName);

            try
            {
                File.WriteAllText(outputPath, texContent);

                Console.WriteLine("TeX file generated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        private string GenerateTexContent()
        {
            // Generate the content for the TeX file
            string texContent = @"\documentclass{exam}
\usepackage{polyglossia}
\usepackage{fontspec}
\usepackage{bidi}

\setmainlanguage{english}
\setotherlanguage{arabic}
\newfontfamily\arabicfont[Script=Arabic]{Amiri}

\makeatletter
\renewcommand{\@seccntformat}[1]{\protect\RTL\protect\textbf{\csname the#1\endcsname\quad}}
\makeatother

\begin{document}


\begin{RTL}
\section{ایم سی کیو}
    \begin{questions}
      \begin{Arabic}
        \question ما هي عاصمة فرنسا؟
      \end{Arabic}
    
      \begin{Arabic}
        \question من رسم لوحة الموناليزا؟
      \end{Arabic}
    \end{questions}

\section{مختصر سوالات}
    \begin{questions}
      \begin{Arabic}
        \question ما هي عاصمة فرنسا؟
      \end{Arabic}
    
      \begin{Arabic}
        \question من رسم لوحة الموناليزا؟
      \end{Arabic}
    \end{questions}

\section{طویل سوالات}
    \begin{questions}
      \begin{Arabic}
        \question ما هي عاصمة فرنسا؟
      \end{Arabic}
    
      \begin{Arabic}
        \question من رسم لوحة الموناليزا؟
      \end{Arabic}
    \end{questions}
\end{RTL}

\end{document}
";

            return texContent;
        }

        private List<int> GetTopicIds()
        {
            List<int> topicIds = new List<int>();
            connection.Open();
            string query = "select TopicId from Topic";
            command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int topicId = Convert.ToInt32(reader["TopicId"]);
                topicIds.Add(topicId);
            }

            reader.Close();
            command.Dispose();
            connection.Close();
            return topicIds;
        }

        private List<(int, string, string, string, string, string, string)> GetMCQs()
        {
            List<(int, string, string, string, string, string, string)> mcQs = new List<(int, string, string, string, string, string, string)>();

            List<int> topicIds = GetTopicIds();
            connection.Open();

            for (int i = 0; i < topicIds.Count; i++)
            {
                string query = "select ";
            }

            connection.Close();

            return mcQs;
        }
        private string GenerateMCQTex()
        {
            string texContent = "";


            return texContent;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EditTopics editTopics = new EditTopics();
        }

        /*
        private string GenerateShortTex()
        {

        }

        private string GenerateLongTex()
        {

        }
        */

    }
}
