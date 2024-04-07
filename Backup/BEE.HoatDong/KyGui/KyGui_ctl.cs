using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEE.NghiepVuKhac;
using BEEREMA;

namespace BEE.HoatDong.KyGui
{
    public partial class KyGui_ctl : UserControl
    {
        int MaPKG = 0;
        bool KT = false, KT1 = false;
        public KyGui_ctl()
        {
            InitializeComponent();

            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBC);
        }

        int GetAccessData()
        {
            it.AccessDataCls o = new it.AccessDataCls(BEE.ThuVien.Common.PerID, 30);

            return o.SDB.SDBID;
        }

        void LoadData()
        {
            it.pkgPhieuKyGuiCls o = new it.pkgPhieuKyGuiCls();
            switch (GetAccessData())
            {
                case 1://Tat ca
                    gridControl1.DataSource = o.Select(dateTuNgay.DateTime, dateDenNgay.DateTime);
                    break;
                case 2://Theo nhom
                    gridControl1.DataSource = o.SelectByGroup(dateTuNgay.DateTime, dateDenNgay.DateTime, BEE.ThuVien.Common.StaffID, BEE.ThuVien.Common.GroupID);
                    break;
                case 3://Theo phong ban
                    gridControl1.DataSource = o.SelectByDeparment(dateTuNgay.DateTime, dateDenNgay.DateTime, BEE.ThuVien.Common.StaffID, BEE.ThuVien.Common.DepartmentID);
                    break;
                case 4://Theo nhan vien
                    gridControl1.DataSource = o.SelectByStaff(dateTuNgay.DateTime, dateDenNgay.DateTime, BEE.ThuVien.Common.StaffID);
                    break;
                default:
                    gridControl1.DataSource = null;
                    break;
            }

            lookUpTinhTrang.DataSource = o.TinhTrang.Select();
            lookUpDaiLy.DataSource = o.HDMB.DaiLy.SelectShow2();
            lookUpNhanVienDL.DataSource = o.NVDL.SelectShow();
        }

        void LoadPermission()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = BEE.ThuVien.Common.PerID;
            o.AccessData.Form.FormID = 30;
            DataTable tblAction = o.SelectBy();
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            barSubItemDuyet.Enabled = false;
            btnIn.Enabled = false;
            btnPreview.Enabled = false;

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
                        case 4:
                            btnIn.Enabled = true;
                            break;
                        case 7:
                            barSubItemDuyet.Enabled = true;
                            break;
                        case 34://Preview
                            btnPreview.Enabled = true;
                            break;
                    }
                }
            }
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

        private void KyGui_ctl_Load(object sender, EventArgs e)
        {
            cmbKyBC.SelectedIndex = 0;
            LoadPermission();
        }

        void LoadQuaTrinh()
        {
            it.pkgQuaTrinhThucHienCls o = new it.pkgQuaTrinhThucHienCls();
            gridControlQTTH.DataSource = o.Select(MaPKG);
            lookUpNhanVienQTTH.DataSource = o.NhanVien.SelectShow();
            lookUpTTQTTH.DataSource = o.TinhTrang.Select();            
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaPKG) != null)
            {
                txtYeuCau.Text = gridView1.GetFocusedRowCellValue(colYeuCau).ToString();
                MaPKG = int.Parse(gridView1.GetFocusedRowCellValue(colMaPKG).ToString());
                LoadQuaTrinh();
                if (gridView1.GetFocusedRowCellValue(colMaTT).ToString() == "2")
                    barSubItemDuyet.Enabled = false;
                else
                    barSubItemDuyet.Enabled = true;
            }
            else
                txtYeuCau.Text = "";
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            KyGui_frm frm = new KyGui_frm();
            frm.Show();
            if (frm.IsUpdate)
                LoadData();
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Edit();
        }

        void Edit()
        {
            if (gridView1.GetFocusedRowCellValue(colMaPKG) != null)
            {
                KyGui_frm frm = new KyGui_frm();
                frm.MaPKG = int.Parse(gridView1.GetFocusedRowCellValue(colMaPKG).ToString());
                frm.MaBDS = gridView1.GetFocusedRowCellValue(colMaBDS).ToString();
                frm.MaHDMB = int.Parse(gridView1.GetFocusedRowCellValue(colMaHDMB).ToString());
                frm.MaKH = int.Parse(gridView1.GetFocusedRowCellValue(colMaKH).ToString());
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadData();
            }
            else
                DialogBox.Infomation("Vui lòng chọn phiếu ký gửi muốn sửa. Xin cảm ơn");
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Edit();
        }
        void Duyet(byte _MaTT)
        {
            if (gridView1.GetFocusedRowCellValue(colMaPKG) != null)
            {
                int RowIndex = 0;
                MaPKG = int.Parse(gridView1.GetFocusedRowCellValue(colMaPKG).ToString());
                RowIndex = gridView1.FocusedRowHandle;
                
                Duyet_frm frm = new Duyet_frm();
                frm.MaTT = _MaTT;
                frm.MaPKG = MaPKG;
                frm.ShowDialog();
                if (frm.IsUpdate)
                {
                    LoadData();
                    gridView1.FocusedRowHandle = RowIndex;
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn phiếu ký gửi cần duyệt. Xin cảm ơn");
        }


        private void btnDaDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Duyet(2);
        }

        private void btnKhongDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Duyet(3);
        }

        private void btnIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaPKG) != null)
            {
                PhieuKyGuiCls o = new PhieuKyGuiCls();
                o.Print(MaPKG);
            }
            else
                DialogBox.Infomation("Vui lòng chọn phiếu ký gửi muốn in. Xin cảm ơn");            
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaPKG) != null)
            {
                if (DialogBox.Question("Bạn có chắc chắn muốn hủy Văn bản chuyển nhượng: <" + gridView1.GetFocusedRowCellValue(colSoPhieu).ToString() + "> không?") == DialogResult.Yes)
                {
                    try
                    {
                        int row = gridView1.FocusedRowHandle;
                        it.pkgPhieuKyGuiCls o = new it.pkgPhieuKyGuiCls();
                        o.MaPKG = int.Parse(gridView1.GetFocusedRowCellValue(colMaPKG).ToString());
                        o.Delete();
                        LoadData();
                        gridView1.FocusedRowHandle = row;
                    }
                    catch
                    {
                        DialogBox.Infomation("Hủy không thành công vì Văn bản chuyển nhượng: <" + gridView1.GetFocusedRowCellValue(colSoPhieu).ToString() + "> đã được sử dụng. Vui lòng kiểm tra lại.");
                    }
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn Văn bản chuyển nhượng muốn xóa. Xin cảm ơn");
        }

        private void hplDownload_Click(object sender, EventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaPKG) != null)
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
                    DialogBox.Infomation("Phiếu ký gửi này không có file đính kèm nên không thể tải về.");
            }
        }

        private void btnExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaPKG) != null)
            {
                SaveFileDialog fbd = new SaveFileDialog();
                fbd.Title = "Chọn thư mục lưu file";
                fbd.Filter = "File Rich Text Format(.rtf)|*.rtf";
                fbd.FileName = string.Format("{0}.rtf", gridView1.GetFocusedRowCellValue(colSoPhieu).ToString());
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    PhieuKyGuiCls o = new PhieuKyGuiCls();
                    o.ExportTo(int.Parse(gridView1.GetFocusedRowCellValue(colMaPKG).ToString()), fbd.FileName);
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn thỏa thuận đặt cọc muốn export document. Vui lòng kiểm tra lại, xin cảm ơn.");
        }

        private void btnPreview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaPKG) != null)
            {
                PhieuKyGuiCls o = new PhieuKyGuiCls();
                Review_frm frm = new Review_frm();
                frm.Content = o.ExportRtf(int.Parse(gridView1.GetFocusedRowCellValue(colMaPKG).ToString()));
                frm.ShowDialog();
            }
            else
                DialogBox.Infomation("Vui lòng chọn phiếu ký gửi muốn review. Xin cảm ơn");
        }
    }
}
