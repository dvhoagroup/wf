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
    public partial class frmCaiDatThuTu : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public frmCaiDatThuTu()
        {
            InitializeComponent();
        }

        private void frmCaiDatThuTu_Load(object sender, EventArgs e)
        {
            gcCanBan.DataSource = db.mglbcThuTuCotCBs.OrderBy(p => p.STT).Where(p => p.ID != 1);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            db.SubmitChanges();
            DialogBox.Infomation();
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        bool isDuplication(string fieldName, int index, object value)
        {
            var newValue = value.ToString();
            for (int i = 0; i < grvCanBan.RowCount - 1; i++)
            {
                if (i == index) continue;
                var oldValue = grvCanBan.GetRowCellValue(i, fieldName).ToString();
                if (oldValue == newValue) return true;
            }
            return false;
        }

        private void grvCanBan_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var sl = (int?)grvCanBan.GetRowCellValue(e.RowHandle, "STT");
            if (sl == null)
            {
                e.ErrorText = "Vui lòng nhập STT";
                e.Valid = false;
                return;
            }
            
        }

        private void grvCanBan_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            DialogBox.Error(e.ErrorText);
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void spinSTT_EditValueChanged(object sender, EventArgs e)
        {
            //if (isDuplication("STT", grvCanBan.FocusedRowHandle, grvCanBan.GetFocusedRowCellValue("STT")))
            //{
            //    DialogBox.Error("Trùng STT, vui lòng nhập STT khác");
            //    grvCanBan.SetFocusedRowCellValue("STT", grvCanBan.GetFocusedRowCellValue("STT"));
            //}
        }
    }
}