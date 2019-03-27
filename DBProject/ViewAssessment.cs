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
    public partial class ViewAssessment : Form
    {
        public ViewAssessment()
        {
            InitializeComponent();
        }

        //buttons to be added in dataGridView
        DataGridViewButtonColumn buttonE = new DataGridViewButtonColumn();
        DataGridViewButtonColumn buttonD = new DataGridViewButtonColumn();
        DataGridViewButtonColumn buttonR = new DataGridViewButtonColumn();

        //attribute to store assessment id
        public static string aid;

        private void ViewAssessment_Load(object sender, EventArgs e)
        {
            //showing list of assessment in dataGridView
            SqlDataReader reader = DataConnection.get_instance().Getdata("SELECT * FROM Assessment");
            //Creting new list of Assessment to store the data from database
            List<Assessment> ass = new List<Assessment>();
            while (reader.Read())
            {
                Assessment c = new Assessment();
                c.Id = Convert.ToInt32(reader.GetValue(0));
                c.Title = reader.GetString(1);
                c.DateCreated = reader.GetDateTime(2);
                c.TotalMarks = Convert.ToInt32(reader.GetValue(3));
                c.TotalWeightage = Convert.ToInt32(reader.GetValue(4));
                ass.Add(c);
            }
            //creating buttons of edit, delete and Manage Components in dataGridView on runtime
            buttonE.Name = "btnEdit";
            buttonE.Text = "EDIT";
            buttonE.UseColumnTextForButtonValue = true;
            buttonD.Name = "btnDelete";
            buttonD.Text = "DELETE";
            buttonD.UseColumnTextForButtonValue = true;
            buttonR.Name = "btnComponent";
            buttonR.Text = "Components";
            buttonR.UseColumnTextForButtonValue = true;
            BindingSource s = new BindingSource();
            s.DataSource = ass;
            dataGridViewclo.DataSource = s;
            dataGridViewclo.Columns.Add(buttonE);
            dataGridViewclo.Columns.Add(buttonD);
            dataGridViewclo.Columns.Add(buttonR);
        }

        /// <summary>
        /// show add assessment form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            AddAssessment a = new AddAssessment();
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
            MainPage m = new MainPage();
            this.Hide();
            m.Show();
        }

        /// <summary>
        /// edit or delete assessment or manage its components
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewclo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //open edit assessment form for Editing clo 
            if (dataGridViewclo.Columns[e.ColumnIndex].Name == "btnEdit")
            {
                DataGridViewRow edit = dataGridViewclo.Rows[e.RowIndex];
                aid = edit.Cells[0].Value.ToString();
                EditAssessment f = new EditAssessment();
                this.Hide();
                f.Show();
            }
            //Deleting Specific CLO
            if (dataGridViewclo.Columns[e.ColumnIndex].Name == "btnDelete")
            {
                DataGridViewRow delete = dataGridViewclo.Rows[e.RowIndex];
                aid = delete.Cells[0].Value.ToString();
                MessageBox.Show("Are you sure you want to Permanently delete the specific Assessment(Its releted components will also be deleted)?");

                //Deleting Assessment Component realted to the Assessment to be deleted
                SqlDataReader related = DataConnection.get_instance().Getdata(string.Format("SELECT * FROM AssessmentComponent WHERE AssessmentId={0}", aid));
                if (related != null)
                {
                    while (related.Read())
                    {
                        int r;
                        r = Convert.ToInt32(related.GetValue(6));
                        if (r.ToString() == aid.ToString())
                        {
                            string cmd2 = string.Format("DELETE FROM AssessmentComponent WHERE AssessmentId='{0}'", r);
                            int rows2 = DataConnection.get_instance().Executequery(cmd2);
                            MessageBox.Show(String.Format("{0} rows affected", rows2));
                            MessageBox.Show("Related Assessment components Deleted");

                        }
                    }
                }

                string cmd = string.Format("DELETE FROM Assessment WHERE Id='{0}'", Convert.ToInt32(aid));
                DataConnection.get_instance().Executequery(cmd);

                ViewAssessment v = new ViewAssessment();
                this.Hide();
                v.Show();
            }
            //On the click of manage components button openening manage assessment component form
            if (dataGridViewclo.Columns[e.ColumnIndex].Name == "btnComponent")
            {
                DataGridViewRow rubr = dataGridViewclo.Rows[e.RowIndex];
                aid = rubr.Cells[0].Value.ToString();
                AssessmentComponent f = new AssessmentComponent();
                this.Hide();
                f.Show();
            }
        }
    }
}
