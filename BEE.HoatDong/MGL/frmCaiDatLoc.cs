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

namespace BEE.HoatDong.MGL
{
    public partial class frmCaiDatLoc : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public frmCaiDatLoc()
        {
            InitializeComponent();
            db = new MasterDataContext();
        }

        private void gvCaiDat_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvCaiDat.SetRowCellValue(0, "MaNV", Common.StaffID);
        }

        private void frmCaiDatLoc_Load(object sender, EventArgs e)
        {

            try
            {
                slookNhanVien.DataSource = lookNhanVien.DataSource = db.NhanViens.Where(p => p.MaTinhTrang == 1).Select(p => new { p.MaNV, p.MaSo, p.HoTen });
                if (Common.PerID == 1)
                    gcCaiDat.DataSource = db.mglCaiDatTimKiems;//.Where(p => p.MaNV == Common.StaffID);
                else
                    gcCaiDat.DataSource = db.mglCaiDatTimKiems.Where(p => p.MaNV == Common.StaffID);
            }
            catch
            {
            }
        }

        private void btnTHoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            db.SubmitChanges();

            DialogBox.Infomation("Dữ liệu đã lưu thành công");
            this.Close();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var indexs = gvCaiDat.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn [Tiêu chí], xin cảm ơn.");
                return;
            }
            if (DialogBox.Question() == DialogResult.No) return;
            foreach (var i in indexs)
            {
                var objKH = db.mglCaiDatTimKiems.Single(p => p.ID == (int?)gvCaiDat.GetRowCellValue(i, "ID"));
                db.mglCaiDatTimKiems.DeleteOnSubmit(objKH);
            }
            db.SubmitChanges();
            gvCaiDat.DeleteSelectedRows();
        }

        bool isDuplication(string fieldName, int index, object value)
        {
            var newValue = value.ToString();
            for (int i = 0; i < gvCaiDat.RowCount - 1; i++)
            {
                if (i == index) continue;
                var oldValue = gvCaiDat.GetRowCellValue(i, fieldName).ToString();
                if (oldValue == newValue) return true;
            }
            return false;
        }

        private void lookNhanVien_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void slookNhanVien_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var sp = (GridLookUpEditBase)sender;
                if (isDuplication("MaNV", gvCaiDat.FocusedRowHandle, sp.EditValue))
                {
                    DialogBox.Error("Trùng nhân viên, vui lòng chọn nhân viên khác");
                    gvCaiDat.SetFocusedRowCellValue("MaNV", gvCaiDat.GetFocusedRowCellValue("MaNV"));
                }
            }
            catch { }
        }
    }
}