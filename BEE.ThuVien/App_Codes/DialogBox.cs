using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.LookAndFeel;
using System.Drawing;

namespace BEEREMA
{
    public class DialogBox
    {
        public static void Error(string text)
        {
            XtraMessageBox.Show(text, "HOALAND", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void Warning(string text)
        {
            XtraMessageBox.Show(text, "HOALAND", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void Infomation()
        {
            XtraMessageBox.Show("Saved!", "HOALAND", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Save()
        {
            XtraMessageBox.Show("Saved!", "HOALAND", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Infomation(string infoString)
        {
            XtraMessageBox.Show(infoString, "HOALAND", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void InfomationColor(string infoString, string color)
        {
            var l = new UserLookAndFeel(Color.DarkRed);

            XtraMessageBox.Show(l, infoString, "HOALAND", MessageBoxButtons.OK);
        }

        /// <summary>
        /// Default: Are you sure delete?
        /// </summary>
        /// <returns></returns>
        public static DialogResult Question()
        {
            return XtraMessageBox.Show("Bạn có chắc chắn muốn xóa không?\r\n\r\nAre you sure delete?", "HOALAND", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static DialogResult Question(string questionString)
        {
            return XtraMessageBox.Show(questionString, "HOALAND", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static WaitDialogForm WaitingForm()
        {
            return new WaitDialogForm("Please wait ...", "HOALAND");
        }
        public static DialogResult QuestionNotApproved()
        {
            return XtraMessageBox.Show("Bạn có chắc chắn không muốn xóa không?\r\n", "HOALAND", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
    }
}
