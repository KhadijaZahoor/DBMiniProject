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

        private static int id;

        private void ViewAttendance_Load(object sender, EventArgs e)
        {

        }

        private void btnfilter_Click(object sender, EventArgs e)
        {
            SqlDataReader att = DataConnection.get_instance().Getdata("SELECT * FROM ClassAttendance");
            while (att.Read())
            {
                if (dateTimePicker1.Value.Date.ToString() == att[1].ToString())
                {
                    id = Convert.ToInt32(att[0]);
                    SqlDataReader reader = DataConnection.get_instance().Getdata(string.Format("SELECT FirstName , LastName , RegistrationNumber , AttendanceId , AttendanceStatus FROM StudentAttendance a JOIN Student s ON a.StudentId = s.Id WHERE a.AttendanceId='{0}'", id));
                    BindingSource s = new BindingSource();
                    s.DataSource = reader;
                    dataGridViewrub.DataSource = s;

                }
            }
            DataGridViewColumn c = new DataGridViewTextBoxColumn();
            c.HeaderText = "Status";
            int colInd = dataGridViewrub.Columns.Add(c);
            foreach (DataGridViewRow r in dataGridViewrub.Rows)
            {
                if (r.Cells[4].FormattedValue.ToString() == "1")
                {
                    r.Cells[5].Value = "Present";
                }
                if (r.Cells[4].FormattedValue.ToString() == "2")
                {
                    r.Cells[5].Value = "Absent";
                }
                if (r.Cells[4].FormattedValue.ToString() == "3")
                {
                    r.Cells[5].Value = "Leave";
                }
                if (r.Cells[4].FormattedValue.ToString() == "4")
                {
                    r.Cells[5].Value = "Late";
                }
            }
            dataGridViewrub.Columns.RemoveAt(4);
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
    }
}
