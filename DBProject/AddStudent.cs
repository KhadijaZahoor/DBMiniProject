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
    public partial class AddStudent : Form
    {
        public AddStudent()
        {
            InitializeComponent();
            
        }

        //making new object of student class type
        Student std = new Student();
        private void AddStudent_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Adding new student
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegisterS_Click(object sender, EventArgs e)
        {
            try
            {
                SqlDataReader dataS = DataConnection.get_instance().Getdata("SELECT * FROM Student");
                List<Student> student = new List<Student>();
                while (dataS.Read())
                {
                    Student std = new Student();
                    std.Id = Convert.ToInt32(dataS.GetValue(0));
                    std.FirstName = dataS.GetString(1);
                    std.LastName = dataS.GetString(2);
                    std.Contact = dataS.GetString(3);
                    std.Email = dataS.GetString(4);
                    std.RegistrationNo = dataS.GetString(5);
                    std.Status = Convert.ToInt32(dataS.GetValue(6));
                    student.Add(std);
                }
                bool condition = true;
                if (txtSFname.Text == "" || txtSLname.Text == "" || txtScontact.Text == "" || txtSemail.Text == "" || txtSRno.Text == "" || txtSStatus.Text == "")
                {
                    MessageBox.Show("Boxes should not be empty");
                    condition = false;
                }
                else
                {
                    foreach (Student s in student)
                    {

                        if (s.RegistrationNo == txtSRno.Text)
                        {
                            //checks whether the entries are unique or not
                            MessageBox.Show("Student cannot have same Registration Number");
                            condition = false;
                        }
                    }
                    try
                    {
                        std.FirstName = txtSFname.Text;
                    }
                    catch (Exception)
                    {
                        condition = false;
                        MessageBox.Show("First Name should be in alphabets");
                    }
                    try
                    {
                        std.LastName = txtSLname.Text;
                    }
                    catch (Exception)
                    {
                        condition = false;
                        MessageBox.Show("Last Name should be in alphabets");
                    }
                    try
                    {
                        std.Email = txtSemail.Text;
                    }
                    catch (Exception)
                    {
                        condition = false;
                        MessageBox.Show("Email cannot contian spaces");
                    }
                }

                if (condition == true)
                {
                    //registering student by setting the attributes of student class equal to the values in textboxes
                    std.FirstName = txtSFname.Text;
                    std.LastName = txtSLname.Text;
                    std.Contact = txtScontact.Text;
                    std.Email = txtSemail.Text;
                    std.RegistrationNo = txtSRno.Text;

                    //getting values from lookUp table in database
                    SqlDataReader status = DataConnection.get_instance().Getdata("SELECT * FROM Lookup");

                    //check the values in textbox with lookup table name column an assign specific id accordingly
                    while (status.Read())
                    {
                        if (status[1].ToString() == txtSStatus.Text)
                        {
                            std.Status = Convert.ToInt32(status[0]);
                        }
                    }

                    //inserting the student in database
                    string cmd = string.Format("INSERT Student(FirstName,LastName,Contact,Email,RegistrationNumber,Status) VALUES('{0}','{1}','{2}','{3}','{4}','{5}')", std.FirstName, std.LastName, std.Contact, std.Email, std.RegistrationNo, std.Status);
                    int rows = DataConnection.get_instance().Executequery(cmd);
                    MessageBox.Show(String.Format("{0} rows affected", rows));

                    //show students list
                    ViewStudent frm = new ViewStudent();
                    this.Hide();
                    frm.Show();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            }
            
        /// <summary>
        /// opening student list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewS_Click(object sender, EventArgs e)
        {
            //show students list
            ViewStudent frm = new ViewStudent();
            this.Hide();
            frm.Show();
        }
    }
}
