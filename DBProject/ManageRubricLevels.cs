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
    public partial class ManageRubricLevels : Form
    {
        public ManageRubricLevels()
        {
            InitializeComponent();
        }

        //variable to check whether to edit or add rubric level
        bool n = false;

        //new object of Rubric level type
        RubricLevel r = new RubricLevel();

        //buttons to be added in DataGridView
        DataGridViewButtonColumn buttonE = new DataGridViewButtonColumn();
        DataGridViewButtonColumn buttonD = new DataGridViewButtonColumn();

        //attribute to store rubric level id
        public static string lId;

        /// <summary>
        /// View list of Rubric levels
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManageRubricLevels_Load(object sender, EventArgs e)
        {
            //select data from rubric level table in database
            string cmd = string.Format("SELECT * FROM RubricLevel WHERE RubricId='{0}'", ManageRubric.rid);
            SqlDataReader reader = DataConnection.get_instance().Getdata(cmd);
            //new list of rubric level type and store the data of rubric level in it from database
            List<RubricLevel> rub = new List<RubricLevel>();
            while (reader.Read())
            {
                RubricLevel ru = new RubricLevel();
                ru.Id = Convert.ToInt32(reader.GetValue(0));
                ru.RubricId = Convert.ToInt32(reader.GetValue(1));
                ru.Details = reader.GetString(2);
                ru.MeasurementLevel = Convert.ToInt32(reader.GetValue(3));
                rub.Add(ru);
            }
            //buttons on runtime in dataGridView
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
        /// Specific tasks on click of specific buttons
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
                    lId = edit.Cells[0].Value.ToString();
                    string det = edit.Cells[2].Value.ToString();
                    string de = edit.Cells[3].Value.ToString();
                    txtDetails.Text = det;
                    txtMLevel.Text = de;
                    n = true;
                }
                //Deleting RubricLevel when delete button is clicked in DataGridView
                if (dataGridViewrub.Columns[e.ColumnIndex].Name == "btnDelete")
                {
                    DataGridViewRow deletel = dataGridViewrub.Rows[e.RowIndex];
                    lId = deletel.Cells[0].Value.ToString();
                    MessageBox.Show("Are you sure you want to Permanently delete the specific Rubric Level Its related result will also be deleted?");

                    //Deleting Student Result realted to the Rubric level to be deleted
                    SqlDataReader related = DataConnection.get_instance().Getdata(string.Format("SELECT * FROM StudentResult WHERE RubricMeasurementId={0}", lId));
                    if (related != null)
                    {
                        while (related.Read())
                        {
                            int r;
                            r = Convert.ToInt32(related.GetValue(2));
                            if (r.ToString() == lId.ToString())
                            {
                                string cmd2 = string.Format("DELETE FROM StudentResult WHERE RubricMeasurementId='{0}'", r);
                                int rows2 = DataConnection.get_instance().Executequery(cmd2);
                                MessageBox.Show(String.Format("{0} rows affected", rows2));
                                MessageBox.Show("Related Student Result Deleted");

                            }
                        }
                    }

                    string cmd = string.Format("DELETE FROM RubricLevel WHERE Id='{0}'", Convert.ToInt32(lId));
                    DataConnection.get_instance().Executequery(cmd);
                    MessageBox.Show("Rubric Level Deleted");

                    ManageRubricLevels v = new ManageRubricLevels();
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
        /// Add or edit the rubric level
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDetails.Text == "" || txtMLevel.Text == "")
                {
                    MessageBox.Show("Boxes should not be empty");
                }
                //adding new rubric level
                else if (!n)
                {
                    r.RubricId = Convert.ToInt32(ManageRubric.rid);
                    r.Details = txtDetails.Text;
                    r.MeasurementLevel = Convert.ToInt32(txtMLevel.Text);

                    string cmd = string.Format("INSERT RubricLevel(RubricId, Details , MeasurementLevel) VALUES('{0}','{1}','{2}')", r.RubricId, r.Details, r.MeasurementLevel);
                    int rows = DataConnection.get_instance().Executequery(cmd);
                    MessageBox.Show(String.Format("{0} rows affected", rows));

                    ManageRubricLevels frm = new ManageRubricLevels();
                    this.Hide();
                    frm.Show();
                }
                //Editing rubric level
                else
                {
                    r.Details = txtDetails.Text;
                    r.MeasurementLevel = Convert.ToInt32(txtMLevel.Text);

                    string cmd = string.Format("UPDATE RubricLevel SET RubricId ='{0}' , Details='{1}', MeasurementLevel ='{2}' WHERE Id='{3}'", ManageRubric.rid, r.Details, r.MeasurementLevel, lId);
                    int rows = DataConnection.get_instance().Executequery(cmd);
                    MessageBox.Show(String.Format("{0} rows affected", rows));
                    MessageBox.Show("Rubric Level Edited Successfully!");
                    this.Hide();
                    ManageRubricLevels vs = new ManageRubricLevels();
                    vs.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// show manage rubric form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {

            ManageRubric frm = new ManageRubric();
            this.Hide();
            frm.Show();
        }

        /// <summary>
        /// show view clo form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {

            ViewCLO frm = new ViewCLO();
            this.Hide();
            frm.Show();
        }

        /// <summary>
        /// show home page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            MainPage s = new MainPage();
            this.Hide();
            s.Show();
        }
    }
}
