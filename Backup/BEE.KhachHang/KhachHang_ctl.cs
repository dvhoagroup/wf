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
    public partial class KhachHang_ctl : XtraUserControl
    {
        MasterDataContext db = new MasterDataContext();

        int MaKH = 0, MaHuyen = 0, HowToKnowID = 0, LevelID = 0, Purpose = 0;
        byte MaTinh = 0;

        public KhachHang_ctl()
        {
            InitializeComponent();
        }

        int GetAccessData()
        {
            it.AccessDataCls o = new it.AccessDataCls(Common.PerID, 9);

            return o.SDB.SDBID;
        }

        private void KhachHang_ctl_Load(object sender, EventArgs e)
        {
            BEE.NgonNgu.Language.TranslateUserControl(this, barManager1);

            //CustomizeColumn.CustomizeColumnGridViewCls.LoadSkin(grvKhachHang, 7, -1, -1);
            //CustomizeColumn.CustomizeColumnGridViewCls.LoadSkin(gridDoanhNghiep, 8, -1, -1);

            if (Common.PerID != 1)
                itemNameNoSign.Enabled = false;
            LoadData();
            tabMain.SelectedTabPageIndex = 1;
            tabMain.SelectedTabPageIndex = 0;
            //quyDanh_getAllTableAdapter.Fill(khachHang_src.QuyDanh_getAll);            



            //lookUpNguonDen.DataSource = from o in db.cdHowToKnows select new { ID = o.ID, Name = o.Name };
            //lookUpMucDich.DataSource = from p in db.cdPurposes select new { ID = p.ID, Name = p.Name };
            //lookUpLevel.DataSource = from q in db.cdLevels select new { ID = q.ID, Name = q.Name };
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
            lookUpEditNhomKH.DataSource = db.NhomKHs;

            LoadPermission();
            LoadPermissionScheduler();
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
                        gcDoanhNghiep.DataSource = db.KhachHang_getByDoanhNghiep();
                        gcCaNhan.DataSource = db.KhachHang_getCaNhan();
                        break;
                    case 2://Theo phong ban
                        gcDoanhNghiep.DataSource = o.SelectComByDeparment(Common.StaffID, Common.DepartmentID);
                        gcCaNhan.DataSource = o.SelectPerByDeparment(Common.StaffID, Common.DepartmentID);
                        break;
                    case 3://Theo nhom
                        gcDoanhNghiep.DataSource = o.SelectComByGroup(Common.StaffID, Common.GroupID);
                        gcCaNhan.DataSource = o.SelectPerByGroup(Common.StaffID, Common.GroupID);
                        break;
                    case 4://Theo nhan vien
                        gcDoanhNghiep.DataSource = o.SelectComByStaff(Common.StaffID);
                        gcCaNhan.DataSource = o.SelectPerByStaff(Common.StaffID);
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
            btnXoa.Enabled = false;
            btnIn.Enabled = false;
            btnImport.Enabled = false;
            btnThemNDD.Enabled = false;
            btnSuaNDD.Enabled = false;
            btnXoaNDD.Enabled = false;
            btnNguoiDD.Enabled = false;
            btnGuiMail.Enabled = false;
            btnGuiSMS.Enabled = false;
            btnNguoiGioiThieu.Enabled = false;
            btnNguoiDD.Enabled = false;
            btnThemNDD.Enabled = false;
            btnSuaNDD.Enabled = false;
            btnXoaNDD.Enabled = false;

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
                        case 3:
                            btnXoa.Enabled = true;
                            break;
                        case 4:
                            btnIn.Enabled = true;
                            break;
                        case 16:
                            btnImport.Enabled = true;
                            break;
                        case 18:
                            btnGuiMail.Enabled = true;
                            break;
                        case 19:
                            btnGuiSMS.Enabled = true;
                            break;
                        case 31://Nguoi dai dien
                            btnNguoiDD.Enabled = true;
                            btnThemNDD.Enabled = true;
                            btnSuaNDD.Enabled = true;
                            btnXoaNDD.Enabled = true;
                            break;
                        case 32://Nguoi gioi thieu
                            btnNguoiGioiThieu.Enabled = true;
                            break;
                    }
                }
            }
        }

        void LoadNguoiGioiThieu()
        {
            if (gvCaNhan.FocusedRowHandle >= 0)
            {
                it.KhachHangCls o = new it.KhachHangCls();
                o.MaKH = (int)gvCaNhan.GetFocusedRowCellValue("MaKH");
                gridControlReferrer.DataSource = o.SelectReferrer();
            }
            else
            {
                gridControlReferrer.DataSource = null;
            }
        }

        void LoadNotes(int maKH)
        {
            it.khNotesTransferCls o = new it.khNotesTransferCls();
            o.MaKH = maKH;
            gridControlNotes.DataSource = o.Select();
        }

        void LoadDataByPage()
        {
            int? maKH = tabMain.SelectedTabPageIndex == 0 ? (int?)gvCaNhan.GetFocusedRowCellValue("MaKH") : (int?)gvDoanhNghiep.GetFocusedRowCellValue("MaKH");
            if (maKH == null)
            {
                switch (xtraTabControl1.SelectedTabPageIndex)
                {
                    case 0:
                        gcLich.DataSource = null;
                        break;
                    case 1:
                        gcLSGD.DataSource = null;
                        break;
                }
            }

            switch (xtraTabControl1.SelectedTabPageIndex)
            {
                case 0:
                    LoadHistory(maKH ?? 0);
                    break;
                case 1:
                    gcLich.DataSource = db.LichHens.Where(p => p.MaKH == maKH)
                        .OrderByDescending(p => p.NgayBD)
                        .AsEnumerable()
                        .Select((p, index) => new
                        {
                            STT = index + 1,
                            p.MaLH,
                            p.TieuDe,
                            HoTenNV = p.NhanVien.HoTen,
                            p.NgayBD,
                            p.NgayKT,
                            p.DiaDiem,
                            p.DienGiai,
                            p.MaNV
                        }).ToList();
                    break;
                case 2:
                    gcLSGD.DataSource = db.mglKhachHang_GiaoDich(maKH);
                    break;

                case 3:
                    gcNhuCau.DataSource = db.mglKhachHang_NhuCau(maKH);
                    break;
                case 4:
                    gcNhuCauBan.DataSource = db.mglKhachHang_CanBan_ChoThue(maKH);
                    break;
                case 5:
                    LoadAvarar();
                    break;
                case 6:
                    LoadNguoiGioiThieu();
                    break;
                case 7:
                    LoadStaff(maKH ?? 0);
                    break;
                case 8:
                    LoadNotes(maKH ?? 0);
                    break;
            }
        }

        void LoadHistory(int maKH)
        {
            it.KhachHangCls o = new it.KhachHangCls();
            o.MaKH = maKH;
            gcQTTH.DataSource = o.SelectHistory();
            gvQTTH.FocusedRowHandle = 0;
            //LoadNhatKyPhanHoi();
        }

        void LoadStaff(int maKH)
        {
            it.KhachHangCls o = new it.KhachHangCls();
            o.MaKH = maKH;
            gridControlStaff.DataSource = o.SelectStaffManager();

            it.NhanVienCls objStaff = new it.NhanVienCls();
            lookUpChucVu.DataSource = objStaff.ChucVu.Select();
            lookUpNhomKD.DataSource = objStaff.NKD.Select();
            lookUpPhongBan.DataSource = objStaff.PhongBan.Select();
        }

        void LoadNhuCau(int MaKH)
        {

        }

        private void gridCaNhan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvCaNhan.GetFocusedRowCellValue(colMaKH) != null)
            {
                MaKH = int.Parse(gvCaNhan.GetFocusedRowCellValue(colMaKH).ToString());
            }
            else
                MaKH = 0;

            LoadDataByPage();
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

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                        objKH.MaTT = 3;
                    }
                    db.SubmitChanges();
                    DialogBox.Infomation("Đã chuyển khách hàng sang khách hàng chờ duyệt xóa");
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
                        objKH.MaTT = 3;
                    }
                    db.SubmitChanges();
                    DialogBox.Infomation("Đã chuyển khách hàng sang khách hàng chờ duyệt xóa");
                }

            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
                db = new BEE.ThuVien.MasterDataContext();
            }
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

        void LoadAvarar()
        {
            if (gvCaNhan.FocusedRowHandle >= 0)
            {
                it.NguoiDaiDienCls o = new it.NguoiDaiDienCls();
                gridControlAvatar.DataSource = o.Select((int)gvCaNhan.GetFocusedRowCellValue("MaKH"));
            }
            else
            {
                gridControlAvatar.DataSource = null;
            }
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

            LoadDataByPage();
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
            LoadDataByPage();
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

        void LoadGiaoDich()
        {
            if (gvCaNhan.FocusedRowHandle >= 0)
            {
                it.KhachHangCls o = new it.KhachHangCls();
                gcLSGD.DataSource = o.GiaoDich((int)gvCaNhan.GetFocusedRowCellValue("MaKH"));
            }
            else
            {
                gcLSGD.DataSource = null;
            }
        }

        private void btnNguoiDD_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (tabMain.SelectedTabPageIndex == 0)
            {
                if (gvCaNhan.GetFocusedRowCellValue(colMaKH) != null)
                    MaKH = int.Parse(gvCaNhan.GetFocusedRowCellValue(colMaKH).ToString());
            }
            else
                if (gvDoanhNghiep.GetFocusedRowCellValue(colMaKH2) != null)
                    MaKH = int.Parse(gvDoanhNghiep.GetFocusedRowCellValue(colMaKH2).ToString());
            NguoiDaiDien_frm frm = new NguoiDaiDien_frm();
            frm.MaKH = MaKH;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
                LoadAvarar();
        }

        private void btnThemNDD_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (tabMain.SelectedTabPageIndex == 0)
            {
                if (gvCaNhan.GetFocusedRowCellValue(colMaKH) != null)
                    MaKH = int.Parse(gvCaNhan.GetFocusedRowCellValue(colMaKH).ToString());
            }
            else
                if (gvDoanhNghiep.GetFocusedRowCellValue(colMaKH2) != null)
                    MaKH = int.Parse(gvDoanhNghiep.GetFocusedRowCellValue(colMaKH2).ToString());
            NguoiDaiDien_frm frm = new NguoiDaiDien_frm();
            frm.MaKH = MaKH;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
                LoadAvarar();
        }

        private void btnXoaNDD_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridAvatar.GetFocusedRowCellValue(colMaKHDD) != null)
            {
                try
                {
                    if (DialogBox.Question("Bạn có chắc chắn muốn xóa [Người đại diện] này không?") == DialogResult.Yes)
                    {
                        it.NguoiDaiDienCls o = new it.NguoiDaiDienCls();
                        o.MaKH = int.Parse(gridAvatar.GetFocusedRowCellValue(colMaKHDD).ToString());
                        o.MaNDD = byte.Parse(gridAvatar.GetFocusedRowCellValue(colSTT2).ToString());
                        o.Delete();
                        gridAvatar.DeleteSelectedRows();
                    }
                }
                catch
                {
                    DialogBox.Infomation("[Người đại diện] này đã được sử dụng. Vui lòng kiểm tra lại.");
                }
            }
        }

        private void btnSuaNDD_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridAvatar.GetFocusedRowCellValue(colMaKHDD) != null)
            {
                try
                {
                    NguoiDaiDien_frm frm = new NguoiDaiDien_frm();
                    frm.MaKH = int.Parse(gridAvatar.GetFocusedRowCellValue(colMaKHDD).ToString());
                    frm.MaNDD = byte.Parse(gridAvatar.GetFocusedRowCellValue(colSTT2).ToString());
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                        LoadAvarar();
                }
                catch
                {
                    DialogBox.Infomation("[Người đại diện] này đã được sử dụng. Vui lòng kiểm tra lại.");
                }
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

        private void btnGuiMail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //List<bdsSanPham> ListReminder = new List<bdsSanPham>();
            //ListReminder.Clear();
            //if (xtraTabControl1.SelectedTabPageIndex == 0)
            //    ListReminder = GetPersonal();
            //else
            //    ListReminder = GetCompany();

            //if (ListReminder.Count <= 0)
            //{
            //    DialogBox.Infomation("Vui lòng chọn [Khách hàng], xin cảm ơn.");
            //    return;
            //}

            //var f = new BEE.QuangCao.Mail.frmGroupReminders();
            //f.IsCare = true;
            //f.ListReminder = ListReminder;
            //f.ShowDialog();
            //ListReminder = null;
            //System.GC.Collect();
        }

        private void btnGuiSMS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //List<bdsSanPham> ListReminder = new List<bdsSanPham>();
            //ListReminder.Clear();
            //if (xtraTabControl1.SelectedTabPageIndex == 0)
            //    ListReminder = GetPersonal();
            //else
            //    ListReminder = GetCompany();

            //if (ListReminder.Count <= 0)
            //{
            //    DialogBox.Infomation("Vui lòng chọn [Khách hàng], xin cảm ơn.");
            //    return;
            //}

            //var f = new BEE.QuangCao.SMS.frmGroupReminders();
            //f.IsCare = true;
            //f.ListReminder = ListReminder;
            //f.ShowDialog();
            //ListReminder = null;
            //System.GC.Collect();
        }

        private void btnImportNDD_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Import_Avatar_frm frm = new Import_Avatar_frm();
            frm.ShowDialog();
            LoadAvarar();
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

        #region Lich hen
        private void itemLichHen_Add_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var maKh = tabMain.SelectedTabPageIndex == 0 ? (int?)gvCaNhan.GetFocusedRowCellValue("MaKH") : (int?)gvDoanhNghiep.GetFocusedRowCellValue("MaKH");
                if (maKh == null)
                {
                    DialogBox.Error("Vui lòng chọn [Khách hàng], xin cảm ơn.");
                    return;
                }
                string hotenKH = tabMain.SelectedTabPageIndex == 0 ? string.Format("{0} {1}", gvCaNhan.GetFocusedRowCellValue("HoKH"),
                    gvCaNhan.GetFocusedRowCellValue("TenKH")) : gvDoanhNghiep.GetFocusedRowCellValue(colTenCongTy).ToString();
                var frm = new BEEREMA.CongViec.LichHen.AddNew_frm(null, maKh, hotenKH);
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    LoadDataByPage();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void itemLichHen_Edit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var maKh = tabMain.SelectedTabPageIndex == 0 ? (int?)gvCaNhan.GetFocusedRowCellValue("MaKH") : (int?)gvDoanhNghiep.GetFocusedRowCellValue("MaKH");
                if (maKh == null)
                {
                    DialogBox.Error("Vui lòng chọn [Khách hàng], xin cảm ơn.");
                    return;
                }
                var maLH = (int?)grvLich.GetFocusedRowCellValue("MaLH");
                if (maLH == null)
                {
                    DialogBox.Error("Vui lòng chọn [Lịch hẹn], xin cảm ơn.");
                    return;
                }
                if ((int)grvLich.GetFocusedRowCellValue("MaNV") == Common.StaffID)
                {
                    string hotenKH = tabMain.SelectedTabPageIndex == 0 ? string.Format("{0} {1}", gvCaNhan.GetFocusedRowCellValue("HoKH"),
                   gvCaNhan.GetFocusedRowCellValue("TenKH")) : gvDoanhNghiep.GetFocusedRowCellValue(colTenCongTy).ToString();
                    var frm = new BEEREMA.CongViec.LichHen.AddNew_frm(maLH, maKh, hotenKH);
                    frm.MaLH = maLH.Value;
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                        LoadDataByPage();
                }
                else
                    DialogBox.Infomation("[Lịch hẹn] này không do bạn quản lý. Vui lòng kiểm tra lại, xin cảm ơn.");
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void itemLichHen_Delete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var indexs = grvLich.GetSelectedRows();
                if (indexs.Length <= 0)
                {
                    DialogBox.Error("Vui lòng chọn [Lịch hẹn], xin cảm ơn.");
                    return;
                }

                if ((int)grvLich.GetFocusedRowCellValue("MaNV") == Common.StaffID)
                {
                    ;

                    if (DialogBox.Question() == DialogResult.No) return;

                    foreach (var i in indexs)
                    {
                        var objLH = db.LichHens.Single(p => p.MaLH == (int)grvLich.GetRowCellValue(i, "MaLH"));
                        db.LichHens.DeleteOnSubmit(objLH);
                    }
                    db.SubmitChanges();
                    LoadDataByPage();
                }
                else
                    DialogBox.Infomation("[Lịch hẹn] này không do bạn quản lý. Vui lòng kiểm tra lại, xin cảm ơn.");
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }
        #endregion

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            LoadDataByPage();
        }

        private void itemAddStaff_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<int> Customers = new List<int>();
            int[] Rows;
            if (tabMain.SelectedTabPageIndex == 0)
                Rows = gvCaNhan.GetSelectedRows();
            else
                Rows = gvDoanhNghiep.GetSelectedRows();

            if (Rows.Length <= 0)
            {
                DialogBox.Infomation("Vui lòng chọn [Khách hàng] muốn thêm nhân viên quản lý, xin cảm ơn.");
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
            frm.CateID = 3;
            frm.ShowDialog();
            if (frm.IsUpdate)
            {
                int? maKH = tabMain.SelectedTabPageIndex == 0 ? (int?)gvCaNhan.GetFocusedRowCellValue("MaKH") : (int?)gvDoanhNghiep.GetFocusedRowCellValue("MaKH");
                LoadStaff(maKH ?? 0);
            }
            try
            {
                System.GC.Collect();
            }
            catch { }
        }

        private void itemDeleteStaff_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridViewStaff.GetFocusedRowCellValue(colStaffID) != null)
            {
                if (DialogBox.Question("Bạn có chắc chắn muốn xóa nhân viên <" + gridViewStaff.GetFocusedRowCellValue(colTenNV).ToString() + "> khỏi danh sách nhân viên quản lý khách hàng này?") == DialogResult.Yes)
                {
                    int? maKH = tabMain.SelectedTabPageIndex == 0 ? (int?)gvCaNhan.GetFocusedRowCellValue("MaKH") : (int?)gvDoanhNghiep.GetFocusedRowCellValue("MaKH");
                    if (Convert.ToInt32(gridViewStaff.GetFocusedRowCellValue("MaDL")) == 0)
                    {
                        it.Staff_CustomerCls o = new it.Staff_CustomerCls();
                        o.StaffID = int.Parse(gridViewStaff.GetFocusedRowCellValue(colStaffID).ToString());
                        o.CustomerID = maKH ?? 0;
                        o.Delete();
                    }
                    else
                    {
                        it.aCustomerCls o = new it.aCustomerCls();
                        o.StaffID = int.Parse(gridViewStaff.GetFocusedRowCellValue(colStaffID).ToString());
                        o.CustomerID = maKH ?? 0;
                        o.Delete();
                    }
                    gridViewStaff.DeleteSelectedRows();
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn [Nhân viên] muốn xóa khỏi danh sách quản lý khách hàng này.\n\tVui lòng kiểm tra lại, xin cảm ơn.");
            try
            {
                System.GC.Collect();
            }
            catch { }
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
            if (f.DialogResult == DialogResult.OK)
                LoadDataByPage();
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

        void LoadNhatKyPhanHoi(int? nhatkyID)
        {
            try
            {
                gcNhatKyPhanHoi.DataSource = db.KhachHang_NhatKy_PhanHois
                                            .Where(p => p.NhatKyID == nhatkyID).Select(p => new
                                            {
                                                p.NgayTao,
                                                p.GhiChu,
                                                p.NhanVien.HoTen
                                            }).OrderByDescending(p => p.NgayTao);
            }
            catch
            {
                gcNhatKyPhanHoi.DataSource = null;
            }
        }

        private void gvQTTH_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int? nhatkyID = (int?)gvQTTH.GetFocusedRowCellValue(ID);
            LoadNhatKyPhanHoi(nhatkyID);
        }

        private void itemThemPH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int? nhatkyID = (int?)gvQTTH.GetFocusedRowCellValue(ID);
            var f = new BEE.KhachHang.frmPhanHoi();
            f.NhatKyID = (int?)gvQTTH.GetFocusedRowCellValue(ID);
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
                LoadNhatKyPhanHoi(nhatkyID);
        }

        private void xtraTabControl1_Click(object sender, EventArgs e)
        {

        }

        private void itemDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}
