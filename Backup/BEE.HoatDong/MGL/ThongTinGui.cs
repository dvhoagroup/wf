using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using BEE.ThuVien;
using BEEREMA;

namespace BEE.HoatDong.MGL
{
    public partial class ThongTinGui : DevExpress.XtraEditors.XtraForm
    {

        MasterDataContext db = new MasterDataContext();
        public int? ID { get; set; }
        mglThongTinGui objTT;
        public ThongTinGui()
        {
            InitializeComponent();
        }

        private void ThongTinGui_Load(object sender, EventArgs e)
        {
            if (this.ID != null)
            {
                objTT = db.mglThongTinGuis.SingleOrDefault(p => p.ID == this.ID);
                chkSoNha.EditValue = objTT.SoNha;
                chkTenDuong.EditValue = objTT.TenDuong;
                chkPhuongXa.EditValue = objTT.PhuongXa;
                chkQuanHuyen.EditValue = objTT.QuanHuyen;
                chkTinhThanhPho.EditValue = objTT.TinhThanhPho;
                chkMatTien.EditValue = objTT.MatTien;
                chkDienTich.EditValue = objTT.DienTich;
                chkSoTang.EditValue = objTT.SoTang;
                chkTongGia.EditValue = objTT.TongGia;
                chkMoTa.EditValue = objTT.MoTa;
                chkGhiChu.EditValue = objTT.GhiChu;
                chkAnh.EditValue = objTT.LinkAnh;
                chkViTri.EditValue = objTT.LinkViTri;
            }
            else
            {
                chkSoNha.EditValue = true;
                chkTenDuong.EditValue = true;
                chkPhuongXa.EditValue = true;
                chkQuanHuyen.EditValue = true;
                chkTinhThanhPho.EditValue = true;
                chkMatTien.EditValue = true;
                chkDienTich.EditValue = true;
                chkSoTang.EditValue = true;
                chkTongGia.EditValue = true;
                chkMoTa.EditValue = true;
                chkGhiChu.EditValue = true;
                chkAnh.EditValue =true;
                chkViTri.EditValue = true;
                objTT = new mglThongTinGui();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            objTT.SoNha = (bool?)chkSoNha.EditValue;
            objTT.TenDuong = (bool?)chkTenDuong.EditValue;
            objTT.PhuongXa = (bool?)chkPhuongXa.EditValue;
            objTT.QuanHuyen = (bool?)chkQuanHuyen.EditValue;
            objTT.TinhThanhPho = (bool?)chkTinhThanhPho.EditValue;
            objTT.MatTien = (bool?)chkMatTien.EditValue;
            objTT.DienTich = (bool?)chkDienTich.EditValue;
            objTT.SoTang = (bool?)chkSoTang.EditValue;
            objTT.TongGia = (bool?)chkTongGia.EditValue;
            objTT.MoTa = (bool?)chkMoTa.EditValue;
            objTT.GhiChu = (bool?)chkGhiChu.EditValue;
            objTT.LinkAnh = (bool?)chkAnh.EditValue;
            objTT.LinkViTri = (bool?)chkViTri.EditValue;

            if (this.ID == null)
                db.mglThongTinGuis.InsertOnSubmit(objTT);
            db.SubmitChanges();
            this.ID = objTT.ID;

            this.Close();
        }
    }
}