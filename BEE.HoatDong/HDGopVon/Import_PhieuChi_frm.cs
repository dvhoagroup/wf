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
    public partial class Import_PhieuChi_frm : DevExpress.XtraEditors.XtraForm
    {
        public Import_PhieuChi_frm()
        {
            InitializeComponent();
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXoaDong_Click(object sender, EventArgs e)
        {
            gridView1.DeleteSelectedRows();
        }

        void OpenFile()
        {
            DataTable tblKH;
        doo:
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Mở file Excel";
            ofd.Filter = "File excel(.xls, .xlsx)|*.xls;*.xlsx";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    dip.cmdExcel cmd = new dip.cmdExcel(ofd.FileName);
                    tblKH = cmd.ExcelSelect("Sheet1$").Tables[0];
                }
                catch
                {
                    DialogBox.Infomation("Xin vui lòng kiểm tra lại file Excel không đúng mẫu. Xin cảm ơn!");
                    goto doo;
                }

                try
                {
                    gridControl1.DataSource = tblKH;
                }
                catch
                {
                    DialogBox.Infomation("Xin vui lòng kiểm tra lại file Excel không đúng mẫu. Xin cảm ơn!");
                    goto doo;
                }
                gridView1.FocusedRowHandle = 0;
            }
        }

        private void btnMoFile_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void Import_PhieuChi_frm_Load(object sender, EventArgs e)
        {
            OpenFile();
        }

        int CheckKH(int i)
        {
            it.KhachHangCls o = new it.KhachHangCls();
            o.HoKH = gridView1.GetRowCellValue(i, colNguoiNhan).ToString().Trim();
            o.SoCMND = gridView1.GetRowCellValue(i, colSoCMND).ToString().Trim();
            o.MaKH = o.GetByHoTenPersonal();

            return o.MaKH;
        }

        int CheckHDGV(int i)
        {
            it.hdGopVonCls o = new it.hdGopVonCls();
            o.SoPhieu = gridView1.GetRowCellValue(i, colSoHDGV).ToString();
            o.BDS.MaSo = gridView1.GetRowCellValue(i, colMaCanHo).ToString();

            return o.CheckHDGV();
        }

        int CheckNhanVien(int i)
        {
            it.NhanVienCls o = new it.NhanVienCls();
            o.HoTen = gridView1.GetRowCellValue(i, colNhanVien).ToString();
            o.SoCMND = gridView1.GetRowCellValue(i, colSoCMNDNhanVien).ToString();

            return o.Check();
        }

        private void btnThucHien_Click(object sender, EventArgs e)
        {
            it.hdgvPhieuChiCls o;
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                o = new it.hdgvPhieuChiCls();
                o.HDGV.MaHDGV = CheckHDGV(i);
                if (o.HDGV.MaHDGV == 0)
                {
                    DialogBox.Infomation("Hợp đồng góp vốn này không có trong hệ thống. Vui lòng kiểm tra lại, xin cảm ơn.");
                    gridView1.FocusedRowHandle = i;
                    return;
                }
                o.KhachHang.MaKH = CheckKH(i);
                if (o.KhachHang.MaKH == 0)
                {
                    DialogBox.Infomation("Khách hàng <" + gridView1.GetRowCellValue(i, colNguoiNhan).ToString().Trim() + "> chưa có trong hệ thống. Vui lòng kiểm tra lại, xin cảm ơn");
                    gridView1.FocusedRowHandle = i;
                    return;
                }
                o.NhanVien.MaNV = CheckNhanVien(i);
                if (o.NhanVien.MaNV == 0)
                {
                    DialogBox.Infomation("Nhân viên <" + gridView1.GetRowCellValue(i, colNhanVien).ToString().Trim() + "> chưa có trong hệ thống. Vui lòng kiểm tra lại, xin cảm ơn");
                    gridView1.FocusedRowHandle = i;
                    return;
                }
            }

            for (int i = 0; i < gridView1.RowCount; i++)
            {
                o = new it.hdgvPhieuChiCls();
                o.ChungTuGoc = gridView1.GetRowCellValue(i, colChungTuGoc).ToString();
                o.DiaChi = gridView1.GetRowCellValue(i, colDiaChi).ToString();
                o.DienGiai = gridView1.GetRowCellValue(i, colDienGiai).ToString();
                o.HDGV.MaHDGV = CheckHDGV(i);
                o.KhachHang.MaKH = CheckKH(i);
                o.LoaiTien.MaLoaiTien = 1;
                o.NgayChi = DateTime.Parse(gridView1.GetRowCellValue(i, colNgayChi).ToString());
                o.NguoiNhan = gridView1.GetRowCellValue(i, colNguoiNhan).ToString();
                o.SoPhieu = gridView1.GetRowCellValue(i, colSoPhieuChi).ToString();
                o.SoTien = double.Parse(gridView1.GetRowCellValue(i, colSoTien).ToString());
                o.TKCo.MaTK = gridView1.GetRowCellValue(i, colTKCo).ToString();
                o.TKNo.MaTK = gridView1.GetRowCellValue(i, colTKNo).ToString();
                o.TyGia = 1;
                o.NhanVien.MaNV = CheckNhanVien(i);
                o.Insert();
            }
        }
    }
}