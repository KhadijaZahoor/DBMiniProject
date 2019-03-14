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
    public partial class AddCLO : Form
    {
        public AddCLO()
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

        CLO clo = new CLO();
        private void AddCLO_Load(object sender, EventArgs e)
        {
            txtCCDate.Text = DateTime.Now.ToString("dd.MM.yyyy  HH:mm:ss");
            txtCUdate.Text = DateTime.Now.ToString("dd.MM.yyyy  HH:mm:ss");
        }

        private void btnRegisterS_Click(object sender, EventArgs e)
        {
            clo.Name = txtCname.Text;
            clo.DateCreated = DateTime.Now.Date;
            clo.DateUpdated = DateTime.Now.Date;

            SqlConnection conn = new SqlConnection("Data Source=HAIER-PC;Initial Catalog=ProjectB;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("INSERT INTO Clo(Name,DateCreated,DateUpdated) VALUES (@name,@dateCreated,@dateUpdated)",conn);
            cmd.Parameters.AddWithValue("@name",clo.Name);
            cmd.Parameters.AddWithValue("@dateCreated", clo.DateCreated);
            cmd.Parameters.AddWithValue("@dateUpdated", clo.DateUpdated);
            conn.Open();
            cmd.ExecuteNonQuery();
            ViewCLO frm = new ViewCLO();
            this.Hide();
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ViewCLO frm = new ViewCLO();
            this.Hide();
            frm.Show();
        }
    }
}
