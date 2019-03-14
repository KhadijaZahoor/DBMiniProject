﻿using System;
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
    public partial class EditStudent : Form
    {
        public EditStudent()
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
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Do you really want to exit this form?");
            txtSFname.Clear();
            txtSLname.Clear();
            txtSemail.Clear();
            txtSRno.Clear();
            txtScontact.Clear();

            MainPage s = new MainPage();
            this.Hide();
            s.Show();
        }

        private void EditStudent_Load(object sender, EventArgs e)
        {
            if (ViewStudent.id != null)
            {
                SqlDataReader data = DataConnection.get_instance().Getdata(string.Format("SELECT * FROM Student"));
                while (data.Read())
                {
                    Student std = new Student();
                    std.Id = Convert.ToInt32(data.GetValue(0));
                    if (std.Id == Convert.ToInt32(ViewStudent.id))
                    {
                        txtSFname.Text = data.GetString(1);
                        txtSLname.Text = data.GetString(2);
                        txtScontact.Text = data.GetString(3);
                        txtSemail.Text = data.GetString(4);
                        txtSRno.Text = data.GetString(5);
                        if (data.GetValue(6).ToString() == "5")
                        {
                            txtSStatus.Text = "Active";
                        }
                        else
                        {
                            txtSStatus.Text = "InActive";
                        }
                    }
                }
            }
        }

        private void btnEditS_Click(object sender, EventArgs e)
        {
            if (ViewStudent.id != null)
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

                string cmd = string.Format("UPDATE Student SET FirstName='{0}',LastName='{1}',Contact='{2}',Email='{3}',RegistrationNumber='{4}',Status='{5}' WHERE Id='{6}'", std.FirstName, std.LastName, std.Contact.ToString(), std.Email, std.RegistrationNo, std.Status, ViewStudent.id);
                int rows = DataConnection.get_instance().Executequery(cmd);
                MessageBox.Show(String.Format("{0} rows affected", rows));
                MessageBox.Show("Student Edited Successfully!");
                this.Hide();
                ViewStudent vs = new ViewStudent();
                vs.Show();
            }
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

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblSFname_Click(object sender, EventArgs e)
        {

        }

        private void txtSFname_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblSLname_Click(object sender, EventArgs e)
        {

        }

        private void txtSLname_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblScontact_Click(object sender, EventArgs e)
        {

        }

        private void txtScontact_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblSEmail_Click(object sender, EventArgs e)
        {

        }

        private void txtSemail_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblrno_Click(object sender, EventArgs e)
        {

        }

        private void txtSRno_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblSstatus_Click(object sender, EventArgs e)
        {

        }

        private void txtSStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
