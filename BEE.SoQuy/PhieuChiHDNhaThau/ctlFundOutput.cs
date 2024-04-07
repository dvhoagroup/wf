using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using BEE.DULIEU;
using BEE.THUVIEN;

namespace BEE.SOQUY.PhieuChiHDNhaThau
{
    public partial class ctlFundOutput : BForm
    {
        public int? MaHDB { get; set; }

        private void Permission()
        {
            try
            {
                    var listAction = db.ActionDatas.Where(p => p.FormID == 107 & p.PerID == BEE.THUVIEN.Common.PerID).Select(p => p.FeatureID).ToList();
                    itemAdd.Enabled = listAction.Contains(1);
                    itemEdit.Enabled = listAction.Contains(2);
                    itemDelete.Enabled = listAction.Contains(3);
                    barPrint.Enabled = listAction.Contains(4);
                
            }
            catch
            {

            }
        }

        public void PhieuChi_Load()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                if (this.MaHDB == null)
                {
                    gcPhieuChi.DataSource = null;
                    gcChiTiet.DataSource = null;
                }
               
                    gcPhieuChi.DataSource = (from pc in db.cNTPhieuChis
                                             join ct in
                                                 (from ct in db.cNTpcChiTiets
                                                  where ct.MaHDB == this.MaHDB
                                                  group ct by ct.MaPC into gr
                                                  select new { MaPC = gr.Key, SoTien = gr.Sum(p => p.SoTien) })
                                                on pc.ID equals ct.MaPC
                                             join nv in db.NhanViens on pc.MaNV equals nv.MaNV
                                             orderby pc.NgayChi descending
                                             select new
                                             {
                                                 pc.ID,
                                                 pc.SoPC,
                                                 pc.NgayChi,
                                                 pc.NguoiNhan,
                                                 pc.MaNV,
                                                 SoTien = ct.SoTien * pc.TyGia,
                                                 pc.MaLT,
                                                 pc.LyDo,
                                                 pc.MaNVN,
                                                 pc.NgayNhap,
                                                 pc.MaNVS,
                                                 pc.NgaySua
                                             }).ToList();
                    if (grvPhieuChi.FocusedRowHandle == 0) grvPhieuChi.FocusedRowHandle = -1;
                
            }
            catch { }
            finally
            {
                wait.Close();
            }
        }

        private void PhieuChi_Add()
        {
            //using (var frm = new frmEdit())
            //{
            //    frm.MaHDB = this.MaHDB;
            //    frm.ShowDialog(this);
            //    if (frm.IsSave)
            //        PhieuChi_Load();
            //}
        }

        private void PhieuChi_Edit()
        {
            //var id = (int?)grvPhieuChi.GetFocusedRowCellValue("ID");
            //if (id == null)
            //{
            //    DialogBox.Error("Vui lòng chọn phiếu");
            //    return;
            //}
            //using (var frm = new frmEdit())
            //{
            //    frm.ID = id;
            //    frm.ShowDialog(this);
            //    if (frm.IsSave)
            //        PhieuChi_Load();
            //}
        }

        private void PhieuChi_Delete()
        {
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
                    var pc = db.cNTPhieuChis.Single(p => p.ID == (int)grvPhieuChi.GetRowCellValue(i, "ID"));
                    db.cNTPhieuChis.DeleteOnSubmit(pc);
                }
                db.SubmitChanges();
            }
            grvPhieuChi.DeleteSelectedRows();
        }

        private void PhieuChi_Click()
        {
            var id = (int?)grvPhieuChi.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                gcChiTiet.DataSource = null;
                return;
            }

            using (var db = new MasterDataContext())
            {
                gcChiTiet.DataSource = (from ct in db.cNTpcChiTiets
                                       // join mh in db.mhMuaHangs on ct.MaMH equals mh.ID into mhang
                                      //  from mh in mhang.DefaultIfEmpty()
                                        where ct.MaPC == id
                                        select new
                                        {
                                            ct.DienGiai,
                                            ct.SoTien,
                                          //  mh.SoMH,
                                          //  mh.NgayMH,
                                         //   mh.SoHD,
                                         //   mh.NgayHD,
                                        }).ToList();
            }
        }

        private void PhieuChi_Print()
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

        public ctlFundOutput()
        {
            InitializeComponent();

            BEE.NGONNGU.Language.TranslateUserControl(this, barManager1);

            this.Load += new EventHandler(ctlSales_Load);
            itemAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemAdd_ItemClick);
            itemEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemEdit_ItemClick);
            itemDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemDelete_ItemClick);
            itemPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemPrint_ItemClick);
            grvPhieuChi.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(grvPhieuChi_FocusedRowChanged);
            grvPhieuChi.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(grvPhieuChi_CustomDrawRowIndicator);
            grvChiTiet.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(grvChiTiet_CustomDrawRowIndicator);
        }
        
        void itemPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PhieuChi_Print();
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
            PhieuChi_Click();
        }

        void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PhieuChi_Delete();
        }

        void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PhieuChi_Edit();
        }

        void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PhieuChi_Add();
        }

        void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PhieuChi_Load();
        }

        void ctlSales_Load(object sender, EventArgs e)
        {
            try
            {
                using (var db = new MasterDataContext())
                {
                    lookNhanVien.DataSource = db.NhanViens.Select(p => new { p.MaNV, p.HoTen });
                    lookLoaiTien.DataSource = db.LoaiTiens.ToList();
                }

                Permission();
            }
            catch { }
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
    }
}
