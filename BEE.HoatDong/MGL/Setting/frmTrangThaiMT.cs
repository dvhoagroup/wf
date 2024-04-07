using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;
using BEE.ThuVien;
using System.Linq;

namespace BEE.HoatDong.MGL.Setting
{
    public partial class frmTrangThaiMT : DevExpress.XtraEditors.XtraForm
    {
        public byte MaTT = 0;
        public bool IsUpdate = false;
        public bool flagAddnew;
        public frmTrangThaiMT()
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
            if (flagAddnew != true)
                using (var db = new MasterDataContext())
                {
                    var objT = db.mglmtTrangThais.FirstOrDefault(p => p.MaTT == MaTT);
                    txtTenHuong.Text = objT.TenTT;
                    txtColor.EditValue = objT.MauNen;
                    spinOder.EditValue = objT.STT;
                }
            txtTenHuong.Focus();


        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtTenHuong.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập tên trạng thái. Xin cảm ơn");
                txtTenHuong.Focus();
                return;
            }

            using (var db = new MasterDataContext())
            {
                if (flagAddnew == true)
                {
                    var objT = new mglmtTrangThai();
                    byte _matt = db.mglmtTrangThais.Max(p => p.MaTT);
                    _matt++;
                    objT.TenTT = txtTenHuong.Text;
                    objT.MauNen = (int?)txtColor.EditValue;
                    objT.MaTT = _matt;
                    if (spinOder.EditValue != null)
                    {
                        objT.STT = Convert.ToByte(spinOder.EditValue);
                    }

                    db.mglmtTrangThais.InsertOnSubmit(objT);

                }
                else
                {
                    var objT = db.mglmtTrangThais.FirstOrDefault(p => p.MaTT == MaTT);
                    if (objT.TenTT != null || objT.MaTT == 0) // có trạng thái = 0 vì vậy phải cheat
                    {
                        objT.TenTT = txtTenHuong.Text;
                        objT.MauNen = (int?)txtColor.EditValue;
                        if (spinOder.EditValue != null)
                        {
                            objT.STT = Convert.ToByte(spinOder.EditValue);
                        }
                    }
                }


                db.SubmitChanges();
            }
            IsUpdate = true;
            DialogBox.Infomation("Dữ liệu đã cập nhật thành công.");
            this.Close();
        }
    }
}