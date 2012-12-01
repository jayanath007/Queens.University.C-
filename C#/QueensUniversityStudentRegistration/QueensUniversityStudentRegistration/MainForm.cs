using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QueensUniversityStudentRegistration
{
    public partial class MainForm : Form
    {

        #region Form Load
        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {

        } 
        #endregion

        #region Menu Items 
        private void newRegistrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StudentDetails frmStudentDetails = new StudentDetails();
            frmStudentDetails.MdiParent = this;
            frmStudentDetails.Show();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

    }
}
