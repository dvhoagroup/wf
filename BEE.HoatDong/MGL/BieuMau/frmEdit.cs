using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using BEE.ThuVien;
using BEEREMA;

namespace BEE.HoatDong.MGL.BieuMau
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public int? MaBM { get; set; }

        MasterDataContext db = new MasterDataContext();
        mglBieuMau objBM;

        public frmEdit()
        {
            InitializeComponent();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            if (this.MaBM != null)
            {
                objBM = db.mglBieuMaus.Single(p => p.MaBM == this.MaBM);
                txtTenBM.EditValue = objBM.TenBM;
                txtDienGiai.EditValue = objBM.DienGiai;
                ckbKhoa.EditValue = objBM.Khoa;
            }
            else
            {
                objBM = new mglBieuMau();
            }
        }

        private void txtTenBM_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //using (DuAn.BieuMau.frmDesign frm = new DuAn.BieuMau.frmDesign())
            //{
            //    frm.RtfText = objBM.NoiDung;
            //    frm.ShowDialog();
            //    if (frm.DialogResult == DialogResult.OK)
            //    {
            //        objBM.NoiDung = frm.RtfText;
            //    }
            //}
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtTenBM.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập tên biểu mẫu");
                txtTenBM.Focus();
                return;
            }
            else
            {
                int count = db.mglBieuMaus.Where(p => p.TenBM == txtTenBM.Text.Trim() & p.MaBM != this.MaBM).Count();
                if (count > 0)
                {
                    DialogBox.Error("Trùng tên biểu mẫu, vui lòng nhập lại");
                    txtTenBM.Focus();
                    return;
                }
            }

            objBM.TenBM = txtTenBM.Text.Trim();
            objBM.DienGiai = txtDienGiai.Text;
            objBM.Khoa = ckbKhoa.Checked;
            objBM.NgayCN = DateTime.Now;
            objBM.MaNV = Common.StaffID;

            if (this.MaBM == null)
            {
                db.mglBieuMaus.InsertOnSubmit(objBM);
            }

            db.SubmitChanges();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}