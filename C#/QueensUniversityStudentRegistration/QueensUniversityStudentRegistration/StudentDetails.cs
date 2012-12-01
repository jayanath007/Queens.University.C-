using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessTier;


namespace QueensUniversityStudentRegistration
{
    public partial class StudentDetails : Form
    {

        #region Variables
        //to manage update and insert
        static bool IsUpdate = false;
        public static List<Registration> NewRegistrations ;
        #endregion

        #region Form Load
        private void StudentDetails_Load(object sender, EventArgs e)
        {
            RefreshGrid();
            ClearFields();
            CusDataGridViewFormat();
        }
        public StudentDetails()
        {
            InitializeComponent();
     
        }
        #endregion


        #region Btn New
        private void btnNew_Click(object sender, EventArgs e)
        {
            StudentRegistration Registration = new StudentRegistration();
            StudentRegistration.studentID = 0;
            Registration.ShowDialog();
            RefreshGrid();
            btnSave.Focus();
        } 
        #endregion

        #region Btn Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (NewRegistrations != null && NewRegistrations.Count > 0)
                {
                    foreach (Registration Registration in NewRegistrations)
                    {
                        Registration oldRecord = Registration.Select(Registration.StudentID);
                        if (oldRecord != null)
                        {
                            Registration.Update();
                        }
                        else
                        {
                            Registration.Insert();
                        }
                    }
                    MessageBox.Show(" Save ", "Queens University Student Registration System ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    NewRegistrations.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                ClearFields();
                RefreshGrid();
            }
        }
        #endregion


        #region Clear Fields
        private void ClearFields()
        {
            txtStudentName.Clear();
        }
        #endregion
     
        #region Refresh Grid
        private void RefreshGrid()
        {

            int iRow;
            dgvDetail.Rows.Clear();
            List<Registration> details;
            if (NewRegistrations != null && NewRegistrations.Count > 0)
                details = NewRegistrations;
            else
                details = Registration.SelectAll().Where(p => p.Name.ToLower().Contains(txtStudentName.Text.ToLower())).ToList();

            foreach (Registration detail in details)
            {
                dgvDetail.Rows.Add();
                iRow = dgvDetail.Rows.Count - 1;
                dgvDetail["StudentID", iRow].Value = detail.StudentID;
                dgvDetail["StudentName", iRow].Value = detail.Name;
                dgvDetail["DateBirth", iRow].Value = detail.DateOfBirth.Date.ToString("dd-MMM-yyyy");
                dgvDetail["GPA", iRow].Value = detail.GradePointAvg;
                dgvDetail["Active", iRow].Value = detail.Active ? "Active" : "Deactivate";
            }
        }
        #endregion

        #region Datagrid Format
        private void CusDataGridViewFormat()
        {
            ClassHelp.ApplyGridFormat(dgvDetail);
        }
        #endregion


        #region Events Text Changed
        private void txtStudentName_TextChanged(object sender, EventArgs e)
        {
            RefreshGrid();
        } 
        #endregion

        #region Event DataGrid
        private void dgvDetail_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int sID = int.Parse(dgvDetail["StudentID", e.RowIndex].Value.ToString());
                if (sID > 0)
                {
                    //fills the values to controls
                    StudentRegistration Registration = new StudentRegistration();
                    StudentRegistration.studentID = sID;
                    Registration.ShowDialog();
                    RefreshGrid();
                    btnSave.Focus();
                }
            }
        }
        #endregion

    }
}
