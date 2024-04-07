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
    public partial class OptionTonKho_frm : DevExpress.XtraEditors.XtraForm
    {
        int MaDA = 0;
        public byte LoaiKH = 0;
        public OptionTonKho_frm()
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
        }

        void LoadDuAn()
        {
            it.DuAnCls o = new it.DuAnCls();
            lookUpDuAn2.Properties.DataSource = o.SelectShow();
            lookUpDuAn2.ItemIndex = 0;
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (dateThoiGian.Text == "")
            {
                DialogBox.Infomation("Vui lòng chọn <Thời gian>. Xin cảm ơn.");
                dateThoiGian.Focus();
                return;
            }

            ThongKe.CTHDQT.TonKho_rpt rpt = new TonKho_rpt(int.Parse(lookUpDuAn2.EditValue.ToString()), dateThoiGian.DateTime);
            rpt.ShowPreviewDialog();
        }
    }
}