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
    public partial class manual : Form
    {
        public manual()
        {
            InitializeComponent();
        }

        /// <summary>
        /// give guidelines in rich textbox about using this application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void manual_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = "Student assessment inlet is a  desktop application that will be operated by the teacher to manage data. All the button's are " +
                "present on the main page from where you can visit pages of your requirement's But if you " +
                "want to manage Rubric, Rubric Level, Assessment Components and Result than their details are as follows" +
                "                                                                                                 " +
                "If you want to #'Add a rubric'# related to a CLO then you " +
                "have to go to the CLO's List and their will be a #'manage rubric button'# at the end from where you" +
                "can add,delete,view or update rubric's and further If you want to #'manage a Rubric Level'# you" +
                "can further go from #'Manage Rubric Level Button'# at the end of Rubric's " +
                "list                                                                    " +
                "Similarly If you want to #'Add an Assessment Component'# related to an Assessment then you " +
                "have to go to the  Assessment's List and their will be a #'Component button'# at the end from where you " +
                "can add,delete,view or update Assessment's Components                                         " +
                "                 Finally, #'Result'# is approached by Result Button on Active Student's List" +
                "                                                               That was some information about how to" +
                " use this application.                                                                             " +
                "Enjoy  #$' STUDENT ASSESSMENT INLET '$#";
        }

        /// <summary>
        /// show clo list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            ViewCLO a = new ViewCLO();
            this.Hide();
            a.Show();
        }

        /// <summary>
        /// show assessments list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            ViewAssessment a = new ViewAssessment();
            this.Hide();
            a.Show();
        }

        /// <summary>
        /// show active students list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            ActiveStudent a = new ActiveStudent();
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
