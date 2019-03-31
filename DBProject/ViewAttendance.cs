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
    public partial class ViewAttendance : Form
    {
        public ViewAttendance()
        {
            InitializeComponent();
        }

        
        //attribute to store attendance id
        private static int id;

        private void ViewAttendance_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// filtering the attendance accordind to attendance date and show filtered list in dataGridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnfilter_Click(object sender, EventArgs e)
        {
            try
            {
                bool n = false;
                //match the date in class attendance table and store attendance id
                SqlDataReader att = DataConnection.get_instance().Getdata("SELECT * FROM ClassAttendance");
                while (att.Read())
                {
                    if (dateTimePicker1.Value.Date.ToString() == att[1].ToString())
                    {
                        id = Convert.ToInt32(att[0]);
                        n = true;
                    }
                }
                if (n)
                {
                    //show the attributes of students and Student attendance with the help of join
                    SqlDataReader reader = DataConnection.get_instance().Getdata(string.Format("SELECT FirstName , LastName , RegistrationNumber , AttendanceId , AttendanceStatus FROM StudentAttendance a JOIN Student s ON a.StudentId = s.Id WHERE a.AttendanceId='{0}'", id));
                    BindingSource s = new BindingSource();
                    s.DataSource = reader;
                    dataGridViewrub.DataSource = s;

                    //show the attendance Status name with the help of id
                    foreach (DataGridViewRow r in dataGridViewrub.Rows)
                    {
                        if (r.Cells[5].FormattedValue.ToString() == "1")
                        {
                            r.Cells[0].Value = "Present";
                        }
                        if (r.Cells[5].FormattedValue.ToString() == "2")
                        {
                            r.Cells[0].Value = "Absent";
                        }
                        if (r.Cells[5].FormattedValue.ToString() == "3")
                        {
                            r.Cells[0].Value = "Leave";
                        }
                        if (r.Cells[5].FormattedValue.ToString() == "4")
                        {
                            r.Cells[0].Value = "Late";
                        }
                    }
                    //change the index of status text box in data grid view
                    dataGridViewrub.Columns["Status"].DisplayIndex = dataGridViewrub.ColumnCount - 1;

                    //remove the attendance status column
                    dataGridViewrub.Columns.RemoveAt(5);
                }
                else
                {
                    MessageBox.Show("No attendance is marked on this date");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// show home page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            MainPage m = new MainPage();
            this.Hide();
            m.Show();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// show active students list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            ActiveStudent a = new ActiveStudent();
            this.Hide();
            a.Show();
        }
    }
}
