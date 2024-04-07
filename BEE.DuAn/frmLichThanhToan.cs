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

namespace BEE.DuAn
{
    public partial class frmLichThanhToan : DevExpress.XtraEditors.XtraForm
    {
        public int MaDA { get; set; }
        private MasterDataContext db = new MasterDataContext();

        public frmLichThanhToan()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
        }

        private void frmLichThanhToan_Load(object sender, EventArgs e)
        {
            lookKTT.DataSource = db.KieuThanhToans;
            gcTruongHop.DataSource = db.daTruongHopThanhToans.Where(p => p.MaDA == this.MaDA);
            lookUpOption.DataSource = db.daOptions;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            db.SubmitChanges();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grvTruongHop_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
            {
                gcLTT.DataSource = null;
                return;
            }
            daTruongHopThanhToan objTHTT = (daTruongHopThanhToan)grvTruongHop.GetFocusedRow();
            gcLTT.DataSource = objTHTT.daLichThanhToans;
        }

        private void grvTruongHop_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvTruongHop.SetFocusedRowCellValue("MaDA", this.MaDA);
            grvTruongHop.SetFocusedRowCellValue("MacDinh", false);
            grvTruongHop.SetFocusedRowCellValue("ChietKhau", (decimal)0);
            grvTruongHop.SetFocusedRowCellValue("TienThuong", (decimal)0);
        }

        bool trungTenTH(int rowIndex)
        {
            string value = grvTruongHop.GetRowCellValue(rowIndex, "TenTH").ToString().Trim();
            for (int i = 0; i < grvTruongHop.RowCount - 1; i++)
            {
                if (i != rowIndex && grvTruongHop.GetRowCellValue(i, "TenTH").ToString() == value)
                    return true;
            }
            return false;
        }

        private void grvTruongHop_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            if (grvTruongHop.GetRowCellValue(e.RowHandle, "TenTH") == null)
            {
                goto Result;
            }
            else if (grvTruongHop.GetRowCellValue(e.RowHandle, "TenTH").ToString().Trim() == "")
            {
                goto Result;
            }
            else if (trungTenTH(e.RowHandle))
            {
                e.ErrorText = "Trùng tên trường hợp";
                e.Valid = false;
            }

            return;

            Result:
                e.ErrorText = "Vui lòng nhập tên trường hợp";
                e.Valid = false;
        }

        private void grvTruongHop_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            DialogBox.Error(e.ErrorText);
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvTruongHop_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.Question() == DialogResult.No) 
                    return;
                grvTruongHop.DeleteSelectedRows();
            }
        }

        bool trungDotTT(int rowIndex)
        {
            string value = grvLTT.GetRowCellValue(rowIndex, "DotTT").ToString();
            for (int i = 0; i < grvLTT.RowCount - 1; i++)
            {
                if (i != rowIndex && grvLTT.GetRowCellValue(i, "DotTT").ToString() == value)
                    return true;
            }
            return false;
        }

        private void grvLTT_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            daLichThanhToan objLTT = (daLichThanhToan)e.Row;
            if( objLTT.DotTT == null)
            {
                e.ErrorText = "Vui lòng nhập đợt thanh toán";
                e.Valid = false;
            }
            else if(trungDotTT(e.RowHandle))
            {
                e.ErrorText = "Trùng đợt thanh toán";
                e.Valid = false;
            }
            else if (objLTT.TyLeTT == null)
            {
                e.ErrorText = "Vui lòng nhập tỷ lệ thanh toán";
                e.Valid = false;
            }
            else if (objLTT.TyLeVAT == null)
            {
                e.ErrorText = "Vui lòng nhập tỷ lệ VAT";
                e.Valid = false;
            }
            else if (objLTT.DienGiai == null || objLTT.DienGiai == "")
            {
                e.ErrorText = "Vui lòng nhập diễn giải";
                e.Valid = false;
            }
        }

        private void grvLTT_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            DialogBox.Error(e.ErrorText);
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvLTT_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.Question() == DialogResult.No)
                    return;
                grvLTT.DeleteSelectedRows();
            }
        }

        private void grvLTT_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvLTT.SetFocusedRowCellValue("MaKTT", 1);
            grvLTT.SetFocusedRowCellValue("SoNgay", 0);
            grvLTT.SetFocusedRowCellValue("OptionID", 1);
            grvLTT.SetFocusedRowCellValue("SoTien", 0);
        }
    }
}