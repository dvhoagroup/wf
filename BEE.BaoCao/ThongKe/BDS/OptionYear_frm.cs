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
    public partial class OptionYear_frm : DevExpress.XtraEditors.XtraForm
    {
        public bool IsRevenue = false;
        public OptionYear_frm()
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

            ChartByYear_frm frm = new ChartByYear_frm();
            if (IsRevenue)
                frm.QueryString = "BDS_DoanhThuTheoName " + int.Parse(cmbYear.SelectedItem.ToString());
            else
                frm.QueryString = "BDS_DaBanTheoName " + int.Parse(cmbYear.SelectedItem.ToString());
            frm.IsRevenue = IsRevenue;
            frm.Year = int.Parse(cmbYear.SelectedItem.ToString());
            frm.ShowDialog();
        }

        private void Options_frm_Load(object sender, EventArgs e)
        {
            for (int i = DateTime.Now.Year + 5; i >= DateTime.Now.Year - 5; i--)
                cmbYear.Properties.Items.Add(i);
            cmbYear.SelectedIndex = 5;
        }
    }
}