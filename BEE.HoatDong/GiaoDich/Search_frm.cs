using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;

namespace LandSoft.NghiepVu.GiaoDich
{
    public partial class Search_frm : DevExpress.XtraEditors.XtraForm
    {
        public int MaGD = 0, MaGD1 = 0, MaKH = 0, MaNV1 = 0, MaNV2 = 0;
        public string MaBDS = "";
        public bool Share1 = false, Share2 = false;
        public byte MaTT = 0;
        public Search_frm()
        {
            InitializeComponent();
        }

        void LoadTienIch()
        {
            it.TienIchCls o = new it.TienIchCls();
            chkListTienIch.DataSource = o.Select();
        }

        void LoadLoaiBDS()
        {
            it.LoaiBDSCls o = new it.LoaiBDSCls();
            lookUpLoaiBDS.Properties.DataSource = o.Select();
            lookUpLoaiBDS.ItemIndex = 0;
            lookUpLoaBDS.DataSource = o.Select();
        }

        void LoadLoaiGD()
        {
            it.LoaiGiaoDichCls o = new it.LoaiGiaoDichCls();
            lookUpLoaiGiaoDich.Properties.DataSource = o.Select();
            lookUpLoaiGiaoDich.ItemIndex = 0;
            lookUpLoaiGD.DataSource = o.Select();
        }

        void LoadHuong()
        {
            it.PhuongHuongCls o = new it.PhuongHuongCls();
            lookUpHuong.Properties.DataSource = o.Select();
            lookUpHuong.ItemIndex = 0;
        }

        void LoadPhapLy()
        {
            it.PhapLyCls o = new it.PhapLyCls();
            lookUpPhapLy.Properties.DataSource = o.Select();
            lookUpPhapLy.ItemIndex = 0;
        }

        void LoadLoaiTien()
        {
            it.LoaiTienCls objLoaiTien = new it.LoaiTienCls();
            lookUpLoaiTien.Properties.DataSource = objLoaiTien.Select();
            lookUpPhapLy.ItemIndex = 0;
        }

        private void Search_frm_Load(object sender, EventArgs e)
        {
            LoadTienIch();
            LoadLoaiGD();
            LoadHuong();
            LoadPhapLy();
            LoadLoaiBDS();
            LoadLoaiTien();
            if (MaGD1 != 0)
                LoadData();
            if (MaTT == 2)
            {
                btnDatCoc.Enabled = true;
                btnGiuCho.Enabled = true;
            }
            timer1.Start();
        }

        void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            

            it.pdkGiaoDichCls o = new it.pdkGiaoDichCls(MaGD1);
            chkBep.Checked = o.Bep;
            chkNguoiGV.Checked = o.PhongNGV;
            chkPhongDocSach.Checked = o.PhongDocSach;
            spinDTPhongKhach.EditValue = o.DTPhongKhach;
            spinKichThuoc.EditValue = o.Dai * o.Rong;
            spinPhongKhach.EditValue = o.PhongKhach;
            spinSoPhongNgu.EditValue = o.PhongNgu;
            spinSoPhongWC.EditValue = o.PhongWC;
            spinSoTang.EditValue = o.SoTang;
            spinDienTichXD.EditValue = o.DienTichXD;
            spinViTri.EditValue = o.ViTriTang;
            spinDonGia.EditValue = o.DonGia;
            
            lookUpHuong.EditValue = o.Huong.MaPhuongHuong;
            lookUpLoaiBDS.EditValue = o.LoaiBDS.MaLBDS;
            switch (o.LGD.MaLDG)
            {
                case 1://Can mua
                    lookUpLoaiGiaoDich.EditValue = (byte)2; break;
                case 2:
                    lookUpLoaiGiaoDich.EditValue = (byte)1; break;
                case 3:
                    lookUpLoaiGiaoDich.EditValue = (byte)4; break;
                case 4:
                    lookUpLoaiGiaoDich.EditValue = (byte)3; break;
            }
            lookUpLoaiGiaoDich.Enabled = false;

            lookUpPhapLy.EditValue = o.PhapLy.MaPL;
            lookUpLoaiTien.EditValue = o.LoaiTien.MaLoaiTien;
            lookUpTinhTrang.DataSource = o.TinhTrang.Select();
            lookUpLoaiGD.DataSource = o.LGD.Select();

            LoadTienIchs();

            try { wait.Close(); wait.Dispose(); }
            catch { }
        }

        void LoadTienIchs()
        {
            it.pdkgd_TienIchCls o = new it.pdkgd_TienIchCls();
            o.MaPGD = MaGD1;
            DataTable tbl = o.SelectBy();
            foreach (DataRow r in tbl.Rows)
            {
                for (int j = 0; j < chkListTienIch.ItemCount; j++)
                {
                    if (byte.Parse(r["MaTienIch"].ToString()) == byte.Parse(chkListTienIch.GetItemValue(j).ToString()))
                        chkListTienIch.SetItemChecked(j, true);
                }
            }
        }

        void LoadComfortable()
        {
            it.pdkgd_TienIchCls o = new it.pdkgd_TienIchCls();
            o.MaPGD = MaGD;
            gridControlTienIch.DataSource = o.SelectBy();
        }

        string GetComfortable()
        {
            string temp = "";
            int i = 0;
            while (chkListTienIch.GetItem(i) != null)
            {
                if (chkListTienIch.GetItemCheckState(i) == CheckState.Checked)
                    temp += "i" + byte.Parse(chkListTienIch.GetItemValue(i).ToString());                    
                i++;
            }

            return temp == "" ? "'%%'" : temp;
        }

        void LoadBDS()
        {
            it.pdkGiaoDichCls o = new it.pdkGiaoDichCls();
            o.MaGD = MaGD;
            vGridControl1.DataSource = o.SelectByBDS();
        }

        void LoadNotes()
        {
            it.pgdNhatKyXuLyCls o = new it.pgdNhatKyXuLyCls();
            o.GiaoDich.MaGD = MaGD;
            gridControl3.DataSource = o.SelectBy();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaGD) != null)
                MaGD = int.Parse(gridView1.GetFocusedRowCellValue(colMaGD).ToString());
            else
                MaGD = 0;
            LoadBDS();
            LoadKhachHang();
            LoadComfortable();
            LoadNotes();
        }

        void LoadKhachHang()
        {
            it.pdkGiaoDichCls o = new it.pdkGiaoDichCls();
            o.MaGD = MaGD;
            o.NhanVien1.MaNV = LandSoft.Library.Common.StaffID;
            vGridKhachHang.DataSource = o.SelectByKhachHang();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string QureyString = "";
            QureyString += byte.Parse(lookUpLoaiGiaoDich.EditValue.ToString());
            QureyString += ", " + byte.Parse(lookUpLoaiBDS.EditValue.ToString());
            QureyString += ", " + byte.Parse(lookUpHuong.EditValue.ToString());
            QureyString += ", " + byte.Parse(spinSoPhongNgu.EditValue.ToString());
            QureyString += ", " + byte.Parse(spinSoPhongWC.EditValue.ToString());
            QureyString += ", " + bool.Parse(chkPhongDocSach.EditValue.ToString());
            QureyString += ", " + bool.Parse(chkBep.EditValue.ToString());
            QureyString += ", " + double.Parse(spinKichThuoc.EditValue.ToString());
            QureyString += ", " + double.Parse(spinDienTichXD.EditValue.ToString());
            QureyString += ", " + byte.Parse(spinSoTang.EditValue.ToString());
            QureyString += ", " + byte.Parse(spinViTri.EditValue.ToString());
            QureyString += ", " + byte.Parse(spinPhongKhach.EditValue.ToString());
            QureyString += ", " + double.Parse(spinDTPhongKhach.EditValue.ToString());
            QureyString += ", " + bool.Parse(chkNguoiGV.EditValue.ToString());
            QureyString += ", " + byte.Parse(lookUpPhapLy.EditValue.ToString());
            QureyString += ", " + double.Parse(spinDonGia.EditValue.ToString());
            QureyString += ", " + GetComfortable();
            QureyString += ", " + LandSoft.Library.Common.StaffID;

            gridControl1.DataSource = it.CommonCls.Table("BatDongSan_Search  " + QureyString);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if (int.Parse(gridView1.GetRowCellValue(i, colSoGiay).ToString()) > 0)
                {
                    gridView1.SetRowCellValue(i, colThoiHan, it.ConvertDateTimeCls.StringDateTime(int.Parse(gridView1.GetRowCellValue(i, colSoGiay).ToString())));
                    gridView1.SetRowCellValue(i, colSoGiay, int.Parse(gridView1.GetRowCellValue(i, colSoGiay).ToString()) - 1);
                }
            }

            timer1.Start();
        }

        private void btnDatCoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaGD) != null)
            {
                BienBanDatCoc_frm frm = new BienBanDatCoc_frm();
                frm.MaGD1 = MaGD1;
                frm.MaGD2 = int.Parse(gridView1.GetFocusedRowCellValue(colMaGD).ToString());
                if (gridView1.GetFocusedRowCellValue(colMaBDS).ToString() != "")
                    frm.MaBDS = gridView1.GetFocusedRowCellValue(colMaBDS).ToString();
                else
                    frm.MaBDS = MaBDS;
                if (lookUpLoaiGiaoDich.ItemIndex == 1 || lookUpLoaiGiaoDich.ItemIndex == 3)//Mua hoac thue
                {
                    frm.MaKH1 = MaKH;
                    frm.MaKH2 = int.Parse(gridView1.GetFocusedRowCellValue(colMaKH).ToString());
                    frm.Share1 = Share1;
                    frm.Share2 = bool.Parse(gridView1.GetFocusedRowCellValue(colShare).ToString());
                    frm.MaNV1 = MaNV1;
                    frm.MaNV2 = int.Parse(gridView1.GetFocusedRowCellValue(colMaNV).ToString());
                }
                else
                {
                    frm.MaKH2 = MaKH;
                    frm.MaKH1 = int.Parse(gridView1.GetFocusedRowCellValue(colMaKH).ToString());
                    frm.Share2 = Share1;
                    frm.Share1 = bool.Parse(gridView1.GetFocusedRowCellValue(colShare).ToString());
                    frm.MaNV2 = MaNV1;
                    frm.MaNV1 = int.Parse(gridView1.GetFocusedRowCellValue(colMaNV).ToString());
                }
                
                frm.ShowDialog();
                if (frm.IsUpdate)
                    this.Close();
            }
            else
                DialogBox.Infomation("Vui lòng chọn phiếu đăng ký giao dịch muốn đặt cọc. Xin cảm ơn.");
        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnGiuCho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaGD) != null)
            {
                PhieuGiuCho_frm frm = new PhieuGiuCho_frm();
                frm.MaGD1 = MaGD1;
                frm.MaGD2 = int.Parse(gridView1.GetFocusedRowCellValue(colMaGD).ToString());
                if (gridView1.GetFocusedRowCellValue(colMaBDS).ToString() != "")
                    frm.MaBDS = gridView1.GetFocusedRowCellValue(colMaBDS).ToString();
                else
                    frm.MaBDS = MaBDS;
                if (lookUpLoaiGiaoDich.ItemIndex == 1 || lookUpLoaiGiaoDich.ItemIndex == 3)//Mua hoac thue
                {
                    frm.MaKH1 = MaKH;
                    frm.MaKH2 = int.Parse(gridView1.GetFocusedRowCellValue(colMaKH).ToString());
                    frm.Share1 = Share1;
                    frm.Share2 = bool.Parse(gridView1.GetFocusedRowCellValue(colShare).ToString());
                    frm.MaNV1 = MaNV1;
                    frm.MaNV2 = int.Parse(gridView1.GetFocusedRowCellValue(colMaNV).ToString());
                }
                else
                {
                    frm.MaKH2 = MaKH;
                    frm.MaKH1 = int.Parse(gridView1.GetFocusedRowCellValue(colMaKH).ToString());
                    frm.Share2 = Share1;
                    frm.Share1 = bool.Parse(gridView1.GetFocusedRowCellValue(colShare).ToString());
                    frm.MaNV2 = MaNV1;
                    frm.MaNV1 = int.Parse(gridView1.GetFocusedRowCellValue(colMaNV).ToString());
                }
                frm.ShowDialog();
                if (frm.IsUpdate)
                    this.Close();
            }
            else
                DialogBox.Infomation("Vui lòng chọn phiếu đăng ký giao dịch muốn giữ chỗ. Xin cảm ơn.");
        }

        private void btnYeuCau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaGD) != null)
            {
                YeuCauHoTro_frm frm = new YeuCauHoTro_frm();
                frm.MaGD = int.Parse(gridView1.GetFocusedRowCellValue(colMaGD).ToString());
                frm.MaSo = gridView1.GetFocusedRowCellValue(colSoPhieu).ToString();
                frm.MaNV = int.Parse(gridView1.GetFocusedRowCellValue(colMaNV).ToString());
                frm.NhanVien = gridView1.GetFocusedRowCellValue(colHoTenNV1).ToString();
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadNotes();
            }
            else
                DialogBox.Infomation("Vui lòng chọn phiếu giao dịch muốn gửi yêu cầu hỗ trợ. Xin cảm ơn.");
        }

        private void btnGiaiDap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView4.GetFocusedRowCellValue(colSTT) != null)
            {
                if (int.Parse(gridView4.GetFocusedRowCellValue(colMaNVPT).ToString()) == LandSoft.Library.Common.StaffID)
                {
                    Reply_frm frm = new Reply_frm();
                    frm.MaSo = gridView1.GetFocusedRowCellValue(colSoPhieu).ToString();
                    frm.MaGD = int.Parse(gridView1.GetFocusedRowCellValue(colMaGD).ToString());
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
    }
}