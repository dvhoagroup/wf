using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using LandSoft;

namespace BEE.BaoCao.BDS
{
    public partial class OptionLo_frm : DevExpress.XtraEditors.XtraForm
    {
        int MaDA = 0;
        public byte MaLoai = 0;
        public OptionLo_frm()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lookUpDuAn_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit _New = (LookUpEdit)sender;
            MaDA = int.Parse(_New.EditValue.ToString());
            LoadKhu();
        }

        void LoadDA()
        {
            it.DuAnCls o = new it.DuAnCls();
            lookUpDuAn.Properties.DataSource = o.SelectShow();
        }

        void LoadKhu()
        {
            it.KhuCls o = new it.KhuCls();
            o.MaDA = MaDA;
            lookUpKhu.Properties.DataSource = o.SelectByMaDA();
            lookUpKhu.ItemIndex = 0;
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (lookUpDuAn.Text == "")
            {
                DialogBox.Infomation("Vui lòng chọn <Dự án>. Xin cảm ơn.");
                lookUpDuAn.Focus();
                return;
            }

            if (lookUpKhu.Text == "")
            {
                DialogBox.Infomation("Vui lòng chọn <Khu>. Xin cảm ơn.");
                lookUpKhu.Focus();
                return;
            }

            switch (MaLoai)
            {
                case 1://Lo da giu cho
                    DaGiuCho_rpt rpt = new DaGiuCho_rpt(lookUpDuAn.EditValue.ToString(), lookUpKhu.EditValue.ToString());
                    rpt.ShowPreviewDialog();
                    break;
                case 2://Lo da ky HDVV
                    DaKyHDVV_rpt rpt2 = new DaKyHDVV_rpt(lookUpDuAn.EditValue.ToString(), lookUpKhu.EditValue.ToString());
                    rpt2.ShowPreviewDialog();
                    break;
                case 3://Lo trong
                    LoTrong_rpt rpt3 = new LoTrong_rpt(lookUpDuAn.EditValue.ToString(), lookUpKhu.EditValue.ToString());
                    rpt3.ShowPreviewDialog();
                    break;
            }
        }

        private void OptionLo_frm_Load(object sender, EventArgs e)
        {
            LoadDA();
        }
    }
}