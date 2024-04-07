using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Linq;
using BEE.ThuVien;
using DevExpress.XtraEditors;
using BEEREMA;

namespace BEE.KhachHang
{
    public partial class KhachHangChoDuyet : XtraUserControl
    {
        MasterDataContext db = new MasterDataContext();

        int MaKH = 0, MaHuyen = 0, HowToKnowID = 0, LevelID = 0, Purpose = 0;
        byte MaTinh = 0;

        public KhachHangChoDuyet()
        {
            InitializeComponent();
        }

        int GetAccessData()
        {
            it.AccessDataCls o = new it.AccessDataCls(Common.PerID, 9);

            return o.SDB.SDBID;
        }

        void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                it.KhachHangCls o = new it.KhachHangCls();
                switch (GetAccessData())
                {
                    case 1://Tat ca
                        gcDoanhNghiep.DataSource = db.KhachHang_getByDoanhNghiepDuyet();
                        gcCaNhan.DataSource = db.KhachHang_getCaNhanDuyet();
                        break;
                    case 2://Theo phong ban
                        gcDoanhNghiep.DataSource = db.KhachHang_getByDoanhNghiepByDeparmentDuyet(Common.StaffID, Common.DepartmentID);
                        gcCaNhan.DataSource = db.KhachHang_getCaNhanByDeparmentDuyet(Common.StaffID, Common.DepartmentID);
                        break;
                    case 3://Theo nhom
                        gcDoanhNghiep.DataSource = db.KhachHang_getByDoanhNghiepByGroupDuyet(Common.StaffID, Common.GroupID);
                        gcCaNhan.DataSource = db.KhachHang_getCaNhanByGroupDuyet(Common.StaffID, Common.GroupID);
                        break;
                    case 4://Theo nhan vien
                        gcDoanhNghiep.DataSource = db.KhachHang_getByDoanhNghiepByStaffDuyet(Common.StaffID);
                        gcCaNhan.DataSource = db.KhachHang_getCaNhanByStaffDuyet(Common.StaffID);
                        break;
                    default:
                        gcCaNhan.DataSource = null;
                        gcDoanhNghiep.DataSource = null;
                        break;
                }

                if (gvCaNhan.FocusedRowHandle == 0)
                    gvCaNhan.FocusedRowHandle = -1;
            }
            catch { }
            finally { wait.Close(); wait.Dispose(); }
        }

        void LoadPermissionScheduler()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = Common.PerID;
            o.AccessData.Form.FormID = 67;
            DataTable tblAction = o.SelectBy();
            itemLichHen_Add.Enabled = false;
            itemLichHen_Delete.Enabled = false;
            itemLichHen_Edit.Enabled = false;

            if (tblAction.Rows.Count > 0)
            {
                foreach (DataRow r in tblAction.Rows)
                {
                    switch (byte.Parse(r["FeatureID"].ToString()))
                    {
                        case 1:
                            itemLichHen_Add.Enabled = true;
                            break;
                        case 2:
                            itemLichHen_Edit.Enabled = true;
                            break;
                        case 3:
                            itemLichHen_Delete.Enabled = true;
                            break;
                    }
                }
            }
        }

        void LoadPermission()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = Common.PerID;
            o.AccessData.Form.FormID = 9;
            DataTable tblAction = o.SelectBy();
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnIn.Enabled = false;
            btnImport.Enabled = false;
            btnThemNDD.Enabled = false;
            btnSuaNDD.Enabled = false;
            btnXoaNDD.Enabled = false;
            btnThemNDD.Enabled = false;
            btnSuaNDD.Enabled = false;
            btnXoaNDD.Enabled = false;
            subDuyet.Enabled = false;

            if (tblAction.Rows.Count > 0)
            {
                foreach (DataRow r in tblAction.Rows)
                {
                    switch (byte.Parse(r["FeatureID"].ToString()))
                    {
                        case 1:
                            btnThem.Enabled = true;
                            break;
                        case 2:
                            btnSua.Enabled = true;
                            break;
                        case 4:
                            btnIn.Enabled = true;
                            break;
                        case 7:
                            subDuyet.Enabled = true;
                            break;
                        case 16:
                            btnImport.Enabled = true;
                            break;
                        case 31://Nguoi dai dien
                            btnThemNDD.Enabled = true;
                            btnSuaNDD.Enabled = true;
                            btnXoaNDD.Enabled = true;
                            break;
                    }
                }
            }
        }

        private void gridCaNhan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvCaNhan.GetFocusedRowCellValue(colMaKH) != null)
            {
                MaKH = int.Parse(gvCaNhan.GetFocusedRowCellValue(colMaKH).ToString());
            }
            else
                MaKH = 0;
        }

        void Edit()
        {
            if (tabMain.SelectedTabPageIndex == 0)
            {
                if (gvCaNhan.GetFocusedRowCellValue(colMaKH) != null)
                {
                    KhachHang_frm frm = new KhachHang_frm();
                    frm.MaKH = int.Parse(gvCaNhan.GetFocusedRowCellValue(colMaKH).ToString());
                    //frm.HowToKnowID = int.Parse(gvCaNhan.GetFocusedRowCellValue(colNguonDen).ToString());
                    //frm.Purpose = int.Parse(gvCaNhan.GetFocusedRowCellValue(colMucDich).ToString());
                    //frm.LevelID = int.Parse(gvCaNhan.GetFocusedRowCellValue(colLevel).ToString());
                    frm.IsPersonal = true;
                    frm.ShowDialog();
                    if (frm.IsUpdate)
                        LoadData();
                }
                else
                    DialogBox.Infomation("Vui lòng chọn [Khách hàng] muốn sửa. Xin cảm ơn");
            }
            else
            {
                if (gvDoanhNghiep.GetFocusedRowCellValue("MaKH") != null)
                {
                    KhachHang_frm frm = new KhachHang_frm();
                    frm.MaKH = int.Parse(gvDoanhNghiep.GetFocusedRowCellValue("MaKH").ToString());
                    frm.IsPersonal = false;
                    frm.ShowDialog();
                    if (frm.IsUpdate)
                        LoadData();
                }
                else
                    DialogBox.Infomation("Vui lòng chọn [Khách hàng] muốn sửa. Xin cảm ơn");
            }
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            KhachHang_frm frm = new KhachHang_frm();
            frm.ShowDialog();
            if (frm.IsUpdate)
                LoadData();
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Edit();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void btnImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmImportLinq frm = new frmImportLinq();
            frm.ShowDialog();
            LoadData();
        }

        private void gridCaNhan_DoubleClick(object sender, EventArgs e)
        {
            if (btnSua.Enabled)
                Edit();
        }

        private void gridDoanhNghiep_DoubleClick(object sender, EventArgs e)
        {
            if (btnSua.Enabled)
                Edit();
        }

        private void gridDoanhNghiep_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvDoanhNghiep.GetFocusedRowCellValue("MaKH") != null)
            {
                MaKH = int.Parse(gvDoanhNghiep.GetFocusedRowCellValue(colMaKH2).ToString());
            }
            else
                MaKH = 0;

        }

        void LoadGhiChu()
        {
            //if (grvKhachHang.FocusedRowHandle >= 0)
            //{
            //    it.KhachHang_GhiChuCls o = new it.KhachHang_GhiChuCls();
            //    gridControlGhiChu.DataSource = o.Select((int)grvKhachHang.GetFocusedRowCellValue("MaKH"));
            //}
            //else
            //{
            //    gridControlGhiChu.DataSource = null;
            //}
        }

        private void xtraTabControl2_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
        }

        private void btnGhiChu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int temp = 0;
            if (tabMain.SelectedTabPageIndex == 0)
                temp = int.Parse(gvCaNhan.GetFocusedRowCellValue(colMaKH).ToString());
            else
                temp = int.Parse(gvDoanhNghiep.GetFocusedRowCellValue(colMaKH2).ToString());
            GhiChu_frm frm = new GhiChu_frm(temp);
            frm.ShowDialog();
            if (frm.IsUpdate)
            {
                if (tabMain.SelectedTabPageIndex == 0)
                    MaKH = int.Parse(gvCaNhan.GetFocusedRowCellValue(colMaKH).ToString());
                else
                    MaKH = int.Parse(gvDoanhNghiep.GetFocusedRowCellValue(colMaKH2).ToString());

                LoadGhiChu();
            }
        }

        private void btnNguoiGioiThieu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (tabMain.SelectedTabPageIndex == 0)
            {
                if (gvCaNhan.GetFocusedRowCellValue(colMaKH) != null)
                    MaKH = int.Parse(gvCaNhan.GetFocusedRowCellValue(colMaKH).ToString());
            }
            else
                if (gvDoanhNghiep.GetFocusedRowCellValue(colMaKH2) != null)
                    MaKH = int.Parse(gvDoanhNghiep.GetFocusedRowCellValue(colMaKH2).ToString());
            if (MaKH != 0)
            {
                SelectReferrer_frm frm = new SelectReferrer_frm();
                frm.MaKH = MaKH;
                frm.ShowDialog();
            }
            else
                DialogBox.Infomation("Vui lòng chọn [Khách hàng] muốn cập nhật thông tin người giới thiệu.");
        }

        List<bdsSanPham> GetPersonal()
        {
            List<bdsSanPham> temp = new List<bdsSanPham>();
            int[] rows = gvCaNhan.GetSelectedRows();
            foreach (int r in rows)
            {
                var o = new bdsSanPham();
                o.MaKH = Convert.ToInt32(gvCaNhan.GetRowCellValue(r, "MaKH"));
                o.ThanhTien = 0;
                temp.Add(o);
            }
            return temp;
        }

        List<bdsSanPham> GetCompany()
        {
            List<bdsSanPham> temp = new List<bdsSanPham>();
            int[] rows = gvCaNhan.GetSelectedRows();
            foreach (int r in rows)
            {
                var o = new bdsSanPham();
                o.MaKH = Convert.ToInt32(gvDoanhNghiep.GetRowCellValue(r, "MaKH"));
                o.ThanhTien = 0;
                temp.Add(o);
            }
            return temp;
        }

        private void itemChuyenQuyen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<int> Customers = new List<int>();
            int[] Rows;
            if (tabMain.SelectedTabPageIndex == 0)
                Rows = gvCaNhan.GetSelectedRows();
            else
                Rows = gvDoanhNghiep.GetSelectedRows();

            if (Rows.Length <= 0)
            {
                DialogBox.Infomation("Vui lòng chọn [Khách hàng] muốn chuyển quyền. Xin cảm ơn.");
                return;
            }
            Customers.Clear();
            foreach (int item in Rows)
            {
                if (tabMain.SelectedTabPageIndex == 0)
                    Customers.Add(Convert.ToInt32(gvCaNhan.GetRowCellValue(item, "MaKH")));
                else
                    Customers.Add(Convert.ToInt32(gvDoanhNghiep.GetRowCellValue(item, "MaKH")));
            }

            BEE.NghiepVuKhac.ChoiceStaff_frm frm = new BEE.NghiepVuKhac.ChoiceStaff_frm();
            frm.Customers = Customers;
            frm.CateID = 1;
            frm.ShowDialog();
            if (frm.IsUpdate)
                LoadData();
            try
            {
                System.GC.Collect();
            }
            catch { }
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (tabMain.SelectedTabPageIndex == 0)
            {
                it.CommonCls.ExportExcel(gcCaNhan);
            }
            else
                it.CommonCls.ExportExcel(gcDoanhNghiep);
        }

        private void itemProcess_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int? maKH = tabMain.SelectedTabPageIndex == 0 ? (int?)gvCaNhan.GetFocusedRowCellValue("MaKH") : (int?)gvDoanhNghiep.GetFocusedRowCellValue("MaKH");
            if (maKH == null)
            {
                DialogBox.Infomation("Vui lòng chọn [Khách hàng], xin cảm ơn.");
                return;
            }

            byte? maNKH = tabMain.SelectedTabPageIndex == 0 ? (byte?)gvCaNhan.GetFocusedRowCellValue("MaNKH") : (byte?)gvDoanhNghiep.GetFocusedRowCellValue("MaNKH");

            var f = new frmProcess();
            f.MaNKH = maNKH;
            f.CustomerID = maKH;
            f.ShowDialog();
        }

        private void itemNameNoSign_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (tabMain.SelectedTabPageIndex == 0)
            {
                var rows = gvCaNhan.GetSelectedRows();
                foreach (var i in rows)
                {
                    it.SqlCommon.ExecuteNonQueryText(string.Format("KhachHang_updateNameNoSign {0}, N'{1}'", Convert.ToInt32(gvCaNhan.GetRowCellValue(i, "MaKH")), it.CommonCls.TiegVietKhongDau(gvCaNhan.GetRowCellValue(i, "HoKH").ToString() + " " + gvCaNhan.GetRowCellValue(i, "TenKH").ToString())));
                }
            }
            else
            {
                var rows = gvDoanhNghiep.GetSelectedRows();
                foreach (var i in rows)
                {
                    it.SqlCommon.ExecuteNonQueryText(string.Format("KhachHang_updateNameNoSign {0}, N'{1}'", Convert.ToInt32(gvDoanhNghiep.GetRowCellValue(i, "MaKH")), it.CommonCls.TiegVietKhongDau(gvDoanhNghiep.GetRowCellValue(i, "TenCongTy").ToString())));
                }
            }

            LoadData();
        }

        private void itemChangeLevel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var f = new BEE.KhachHang.frmLevel();
            f.MaKH = (int?)gvCaNhan.GetFocusedRowCellValue("MaKH");
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
                LoadData();
        }

        private void xtraTabControl1_Click(object sender, EventArgs e)
        {

        }

        private void itemKhongDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (tabMain.SelectedTabPageIndex == 0)
                {
                    var indexs = gvCaNhan.GetSelectedRows();
                    if (indexs.Length <= 0)
                    {
                        DialogBox.Error("Vui lòng chọn [Khách hàng], xin cảm ơn.");
                        return;
                    }

                    if (DialogBox.Question("Bạn có chắc không?") == DialogResult.No) return;
                    foreach (var i in indexs)
                    {
                        var objKH = db.KhachHangs.Single(p => p.MaKH == (int?)gvCaNhan.GetRowCellValue(i, "MaKH"));
                        objKH.MaTT = 2;
                    }
                    db.SubmitChanges();
                }
                else
                {
                    var indexs = gvDoanhNghiep.GetSelectedRows();
                    if (indexs.Length <= 0)
                    {
                        DialogBox.Error("Vui lòng chọn doanh nghiệp");
                        return;
                    }
                    if (DialogBox.Question() == DialogResult.No) return;
                    foreach (var i in indexs)
                    {
                        var objKH = db.KhachHangs.Single(p => p.MaKH == (int?)gvCaNhan.GetRowCellValue(i, "MaKH"));
                        objKH.MaTT = 2;
                    }
                    db.SubmitChanges();
                }

            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
                db = new BEE.ThuVien.MasterDataContext();
            }
        }

        private void itemDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {                
                if (tabMain.SelectedTabPageIndex == 0)
                {
                    var indexs = gvCaNhan.GetSelectedRows();
                    if (indexs.Length <= 0)
                    {
                        DialogBox.Error("Vui lòng chọn [Khách hàng], xin cảm ơn.");
                        return;
                    }
                    if (DialogBox.Question() == DialogResult.No) return;
                    foreach (var i in indexs)
                    {
                        var objKH = db.KhachHangs.Single(p => p.MaKH == (int?)gvCaNhan.GetRowCellValue(i, "MaKH"));
                        db.KhachHangs.DeleteOnSubmit(objKH);
                    }
                    db.SubmitChanges();
                    gvCaNhan.DeleteSelectedRows();
                }
                else
                {
                    var indexs = gvDoanhNghiep.GetSelectedRows();
                    if (indexs.Length <= 0)
                    {
                        DialogBox.Error("Vui lòng chọn doanh nghiệp");
                        return;
                    }
                    if (DialogBox.Question() == DialogResult.No) return;
                    foreach (var i in indexs)
                    {
                        var objKH = db.KhachHangs.Single(p => p.MaKH == (int?)gvDoanhNghiep.GetRowCellValue(i, "MaKH"));
                        db.KhachHangs.DeleteOnSubmit(objKH);
                    }
                    db.SubmitChanges();
                    gvDoanhNghiep.DeleteSelectedRows();
                }               

            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
                db = new BEE.ThuVien.MasterDataContext();
            }
        }

        private void KhachHangChoDuyet_Load(object sender, EventArgs e)
        {
            BEE.NgonNgu.Language.TranslateUserControl(this, barManager1);

            LoadPermission();
            LoadPermissionScheduler();
            LoadData();
            tabMain.SelectedTabPageIndex = 1;
            tabMain.SelectedTabPageIndex = 0;
            lookUpNguonDen.DataSource = db.mglNguons.Select(p => new { ID = p.MaNguon, Name = p.TenNguon }); //db.cdHowToKnows.Select(p=>new {p.})
            lookUpMucDich.DataSource = db.cdPurposes;
            lookUpLevel.DataSource = db.cdLevels;

            it.NhomKHCls objNKH = new it.NhomKHCls();
            lookUpNhomKH1.DataSource = objNKH.Select();
            lookUpNhomKH2.DataSource = objNKH.Select();

            it.NhanVienCls objNV = new it.NhanVienCls();
            lookUpNhanVien1.DataSource = objNV.SelectShow();
            lookUpNhanVien2.DataSource = objNV.SelectShow();

            lookUpDanhXung.DataSource = db.QuyDanhs;
            lookupQuyDanh.DataSource = db.QuyDanhs;
            lookUpLoaiBDS.DataSource = db.LoaiBDs;
            lookUpDuAn.DataSource = db.DuAn_getList();
            lookUpNgheNghiep.DataSource = db.NgheNghieps;
        }
    }
}
