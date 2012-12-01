
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Data.SqlClient;

using System.Threading;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using System.Drawing;


namespace QueensUniversityStudentRegistration
{
    class ClassHelp
    {
        #region  Grid Formats
        public static void ApplyGridFormat(DataGridView datag)
        {
            datag.RowHeadersVisible = false;
            datag.AllowUserToResizeRows = false;
            datag.MultiSelect = false;
            datag.ReadOnly = true;
            datag.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            datag.AllowUserToAddRows = false;


            datag.RowsDefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);
            datag.RowsDefaultCellStyle.Font = new Font("Calibri", 9, FontStyle.Bold);
            datag.RowsDefaultCellStyle.ForeColor = Color.FromArgb(99, 50, 50);
            datag.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(84, 141, 212);
            datag.RowsDefaultCellStyle.SelectionForeColor = Color.White;

            datag.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(180, 205, 205); //Color.FromKnownColor(KnownColor.DarkGray);
            datag.ColumnHeadersDefaultCellStyle.Font = new Font("Calibri", 9, FontStyle.Bold);
            datag.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black; //Color.FromKnownColor(KnownColor.WindowText);
            datag.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromKnownColor(KnownColor.Highlight);
            datag.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromKnownColor(KnownColor.HighlightText);
            datag.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        } 
        #endregion

        #region Allow Decimal With Length
        /// <summary>
        /// Allows only decimal values, and the same time it validate the leanth
        /// </summary>
        /// <param name="control"></param>
        /// <param name="e"></param>
        /// <param name="noOfIntegers">number of integers</param>
        /// <param name="noOfPrecisions">Number of Prcisions</param>
        public static void AllowDecimalWithLength(Control control, KeyPressEventArgs e, int noOfIntegers, int noOfPrecisions)
        {
            string decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;

            if ((47 < e.KeyChar && e.KeyChar < 58) || e.KeyChar == 8 || e.KeyChar.ToString() == decimalSeparator)
            {
                if (e.KeyChar.ToString() == decimalSeparator && control.Text != "")
                {
                    if (control.Text.Contains(decimalSeparator))
                    {
                        e.Handled = true;
                    }
                    else
                    {
                        CheckLenthForDecimal(control, e, noOfIntegers, noOfPrecisions);         //when double only ok
                    }
                }
                else
                {
                    CheckLenthForDecimal(control, e, noOfIntegers, noOfPrecisions);           //when double only ok
                }
            }
            else
            {
                e.Handled = true;
            }

        }
        #endregion

        #region Check Length for Decimal
        /// <summary>
        /// This is use by 'AllowDecimal' Method for validate leanth of the input
        /// </summary>
        /// <param name="control"></param>
        /// <param name="e"></param>
        /// <param name="noOfIntegers"></param>
        /// <param name="noOfPrecisions"></param>
        private static void CheckLenthForDecimal(Control control, KeyPressEventArgs e, int noOfIntegers, int noOfPrecisions)
        {
            int cursorPosistion = ((TextBox)control).SelectionStart;
            string decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;

            if (e.KeyChar.ToString() == decimalSeparator || e.KeyChar == 8 || ((TextBox)control).SelectionLength > 0)
            {
                e.Handled = false;
            }
            else
            {
                if (control.Text.Contains(decimalSeparator))
                {
                    if (cursorPosistion > control.Text.IndexOf(decimalSeparator))
                    {
                        if (control.Text.Substring(control.Text.IndexOf(decimalSeparator) + 1).Length > (noOfPrecisions - 1))
                        {
                            e.Handled = true;
                        }
                        else
                        {
                            e.Handled = false;
                        }
                    }
                    else
                    {
                        if (control.Text.Substring(0, (control.Text.IndexOf(decimalSeparator) + 1)).Length > noOfIntegers)
                        {
                            e.Handled = true;
                        }
                        else
                        {
                            e.Handled = false;
                        }
                    }

                }
                else
                {
                    if (control.Text.Length > (noOfIntegers - 1))
                    {
                        e.Handled = true;
                    }
                    else
                    {
                        e.Handled = false;
                    }
                }
            }
        }
        #endregion
    }
}
