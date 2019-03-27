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

namespace DBProject
{
    public partial class AddAssessment : Form
    {
        public AddAssessment()
        {
            InitializeComponent();
        }

        //making new object of assessment
        Assessment ass = new Assessment();

        /// <summary>
        /// Adding new assessment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegisterS_Click(object sender, EventArgs e)
        {
            if (txtSFname.Text == "" || txtSLname.Text == "" || txtScontact.Text == "" || txtSemail.Text == "")
            {
                MessageBox.Show("Boxes should not be empty");
            }
            else
            {
                //adding assessment by setting the attributes of assessment class equal to the values in textboxes
                ass.Title = txtSFname.Text;
                ass.DateCreated = DateTime.Now.Date;
                ass.TotalMarks = Convert.ToInt32(txtScontact.Text);
                ass.TotalWeightage = Convert.ToInt32(txtSemail.Text);

                //making connection and adding assessment attributes in the Assessment table in database
                SqlConnection conn = new SqlConnection("Data Source=HAIER-PC;Initial Catalog=ProjectB;Integrated Security=True");
                SqlCommand cmd = new SqlCommand("INSERT INTO Assessment(Title,DateCreated,TotalMarks,TotalWeightage) VALUES (@title,@dateCreated,@totalMarks,@totalWeightage)", conn);
                cmd.Parameters.AddWithValue("@title", ass.Title);
                cmd.Parameters.AddWithValue("@dateCreated", ass.DateCreated);
                cmd.Parameters.AddWithValue("@totalMarks", ass.TotalMarks);
                cmd.Parameters.AddWithValue("@totalWeightage", ass.TotalWeightage);
                conn.Open();
                cmd.ExecuteNonQuery();

                //show students list
                ViewAssessment frm = new ViewAssessment();
                this.Hide();
                frm.Show();
            }
        }

        /// <summary>
        /// show fixed datetime in date created box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddAssessment_Load(object sender, EventArgs e)
        {
            txtSLname.Text = DateTime.Now.ToString("dd.MM.yyyy  HH:mm:ss");
        }

        /// <summary>
        /// show assessment list form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewS_Click(object sender, EventArgs e)
        {
            ViewAssessment a = new ViewAssessment();
            this.Hide();
            a.Show();
        }
    }
}
