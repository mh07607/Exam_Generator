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

namespace Khidmat_UI
{
    public partial class NewExam : Form
    {
        public NewExam()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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

            // Specify the output directory for the TeX file
            string outputDirectory = @"C:\Users\Arsalan\Downloads";

            // Specify the name of the TeX file (including the .tex extension)
            string outputFileName = "exam.tex";

            // Combine the output directory and file name to create the full file path
            string outputPath = Path.Combine(outputDirectory, outputFileName);

            try
            {
                // Write the TeX content to the file
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

        private string GenerateMCQTex()
        {

        }

        private string GenerateShortTex()
        {

        }

        private string GenerateLongTex()
        {

        }
    }
}
