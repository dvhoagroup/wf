using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Data.Linq.SqlClient;
using BEE.ThuVien;
using BEEREMA;

namespace BEE.THUCHI.PhieuChi
{
    public partial class ctlManagerCongNo : DevExpress.XtraEditors.XtraUserControl
    {
        MasterDataContext db = new MasterDataContext();

        public ctlManagerCongNo()
        {
            InitializeComponent();
      

            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);
        }

        void LoadPermission()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = Common.PerID;
            o.AccessData.Form.FormID = 34;
            DataTable tblAction = o.SelectBy();
            itemSua.Enabled = false;
            itemThem.Enabled = false;
            itemXoa.Enabled = false;
            itemDuyet.Enabled = false;
            itemKhongDuyet.Enabled = false;
            itemDaThuTien.Enabled = false;
            itemChuaThuTien.Enabled = false;
            itemTHuChuaDu.Enabled = false;
            itemGiaoDichThanhCong.Enabled = false;

            if (tblAction.Rows.Count > 0)
            {
                foreach (DataRow r in tblAction.Rows)
                {
                    switch (byte.Parse(r["FeatureID"].ToString()))
                    {
                        case 1:
                            itemThem.Enabled = true;
                            break;
                        case 2:
                           itemSua.Enabled = true;
                            break;
                        case 3:
                         itemXoa.Enabled = true;
                            break;
                        case 7://duyet thong tin
                            itemDuyet.Enabled = true;
                            itemKhongDuyet.Enabled = true;
                            break;
                        case 8://Xác nhận thu tiền
                            itemTHuChuaDu.Enabled = true;
                            itemChuaThuTien.Enabled = true;
                            break;
                        case 44:// xác nhận hoàn thành
                            itemGiaoDichThanhCong.Enabled = true;
                            break;
                    }
                }
            }
        }

        int GetAccessData()
        {
            it.AccessDataCls o = new it.AccessDataCls(Common.PerID, 34);

            return o.SDB.SDBID;
        }

        void GiaoDich_Load()
        {

            var tuNgay = (DateTime?)itemTuNgay.EditValue ?? DateTime.Now;
            var denNgay = (DateTime?)itemDenNgay.EditValue ?? DateTime.Now;
            int MaNV = Common.StaffID;
           
            db = new MasterDataContext();

            gcLichTT.DataSource = db.pgcPhieuChi_Select(tuNgay, denNgay,0, 0, 0, 0);
            var wait = DialogBox.WaitingForm();

            try
            {
              

                switch (GetAccessData())
                {
                    case 1://Tat ca
                        gcLichTT.DataSource = db.pgcPhieuChi_Select(tuNgay, denNgay, 0, 0, 0, 0).ToList();
                        break;
                    case 2://Theo phong ban
                        gcLichTT.DataSource = db.pgcPhieuChi_Select(tuNgay, denNgay,0, (byte)BEE.ThuVien.Common.DepartmentID, 0, 0);
                        break;
                    case 3://Theo nhom
                        gcLichTT.DataSource = db.pgcPhieuChi_Select(tuNgay, denNgay, 0,0, Common.GroupID, 0);
                        break;
                    case 4://Theo nhan vien
                        gcLichTT.DataSource = db.pgcPhieuChi_Select(tuNgay, denNgay,0, 0, 0, Common.StaffID);
                        break;
                    default:
                        gcLichTT.DataSource = null;
                        break;
                }
            }
            catch { }

            wait.Close();                          
         
        }

        private void GiaoDich_Edit()
        {
           
        }
        private void GiaoDich_Edit1()
        {
            
        }

        private void GiaoDich_Delete()
        {
            var indexs = grvLichTT.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn phiếu giao dịch");
                return;
            }
            if (DialogBox.Question() == DialogResult.No) return;
            foreach (var i in indexs)
            {
                try
                {
                    var obj = db.pgcPhieuChis.SingleOrDefault(p => p.MaPC == (int)grvLichTT.GetRowCellValue(i, "MaPC"));
                    db.pgcPhieuChis.DeleteOnSubmit(obj);
                }
                catch
                {
                }
             
            }
            db.SubmitChanges();

            grvLichTT.DeleteSelectedRows();
        }

        private void TabPage_Load()
        {
          
           
        }

        private void ctlManager_Load(object sender, EventArgs e)
        {
            SetDate(0);
            lkLoaiChi.DataSource = db.pgcLoaiPhieuThuChis;
            lkKhachHang.DataSource = db.KhachHangs.Select(p => new { p.MaKH, HoTen = p.HoKH + " " + p.TenKH });
            LoadPermission();
            GetAccessData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GiaoDich_Load();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var ID = (int?)grvLichTT.GetFocusedRowCellValue("MaPC");
                var frm = new frmEdit();
                frm.ID = ID;
                frm.ShowDialog();
                GiaoDich_Load();
            }
            catch
            {
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GiaoDich_Delete();
        }

        private void grvGiaoDich_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void grvGiaoDich_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                GiaoDich_Delete();
            }
        }

        void SetDate(int index)
        {
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            GiaoDich_Load();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            GiaoDich_Load();
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void grvGiaoDich_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            TabPage_Load();
        }

        private void tabMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            TabPage_Load();
        }

        private void grvGiaoDich_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator & e.RowHandle >= 0)
            {
                //var cl = System.Drawing.Color.FromArgb((int)grvGiaoDich.GetRowCellValue(e.RowHandle, "MauNen"));
                //e.Graphics.FillRectangle(new SolidBrush(cl), e.Info.Bounds);
                //e.Handled = true;
            }
        }

        private void grvGiaoDich_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            //if (e.Column.FieldName == "TenHT")
            //{
            //    e.Appearance.ForeColor = grvGiaoDich.GetRowCellValue(e.RowHandle, "TenHT").ToString() == "Bán" ?
            //            System.Drawing.Color.Red : System.Drawing.Color.Black;
            //    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            //}
        }

        void Duyet(byte maTT)
        {
            //if (grvGiaoDich.FocusedRowHandle < 0)
            //{
            //    DialogBox.Error("Vui lòng chọn giao dịch");
            //    return;
            //}

            //frmDuyet frm = new frmDuyet();
            //frm.MaTT = maTT;
            //frm.MaGD = (int)grvGiaoDich.GetFocusedRowCellValue("MaGD");
            //frm.ShowDialog();
            //if (frm.DialogResult == DialogResult.OK)
            //    GiaoDich_Load();
        }

        private void itemDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Duyet(3);
        }

        private void itemKhongDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void itemChuaThuTien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Duyet(5);
        }

        private void itemGiaoDichThanhCong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private void itemTHuChuaDu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Duyet(6);
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Duyet(2);
        }

        private void itemDaKyHD_Click(object sender, EventArgs e)
        {

        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new frmEdit();
            frm.ShowDialog();
        }

        private void itemCapNhat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var MaKH = (int?)grvLichTT.GetFocusedRowCellValue("MaKH");
            var frm = new BEE.THUCHI.PhieuThu.frmEdit();
            frm.MaKH = MaKH;
            frm.ShowDialog();
        }
    }
}
