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
    public partial class ManageRubric : Form
    {
        public ManageRubric()
        {
            InitializeComponent();
        }

        //variable to check whether user want to edit the rubric or add a new one 
        bool n = false;

        //new object of rubric
        Rubric r = new Rubric();

        //buttons to be added in dataGridView
        DataGridViewButtonColumn buttonE = new DataGridViewButtonColumn();
        DataGridViewButtonColumn buttonD = new DataGridViewButtonColumn();
        DataGridViewButtonColumn buttonR = new DataGridViewButtonColumn();

        //variable to store rubric id to be used in rubric level form
        public static string rid ;

        /// <summary>
        /// view list of rubrics in dataGridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManageRubric_Load(object sender, EventArgs e)
        {
            //showing list of rubrics in dataGridView on form load
            string cmd = string.Format("SELECT * FROM Rubric WHERE CloId='{0}'", ViewCLO.cid);
            SqlDataReader reader = DataConnection.get_instance().Getdata(cmd);
            //creating new list of rubrics and storing data from daabase in it
            List<Rubric> rub = new List<Rubric>();
            while (reader.Read())
            {
                Rubric ru = new Rubric();
                ru.Id = Convert.ToInt32(reader.GetValue(0));
                ru.Details = reader.GetString(1);
                ru.CloId = Convert.ToInt32(reader.GetValue(2));
                rub.Add(ru);
            }
            //creating buttons of edit, delete and Manage Rubric Level in dataGridView on runtime
            buttonE.Name = "btnEdit";
            buttonE.Text = "EDIT";
            buttonE.UseColumnTextForButtonValue = true;
            buttonD.Name = "btnDelete";
            buttonD.Text = "DELETE";
            buttonD.UseColumnTextForButtonValue = true;
            buttonR.Name = "btnRubric";
            buttonR.Text = "RUBRIC LEVEL";
            buttonR.UseColumnTextForButtonValue = true;
            BindingSource s = new BindingSource();
            s.DataSource = rub;
            dataGridViewrub.DataSource = s;
            dataGridViewrub.Columns.Add(buttonE);
            dataGridViewrub.Columns.Add(buttonD);
            dataGridViewrub.Columns.Add(buttonR);
        }

        /// <summary>
        /// Add or edit the rubric
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (richTextBox1.Text == "")
                {
                    MessageBox.Show("Details Box should not be empty");
                }
                //Adding new Rubric
                else if (!n)
                {
                    r.Details = richTextBox1.Text;
                    r.CloId = Convert.ToInt32(ViewCLO.cid);

                    string cmd = string.Format("INSERT Rubric(Details , CloId) VALUES('{0}','{1}')", r.Details, r.CloId);
                    int rows = DataConnection.get_instance().Executequery(cmd);
                    MessageBox.Show(String.Format("{0} rows affected", rows));

                    ManageRubric frm = new ManageRubric();
                    this.Hide();
                    frm.Show();
                }
                //Editing Rubric
                else
                {
                    r.Details = richTextBox1.Text;

                    string cmd = string.Format("UPDATE Rubric SET Details='{0}', CloId ='{1}' WHERE Id='{2}'", r.Details, ViewCLO.cid, rid);
                    int rows = DataConnection.get_instance().Executequery(cmd);
                    MessageBox.Show(String.Format("{0} rows affected", rows));
                    MessageBox.Show("Rubric Edited Successfully!");
                    this.Hide();
                    ManageRubric vs = new ManageRubric();
                    vs.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// Do specific tasks on click of specific buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewrub_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //When edit button is clicked in dataGridView then detailes to be edited are shown in the textbox
                if (dataGridViewrub.Columns[e.ColumnIndex].Name == "btnEdit")
                {
                    DataGridViewRow edit = dataGridViewrub.Rows[e.RowIndex];
                    rid = edit.Cells[0].Value.ToString();
                    string det = edit.Cells[1].Value.ToString();
                    richTextBox1.Text = det;
                    n = true;
                }
                //Deleting Rubric when delete button is clicked in DataGridView
                if (dataGridViewrub.Columns[e.ColumnIndex].Name == "btnDelete")
                {
                    int c;
                    DataGridViewRow delete = dataGridViewrub.Rows[e.RowIndex];
                    rid = delete.Cells[0].Value.ToString();
                    MessageBox.Show("Are you sure you want to Permanently delete the specific Rubric Its related Components,levels and their related result will also be deleted?");

                    // Deleting assessment component related to that specific rubric
                    SqlDataReader dataAs = DataConnection.get_instance().Getdata(string.Format("SELECT * FROM AssessmentComponent WHERE RubricId={0}", rid));
                    if (dataAs != null)
                    {
                        while (dataAs.Read())
                        {
                            c = Convert.ToInt32(dataAs.GetValue(0));
                            SqlDataReader dataSR = DataConnection.get_instance().Getdata(string.Format("SELECT * FROM StudentResult WHERE AssessmentComponentId={0}", c));
                            if (dataSR != null)
                            {
                                while (dataSR.Read())
                                {

                                    //Deleting Student Result related to that assessment component
                                    string cmd = string.Format("DELETE FROM StudentResult WHERE AssessmentComponentId='{0}'", c);
                                    DataConnection.get_instance().Executequery(cmd);
                                    MessageBox.Show("Related student result deleted");

                                }

                            }
                            //Deleting Assessment Component related to the rubric
                            string cmd1 = string.Format("DELETE FROM AssessmentComponent WHERE RubricId='{0}'", rid);
                            DataConnection.get_instance().Executequery(cmd1);
                            MessageBox.Show("Related Assessment Component deleted");
                        }
                    }

                    //Deleting Rubric level realted to the Rubric to be deleted
                    SqlDataReader related = DataConnection.get_instance().Getdata(string.Format("SELECT * FROM RubricLevel WHERE RubricId={0}", rid));
                    if (related != null)
                    {
                        while (related.Read())
                        {
                            int r;
                            r = Convert.ToInt32(related.GetValue(1));
                            if (r.ToString() == rid.ToString())
                            {
                                string cmd2 = string.Format("DELETE FROM RubricLevel WHERE RubricId='{0}'", r);
                                int rows2 = DataConnection.get_instance().Executequery(cmd2);
                                MessageBox.Show(String.Format("{0} rows affected", rows2));
                                MessageBox.Show("Related Rubric Levels Deleted");

                            }
                        }
                    }

                    string cmd3 = string.Format("DELETE FROM Rubric WHERE Id='{0}'", Convert.ToInt32(rid));
                    int row3 = DataConnection.get_instance().Executequery(cmd3);
                    MessageBox.Show(String.Format("{0} rows affected", row3));
                    MessageBox.Show("Rubric Deleted");


                    ManageRubric v = new ManageRubric();
                    this.Hide();
                    v.Show();
                }
                //when rubric level button is clicked in the dataGridView Then Manage rubric level form is Opened and Rubric id is also passed to the next form
                if (dataGridViewrub.Columns[e.ColumnIndex].Name == "btnRubric")
                {
                    DataGridViewRow edit = dataGridViewrub.Rows[e.RowIndex];
                    rid = edit.Cells[0].Value.ToString();
                    ManageRubricLevels f = new ManageRubricLevels();
                    this.Hide();
                    f.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// shoe view clo form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            ViewCLO frm = new ViewCLO();
            this.Hide();
            frm.Show();
        }

        /// <summary>
        /// On exit click Home page is opened
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            MainPage s = new MainPage();
            this.Hide();
            s.Show();
        }
    }
}
