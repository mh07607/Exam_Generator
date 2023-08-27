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
//using System.Reflection.Metadata;
using System.Diagnostics;
using Xceed.Words.NET;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.IO;
using Xceed.Document.NET;
using System.Xml.Linq;

namespace Khidmat_Project
{
    public partial class NewExam : Form
    {
        List<(int, TextBox, TextBox, TextBox)> ShortQuestions = new List<(int, TextBox, TextBox, TextBox)>();
        List<(int, TextBox, TextBox, TextBox)> LongQuestions = new List<(int, TextBox, TextBox, TextBox)>();
        List<(int, TextBox, TextBox, TextBox)> MCQs = new List<(int, TextBox, TextBox, TextBox)>();
        int subjectId;


        SqlConnection connection = new SqlConnection(connectDb.connectionString);
        SqlCommand command = new SqlCommand();

        public NewExam(int subjectId)
        {
            InitializeComponent();
            this.subjectId = subjectId;
        }

        private int dynamicallyGenerateTextboxes(System.Windows.Forms.GroupBox groupBox, List<(int, TextBox, TextBox, TextBox)> questions)
        {
            List<(int, string)> topicList = GetTopics(subjectId);
            int i;
            for (i = 0; i < topicList.Count; i++)
            {
                Label topicName = new Label();

                topicName.Text = topicList[i].Item2;

                TextBox numEasyShort = new TextBox();
                TextBox numMediumShort = new TextBox();
                TextBox numHardShort = new TextBox();

                topicName.Location = new System.Drawing.Point(65, 55 + (i * 30));
                numEasyShort.Location = new System.Drawing.Point(264, 50 + (i * 30));
                numMediumShort.Location = new System.Drawing.Point(350, 50 + (i * 30));
                numHardShort.Location = new System.Drawing.Point(436, 50 + (i * 30));

                topicName.AutoSize = true;

                numEasyShort.Width = numMediumShort.Width = numHardShort.Width = 40;
                numEasyShort.Height = numMediumShort.Height = numHardShort.Height = 25;

                groupBox.Controls.Add(topicName);
                groupBox.Controls.Add(numEasyShort);
                groupBox.Controls.Add(numMediumShort);
                groupBox.Controls.Add(numHardShort);

                (int topicId, TextBox easyShort, TextBox mediumShort, TextBox hardShort) data = (topicList[i].Item1, numEasyShort, numMediumShort, numHardShort);
                questions.Add(data);
            }

            return i;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            int offset = dynamicallyGenerateTextboxes(groupBox4, MCQs);
            groupBox5.Location = new System.Drawing.Point(groupBox5.Location.X, (offset) * 30 + groupBox5.Location.Y);
            button2.Location = new System.Drawing.Point(button2.Location.X, (offset) * 30 + button2.Location.Y);
            groupBox6.Location = new System.Drawing.Point(groupBox6.Location.X, (offset) * 60 + groupBox6.Location.Y);
            button3.Location = new System.Drawing.Point(button2.Location.X, (offset) * 60 + button3.Location.Y);

            dynamicallyGenerateTextboxes(groupBox5, ShortQuestions);
            dynamicallyGenerateTextboxes(groupBox6, LongQuestions);

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
            //pc
            //string docxFilePath = @"C:\Users\pc\Downloads\exams\exam-template.docx"; // Path for the Word document

            //laptop
            string docxFilePath = @"C:\Users\Arsalan\Downloads\exams\exam-template.docx"; // Path for the Word document

            using (DocX doc = DocX.Load(docxFilePath))
            {
               InsertMCQInDocx(doc);

               using (SaveFileDialog saveFileDialog = new SaveFileDialog())
               {
                    saveFileDialog.Filter = "Word Document|*.docx";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var savePath = saveFileDialog.FileName;
                        doc.SaveAs(savePath);
                    }
                    else
                    {
                        Console.WriteLine("User canceled the save operation.");
                    }
               }
            }
        }

        private void GenerateLatexPdf()
        {
            //string texContent = GenerateTexContent();
            //string outputDirectory = @"C:\Users\Arsalan\Downloads\Exam";
            //string outputFileName = "exam.tex";
            //string pdfFileName = "exam.pdf";
            //string outputPath = Path.Combine(outputDirectory, outputFileName);
            //string pdfPath = Path.Combine(outputDirectory, pdfFileName);

            /*File.WriteAllText(outputPath, texContent);

            // Run xelatex command to compile the .tex file to PDF.
            ProcessStartInfo processInfo = new ProcessStartInfo
            {
                FileName = "xelatex",
                Arguments = $"-output-directory={outputDirectory} {outputPath}",
                UseShellExecute = false,
                RedirectStandardError = true,
                CreateNoWindow = false,
                WorkingDirectory = outputDirectory  // Set the working directory to the .tex file location.
            };

            Process process = new Process
            {
                StartInfo = processInfo
            };

            process.Start();
            process.WaitForExit();

            // Check if the PDF was successfully generated.
            if (File.Exists(pdfPath))
            {
                // Optionally, you can open the generated PDF file using the default PDF viewer.
                //Process.Start(pdfPath);
            }
            else
            {
                string errorOutput = process.StandardError.ReadToEnd();
                // Handle the error if the PDF was not generated.
                // You can display the error message to the user or log it for further investigation.
            }*/

        }

        private void InsertMCQInDocx(DocX doc)
        {
            var placeholder = "{mcqs}";
            var paragraph = doc.Paragraphs.FirstOrDefault(p => p.Text.Contains(placeholder));

            if(paragraph == null) 
            {
                MessageBox.Show("Your template is faulty, there is no '{mcqs}' placeholder to place the MCQs.");
                return;
            }

            if (MCQs.Count == 0)
            {
                return;
            }

            //doc.Paragraphs.Append()
            

            List<int> MCQIDs = new List<int>(); //should use this list to push these ids in past paper_mcqs
            var mcqList = doc.AddList();

            for (int i = 0; i < MCQs.Count; i++)
            {
                string topic = MCQs[i].Item1.ToString();
                string numEasy = MCQs[i].Item2.Text;
                string numMedium = MCQs[i].Item3.Text;
                string numHard = MCQs[i].Item4.Text;

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

                connection.Open();
                string query = "EXEC GetRandomMCQs " + numEasy + "," + numMedium + "," + numHard + "," + topic;
                command = new SqlCommand(query, connection);
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
            }

            var index = doc.Paragraphs.IndexOf(paragraph);
            doc.InsertList(index, mcqList);
            //doc.Paragraphs.Remove();
        }

        private List<int> GenerateShortDocx(DocX doc)
        {
            string placeholder = "{short}";
            var paragraph = doc.Paragraphs.FirstOrDefault(p => p.Text.Contains(placeholder));

            if (paragraph == null)
            {
                MessageBox.Show("Your template is faulty, there is no '{mcqs}' placeholder to place the MCQs.");
                return null;
            }

            if (MCQs.Count == 0)
            {
                return null;
            }


            List<int> shortIDs = new List<int>(); //should use this list to push these ids in past paper_mcqs
            Formatting format = new Formatting();
            var mcqList = doc.AddList(formatting: new Formatting());

            for (int i = 0; i < MCQs.Count; i++)
            {
                string topic = ShortQuestions[i].Item1.ToString();
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

                connection.Open();
                string query = "EXEC GetRandomMCQs " + numEasy + "," + numMedium + "," + numHard + "," + topic;
                command = new SqlCommand(query, connection);
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
            }

            var index = doc.Paragraphs.IndexOf(paragraph);
            doc.InsertList(index, mcqList);
            //doc.Paragraphs.Remove();
            return shortIDs;
        }
    

        private string GenerateLongDocx()
        {
            string docxContent = "";
            return docxContent;
        }

        private string GenerateTexContent()
        {
            string texContent = @"\documentclass{exam}
\usepackage{polyglossia}
\usepackage{fontspec}
\usepackage{titlesec}
\usepackage{bidi}
\setmainlanguage{english}
\setotherlanguage{arabic}
\newfontfamily\arabicfont[Script=Arabic]{Amiri}

% Redefine section headings to use Arabic font
\makeatletter
\renewcommand{\@seccntformat}[1]{\protect\RTL\protect\textbf{\csname the#1\endcsname\quad}}
\makeatother

\titleformat*{\section}{\Large\bfseries\arabicfont}
\titleformat*{\subsection}{\large\bfseries\arabicfont}


\begin{document}
\begin{RTL} ";
            texContent = texContent + GenerateMCQTex();
            texContent = texContent + GenerateShortTex();
            texContent = texContent + GenerateLongTex();
            texContent = texContent + @" \end{RTL}
\end{document}";
            return texContent;
        }


        private List<(int, string)> GetTopics(int subjectId)
        {
            List<(int, string)> topicList = new List<(int, string)>();
            connection.Open();
            string query = "select * from Topic where SubjectId = " + subjectId;
            command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int topicId = Convert.ToInt32(reader["TopicId"]);
                string topicName = reader["TopicName"].ToString();
                //Tuple<int, string> topicInfo = (topicId, topicName);
                topicList.Add((topicId, topicName));
            }
            reader.Close();
            command.Dispose();
            connection.Close();
            return topicList;
        }


        private string GenerateMCQTex()
        {
            if (MCQs.Count == 0)
            {
                return "";
            }
            string texContent = @" \section{ایم سی کیو}
    \begin{questions} ";
            List<int> MCQIDs = new List<int>();
            for (int i = 0; i < MCQs.Count; i++)
            {
                string topic = MCQs[i].Item1.ToString();
                string numEasy = MCQs[i].Item2.Text;
                string numMedium = MCQs[i].Item3.Text;
                string numHard = MCQs[i].Item4.Text;
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
                connection.Open();
                string query = "EXEC GetRandomMCQs " + numEasy + "," + numMedium + "," + numHard + "," + topic;
                command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int value1 = reader.GetInt32(0); //MCQID
                    string value2 = reader.GetString(1); //Content
                    string value3 = reader.GetString(4); //Option A
                    string value4 = reader.GetString(5); //Option B
                    string value5 = reader.GetString(6); //Option C
                    string value6 = reader.GetString(7); //Option D

                    texContent = texContent + @"\begin{Arabic} \question " + value2 + @" \begin{choices} \choice " + value3 + @" \choice " + value4 + @" \choice " + value5 + @" \choice  " + value6 + @" \end{choices} \end{Arabic} ";
                    MCQIDs.Add(value1);
                }
                reader.Close();
                command.Dispose();
                connection.Close();
            }
            texContent = texContent + @" \end{questions}";
            return texContent;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }


        private string GenerateShortTex()
        {
            if (ShortQuestions.Count == 0)
            {
                return "";
            }
            string texContent = @" \section{مختصر سوالات}
    \begin{questions} ";
            List<int> shortQuestionIDs = new List<int>();
            for (int i = 0; i < ShortQuestions.Count; i++)
            {
                string topic = ShortQuestions[i].Item1.ToString();
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
                connection.Open();
                string query = "EXEC GetRandomQuestions " + numEasy + "," + numMedium + "," + numHard + "," + "short" + "," + topic;
                command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int value1 = reader.GetInt32(0); //QuestionID
                    string value5 = reader.GetString(4); //Content

                    texContent = texContent + @" \begin{Arabic} \question " + value5 + @" \end{Arabic} ";
                    shortQuestionIDs.Add(value1);
                }
                reader.Close();
                command.Dispose();
                connection.Close(); //Here readrer is being opened and closed for each topic would that be a problem?*
            }
            texContent = texContent + @" \end{questions}";
            return texContent;
        }


        private string GenerateLongTex()
        {
            if (LongQuestions.Count == 0)
            {
                return "";
            }
            string texContent = @" \section{طویل سوالات}
\begin{questions} ";
            List<int> longQuestionIDs = new List<int>();
            for (int i = 0; i < LongQuestions.Count; i++)
            {
                string topic = LongQuestions[i].Item1.ToString();
                string numEasy = LongQuestions[i].Item2.Text;
                string numMedium = LongQuestions[i].Item3.Text;
                string numHard = LongQuestions[i].Item4.Text;
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

                connection.Open();
                string query = "EXEC GetRandomQuestions " + numEasy + "," + numMedium + "," + numHard + "," + "long" + "," + topic;
                command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int value1 = reader.GetInt32(0); //QuestionID
                    string value5 = reader.GetString(4); //Content

                    texContent = texContent + @" \begin{Arabic} \question " + value5 + @" \end{Arabic} ";
                    longQuestionIDs.Add(value1);
                }
                reader.Close();
                command.Dispose();
                connection.Close(); //Here readrer is being opened and closed for each topic would that be a problem?*
            }
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
        private void button2_Click_1(object sender, EventArgs e)
        {
            groupBox5.Visible = !groupBox5.Visible;
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            groupBox6.Visible = !groupBox6.Visible;
        }
    }
}

