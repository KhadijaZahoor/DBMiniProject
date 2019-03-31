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
    public partial class ViewStudent : Form
    {
        public ViewStudent()
        {
            InitializeComponent();
            
        }

        //buttons to be added in DataGridView
        DataGridViewButtonColumn buttonE = new DataGridViewButtonColumn();
        DataGridViewButtonColumn buttonD = new DataGridViewButtonColumn();
        public static string id;
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// Show the list of students in dataGridView From database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewStudent_Load(object sender, EventArgs e)
        {
            //Get the data from student table in database
            SqlDataReader reader = DataConnection.get_instance().Getdata("SELECT * FROM Student");

            //crete new list of student class type
            List<Student> student = new List<Student>();
            //add the student data in student list
            while (reader.Read())
            {
                Student std = new Student();
                std.Id = Convert.ToInt32(reader.GetValue(0));
                std.FirstName = reader.GetString(1);
                std.LastName = reader.GetString(2);
                std.Contact = reader.GetString(3);
                std.Email = reader.GetString(4);
                std.RegistrationNo = reader.GetString(5);
                std.Status = Convert.ToInt32(reader.GetValue(6));
                student.Add(std);
            }
            //add button on runtime in dataGridView
            buttonE.Name = "btnEdit";
            buttonE.Text = "EDIT";
            buttonE.UseColumnTextForButtonValue = true;
            buttonD.Name = "btnDelete";
            buttonD.Text = "DELETE";
            buttonD.UseColumnTextForButtonValue = true;
            BindingSource s = new BindingSource();
            s.DataSource = student;
            dataGridView1.DataSource = s;
            dataGridView1.Columns.Add(buttonE);
            dataGridView1.Columns.Add(buttonD);
        }

        /// <summary>
        /// Do specific tasks on click of buttons in data grid view 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //on edit click move to student edit form
                if (dataGridView1.Columns[e.ColumnIndex].Name == "btnEdit")
                {
                    DataGridViewRow edit = dataGridView1.Rows[e.RowIndex];
                    id = edit.Cells[0].Value.ToString();
                    EditStudent f = new EditStudent();
                    this.Hide();
                    f.Show();
                }
                //Deleting specific student
                if (dataGridView1.Columns[e.ColumnIndex].Name == "btnDelete")
                {
                    DataGridViewRow delete = dataGridView1.Rows[e.RowIndex];
                    id = delete.Cells[0].Value.ToString();
                    MessageBox.Show("Are you sure you want to Permanently delete the specific Student Its related result will also be deleted?");

                    //Deleting Student Result realted to the Student to be deleted
                    SqlDataReader related = DataConnection.get_instance().Getdata(string.Format("SELECT * FROM StudentResult WHERE StudentId={0}", id));
                    if (related != null)
                    {
                        while (related.Read())
                        {
                            int r;
                            r = Convert.ToInt32(related.GetValue(1));
                            if (r.ToString() == id.ToString())
                            {
                                string cmd2 = string.Format("DELETE FROM StudentResult WHERE StudentId='{0}'", r);
                                int rows2 = DataConnection.get_instance().Executequery(cmd2);
                                MessageBox.Show(String.Format("{0} rows affected", rows2));
                                MessageBox.Show("Related Student Result Deleted");

                            }
                        }
                    }

                    string cmd = string.Format("DELETE FROM Student WHERE Id='{0}'", Convert.ToInt32(id));
                    DataConnection.get_instance().Executequery(cmd);
                    MessageBox.Show("Student Deleted");

                    ViewStudent v = new ViewStudent();
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
        /// show add student form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            AddStudent frm = new AddStudent();
            this.Hide();
            frm.Show();

        }

        /// <summary>
        /// move to home page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            MainPage s = new MainPage();
            this.Hide();
            s.Show();
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
