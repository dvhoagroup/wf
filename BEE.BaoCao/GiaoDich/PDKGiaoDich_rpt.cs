using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace BEE.BaoCao.GiaoDich
{
    public partial class PDKGiaoDich_rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public PDKGiaoDich_rpt(int MaGD)
        {
            InitializeComponent();

            it.pdkGiaoDichCls o = new it.pdkGiaoDichCls(MaGD);
            o.KhachHang1.Detail();
            o.KhachHang2.Detail();
            lblCMND1.Text = o.KhachHang1.SoCMND;
            lblCMND2.Text = o.KhachHang2.SoCMND;
            lblDCLL1.Text = o.KhachHang1.DiaChi;
            lblDCLL2.Text = o.KhachHang2.DiaChi;
            lblDCTT1.Text = o.KhachHang1.ThuongTru;
            lblDCTT2.Text = o.KhachHang2.ThuongTru;
            lblDiDong11.Text = o.KhachHang1.DiDong;
            lblDiDong12.Text = o.KhachHang2.DiDong;
            lblDiDong21.Text = o.KhachHang1.DiDong2;
            lblDiDong22.Text = o.KhachHang2.DiDong2;
            lblDienGiai.Text = o.DienGiai;
            lblDienTichXD.Text = string.Format("{0:#,0.#} m2", o.DienTichXD);
            lblDonGia.Text = string.Format("{0:#,0.#} VNĐ", o.DonGia);
            lblDTCoQuan1.Text = o.KhachHang1.DTCoQuan;
            lblDTCoQuan2.Text = o.KhachHang2.DTCoQuan;
            lblDTNha1.Text = o.KhachHang1.DTCD;
            lblDTNha2.Text = o.KhachHang2.DTCD;
            lblDTPhongKhach.Text = string.Format("{0:#,0.#} m2", o.DTPhongKhach);
            lblDTSanSau.Text = string.Format("{0:#,0.#} m2", o.DTSanSau);
            lblDTSanTruoc.Text = string.Format("{0:#,0.#} m2", o.DTSanTruoc);
            lblDuongBen.Text = string.Format("{0:#,0.#} m", o.DuongBen);
            lblDuongSau.Text = string.Format("{0:#,0.#} m", o.DuongSau);
            lblDuongTruoc.Text = string.Format("{0:#,0.#} m", o.DuongTruoc);
            lblKichThuoc.Text = string.Format("{0:#,0.#} m2", o.Rong * o.Dai);
            lblKhachHang1.Text = o.KhachHang1.GetCustomerPay();
            lblKhachHang2.Text = o.KhachHang2.GetCustomerPay();
            lblMaGD.Text = o.SoPhieu;

            o.NhanVien1.Detail();
            o.NhanVien2.Detail();
            lblMaSo1.Text = o.NhanVien1.MaSo;
            lblMaSo2.Text = o.NhanVien2.MaSo;
            lblNhanVien1.Text = o.NhanVien1.HoTen;
            lblNhanVien2.Text = o.NhanVien2.HoTen;

            lblMoiQuanHe2.Text = o.MoiQuanHe;
            lblNam.Text = string.Format("{0:yyyy}", o.NgayKy);
            lblNoiCap1.Text = o.KhachHang1.NoiCap;
            lblNoiCap2.Text = o.KhachHang2.NoiCap;
            lblNgay.Text = string.Format("{0:dd}", o.NgayKy);
            if (o.KhachHang1.NgayCap.Year != 1)
                lblNgayCap1.Text = string.Format("{0:dd/MM/yyyy}", o.KhachHang1.NgayCap);
            if (o.KhachHang2.NgayCap.Year != 1)
                lblNgayCap2.Text = string.Format("{0:dd/MM/yyyy}", o.KhachHang2.NgayCap);
            lblNguoiDangKy.Text = string.Format("8. Tôi {0} cam kết những thông tin trên đây do chính chúng tôi cung cấp tự nguyện và đúng sự thật.", o.KhachHang1.GetCustomerPay());
            lblNguoiLienHe.Text = o.NguoiLienHe;
            
            //lblOtherComfortable.Text = "";
            //lblOtherInfo.Text = "";
            if (o.PhapLyKhac.Trim() != "")
                lblPhapLyKhac.Text = o.PhapLyKhac;
            
            //lblPhiMGKhac.Text = "";
            lblPhiMoiGioi.Text = string.Format("{0:#,0.#}", o.PhiMoiGioi);
            lblPhongKhach.Text = string.Format("{0:#,0.#} phòng", o.PhongKhach);
            lblPhongNgu.Text = string.Format("{0:#,0.#} phòng", o.PhongNgu);
            lblPhongWC.Text = string.Format("{0:#,0.#} phòng", o.PhongWC);
            lblSoTang.Text = string.Format("{0:#,0.#} tầng", o.SoTang);
            lblTiLe1.Text = string.Format("{0:#,0.#} %", o.TyLe1);
            lblTiLe2.Text = string.Format("{0:#,0.#} %", o.TyLe2);
            lblTongTien.Text = string.Format("{0:#,0.#} VNĐ", o.TongTien);
            lblThang.Text = string.Format("{0:MM}", o.NgayKy);
            lblViTriTang.Text = o.ViTriTang.ToString();

            //Thoi han
            SetDuration(o.ThoiHan);

            //Loai giao dich
            SetCategoryTransaction(o.LGD.MaLDG);

            //Loai BDS
            SetCategoryLand(o.LoaiBDS.MaLBDS);

            //Tien ich
            SetComfortable(MaGD);

            //Phap ly
            SetItemLaw(o.PhapLy.MaPL);
            
            //Phuong huong
            SetDirection(o.Huong.MaPhuongHuong);

            //VAT
            if (o.IsVAT)
                chkVAT.Checked = true;
            else
                chkNotVAT.Checked = true;

            //Ho so kem theo
            if (o.HoSo1.Trim() != "")
                lblHS1.Text = o.HoSo1;
            if (o.HoSo2.Trim() != "")
                lblHS2.Text = o.HoSo2;
            if (o.HoSo3.Trim() != "")
                lblHS3.Text = o.HoSo3;
            if (o.HoSo1.Trim() != "")
                lblHS4.Text = o.HoSo4;
            if (o.HoSo5.Trim() != "")
                lblHS5.Text = o.HoSo5;
            if (o.HoSo6.Trim() != "")
                lblHS6.Text = o.HoSo6;
            if (o.HoSo7.Trim() != "")
                lblHS7.Text = o.HoSo7;
            if (o.HoSo8.Trim() != "")
                lblHS8.Text = o.HoSo8;
            if (o.HoSo9.Trim() != "")
                lblHS9.Text = o.HoSo9;
            if (o.HoSo10.Trim() != "")
                lblHS10.Text = o.HoSo10;
        }

        void SetDuration(byte _val)
        {
            switch (_val)
            {
                case 1:
                    chk1Nam.Checked = true; break;
                case 2: 
                    chk6Thang.Checked = true; break;
                case 3: 
                    chk3Thang.Checked = true; break;
                case 4: 
                    chk1Thang.Checked = true; break;
            }
        }

        void SetCategoryTransaction(byte _val)
        {
            switch (_val)
            {
                case 1://Can mua
                    chkCanMua.Checked = true; break;
                case 2://Can ban
                    chkCanBan.Checked = true; break;
                case 3://Can thue
                    chkCanThue.Checked = true; break;
                case 4://Can cho thue
                    chkCanChoThue.Checked = true; break;
            }
        }

        void SetCategoryLand(short _val)
        {
            switch (_val)
            {
                case 1://Dat nen
                    chkDatNen.Checked = true; break;
                case 2://Biet thu
                    chkBietThu.Checked = true; break;
                case 3://Can ho
                    chkCanHo.Checked = true; break;
                case 4://Nha pho
                    chkNhaPho.Checked = true; break;
                case 5://Nha xuong
                    chkNhaXuong.Checked = true; break;
                case 6://Dat hoa mau
                    chkDatHoaMau.Checked = true; break;
                case 7://Dat trong cay lau nam
                    chkDatTrong.Checked = true; break;
                case 8://Dat o
                    chkDatO.Checked = true; break;
            }
        }

        void SetComfortable(int _MaGD)
        {
            it.pdkgd_TienIchCls o = new it.pdkgd_TienIchCls();
            o.MaPGD = _MaGD;
            DataTable tblCom = o.SelectBy();
            foreach (DataRow r in tblCom.Rows)
                SetItemComfortable(byte.Parse(r["MaTienIch"].ToString()));
        }

        void SetItemComfortable(byte _val)
        {
            switch (_val)
            {
                case 1://Truong hoc
                    chkTruongHoc.Checked = true; break;
                case 2://Sieu thi
                    chkSieuThi.Checked = true; break;
                case 3://Benh vien
                    chkBenhVien.Checked = true; break;
                case 4://Cong vien
                    chkCongVien.Checked = true; break;
                case 5://Trung tam TDTT
                    chkTrungTamTDTT.Checked = true; break;
                case 6://Cho
                    chkCho.Checked = true; break;
                case 7://Dien
                    chkDien.Checked = true; break;
                case 8://Nuoc
                    chkNuoc.Checked = true; break;
                case 9://Dien thoai
                    chkDienThoai.Checked = true; break;
                case 10://Cap TV
                    chkCapTV.Checked = true; break;
                case 11://Internet
                    chkInternet.Checked = true; break;
            }
        }

        void SetItemLaw(byte _val)
        {
            switch (_val)
            {
                case 1://So hong
                    chkSoHong.Checked = true; break;
                case 2://So do
                    chkSoDo.Checked = true; break;
                case 3://HDMB
                    chkHDMB.Checked = true; break;
                case 4://HDGV
                    chkHDGV.Checked = true; break;
                case 5://HDCN
                    chkHDCN.Checked = true; break;
                case 6://HDHM
                    chkHDHM.Checked = true; break;
                case 7://HDHB
                    chkHDHB.Checked = true; break;
                case 8://HDCV
                    chkHDCV.Checked = true; break;
                case 9://HDCT
                    chkHDCT.Checked = true; break;
                case 10://BBDC
                    chkBBDC.Checked = true; break;
                case 11://Giay to hop le
                    chkGiayToHopLe.Checked = true; break;
            }
        }

        void SetDirection(byte _val)
        {
            switch (_val)
            {
                case 1://Dong
                    chkHDong.Checked = true; break;
                case 2://Tay
                    chkHTay.Checked = true; break;
                case 3://Nam
                    chkHNam.Checked = true; break;
                case 4://Bac
                    chkHBac.Checked = true; break;
                case 5://Dong Nam
                    chkHDNam.Checked = true; break;
                case 6://Dong Bac
                    chkHDBac.Checked = true; break;
                case 7://Tay Nam
                    chkHTNam.Checked = true; break;
                case 8://Tay Bac
                    chkHTBac.Checked = true; break;
            }
        }

        public void ExportRtf(System.IO.Stream stream, int MaGD)
        {
            it.pdkGiaoDichCls o = new it.pdkGiaoDichCls(MaGD);
            o.KhachHang1.Detail();
            o.KhachHang2.Detail();
            lblCMND1.Text = o.KhachHang1.SoCMND;
            lblCMND2.Text = o.KhachHang2.SoCMND;
            lblDCLL1.Text = o.KhachHang1.DiaChi;
            lblDCLL2.Text = o.KhachHang2.DiaChi;
            lblDCTT1.Text = o.KhachHang1.ThuongTru;
            lblDCTT2.Text = o.KhachHang2.ThuongTru;
            lblDiDong11.Text = o.KhachHang1.DiDong;
            lblDiDong12.Text = o.KhachHang2.DiDong;
            lblDiDong21.Text = o.KhachHang1.DiDong2;
            lblDiDong22.Text = o.KhachHang2.DiDong2;
            lblDienGiai.Text = o.DienGiai;
            lblDienTichXD.Text = string.Format("{0:#,0.#} m2", o.DienTichXD);
            lblDonGia.Text = string.Format("{0:#,0.#} VNĐ", o.DonGia);
            lblDTCoQuan1.Text = o.KhachHang1.DTCoQuan;
            lblDTCoQuan2.Text = o.KhachHang2.DTCoQuan;
            lblDTNha1.Text = o.KhachHang1.DTCD;
            lblDTNha2.Text = o.KhachHang2.DTCD;
            lblDTPhongKhach.Text = string.Format("{0:#,0.#} m2", o.DTPhongKhach);
            lblDTSanSau.Text = string.Format("{0:#,0.#} m2", o.DTSanSau);
            lblDTSanTruoc.Text = string.Format("{0:#,0.#} m2", o.DTSanTruoc);
            lblDuongBen.Text = string.Format("{0:#,0.#} m", o.DuongBen);
            lblDuongSau.Text = string.Format("{0:#,0.#} m", o.DuongSau);
            lblDuongTruoc.Text = string.Format("{0:#,0.#} m", o.DuongTruoc);
            lblKichThuoc.Text = string.Format("{0:#,0.#} m2", o.Rong * o.Dai);
            lblKhachHang1.Text = o.KhachHang1.GetCustomerPay();
            lblKhachHang2.Text = o.KhachHang2.GetCustomerPay();
            lblMaGD.Text = o.SoPhieu;

            o.NhanVien1.Detail();
            o.NhanVien2.Detail();
            lblMaSo1.Text = o.NhanVien1.MaSo;
            lblMaSo2.Text = o.NhanVien2.MaSo;
            lblNhanVien1.Text = o.NhanVien1.HoTen;
            lblNhanVien2.Text = o.NhanVien2.HoTen;

            lblMoiQuanHe2.Text = o.MoiQuanHe;
            lblNam.Text = string.Format("{0:yyyy}", o.NgayKy);
            lblNoiCap1.Text = o.KhachHang1.NoiCap;
            lblNoiCap2.Text = o.KhachHang2.NoiCap;
            lblNgay.Text = string.Format("{0:dd}", o.NgayKy);
            if (o.KhachHang1.NgayCap.Year != 1)
                lblNgayCap1.Text = string.Format("{0:dd/MM/yyyy}", o.KhachHang1.NgayCap);
            if (o.KhachHang2.NgayCap.Year != 1)
                lblNgayCap2.Text = string.Format("{0:dd/MM/yyyy}", o.KhachHang2.NgayCap);
            lblNguoiDangKy.Text = string.Format("8. Tôi {0} cam kết những thông tin trên đây do chính chúng tôi cung cấp tự nguyện và đúng sự thật.", o.KhachHang1.GetCustomerPay());
            lblNguoiLienHe.Text = o.NguoiLienHe;

            //lblOtherComfortable.Text = "";
            //lblOtherInfo.Text = "";
            if (o.PhapLyKhac.Trim() != "")
                lblPhapLyKhac.Text = o.PhapLyKhac;

            //lblPhiMGKhac.Text = "";
            lblPhiMoiGioi.Text = string.Format("{0:#,0.#}", o.PhiMoiGioi);
            lblPhongKhach.Text = string.Format("{0:#,0.#} phòng", o.PhongKhach);
            lblPhongNgu.Text = string.Format("{0:#,0.#} phòng", o.PhongNgu);
            lblPhongWC.Text = string.Format("{0:#,0.#} phòng", o.PhongWC);
            lblSoTang.Text = string.Format("{0:#,0.#} tầng", o.SoTang);
            lblTiLe1.Text = string.Format("{0:#,0.#} %", o.TyLe1);
            lblTiLe2.Text = string.Format("{0:#,0.#} %", o.TyLe2);
            lblTongTien.Text = string.Format("{0:#,0.#} VNĐ", o.TongTien);
            lblThang.Text = string.Format("{0:MM}", o.NgayKy);
            lblViTriTang.Text = o.ViTriTang.ToString();

            //Thoi han
            SetDuration(o.ThoiHan);

            //Loai giao dich
            SetCategoryTransaction(o.LGD.MaLDG);

            //Loai BDS
            SetCategoryLand(o.LoaiBDS.MaLBDS);

            //Tien ich
            SetComfortable(MaGD);

            //Phap ly
            SetItemLaw(o.PhapLy.MaPL);

            //Phuong huong
            SetDirection(o.Huong.MaPhuongHuong);

            //VAT
            if (o.IsVAT)
                chkVAT.Checked = true;
            else
                chkNotVAT.Checked = true;

            //Ho so kem theo
            if (o.HoSo1.Trim() != "")
                lblHS1.Text = o.HoSo1;
            if (o.HoSo2.Trim() != "")
                lblHS2.Text = o.HoSo2;
            if (o.HoSo3.Trim() != "")
                lblHS3.Text = o.HoSo3;
            if (o.HoSo1.Trim() != "")
                lblHS4.Text = o.HoSo4;
            if (o.HoSo5.Trim() != "")
                lblHS5.Text = o.HoSo5;
            if (o.HoSo6.Trim() != "")
                lblHS6.Text = o.HoSo6;
            if (o.HoSo7.Trim() != "")
                lblHS7.Text = o.HoSo7;
            if (o.HoSo8.Trim() != "")
                lblHS8.Text = o.HoSo8;
            if (o.HoSo9.Trim() != "")
                lblHS9.Text = o.HoSo9;
            if (o.HoSo10.Trim() != "")
                lblHS10.Text = o.HoSo10;

            this.ExportToRtf(stream);
        }
    }
}
