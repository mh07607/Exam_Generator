using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Khidmat_Project
{
    public partial class Form13 : Form
    {
        int topicId;

        public Form13(int topicId)
        {
            InitializeComponent();
            this.topicId = topicId;
        }

        public void Form13_Load(object sender, EventArgs e)
        {
            //Populating all the feilds based on the topicId

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form11 form11 = new Form11();
            form11.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e) //Edit Button
        {
            Form11 form11 = new Form11();
            form11.Show();
            this.Hide();
        }
    }
}
