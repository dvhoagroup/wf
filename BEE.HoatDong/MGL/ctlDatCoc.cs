using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using BEE.ThuVien;
using BEEREMA;

namespace BEE.HoatDong.MGL
{
    public partial class ctlDatCoc : DevExpress.XtraEditors.XtraUserControl
    {
        it.KyBaoCaoCls objKBC;
        MasterDataContext db;

        public ctlDatCoc()
        {
            InitializeComponent();

            objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBC);

            db = new MasterDataContext();
            lookTinhTrang.DataSource = db.mglTinhTrangs;
            lookTinhTrang2.DataSource = db.mglTinhTrangs;
        }


        void DatCoc_Load()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
                {
                    gcDatCoc.DataSource = db.mglDatCoc_getAll((DateTime)itemTuNgay.EditValue, (DateTime)itemDenNgay.EditValue);
                }
                else
                {
                    gcDatCoc.DataSource = null;
                }
            }
            finally
            {
                wait.Close();
            }
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            ComboBoxEdit cmd = (ComboBoxEdit)sender;
            objKBC.Index = cmd.SelectedIndex;
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);

            itemDenNgay.EditValue = objKBC.DateTo;
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            DatCoc_Load();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            DatCoc_Load();
        }


        private void itemThem_MuaBan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (frmDatCoc frm = new frmDatCoc())
            {
                frm.IsBan = true;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    DatCoc_Load();
            }
        }

        private void itemThem_ChoThue_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (frmDatCoc frm = new frmDatCoc())
            {
                frm.IsBan = false;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    DatCoc_Load();
            }
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DatCoc_Load();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvDatCoc.FocusedRowHandle >= 0)
            {
                using (frmDatCoc frm = new frmDatCoc())
                {
                    frm.MaDC = (int)grvDatCoc.GetFocusedRowCellValue("MaDC");
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                        DatCoc_Load();
                }
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] indexs = grvDatCoc.GetSelectedRows();

            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn những phiếu cần xóa");
                return;
            }

            if (DialogBox.Question() == DialogResult.No)
                return;

            foreach (int i in indexs)
            {
                var objLS = db.mglLichSus.Select(p => p).Where(p => p.MaDC == (int)grvDatCoc.GetRowCellValue(i, "MaDC"));
                db.mglLichSus.DeleteAllOnSubmit(objLS);
                mglDatCoc objDC = db.mglDatCocs.Single(p => p.MaDC == (int)grvDatCoc.GetRowCellValue(i, "MaDC"));
                db.mglDatCocs.DeleteOnSubmit(objDC);
            }
            db.SubmitChanges();

            DialogBox.Infomation("Đã xóa những dòng đã chọn");

            DatCoc_Load();
        }

        private void itemPrint_List_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gcDatCoc.ShowPrintPreview();
        }

        void Duyet(byte maTT)
        {
            int[] indexs = grvDatCoc.GetSelectedRows();

            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn một hoặc nhiều phiếu");
                return;
            }

            using (frmDuyet frm = new frmDuyet())
            {
                frm.MaTT = maTT;
                frm.DatCocs = new int[indexs.Length];
                for (int i = 0; i < indexs.Length; i++)
                    frm.DatCocs[i] = (int)grvDatCoc.GetRowCellValue(indexs[i], "MaDC");
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    DatCoc_Load();
            }
        }

        private void itemKeToan_ChuaThu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Duyet(1);
        }

        private void itemKeToan_DaThu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Duyet(2);
        }

        private void itemGiamDoc_KhongThanhCong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Duyet(3);
        }

        private void itemGiamDoc_ThanhCong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Duyet(4);
        }

        private void itemSua_BieuMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvDatCoc.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn phiếu");
                return;
            }

            var objDC = db.mglDatCocs.Single(p => p.MaDC == (int)grvDatCoc.GetFocusedRowCellValue("MaDC"));

            BEE.NghiepVuKhac.Editor_frm frm = new BEE.NghiepVuKhac.Editor_frm();
            frm.Template = objDC.Template;
            frm.LoaiHD = 4;
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.OK)
            {
                objDC.Template = frm.Template;
                db.SubmitChanges();
            }
        }

        void LichSu_Load()
        {
            if (grvDatCoc.FocusedRowHandle >= 0)
            {
                gcLichSu.DataSource = from p in db.mglLichSus
                                      where p.MaDC == (int)grvDatCoc.GetFocusedRowCellValue("MaDC")
                                      orderby p.NgayCN descending
                                      select new { MaLS = p.MaLS, MaTT = p.MaTT, NgayCN=p.NgayCN, 
                                          DienGiai = p.DienGiai, HoTenNV = p.NhanVien.HoTen };
            }
            else
            {
                gcLichSu.DataSource = null;
            }
        }

        private void grvLichSu_DoubleClick(object sender, EventArgs e)
        {
            using (frmDuyet frm = new frmDuyet())
            {
                frm.MaLS = (int)grvLichSu.GetFocusedRowCellValue("MaLS");
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LichSu_Load();
            }
        }

        private void grvDatCoc_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LichSu_Load();
        }
    }
}
