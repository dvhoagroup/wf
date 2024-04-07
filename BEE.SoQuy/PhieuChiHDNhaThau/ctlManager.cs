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
using BEE.DULIEU;
using BEE.THUVIEN;

namespace BEE.SoQuy.PhieuChiHDNhaThau
{
    public partial class ctlManager : DevExpress.XtraEditors.XtraUserControl
    {
        byte SDBID;

        private void SetDate(int index)
        {
            KyBaoCaoCls objKBC = new KyBaoCaoCls();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
        }
                
        private void PhieuChiLoad()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                var tuNgay = (DateTime?)itemTuNgay.EditValue;
                var denNgay = (DateTime?)itemDenNgay.EditValue;
                var strCongTy = (itemCongTy.EditValue ?? "").ToString().Replace(" ", "");
                var arrCongTy = "," + strCongTy + ",";
                short maPB = 0;short MaNKD = 0; int maNV = 0;
                switch (this.SDBID)
                {
                    case 2: maPB = BEE.THUVIEN.Common.MaPB; break;
                    case 3: MaNKD = BEE.THUVIEN.Common.MaNKD; break;
                    case 4: maNV = BEE.THUVIEN.Common.MaNV; break;
                }

                using (var db = new MasterDataContext())
                {
                    gcPhieuChi.DataSource = db.cTongHopCongNoPCNhaThau_Select(arrCongTy.ToString(), tuNgay, denNgay, maPB, MaNKD, maNV).ToList();
                    if (grvPhieuChi.FocusedRowHandle == 0) grvPhieuChi.FocusedRowHandle = -1;

                    //gcPhieuChi.DataSource = (from pc in db.cNTPhieuChis
                    //                         join hd in db.cHopDongNhaThaus on pc.ContractID equals hd.ContractID into hopdong
                    //                         from hd in hopdong.DefaultIfEmpty()
                    //                         join kh in db.KhachHangs on pc.MaKH equals kh.MaKH into khang
                    //                         from kh in khang.DefaultIfEmpty()
                    //                         join ltt in db.cLichThanhToanNhaThaus on pc.DotTT equals ltt.ID 
                                             
                    //                        // join l in db.mhpcLoaiChis on pc.MaLPC equals l.ID
                    //                        // join t in db.LoaiTiens on pc.MaLT equals t.MaLT
                    //                         join nv in db.NhanViens on pc.MaNVN equals nv.MaNV
                    //                         where SqlMethods.DateDiffDay(tuNgay, pc.NgayChi) >= 0 & SqlMethods.DateDiffDay(pc.NgayChi, denNgay) >= 0
                    //                            & (arrCongTy.Contains(nv.MaCT.ToString()) == true | strCongTy == "")
                    //                            & (nv.MaPB == maPB | maPB == 0) & (nv.MaNKD == MaNKD | MaNKD == 0) & (nv.MaNV == maNV | maNV == 0)
                    //                         orderby pc.NgayChi descending
                    //                         select new
                    //                         {
                    //                             pc.ID,
                    //                             pc.SoPC,
                    //                             pc.NgayChi,
                    //                             kh.TenCongTy,
                    //                             pc.NguoiNhan,
                    //                             hd.ContractNo,
                    //                             hd.DichVu,
                    //                             pc.MaNV,
                    //                            TienChi = pc.SoTien,
                    //                           //  SoTien = pc.mhpcChiTiets.Sum(p => (decimal?)p.SoTien) * pc.TyGia,
                    //                            // l.TenLPC,
                    //                            // t.TenVT,
                    //                             pc.LyDo,
                    //                             pc.MaNVN,
                    //                             pc.NgayNhap,
                    //                             pc.MaNVS,
                    //                             pc.NgaySua,
                    //                             ltt.DotThanhToan,
                    //                             ltt.ThanhTien,
                    //                             ConLai = ltt.ThanhTien - pc.SoTien

                    //                         }).ToList();
                   
                }
            }
            catch { }
            finally
            {
                wait.Close();
            }
        }

        private void PhieuChiAdd()
        {
            if (!itemAdd.Enabled) return;

            using (var frm = new BEE.SOQUY.PhieuChiHDNhaThau.frmEdit())
            {
                frm.ShowDialog(this);
                if (frm.IsSave) PhieuChiLoad();
            }
        }

        private void PhieuChiEdit()
        {
            if (!itemEdit.Enabled) return;

            var id = (int?)grvPhieuChi.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn phiếu");
                return;
            }
            using (var frm = new BEE.SOQUY.PhieuChiHDNhaThau.frmEdit())
            {
                frm.ID = id;
                frm.ShowDialog(this);
                if (frm.IsSave) PhieuChiLoad();
            }
        }

        private void PhieuChiDelete()
        {
            if (!itemDelete.Enabled) return;

            var indexs = grvPhieuChi.GetSelectedRows();
            if (indexs.Length < 0)
            {
                DialogBox.Error("Vui lòng chọn phiếu");
                return;
            }
            if (DialogBox.Question() == DialogResult.No) return;
            using (var db = new MasterDataContext())
            {
                foreach (var i in indexs)
                {
                    var pt = db.cNTPhieuChis.Single(p => p.ID == (int)grvPhieuChi.GetRowCellValue(i, "ID"));
                    db.cNTPhieuChis.DeleteOnSubmit(pt);
                }
                db.SubmitChanges();
            }
            grvPhieuChi.DeleteSelectedRows();
        }

        private void PhieuChiClick()
        {
            //var id = (int?)grvPhieuChi.GetFocusedRowCellValue("ID");
            //if (id == null)
            //{
            //    switch (xtraTabControl1.SelectedTabPageIndex)
            //    {
            //        case 0: gcChiTiet.DataSource = null; break;
            //    }
            //    return;
            //}

            //using (var db = new MasterDataContext())
            //{
            //    switch (xtraTabControl1.SelectedTabPageIndex)
            //    {
            //        case 0:
            //            gcChiTiet.DataSource = (from ct in db.mhpcChiTiets
            //                                    join mh in db.mhMuaHangs on ct.MaMH equals mh.ID into mhang
            //                                    from mh in mhang.DefaultIfEmpty()
            //                                    join hd in db.cContracts on ct.MaHDB equals hd.ContractID into hdong
            //                                    from hd in hdong.DefaultIfEmpty()
            //                                    where ct.MaPC == id
            //                                    select new
            //                                    {
            //                                        ct.DienGiai,
            //                                        ct.SoTien,
            //                                        mh.SoMH,
            //                                        mh.NgayMH,
            //                                        mh.SoHD,
            //                                        mh.NgayHD,
            //                                        NgayHopDong = hd.SigningDate,
            //                                        SoHopDong = hd.ContractNo,
            //                                        ct.MucThuChi.TenMTC
            //                                    }).ToList();
            //            break;
            //    }
            //}
        }

        public ctlManager()
        {
            InitializeComponent();

            BEE.NGONNGU.Language.TranslateUserControl(this, barManager1);

            this.Load += new EventHandler(ctlManager_Load);
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);
            itemDenNgay.EditValueChanged += new EventHandler(itemDenNgay_EditValueChanged);
            cmbKyBaoCao.EditValueChanged += new EventHandler(cmbKyBaoCao_EditValueChanged);
            itemRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemRefresh_ItemClick);
            itemAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemAdd_ItemClick);
            itemEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemEdit_ItemClick);
            itemDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemDelete_ItemClick);
            itemCongTy.EditValueChanged += new EventHandler(itemCongTy_EditValueChanged);

            grvPhieuChi.DoubleClick += new EventHandler(grvPhieuChi_DoubleClick);
            grvPhieuChi.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(grvPhieuChi_FocusedRowChanged);
            grvPhieuChi.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(grvPhieuChi_CustomDrawRowIndicator);
            grvChiTiet.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(grvChiTiet_CustomDrawRowIndicator);
        }

        void itemCongTy_EditValueChanged(object sender, EventArgs e)
        {
            PhieuChiLoad();
        }

        void grvPhieuChi_DoubleClick(object sender, EventArgs e)
        {
            PhieuChiEdit();
        }

        void grvChiTiet_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        void grvPhieuChi_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        void grvPhieuChi_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            PhieuChiClick();
        }

        void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PhieuChiDelete();
        }

        void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PhieuChiEdit();
        }

        void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PhieuChiAdd();
        }

        void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PhieuChiLoad();
        }

        void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            PhieuChiLoad();
        }

        void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            PhieuChiLoad();
        }

        void ctlManager_Load(object sender, EventArgs e)
        {
            this.SDBID = BEE.THUVIEN.Common.Permission(barManager1, 185);

            using (var db = new MasterDataContext())
            {
              //  lookLoaiTien.DataSource = db.LoaiTiens.Select(p => new { p.MaLT, p.TenVT }).ToList();
                lookNhanVien.DataSource = db.NhanViens.Select(p => new { p.MaNV, p.HoTen }).ToList();
                cmbCongTy.DataSource = db.CongTies.Select(p => new { p.ID, p.TenCT }).ToList();
            }

            if (BEE.THUVIEN.Common.getAccess(148) != 1)
            {
                itemCongTy.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                itemCongTy.EditValue = BEE.THUVIEN.Common.ComID;
            }

            KyBaoCaoCls objKBC = new KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);
            SetDate(0);
        }

        private void itemPhieuChi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var id = (int?)grvPhieuChi.GetFocusedRowCellValue("ID");
            //if (id == null)
            //{
            //    DialogBox.Error("Vui lòng chọn phiếu");
            //    return;
            //}

            //using (var frm = new BEE.CONGCU.PrintForm())
            //{
            //    frm.PrintControl.Report = new rptDetail(id);
            //    frm.ShowDialog();
            //}
        }

        private void itemGiayBaoNo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var id = (int?)grvPhieuChi.GetFocusedRowCellValue("ID");
            //if (id == null)
            //{
            //    DialogBox.Error("Vui lòng chọn phiếu");
            //    return;
            //}

            //using (var frm = new BEE.CONGCU.PrintForm())
            //{
            //    frm.PrintControl.Report = new rptGiayBaoNo(id);
            //    frm.ShowDialog();
            //}
        }

        private void itemUyNhiemChi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var id = (int?)grvPhieuChi.GetFocusedRowCellValue("ID");
            //if (id == null)
            //{
            //    DialogBox.Error("Vui lòng chọn phiếu");
            //    return;
            //}

            //using (var frm = new BEE.CONGCU.PrintForm())
            //{
            //    frm.PrintControl.Report = new rptUyNhiemChi(id);
            //    frm.ShowDialog();
            //}
        }

        private void itemPrintMauA5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var id = (int?)grvPhieuChi.GetFocusedRowCellValue("ID");
            //if (id == null)
            //{
            //    DialogBox.Error("Vui lòng chọn phiếu");
            //    return;
            //}

            //using (var frm = new BEE.CONGCU.PrintForm())
            //{
            //    frm.PrintControl.Report = new rptDetailA5(id);
            //    frm.ShowDialog();
            //}
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CommonCls.ExportExcel(gcPhieuChi);
        }
    }
}
