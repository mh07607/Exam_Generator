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
using System.Security.AccessControl;
using System.Reflection.Metadata;
using Aspose.Pdf;
using System.Diagnostics;

namespace Khidmat_UI
{
    public partial class NewExam : Form
    {
        //Arsalan laptopdb: DESKTOP-PEGIUMG\YEET
        //Arsalan pcdb: DESKTOP-6N9R52E\SQLEXPRESS
        // connecting laptop db to pc app: const string connectionString = @"Data Source=DESKTOP-PEGIUMG; Initial Catalog = khidmat_test1; Integrated Security = False; user id=Admin;password=Blaze30083;";
        //same laptop: const string connectionString = @"Data Source=DESKTOP-PEGIUMG; Initial Catalog = khidmat_test1; Integrated Security = True;"

        List<(string, TextBox, TextBox, TextBox)> ShortQuestions = new List<(string, TextBox, TextBox, TextBox)>();
        List<(string, TextBox, TextBox, TextBox)> LongQuestions = new List<(string, TextBox, TextBox, TextBox)>();
        List<(string, TextBox, TextBox, TextBox)> MCQs = new List<(string, TextBox, TextBox, TextBox)>();
        const string connectionString = @"Data Source=DESKTOP-PEGIUMG; Initial Catalog = khidmat_test1; Integrated Security = False; user id=Admin;password=Blaze30083;";
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


            List<string> topicList = new List<string>();
            connection.Open();
            string query = "select * from Topic";
            command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string topicName = reader["TopicName"].ToString();
                topicList.Add(topicName);
            }
            reader.Close();
            command.Dispose();
            connection.Close();

            for (int i = 0; i < topicList.Count; i++)
            {
                Label topicName = new Label();

                topicName.Text = topicList[i];

                TextBox numEasyShort = new TextBox();
                TextBox numMediumShort = new TextBox();
                TextBox numHardShort = new TextBox();

                topicName.Location = new System.Drawing.Point(60, 55 + (i * 30));
                numEasyShort.Location = new System.Drawing.Point(320, 50 + (i * 30));
                numMediumShort.Location = new System.Drawing.Point(420, 50 + (i * 30));
                numHardShort.Location = new System.Drawing.Point(520, 50 + (i * 30));

                topicName.AutoSize = true;

                numEasyShort.Width = numMediumShort.Width = numHardShort.Width = 40;
                numEasyShort.Height = numMediumShort.Height = numHardShort.Height = 25;

                groupBox4.Controls.Add(topicName);
                groupBox4.Controls.Add(numEasyShort);
                groupBox4.Controls.Add(numMediumShort);
                groupBox4.Controls.Add(numHardShort);

                (string topicName, TextBox easyShort, TextBox mediumShort, TextBox hardShort) data = (topicList[i], numEasyShort, numMediumShort, numHardShort);
                ShortQuestions.Add(data);
            }
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
            /*
            string texContent = GenerateTexContent();

            string outputDirectory = @"C:\Users\Arsalan\Downloads";

            string outputFileName = "exam.tex";
            string pdfFileName = "exam.pdf";

            // Combine the output directory and file name to create the full file path
            string outputPath = Path.Combine(outputDirectory, outputFileName);
            string pdfPath = Path.Combine(outputDirectory, pdfFileName);

            try
            {
                File.WriteAllText(outputPath, texContent);

                TeXLoadOptions options = new TeXLoadOptions();
                // Create Document object and initialize it
                Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(outputPath, options);
                pdfDocument.LaTeXOptions.TeXProgramPath = "xelatex";
                // Save LaTeX file as PDF
                pdfDocument.Save(pdfPath);

                Console.WriteLine("TeX file generated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            */

            string texContent = GenerateTexContent();
            string outputDirectory = @"C:\Users\Arsalan\Downloads";
            string outputFileName = "exam.tex";
            string pdfFileName = "exam.pdf";
            string outputPath = Path.Combine(outputDirectory, outputFileName);
            string pdfPath = Path.Combine(outputDirectory, pdfFileName);

            try
            {
                File.WriteAllText(outputPath, texContent);

                // Execute XeLaTeX command using Process class
                Process process = new Process();
                process.StartInfo.FileName = "xelatex"; // Path to XeLaTeX executable
                process.StartInfo.Arguments = $"-output-directory=\"{outputDirectory}\" \"{outputPath}\"";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();

                // Wait for the process to finish
                process.WaitForExit();

                if (process.ExitCode == 0)
                {
                    Console.WriteLine("TeX file compiled successfully.");

                    // Rename the generated PDF file
                    string generatedPdfPath = Path.ChangeExtension(outputPath, ".pdf");
                    File.Move(generatedPdfPath, pdfPath);

                    Console.WriteLine("PDF file generated successfully.");
                }
                else
                {
                    Console.WriteLine("An error occurred while compiling the TeX file.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        private string GenerateTexContent()
        {
            // Generate the content for the TeX file
            /*string texContent = @"\documentclass{exam}
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
";*/
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


\begin{RTL} ";

            texContent = texContent + GenerateShortTex();
            texContent = texContent + GenerateLongTex();
            texContent = texContent + GenerateMCQTex();
            texContent = texContent + @" \end{RTL}
\end{document}";
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
            string texContent = @" \section{ایم سی کیو}
    \begin{questions} ";
            string numEasy = textBox1.Text;
            string numMedium = textBox3.Text;
            string numHard = textBox4.Text;

            connection.Open();
            string query = "EXEC GetRandomMCQs " + numEasy + "," + numMedium + "," + numHard + "," + "short";
            command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int value1 = reader.GetInt32(0); /*MCQID*/
                string value2 = reader.GetString(1); /*#Content*/
                string value3 = reader.GetString(4); /*Option A*/
                string value4 = reader.GetString(5); /*Option B*/
                string value5 = reader.GetString(6); /*Option C*/
                string value6 = reader.GetString(7); /*Option D*/

                texContent = texContent + @"\begin{Arabic} \question " + value2 + @" // A. " + value3 + @" // B. " + value4 + @" // C. " + value5 + @" // D. " + value6 + @" \end{Arabic} ";
            }
            reader.Close();
            command.Dispose();
            connection.Close();

            texContent = texContent + @" \end{questions}";
            return texContent;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EditTopics editTopics = new EditTopics();
            editTopics.Show();
        }


        private string GenerateShortTex()
        {
            string texContent = @" \section{مختصر سوالات}
    \begin{questions} ";
            for (int i = 0; i < ShortQuestions.Count; i++)
            {
                string topic = ShortQuestions[i].Item1;
                string numEasy = ShortQuestions[i].Item2.Text;
                string numMedium = ShortQuestions[i].Item3.Text;
                string numHard = ShortQuestions[i].Item4.Text;
                if (string.IsNullOrEmpty(numEasy))
                {
                    numEasy = "0";
                }
                if (string.IsNullOrEmpty(numMedium))
                {
                    numMedium = "0";
                }
                if (string.IsNullOrEmpty(numHard))
                {
                    numHard = "0";
                }

                List<int> shortQuestionIDs = new List<int>();
                connection.Open();
                string query = "EXEC GetRandomQuestions " + numEasy + "," + numMedium + "," + numHard + "," + "short" + "," + topic;
                command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int value1 = reader.GetInt32(0); /*QuestionID*/
                    string value5 = reader.GetString(4); /*Content*/

                    texContent = texContent + @" \begin{Arabic} \question " + value5 + @" \end{Arabic} ";
                    shortQuestionIDs.Add(value1);
                }
                reader.Close();
                command.Dispose();
                connection.Close(); /*Here readrer is being opened and closed for each topic would that be a problem?*/
            }
            texContent = texContent + @" \end{questions}";
            return texContent;
            /*string numEasy = textBox5.Text;
            string numMedium = textBox6.Text;
            string numHard = textBox7.Text;

            List<Tuple<int, int, int, string, string>> shortQuestions =
                new List<Tuple<int, int, int, string, string>>();

            connection.Open();
            string query = "EXEC GetRandomQuestions " + numEasy + "," + numMedium + "," + numHard + "," + "short";
            command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int value1 = reader.GetInt32(0);
                int value2 = reader.GetInt32(1);
                int value3 = reader.GetInt32(2);
                string value4 = reader.GetString(3);
                string value5 = reader.GetString(4);

                texContent = texContent + @" \begin{Arabic} \question " + value5 + @" \end{Arabic} ";


                Tuple<int, int, int, string, string> question = Tuple.Create(value1, value2, value3, value4, value5);
                shortQuestions.Add(question);

            }
            reader.Close();
            command.Dispose();
            connection.Close();

            texContent = texContent + @" \end{questions}";
            return texContent; */

        }


        private string GenerateLongTex()
        {
            string texContent = @" \section{طویل سوالات}
    \begin{questions} ";

            string numEasy = textBox11.Text;
            string numMedium = textBox10.Text;
            string numHard = textBox9.Text;

            List<Tuple<int, int, int, string, string>> shortQuestions =
                new List<Tuple<int, int, int, string, string>>();

            connection.Open();
            string query = "EXEC GetRandomQuestions " + numEasy + "," + numMedium + "," + numHard + "," + "long";
            command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int value1 = reader.GetInt32(0);
                int value2 = reader.GetInt32(1);
                int value3 = reader.GetInt32(2);
                string value4 = reader.GetString(3);
                string value5 = reader.GetString(4);

                texContent = texContent + @" \begin{Arabic} \question " + value5 + @" \end{Arabic} ";


                Tuple<int, int, int, string, string> question = Tuple.Create(value1, value2, value3, value4, value5);
                shortQuestions.Add(question);

            }
            reader.Close();
            command.Dispose();
            connection.Close();

            texContent = texContent + @" \end{questions}";
            return texContent;
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            groupBox4.Visible = !groupBox4.Visible;
        }
    }
}
