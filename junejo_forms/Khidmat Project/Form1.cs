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
    public partial class Form1 : Form
    {
        SqlConnection connection = new SqlConnection(connectDb.connectionString);
        SqlCommand command = new SqlCommand();

        public Form1()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            
        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Please enter your username!");
                return;
            }
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please enter your password!");
                return;
            }

            connection.Open();
            string query = "SELECT * FROM Admin WHERE Email = @email AND Password = @password";
            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@email", textBox2.Text);
            command.Parameters.AddWithValue("@password", textBox1.Text);

            if (command.ExecuteScalar() != null)
            {
                // Successful authentication
                Form2 form2 = new Form2();
                form2.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid credentials. Please try again.");
            }

            command.Dispose();
            connection.Close();
        }
    }
}
