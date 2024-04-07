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

namespace BEE.HoatDong.MGL.XuLy
{
    public partial class ctlManager : DevExpress.XtraEditors.XtraUserControl
    {
        MasterDataContext db = new MasterDataContext();

        public ctlManager()
        {
            InitializeComponent();
        }

        void LoadPermission()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = Common.PerID;
            o.AccessData.Form.FormID = 174;
            DataTable tblAction = o.SelectBy();
            itemSua.Enabled = false;
            itemXoa.Enabled = false;
            itemExport.Enabled = false;
            itemUp.Enabled = false;
          //  itemGiaoDich.Enabled = false;

            if (tblAction.Rows.Count > 0)
            {
                foreach (DataRow r in tblAction.Rows)
                {
                    switch (byte.Parse(r["FeatureID"].ToString()))
                    {
                        case 2:
                            itemSua.Enabled = true;
                            break;
                        case 3:
                            itemXoa.Enabled = true;
                            break;
                        case 13:
                            itemExport.Enabled = true;
                            break;
                        case 76:
                            itemUp.Enabled = true;
                            break;
                    }
                }
            }
        }

        int GetAccessData()
        {
            it.AccessDataCls o = new it.AccessDataCls(Common.PerID, 178);

            return o.SDB.SDBID;
        }

        private string GetAdress(int? MaBC)
        {
            var obj = db.mglbcBanChoThues.First(p => p.MaBC == MaBC);
            var xa = obj.MaXa == null ? "" : db.Xas.FirstOrDefault(p => p.MaXa == obj.MaXa).TenXa;
            var huyen = obj.MaHuyen == null ? "" : db.Huyens.First(p => p.MaHuyen == obj.MaHuyen).TenHuyen;
            var tinh = obj.MaTinh == null ? "" : db.Tinhs.First(p => p.MaTinh == obj.MaTinh).TenTinh;
            string DiaChi = string.Format("{0} - {1} - {2} - {3} - {4}", obj.SoNha, obj.TenDuong, xa, huyen, tinh);
            return DiaChi;
        }

        void BaoCao_Load()
        {
            var tuNgay = (DateTime?)itemTuNgay.EditValue ?? DateTime.Now;
            var denNgay = (DateTime?)itemDenNgay.EditValue ?? DateTime.Now;
            int MaNV = Common.StaffID;
            var wait = DialogBox.WaitingForm();
            try
            {
                if (itemTuNgay.EditValue == null || itemDenNgay.EditValue == null)
                {
                    gcBaoCao.DataSource = null;
                    return;
                }
                db = new MasterDataContext();
                switch (GetAccessData())
                {
                    case 1://Tat ca
                        gcBaoCao.DataSource = db.mglBCCongViec_GetInfo(tuNgay, denNgay, -1, -1, -1, MaNV, true);
                        break;
                    case 2://Theo phong ban 
                        gcBaoCao.DataSource = db.mglBCCongViec_GetInfo(tuNgay, denNgay, -1, -1, Common.DepartmentID, MaNV, false);
                        break;
                    case 3://Theo nhom
                        gcBaoCao.DataSource = db.mglBCCongViec_GetInfo(tuNgay, denNgay, -1, Common.GroupID, -1, MaNV, false);
                        break;
                    case 4://Theo nhan vien
                        gcBaoCao.DataSource = db.mglBCCongViec_GetInfo(tuNgay, denNgay, MaNV, -1, -1, MaNV, false);
                        break;
                    default:
                        gcBaoCao.DataSource = null;
                        break;
                }

            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                wait.Close();
            }

        }

        private void BaoCao_Edit()
        {
            if (grvBaoCao.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn công việc cần chỉnh sửa!");
                return;
            }
            using (var frm = new frmEdit())
            {
                frm.MaCV = (int?)grvBaoCao.GetFocusedRowCellValue("ID");
                frm.ShowDialog();
            }
            
        }

        private void BaoCao_Delete()
        {
            var indexs = grvBaoCao.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn báo cáo!");
                return;
            }
            if (DialogBox.Question() == DialogResult.No) return;
            try
            {
                foreach (var i in indexs)
                {
                    var objBC = db.mglBCCongViecs.Single(p => p.ID == (int)grvBaoCao.GetRowCellValue(i, "ID"));
                    db.mglBCCongViecs.DeleteOnSubmit(objBC);
                }
                db.SubmitChanges();

            }
            catch (Exception ex)
            {
                DialogBox.Error("Dữ liệu đã tồn tại ràng buộc nên bạn không thể xóa!");
            }
            grvBaoCao.DeleteSelectedRows();
        }

        private void TabPage_Load()
        {
            try
            {
                int? MaBC = (int?)grvBaoCao.GetFocusedRowCellValue("ID");
                if (MaBC == null)
                {
                    gcSanPham.DataSource = null;
                    return;
                }

                gcSanPham.DataSource = null;
                gcXuLy.DataSource = null;
                switch (tabMain.SelectedTabPageIndex)
                {
                    case 0:
                        #region San pham
                        gcSanPham.DataSource = db.mglBCSanPhams.Where(p => p.MaCV == MaBC)
                            .Select(p => new
                            {
                                p.MaCV,
                                p.ID,
                                p.MaSP,
                                p.mglbcTrangThaiXL.TenTT,
                                KhachHang = p.mglbcBanChoThue.KhachHang.IsPersonal == true ? p.mglbcBanChoThue.KhachHang.HoKH + " " +
                                p.mglbcBanChoThue.KhachHang.TenKH : p.mglbcBanChoThue.KhachHang.TenCongTy,
                                p.mglbcBanChoThue.LoaiBD.TenLBDS,
                                DiaChi = GetAdress(p.MaSP),
                                p.mglbcBanChoThue.KhachHang.DiDong,
                                p.mglbcBanChoThue.KhachHang.DiDong2,
                                p.mglbcBanChoThue.KyHieu
                            });
                      //  var obj =from p in db.mglBCSanPhams
                        gvSanPham.SelectRow(0);

                        #endregion
                        break;
                    case 1:
                        ctlTaiLieu1.FormID = 178;
                        ctlTaiLieu1.LinkID = (int?)grvBaoCao.GetFocusedRowCellValue("ID");
                        ctlTaiLieu1.MaNV = Common.StaffID;
                        ctlTaiLieu1.TaiLieu_Load();
                        break;
                }
            }
            catch { }
        }

        private void ctlManager_Load(object sender, EventArgs e)
        {
            lookTrangThai.DataSource = db.mglmtTrangThais;
            lookLoaiBDS.DataSource = db.LoaiBDs;
            lookCapDo.DataSource = db.mglCapDos;
            lookNguon.DataSource = db.mglNguons;    
        //    gcXuLy.car

            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);
            SetDate(0);
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaoCao_Load();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaoCao_Edit();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaoCao_Delete();
        }

        private void grvMuaThue_DoubleClick(object sender, EventArgs e)
        {
            if (grvBaoCao.FocusedRowHandle < 0)
            {
                return;
            }
            BaoCao_Edit();
        }

        private void grvMuaThue_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                BaoCao_Delete();
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
            BaoCao_Load();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            BaoCao_Load();
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void grvMuaThue_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            TabPage_Load();
        }
       
        private void tabMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            TabPage_Load();
        }

        private void itemIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void itemSP_GiaoDich_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvBaoCao.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn nhu cầu");
                return;
            }

            if ((byte)grvBaoCao.GetFocusedRowCellValue("MaTT") > 2)
            {
                DialogBox.Error("Nhu cầu đã giao dịch hoặc đang khóa");
                return;
            }

            int maBC = (int)gvSanPham.GetFocusedRowCellValue("MaSP");
            int maMT = (int)grvBaoCao.GetFocusedRowCellValue("MaMT");
            int rowCount = db.mglgdGiaoDiches.Where(p => p.MaBC == maBC & p.MaMT == maMT).Count();
            if (rowCount > 0)
            {
                DialogBox.Error("Sản phẩm đã được giao dịch đang chờ duyệt");
                return;
            }
            GiaoDich.frmEdit frm = new BEE.HoatDong.MGL.GiaoDich.frmEdit();
            frm.MaMT = maMT;
            frm.MaBC = maBC;
            frm.ShowDialog();
        }

        private void itemUp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] indexs = grvBaoCao.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn sản phẩm");
                return;
            }
            foreach (var i in indexs)
            {
                if ((int)grvBaoCao.GetRowCellValue(i, "MaNV") == Common.StaffID)
                {
                    db.mglBCCongViecs.Single(p => p.ID == (int)grvBaoCao.GetRowCellValue(i, "ID")).NgayXuLy = DateTime.Now;
                }
            }
            db.SubmitChanges();

            BaoCao_Load();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            it.CommonCls.ExportExcel(gcBaoCao);
        }

        private void gvSanPham_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvSanPham.FocusedRowHandle < 0)
            {
                gcXuLy.DataSource = null;
                return;
            }

            gcXuLy.DataSource = null;
            var MaSP = (int?)gvSanPham.GetFocusedRowCellValue("ID");
            gcXuLy.DataSource = db.mglspNhatKyXuLies.Where(p => p.MaSP == MaSP).OrderByDescending(p=>p.NgayXL).Select(p => new
            {
                p.ID,
                p.NhanVien.HoTen,
                p.NoiDung,
                NgayXuLy=p.NgayXL,
                p.mglbcTrangThaiXL.TenTT

            });
         //   cardXuLy.CardCaptionFormat = "{" + cardXuLy.Columns[0].AbsoluteIndex + 1 + "}";
        }

        private void itemXuLySP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvSanPham.FocusedRowHandle < 0)
            {
                DialogBox.Infomation("Vui lòng chọn bất động sản để xử lý!");
                gvSanPham.Focus();
                return;
            }
            using (var frm = new frmXuLy() { maSP = (int?)gvSanPham.GetFocusedRowCellValue("ID") })
            {
                frm.ShowDialog();
            }
        }

        private void itemXoaSP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvSanPham.FocusedRowHandle < 0)
            {
                DialogBox.Infomation("Vui lòng chọn bất động sản để xóa!");
                gvSanPham.Focus();
                return;
            }
            try
            {
                var objdelete = db.mglBCSanPhams.FirstOrDefault(p => p.ID == (int?)gvSanPham.GetFocusedRowCellValue("ID"));
                db.mglBCSanPhams.DeleteOnSubmit(objdelete);
                db.SubmitChanges();
                DialogBox.Infomation("Dữ liệu đã được xóa!");
            }
            catch (Exception ex)
            {
                DialogBox.Error("Lỗi: " + ex.Message);
            }
        }

        private void itemGDSP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvSanPham.FocusedRowHandle < 0)
            {
                DialogBox.Infomation("Vui lòng chọn bất động sản để giao dịch!");
                gvSanPham.Focus();
                return;
            }
            var objSP = db.mglBCSanPhams.FirstOrDefault(p => p.ID == (int?)gvSanPham.GetFocusedRowCellValue("ID"));
            using (var frm = new MGL.GiaoDich.frmEdit())
            {
                int? maMT = objSP.mglBCCongViec.MaCoHoiMT;
                int? maBC = objSP.MaSP;
                int rowCount = db.mglgdGiaoDiches.Where(p => p.MaBC == maBC & p.MaMT == maMT & p.MaTT != 6).Count();
                if (rowCount > 0)
                {
                    DialogBox.Error("Sản phẩm đã được giao dịch đang chờ duyệt");
                    return;
                }
                frm.objCV = objSP.mglBCCongViec;
                frm.MaMT = (int)maMT;
                frm.MaBC = (int)maBC;
                frm.ShowDialog();
            }
        }

        private void gvSanPham_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

            //if (gvSanPham.FocusedRowHandle < 0)
            //{
            //    gcXuLy.DataSource = null;
            //    return;
            //}

            //gcXuLy.DataSource = null;
            //var MaSP = (int?)gvSanPham.GetFocusedRowCellValue("ID");
            //gcXuLy.DataSource = db.mglspNhatKyXuLies.Where(p => p.MaSP == MaSP).OrderByDescending(p => p.NgayXL).Select(p => new
            //{
            //    p.ID,
            //    p.NhanVien.HoTen,
            //    p.NoiDung,
            //    NgayXuLy = p.NgayXL,
            //    p.mglbcTrangThaiXL.TenTT

            //});
        }

        private void gvSanPham_Click(object sender, EventArgs e)
        {

            if (gvSanPham.FocusedRowHandle < 0)
            {
                gcXuLy.DataSource = null;
                return;
            }

            gcXuLy.DataSource = null;
            var MaSP = (int?)gvSanPham.GetFocusedRowCellValue("ID");
            gcXuLy.DataSource = db.mglspNhatKyXuLies.Where(p => p.MaSP == MaSP).OrderByDescending(p => p.NgayXL).Select(p => new
            {
                p.ID,
                p.NhanVien.HoTen,
                p.NoiDung,
                NgayXuLy = p.NgayXL,
                p.mglbcTrangThaiXL.TenTT

            });
        }
    }
}
