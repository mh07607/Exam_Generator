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
    public partial class Form7 : Form
    {
        SqlConnection connection = new SqlConnection(connectDb.connectionString);
        SqlCommand command = new SqlCommand();
        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            Search();
        }

        private void Search()
        {
            connection.Open();

            string query = "SELECT SubjectId, SubjectName FROM Subject WHERE 1=1 ";
            if(textBox1.Text.Length > 0)
            {
                query += @" AND SubjectName LIKE '%' + @subjectname + '%'";
            }

            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@subjectname", textBox1.Text);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            command.Dispose();
            connection.Close();
            dataGridView1.DataSource = dataTable;
            dataGridView1.Columns[0].Visible = false;
        }

        private void UpdateSubject(int subjectId)
        {
            connection.Open();

            string query = "UPDATE Subject SET SubjectName = @subjectname WHERE SubjectId = @subjectid";
            command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@subjectname", textBox1.Text);
            command.Parameters.AddWithValue("@subjectid", subjectId);

            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();
        }

        private void DeleteSubject(int subjectId)
        {
            connection.Open();

            string query = "DELETE FROM Subject WHERE SubjectId = @subjectid";
            command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@subjectid", subjectId);

            command.ExecuteNonQuery();  

            command.Dispose();
            connection.Close();
        }

        private void InsertSubject()
        {
            connection.Open();
            string query = "INSERT INTO SUBJECT (SubjectName) VALUES (@subjectname)";
            command = new SqlCommand (query, connection);

            command.Parameters.AddWithValue("@subjectname", textBox1.Text);

            command.ExecuteNonQuery();

            command.Dispose();
            connection.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete these entries?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                    {
                        DataGridViewRow row = dataGridView1.SelectedRows[i];
                        DeleteSubject(Convert.ToInt32(row.Cells[0].Value));
                    }
                    Search();
                }
                else
                {
                    MessageBox.Show("Select an entire row to delete it!");
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length <= 0)
            {
                MessageBox.Show("Please enter subject name to add a subject!");
                return;     
            }
            InsertSubject();
            MessageBox.Show(textBox1.Text + " is now a subject!");
            Search();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("Please enter subject name to edit a subject!");
                return;
            }
            if(dataGridView1.SelectedRows.Count > 0)
            {
                if(dataGridView1.SelectedRows.Count > 1)
                {
                    MessageBox.Show("You can only edit one subject at a time!");
                    return;
                }

                UpdateSubject(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                MessageBox.Show("Subject has been updated!");
                Search();
            }
            else
            {
                MessageBox.Show("Please select entire row to edit a subject!");
                return;
            }
        }
    }
}
