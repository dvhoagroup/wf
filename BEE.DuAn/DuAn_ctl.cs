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

namespace BEE.DuAn
{
    public partial class DuAn_ctl : XtraUserControl
    {
        MasterDataContext db = new MasterDataContext();

        int MaDA = 0;
        int maNV = 0;

        public DuAn_ctl()
        {
            InitializeComponent();

            it.DuAnCls o = new it.DuAnCls();
            lookUpLoaiDA.DataSource =  o.LoaiDA.Select();
            //lookUpLoaiDA.DataSource = db.DuAn_getListByStaff(Common.StaffID);
            lookUpNhanVien.DataSource = o.NhanVien.SelectShow();
        }

        private void DuAn_ctl_Load(object sender, EventArgs e)
        {

            LoadData();
            LoadPermission();

            BEE.NgonNgu.Language.TranslateUserControl(this, barManager1);
        }

        void LoadData()
        {
            it.DuAnCls o = new it.DuAnCls();
            gcDuAn.DataSource = o.SelectShow();
        }

        void LoadPermission()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = Common.PerID;
            o.AccessData.Form.FormID = 1;
            DataTable tblAction = o.SelectBy();
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBlock.Enabled = false;
            btnLichThanhToan.Enabled = false;
            btnCapNhat.Enabled = false;
            subConfirm.Enabled = false;

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
                        case 7:
                            subConfirm.Enabled = true;
                            break;
                        case 15:
                            btnBlock.Enabled = true;
                            break;
                        case 29://Lich thanh toan
                            btnLichThanhToan.Enabled = true;
                            btnCapNhat.Enabled = true;
                            break;
                    }
                }
            }
        }

        void DuAn_Click()
        {
            if (grvDuAn.GetFocusedRowCellValue("MaDA") == null)
            {
                gcBieuMau.DataSource = null;
                //ctlChinhSach1.ChinhSach_Clear();
                ctlPromotion1.ClearData();
                return;
            }

            db = new MasterDataContext();
            int maDA = (int)grvDuAn.GetFocusedRowCellValue("MaDA");
            switch (tabMain.SelectedTabPageIndex)
            {
                case 0:
                    gcLichSu.DataSource = db.daLichSus.Where(p => p.MaDA == maDA)
                        .Select(p => new { 
                            p.DienGiai,
                            p.IsApprove,
                            NhanVien = p.NhanVien.HoTen,
                            p.NgayTH
                        }).ToList();
                    break;
                case 1:
                    gcBieuMau.DataSource = db.daBieuMaus.Where(p => p.MaDA == maDA)
                        .OrderBy(p => p.MaLBM).OrderBy(p => p.NgayCN)
                        .Select(p => new { p.MaBM, p.TenBM, p.DienGiai, p.daLoaiBieuMau.TenLBM, p.Khoa, p.NgayCN, p.NhanVien.HoTen })
                        .ToList();
                    break;
                case 2:
                    ctlTaiLieu1.FormID = 1;
                    ctlTaiLieu1.LinkID = maDA;
                    ctlTaiLieu1.MaNV = (int?)grvDuAn.GetFocusedRowCellValue("MaNV");
                    ctlTaiLieu1.TaiLieu_Load();
                    break;
                case 3:
                    gridControlStaff.DataSource = db.DuAn_NhanViens.Where(o => o.ProjectID == maDA)
                                            //.OrderBy(o => o.DateCreate)
                                            .OrderByDescending(o => o.DateCreate)
                                            .AsEnumerable()
                                            .Select((o, index) => new
                                            {
                                                STT = index + 1,
                                                ID = o.ID,
                                                HoTen = o.NhanVien.HoTen,
                                                NgaySinh = o.NhanVien.NgaySinh,
                                                DiaChi = o.NhanVien.DiaChi,
                                                DateCreate = o.DateCreate
                                            }).ToList();
                    break;
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DuAn_Click();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DuAn_frm frm = new DuAn_frm();
            frm.ShowDialog();
            if (frm.IsUpdate)
                LoadData();
        }

        void View()
        {            
            if (grvDuAn.GetFocusedRowCellValue(colMaDa) != null)
            {
                //if (int.Parse(grvDuAn.GetFocusedRowCellValue(colMaDa).ToString()) == 1)
                //{
                //    DialogBox.Infomation("[Dự án] này không sửa được. Vui lòng kiểm tra lại, xin cảm ơn.");
                //    return;
                //}
                int row = grvDuAn.FocusedRowHandle;
                DuAn_frm frm = new DuAn_frm();
                frm.MaDA = int.Parse(grvDuAn.GetFocusedRowCellValue(colMaDa).ToString());
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadData();
                grvDuAn.FocusedRowHandle = row;

            }
            else
                DialogBox.Infomation("Vui lòng chọn [Dự án] cần sửa. Xin cảm ơn.\r\n\r\nPlease select [Project]. Thanks!");
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvDuAn.GetFocusedRowCellValue(colMaDa) != null)
            {
                //if (int.Parse(grvDuAn.GetFocusedRowCellValue(colMaDa).ToString()) == 1)
                //{
                //    DialogBox.Infomation("[Dự án] này không xóa được. Vui lòng kiểm tra lại, xin cảm ơn.");
                //    return;
                //}
                if (DialogBox.Question("Bạn có chắc chắn muốn xóa [Dự án]: <" + grvDuAn.GetFocusedRowCellValue(colTenDA).ToString() + "> ra khỏi hệ thống không?") == DialogResult.Yes)
                {
                    try
                    {
                        it.DuAnCls o = new it.DuAnCls();
                        o.MaDA = int.Parse(grvDuAn.GetFocusedRowCellValue(colMaDa).ToString());
                        o.Delete();
                        grvDuAn.DeleteSelectedRows();
                    }
                    catch
                    {
                        DialogBox.Infomation("Xóa không thành công vì [Dự án]: <" + grvDuAn.GetFocusedRowCellValue(colTenDA).ToString() + "> đã được sử dụng. Vui lòng kiểm tra lại.");
                    }
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn [Dự án] cần xóa. Xin cảm ơn.\r\n\r\nPlease select [Project]. Thanks!");
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (btnSua.Enabled)
                View();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            View();
        }

        private void btnBlock_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvDuAn.GetFocusedRowCellValue(colMaDa) != null)
            {
                Blocks_frm frm = new Blocks_frm();
                frm.MaDA = int.Parse(grvDuAn.GetFocusedRowCellValue(colMaDa).ToString());
                frm.ShowDialog();
            }
            else
                DialogBox.Infomation("Vui lòng chọn [Dự án] để thêm block. Xin cảm ơn.\r\n\r\nPlease select [Project]. Thanks!");
        }

        private void btnLichThanhToan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvDuAn.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn [Dự án], xin cảm ơn.\r\n\r\nPlease select [Project]. Thanks!");
                return;
            }

            frmLichThanhToan frm = new frmLichThanhToan();
            frm.MaDA = (int)grvDuAn.GetFocusedRowCellValue("MaDA");
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
                DuAn_Click();
        }

        private void hplDownload_Click(object sender, EventArgs e)
        {
            if (grvDuAn.GetFocusedRowCellValue(colMaDa) != null)
            {
                if (grvDuAn.GetFocusedRowCellValue(colFileAttach).ToString() != "")
                {
                    FolderBrowserDialog fbd = new FolderBrowserDialog();
                    fbd.Description = "Chọn thư mục để lưu file tải về";
                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        it.FTPCls objFTP = new it.FTPCls();
                        objFTP.GetAccountFTP();
                        objFTP.Download(fbd.SelectedPath, grvDuAn.GetFocusedRowCellValue(colFileAttach).ToString());
                    }
                }
                else
                    DialogBox.Infomation("[Dự án] này không có file đính kèm nên không thể tải về.");
            }
        }

        private void btnKhu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvDuAn.GetFocusedRowCellValue(colMaDa) != null)
            {
                Khu_frm frm = new Khu_frm();
                frm.MaDA = int.Parse(grvDuAn.GetFocusedRowCellValue(colMaDa).ToString());
                frm.ShowDialog();
            }
            else
                DialogBox.Infomation("Vui lòng chọn [Dự án] để thực hiện chức năng này. Xin cảm ơn.\r\n\r\nPlease select [Project]. Thanks!");
        }

        private void tabMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            DuAn_Click();
        }

        void BieuMau_Them()
        {
            int? maDA = (int?)grvDuAn.GetFocusedRowCellValue("MaDA");
            if (maDA == null)
            {
                DialogBox.Error("Vui lòng chọn [Dự án]. Xin cảm ơn.\r\n\r\nPlease select [Project]. Thanks!");
                return;
            }

            BieuMau.frmEdit frm = new BieuMau.frmEdit();
            frm.MaDA = maDA.Value;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
                DuAn_Click();
        }

        void BieuMau_Sua()
        {
            int? maBM = (int?)grvBieuMau.GetFocusedRowCellValue("MaBM");
            if (maBM == null)
            {
                DialogBox.Error("Vui lòng chọn [Biểu mẫu]. Xin cảm ơn.\r\n\r\nPlease select [Template]. Thank!");
                return;
            }

            BieuMau.frmEdit frm = new BieuMau.frmEdit();
            frm.MaBM = maBM.Value;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
                DuAn_Click();
        }

        void BieuMau_Xoa()
        {
            var indexs = grvBieuMau.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn [Biểu mẫu]. Xin cảm ơn.\r\n\r\nPlease select [Template]. Thank!");
                return;
            }

            if (DialogBox.Question() == DialogResult.No) return;

            foreach (var i in indexs)
            {
                daBieuMau objBM = db.daBieuMaus.Single(p => p.MaBM == (int)grvBieuMau.GetRowCellValue(i, "MaBM"));
                db.daBieuMaus.DeleteOnSubmit(objBM);
            }

            db.SubmitChanges();

            DuAn_Click();
        }

        private void itemBieuMau_Them_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BieuMau_Them();   
        }

        private void itemBieuMau_Sua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BieuMau_Sua();
        }

        private void itemBieuMau_Xoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BieuMau_Xoa();
        }

        private void grvBieuMau_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                BieuMau_Xoa();
            }
        }

        private void grvBieuMau_DoubleClick(object sender, EventArgs e)
        {
            BieuMau_Sua();
        }

        private void itemBieuMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BieuMau_Them();   
        }

        private void itemAgree_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Confirm(true);
        }

        private void itemDisagree_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Confirm(false);
        }

        void Confirm(bool val)
        {
            if (grvDuAn.GetFocusedRowCellValue(colMaDa) != null)
            {
                var f = new BEEREMA.Project.frmApprove();
                f.ProjectID = (int?)grvDuAn.GetFocusedRowCellValue("MaDA");
                f.IsApprove = val;
                f.ShowDialog();
                if (f.DialogResult == DialogResult.OK)
                    LoadData();
            }
            else
                DialogBox.Infomation("Vui lòng chọn [Dự án] để thực hiện chức năng này. Xin cảm ơn!\r\n\r\nPlease select [Project]. Thanks!");
        }
       

        private void itemXoaNV_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.Question("Bạn có muốn xóa [Nhân viên quản lý]?") == DialogResult.No) return;
            DuAn_NhanVien objNHQL = db.DuAn_NhanViens.Single(p => p.ID == (int)gridViewStaff.GetFocusedRowCellValue("ID"));
            db.DuAn_NhanViens.DeleteOnSubmit(objNHQL);
            db.SubmitChanges();
            DuAn_Click();
        }
    }
}
