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
    public partial class Result : Form
    {
        public Result()
        {
            InitializeComponent();
        }

        StudentResult s = new StudentResult();

        public int level;

        //variable to store Assessment id to generate related Assessment Component
        public static int aid;

        //variable to store AssessmentComponent id 
        public static int cid;

        //variable to store Rubric id 
        public static int rid;

        //variable to store ontained marks
        public float ObtainedMarks;

        //variable to store component total marks to calculate obtained marks
        public int TotalMarks;

        /// <summary>
        /// Show student's registration number and show assessment's list in combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Result_Load(object sender, EventArgs e)
        {
            //hide the specific buttons
            label5.Hide();
            comboBoxCom.Hide();
            btnCom.Hide();
            label6.Hide();
            comboBoxLevel.Hide();
            btnLevel.Hide();
            btnResult.Hide();

            //store student id instudentResult's attribute
            s.StudentId = Convert.ToInt32(ActiveStudent.id);

            //getting values from Student table in database
            SqlDataReader student = DataConnection.get_instance().Getdata("SELECT * FROM Student WHERE Status=5 ");

            //Getting student's registration number from id
            while (student.Read())
            {
                if (student[0].ToString() == ActiveStudent.id)
                {
                    lblRegNo.Text = student[5].ToString();
                }
            }

            //Show all assessments Title in combo box
            string cmd1 = "SELECT * FROM Assessment";
            SqlDataReader reader1 = DataConnection.get_instance().Getdata(cmd1);
            List<string> rname = new List<string>();
            while (reader1.Read())
            {
                rname.Add(reader1.GetString(1));
            }

            comboBoxAss.DataSource = rname;
            comboBoxAss.Text = "";

        }

        /// <summary>
        /// show assessment component's list in comboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAss_Click(object sender, EventArgs e)
        {
            if(comboBoxAss.Text == "" )
            {
                MessageBox.Show("Select Assessment First!!!");
            }
            else
            {
                //show assessment component's button and combo box
                label5.Show();
                comboBoxCom.Show();
                btnCom.Show();

                //getting values from Assessment table in database
                SqlDataReader ass = DataConnection.get_instance().Getdata("SELECT * FROM Assessment");

                //check the values in combobox with Assessment table Title column and assign specific id accordingly
                while (ass.Read())
                {
                    if (ass[1].ToString() == comboBoxAss.Text)
                    {
                        aid = Convert.ToInt32(ass[0]);
                    }
                }

                //Show all assessment component's Name in combo box
                string cmd = string.Format("SELECT * FROM AssessmentComponent WHERE AssessmentId='{0}'", aid);
                SqlDataReader reader = DataConnection.get_instance().Getdata(cmd);
                List<string> rname = new List<string>();
                while (reader.Read())
                {
                    rname.Add(reader.GetString(1));
                }

                comboBoxCom.DataSource = rname;
                comboBoxCom.Text = "";
            }
        }

        /// <summary>
        /// show rubric measurement list in comboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCom_Click(object sender, EventArgs e)
        {
            if (comboBoxAss.Text == "")
            {
                MessageBox.Show("Select Assessment First!!!");
                comboBoxCom.Text = "";
            }
            else if (comboBoxCom.Text == "")
            {
                MessageBox.Show("Select Assessment component First!!!");
            }
            else
            {
                //show rubric level button and comboBox
                label6.Show();
                comboBoxLevel.Show();
                btnLevel.Show();
                
                //getting values from Assessment table in database
                SqlDataReader ass = DataConnection.get_instance().Getdata("SELECT * FROM AssessmentComponent");

                //check the values in combobox with Assessment table Title column and assign specific id accordingly
                while (ass.Read())
                {
                    if (ass[1].ToString() == comboBoxCom.Text)
                    {
                        s.AssessmentComponentId = Convert.ToInt32(ass[0]);
                        rid= Convert.ToInt32(ass[2]);
                        cid = Convert.ToInt32(ass[0]);
                        TotalMarks = Convert.ToInt32(ass[3]);
                    }
                }

                string cmd3 = string.Format("SELECT * FROM RubricLevel WHERE RubricId='{0}'", rid);
                SqlDataReader reader3 = DataConnection.get_instance().Getdata(cmd3);
                List<int> rname = new List<int>();
                while (reader3.Read())
                {
                    rname.Add(Convert.ToInt32(reader3.GetValue(3)));
                }

                comboBoxLevel.DataSource = rname;
                comboBoxLevel.Text = "";
            }
        }

        /// <summary>
        /// store the level of rubric and calculate obtained marks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLevel_Click(object sender, EventArgs e)
        {
            if (comboBoxAss.Text == "")
            {
                MessageBox.Show("Select Assessment First!!!");
            }
            else if (comboBoxCom.Text == "")
            {
                MessageBox.Show("Select Assessment Component First!!!");
            }
            else if (comboBoxLevel.Text == "")
            {
                MessageBox.Show("Select Level First!!!");
            }
            else
            {
                btnResult.Show();
                level = Convert.ToInt32(comboBoxLevel.Text);

                //store Rubric measurement level id
                string cmd3 = string.Format("SELECT * FROM RubricLevel WHERE RubricId='{0}'", rid);
                SqlDataReader reader3 = DataConnection.get_instance().Getdata(cmd3);
                
                while (reader3.Read())
                {
                    if (level.ToString() == reader3[3].ToString())
                    {
                        s.RubricMeasurementId = Convert.ToInt32(reader3[0]);
                    }
                }

                //calculate obtained marks
                ObtainedMarks = (float)(level * TotalMarks)/4;

            }
        }

        /// <summary>
        /// show Home page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            MainPage m = new MainPage();
            this.Hide();
            m.Show();
        }

        /// <summary>
        /// show Assessment's list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            ViewAssessment m = new ViewAssessment();
            this.Hide();
            m.Show();
        }

        /// <summary>
        /// insert values in student result table and show result in dataGridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnResult_Click(object sender, EventArgs e)
        {
            try
            {
                s.EvaluationDate = DateTime.Now.Date;

                //inserting the studentResult in database
                SqlConnection conn = new SqlConnection("Data Source=HAIER-PC;Initial Catalog=ProjectB;Integrated Security=True");
                SqlCommand cmd = new SqlCommand("INSERT StudentResult(StudentId,AssessmentComponentId,RubricMeasurementId,EvaluationDate) VALUES (@sId,@acId,@mId,@eDate)", conn);
                cmd.Parameters.AddWithValue("@sId", s.StudentId);
                cmd.Parameters.AddWithValue("@acId", s.AssessmentComponentId);
                cmd.Parameters.AddWithValue("@mId", s.RubricMeasurementId);
                cmd.Parameters.AddWithValue("@eDate", s.EvaluationDate);
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student Result added successfully");

                //show result in dataGridView
                SqlDataReader reader = DataConnection.get_instance().Getdata(string.Format("SELECT AssessmentComponent.Name AS Component , Rubric.Details AS Rubric , AssessmentComponent.TotalMarks AS ComponentMarks , '" + level + "' AS StudentRubricLevel , '" + ObtainedMarks + "' AS ObtainedMarks FROM AssessmentComponent JOIN Rubric ON AssessmentComponent.RubricId = Rubric.Id WHERE AssessmentComponent.Id='{0}'", cid));
                BindingSource s1 = new BindingSource();
                s1.DataSource = reader;
                dataGridViewrub.DataSource = s1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
