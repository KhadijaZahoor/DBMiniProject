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
    public partial class ViewCLO : Form
    {
        public ViewCLO()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// open add clo form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            AddCLO frm = new AddCLO();
            this.Hide();
            frm.Show();
        }

        //buttons to be added in dataGridView
        DataGridViewButtonColumn buttonE = new DataGridViewButtonColumn();
        DataGridViewButtonColumn buttonD = new DataGridViewButtonColumn();
        DataGridViewButtonColumn buttonR = new DataGridViewButtonColumn();

        //attribute to store clo id
        public static string cid;

        /// <summary>
        /// View list of clos in dataGridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewCLO_Load(object sender, EventArgs e)
        {
            //showing list of CLO's in dataGridView
            SqlDataReader reader = DataConnection.get_instance().Getdata("SELECT * FROM Clo");
            //Creting new list of clo to store the data from database
            List<CLO> clos = new List<CLO>();
            while (reader.Read())
            {
                CLO c = new CLO();
                c.Id = Convert.ToInt32(reader.GetValue(0));
                c.Name = reader.GetString(1);
                c.DateCreated = reader.GetDateTime(2);
                c.DateUpdated = reader.GetDateTime(3);
                clos.Add(c);
            }
            //creating buttons of edit, delete and Manage Rubric in dataGridView on runtime
            buttonE.Name = "btnEdit";
            buttonE.Text = "EDIT";
            buttonE.UseColumnTextForButtonValue = true;
            buttonD.Name = "btnDelete";
            buttonD.Text = "DELETE";
            buttonD.UseColumnTextForButtonValue = true;
            buttonR.Name = "btnRubric";
            buttonR.Text = "MANAGE RUBRIC";
            buttonR.UseColumnTextForButtonValue = true;
            BindingSource s = new BindingSource();
            s.DataSource = clos;
            dataGridViewclo.DataSource = s;
            dataGridViewclo.Columns.Add(buttonE);
            dataGridViewclo.Columns.Add(buttonD);
            dataGridViewclo.Columns.Add(buttonR);
        }

        /// <summary>
        /// Do specific tasks on click of different buttons in DataGridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewclo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //open edit clo form for Editing clo 
            if (dataGridViewclo.Columns[e.ColumnIndex].Name == "btnEdit")
            {
                DataGridViewRow edit = dataGridViewclo.Rows[e.RowIndex];
                cid = edit.Cells[0].Value.ToString();
                EditCLO f = new EditCLO();
                this.Hide();
                f.Show();
            }
            //Deleting Specific CLO
            if (dataGridViewclo.Columns[e.ColumnIndex].Name == "btnDelete")
            {
                DataGridViewRow delete = dataGridViewclo.Rows[e.RowIndex];
                cid = delete.Cells[0].Value.ToString();
                MessageBox.Show("Are you sure you want to Permanently delete the specific CLO(Its releted rubrics and their levels will also be deleted)?");

                //Deleting Rubrics related to that clo
                SqlDataReader Drelated = DataConnection.get_instance().Getdata(string.Format("SELECT * FROM Rubric WHERE CloId={0}", cid));
                if (Drelated != null)
                {
                    while (Drelated.Read())
                    {
                        int ru;
                        ru = Convert.ToInt32(Drelated.GetValue(2));
                        if (ru.ToString() == cid)
                        {
                            //Deleting Rubric Levels related to the Rubrics which are related to that specific CLO 
                            SqlDataReader related = DataConnection.get_instance().Getdata(string.Format("SELECT * FROM RubricLevel WHERE Rubricid={0}", ru));
                            if (related != null)
                            {
                                while (related.Read())
                                {
                                    int r;
                                    r = Convert.ToInt32(related.GetValue(1));
                                    if (r.ToString() == ru.ToString())
                                    {
                                        string cmd2 = string.Format("DELETE FROM RubricLevel WHERE RubricId='{0}'", r);
                                        int rows2 = DataConnection.get_instance().Executequery(cmd2);
                                        MessageBox.Show(String.Format("{0} rows affected", rows2));
                                        MessageBox.Show("Related Rubric Levels Deleted");

                                    }
                                }
                            }


                            string cmd1 = string.Format("DELETE FROM Rubric WHERE Cloid='{0}'", cid);
                            DataConnection.get_instance().Executequery(cmd1);
                            MessageBox.Show("Related Rubrics Deleted");
                        }
                    }
                }

                string cmd = string.Format("DELETE FROM Clo WHERE Id='{0}'", Convert.ToInt32(cid));
                DataConnection.get_instance().Executequery(cmd);

                ViewCLO v = new ViewCLO();
                this.Hide();
                v.Show();
            }
            //On the click of manage rubric button openening manage rubric form
            if (dataGridViewclo.Columns[e.ColumnIndex].Name == "btnRubric")
            {
                DataGridViewRow rubr = dataGridViewclo.Rows[e.RowIndex];
                cid = rubr.Cells[0].Value.ToString();
                ManageRubric f = new ManageRubric();
                this.Hide();
                f.Show();
            }
        }

        /// <summary>
        /// opening home page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Do you really want to exit this form?");
            MainPage s = new MainPage();
            this.Hide();
            s.Show();
        }

        /// <summary>
        /// Open add clo form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            AddCLO frm = new AddCLO();
            this.Hide();
            frm.Show();
        }

        /// <summary>
        /// move to home page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Do you really want to exit this form?");
            MainPage s = new MainPage();
            this.Hide();
            s.Show();
        }
        
    }
}
