using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;

namespace BEE.NghiepVuKhac
{
    public partial class KyKinhDoanh_frm : DevExpress.XtraEditors.XtraForm
    {
        public int KeyID = 0;
        public byte STT = 0;
        public bool IsUpdate = false;
        public KyKinhDoanh_frm()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Huong_frm_Load(object sender, EventArgs e)
        {
            it.ChiTieuBanHangCls objDA = new it.ChiTieuBanHangCls();
            lookUpDuAn.Properties.DataSource = objDA.SelectShow();

            if (STT != 0)
            {
                it.KyKinhDoanhCls o = new it.KyKinhDoanhCls(KeyID, STT);
                spinSLMax.EditValue = o.SLMax;
                spinSLMin.EditValue = o.SLMin;
                spinMucPhi.EditValue = o.MucPhi;
                lookUpDuAn.EditValue = o.ChiTieu.MaCT;
                spinSLMax.Tag = o.STT;
            }
            else
                lookUpDuAn.ItemIndex = 0;
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (lookUpDuAn.Text == "")
            {
                DialogBox.Infomation("Vui lòng chọn chỉ tiêu. Xin cảm ơn");
                lookUpDuAn.Focus();
                return;
            }

            it.KyKinhDoanhCls o = new it.KyKinhDoanhCls();
            o.SLMin = int.Parse(spinSLMin.EditValue.ToString());
            o.SLMax = int.Parse(spinSLMax.EditValue.ToString());
            o.MucPhi = float.Parse(spinMucPhi.EditValue.ToString());
            o.ChiTieu.MaCT = KeyID;
            if (STT != 0)
            {                
                o.STT = byte.Parse(spinSLMax.Tag.ToString());
                o.Update();                
            }
            else
                o.Insert();
            IsUpdate = true;
            DialogBox.Infomation("Dữ liệu đã cập nhật thành công.");
            this.Close();
        }
    }
}