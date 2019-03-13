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

        Student std = new Student();
        private void AddStudent_Load(object sender, EventArgs e)
        {

        }

        private void btnRegisterS_Click(object sender, EventArgs e)
        {
            std.FirstName = txtSFname.Text;
            std.LastName = txtSLname.Text;
            std.Contact = txtScontact.Text;
            std.Email = txtSemail.Text;
            std.RegistrationNo = txtSRno.Text;

            SqlDataReader status = DataConnection.get_instance().Getdata("SELECT * FROM Lookup");

            while (status.Read())
            {
                if (status[1].ToString() == txtSStatus.Text)
                {
                    std.Status = Convert.ToInt32(status[0]);
                }
            }

            string cmd = string.Format("INSERT Student(FirstName,LastName,Contact,Email,RegistrationNumber,Status) VALUES('{0}','{1}','{2}','{3}','{4}','{5}')", std.FirstName, std.LastName, std.Contact, std.Email, std.RegistrationNo, std.Status);
            int rows = DataConnection.get_instance().Executequery(cmd);
            MessageBox.Show(String.Format("{0} rows affected", rows));
            ViewStudent frm = new ViewStudent();
            this.Hide();
            frm.Show();
        }

        private void btnViewS_Click(object sender, EventArgs e)
        {
            ViewStudent frm = new ViewStudent();
            this.Hide();
            frm.Show();
        }
    }
}
