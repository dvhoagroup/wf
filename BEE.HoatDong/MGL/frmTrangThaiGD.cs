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

namespace BEE.HoatDong.MGL
{
    public partial class frmTrangThaiGD : DevExpress.XtraEditors.XtraForm
    {
        public byte KeyID = 0;
        public bool IsUpdate = false;
        public bool flagAddnew;
        public frmTrangThaiGD()
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
            {
                using (var db = new MasterDataContext())
                {
                    var objT = db.mglTrangThaiGiaoDiches.FirstOrDefault(p => p.MaLoai == KeyID);
                    txtTenHuong.Text = objT.TenTT;
                    txtColor.Text = objT.Color;
                    spinOder.EditValue = objT.Ord;
                    txtCode.Text = objT.Code;
                    spinOverTime.EditValue = objT.OverTime;
                }
            }
            txtTenHuong.Focus();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtTenHuong.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập tên trạng thái giao dịch. Xin cảm ơn");
                txtTenHuong.Focus();
                return;
            }

            using (var db = new MasterDataContext())
            {

                if (flagAddnew == true)
                {
                    var objT = new mglTrangThaiGiaoDich();
                    byte _matt = db.mglTrangThaiGiaoDiches.Max(p => p.MaLoai);
                    _matt++;
                    objT.TenTT = txtTenHuong.Text;
                    objT.Color = txtColor.Text;
                    objT.Code = txtCode.Text;
                    objT.MaLoai = _matt;
                    if (spinOverTime.EditValue != null)
                    {
                        objT.OverTime = Convert.ToInt32(spinOverTime.EditValue);
                    }

                    if (spinOder.EditValue != null)
                    {
                        objT.Ord = Convert.ToByte(spinOder.EditValue);
                    }

                    db.mglTrangThaiGiaoDiches.InsertOnSubmit(objT);


                }
                else
                {
                    var objT = db.mglTrangThaiGiaoDiches.FirstOrDefault(p => p.MaLoai == KeyID);
                    objT.TenTT = txtTenHuong.Text;
                    objT.Color = txtColor.Text;
                    objT.Code = txtCode.Text;
                    if(spinOverTime.EditValue != null)
                    {
                        objT.OverTime = Convert.ToInt32(spinOverTime.EditValue);
                    }
                   
                    if (spinOder.EditValue != null)
                    {
                        objT.Ord = Convert.ToByte(spinOder.EditValue);
                    }

                }
                db.SubmitChanges();
            }
            IsUpdate = true;
            DialogBox.Infomation("Dữ liệu đã cập nhật thành công.");
            this.Close();
        }

        private void spinOverTime_EditValueChanged(object sender, EventArgs e)
        {
           
            var ng = Convert.ToInt32(spinOverTime.EditValue) / 24;
            if (ng >= 1)
            {
                lbNgay.Visible = true;
                try
                {
                    lbNgay.Text = (ng).ToString() + " Ngày";
                }
                catch
                {

                }
            }
            else
                lbNgay.Visible = false;
        }
    }
}