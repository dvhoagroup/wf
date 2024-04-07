using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using LandSoft;

namespace BEE.BaoCao.ThongKe.BDS
{
    public partial class Options_frm : DevExpress.XtraEditors.XtraForm
    {
        public bool IsRevenue = false;
        public Options_frm()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (cmbYear.Text == "<Chọn năm>")
            {
                DialogBox.Infomation("Vui lòng chọn <Năm>, xin cảm ơn.");
                cmbYear.Focus();
                return;
            }

            if (cmbMonth.Text == "<Chọn tháng>")
            {
                DialogBox.Infomation("Vui lòng chọn <Tháng>, xin cảm ơn.");
                cmbMonth.Focus();
                return;
            }

            ChartByMonth_frm frm = new ChartByMonth_frm();
            if (IsRevenue)
                frm.QueryString = "BDS_DoanhThuTheoThang " + (cmbMonth.SelectedIndex + 1) + "," + int.Parse(cmbYear.SelectedItem.ToString());
            else
                frm.QueryString = "BDS_DaBanTheoThang " + (cmbMonth.SelectedIndex + 1) + "," + int.Parse(cmbYear.SelectedItem.ToString());
            frm.IsRevenue = IsRevenue;
            frm.Month = cmbMonth.SelectedIndex + 1;
            frm.Year = int.Parse(cmbYear.SelectedItem.ToString());
            frm.Amount = GetDay(cmbMonth.SelectedIndex + 1, int.Parse(cmbYear.SelectedItem.ToString()));
            frm.ShowDialog();
        }

        private void Options_frm_Load(object sender, EventArgs e)
        {
            for (int i = 1; i <= 12; i++)
                cmbMonth.Properties.Items.Add("Tháng " + i);
            cmbMonth.SelectedIndex = DateTime.Now.Month - 1;

            for (int i = DateTime.Now.Year + 5; i >= DateTime.Now.Year - 5; i--)
                cmbYear.Properties.Items.Add(i);
            cmbYear.SelectedIndex = 5;
        }

        byte GetDay(int Month, int Year)
        {
            byte temp = 0;
            switch (Month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12://31 ngay
                    temp = 31;
                    break;
                case 4:
                case 6:
                case 9:
                case 11://30 ngay
                    temp = 30;
                    break;
                case 2:
                    if (DateTime.IsLeapYear(Year))
                        temp = 29;
                    else
                        temp = 28;
                    break;
            }
            return temp;
        }
    }
}