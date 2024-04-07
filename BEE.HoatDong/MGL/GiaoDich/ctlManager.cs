using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Data.Linq.SqlClient;
using BEE.ThuVien;
using BEEREMA;

namespace BEE.HoatDong.MGL.GiaoDich
{
    public partial class ctlManager : DevExpress.XtraEditors.XtraUserControl
    {
        MasterDataContext db = new MasterDataContext();

        public ctlManager()
        {
            InitializeComponent();
           lookTrangThaiNK.DataSource = lookTrangThai.DataSource = db.mglgdTrangThais.OrderBy(p=>p.STT);

            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);
        }

        void LoadPermission()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = Common.PerID;
            o.AccessData.Form.FormID = 180;
            DataTable tblAction = o.SelectBy();
            itemSua.Enabled = false;
            itemXoa.Enabled = false;
            itemDuyet.Enabled = false;
            itemKhongDuyet.Enabled = false;
            itemDaThuTien.Enabled = false;
            itemChuaThuTien.Enabled = false;
            itemTHuChuaDu.Enabled = false;
            itemGiaoDichThanhCong.Enabled = false;

            if (tblAction.Rows.Count > 0)
            {
                foreach (DataRow r in tblAction.Rows)
                {
                    switch (byte.Parse(r["FeatureID"].ToString()))
                    {
                        case 2:
                            itemSua.Enabled = true;
                            break;
                        case 3:
                            itemXoa.Enabled = true;
                            break;
                        case 7://duyet thong tin
                            itemDuyet.Enabled = true;
                            itemKhongDuyet.Enabled = true;
                            break;
                        case 8://Xác nhận thu tiền
                            itemTHuChuaDu.Enabled = true;
                            itemChuaThuTien.Enabled = true;
                            break;
                        case 44:// xác nhận hoàn thành
                            itemGiaoDichThanhCong.Enabled = true;
                            break;
                    }
                }
            }
        }

        int GetAccessData()
        {
            it.AccessDataCls o = new it.AccessDataCls(Common.PerID, 180);

            return o.SDB.SDBID;
        }

        void GiaoDich_Load()
        {

            var tuNgay = (DateTime?)itemTuNgay.EditValue ?? DateTime.Now;
            var denNgay = (DateTime?)itemDenNgay.EditValue ?? DateTime.Now;
            int MaNV = Common.StaffID;
            if (itemTuNgay.EditValue == null || itemDenNgay.EditValue == null)
            {
                itemDaKyHD.DataSource = null;
            }

            db = new MasterDataContext();
            switch (GetAccessData())
            {
                case 1://Tat ca
                    itemDaKyHD.DataSource = db.mglgdGiaoDich_GetInfo(tuNgay, denNgay, -1, -1, -1, MaNV, true);
                    break;
                case 2://Theo phong ban 
                    itemDaKyHD.DataSource = db.mglgdGiaoDich_GetInfo(tuNgay, denNgay, -1, -1, Common.DepartmentID, MaNV, false);
                    break;
                case 3://Theo nhom
                    itemDaKyHD.DataSource = db.mglgdGiaoDich_GetInfo(tuNgay, denNgay, -1, Common.GroupID, -1, MaNV, false);
                    break;
                case 4://Theo nhan vienbreak;
                    itemDaKyHD.DataSource = db.mglgdGiaoDich_GetInfo(tuNgay, denNgay, MaNV, -1, -1, MaNV, false);
                    break;
                default:
                    itemDaKyHD.DataSource = null;
                    break;
            }
            //gcGiaoDich.DataSource = db.mglgdGiaoDiches.Where(p =>
            //    SqlMethods.DateDiffDay((DateTime)itemTuNgay.EditValue, p.NgayGD) >= 0 &
            //    SqlMethods.DateDiffDay(p.NgayGD, (DateTime)itemDenNgay.EditValue) >= 0)
            //    .OrderBy(p => p.mglgdTrangThai.STT)
            //    .OrderByDescending(p => p.NgayGD)
            //    .AsEnumerable()
            //    .Select((p, index) => new
            //    {
            //        p.MaGD,
            //        STT = index + 1,
            //        p.SoGD,
            //        p.NgayGD,
            //        p.ThoiHan,
            //        p.TienCoc,
            //        p.MaTT,
            //        p.mglgdTrangThai.MauNen,
            //        p.mglbcBanChoThue.KyHieu,
            //        TenHT = p.mglbcBanChoThue.IsBan.Value ? "Bán" : "Cho thuê",
            //        p.mglbcBanChoThue.LoaiBD.TenLBDS,
            //        p.mglbcBanChoThue.DienTich,
            //        p.mglbcBanChoThue.DonGia,
            //        p.mglbcBanChoThue.ThanhTien,
            //        PhiMG = string.Format("{0:0.##}/{1:0.##}", p.TienMGBenBan, p.TienMGBenMua),
            //        HoTenKH1 = p.mglbcBanChoThue.MaNVKD == BEEREMA.Library.Common.StaffID ?
            //            p.mglbcBanChoThue.KhachHang.HoKH + " " + p.mglbcBanChoThue.KhachHang.TenKH :
            //            "(nv) " + p.mglbcBanChoThue.NhanVien.HoTen,
            //        HoTenKH2 = p.mglmtMuaThue.MaNVKD == BEEREMA.Library.Common.StaffID ?
            //                                p.mglmtMuaThue.KhachHang.HoKH + " " + p.mglmtMuaThue.KhachHang.TenKH :
            //                                "(nv) " + p.mglmtMuaThue.NhanVien.HoTen,
            //        HoTenNV1 = p.NhanVien.HoTen,
            //        HoTenNV2 = p.NhanVien1.HoTen,
            //        HoTenNV = p.NhanVien2.HoTen
            //    }).ToList();
        }

        private void GiaoDich_Edit()
        {
            var maTT = (int)grvGiaoDich.GetFocusedRowCellValue("MaGD");
            frmEdit frm = new frmEdit();
            frm.MaGD = (int)grvGiaoDich.GetFocusedRowCellValue("MaGD");
            if (maTT == 7)
                frm.IsEdit = false;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                GiaoDich_Load();
            }
        }

        private void GiaoDich_Delete()
        {
            var indexs = grvGiaoDich.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn phiếu giao dịch");
                return;
            }
            if (DialogBox.Question() == DialogResult.No) return;
            foreach (var i in indexs)
            {
                mglgdGiaoDich objGD = db.mglgdGiaoDiches.Single(p => p.MaGD == (int)grvGiaoDich.GetRowCellValue(i, "MaGD"));
                db.mglgdGiaoDiches.DeleteOnSubmit(objGD);
            }
            db.SubmitChanges();

            grvGiaoDich.DeleteSelectedRows();
        }

        private void TabPage_Load()
        {
            if (grvGiaoDich.FocusedRowHandle < 0)
            {
                gcNhatKy.DataSource = null;
                return;
            }

            int maGD = (int)grvGiaoDich.GetFocusedRowCellValue("MaGD");

            switch (tabMain.SelectedTabPageIndex)
            {
                case 0:
                    gcNhatKy.DataSource = db.mglgdNhatKyXuLies.Where(p => p.MaGD == maGD)
                        .OrderByDescending(p => p.NgayXL)
                        .AsEnumerable()
                        .Select((p, index) => new
                        {
                            p.ID,
                            STT = index + 1,
                            p.NgayXL,
                            p.MaTT,
                            p.DienGiai,
                            HoTenNV = p.NhanVien.HoTen
                        }).ToList();
                    break;
                case 1:
                    ctlTaiLieu1.FormID = 180;
                    ctlTaiLieu1.LinkID = (int?)grvGiaoDich.GetFocusedRowCellValue("MaGD");
                    ctlTaiLieu1.MaNV = Common.StaffID;
                    ctlTaiLieu1.TaiLieu_Load();
                    break;
            }
        }

        private void ctlManager_Load(object sender, EventArgs e)
        {
            SetDate(0);
            LoadPermission();
            GetAccessData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GiaoDich_Load();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvGiaoDich.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn phiếu đăng ký");
                return;
            }
            GiaoDich_Edit();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GiaoDich_Delete();
        }

        private void grvGiaoDich_DoubleClick(object sender, EventArgs e)
        {
            if (grvGiaoDich.FocusedRowHandle < 0)
            {
                return;
            }

            GiaoDich_Edit();
        }

        private void grvGiaoDich_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                GiaoDich_Delete();
            }
        }

        void SetDate(int index)
        {
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            GiaoDich_Load();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            GiaoDich_Load();
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void grvGiaoDich_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            TabPage_Load();
        }

        private void tabMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            TabPage_Load();
        }

        private void grvGiaoDich_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator & e.RowHandle >= 0)
            {
                var cl = System.Drawing.Color.FromArgb((int)grvGiaoDich.GetRowCellValue(e.RowHandle, "MauNen"));
                e.Graphics.FillRectangle(new SolidBrush(cl), e.Info.Bounds);
                e.Handled = true;
            }
        }

        private void grvGiaoDich_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            if (e.Column.FieldName == "TenHT")
            {
                e.Appearance.ForeColor = grvGiaoDich.GetRowCellValue(e.RowHandle, "TenHT").ToString() == "Bán" ?
                        System.Drawing.Color.Red : System.Drawing.Color.Black;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
        }

        void Duyet(byte maTT)
        {
            if (grvGiaoDich.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn giao dịch");
                return;
            }

            frmDuyet frm = new frmDuyet();
            frm.MaTT = maTT;
            frm.MaGD = (int)grvGiaoDich.GetFocusedRowCellValue("MaGD");
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
                GiaoDich_Load();
        }

        private void itemDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Duyet(3);
        }

        private void itemKhongDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Duyet(4);
            var objGD = db.mglgdGiaoDiches.FirstOrDefault(p => p.MaGD == (int)grvGiaoDich.GetFocusedRowCellValue("MaGD"));
            try
            {
                var objmt = db.mglmtMuaThues.FirstOrDefault(p => p.MaMT == objGD.MaMT);
                objmt.MaTT = 0;
                var obj = db.mglbcBanChoThues.First(p => p.MaBC == objGD.MaBC);
                obj.MaTT = 0;
                db.SubmitChanges();
            }
            catch
            { }
        }

        private void itemChuaThuTien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Duyet(5);
        }

        private void itemGiaoDichThanhCong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Duyet(7);
            var objGD = db.mglgdGiaoDiches.FirstOrDefault(p => p.MaGD == (int)grvGiaoDich.GetFocusedRowCellValue("MaGD"));
            try
            {
                var objmt = db.mglmtMuaThues.FirstOrDefault(p => p.MaMT == objGD.MaMT);
                objmt.MaTT = 4;
                var obj = db.mglbcBanChoThues.First(p => p.MaBC == objGD.MaBC);
                obj.MaTT = 4;
                db.SubmitChanges();
            }
            catch
            { }
        }

        private void itemTHuChuaDu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Duyet(6);
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Duyet(2);
        }
    }
}
