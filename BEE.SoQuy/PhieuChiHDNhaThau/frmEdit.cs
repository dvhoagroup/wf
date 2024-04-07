using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using BEE.DULIEU;
using BEE.THUVIEN;

namespace BEE.SOQUY.PhieuChiHDNhaThau
{
    public partial class frmEdit : BForm
    {
        public int? ID { get; set; }
        public int? MaKH { get; set; }
        public int? MaHDB { get; set; }
        public byte MaPTTT { set { cmbPTTT.SelectedIndex = value; } }
        public bool IsSave { get; set; }
        private BEE.DULIEU.KhachHang objKH;

        cNTPhieuChi objPC;
       // BEE.CONGCU.ctlMucThuChiItemEdit ctlMucThuChiItemEdit1;
        byte SDBID;

        void PhieuChiLoad()
        {
           
            objPC = db.cNTPhieuChis.Single(p => p.ID == this.ID);
            lookLoaiPC.EditValue = objPC.MaLPC;
            txtSoPC.EditValue = objPC.SoPC;
            dateNgayChi.EditValue = objPC.NgayChi;
         //   ctlNhanVienEdit1.EditValue = objPC.MaNV;
         //   ctlLoaiTienEdit1.EditValue = objPC.MaLT;
            spinTyGia.EditValue = objPC.TyGia;
            
          //  ctlCusTienMat.EditValue = objPC.MaKH;
            txtNguoiNhan.EditValue = objPC.NguoiNhan;
            txtLyDo.EditValue = objPC.LyDo;
            spinSoTienChi.EditValue = objPC.SoTien;
          //  ctlTKNHEdit2.EditValue = objPC.MaTKNH;

            cmbPTTT.SelectedIndex = objPC.MaPTTT.Value;
            switch (cmbPTTT.SelectedIndex)
            {
                case 0:
                    txtDiaChi.EditValue = objPC.DiaChiNN;
                    txtChungTuGoc.EditValue = objPC.ChungTuGoc;
                    break;
                case 1:
                case 2:
                    txtSoTKNNUNC.EditValue = objPC.SoTKNN;
                    txtTenNHNNUNC.EditValue = objPC.TenNHNN;
                    break;
                case 3:
                    txtSoCMND.EditValue = objPC.SoCMND;
                    txtNgayCap.EditValue = objPC.NgayCap;
                    txtNoiCap.EditValue = objPC.NoiCap;
                    break;
            }

            gcChiTiet.DataSource = objPC.cNTpcChiTiets;

            PhieuChiEnable(false);
        }

        void PhieuChiAddNew()
        {
            //if ((bool)itemAdd.Tag == false)
            //{
            //    DialogBox.Error("Bạn không có quyền thêm");
            //    return;
            //}

            db = new MasterDataContext();
            objPC = new cNTPhieuChi();
            dateNgayChi.EditValue = DateTime.Now;
           // ctlCusTienMat.EditValue = null;
            txtNguoiNhan.EditValue = null;
            txtDiaChi.EditValue = null;
         //   ctlTKNHEdit2.EditValue = null;
            txtLyDo.EditValue = null;
            txtSoTKNNUNC.EditValue = null;
            txtTenNHNNUNC.EditValue = null;
            txtSoCMND.EditValue = null;
            txtNgayCap.EditValue = null;
            txtNoiCap.EditValue = null;
            gcChiTiet.DataSource = objPC.cNTpcChiTiets;
            txtSoPC.EditValue  = db.DinhDang(12, (db.cNTPhieuChis.Max(p => (int?)p.ID) ?? 0) + 1);
            PhieuChiEnable(true);
        }

        void PhieuChiEdit()
        {
            //if ((bool)itemEdit.Tag == false)
            //{
            //    DialogBox.Error("Bạn không có quyền sửa");
            //    return;
            //}
            PhieuChiEnable(true);
        }

        void PhieuChiEnable(bool enable)
        {
            cmbPTTT.Enabled = enable;
            xtraTabControl1.Enabled = enable;
            grPhieuChi2.Enabled = enable;
            grvChiTiet.OptionsBehavior.Editable = enable;

            itemAdd.Enabled = !enable;
            itemEdit.Enabled = !enable;
            itemSave.Enabled = enable;
            itemDelay.Enabled = enable;
        }

        void LoadSoTien()
        {
            //ctlCusUNC.EditValue = ctlCusTienMat.EditValue;

            //grvChiTiet.SelectAll();
            //grvChiTiet.DeleteSelectedRows();

            //if (ctlCusTienMat.EditValue == null)
            //{
            //    lookMuaHang.DataSource = null;
            //    return;
            //}

            //var ltMH = (from mh in db.mhMuaHangs
            //            join sp in
            //                (from ct in db.mhSanPhams
            //                 join mh in db.mhMuaHangs on ct.MaMH equals mh.ID
            //                 group new { ct, mh } by ct.MaMH into ct
            //                 select new { MaMH = ct.Key, SoTien = ct.Sum(p => (decimal?)(p.ct.SoTien * p.mh.TyGia)) })
            //            on mh.ID equals sp.MaMH
            //            join pc in
            //                (from ct in db.mhpcChiTiets
            //                 join pc in db.mhPhieuChis on ct.MaPC equals pc.ID
            //                 where ct.MaPC != this.ID.GetValueOrDefault()
            //                 group new { pc, ct } by ct.MaMH into ct
            //                 select new { MaMH = ct.Key, TienPC = ct.Sum(p => (decimal?)(p.ct.SoTien * p.pc.TyGia)) })
            //            on mh.ID equals pc.MaMH into ctpc
            //            from pc in ctpc.DefaultIfEmpty()
            //            where mh.MaKH == (int?)ctlCusTienMat.EditValue & sp.SoTien - pc.TienPC.GetValueOrDefault() > 0
            //            select new { mh.ID, mh.SoMH, mh.NgayMH, mh.DienGiai, SoTien = sp.SoTien - pc.TienPC.GetValueOrDefault() }).ToList();
            ////
            //lookMuaHang.DataSource = ltMH;
            ////
            //foreach (var mh in ltMH)
            //{
            //    if (mh.SoTien > 0)
            //    {
            //        grvChiTiet.AddNewRow();
            //        grvChiTiet.SetFocusedRowCellValue("MaMH", mh.ID);
            //        grvChiTiet.SetFocusedRowCellValue("DienGiai", mh.DienGiai);
            //        grvChiTiet.SetFocusedRowCellValue("SoTien", mh.SoTien);
            //    }
            //}
            //grvChiTiet.RefreshData();
        }

        public frmEdit()
        {
            InitializeComponent();

            BEE.NGONNGU.Language.TranslateUserControl(this, barManager1);

           // ctlMucThuChiItemEdit1 = new BEE.CONGCU.ctlMucThuChiItemEdit();
         //   colMaMTC.ColumnEdit = ctlMucThuChiItemEdit1;

            this.Load += new EventHandler(frmEdit_Load);

            //ctlLoaiTienEdit1.EditValueChanged += new EventHandler(ctlLoaiTienEdit1_EditValueChanged);
            //cmbPTTT.SelectedIndexChanged += new EventHandler(cmbPTTT_SelectedIndexChanged);
            //lookLoaiPC.EditValueChanged += new EventHandler(lookLoaiPC_EditValueChanged);
      
            //ctlCusTienMat.EditValueChanged += new EventHandler(ctlCustomerEdit1_EditValueChanged);
            //ctlCusUNC.EditValueChanged += new EventHandler(ctlCusUNC_EditValueChanged);
            //ctlCusSTM.EditValueChanged += new EventHandler(ctlCusSTM_EditValueChanged);

            //lookMuaHang.EditValueChanged += new EventHandler(lookMuaHang_EditValueChanged);
            //ctlTKNHEdit2.EditValueChanged += new EventHandler(ctlTKNHEdit2_EditValueChanged);
            //ctlTKNHEdit1.EditValueChanged += new EventHandler(ctlTKNHEdit1_EditValueChanged);

            txtNguoiNhan.EditValueChanged += new EventHandler(txtNguoiNhan_EditValueChanged);
            txtNguoiNhanUNC.EditValueChanged += new EventHandler(txtNguoiNhanUNC_EditValueChanged);
            txtNguoiNhanSTM.EditValueChanged += new EventHandler(txtNguoiNhanSTM_EditValueChanged);

            txtLyDo.EditValueChanged += new EventHandler(txtLyDo_EditValueChanged);
            txtLyDoUNC.EditValueChanged += new EventHandler(txtLyDoUNC_EditValueChanged);
            txtLyDoSTM.EditValueChanged += new EventHandler(txtLyDoSTM_EditValueChanged);

            spinSoTien.EditValueChanged += new EventHandler(spinSoTien_EditValueChanged);
            
            grvChiTiet.InitNewRow+=new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(grvChiTiet_InitNewRow);
            grvChiTiet.KeyUp += new KeyEventHandler(grvChiTiet_KeyUp);
            grvChiTiet.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(grvChiTiet_ValidateRow);
            grvChiTiet.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(grvChiTiet_InvalidRowException);
                        
            itemPrevious.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemPrevious_ItemClick);
            itemNext.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemNext_ItemClick);
            itemAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemAdd_ItemClick);
            itemEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemEdit_ItemClick);
            itemDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemDelete_ItemClick);
            itemSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemSave_ItemClick);
            itemDelay.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemDelay_ItemClick);
            itemClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemClose_ItemClick); 
            itemPrintDetail.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemPrintDetail_ItemClick);
            itemPrintGiayBaoNo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemPrintGiayBaoNo_ItemClick);
            itemPrintUNC.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemPrintUNC_ItemClick);
        }

        void lookLoaiPC_EditValueChanged(object sender, EventArgs e)
        {
            if ((short?)lookLoaiPC.EditValue == 2)
            {
                txtSoPC.EditValue = db.DinhDang(31, (db.cNTPhieuChis.Max(p => (int?)p.ID) ?? 0) + 1);
             //   grvChiTiet.SelectAll();
             //   grvChiTiet.DeleteSelectedRows();
            }
            else
            {
                txtSoPC.EditValue = db.DinhDang(12, (db.cNTPhieuChis.Max(p => (int?)p.ID) ?? 0) + 1);
              //  LoadSoTien();
            }
        }

        void spinSoTien_EditValueChanged(object sender, EventArgs e)
        {
            grvChiTiet.SetFocusedRowCellValue("SoTien", ((SpinEdit)sender).Value);
        }

        void itemPrintUNC_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (this.ID == null) return;
            //using (var frm = new BEE.CONGCU.PrintForm())
            //{
            //    frm.PrintControl.Report = new rptUyNhiemChi(this.ID);
            //    frm.ShowDialog();
            //}
        }

        void itemPrintGiayBaoNo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (this.ID == null) return;
            //using (var frm = new BEE.CONGCU.PrintForm())
            //{
            //    frm.PrintControl.Report = new rptGiayBaoNo(this.ID);
            //    frm.ShowDialog();
            //}
        }
        
        void itemPrintDetail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (this.ID == null) return;
            //using (var frm = new BEE.CONGCU.PrintForm())
            //{
            //    frm.PrintControl.Report = new rptDetail(this.ID);
            //    frm.ShowDialog();
            //}
        }

        void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        void itemDelay_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PhieuChiEnable(false);
        }

        void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
            if (txtSoPC.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập số phiếu");
                txtSoPC.Focus();
                return;
            }

            if (dateNgayChi.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập ngày chi");
                dateNgayChi.Focus();
                return;
            }

            //if (spinTyGia.Value <= 0)
            //{
            //    DialogBox.Error("<Tỷ giá> phải lớn hơn không (>0)");
            //    spinTyGia.Focus();
            //    return;
            //}

            //if (cmbPTTT.SelectedIndex > 0 && ctlTKNHEdit2.EditValue == null)
            //{
            //    DialogBox.Error("Vui lòng chọn tài khoản trả tiền");
            //    return;
            //}

            //grvChiTiet.RefreshData();
            //if (grvChiTiet.RowCount <= 1)
            //{
            //    DialogBox.Error("Vui lòng nhập thông tin chi tiết");
            //    return;
            //}
      

            objPC.SoPC = txtSoPC.Text;
            objPC.NgayChi = (DateTime?)dateNgayChi.EditValue;
            //if (ctlCusTienMat.EditValue == null)
            //{
            //    objPC.MaKH = null;
            //    objPC.KhachHang = null;                
            //}
            //else
            //{
            //    //objPC.MaKH = (int?)ctlCusTienMat.EditValue;
            //    objPC.KhachHang = db.KhachHangs.Single(p => p.MaKH == objPC.MaKH);
            //    objPC.KhachHang.NgayDangKy = DateTime.Now;
            //}
          //  objPC.MaLT = (short)ctlLoaiTienEdit1.EditValue;
            objPC.TyGia = spinTyGia.Value;
         //   objPC.MaNV = (int?)ctlNhanVienEdit1.EditValue;
            objPC.NguoiNhan = txtNguoiNhan.Text;
            objPC.DiaChiNN = txtDiaChi.Text;
            objPC.LyDo = txtLyDo.Text;
            objPC.ChungTuGoc = txtChungTuGoc.Text;
            objPC.MaLPC = (short?)lookLoaiPC.EditValue;
            objPC.MaPTTT = Convert.ToByte(cmbPTTT.SelectedIndex);
            objPC.SoTien = (decimal?)spinSoTienChi.EditValue;
            objPC.ContractID = MaHDB;
            switch (cmbPTTT.SelectedIndex)
            {
                case 0: objPC.MaTKNH = null; break;
                case 1:
                case 2:
                   // objPC.MaTKNH = (short?)ctlTKNHEdit2.EditValue;
                    objPC.SoTKNN = txtSoTKNNUNC.Text;
                    objPC.TenNHNN = txtTenNHNNUNC.Text;
                    break;
                case 3:
                  //  objPC.MaTKNH = (short?)ctlTKNHEdit1.EditValue;
                    objPC.SoCMND = txtSoCMND.Text;
                    objPC.NgayCap = txtNgayCap.Text;
                    objPC.NoiCap = txtNoiCap.Text;
                    break;
            }

            if (objPC.ID != 0)
            {
                objPC.MaNVS = BEE.THUVIEN.Common.MaNV;
                objPC.NgaySua = DateTime.Now;
            }
            else
            {
                objPC.MaNVN = BEE.THUVIEN.Common.MaNV;
                objPC.NgayNhap = DateTime.Now;
                db.cNTPhieuChis.InsertOnSubmit(objPC);
            }
            //
            db.SubmitChanges();
            //
            this.IsSave = true;
            this.ID = objPC.ID;
            PhieuChiLoad();
            PhieuChiEnable(false);
        }

        void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PhieuChiEdit();
        }

        void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PhieuChiAddNew();
        }

        void itemNext_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int maPB = 0, MaNKD = 0, maNV = 0;
            switch (this.SDBID)
            {
                case 2: maPB = BEE.THUVIEN.Common.MaPB; break;
                case 3: MaNKD = BEE.THUVIEN.Common.MaNKD; break;
                case 4: maNV = BEE.THUVIEN.Common.MaNV; break;
            }
            var idTemp = (from mh in db.cNTPhieuChis
                          join nv in db.NhanViens on mh.MaNV equals nv.MaNV
                          where mh.ID > (this.ID ?? 0) & (nv.MaPB == maPB | maPB == 0) & (nv.MaNKD == MaNKD | MaNKD == 0) & (nv.MaNV == maNV | maNV == 0)
                          select (int?)mh.ID)
                         .Min();
            if (idTemp != null)
            {
                this.ID = idTemp;
                PhieuChiLoad();
            }
        }

        void itemPrevious_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int maPB = 0, MaNKD = 0, maNV = 0;
            switch (this.SDBID)
            {
                case 2: maPB = BEE.THUVIEN.Common.MaPB; break;
                case 3: MaNKD = BEE.THUVIEN.Common.MaNKD; break;
                case 4: maNV = BEE.THUVIEN.Common.MaNV; break;
            }
            var idTemp = (from mh in db.cNTPhieuChis
                          join nv in db.NhanViens on mh.MaNV equals nv.MaNV
                          where (mh.ID < (this.ID ?? 0) | this.ID == null) & (nv.MaPB == maPB | maPB == 0) & (nv.MaNKD == MaNKD | MaNKD == 0) & (nv.MaNV == maNV | maNV == 0)
                          select (int?)mh.ID)
                         .Max();
            if (idTemp != null)
            {
                this.ID = idTemp;
                PhieuChiLoad();
            }
        }

        void grvChiTiet_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            DialogBox.Error(e.ErrorText);
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }
        
        void grvChiTiet_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var soTien = (decimal?)grvChiTiet.GetRowCellValue(e.RowHandle, "SoTien") ?? 0;
            if (soTien <= 0)
            {
                e.ErrorText = "Vui lòng nhập số tiền";
                e.Valid = false;
                return;
            }
        }

        void grvChiTiet_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogBox.Question() == System.Windows.Forms.DialogResult.No) return;
                grvChiTiet.DeleteSelectedRows();
            }
        }

        void grvChiTiet_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            if (this.MaHDB != null)
            {
                grvChiTiet.SetFocusedRowCellValue("MaHDB", this.MaHDB);
            }
        }

        void ctlTKNHEdit1_EditValueChanged(object sender, EventArgs e)
        {
           // ctlTKNHEdit2.EditValue = ctlTKNHEdit1.EditValue;
          //  txtTenNHSTM.EditValue = ctlTKNHEdit1.GetColumnValue("TenNH");
        }

        void ctlTKNHEdit2_EditValueChanged(object sender, EventArgs e)
        {
          //  ctlTKNHEdit1.EditValue = ctlTKNHEdit2.EditValue;
          //  txtTenNHUNC.EditValue = ctlTKNHEdit2.GetColumnValue("TenNH");
        }

        void lookMuaHang_EditValueChanged(object sender, EventArgs e)
        {
            var ltBH = (LookUpEdit)sender;
            grvChiTiet.SetFocusedRowCellValue("DienGiai", ltBH.GetColumnValue("DienGiai"));
            var tyGia = spinTyGia.Value > 0 ? spinTyGia.Value : 1;
            grvChiTiet.SetFocusedRowCellValue("SoTien", (decimal?)ltBH.GetColumnValue("SoTien") / tyGia);
        }

        void txtLyDoSTM_EditValueChanged(object sender, EventArgs e)
        {
            txtLyDo.EditValue = txtLyDoSTM.EditValue;
        }

        void txtLyDoUNC_EditValueChanged(object sender, EventArgs e)
        {
            txtLyDoSTM.EditValue = txtLyDoUNC.EditValue;
        }

        void txtLyDo_EditValueChanged(object sender, EventArgs e)
        {
            txtLyDoUNC.EditValue = txtLyDo.EditValue;
        }

        void txtNguoiNhanSTM_EditValueChanged(object sender, EventArgs e)
        {
            txtNguoiNhan.EditValue = txtNguoiNhanSTM.EditValue;
        }

        void txtNguoiNhanUNC_EditValueChanged(object sender, EventArgs e)
        {
            txtNguoiNhanSTM.EditValue = txtNguoiNhanUNC.EditValue;
        }

        void txtNguoiNhan_EditValueChanged(object sender, EventArgs e)
        {
            txtNguoiNhanUNC.EditValue = txtNguoiNhan.EditValue;
        }

        void ctlCusSTM_EditValueChanged(object sender, EventArgs e)
        {
            //ctlCusTienMat.EditValue = ctlCusSTM.EditValue;
        }

        void ctlCusUNC_EditValueChanged(object sender, EventArgs e)
        {
            //ctlCusSTM.EditValue = ctlCusUNC.EditValue;
        }

        void ctlCustomerEdit1_EditValueChanged(object sender, EventArgs e)
        {
            LoadSoTien();
        }

        void cmbPTTT_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbPTTT.SelectedIndex)
            {
                case 0: 
                    this.Text = "Phiếu chi";
                    xtraTabControl1.SelectedTabPageIndex = 0;
                    itemPrintDetail.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    itemPrintGiayBaoNo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    itemPrintUNC.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;                    
                    break;
                case 1: 
                    this.Text = "Ủy nhiệm chi";
                    xtraTabControl1.SelectedTabPageIndex = 1;
                    itemPrintDetail.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    itemPrintGiayBaoNo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    itemPrintUNC.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;                    
                    break;
                case 2:
                    this.Text = "Séc chuyển khoản";
                    xtraTabControl1.SelectedTabPageIndex = 1;
                    itemPrintDetail.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    itemPrintGiayBaoNo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    itemPrintUNC.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;        
                    break;
                case 3:
                    this.Text = "Séc tiền mặt";
                    xtraTabControl1.SelectedTabPageIndex = 2;
                    itemPrintDetail.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    itemPrintGiayBaoNo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    itemPrintUNC.Visibility = DevExpress.XtraBars.BarItemVisibility.Never; 
                    break;
            }
        }

        void ctlLoaiTienEdit1_EditValueChanged(object sender, EventArgs e)
        {
          //  spinTyGia.EditValue = ctlLoaiTienEdit1.GetColumnValue("TyGia");
        }

        void frmEdit_Load(object sender, EventArgs e)
        {
            this.SDBID = BEE.THUVIEN.Common.Permission(barManager1, 121);
            lookNhanVienPhuTrach.Properties.DataSource = db.NhanViens.Select(p => new { p.HoTen, p.MaNV });
            lookLoaiTien.Properties.DataSource = db.DonViTinhs;
          //  db = new MasterDataContext();
            //ctlLoaiTienEdit1.LoadData();
            //ctlNhanVienEdit1.LoadData();
            //ctlTKNHEdit1.LoadData();
            //ctlTKNHEdit2.CreateColumns();
            //ctlTKNHEdit2.Properties.DataSource = ctlTKNHEdit1.Properties.DataSource;
            //ctlCusTienMat.LoadData();
            //ctlCusUNC.CreateColumns();
            //ctlCusUNC.Properties.DataSource = ctlCusTienMat.Properties.DataSource;
            //ctlCusSTM.CreateColumns();
            //ctlCusSTM.Properties.DataSource = ctlCusTienMat.Properties.DataSource;
          //  ctlMucThuChiItemEdit1.LoadData();
             lookLoaiPC.Properties.DataSource = db.mhpcLoaiChis.ToList();

            var objhd = db.cHopDongNhaThaus.Where(p => p.ContractID == MaHDB);


            if (this.ID != null)
            {
                PhieuChiLoad();
            }
            else
            {
                PhieuChiAddNew();

                //ctlNhanVienEdit1.EditValue = BEE.THUVIEN.Common.MaNV;
                //ctlLoaiTienEdit1.ItemIndex = 0;
                lookLoaiPC.ItemIndex = 0;

                if (this.MaHDB != null)
                {
                    this.MaKH = db.cHopDongNhaThaus.Where(p => p.ContractID == this.MaHDB).Select(p => p.CusID).FirstOrDefault();
                    this.objKH = db.KhachHangs.SingleOrDefault(p => p.MaKH == MaKH);
                    KhachHang_Load();
                    //colChungTu.Visible = false;
                    
                }
            }
        }

        void KhachHang_Load()
        {
            txtHoTenKH.EditValue = objKH.HoKH + " " + objKH.TenKH;
            this.MaKH = objKH.MaKH;
        }

        private void itemPrintMauA5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (this.ID == null) return;
            //using (var frm = new BEE.CONGCU.PrintForm())
            //{
            //    frm.PrintControl.Report = new rptDetailA5(this.ID);
            //    frm.ShowDialog();
            //}
        }

        private void txtHoTenKH_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                BEE.KHACHHANG.Find_frm frm = new BEE.KHACHHANG.Find_frm();
                frm.ShowDialog();
                if (frm.MaKH != 0)
                {
                    objKH = db.KhachHangs.Single(p => p.MaKH == frm.MaKH);
                    KhachHang_Load();

                    // CalculatorCK();
                }
            }
            else
            {
                if (e.Button.Index == 1)
                {
                    //KhachHang_AddNew();
                    //CustomerControl(false);
                    var f = new BEE.KHACHHANG.KhachHang_frm();
                    f.ShowDialog();
                    if (f.IsUpdate)
                    {
                        db = new MasterDataContext();
                        objKH = db.KhachHangs.Single(p => p.MaKH == f.MaKH);
                        KhachHang_Load();


                    }
                }
                else
                {
                    if (objKH != null)
                    {
                        var f = new BEE.KHACHHANG.KhachHang_frm();
                        f.MaKH = objKH.MaKH;
                        f.IsPersonal = objKH.IsPersonal.GetValueOrDefault();
                        f.ShowDialog();
                        if (f.IsUpdate)
                        {
                            db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, objKH);
                            objKH = db.KhachHangs.Single(p => p.MaKH == f.MaKH);
                            KhachHang_Load();
                        }
                    }
                }
            }
        }
    }
}