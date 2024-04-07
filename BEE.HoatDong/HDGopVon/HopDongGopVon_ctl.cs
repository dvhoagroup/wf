using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace LandSoft.NghiepVu.HDGopVon
{
    public partial class HopDongGopVon_ctl : UserControl
    {
        int MaHDGV = 0;
        bool KT = false, KT1 = false;
        public HopDongGopVon_ctl()
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

        int GetAccessData()
        {
            it.AccessDataCls o = new it.AccessDataCls(LandSoft.Library.Common.PerID, 29);

            return o.SDB.SDBID;
        }

        void LoadData()
        {
            it.hdGopVonCls o = new it.hdGopVonCls();
            switch (GetAccessData())
            {
                case 1://Tat ca
                    gridControl1.DataSource = o.Select(dateTuNgay.DateTime, dateDenNgay.DateTime);
                    break;
                case 2://Theo nhom
                    gridControl1.DataSource = o.SelectByGroup(dateTuNgay.DateTime, dateDenNgay.DateTime, LandSoft.Library.Common.StaffID, LandSoft.Library.Common.GroupID);
                    break;
                case 3://Theo phong ban
                    gridControl1.DataSource = o.SelectByDeparment(dateTuNgay.DateTime, dateDenNgay.DateTime, LandSoft.Library.Common.StaffID, LandSoft.Library.Common.DepartmentID);
                    break;
                case 4://Theo nhan vien
                    gridControl1.DataSource = o.SelectByStaff(dateTuNgay.DateTime, dateDenNgay.DateTime, LandSoft.Library.Common.StaffID);
                    break;
                default:
                    gridControl1.DataSource = null;
                    break;
            }
            lookUpTinhTrang.DataSource = o.TinhTrang.Select();
            lookUpNhanVienKT.DataSource = o.NhanVien.SelectShow();
        }

        private void PhieuGiuCho_ctl_Load(object sender, EventArgs e)
        {
            cmbKyBC.SelectedIndex = 0;
        }

        void LoadLichTT()
        {
            it.hdGopVonCls o = new it.hdGopVonCls();
            gridControl2.DataSource = o.LichThanhToan(MaHDGV);
        }

        void LoadPhieuThu()
        {
            it.hdgvPhieuThuCls o = new it.hdgvPhieuThuCls();
            o.Lich.HDGV.MaHDGV = MaHDGV;
            gridControlPT.DataSource = o.SelectBy();
            lookUpLoaiTien2.DataSource = o.LoaiTien.Select();
            lookUpNhanVien2.DataSource = o.NhanVien.SelectShow();
        }

        void LoadQuaTrinh()
        {
            it.hdgvQuaTrinhThucHienCls o = new it.hdgvQuaTrinhThucHienCls();
            gridControlQTTH.DataSource = o.Select(MaHDGV);
            lookUpNhanVienQTTH.DataSource = o.NhanVien.SelectShow();
            lookUpTTQTTH.DataSource = o.TinhTrang.Select();
        }

        void LoadPhieuChi()
        {
            it.hdgvPhieuChiCls o = new it.hdgvPhieuChiCls();
            gridControlPC.DataSource = o.Select(MaHDGV);
            lookUpLoaiTienPC.DataSource = o.LoaiTien.Select();
            lookUpNhanVienPC.DataSource = o.NhanVien.SelectShow();
        }

        void LoadHoaDon()
        {
            it.HoaDonGTGTCls o = new it.HoaDonGTGTCls();
            o.MaHDGV = MaHDGV;
            gridControlHoaDon.DataSource = o.SelectByHDGV();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaHDGV) != null)
                MaHDGV = int.Parse(gridView1.GetFocusedRowCellValue(colMaHDGV).ToString());
            else
                MaHDGV = 0;
            LoadLichTT();
            LoadPhieuThu();
            LoadQuaTrinh();
            LoadPhieuChi();
            LoadNhacNo();
            LoadHoaDon();
        }

        private void btnThuTien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ThuTien();
        }

        private void btnThuTienTD_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ThuTien();
        }

        void ThuTien()
        {
            if (gridView1.GetFocusedRowCellValue(colMaHDGV) != null)
            {
                if (byte.Parse(gridView1.GetFocusedRowCellValue(colMaTT).ToString()) == 1 || byte.Parse(gridView1.GetFocusedRowCellValue(colMaTT).ToString()) == 3)
                {
                    DialogBox.Infomation("Hợp đồng góp vốn này chưa được duyệt nên không thể thu tiền. Vui lòng kiểm tra lại, xin cảm ơn.");
                    return;
                }

                if (byte.Parse(gridView1.GetFocusedRowCellValue(colMaTT).ToString()) == 1 || byte.Parse(gridView1.GetFocusedRowCellValue(colMaTT).ToString()) == 5)
                {
                    DialogBox.Infomation("Hợp đồng góp vốn này đã ra hợp đồng mua bán nên không thể thu tiền, nếu muốn thu tiền thì bạn vui lòng sang hợp đồng mua bán. Vui lòng kiểm tra lại, xin cảm ơn.");
                    return;
                }

                int row = gridView1.FocusedRowHandle;
                PhieuThu2_frm frm = new PhieuThu2_frm();
                frm.MaHDGV = int.Parse(gridView1.GetFocusedRowCellValue(colMaHDGV).ToString());
                frm.HoTenKH = gridView1.GetFocusedRowCellValue(colHoTenKH).ToString();
                frm.MaKH = int.Parse(gridView1.GetFocusedRowCellValue(colMaKH).ToString());
                frm.MaBDS = gridView1.GetFocusedRowCellValue(colMaBDS).ToString();
                frm.ShowDialog();
                if (frm.IsUpdate)
                {
                    LoadData();
                    gridView1.FocusedRowHandle = row;
                    LoadLichTT();
                }
            }
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaHDGV) != null)
                MaHDGV = int.Parse(gridView1.GetFocusedRowCellValue(colMaHDGV).ToString());
            else
                MaHDGV = 0;
            LoadLichTT();
            LoadPhieuThu();
            LoadQuaTrinh();
            LoadPhieuChi();
            LoadNhacNo();
            LoadHoaDon();
        }

        private void btnXacThucDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Duyet(2);
        }

        private void btnXacThucKhongDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Duyet(3);
        }        

        void Duyet(byte _MaTT)
        {
            if (gridView1.GetFocusedRowCellValue(colMaHDGV) != null)
            {
                int RowIndex = 0;
                MaHDGV = int.Parse(gridView1.GetFocusedRowCellValue(colMaHDGV).ToString());
                RowIndex = gridView1.FocusedRowHandle;
                //Giam doc khong ky
                Duyet_frm frm = new Duyet_frm();
                frm.MaTT = _MaTT;
                frm.MaHDGV = MaHDGV;
                frm.ShowDialog();
                if (frm.IsUpdate)
                {
                    LoadData();
                    LoadQuaTrinh();
                    gridView1.FocusedRowHandle = RowIndex;
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn hợp đồng góp vốn cần duyệt. Xin cảm ơn");
        }

        private void barButtonItemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnThem_ItemClick(sender, e);
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //PhieuGiuCho_frm frm = new PhieuGiuCho_frm();
            //frm.ShowDialog();
            //if (frm.IsUpdate)
            //    LoadData();
        }

        private void barButtonItemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnXoa_ItemClick(sender, e);
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaHDGV) != null)
            {
                switch (int.Parse(gridView1.GetFocusedRowCellValue(colMaTT).ToString()))
                {
                    case 1:
                        DialogBox.Infomation("Hợp đồng góp vốn này đang chờ duyệt nên không thể thanh lý. Vui lòng kiểm tra lại, in cảm ơn");
                        break;
                    case 2:
                        if (DialogBox.Question("Bạn có chắc chắn muốn thanh lý hợp đồng góp vốn: <" + gridView1.GetFocusedRowCellValue(colSoPhieu).ToString() + "> không?") == DialogResult.Yes)
                        {
                            try
                            {
                                int row = gridView1.FocusedRowHandle;
                                ThanhLy_frm frm = new ThanhLy_frm();
                                frm.MaHDGV = int.Parse(gridView1.GetFocusedRowCellValue(colMaHDGV).ToString());
                                frm.MaSo = gridView1.GetFocusedRowCellValue(colMaSo).ToString();
                                frm.MaKH = int.Parse(gridView1.GetFocusedRowCellValue(colMaKH).ToString());
                                frm.ShowDialog();
                                if (frm.IsUpdate)
                                {
                                    LoadData();
                                    LoadQuaTrinh();
                                    gridView1.FocusedRowHandle = row;
                                }
                            }
                            catch
                            {
                                DialogBox.Infomation("Thanh lý không thành công vì hợp đồng góp vốn: <" + gridView1.GetFocusedRowCellValue(colSoPhieu).ToString() + "> đã được sử dụng. Vui lòng kiểm tra lại.");
                            }
                        }
                        break;
                    case 3:
                        DialogBox.Infomation("Hợp đồng góp vốn này không được duyệt nên không thể thanh lý. Vui lòng kiểm tra lại, in cảm ơn");
                        break;
                    case 4:
                        DialogBox.Infomation("Hợp đồng góp vốn này đã hủy nên không thể thanh lý. Vui lòng kiểm tra lại, in cảm ơn");
                        break;
                    case 5:
                        DialogBox.Infomation("Hợp đồng góp vốn này đã ra hợp đồng mua bán nên không thể thanh lý. Vui lòng kiểm tra lại, in cảm ơn");
                        break;
                    case 6:
                        DialogBox.Infomation("Hợp đồng góp vốn này đang ở tình trạng <Chờ duyệt thanh lý> nên không thể thanh lý nữa. Vui lòng kiểm tra lại, in cảm ơn");
                        break;
                    case 7:
                        DialogBox.Infomation("Hợp đồng góp vốn này đã thanh lý nên không thể thanh lý nữa. Vui lòng kiểm tra lại, in cảm ơn");
                        break;
                }                
            }
            else
                DialogBox.Infomation("Vui lòng chọn hợp đồng góp vốn muốn thanh lý. Xin cảm ơn");
        }

        private void barButtonItemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditPGC();
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditPGC();
        }

        void EditPGC()
        {
            if (gridView1.GetFocusedRowCellValue(colMaHDGV) != null)
            {
                if (gridView1.GetFocusedRowCellValue(colMaTT).ToString() == "12")
                    DialogBox.Infomation("Hợp đồng góp vốn này đã ký hợp đồng góp vốn nên không thể sửa. Vui lòng kiểm tra lại, xin cảm ơn");
                else
                {
                    //PhieuGiuCho_frm frm = new PhieuGiuCho_frm();
                    //frm.MaHDGV = int.Parse(gridView1.GetFocusedRowCellValue(colMaHDGV).ToString());
                    //frm.ShowDialog();
                    //if (frm.IsUpdate)
                    //    LoadData();
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn hợp đồng góp vốn muốn sửa. Xin cảm ơn");
        }

        private void barButtonItemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnNap_ItemClick(sender, e);
        }

        private void barButtonItemXTTTDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Duyet(2);
        }

        private void barButtonItemXTTTKhongDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Duyet(3);
        }

        private void barButtonItemThuTienDT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Duyet(4);
        }

        private void barButtonItemThuTienCT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Duyet(5);
        }

        private void barButtonItemGDDD_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Duyet(6);
        }

        private void barButtonItemGDKD_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Duyet(7);
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            EditPGC();
        }

        private void barButtonItemThuTien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ThuTien();
        }

        private void barButtonItemCapNhatTD_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaHDGV) != null)
            {
                //LichThanhToan_frm frm = new LichThanhToan_frm();
                //frm.MaHDGV = int.Parse(gridView1.GetFocusedRowCellValue(colMaHDGV).ToString());
                //frm.ShowDialog();
            }
            else
                DialogBox.Infomation("Vui lòng chọn hợp đồng góp vốn để cập nhật lịch thanh toán. Xin cảm ơn");
        }

        private void btnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Import_frm frm = new Import_frm();
            frm.ShowDialog();
            if (frm.IsUpdate)
                LoadData();
        }

        private void btnRaHDMB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaHDGV) != null)
            {
                switch (int.Parse(gridView1.GetFocusedRowCellValue(colMaTT).ToString()))
                {
                    case 1:
                        DialogBox.Infomation("Hợp đồng góp vốn này đang chờ duyệt nên không thể ra hợp đồng mua bán. Vui lòng kiểm tra lại, in cảm ơn");
                        break;
                    case 2:
                        LandSoft.HDMB.HDMBConvertHDGV_frm frm = new LandSoft.HDMB.HDMBConvertHDGV_frm();
                        frm.MaBDS = gridView1.GetFocusedRowCellValue(colMaBDS).ToString();
                        frm.MaHDGV = int.Parse(gridView1.GetFocusedRowCellValue(colMaHDGV).ToString());
                        frm.ShowDialog();
                        if (frm.IsUpdate)
                            LoadData();
                        break;
                    case 3:
                        DialogBox.Infomation("Hợp đồng góp vốn này không được duyệt nên không thể ra hợp đồng mua bán. Vui lòng kiểm tra lại, in cảm ơn");
                        break;
                    case 4:
                        DialogBox.Infomation("Hợp đồng góp vốn này đã hủy nên không thể ra hợp đồng mua bán. Vui lòng kiểm tra lại, in cảm ơn");
                        break;
                    case 5:
                        DialogBox.Infomation("Hợp đồng góp vốn này đã ra hợp đồng mua bán. Vui lòng kiểm tra lại, in cảm ơn");
                        break;
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn hợp đồng góp vốn để ra hợp đồng mua bán. Xin cảm ơn");
        }

        void LoadNhacNo()
        {
            it.hdgvNhacNoCls o = new it.hdgvNhacNoCls();
            gridControlNhacNo.DataSource = o.SelectAllBy(MaHDGV);
        }

        void NhacNo(byte _Lan)
        {
            if (gridView1.GetFocusedRowCellValue(colMaHDGV) != null)
            {
                if (gridLichThanhToan.RowCount <= 0)
                {
                    DialogBox.Infomation("Hợp đồng góp vốn này không có lịch thanh toán nên không thể nhắc nợ. Vui lòng kiểm tra lại, xin cảm ơn.");
                    return;
                }
                NhacNo_frm frm = new NhacNo_frm();
                frm.MaHDGV = int.Parse(gridView1.GetFocusedRowCellValue(colMaHDGV).ToString());
                frm.LanNN = _Lan;
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadNhacNo();
                //}
            }
            else
                DialogBox.Infomation("Vui lòng chọn hợp đồng góp vốn muốn nhắc nợ. Xin cảm ơn");
        }

        private void btnNhacNo1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NhacNo(1);
        }

        private void btnDuyetTL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaHDGV) != null)
            {
                if (byte.Parse(gridView1.GetFocusedRowCellValue(colMaTT).ToString()) == 6)
                    Duyet(7);
                else
                    DialogBox.Infomation("Chỉ được xác nhận thanh lý những hợp đồng góp vốn đang ở tình trạng <Chờ duyệt thanh lý>. Vui lòng kiểm tra lại, xin cảm ơn");
            }
        }

        private void btnKhonDuyetTL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaHDGV) != null)
            {
                if (byte.Parse(gridView1.GetFocusedRowCellValue(colMaTT).ToString()) == 6)
                    Duyet(6);
                else
                    DialogBox.Infomation("Chỉ được xác nhận thanh lý những hợp đồng góp vốn đang ở tình trạng <Chờ duyệt thanh lý>. Vui lòng kiểm tra lại, xin cảm ơn");
            }
        }

        private void btnInBienBanTLHD_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaHDGV) != null)
            {
                if (byte.Parse(gridView1.GetFocusedRowCellValue(colMaTT).ToString()) == 7)
                {
                    ThanhLyCls o = new ThanhLyCls();
                    o.Print(int.Parse(gridView1.GetFocusedRowCellValue(colMaHDGV).ToString()));
                }
                else
                    DialogBox.Infomation("Hợp đồng góp vốn số <" + gridView1.GetFocusedRowCellValue(colSoPhieu).ToString() + "> chưa thanh lý hoặc đang chờ duyệt thanh lý nên không thể in biên bản thanh lý. Vui lòng kiểm tra lại, xin cảm ơn");
            }
            else
                DialogBox.Infomation("Vui lòng chọn hợp đồng góp vốn muốn in biên bản thanh lý. Xin cảm ơn");
        }

        private void btnDuyetTL2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnDuyetTL_ItemClick(sender, e);
        }

        private void btnKhongDuyetTL2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnKhonDuyetTL_ItemClick(sender, e);
        }

        private void btnNhacNo2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NhacNo(2);
        }

        bool CheckHoaDon(byte DotTT)
        {
            it.HoaDonGTGTCls o = new it.HoaDonGTGTCls();
            o.MaHDGV = MaHDGV;
            o.DotTT = DotTT;
            return o.CheckHDGV();
        }

        private void btnXuatHoaDon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridLichThanhToan.GetFocusedRowCellValue(colDotTT) != null)
            {
                if (double.Parse(gridLichThanhToan.GetFocusedRowCellValue(colConLai).ToString()) == 0)
                {
                    //Kiem tra da xuat hoa don hay chua
                    if (CheckHoaDon(byte.Parse(gridLichThanhToan.GetFocusedRowCellValue(colDotTT).ToString())))
                        DialogBox.Infomation("Đã xuất hóa đơn GTGT đợt " + gridLichThanhToan.GetFocusedRowCellValue(colDotTT).ToString() + " cho khách hàng này rồi nên không thể xuất hóa đơn nữa. Vui lòng kiểm tra lại, xin cảm ơn.");
                    else
                    {
                        LandSoft.HDMB.HoaDon_frm frm = new LandSoft.HDMB.HoaDon_frm();
                        frm.DotTT = byte.Parse(gridLichThanhToan.GetFocusedRowCellValue(colDotTT).ToString());
                        frm.MaHDGV = int.Parse(gridView1.GetFocusedRowCellValue(colMaHDGV).ToString());
                        frm.MaSo = gridView1.GetFocusedRowCellValue(colMaSo).ToString();
                        frm.MaKH = int.Parse(gridView1.GetFocusedRowCellValue(colMaKH).ToString());
                        frm.ShowDialog();
                    }
                }
                else
                    DialogBox.Infomation("Khách hàng chưa thanh toán xong đợt " + gridLichThanhToan.GetFocusedRowCellValue(colDotTT).ToString() + " nên không thể xuất hóa đơn. Vui lòng kiểm tra lại, xin cảm ơn.");
            }
            else
                DialogBox.Infomation("Vui lòng chọn đợt thanh toán muốn xuất hóa đơn GTGT. Xin cảm ơn.");
        }

        private void btnTraTienThua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridLichThanhToan.GetFocusedRowCellValue(colMaPGC2) != null)
            {
                if (double.Parse(gridLichThanhToan.GetFocusedRowCellValue(colSoTien).ToString()) == 0)
                {
                    DialogBox.Infomation("Đợt thanh toán này chưa thu tiền khách hàng nên không thể trả lại tiền thừa. Vui lòng kiểm lại, xin cảm ơn.");
                    return;
                }
                if (CheckPaymentSurplus2())
                {
                    try
                    {
                        NghiepVu.HDGopVon.PhieuChi_frm frm = new LandSoft.NghiepVu.HDGopVon.PhieuChi_frm();
                        frm.MaKH = int.Parse(gridView1.GetFocusedRowCellValue(colMaKH).ToString());
                        frm.HoTenKH = gridView1.GetFocusedRowCellValue(colHoTenKH).ToString().ToString();
                        frm.DotTT = byte.Parse(gridLichThanhToan.GetFocusedRowCellValue(colDotTT).ToString());
                        frm.PayDeposit = true;
                        frm.SoTien = GetMoney();
                        frm.MaHDGV = MaHDGV;
                        frm.ShowDialog();
                        if (frm.IsUpdate)
                        {
                            DeletePaymentSurplus2();
                            LoadLichTT();
                        }
                    }
                    catch (Exception ex)
                    {
                        DialogBox.Infomation(ex.Message);
                    }
                }
                else
                {
                    if (double.Parse(gridLichThanhToan.GetFocusedRowCellValue(colConLai).ToString()) == 0)
                        DialogBox.Infomation("Đợt thanh toán này khách hàng trả đúng số tiền phải thu nên không có tiền thừa. Vui lòng kiểm lại, xin cảm ơn.");
                    else
                        DialogBox.Infomation("Khách hàng chưa thanh toán hết số tiền đợt này nên không thể trả lại tiền thừa. Vui lòng kiểm lại, xin cảm ơn.");
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn đợt thanh toán muốn trả lại tiền dư cho khách hàng. Xin cảm ơn.");
        }

        bool CheckPaymentSurplus2()
        {
            it.hdgvPhieuChiCls o = new it.hdgvPhieuChiCls();
            o.HDGV.MaHDGV = MaHDGV;

            return o.CheckPaymentSurplus(byte.Parse(gridLichThanhToan.GetFocusedRowCellValue(colDotTT).ToString()));
        }

        double GetMoney()
        {
            double temp = 0;
            for (int i = gridLichThanhToan.FocusedRowHandle + 1; i < gridLichThanhToan.RowCount; i++)
                temp += double.Parse(gridLichThanhToan.GetRowCellValue(i, colSoTien).ToString());

            return temp;
        }

        void DeletePaymentSurplus2()
        {
            it.hdgvPhieuThuCls o;
            for (int i = gridLichThanhToan.FocusedRowHandle + 1; i < gridLichThanhToan.RowCount; i++)
            {
                o = new it.hdgvPhieuThuCls();
                o.Lich.HDGV.MaHDGV = MaHDGV;
                o.Lich.DotTT = byte.Parse(gridLichThanhToan.GetRowCellValue(i, colDotTT).ToString());
                o.DeletePaymenSurplus();
            }
        }
    }
}
