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

namespace BEE.HoatDong.MGL.Ban
{
    public partial class frmPhanQuyen : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public frmPhanQuyen()
        {
            InitializeComponent();
        }

        private void frmPhanQuyen_Load(object sender, EventArgs e)
        {
            slookNhanVien.DataSource = db.NhanViens.Select(p => new { p.MaNV, p.MaSo, p.HoTen });
            gcPhanQuyen.DataSource = db.mglbcPhanQuyens;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                db.SubmitChanges();

                DialogBox.Infomation();

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        bool isDuplication(string fieldName, int index, object value)
        {
            var newValue = value.ToString();
            for (int i = 0; i < grvPhanQuyen.RowCount - 1; i++)
            {
                if (i == index) continue;
                var oldValue = grvPhanQuyen.GetRowCellValue(i, fieldName).ToString();
                if (oldValue == newValue) return true;
            }
            return false;
        }

        private void slookNhanVien_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var sp = (GridLookUpEditBase)sender;
                if (isDuplication("MaNV", grvPhanQuyen.FocusedRowHandle, sp.EditValue))
                {
                    DialogBox.Error("Trùng nhân viên, vui lòng chọn nhân viên khác");
                    grvPhanQuyen.SetFocusedRowCellValue("MaNV", grvPhanQuyen.GetFocusedRowCellValue("MaNV"));
                }
            }
            catch { }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var indexs = grvPhanQuyen.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn dòng để xóa");
                return;
            }

            if (DialogBox.Question() == DialogResult.No) return;
            foreach (var i in indexs)
            {
                mglbcPhanQuyen objBC = db.mglbcPhanQuyens.Single(p => p.ID == (int)grvPhanQuyen.GetRowCellValue(i, "ID"));
                db.mglbcPhanQuyens.DeleteOnSubmit(objBC);
                grvPhanQuyen.DeleteRow(i);
            }
            db.SubmitChanges();
            DialogBox.Infomation("Đã xóa thành công");
        }
    }
}