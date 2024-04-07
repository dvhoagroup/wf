using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BEEREMA.Test
{
    public partial class Option_frm : DevExpress.XtraEditors.XtraForm
    {
        public Option_frm()
        {
            InitializeComponent();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (cmbToDay.SelectedIndex > cmbFromDay.SelectedIndex)
            {
                DialogBox.Infomation("<Từ ngày> phải lớn hơn hoặc bằng <Đến ngày>. Vui lòng chọn lại, xin cảm ơn.");
                return;
            }
            CongNo_frm frm = new CongNo_frm();
            frm.TuNgay = cmbToDay.SelectedIndex + 1;
            frm.DenNgay = cmbFromDay.SelectedIndex + 1;
            frm.Nam = int.Parse(cmbYear.SelectedItem.ToString());
            frm.Thang = cmbMonth.SelectedIndex + 1;
            frm.ShowDialog();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void AddDay(int val)
        {
            cmbToDay.Properties.Items.Clear();
            cmbFromDay.Properties.Items.Clear();
            for (int i = 1; i <= val; i++)
            {
                cmbToDay.Properties.Items.Add(i);
                cmbFromDay.Properties.Items.Add(i);
            }
        }

        private void Option_frm_Shown(object sender, EventArgs e)
        {
            for (int i = 1; i <= 12; i++)
                cmbMonth.Properties.Items.Add("Tháng " + i);

            for (int i = DateTime.Now.Year; i >= DateTime.Now.Year - 5; i--)
                cmbYear.Properties.Items.Add(i);           
        }

        void SetDay(int Month, int Year)
        {
            switch (Month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 10:
                case 12://31 ngay
                    AddDay(31);
                    break;
                case 4:
                case 6:
                case 9:
                case 11://30 ngay
                    AddDay(30);
                    break;
                case 2:
                    if (DateTime.IsLeapYear(Year))
                        AddDay(29);
                    else
                        AddDay(28);
                    break;
            }
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxEdit _cmbMonth = (ComboBoxEdit)sender;
            if (cmbYear.Text != "")
                SetDay(_cmbMonth.SelectedIndex + 1, int.Parse(cmbYear.SelectedItem.ToString()));
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxEdit _cmbYear = (ComboBoxEdit)sender;
            if (cmbYear.Text != "")
                SetDay(cmbMonth.SelectedIndex + 1, int.Parse(_cmbYear.SelectedItem.ToString()));
        }
    }
}