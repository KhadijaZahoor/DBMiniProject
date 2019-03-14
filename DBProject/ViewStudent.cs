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

        DataGridViewButtonColumn buttonE = new DataGridViewButtonColumn();
        DataGridViewButtonColumn buttonD = new DataGridViewButtonColumn();
        public static string id;
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ViewStudent_Load(object sender, EventArgs e)
        {
            SqlDataReader reader = DataConnection.get_instance().Getdata("SELECT * FROM Student");
            List<Student> student = new List<Student>();
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "btnEdit")
            {
                DataGridViewRow edit = dataGridView1.Rows[e.RowIndex];
                id = edit.Cells[0].Value.ToString();
                EditStudent f = new EditStudent();
                this.Hide();
                f.Show();
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "btnDelete")
            {
                DataGridViewRow delete = dataGridView1.Rows[e.RowIndex];
                id = delete.Cells[0].Value.ToString();
                MessageBox.Show("Are you sure you want to Permanently delete the specific Student?");
                string cmd = string.Format("DELETE FROM Student WHERE Id='{0}'", Convert.ToInt32(id));
                DataConnection.get_instance().Executequery(cmd);

                ViewStudent v = new ViewStudent();
                this.Hide();
                v.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddStudent frm = new AddStudent();
            this.Hide();
            frm.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Do you really want to exit this form?");
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
