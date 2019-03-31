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
    public partial class ActiveStudent : Form
    {
        public ActiveStudent()
        {
            InitializeComponent();
        }

        //buttons to be added in DataGridView
        DataGridViewButtonColumn buttonE = new DataGridViewButtonColumn();

        // attribute to store student id
        public static string id;

        /// <summary>
        /// Show list of active student
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActiveStudent_Load(object sender, EventArgs e)
        {
            //Get the data of active students from student table in database
            SqlDataReader reader = DataConnection.get_instance().Getdata("SELECT * FROM Student WHERE Status=5 ");

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
            buttonE.Name = "btnResult";
            buttonE.Text = "Result";
            buttonE.UseColumnTextForButtonValue = true;
            BindingSource s = new BindingSource();
            s.DataSource = student;
            dataGridView1.DataSource = s;
            dataGridView1.Columns.Add(buttonE);

        }

        /// <summary>
        /// On click of result button in dataGridView open result form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //on Result button click move to Result form
            if (dataGridView1.Columns[e.ColumnIndex].Name == "btnResult")
            {
                DataGridViewRow edit = dataGridView1.Rows[e.RowIndex];
                id = edit.Cells[0].Value.ToString();
                Result f = new Result();
                this.Hide();
                f.Show();
            }
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
        /// show student list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            ViewStudent s = new ViewStudent();
            this.Hide();
            s.Show();
        }
    }
}
