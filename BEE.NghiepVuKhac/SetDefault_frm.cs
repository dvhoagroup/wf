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
    public partial class SetDefault_frm : DevExpress.XtraEditors.XtraForm
    {
        public byte KeyID = 0;
        public bool IsUpdate = false;
        public SetDefault_frm()
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
            it.SetDefaultCls o = new it.SetDefaultCls(KeyID);
            spinSoTien.EditValue = o.SoTien;
            spinThoiGian.EditValue = o.ThoiGian;
            if (KeyID == 1)//Phieu giu cho                
                lblThoiGian.Text = "(giờ)";
            else//Phieu dat coc
                lblThoiGian.Text = "(ngày)";
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            it.SetDefaultCls o = new it.SetDefaultCls();
            o.ThoiGian = byte.Parse(spinThoiGian.EditValue.ToString());
            o.SoTien = double.Parse(spinSoTien.EditValue.ToString());
            o.SetID = KeyID;
            o.Update();

            DialogBox.Infomation("Dữ liệu đã cập nhật thành công.");
            this.Close();
        }
    }
}