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
    public partial class AssessmentComponent : Form
    {
        public AssessmentComponent()
        {
            InitializeComponent();
        }

        //variable to check whether user want to edit the component or add a new one 
        bool n = false;

        //new object of Assessment component type
        AssessComponent r = new AssessComponent();

        //buttons to be added in dataGridView
        DataGridViewButtonColumn buttonE = new DataGridViewButtonColumn();
        DataGridViewButtonColumn buttonD = new DataGridViewButtonColumn();

        //variable to store Assessment Component's id
        public static string cid;

        /// <summary>
        /// show rubric's Detail in comboBox and show Assessment Component's list in dataGridView on form load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssessmentComponent_Load(object sender, EventArgs e)
        {
            //Show rubric details in combobox
            string cmd1 = "SELECT * FROM Rubric";
            SqlDataReader reader1 = DataConnection.get_instance().Getdata(cmd1);
            List<string> rname = new List<string>();
            while (reader1.Read())
            {
                rname.Add(reader1.GetString(1));
            }

            txtSStatus.DataSource = rname;
            txtSStatus.Text = "";

            //showing list of Assessment components in dataGridView on form load
            string cmd = string.Format("SELECT * FROM AssessmentComponent WHERE AssessmentId='{0}'", ViewAssessment.aid);
            SqlDataReader reader = DataConnection.get_instance().Getdata(cmd);
            //creating new list of components and storing data from database in it
            List<AssessComponent> rub = new List<AssessComponent>();
            while (reader.Read())
            {
                AssessComponent ru = new AssessComponent();
                ru.Id = Convert.ToInt32(reader.GetValue(0));
                ru.Name = reader.GetString(1);
                ru.RubricId = Convert.ToInt32(reader.GetValue(2));
                ru.TotalMarks = Convert.ToInt32(reader.GetValue(3));
                ru.DateCreated = reader.GetDateTime(4);
                ru.DateUpdated = reader.GetDateTime(5);
                ru.AssessmentId = Convert.ToInt32(reader.GetValue(6));
                rub.Add(ru);
            }
            //creating buttons of edit and delete in dataGridView on runtime
            buttonE.Name = "btnEdit";
            buttonE.Text = "EDIT";
            buttonE.UseColumnTextForButtonValue = true;
            buttonD.Name = "btnDelete";
            buttonD.Text = "DELETE";
            buttonD.UseColumnTextForButtonValue = true;
            BindingSource s = new BindingSource();
            s.DataSource = rub;
            dataGridViewrub.DataSource = s;
            dataGridViewrub.Columns.Add(buttonE);
            dataGridViewrub.Columns.Add(buttonD);

        }

        /// <summary>
        /// Edit an Assessment Component if the id is passed or Add otherwise
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //validation that boxes should not be empty
                if (txtDetails.Text == "" || txtMLevel.Text == "" || txtSStatus.Text == "")
                {
                    MessageBox.Show("Boxes should not be empty");
                }
                //Adding new component
                else if (!n)
                {
                    r.Name = txtDetails.Text;

                    //getting values from rubric table in database
                    SqlDataReader rubric = DataConnection.get_instance().Getdata("SELECT * FROM Rubric");

                    //check the values in textbox with rubric table details column and assign specific id accordingly
                    while (rubric.Read())
                    {
                        if (rubric[1].ToString() == txtSStatus.Text)
                        {
                            r.RubricId = Convert.ToInt32(rubric[0]);
                        }
                    }

                    r.TotalMarks = Convert.ToInt32(txtMLevel.Text);
                    r.DateCreated = DateTime.Now.Date;
                    r.DateUpdated = DateTime.Now.Date;
                    r.AssessmentId = Convert.ToInt32(ViewAssessment.aid);

                    //making connection and adding Assessment Component's attributes in the Assessment Component table in database
                    SqlConnection conn = new SqlConnection("Data Source=HAIER-PC;Initial Catalog=ProjectB;Integrated Security=True");
                    SqlCommand cmd = new SqlCommand("INSERT INTO AssessmentComponent(Name,RubricId,TotalMarks,DateCreated,DateUpdated,AssessmentId) VALUES (@name,@rubricId,@totalMarks,@dateCreated,@dateUpdated,@assessmentId)", conn);
                    cmd.Parameters.AddWithValue("@name", r.Name);
                    cmd.Parameters.AddWithValue("@rubricId", r.RubricId);
                    cmd.Parameters.AddWithValue("@totalMarks", r.TotalMarks);
                    cmd.Parameters.AddWithValue("@dateCreated", r.DateCreated);
                    cmd.Parameters.AddWithValue("@dateUpdated", r.DateUpdated);
                    cmd.Parameters.AddWithValue("@assessmentId", r.AssessmentId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    AssessmentComponent frm = new AssessmentComponent();
                    this.Hide();
                    frm.Show();
                }
                //Editing component
                else
                {
                    r.Name = txtDetails.Text;

                    //getting values from rubric table in database
                    SqlDataReader rubric = DataConnection.get_instance().Getdata("SELECT * FROM Rubric");

                    //check the values in textbox with rubric table details column and assign specific id accordingly
                    while (rubric.Read())
                    {
                        if (rubric[1].ToString() == txtSStatus.Text)
                        {
                            r.RubricId = Convert.ToInt32(rubric[0]);
                        }
                    }

                    r.TotalMarks = Convert.ToInt32(txtMLevel.Text);
                    r.DateUpdated = DateTime.Now.Date;
                    r.AssessmentId = Convert.ToInt32(ViewAssessment.aid);

                    //updating the values in Assessment Component Table in database
                    SqlConnection conn = new SqlConnection("Data Source=HAIER-PC;Initial Catalog=ProjectB;Integrated Security=True");
                    SqlCommand cmd = new SqlCommand("UPDATE AssessmentComponent SET Name=@name ,RubricId=@rid,TotalMarks=@tmarks, DateUpdated=@dateUpdated,AssessmentId=@assCom WHERE Id=@id", conn);
                    cmd.Parameters.AddWithValue("@name", r.Name);
                    cmd.Parameters.AddWithValue("@rid", r.RubricId);
                    cmd.Parameters.AddWithValue("@tmarks", r.TotalMarks);
                    cmd.Parameters.AddWithValue("@dateUpdated", r.DateUpdated);
                    cmd.Parameters.AddWithValue("@assCom", r.AssessmentId);
                    cmd.Parameters.AddWithValue("@id", cid);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Assessment Component Edited Successfully!");

                    //show assessment component form
                    this.Hide();
                    AssessmentComponent vs = new AssessmentComponent();
                    vs.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Edit and Delete Assessment Component when specific buttons are clicked in DataGridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewrub_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //When edit button is clicked in dataGridView then details to be edited are shown in the textbox
                if (dataGridViewrub.Columns[e.ColumnIndex].Name == "btnEdit")
                {
                    DataGridViewRow edit = dataGridViewrub.Rows[e.RowIndex];
                    cid = edit.Cells[0].Value.ToString();
                    string det = edit.Cells[1].Value.ToString();
                    string tm = edit.Cells[3].Value.ToString();

                    //getting values from rubric table in database
                    SqlDataReader rubric = DataConnection.get_instance().Getdata("SELECT * FROM Rubric");

                    //check the values in textbox with rubric table details column and assign specific id accordingly
                    while (rubric.Read())
                    {
                        if (r.RubricId.ToString() == rubric[0].ToString())
                        {
                            txtSStatus.Text = rubric[1].ToString();
                        }
                    }

                    txtDetails.Text = det;
                    txtMLevel.Text = tm;
                    n = true;
                }
                //Deleting Assessment Component when delete button is clicked in DataGridView
                if (dataGridViewrub.Columns[e.ColumnIndex].Name == "btnDelete")
                {
                    DataGridViewRow delete = dataGridViewrub.Rows[e.RowIndex];
                    cid = delete.Cells[0].Value.ToString();
                    MessageBox.Show("Are you sure you want to Permanently delete the specific Component Its related Student Result will also be deleted?");

                    //Deleting Student Result realted to the Assessment Component to be deleted
                    SqlDataReader related = DataConnection.get_instance().Getdata(string.Format("SELECT * FROM StudentResult WHERE AssessmentComponentId={0}", cid));
                    if (related != null)
                    {
                        while (related.Read())
                        {
                            int r;
                            r = Convert.ToInt32(related.GetValue(1));
                            if (r.ToString() == cid.ToString())
                            {
                                string cmd2 = string.Format("DELETE FROM StudentResult WHERE AssessmentComponentId='{0}'", r);
                                int rows2 = DataConnection.get_instance().Executequery(cmd2);
                                MessageBox.Show(String.Format("{0} rows affected", rows2));
                                MessageBox.Show("Related Student Result Deleted");

                            }
                        }
                    }

                    string cmd = string.Format("DELETE FROM AssessmentComponent WHERE Id='{0}'", Convert.ToInt32(cid));
                    int row3 = DataConnection.get_instance().Executequery(cmd);
                    MessageBox.Show(String.Format("{0} rows affected", row3));
                    MessageBox.Show("Assessment Component Deleted");


                    AssessmentComponent v = new AssessmentComponent();
                    this.Hide();
                    v.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// show assessment form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            ViewAssessment a = new ViewAssessment();
            this.Hide();
            a.Show();
        }

        /// <summary>
        /// show home page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            MainPage a = new MainPage();
            this.Hide();
            a.Show();
        }
    }
}
