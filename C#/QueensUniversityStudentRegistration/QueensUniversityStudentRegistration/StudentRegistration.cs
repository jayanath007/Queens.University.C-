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
    public partial class StudentRegistration : Form
    {

        #region Variables
        //to manage update and insert
        static bool IsUpdate = false;
        public static int studentID = 0;
        List<Registration> NewRegistrations =  new List<Registration>();
        #endregion

        #region Form Load
        public StudentRegistration()
        {
            InitializeComponent();
        }
        private void StudentRegistration_Load(object sender, EventArgs e)
        {
            if (studentID > 0)
            {
                FillDetails(studentID);
            }
            else
            {
                ClearFields();
            }
        } 
        #endregion


        #region Btn New
        private void btnNew_Click(object sender, EventArgs e)
        {
            AddRegistration();
        }
        #endregion

        #region Btn Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (txtStudentID.TextLength > 0)
                {
                    if (IsUpdate)  //update records
                    {
                        Registration oldRecord = Registration.Select(int.Parse(txtStudentID.Text.Trim()));
                        if (oldRecord != null)
                        {
                            oldRecord.Name = txtStudentName.Text.Trim();
                            oldRecord.DateOfBirth = dtpDateOfBirth.Value;
                            oldRecord.GradePointAvg = decimal.Parse(txtGPA.Text);
                            oldRecord.Active = chkActive.Checked; ;
                            oldRecord.Update();
                            MessageBox.Show(" Update Recode ", "Queens University Student Registration System ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else  //insert records
                    {
                        foreach (Registration New in NewRegistrations)
                        {
                            New.Insert();
                        }
                        MessageBox.Show(" Save ", "Queens University Student Registration System ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
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
  
            }
        }
        #endregion

        #region Btn OK
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtStudentName.TextLength > 0)
            {
                AddRegistration();
            }
            StudentDetails.NewRegistrations = NewRegistrations;
            this.Close();
        }
        #endregion

        #region btn Cancel
        private void btnCancel_Click(object sender, EventArgs e)
        {
            NewRegistrations.Clear();
            StudentDetails.NewRegistrations = NewRegistrations;
            this.Close();
        }
        #endregion


        #region Event KeyPress
        private void txtGPA_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassHelp.AllowDecimalWithLength((TextBox)sender, e, 1, 2);
        }
        #endregion


        #region Fill Details
        private void FillDetails(int sID)
        {
            if (sID > 0)
            {
                Registration detail = Registration.Select(sID);
                if (detail != null)
                {
                    //set the update flag and Locked
                    IsUpdate = true;
                    //asign values
                    txtStudentID.Text = detail.StudentID.ToString();
                    txtStudentName.Text = detail.Name;
                    dtpDateOfBirth.Value = detail.DateOfBirth;
                    txtGPA.Text = detail.GradePointAvg.ToString();
                    chkActive.Checked = detail.Active;
                }
            }
        }
        #endregion

        #region Clear Fields
        private void ClearFields()
        {
            //set the flag and enble the id
            IsUpdate = false;
           

                txtStudentName.Clear();
                txtStudentID.Text = "<Auto Generate>";

                txtStudentName.Clear();
                txtGPA.Clear();
                chkActive.Checked = true;
        }
        #endregion

        #region Check Validity
        private bool CheckValidity()
        {
            string strMessage = "";
            bool bStatus = true;

            if (txtStudentName.TextLength == 0)
            {
                strMessage += "\n" + "Student Name ";
                bStatus = false;
            }
            if (txtGPA.TextLength == 0)
            {
                //strMessage += "\n" + "Grade Point Avg  ";
                //bStatus = false;
                txtGPA.Text = "0";
            }
            if (bStatus == false)
            {
                MessageBox.Show("Please Check the Following Field(s) As It Cannot Be Empty !!!! " + strMessage, "Queens University Student Registration System ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return bStatus;
        }
        #endregion


        #region Add Registration
        private void AddRegistration()
        {
            if (CheckValidity())
            {
                Registration StudentRegistration;
                if (txtStudentID.Text == "<Auto Generate>")
                {
                    StudentRegistration = new Registration(txtStudentName.Text.Trim(), dtpDateOfBirth.Value, decimal.Parse(txtGPA.Text), chkActive.Checked);
                }
                else
                {
                    StudentRegistration = new Registration(int.Parse(txtStudentID.Text), txtStudentName.Text.Trim(), dtpDateOfBirth.Value, decimal.Parse(txtGPA.Text), chkActive.Checked);
                }
                NewRegistrations.Add(StudentRegistration);
                ClearFields();
            }
        } 
        #endregion


    }
}
