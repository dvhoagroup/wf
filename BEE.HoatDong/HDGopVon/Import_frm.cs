using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace LandSoft.NghiepVu.HDGopVon
{
    public partial class Import_frm : DevExpress.XtraEditors.XtraForm
    {
        int MaHDGV = 0;
        public bool IsUpdate = false;
        public Import_frm()
        {
            InitializeComponent();
        }

        private void Import_frm_Shown(object sender, EventArgs e)
        {
            OpenFile();
        }

        void OpenFile()
        {
            DataTable tblKH;
            DataTable tblKH2;
        doo:
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Mở file Excel";
            ofd.Filter = "File excel(.xls, .xlsx)|*.xls;*.xlsx";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    dip.cmdExcel cmd = new dip.cmdExcel(ofd.FileName);
                    tblKH = cmd.ExcelSelect("CaNhan$").Tables[0];
                    tblKH2 = cmd.ExcelSelect("DoanhNghiep$").Tables[0];
                    //cmd.getVersion();
                }
                catch
                {
                    DialogBox.Infomation("Xin vui lòng kiểm tra lại file Excel không đúng mẫu. Xin cảm ơn!");
                    goto doo;
                }

                try
                {
                    gridControl1.DataSource = tblKH;
                    gridControl2.DataSource = tblKH2;
                }
                catch
                {
                    DialogBox.Infomation("Xin vui lòng kiểm tra lại file Excel không đúng mẫu. Xin cảm ơn!");
                    goto doo;
                }
                gridView1.FocusedRowHandle = 0;
                gridView2.FocusedRowHandle = 0;
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMoFile_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void btnXoaDong_Click(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
                gridView1.DeleteSelectedRows();
            else
                gridView2.DeleteSelectedRows();
        }

        private void btnThucHien_Click(object sender, EventArgs e)
        {
            it.hdGopVonCls o;
            #region Khach hang la Ca nhan
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                o = new it.hdGopVonCls();
                //Kiem tra BDS
                o.BDS.MaSo = gridView1.GetRowCellValue(i, colMaCanHo).ToString().Trim();
                o.BDS.MaBDS = o.BDS.GetByMaSo();
                if (o.BDS.MaBDS.Trim() == "")
                {
                    DialogBox.Infomation("Bất động sản có mã số <" + o.BDS.MaSo + "> chưa có trong hệ thống. Vui lòng kiểm tra lại, xin cảm ơn");
                    gridView1.FocusedRowHandle = i;
                    return;
                }

                //Kiem tra Khach hang
                o.KhachHang.HoKH = gridView1.GetRowCellValue(i, colKhachHang).ToString().Trim();
                o.KhachHang.SoCMND = gridView1.GetRowCellValue(i, colSoCMND).ToString().Trim();
                o.KhachHang.MaKH = o.KhachHang.GetByHoTenPersonal();
                if (o.KhachHang.MaKH == 0)
                {
                    DialogBox.Infomation("Khách hàng <" + o.KhachHang.HoKH + "> chưa có trong hệ thống. Vui lòng kiểm tra lại, xin cảm ơn");
                    gridView1.FocusedRowHandle = i;
                    return;
                }

                //Kiem tra BDS trong HDGV
                if (o.CheckBDS())
                {
                    DialogBox.Infomation("Bất động sản có mã số <" + o.BDS.MaSo + "> đã lập hợp đồng góp vốn số <" + gridView1.GetRowCellValue(i, colSoHDGV).ToString().Trim() + ">. Vui lòng kiểm tra lại, xin cảm ơn");
                    gridView1.FocusedRowHandle = i;
                    return;
                }
                
                //Kiem tra HDGV, SoPhieu, MaKH, MaBDS
                o.SoPhieu = gridView1.GetRowCellValue(i, colSoHDGV).ToString().Trim();
                if (o.Check())
                {
                    DialogBox.Infomation("Hợp đồng góp vốn số <" + o.SoPhieu + "> của khách hàng <" + o.KhachHang.HoKH + "> cho BĐS <" + o.BDS.MaSo + "> đã tồn tại trong hệ thống. Vui lòng kiểm tra lại, xin cảm ơn");
                    gridView1.FocusedRowHandle = i;
                    return;
                }
            }
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                MaHDGV = 0;
                o = new it.hdGopVonCls();
                o.BDS.MaSo = gridView1.GetRowCellValue(i, colMaCanHo).ToString().Trim();
                o.BDS.MaBDS = o.BDS.GetByMaSo();
                o.GiaTriHD = double.Parse(gridView1.GetRowCellValue(i, colGiaTriHD).ToString());
                o.KhachHang.HoKH = gridView1.GetRowCellValue(i, colKhachHang).ToString().Trim();
                o.KhachHang.SoCMND = gridView1.GetRowCellValue(i, colSoCMND).ToString().Trim();
                o.KhachHang.MaKH = o.KhachHang.GetByHoTenPersonal();
                o.LoaiTien.MaLoaiTien = 1;
                o.NgayKy = DateTime.Parse(gridView1.GetRowCellValue(i, colNgayKy).ToString());
                o.NhanVien.MaNV = LandSoft.Library.Common.StaffID;
                o.SoPhieu = gridView1.GetRowCellValue(i, colSoHDGV).ToString().Trim();
                o.TinhTrang.MaTT = gridView1.GetRowCellValue(i, colTinhTrang).ToString() == "Đa thanh lý" ? (byte)7 : (byte)2;
                o.DonGia = double.Parse(gridView1.GetRowCellValue(i, colDonGia).ToString());
                o.DTSD = double.Parse(gridView1.GetRowCellValue(i, colDienTich).ToString());
                try
                {
                    o.LaiSuat = double.Parse(gridView2.GetRowCellValue(i, colLaiSuat1).ToString());
                }
                catch { o.LaiSuat = 0; }
                try
                {
                    o.LoiNhuan = double.Parse(gridView2.GetRowCellValue(i, colLoiNhuan1).ToString());
                }
                catch { o.LoiNhuan = 0; }
                MaHDGV = o.Insert();

                //Them lich thanh toan
                InsertCalenderPay(i);
            }
            #endregion

            #region Khach hang la Doanh nghiep
            for (int i = 0; i < gridView2.RowCount; i++)
            {
                o = new it.hdGopVonCls();
                //Kiem tra BDS
                o.BDS.MaSo = gridView2.GetRowCellValue(i, colMaCanHo1).ToString().Trim();
                o.BDS.MaBDS = o.BDS.GetByMaSo();
                if (o.BDS.MaBDS.Trim() == "")
                {
                    DialogBox.Infomation("Bất động sản có mã số <" + o.BDS.MaSo + "> chưa có trong hệ thống. Vui lòng kiểm tra lại, xin cảm ơn");
                    gridView2.FocusedRowHandle = i;
                    return;
                }

                //Kiem tra Khach hang
                o.KhachHang.TenCongTy = gridView2.GetRowCellValue(i, colKhachHang1).ToString().Trim();
                o.KhachHang.MaKH = o.KhachHang.GetByTenCongTy();
                if (o.KhachHang.MaKH == 0)
                {
                    DialogBox.Infomation("Khách hàng <" + o.KhachHang.TenCongTy + "> chưa có trong hệ thống. Vui lòng kiểm tra lại, xin cảm ơn");
                    gridView2.FocusedRowHandle = i;
                    return;
                }

                //Kiem tra BDS trong HDGV
                if (o.CheckBDS())
                {
                    DialogBox.Infomation("Bất động sản có mã số <" + o.BDS.MaSo + "> đã lập hợp đồng góp vốn số <" + gridView2.GetRowCellValue(i, colSoHDGV1).ToString().Trim() + ">. Vui lòng kiểm tra lại, xin cảm ơn");
                    gridView2.FocusedRowHandle = i;
                    return;
                }

                //Kiem tra HDGV, SoPhieu, MaKH, MaBDS
                o.SoPhieu = gridView2.GetRowCellValue(i, colSoHDGV1).ToString().Trim();
                if (o.Check())
                {
                    DialogBox.Infomation("Hợp đồng góp vốn số <" + o.SoPhieu + "> của khách hàng <" + o.KhachHang.HoKH + "> cho BĐS <" + o.BDS.MaSo + "> đã tồn tại trong hệ thống. Vui lòng kiểm tra lại, xin cảm ơn.");
                    gridView2.FocusedRowHandle = i;
                    return;
                }
            }
            for (int i = 0; i < gridView2.RowCount; i++)
            {
                o = new it.hdGopVonCls();
                o.BDS.MaSo = gridView2.GetRowCellValue(i, colMaCanHo1).ToString().Trim();
                o.BDS.MaBDS = o.BDS.GetByMaSo();
                o.GiaTriHD = double.Parse(gridView2.GetRowCellValue(i, colGiaTriHD1).ToString());
                o.KhachHang.TenCongTy = gridView2.GetRowCellValue(i, colKhachHang1).ToString().Trim();
                o.KhachHang.MaKH = o.KhachHang.GetByTenCongTy();
                o.LoaiTien.MaLoaiTien = 1;
                o.NgayKy = DateTime.Parse(gridView2.GetRowCellValue(i, colNgayKy1).ToString());
                o.NhanVien.MaNV = LandSoft.Library.Common.StaffID;
                o.SoPhieu = gridView2.GetRowCellValue(i, colSoHDGV1).ToString().Trim();
                o.TinhTrang.MaTT = gridView2.GetRowCellValue(i, colTinhTrang1).ToString() == "Đa thanh lý" ? (byte)7 : (byte)2;
                o.DonGia = double.Parse(gridView2.GetRowCellValue(i, colDonGia1).ToString());
                o.DTSD = double.Parse(gridView2.GetRowCellValue(i, colDienTich1).ToString());
                try
                {
                    o.LaiSuat = double.Parse(gridView2.GetRowCellValue(i, colLaiSuat1).ToString());
                }
                catch { o.LaiSuat = 0; }
                try
                {
                    o.LoiNhuan = double.Parse(gridView2.GetRowCellValue(i, colLoiNhuan1).ToString());
                }
                catch { o.LoiNhuan = 0; }
                MaHDGV = o.Insert();

                //Them lich thanh toan
                InsertCalenderPay1(i);
            }

            IsUpdate = true;
            DialogBox.Infomation("Dữ liệu đã được cập nhật.");
            this.Close();
            #endregion
        }

        void InsertCalenderPay(int row)
        {
            it.hdgvLichThanhToanCls o = new it.hdgvLichThanhToanCls();
            o.HDGV.MaHDGV = MaHDGV;
            for (int i = 0; i < 8; i++)
            {
                o.DotTT = (byte)(i + 1);
                o.DienGiai = GetDescription(o.DotTT);
                o.IsPay = false;
                o.TyLeTT = GetRate(o.DotTT);

                o.TuongUng = GetTienThu(i + 1, row);
                o.ThueVAT = o.TuongUng / 10;                
                o.SoTien = o.TuongUng + o.ThueVAT;
                o.NgayTT = GetNgayThu(i + 1, row);
                o.Insert();
            }
        }

        void InsertCalenderPay1(int row)
        {
            it.hdgvLichThanhToanCls o = new it.hdgvLichThanhToanCls();
            o.HDGV.MaHDGV = MaHDGV;
            for (int i = 0; i < 8; i++)
            {
                o.DotTT = (byte)(i + 1);
                o.DienGiai = GetDescription(o.DotTT);
                o.IsPay = false;
                o.TyLeTT = GetRate(o.DotTT);
                o.TuongUng = GetTienThu1(i + 1, row);
                o.ThueVAT = o.TuongUng / 10;
                o.SoTien = o.TuongUng + o.ThueVAT;
                o.NgayTT = GetNgayThu1(i + 1, row);
                o.Insert();
            }
        }

        double GetTienThu(int i, int row)
        {
            double temp = 0;
            switch (i)
            {
                case 1:
                    temp = double.Parse(gridView1.GetRowCellValue(row, colDot1).ToString());
                    break;
                case 2:
                    temp = double.Parse(gridView1.GetRowCellValue(row, colDot2).ToString());
                    break;
                case 3:
                    temp = double.Parse(gridView1.GetRowCellValue(row, colDot3).ToString());
                    break;
                case 4:
                    temp = double.Parse(gridView1.GetRowCellValue(row, colDot4).ToString());
                    break;
                case 5:
                    temp = double.Parse(gridView1.GetRowCellValue(row, colDot5).ToString());
                    break;
                case 6:
                    temp = double.Parse(gridView1.GetRowCellValue(row, colDot6).ToString());
                    break;
                case 7:
                    temp = double.Parse(gridView1.GetRowCellValue(row, colDot7).ToString());
                    break;
                case 8:
                    temp = double.Parse(gridView1.GetRowCellValue(row, colDot8).ToString());
                    break;
            }
            return temp;
        }

        double GetTienThu1(int i, int row)
        {
            double temp = 0;
            switch (i)
            {
                case 1:
                    temp = double.Parse(gridView2.GetRowCellValue(row, colDot11).ToString());
                    break;
                case 2:
                    temp = double.Parse(gridView2.GetRowCellValue(row, colDot21).ToString());
                    break;
                case 3:
                    temp = double.Parse(gridView2.GetRowCellValue(row, colDot31).ToString());
                    break;
                case 4:
                    temp = double.Parse(gridView2.GetRowCellValue(row, colDot41).ToString());
                    break;
                case 5:
                    temp = double.Parse(gridView2.GetRowCellValue(row, colDot51).ToString());
                    break;
                case 6:
                    temp = double.Parse(gridView2.GetRowCellValue(row, colDot61).ToString());
                    break;
                case 7:
                    temp = double.Parse(gridView2.GetRowCellValue(row, colDot71).ToString());
                    break;
                case 8:
                    temp = double.Parse(gridView2.GetRowCellValue(row, colDot81).ToString());
                    break;
            }
            return temp;
        }

        DateTime GetNgayThu(int i, int row)
        {
            DateTime date = DateTime.Now;
            switch (i)
            {
                case 1:
                    date = DateTime.Parse(gridView1.GetRowCellValue(row, colThuDot1).ToString());
                    break;
                case 2:
                    date = DateTime.Parse(gridView1.GetRowCellValue(row, colThuDot2).ToString());
                    break;
                case 3:
                    date = DateTime.Parse(gridView1.GetRowCellValue(row, colThuDot3).ToString());
                    break;
                case 4:
                    date = DateTime.Parse(gridView1.GetRowCellValue(row, colThuDot4).ToString());
                    break;
                case 5:
                    date = DateTime.Parse(gridView1.GetRowCellValue(row, colThuDot5).ToString());
                    break;
                case 6:
                    date = DateTime.Parse(gridView1.GetRowCellValue(row, colThuDot6).ToString());
                    break;
                case 7:
                    date = DateTime.Parse(gridView1.GetRowCellValue(row, colThuDot7).ToString());
                    break;
                case 8:
                    date = DateTime.Parse(gridView1.GetRowCellValue(row, colThuDot8).ToString());
                    break;
            }
            return date;
        }

        DateTime GetNgayThu1(int i, int row)
        {
            DateTime date = DateTime.Now;
            switch (i)
            {
                case 1:
                    date = DateTime.Parse(gridView2.GetRowCellValue(row, colThuDot11).ToString());
                    break;
                case 2:
                    date = DateTime.Parse(gridView2.GetRowCellValue(row, colThuDot21).ToString());
                    break;
                case 3:
                    date = DateTime.Parse(gridView2.GetRowCellValue(row, colThuDot31).ToString());
                    break;
                case 4:
                    date = DateTime.Parse(gridView2.GetRowCellValue(row, colThuDot41).ToString());
                    break;
                case 5:
                    date = DateTime.Parse(gridView2.GetRowCellValue(row, colThuDot51).ToString());
                    break;
                case 6:
                    date = DateTime.Parse(gridView2.GetRowCellValue(row, colThuDot61).ToString());
                    break;
                case 7:
                    date = DateTime.Parse(gridView2.GetRowCellValue(row, colThuDot71).ToString());
                    break;
                case 8:
                    date = DateTime.Parse(gridView2.GetRowCellValue(row, colThuDot81).ToString());
                    break;
            }
            return date;
        }

        byte GetRate(int i)
        {
            byte rate = 0;
            switch (i)
            {
                case 1:
                    rate = 15;
                    break;
                case 2:
                    rate = 15;
                    break;
                case 3:
                    rate = 10;
                    break;
                case 4:
                    rate = 10;
                    break;
                case 5:
                    rate = 10;
                    break;
                case 6:
                    rate = 10;
                    break;
                case 7:
                    rate = 25;
                    break;
                case 8:
                    rate = 5;
                    break;
            }
            return rate;
        }

        string GetDescription(int i)
        {
            string temp = "";
            switch (i)
            {
                case 1:
                    temp = "Ngay sau khi ký hợp đồng này, Bên B sẽ góp cho Bên A đợt 1 tương đương 15% ( mười lăm phần trăm) giá trị Hợp đồng.";
                    break;
                case 2:
                    temp = "Bên B sẽ góp cho Bên A đợt 2 tương đương 15% ( mười lăm phần trăm) giá trị hợp đồng kể từ ngày Ban quản lý dự án có văn bản thông báo thi công hoàn tất phần móng.";
                    break;
                case 3:
                    temp = "Bên B sẽ góp cho Bên A đợt 3 tương đương 10% (mười phần trăm) giá trị hợp đồng kể từ ngày Ban quản lý dự án có văn bản thông báo thi công hoàn tất phần tầng trệt.";
                    break;
                case 4:
                    temp = "Bên B sẽ chuyển góp vốn Đợt 4 cho Bên A tương đương 10% (mười phần trăm) giá trị hợp đồng kể từ ngày Ban quản lý dự án có văn bản thông báo thi công hoàn tất phần thô (thân) đến tầng thứ 10.";
                    break;
                case 5:
                    temp = "Bên B sẽ chuyển góp vốn Đợt 5 cho Bên A tương đương 10% (mười phần trăm) giá trị hợp đồng kể từ ngày Ban quản lý dự án có có văn bản thông báo thi công hoàn tất phần thô (thân) đến tầng thứ 20.";
                    break;
                case 6:
                    temp = "Bên B sẽ chuyển góp vốn Đợt 6 cho Bên A tương đương 10% (mười phần trăm) giá trị hợp đồng kể từ ngày Ban quản lý dự án có có văn bản thông báo thi công hoàn tất phần thô (thân) đến tầng thứ 30.";
                    break;
                case 7:
                    temp = "Trong vòng 05 (năm) ngày kể từ ngày Bên A bàn giao thực tến diện tích và căn hộ như nêu tại khoản 6.2, điều 6 của hợp đồng này, thì Bên B sẽ chuyển tiền góp vốn Đợt 7 cho Bên A tương đương 25% (hai lăm phần trăm) giá trị hợp đồng.";
                    break;
                case 8:
                    temp = "Trong vòng 10 (mười) ngày làm việc kể từ ngày các Bên A bàn giao căn Giấy chứng nhận quyền sở hữu nhà, quyền sử dụng đất ở (và các tài liệu kèm theo) cho Bên B hoặc người do Bên B chỉ định, Bên B sẽ góp cho Bên A tương đương 5% (năm phần trăm) giá trị hợp đồng.";
                    break;
            }

            return temp;
        }

        private void Import_frm_Load(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 1;
            xtraTabControl1.SelectedTabPageIndex = 0;
        }
    }
}