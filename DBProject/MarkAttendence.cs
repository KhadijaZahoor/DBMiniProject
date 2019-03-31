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
    public partial class MarkAttendence : Form
    {
        public MarkAttendence()
        {
            InitializeComponent();
        }

        //make new column of comboBox
        DataGridViewComboBoxColumn com = new DataGridViewComboBoxColumn();

        //new object of studentAttendance type
        StudentAttendance sta = new StudentAttendance();

        //crete new list of student class type
        List<Student> student = new List<Student>();

        /// <summary>
        /// show list of active students in DataGridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MarkAttendence_Load(object sender, EventArgs e)
        {
            //Get the data from student table in database
            SqlDataReader reader = DataConnection.get_instance().Getdata("SELECT * FROM Student WHERE Status=5 ");

            
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
            //add combobox on runtime in dataGridView
            com.HeaderText = "AttendanceStatus";
            com.MaxDropDownItems = 4;
            com.Items.Add("Present");
            com.Items.Add("Absent");
            com.Items.Add("Leave");
            com.Items.Add("Late");
            BindingSource s = new BindingSource();
            s.DataSource = student;
            dataGridView1.DataSource = s;
            dataGridView1.Columns.Add(com);
            dataGridView1.Columns.RemoveAt(3);
            dataGridView1.Columns.RemoveAt(4);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        
        }

        /// <summary>
        /// mark the attendance of active Students
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            //making connection and adding class attendance attributes in database
            SqlConnection conn = new SqlConnection("Data Source=HAIER-PC;Initial Catalog=ProjectB;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("INSERT INTO ClassAttendance(AttendanceDate) VALUES (@dateCreated)", conn);
            cmd.Parameters.AddWithValue("@dateCreated", DateTime.Now.Date);
            conn.Open();
            cmd.ExecuteNonQuery();

            //store the attributes in StudentAttendance of active students
            for (int x=0;x<student.Count();x++)
            {
                SqlDataReader attendance = DataConnection.get_instance().Getdata("SELECT * FROM ClassAttendance");
                while(attendance.Read())
                {
                    if(attendance[1].ToString() == DateTime.Now.Date.ToString())
                    {
                        sta.AttendanceId = Convert.ToInt32(attendance[0]);
                    }
                }

                sta.StudentId = Convert.ToInt32(dataGridView1.Rows[x].Cells[0].Value.ToString());

                string comVal = dataGridView1.Rows[x].Cells[5].Value.ToString();
                //getting values from lookUp table in database
                SqlDataReader status = DataConnection.get_instance().Getdata("SELECT * FROM Lookup");

                //check the values in textbox with lookup table name column an assign specific id accordingly
                while (status.Read())
                {
                    if (status[1].ToString() == comVal)
                    {
                        sta.AttendanceStatus = Convert.ToInt32(status[0]);
                    }
                }

                //inserting the studentAttendance in database
                string cmd1 = string.Format("INSERT StudentAttendance(AttendanceId,StudentId,AttendanceStatus) VALUES('{0}','{1}','{2}')", sta.AttendanceId , sta.StudentId , sta.AttendanceStatus);
                int rows = DataConnection.get_instance().Executequery(cmd1);
                MessageBox.Show(String.Format("{0} rows affected", rows));
   
            }
            //show Attendance list
            ViewAttendance frm = new ViewAttendance();
            this.Hide();
            frm.Show();
        }

        /// <summary>
        /// show attendance list form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            ViewAttendance a = new ViewAttendance();
            this.Hide();
            a.Show();
        }
    }
}
