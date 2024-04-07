using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
using System.Linq;
using LandSoft.Library;

namespace LandSoft.NghiepVu.GiaoDich
{
    public partial class PhieuDangKy_ctl : UserControl
    {
        bool KT = false, KT1 = false;
        int MaPGD = 0;
        string MaBDS = "";

        public PhieuDangKy_ctl()
        {
            InitializeComponent();

            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBC);
        }

        void SetDate(int index)
        {
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Index = index;
            objKBC.SetToDate();

            dateTuNgay.EditValueChanged -= new EventHandler(dateTuNgay_EditValueChanged);
            dateTuNgay.EditValue = objKBC.DateFrom;
            dateDenNgay.EditValue = objKBC.DateTo;
            dateTuNgay.EditValueChanged += new EventHandler(dateTuNgay_EditValueChanged);
        }

        private void cmbKyBC_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDate(cmbKyBC.SelectedIndex);
        }

        private void dateTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dateDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadPermission()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = LandSoft.Library.Common.PerID;
            o.AccessData.Form.FormID = 66;
            DataTable tblAction = o.SelectBy();
            btnThem.Enabled = false;
            barSubItemSua.Enabled = false;
            barSubItemPreview.Enabled = false;
            btnXoa.Enabled = false;
            btnIn.Enabled = false;
            btnThem.Enabled = false;
            barSubItemXacNhan.Enabled = false;
            btnDatCoc.Enabled = false;
            btnGiaoDichTC.Enabled = false;
            btnHinhAnh.Enabled = false;
            btnSearch.Enabled = false;

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
                            barSubItemSua.Enabled = true;
                            break;
                        case 3:
                            btnXoa.Enabled = true;
                            break;
                        case 4:
                            btnIn.Enabled = true;

                            break;
                        case 7://Xac thu thong tin
                            barSubItemXacNhan.Enabled = true;
                            break;
                        case 24://Dat coc
                            btnDatCoc.Enabled = true;
                            break;
                        case 30://Giao dich thanh cong
                            btnGiaoDichTC.Enabled = true;
                            break;
                        case 34://Preview
                            barSubItemPreview.Enabled = true;
                            break;
                        case 40://Hinh anh
                            btnHinhAnh.Enabled = true;
                            break;
                        case 41://Tim kiếm
                            btnSearch.Enabled = true;
                            break;
                    }
                }
            }
        }

        void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            

            it.pdkGiaoDichCls o = new it.pdkGiaoDichCls();
            o.NhanVien1.MaNV = LandSoft.Library.Common.StaffID;
            gridControl1.DataSource = o.Select(dateTuNgay.DateTime, dateDenNgay.DateTime);
            lookUpTinhTrang.DataSource = o.TinhTrang.Select();
            LoadLoaiGD();

            try { wait.Close(); wait.Dispose(); }
            catch { }
        }

        void LoadLoaiGD()
        {
            it.LoaiGiaoDichCls o = new it.LoaiGiaoDichCls();
            lookUpLoaiGD.DataSource = o.Select();
        }

        void LoadQuaTrinh()
        {
            it.pdkgdQuaTrinhThucHienCls o = new it.pdkgdQuaTrinhThucHienCls();
            gridControlQTTH.DataSource = o.Select(MaPGD);
            lookUpNhanVienQTTH.DataSource = o.NhanVien.SelectShow();
            lookUpTTQTTH.DataSource = o.TinhTrang.Select();
        }

        void LoadBDS()
        {
            it.pdkGiaoDichCls o = new it.pdkGiaoDichCls();
            o.MaGD = MaPGD;
            gridControl2.DataSource = o.SelectByBDS();
        }

        private void PhieuDangKy_ctl_Load(object sender, EventArgs e)
        {
            LoadPermission();
            cmbKyBC.SelectedIndex = 0;
            timer1.Start();
        }

        void Edit()
        {
            if (grvPDK.GetFocusedRowCellValue(colMaGD) != null)
            {
                PhieuDangKy_frm frm = new PhieuDangKy_frm();
                frm.MaGD = int.Parse(grvPDK.GetFocusedRowCellValue(colMaGD).ToString());
                frm.MaBDS = grvPDK.GetFocusedRowCellValue(colMaBDS).ToString();
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadData();
            }
            else
                DialogBox.Infomation("Vui lòng chọn phiếu đăng ký giao dịch muốn sửa. Xin cảm ơn.");
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (btnSua.Enabled)
                Edit();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (btnSua.Enabled)
                Edit();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PhieuDangKy_frm frm = new PhieuDangKy_frm();
            frm.ShowDialog();
            if (frm.IsUpdate)
                LoadData();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void btnDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPDK.GetFocusedRowCellValue(colMaGD) != null)
            {
                Duyet_frm frm = new Duyet_frm();
                frm.MaGD = int.Parse(grvPDK.GetFocusedRowCellValue(colMaGD).ToString());
                frm.MaTT = 2;
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadData();
            }
            else
                DialogBox.Infomation("Vui lòng chọn phiếu đăng ký giao dịch muốn duyệt. Xin cảm ơn.");
        }

        private void btnKhongDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPDK.GetFocusedRowCellValue(colMaGD) != null)
            {
                Duyet_frm frm = new Duyet_frm();
                frm.MaGD = int.Parse(grvPDK.GetFocusedRowCellValue(colMaGD).ToString());
                frm.MaTT = 3;
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadData();
            }
            else
                DialogBox.Infomation("Vui lòng chọn phiếu đăng ký giao dịch muốn duyệt. Xin cảm ơn.");
        }

        void LoadGeneral()
        {
            switch (xtraTabControl1.SelectedTabPageIndex)
            {
                case 0:
                    LoadBDS(); break;
                case 1:
                    LoadQuaTrinh(); break;
                case 2:
                    LoadNotes(); break;
                case 3:
                    MasterDataContext db = new MasterDataContext();
                    var objGD = db.pdkGiaoDiches.Single(p => p.MaGD == (int)grvPDK.GetFocusedRowCellValue("MaGD"));
                    var listData = db.mglmtMuaThues.Where(p=> (p.ChiaSe > 1 ||p.MaNVKD == objGD.MaNV1) &
                        (p.IsMua.Value ? 1 : 2) == objGD.MaLGD & p.MaLBDS == objGD.LoaiBDS &
                        p.DienTichTu <= objGD.DienTichXD & (p.DienTichDen >= objGD.DienTichXD || p.DienTichDen == 0) &
                        p.GiaTu * p.LoaiTien.TyGia <= objGD.TongTien * objGD.LoaiTien.TyGia & 
                        (p.GiaDen * p.LoaiTien.TyGia >= objGD.TongTien * objGD.LoaiTien.TyGia || p.GiaDen == 0) &
                        p.PhKhTu <= objGD.PhongKhach & (p.PhKhDen >= objGD.PhongKhach || p.PhKhDen == 0) &
                        p.PhNguTu <= objGD.PhongNgu & (p.PhNguDen >= objGD.PhongNgu || p.PhNguDen == 0) &
                        p.PhTamTu <= objGD.PhongWC & (p.PhTamDen >= objGD.PhongWC || p.PhTamTu == 0) &
                        p.TangTu <= objGD.SoTang & (p.TangDen >= objGD.SoTang || p.TangDen == 0) &
                        p.LauTu <= objGD.ViTriTang & (p.LauDen >= objGD.ViTriTang || p.LauDen == 0) &
                        (p.mglmtHuongs.Count == 0 || p.mglmtHuongs.Select(h => h.MaHuong).Contains(objGD.MaHuong)) &
                        (p.mglmtPhapLies.Count == 0 || p.mglmtPhapLies.Select(pl => pl.MaPL).Contains(objGD.MaPL)))
                    .OrderByDescending(p => p.NgayDK)
                    .AsEnumerable()
                    .Select((p, index) => new
                    {
                        STT = index + 1,
                        p.MaMT,
                        p.SoDK,
                        p.NgayDK,
                        p.ThoiHan,
                        HoTenKH = p.KhachHang.HoKH + " " + p.KhachHang.TenKH,
                        p.MaLBDS,
                        TenNC = p.IsMua.Value ? "Cần mua" : "cần thuê",
                        KhoangGia = string.Format("{0:#,0.##} -> {1:#,0.##} {2}", p.GiaTu, p.GiaDen, p.LoaiTien.TenLoaiTien),
                        DienTich = string.Format("{0:#,0.##} -> {1:#,0.##}", p.DienTichTu, p.DienTichDen),
                        PhongKhach = string.Format("{0} -> {1}", p.PhKhTu, p.PhKhDen),
                        PhongNgu = string.Format("{0} -> {1}", p.PhNguTu, p.PhNguDen),
                        PhongTam = string.Format("{0} -> {1}", p.PhTamTu, p.PhTamDen),
                        HoTenNV = p.NhanVien.HoTen
                    }).ToList();

                    gcMuaThue.DataSource = listData;
                    break;
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvPDK.GetFocusedRowCellValue(colMaGD) != null)
            {
                MaPGD = int.Parse(grvPDK.GetFocusedRowCellValue(colMaGD).ToString());
                MaBDS = grvPDK.GetFocusedRowCellValue(colMaBDS).ToString();
            }
            else
            {
                MaPGD = 0;
                MaBDS = "";
            }

            LoadGeneral();
        }

        private void btnDatCoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPDK.GetFocusedRowCellValue(colMaGD) != null)
            {
                PhieuDatCoc_frm frm = new PhieuDatCoc_frm();
                frm.ShowDialog();
            }
            else
                DialogBox.Infomation("Vui lòng chọn phiếu giao dịch muốn đặt cọc. Xin cảm ơn.");
        }

        private void btnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Search_frm frm = new Search_frm();
            if (grvPDK.GetFocusedRowCellValue(colMaGD) != null)
            {
                frm.MaGD1 = int.Parse(grvPDK.GetFocusedRowCellValue(colMaGD).ToString());
                frm.MaKH = int.Parse(grvPDK.GetFocusedRowCellValue(colMaKH).ToString());
                frm.MaBDS = grvPDK.GetFocusedRowCellValue(colMaBDS).ToString();
                frm.Share1 = bool.Parse(grvPDK.GetFocusedRowCellValue(colShare).ToString());
                frm.MaNV1 = int.Parse(grvPDK.GetFocusedRowCellValue(colMaNV1).ToString());
                frm.MaTT = byte.Parse(grvPDK.GetFocusedRowCellValue(colMaTT).ToString());                
            }
            frm.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            for (int i = 0; i < grvPDK.RowCount; i++)
            {
                if (int.Parse(grvPDK.GetRowCellValue(i, colSoGiay).ToString()) > 0)
                {
                    grvPDK.SetRowCellValue(i, colThoiHan, it.ConvertDateTimeCls.StringDateTime(int.Parse(grvPDK.GetRowCellValue(i, colSoGiay).ToString())));
                    grvPDK.SetRowCellValue(i, colSoGiay, int.Parse(grvPDK.GetRowCellValue(i, colSoGiay).ToString()) - 1);
                }
            }

            timer1.Start();
        }

        private void btnSupport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPDK.GetFocusedRowCellValue(colMaGD) != null)
            {
                YeuCauHoTro_frm frm = new YeuCauHoTro_frm();
                frm.MaGD = int.Parse(grvPDK.GetFocusedRowCellValue(colMaGD).ToString());
                frm.MaSo = grvPDK.GetFocusedRowCellValue(colSoPhieu).ToString();
                frm.MaNV = int.Parse(grvPDK.GetFocusedRowCellValue(colMaNV1).ToString());
                frm.NhanVien = grvPDK.GetFocusedRowCellValue(colHoTenNV1).ToString();
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadNotes();
            }
            else
                DialogBox.Infomation("Vui lòng chọn phiếu giao dịch muốn gửi yêu cầu hỗ trợ. Xin cảm ơn.");
        }

        void LoadNotes()
        {
            it.pgdNhatKyXuLyCls o = new it.pgdNhatKyXuLyCls();
            o.GiaoDich.MaGD = MaPGD;
            gridControl3.DataSource = o.SelectBy();
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (grvPDK.GetFocusedRowCellValue(colMaGD) != null)
            {
                MaPGD = int.Parse(grvPDK.GetFocusedRowCellValue(colMaGD).ToString());
                MaBDS = grvPDK.GetFocusedRowCellValue(colMaBDS).ToString();
            }
            else
            {
                MaPGD = 0;
                MaBDS = "";
            }

            LoadGeneral();
        }

        private void btnSendSupport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnSupport_ItemClick(sender, e);
        }

        private void btnReplySupport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView4.GetFocusedRowCellValue(colSTT) != null)
            {
                if (int.Parse(gridView4.GetFocusedRowCellValue(colMaNVPT).ToString()) == LandSoft.Library.Common.StaffID)
                {
                    Reply_frm frm = new Reply_frm();
                    frm.MaSo = grvPDK.GetFocusedRowCellValue(colSoPhieu).ToString();
                    frm.MaGD = int.Parse(grvPDK.GetFocusedRowCellValue(colMaGD).ToString());
                    frm.HoTenNV = gridView4.GetFocusedRowCellValue(colHoTenNVXL).ToString();
                    frm.STT = int.Parse(gridView4.GetFocusedRowCellValue(colSTT).ToString());
                    frm.ShowDialog();
                    if (frm.IsUpdate)
                        LoadNotes();
                }
                else
                    DialogBox.Infomation("Bạn không phải là người phụ trách phiếu giao dịch này nên bạn không có quyền giải đáp yêu cầu. Xin cảm ơn.");
            }
            else
                DialogBox.Infomation("Vui lòng chọn yêu cầu hỗ trợ muốn giải đáp. Xin cảm ơn.");
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPDK.GetFocusedRowCellValue(colMaGD) != null)
            {
                if (byte.Parse(grvPDK.GetFocusedRowCellValue(colMaTT).ToString()) == 2)
                {
                    if (DialogBox.Question("Bạn có chắc chắn muốn xóa phiếu đăng ký giao dịch này không?") == DialogResult.Yes)
                    {
                        it.pdkGiaoDichCls o = new it.pdkGiaoDichCls();
                        o.MaGD = int.Parse(grvPDK.GetFocusedRowCellValue(colMaGD).ToString());                        
                        o.Delete();
                        LoadData();
                    }
                }
                else
                    DialogBox.Infomation("Phiếu đăng ký giao dịch này đã phát sinh những giao dịch khác nên không thể hủy.\n\t\tVui lòng kiểm tra lại, xin cảm ơn.");
            }
            else
                DialogBox.Infomation("Vui lòng chọn phiếu đăng ký giao dịch muốn hủy. Xin cảm ơn.");
        }

        private void btnGiaoDichTC_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPDK.GetFocusedRowCellValue(colMaGD) != null)
            {
                switch (byte.Parse(grvPDK.GetFocusedRowCellValue(colMaTT).ToString()))
                {
                    case 1:
                    case 2:
                    case 3:
                    case 9:
                        DialogBox.Infomation("Chỉ duyệt giao dịch thành công đối với những phiếu đăng ký giao dịch đang ở tình trạng \n<Giữ chỗ, Đặt cọc, Ký hợp đồng mua bán>.\n\t\tVui lòng kiểm tra lại, xin cảm ơn.");
                        break;
                    case 4:
                    case 5:
                        LandSoft.HDMB.GiayXacNhan_frm frm = new LandSoft.HDMB.GiayXacNhan_frm();
                        frm.MaGD1 = int.Parse(grvPDK.GetFocusedRowCellValue(colMaGD).ToString());
                        frm.MaGD2 = int.Parse(grvPDK.GetFocusedRowCellValue(colMaGD2).ToString());
                        frm.SoPhieu = grvPDK.GetFocusedRowCellValue(colSoPhieu).ToString();
                        frm.MaSo = grvPDK.GetFocusedRowCellValue(colMaSo).ToString();
                        frm.MaBDS = grvPDK.GetFocusedRowCellValue(colMaBDS).ToString();
                        frm.HoTen = grvPDK.GetFocusedRowCellValue(colHoTenKH).ToString();
                        frm.MaPGC = int.Parse(grvPDK.GetFocusedRowCellValue(colMaPGC).ToString());
                        frm.MaKH = int.Parse(grvPDK.GetFocusedRowCellValue(colMaKH).ToString());
                        frm.ShowDialog();
                        if (frm.IsUpdate)
                        {
                            //Cap nhat tinh trang cho phieu giao dich
                            it.pgcPhieuGiuChoCls o = new it.pgcPhieuGiuChoCls();
                            o.MaPGC = int.Parse(grvPDK.GetFocusedRowCellValue(colMaPGC).ToString());
                            o.UpdateTransaction();

                            LoadData();
                        }
                        break;
                    case 8:
                        DialogBox.Infomation("Phiếu đăng ký giao dịch ngày đã cấp giấy xác nhận qua sàn.\n\t\tVui lòng kiểm tra lại, xin cảm ơn.");
                        break;
                }                
            }
            else
                DialogBox.Infomation("Vui lòng chọn phiếu đăng ký giao dịch muốn cập nhật tình trang giao dịch. Xin cảm ơn.");
        }

        private void btnHinhAnh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (grvPDK.GetFocusedRowCellValue(colMaGD) != null)
            //{
            //    Gallery_frm frm = new Gallery_frm();
            //    frm.MaGD = int.Parse(grvPDK.GetFocusedRowCellValue(colMaGD).ToString());
            //    frm.ShowDialog();
            //}
            //else
            //    DialogBox.Infomation("Vui lòng chọn phiếu đăng ký giao dịch muốn xem hình ảnh. Xin cảm ơn.");
        }

        private void btnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPDK.GetFocusedRowCellValue(colMaGD) != null)
            {                
                Report.GiaoDich.PDKGiaoDich_rpt rpt = new LandSoft.Report.GiaoDich.PDKGiaoDich_rpt(int.Parse(grvPDK.GetFocusedRowCellValue(colMaGD).ToString()));
                rpt.Print();
            }
            else
                DialogBox.Infomation("Vui lòng chọn phiếu đăng ký giao dịch muốn in. Xin cảm ơn.");
        }

        private void btnPreview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPDK.GetFocusedRowCellValue(colMaGD) != null)
            {
                Report.GiaoDich.ShowGiaoDich_frm frm = new LandSoft.Report.GiaoDich.ShowGiaoDich_frm(int.Parse(grvPDK.GetFocusedRowCellValue(colMaGD).ToString()));
                frm.ShowDialog();
            }
            else
                DialogBox.Infomation("Vui lòng chọn phiếu đăng ký giao dịch muốn preview. Xin cảm ơn.");
        }

        private void btnInGiayXN_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPDK.GetFocusedRowCellValue(colMaGD) != null)
            {
                if (int.Parse(grvPDK.GetFocusedRowCellValue(colMaGXN).ToString()) != 0)
                {
                    LandSoft.HDMB.GiayXacNhanCls obj = new LandSoft.HDMB.GiayXacNhanCls();
                    obj.Print(int.Parse(grvPDK.GetFocusedRowCellValue(colMaGXN).ToString()), int.Parse(grvPDK.GetFocusedRowCellValue(colMaGD).ToString()));
                }
                else
                    DialogBox.Infomation("Phiếu đăng ký giao dịch này chưa cấp giấy xác nhận giao dịch qua sàn nên không thể in được.\n\t\tVui lòng kiểm tra lại, xin cảm ơn.");
            }
            else
                DialogBox.Infomation("Vui lòng chọn phiếu đăng ký giao dịch muốn in giấy xác nhận giao dịch qua sàn. Xin cảm ơn.");
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                e.Appearance.BackColor = Color.FromArgb(int.Parse(grvPDK.GetRowCellValue(e.RowHandle, colMauSac).ToString()));
            }
            catch { }
        }

        private void btnExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPDK.GetFocusedRowCellValue(colMaGD) != null)
            {
                SaveFileDialog fbd = new SaveFileDialog();
                fbd.Title = "Chọn thư mục lưu file";
                fbd.Filter = "File Rich Text Format(.rtf)|*.rtf";
                fbd.FileName = string.Format("{0}.rtf", grvPDK.GetFocusedRowCellValue(colSoPhieu).ToString());
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    Report.GiaoDich.PDKGiaoDich_rpt rpt = new LandSoft.Report.GiaoDich.PDKGiaoDich_rpt(int.Parse(grvPDK.GetFocusedRowCellValue(colMaGD).ToString()));
                    rpt.ExportToRtf(fbd.FileName);
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn phiếu đăng ký giao dịch muốn export. Xin cảm ơn.");            
        }

        private void btnSuaGXN_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPDK.GetFocusedRowCellValue(colMaGD) != null)
            {
                if (int.Parse(grvPDK.GetFocusedRowCellValue(colMaGXN).ToString()) != 0)
                {
                    LandSoft.Khac.Editor_frm frm = new LandSoft.Khac.Editor_frm();
                    frm.Template = it.CommonCls.Row("GiayXacNhan_getTemplate " + int.Parse(grvPDK.GetFocusedRowCellValue(colMaGXN).ToString()))["Template"].ToString();
                    frm.LoaiHD = 4;
                    frm.ShowDialog();

                    it.GiayXacNhanCls o = new it.GiayXacNhanCls();
                    o.MaGXN = int.Parse(grvPDK.GetFocusedRowCellValue(colMaGXN).ToString());
                    o.Template = frm.Template;
                    o.UpdateTemplate();
                }
                else
                    DialogBox.Infomation("Phiếu đăng ký giao dịch này chưa cấp giấy xác nhận giao dịch qua sàn nên không thể sửa được.\n\t\tVui lòng kiểm tra lại, xin cảm ơn.");
            }
            else
                DialogBox.Infomation("Vui lòng chọn phiếu đăng ký giao dịch muốn sửa giấy xác nhận giao dịch qua sàn. Xin cảm ơn.");   
        }

        private void btnPreviewGXN_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPDK.GetFocusedRowCellValue(colMaGD) != null)
            {
                if (int.Parse(grvPDK.GetFocusedRowCellValue(colMaGXN).ToString()) != 0)
                {
                    LandSoft.HDMB.GiayXacNhanCls o = new LandSoft.HDMB.GiayXacNhanCls();
                    NghiepVu.Khac.Review_frm frm = new LandSoft.NghiepVu.Khac.Review_frm();
                    frm.Content = o.ExportRtf2(int.Parse(grvPDK.GetFocusedRowCellValue(colMaGXN).ToString()), int.Parse(grvPDK.GetFocusedRowCellValue(colMaGD).ToString()));
                    frm.ShowDialog();
                }
                else
                    DialogBox.Infomation("Phiếu đăng ký giao dịch này chưa cấp giấy xác nhận giao dịch qua sàn nên không thể preview được.\n\t\tVui lòng kiểm tra lại, xin cảm ơn.");
            }
            else
                DialogBox.Infomation("Vui lòng chọn phiếu đăng ký giao dịch muốn preview giấy xác nhận giao dịch qua sàn. Xin cảm ơn.");   
        }
    }
}
