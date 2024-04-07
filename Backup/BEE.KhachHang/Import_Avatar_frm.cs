﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;

namespace BEE.KhachHang
{
    public partial class Import_Avatar_frm : DevExpress.XtraEditors.XtraForm
    {
        public Import_Avatar_frm()
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
                    tblKH = cmd.ExcelSelect("NguoiDaiDien$").Tables[0];
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

        private void Import_Avatar_frm_Load(object sender, EventArgs e)
        {
            OpenFile();
        }

        int CheckKH(int i)
        {
            it.KhachHangCls o = new it.KhachHangCls();
            o.HoKH = gridView1.GetRowCellValue(i, colHoTenKhachHang).ToString().Trim();
            o.SoCMND = gridView1.GetRowCellValue(i, colSoCMNDKH).ToString().Trim();
            o.MaKH = o.GetByHoTenPersonal();

            return o.MaKH;            
        }

        private void btnThucHien_Click(object sender, EventArgs e)
        {
            it.NguoiDaiDienCls o;
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                o = new it.NguoiDaiDienCls();
                //Kiem tra Khach hang
                if (CheckKH(i) == 0)
                {
                    DialogBox.Infomation("Khách hàng <" + gridView1.GetRowCellValue(i, colHoTenKhachHang).ToString().Trim() + "> chưa có trong hệ thống. Vui lòng kiểm tra lại, xin cảm ơn");
                    gridView1.FocusedRowHandle = i;
                    return;
                }
            }

            for (int i = 0; i < gridView1.RowCount; i++)
            {
                o = new it.NguoiDaiDienCls();
                o.MaKH = CheckKH(i);
                o.MaSoThue = gridView1.GetRowCellValue(i, colMaSoTTNCN).ToString().Trim();
                o.DiaChiLL = gridView1.GetRowCellValue(i, colDiaChiLienLac).ToString().Trim();
                o.DiaChiTT = gridView1.GetRowCellValue(i, colDiaChiThuongTru).ToString().Trim();
                o.DTCD = gridView1.GetRowCellValue(i, colDTCD).ToString().Trim();
                o.DTDD = gridView1.GetRowCellValue(i, colDiDong).ToString().Trim();
                o.Email = "";
                o.HoTen = gridView1.GetRowCellValue(i, colHoTenNguoiDaiDien).ToString().Trim();
                o.NoiCap = gridView1.GetRowCellValue(i, colNoiCap).ToString().Trim();
                o.NoiSinh = gridView1.GetRowCellValue(i, colNoiSinh).ToString().Trim();
                o.NgayCap = DateTime.Parse(gridView1.GetRowCellValue(i, colNgayCap).ToString().Trim());
                o.NgaySinh = DateTime.Parse(gridView1.GetRowCellValue(i, colNgaySinh).ToString().Trim());
                o.SoCMND = gridView1.GetRowCellValue(i, colSoCMNDNDD).ToString().Trim();
                o.Xa.MaXa = 0;
                o.Xa2.MaXa = 0;
                o.Insert();
            }
        }
    }
}