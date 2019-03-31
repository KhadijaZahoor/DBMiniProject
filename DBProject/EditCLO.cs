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
           
        }

        //new object of clo type
        CLO clo = new CLO();

        /// <summary>
        /// show the data of clo in textboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditCLO_Load(object sender, EventArgs e)
        {
            if (ViewCLO.cid != null)
            {
                SqlDataReader data = DataConnection.get_instance().Getdata(string.Format("SELECT * FROM Clo"));
                while (data.Read())
                {
                    CLO clo = new CLO();
                    clo.Id = Convert.ToInt32(data.GetValue(0));
                    if (clo.Id == Convert.ToInt32(ViewCLO.cid))
                    {
                        txtSFname.Text = data.GetString(1);
                        //set the date time of textbox date updated to the datetime right now
                        txtSLname.Text = DateTime.Now.ToString("dd.MM.yyyy  HH:mm:ss"); ;
                        
                    }
                }
            }
        }

        /// <summary>
        /// edit the data of clo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditS_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewCLO.cid != null)
                {
                    if (txtSFname.Text == "")
                    {
                        MessageBox.Show("CLO Name should not be empty");
                    }
                    else
                    {
                        clo.Name = txtSFname.Text;
                        clo.DateUpdated = DateTime.Now;

                        SqlConnection conn = new SqlConnection("Data Source=HAIER-PC;Initial Catalog=ProjectB;Integrated Security=True");
                        SqlCommand cmd = new SqlCommand("UPDATE Clo SET Name=@name , DateUpdated=@dateUpdated WHERE Id=@id", conn);
                        cmd.Parameters.AddWithValue("@name", clo.Name);
                        cmd.Parameters.AddWithValue("@dateUpdated", clo.DateUpdated);
                        cmd.Parameters.AddWithValue("@id", ViewCLO.cid);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("CLO Edited Successfully!");

                        this.Hide();
                        ViewCLO vs = new ViewCLO();
                        vs.Show();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// clear the data in the textboxes and move to home page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
