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
    public partial class EditCLO : Form
    {
        public EditCLO()
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
        private void EditCLO_Load(object sender, EventArgs e)
        {
            if (ViewCLO.id != null)
            {
                SqlDataReader data = DataConnection.get_instance().Getdata(string.Format("SELECT * FROM Clo"));
                while (data.Read())
                {
                    CLO clo = new CLO();
                    clo.Id = Convert.ToInt32(data.GetValue(0));
                    if (clo.Id == Convert.ToInt32(ViewCLO.id))
                    {
                        txtSFname.Text = data.GetString(1);
                        txtSLname.Text = DateTime.Now.ToString("dd.MM.yyyy  HH:mm:ss"); ;
                        
                    }
                }
            }
        }

        private void btnEditS_Click(object sender, EventArgs e)
        {
            if (ViewCLO.id != null)
            {
                clo.Name = txtSFname.Text;
                clo.DateUpdated = DateTime.Now;

                SqlConnection conn = new SqlConnection("Data Source=HAIER-PC;Initial Catalog=ProjectB;Integrated Security=True");
                SqlCommand cmd = new SqlCommand("UPDATE Clo SET Name=@name , DateUpdated=@dateUpdated WHERE Id=@id" ,conn);
                cmd.Parameters.AddWithValue("@name", clo.Name);
                cmd.Parameters.AddWithValue("@dateUpdated", clo.DateUpdated);
                cmd.Parameters.AddWithValue("@id", ViewCLO.id);
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("CLO Edited Successfully!");

                this.Hide();
                ViewCLO vs = new ViewCLO();
                vs.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Do you really want to exit this form?");
            txtSFname.Clear();
            txtSLname.Clear();

            MainPage s = new MainPage();
            this.Hide();
            s.Show();
        }
    }
}
