using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using DevExpress.Utils;

namespace BEEREMA
{
    public class DialogBox
    {
        public static void Error(string text)
        {
            XtraMessageBox.Show(text, "BEEREM", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void Warning(string text)
        {
            XtraMessageBox.Show(text, "BEEREM", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Infomation()
        {
            XtraMessageBox.Show("Saved!", "BEEREM", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Save()
        {
            XtraMessageBox.Show("Saved!", "BEEREM", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Infomation(string infoString)
        {
            XtraMessageBox.Show(infoString, "BEEREM", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Default: Are you sure delete?
        /// </summary>
        /// <returns></returns>
        public static DialogResult Question()
        {
            return XtraMessageBox.Show("Bạn có chắc chắn muống xóa không?\r\n\r\nAre you sure delete?", "BEEREM", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static DialogResult Question(string questionString)
        {
            return XtraMessageBox.Show(questionString, "BEEREM", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static WaitDialogForm WaitingForm()
        {
            return new WaitDialogForm("Please wait ...", "BEEREM");
        }
    }
}
