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
    public partial class EditAssessment : Form
    {
        public EditAssessment()
        {
            InitializeComponent();
        }

        //new object of assessment
        Assessment ass = new Assessment();

        /// <summary>
        /// Show data of assessment to be edited in textboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditAssessment_Load(object sender, EventArgs e)
        {
            if (ViewAssessment.aid != null)
            {
                //show the data in textboxes
                SqlDataReader data = DataConnection.get_instance().Getdata(string.Format("SELECT * FROM Assessment"));
                while (data.Read())
                {
                    Assessment ass = new Assessment();
                    ass.Id = Convert.ToInt32(data.GetValue(0));
                    if (ass.Id == Convert.ToInt32(ViewAssessment.aid))
                    {
                        txtSFname.Text = data.GetString(1);
                        txtScontact.Text = data.GetValue(3).ToString();
                        txtSemail.Text = data.GetValue(4).ToString();
                    }
                }
            }
        }

        /// <summary>
        /// edit the assessment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegisterS_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewAssessment.aid != null)
                {
                    if (txtSFname.Text == "" || txtScontact.Text == "" || txtSemail.Text == "")
                    {
                        MessageBox.Show("Boxes should not be empty");
                    }
                    else
                    {
                        ass.Title = txtSFname.Text;
                        ass.TotalMarks = Convert.ToInt32(txtScontact.Text);
                        ass.TotalWeightage = Convert.ToInt32(txtSemail.Text);

                        SqlConnection conn = new SqlConnection("Data Source=HAIER-PC;Initial Catalog=ProjectB;Integrated Security=True");
                        SqlCommand cmd = new SqlCommand("UPDATE Assessment SET Title=@title , TotalMarks=@totalMarks , TotalWeightage=@totalWeightage WHERE Id=@id", conn);
                        cmd.Parameters.AddWithValue("@title", ass.Title);
                        cmd.Parameters.AddWithValue("@totalMarks", ass.TotalMarks);
                        cmd.Parameters.AddWithValue("@totalWeightage", ass.TotalWeightage);
                        cmd.Parameters.AddWithValue("@id", ViewAssessment.aid);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Assessment Edited Successfully!");

                        //show view assessment form
                        this.Hide();
                        ViewAssessment vs = new ViewAssessment();
                        vs.Show();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// open view assessment form
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
