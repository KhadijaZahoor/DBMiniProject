using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBProject
{
    public partial class MainPage : Form
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddStudent frm = new AddStudent();
            this.Hide();
            frm.Show();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            ViewStudent frm = new ViewStudent();
            this.Hide();
            frm.Show();
        }
    }
}
