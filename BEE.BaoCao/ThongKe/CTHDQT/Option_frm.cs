using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using LandSoft;

namespace BEE.BaoCao.ThongKe.CTHDQT
{
    public partial class Option_frm : DevExpress.XtraEditors.XtraForm
    {
        int MaDA = 0;
        public byte LoaiKH = 0;
        public Option_frm()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Option_frm_Load(object sender, EventArgs e)
        {
            LoadDuAn();
            dateThoiGian.DateTime = DateTime.Now;
            switch (LoaiKH)
            {
                case 1:
                    cmbLoaiKH.SelectedIndex = 0;
                    break;
                case 2:
                    cmbLoaiKH.SelectedIndex = 1;
                    break;
                case 3:
                    cmbLoaiKH.SelectedIndex = 2;
                    cmbLoaiKH.Enabled = false;
                    break;
            }
        }

        void LoadDuAn()
        {
            it.DuAnCls o = new it.DuAnCls();
            lookUpDuAn2.Properties.DataSource = o.SelectShow();
            lookUpDuAn2.ItemIndex = 0;
        }

        void LoadBlock()
        {
            it.BlocksCls o = new it.BlocksCls();
            lookUpBlock.Properties.DataSource = o.Select(MaDA);
            lookUpBlock.ItemIndex = 0;
        }

        private void lookUpDuAn_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit _DA = (LookUpEdit)sender;
            MaDA = int.Parse(_DA.EditValue.ToString());
            LoadBlock();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (dateThoiGian.Text == "")
            {
                DialogBox.Infomation("Vui lòng chọn <Thời gian>. Xin cảm ơn.");
                dateThoiGian.Focus();
                return;
            }

            if (cmbLoaiKH.SelectedIndex != 2)
            {
                ThongKe.CTHDQT.CongNo_CaNhan_rpt rpt = new CongNo_CaNhan_rpt(dateThoiGian.DateTime, int.Parse(lookUpBlock.EditValue.ToString()), lookUpBlock.Text, cmbLoaiKH.SelectedIndex == 0 ? true : false);
                rpt.ShowPreviewDialog();
            }
            else
            {
                ThongKe.CTHDQT.TongHopCongNo_rpt rpt = new TongHopCongNo_rpt(dateThoiGian.DateTime, int.Parse(lookUpBlock.EditValue.ToString()), lookUpBlock.Text);
                rpt.ShowPreviewDialog();
            }
        }

        private void cmbLoaiKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLoaiKH.SelectedIndex == 0)
                LoaiKH = 1;
            else
                LoaiKH = 2;
        }
    }
}