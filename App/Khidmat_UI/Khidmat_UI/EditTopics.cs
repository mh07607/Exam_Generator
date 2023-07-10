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
using System.Data.SqlClient;

namespace Khidmat_UI
{
    public partial class EditTopics : Form
    {
        const string connectionString = @"Data Source=DESKTOP-PEGIUMG\YEET; Initial Catalog = khidmat_test; Integrated Security = True";
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand command = new SqlCommand();
        public EditTopics()
        {
            InitializeComponent();
        }

        private void EditTopics_Load(object sender, EventArgs e)
        {
            connection.Open();
            string sql = "SELECT * FROM TOPIC";
            command = new SqlCommand(sql, connection);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            command.Dispose();
            connection.Close();
        }
    }
}
