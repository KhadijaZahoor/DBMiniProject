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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace DBProject
{
    public partial class pdfReports : Form
    {
        public pdfReports()
        {
            InitializeComponent();
        }

        // variable to store clo id
        static int cid;

        // variable to store Assessment id
        static int aid;

        /// <summary>
        /// Export Grid to pdf
        /// </summary>
        /// <param name="dg"></param>
        /// <param name="filename"></param>
        public void exportgridtopdf(DataGridView dg, string filename)
        {
            BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.EMBEDDED);
            PdfPTable pdftable = new PdfPTable(dg.Columns.Count);
            pdftable.DefaultCell.Padding = 3;
            pdftable.WidthPercentage = 100;
            pdftable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdftable.DefaultCell.BorderWidth = 1;

            iTextSharp.text.Font text = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
            //Add header
            foreach (DataGridViewColumn column in dg.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, text));
                cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                pdftable.AddCell(cell);
            }

            //Add datarow
            foreach (DataGridViewRow row in dg.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    pdftable.AddCell(new Phrase(cell.Value.ToString(), text));
                }
            }
            var savefiledialogue = new SaveFileDialog();
            savefiledialogue.FileName = filename;
            savefiledialogue.DefaultExt = ".pdf";

            if (savefiledialogue.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(savefiledialogue.FileName, FileMode.Create))
                {
                    Document pdfdoc = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
                    PdfWriter.GetInstance(pdfdoc, stream);
                    pdfdoc.Open();
                    pdfdoc.Add(pdftable);
                    pdfdoc.Close();
                    stream.Close();
                }
            }
        }

        /// <summary>
        /// Show home page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            MainPage m = new MainPage();
            this.Hide();
            m.Show();
        }

        /// <summary>
        /// show all assessments and clos in combo boxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pdfReports_Load(object sender, EventArgs e)
        {
            //Show all CLO's Name in combo box
            string cmd1 = "SELECT * FROM Clo";
            SqlDataReader reader1 = DataConnection.get_instance().Getdata(cmd1);
            List<string> rname1 = new List<string>();
            while (reader1.Read())
            {
                rname1.Add(reader1.GetString(1));
            }

            comboBoxClo.DataSource = rname1;
            comboBoxClo.Text = "";


            //Show all assessments Title in combo box
            string cmd = "SELECT * FROM Assessment";
            SqlDataReader reader = DataConnection.get_instance().Getdata(cmd);
            List<string> rname = new List<string>();
            while (reader.Read())
            {
                rname.Add(reader.GetString(1));
            }

            comboBoxAss.DataSource = rname;
            comboBoxAss.Text = "";

        }

        private void btnfilterClo_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxClo.Text == "")
                {
                    MessageBox.Show("Select Clo First");
                }
                else
                {
                    //reading data from CLO table
                    SqlDataReader data = DataConnection.get_instance().Getdata("SELECT * FROM Clo");
                    while (data.Read())
                    {
                        if (data.GetString(1) == comboBoxClo.Text)
                        {
                            cid = Convert.ToInt32(data.GetValue(0));

                        }
                    }

                    SqlDataReader result = DataConnection.get_instance().Getdata(string.Format("SELECT Student.RegistrationNumber as RegNo, CONCAT(Student.FirstName,' ',Student.LastName) as Name, AssessmentComponent.Name as Component,Rubric.Details as RubricDetails, AssessmentComponent.TotalMarks as TotalMarks, RubricLevel.MeasurementLevel as MeasurementLevel, ((RubricLevel.MeasurementLevel*AssessmentComponent.TotalMarks)/4) As ObtainedMarks  FROM Student JOIN StudentResult ON Student.Id = StudentResult.StudentId JOIN AssessmentComponent ON StudentResult.AssessmentComponentId = AssessmentComponent.Id JOIN RubricLevel ON StudentResult.RubricMeasurementId = RubricLevel.Id JOIN Rubric ON RubricLevel.RubricId = Rubric.Id WHERE Rubric.CloId='{0}'", cid));
                    BindingSource S = new BindingSource();
                    S.DataSource = result;
                    dataGridViewrub.DataSource = S;


                    exportgridtopdf(dataGridViewrub, comboBoxClo.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnFilterAss_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxAss.Text == "")
                {
                    MessageBox.Show("Select Assessment First");
                }
                else
                {
                    //reading data from Assessment table
                    SqlDataReader data = DataConnection.get_instance().Getdata("SELECT * FROM Assessment");
                    while (data.Read())
                    {
                        if (data.GetString(1) == comboBoxAss.Text)
                        {
                            aid = Convert.ToInt32(data.GetValue(0));

                        }
                    }

                    SqlDataReader result = DataConnection.get_instance().Getdata(string.Format("SELECT Student.RegistrationNumber as RegNo, CONCAT(Student.FirstName,' ',Student.LastName) as Name, AssessmentComponent.Name as Component, AssessmentComponent.TotalMarks as TotalMarks, RubricLevel.MeasurementLevel as MeasurementLevel, ((RubricLevel.MeasurementLevel*AssessmentComponent.TotalMarks)/4) As ObtainedMarks  FROM Student JOIN StudentResult ON Student.Id = StudentResult.StudentId JOIN AssessmentComponent ON StudentResult.AssessmentComponentId = AssessmentComponent.Id JOIN RubricLevel ON StudentResult.RubricMeasurementId = RubricLevel.Id WHERE AssessmentComponent.AssessmentId='{0}'", aid));
                    BindingSource S = new BindingSource();
                    S.DataSource = result;
                    dataGridViewrub.DataSource = S;


                    exportgridtopdf(dataGridViewrub, comboBoxAss.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// show active student's list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            ActiveStudent a = new ActiveStudent();
            this.Hide();
            a.Show();
        }
    }
}
