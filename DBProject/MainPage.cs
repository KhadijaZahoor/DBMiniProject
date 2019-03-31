using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBProject
{
    public partial class MainPage : Form
    {
        /// <summary>
        /// dataconnection to the database
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            DataConnection.get_instance().connectionstring = "Data Source=HAIER-PC;Initial Catalog=ProjectB;Integrated Security=True";
            try
            {
                var con = DataConnection.get_instance().Getconnection();
                con.Open();
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
        /// show view student form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            ViewStudent frm = new ViewStudent();
            this.Hide();
            frm.Show();
        }

        /// <summary>
        /// show add clo form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            AddCLO frm = new AddCLO();
            this.Hide();
            frm.Show();
        }

        /// <summary>
        /// show view clo form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            ViewCLO frm = new ViewCLO();
            this.Hide();
            frm.Show();
        }

        private void MainPage_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// hide this form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Are you sure you want to exit");
            this.Hide();
        }

        /// <summary>
        /// show mark attendance form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button10_Click(object sender, EventArgs e)
        {
            MarkAttendence m = new MarkAttendence();
            this.Hide();
            m.Show();
        }

        /// <summary>
        /// show add assessment form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            AddAssessment m = new AddAssessment();
            this.Hide();
            m.Show();
        }

        /// <summary>
        /// show view assessment form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click(object sender, EventArgs e)
        {
            ViewAssessment a = new ViewAssessment();
            this.Hide();
            a.Show();
        }

        /// <summary>
        /// show attendance list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            ViewAttendance a = new ViewAttendance();
            this.Hide();
            a.Show();
        }

        /// <summary>
        /// show students list to generate result
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            ActiveStudent a = new ActiveStudent();
            this.Hide();
            a.Show();
        }

        /// <summary>
        /// Show pdf reports form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button11_Click(object sender, EventArgs e)
        {
            pdfReports a = new pdfReports();
            this.Hide();
            a.Show();
        }
    }
}
