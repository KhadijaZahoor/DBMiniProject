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
    public partial class ViewCLO : Form
    {
        public ViewCLO()
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

        private void button1_Click(object sender, EventArgs e)
        {
            AddCLO frm = new AddCLO();
            this.Hide();
            frm.Show();
        }

        DataGridViewButtonColumn buttonE = new DataGridViewButtonColumn();
        DataGridViewButtonColumn buttonD = new DataGridViewButtonColumn();
        public static string id;

        private void ViewCLO_Load(object sender, EventArgs e)
        {
            SqlDataReader reader = DataConnection.get_instance().Getdata("SELECT * FROM Clo");
            List<CLO> clos = new List<CLO>();
            while (reader.Read())
            {
                CLO c = new CLO();
                c.Id = Convert.ToInt32(reader.GetValue(0));
                c.Name = reader.GetString(1);
                c.DateCreated = reader.GetDateTime(2);
                c.DateUpdated = reader.GetDateTime(3);
                clos.Add(c);
            }
            buttonE.Name = "btnEdit";
            buttonE.Text = "EDIT";
            buttonE.UseColumnTextForButtonValue = true;
            buttonD.Name = "btnDelete";
            buttonD.Text = "DELETE";
            buttonD.UseColumnTextForButtonValue = true;
            BindingSource s = new BindingSource();
            s.DataSource = clos;
            dataGridViewclo.DataSource = s;
            dataGridViewclo.Columns.Add(buttonE);
            dataGridViewclo.Columns.Add(buttonD);
        }

        private void dataGridViewclo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewclo.Columns[e.ColumnIndex].Name == "btnEdit")
            {
                DataGridViewRow edit = dataGridViewclo.Rows[e.RowIndex];
                id = edit.Cells[0].Value.ToString();
                EditCLO f = new EditCLO();
                this.Hide();
                f.Show();
            }
            if (dataGridViewclo.Columns[e.ColumnIndex].Name == "btnDelete")
            {
                DataGridViewRow delete = dataGridViewclo.Rows[e.RowIndex];
                id = delete.Cells[0].Value.ToString();
                MessageBox.Show("Are you sure you want to Permanently delete the specific CLO?");
                string cmd = string.Format("DELETE FROM Clo WHERE Id='{0}'", Convert.ToInt32(id));
                DataConnection.get_instance().Executequery(cmd);

                ViewCLO v = new ViewCLO();
                this.Hide();
                v.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Do you really want to exit this form?");
            MainPage s = new MainPage();
            this.Hide();
            s.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            AddCLO frm = new AddCLO();
            this.Hide();
            frm.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Do you really want to exit this form?");
            MainPage s = new MainPage();
            this.Hide();
            s.Show();
        }
        
    }
}
