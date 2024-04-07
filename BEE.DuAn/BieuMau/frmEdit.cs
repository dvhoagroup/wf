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

namespace BEE.DuAn.BieuMau
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public int MaBM, MaDA;
        MasterDataContext db = new MasterDataContext();
        daBieuMau objBM;

        public frmEdit()
        {
            InitializeComponent();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            lookLoaiBM.Properties.DataSource = db.daLoaiBieuMaus;
            if (this.MaBM != 0)
            {
                objBM = db.daBieuMaus.Single(p => p.MaBM == this.MaBM);
                txtTenBM.EditValue = objBM.TenBM;
                lookLoaiBM.EditValue = objBM.MaLBM;
                txtDienGiai.EditValue = objBM.DienGiai;
                ckbKhoa.EditValue = objBM.Khoa;
                lookUpThaoTac.EditValue = objBM.MaTTac;
                txtTenLM.Text = objBM.TenLM;
            }
            else
            {
                objBM = new daBieuMau();
            }

            BEE.NgonNgu.Language.TranslateControl(this);
        }

        private void txtTenBM_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (frmDesign frm = new frmDesign())
            {
                frm.RtfText = objBM.NoiDung;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    objBM.NoiDung = frm.RtfText;
                }
            }
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
                int count = db.daBieuMaus.Where(p => p.MaDA == this.MaDA & p.TenBM == txtTenBM.Text.Trim() & p.MaBM != this.MaBM).Count();
                if (count > 0)
                {
                    DialogBox.Error("Trùng tên biểu mẫu, vui lòng nhập lại");
                    txtTenBM.Focus();
                    return;
                }
            }

            if (lookLoaiBM.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn loại biểu mẫu");
                lookLoaiBM.Focus();
                return;
            }

            objBM.TenBM = txtTenBM.Text.Trim();
            objBM.MaLBM = (byte)lookLoaiBM.EditValue;
            objBM.DienGiai = txtDienGiai.Text;
            objBM.Khoa = ckbKhoa.Checked;
            objBM.NgayCN = DateTime.Now;
            objBM.MaNV = Common.StaffID;
            objBM.MaTTac = (int?)lookUpThaoTac.EditValue;
            objBM.TenLM = txtTenLM.Text.Trim();

            if (this.MaBM == 0)
            {
                objBM.MaDA = this.MaDA;
                db.daBieuMaus.InsertOnSubmit(objBM);
            }

            db.SubmitChanges();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lookLoaiBM_EditValueChanged(object sender, EventArgs e)
        {
            lookUpThaoTac.Properties.DataSource = db.nvFormActions.Where(p => p.FormID == Convert.ToInt32(lookLoaiBM.EditValue))
                .Select(p => new { p.ActionID, p.No, p.nvThaoTac.Name, p.nvThaoTac.ID });
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}