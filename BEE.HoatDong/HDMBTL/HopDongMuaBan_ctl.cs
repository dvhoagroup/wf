using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace LandSoft.HDMBTL
{
    public partial class HopDongMuaBan_ctl : UserControl
    {
        int MaPDC = 0, MaPGC = 0, MaHDMB = 0, MaHDGV = 0;
        bool KT = false, KT1 = false;
        public HopDongMuaBan_ctl()
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
            o.AccessData.Form.FormID = 29;
            DataTable tblAction = o.SelectBy();
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnIn.Enabled = false;
            btnSua.Enabled = false;

            if (tblAction.Rows.Count > 0)
            {
                foreach (DataRow r in tblAction.Rows)
                {
                    switch (byte.Parse(r["FeatureID"].ToString()))
                    {
                        case 1:
                            btnThem.Enabled = true;
                            barButtonItemThem.Enabled = true;
                            break;
                        case 2:
                            btnSua.Enabled = true;
                            barButtonItemSua.Enabled = true;
                            break;
                        case 3:
                            btnXoa.Enabled = true;
                            barButtonItemXoa.Enabled = true;
                            break;
                        case 4:
                            btnIn.Enabled = true;
                            break;
                    }
                }
            }
        }

        void LoadData()
        {
            it.HopDongMuaBanCls o = new it.HopDongMuaBanCls();
            gridControl1.DataSource = o.SelectTL(dateTuNgay.DateTime, dateDenNgay.DateTime);

            lookUpTinhTrang.DataSource = o.TinhTrang.Select();
            lookUpNhanVienKT.DataSource = o.NhanVienKT.SelectShow();
            lookUpDaiLy.DataSource = o.DaiLy.SelectShow();
            lookUpKhachHang.DataSource = o.KhachHang.SelectShow();
        }

        private void PhieuGiuCho_ctl_Load(object sender, EventArgs e)
        {
            cmbKyBC.SelectedIndex = 3;
            LoadPermission();
        }

        void LoadPhieuThu()
        {
            it.pgcPhieuThuCls o = new it.pgcPhieuThuCls();
            gridControlPT.DataSource = o.Select(MaPGC);
            lookUpLoaiTien2.DataSource = o.LoaiTien.Select();
            lookUpNhanVien2.DataSource = o.NhanVien.SelectShow();
        }

        void LoadPhieuThu2()
        {
            if (MaHDMB != 0)
            {
                it.pgcPhieuThuCls o = new it.pgcPhieuThuCls();
                gridControlPT.DataSource = o.Select(MaHDMB, "HDMB");
                lookUpLoaiTien2.DataSource = o.LoaiTien.Select();
                lookUpNhanVien2.DataSource = o.NhanVien.SelectShow();
            }
            else
            {
                it.hdgvPhieuThuCls o = new it.hdgvPhieuThuCls();
                o.Lich.HDGV.MaHDGV = MaHDGV;
                gridControlPT.DataSource = o.SelectBy();
                lookUpLoaiTien2.DataSource = o.LoaiTien.Select();
                lookUpNhanVien2.DataSource = o.NhanVien.SelectShow();
            }
        }

        void LoadPhieuChi()
        {
            it.pgcPhieuChiCls o = new it.pgcPhieuChiCls();
            gridControlPC.DataSource = o.Select(MaPGC);
            lookUpNhanVienPC.DataSource = o.NhanVien.SelectShow();
        }

        void LoadPhieuChi2()
        {
            if (MaHDMB != 0)
            {
                it.pgcPhieuChiCls o = new it.pgcPhieuChiCls();
                gridControlPC.DataSource = o.Select(MaHDMB, "HDMB");
                lookUpNhanVienPC.DataSource = o.NhanVien.SelectShow();
            }
            else
            {
                it.hdgvPhieuChiCls o = new it.hdgvPhieuChiCls();
                gridControlPC.DataSource = o.Select(MaHDGV);
                lookUpLoaiTienPC.DataSource = o.LoaiTien.Select();
                lookUpNhanVienPC.DataSource = o.NhanVien.SelectShow();
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaHDMB) != null)
            {
                MaPGC = int.Parse(gridView1.GetFocusedRowCellValue(colMaPGC).ToString());
                MaHDMB = int.Parse(gridView1.GetFocusedRowCellValue(colMaHDMB).ToString());
                MaHDGV = int.Parse(gridView1.GetFocusedRowCellValue(colMaHDGV).ToString());
                txtNoiDung.Text = gridView1.GetFocusedRowCellValue(colNoiDung).ToString();
            }
            else
            {
                MaPGC = 0;
                MaHDMB = 0;
                txtNoiDung.Text = "";
            }

            if (MaHDGV != 0)
            {
                LoadPhieuThu2();
                LoadPhieuChi2();
            }
            else
            {
                LoadPhieuThu();                
                LoadPhieuChi();
            }
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            //if (gridView1.GetFocusedRowCellValue(colMaPGC) != null)
            //{
            //    MaPGC = int.Parse(gridView1.GetFocusedRowCellValue(colMaPGC).ToString());
            //    LoadLichTT();
            //    LoadPhieuThu();
            //    LoadPhieuChi();
            //}

            if (gridView1.GetFocusedRowCellValue(colMaPGC) != null)
            {
                MaPGC = int.Parse(gridView1.GetFocusedRowCellValue(colMaPGC).ToString());
                MaHDMB = int.Parse(gridView1.GetFocusedRowCellValue(colMaHDMB).ToString());
                MaHDGV = int.Parse(gridView1.GetFocusedRowCellValue(colMaHDGV).ToString());
            }
            else
            {
                MaPGC = 0;
                MaHDMB = 0;
            }

            if (MaHDGV != 0)
            {
                LoadPhieuThu2();
                LoadPhieuChi2();
            }
            else
            {
                LoadPhieuThu();
                LoadPhieuChi();
            }
        }
        
        private void barButtonItemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnNap_ItemClick(sender, e);
        }

        private void btnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaHDMB) != null)
            {
                HDMB.BBThanhLyCls o = new HDMB.BBThanhLyCls();
                o.Print(int.Parse(gridView1.GetFocusedRowCellValue(colMaHDMB).ToString()));
            }
            else
                DialogBox.Infomation("Vui lòng chọn hợp đồng mua bán muốn in biên bản thanh lý. Vui lòng kiểm tra lại, xin cảm ơn");    
        }

        private void hplDownload_Click(object sender, EventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaHDMB) != null)
            {
                if (gridView1.GetFocusedRowCellValue(colFileAttach).ToString() != "")
                {
                    FolderBrowserDialog fbd = new FolderBrowserDialog();
                    fbd.Description = "Chọn thư mục để lưu file tải về";
                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        it.FTPCls objFTP = new it.FTPCls();
                        objFTP.Download(fbd.SelectedPath, gridView1.GetFocusedRowCellValue(colFileAttach).ToString());
                    }
                }
                else
                    DialogBox.Infomation("Hợp đồng mua bán này không có file đính kèm nên không thể tải về.");
            }
        }

        void Edit()
        {
            if (gridView1.GetFocusedRowCellValue(colMaHDMB) != null)
            {
                HDMB.ThanhLy_frm frm = new LandSoft.HDMB.ThanhLy_frm();
                frm.Edit = true;
                frm.MaHDMB = int.Parse(gridView1.GetFocusedRowCellValue(colMaHDMB).ToString());
                frm.MaKH = int.Parse(gridView1.GetFocusedRowCellValue(colMaKH).ToString());
                frm.MaSo = gridView1.GetFocusedRowCellValue(colMaSo).ToString();
                frm.HoTen = lookUpKhachHang.GetDisplayValueByKeyValue(int.Parse(gridView1.GetFocusedRowCellValue(colMaKH).ToString())).ToString();
                frm.SoPhieu = gridView1.GetFocusedRowCellValue(colSoPhieu).ToString();
                frm.ShowDialog();
            }
            else
                DialogBox.Infomation("Vui lòng chọn hợp đồng mua bán muốn cập nhật thông tin. Xin cảm ơn.");
        }

        private void btnSua2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Edit();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Edit();
        }

        private void btnPreview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaHDMB) != null)
            {
                HDMB.BBThanhLyCls o = new HDMB.BBThanhLyCls();
                NghiepVu.Khac.Review_frm frm = new LandSoft.NghiepVu.Khac.Review_frm();
                frm.Content = o.ExportRtf(int.Parse(gridView1.GetFocusedRowCellValue(colMaHDMB).ToString()));
                frm.ShowDialog();
            }
            else
                DialogBox.Infomation("Vui lòng chọn hợp đồng mua bán muốn Preview biên bản thanh lý. Xin cảm ơn");
        }
    }
}