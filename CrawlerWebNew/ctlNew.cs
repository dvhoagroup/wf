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
using System.Threading;

namespace CrawlerWebNew
{
    public partial class ctlNew : DevExpress.XtraEditors.XtraUserControl
    {
        it.KyBaoCaoCls objKBC;
        MasterDataContext db;

        public ctlNew()
        {
            InitializeComponent();

            objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBC);
            db = new MasterDataContext();
        }

        void LoadPermission()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = Common.PerID;
            o.AccessData.Form.FormID = 182;
            DataTable tblAction = o.SelectBy();
            itemXoa.Enabled = false;
            btnXoa.Enabled = false;
            itemMG.Enabled = false;
            itemTinDangXL.Enabled = false;
            tinMGCanDuyet.Enabled = false;
            itemTinCCCanDuyet.Enabled = false;
            itemTinMG.Enabled = false;
            itemTinCC.Enabled = false;
            itemTinTrung.Enabled = false;
            itemChuyenVeTinMoi.Enabled = false;

            if (tblAction.Rows.Count > 0)
            {
                foreach (DataRow r in tblAction.Rows)
                {
                    switch (byte.Parse(r["FeatureID"].ToString()))
                    {
                        case 3:
                            itemXoa.Enabled = true;
                            btnXoa.Enabled = true;
                            break;
                        case 78:
                            itemTinDangXL.Enabled = true;
                            break;
                        case 79:
                            tinMGCanDuyet.Enabled = true;
                            break;
                        case 80:
                            itemTinCCCanDuyet.Enabled = true;
                            break;
                        case 81:
                            itemTinMG.Enabled = true;
                            break;
                        case 82:
                            itemTinCC.Enabled = true;
                            break;
                        case 83:
                            itemTinTrung.Enabled = true;
                          //  itemTinMG.Enabled = true;
                            break;
                        case 85:
                            itemChuyenVeTinMoi.Enabled = true;
                            break;
                    }
                }
            }
        }

        public void ThreadLoadData()
        {
            db = new MasterDataContext();
            var Web = (int?)itemWebSite.EditValue ?? 0;
            var Nhom = (short?)itemNhomTin.EditValue ?? 0;
            var TuNgay = (DateTime?)itemTuNgay.EditValue;
            var DenNgay = (DateTime?)itemDenNgay.EditValue;
            var ListHuyenQL = db.crlHuyenQuanLies.Where(p => p.MaNV == Common.StaffID).Select(p => p.MaHuyen).ToList();
            var ListChuyenMuc = db.crlNhanVien_HangMucTins.Where(p => p.MaNV == Common.StaffID).Select(p => p.MaHangMuc).ToList();
            if (Common.PerID != 1 && (ListChuyenMuc.Count == 0 || ListHuyenQL.ToList().Count == 0))
            {
                gcNew.DataSource = null;
                return;
            }
            if (itemTuNgay.EditValue != null && itemDenNgay.EditValue != null)
            {
                if (Common.PerID == 1)
                    gcNew.DataSource = db.crlNews.Where(p => SqlMethods.DateDiffDay(p.NgayDang, TuNgay) <= 0 && SqlMethods.DateDiffDay(p.NgayDang, DenNgay) >= 0 && (p.GroupID == Nhom || Nhom == 0) && (p.crlCategory.WebID == Web || Web == 0)).
                      Select(p => new
                      {
                          p.CategoryID,
                          HangMuc = p.CategoryID != null ? p.crlCategory.Name : "",
                          Nguon = p.CategoryID != null ? p.crlCategory.crlWebsite.Name : "",
                          Nhom = p.GroupID != null ? p.crlNewsGroup.Name : "",
                          p.DiaChi,
                          p.DiaChiKH,
                          p.DiDongKH,
                          p.DienThoaiKH,
                          p.DienTich,
                          p.ID,
                          p.KhachHang,
                          p.KhoangGia,
                          p.NgayDang,
                          p.NoiDung,
                          p.TieuDe,
                          p.Website,
                          p.MaTT,
                          p.DienThoaiCont,
                          TenHuyen = p.crlCategory.MaHuyen == null ? "" : p.crlCategory.Huyen.TenHuyen,
                          ChuyenMuc = p.crlCategory.MaHangMuc == null ? "" : p.crlCategory.crlHangMucTin.Name
                      }).OrderByDescending(p => p.NgayDang);
                else
                    gcNew.DataSource = db.crlNews.Where(p => ListChuyenMuc.Contains(p.crlCategory.MaHangMuc) == true && ListHuyenQL.Contains(p.crlCategory.MaHuyen) == true && SqlMethods.DateDiffDay(p.NgayDang, TuNgay) <= 0 && SqlMethods.DateDiffDay(p.NgayDang, DenNgay) >= 0 && (p.GroupID == Nhom || Nhom == 0) && (p.crlCategory.WebID == Web || Web == 0)).
                        Select(p => new
                        {
                            p.CategoryID,
                            HangMuc = p.CategoryID != null ? p.crlCategory.Name : "",
                            Nguon = p.CategoryID != null ? p.crlCategory.crlWebsite.Name : "",
                            Nhom = p.GroupID != null ? p.crlNewsGroup.Name : "",
                            p.DiaChi,
                            p.DiaChiKH,
                            p.DiDongKH,
                            p.DienThoaiKH,
                            p.DienTich,
                            p.ID,
                            p.KhachHang,
                            p.KhoangGia,
                            p.NgayDang,
                            p.NoiDung,
                            p.TieuDe,
                            p.Website,
                            TenHuyen = p.crlCategory.MaHuyen == null ? "" : p.crlCategory.Huyen.TenHuyen,
                            p.MaTT,
                            p.DienThoaiCont,
                            ChuyenMuc = p.crlCategory.MaHangMuc == null ? "" : p.crlCategory.crlHangMucTin.Name
                        }).OrderByDescending(p => p.NgayDang);
            }
            else
            {
                gcNew.DataSource = null;
            }
        }

        void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                ThreadLoadData();
            }
            finally
            {
                wait.Close();
            }
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            ComboBoxEdit cmd = (ComboBoxEdit)sender;
            objKBC.Index = cmd.SelectedIndex;
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemTuNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemTuNgay.EditValueChanged += new EventHandler(itemTuNgay_EditValueChanged);

            itemDenNgay.EditValue = objKBC.DateTo;
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] indexs = grvNew.GetSelectedRows();

            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn tin cần xóa");
                return;
            }

            if (DialogBox.Question() == DialogResult.No)
                return;

            foreach (int i in indexs)
            {
                var objDC = db.crlNews.Single(p => p.ID == (int)grvNew.GetRowCellValue(i, "ID"));
                db.crlNews.DeleteOnSubmit(objDC);
            }
            db.SubmitChanges();

            DialogBox.Infomation("Đã xóa những dòng đã chọn");

            LoadData();
        }

        private void ctlNew_Load(object sender, EventArgs e)
        {
            var ListWeb = db.crlWebsites.ToList();//.Add(new Library.crlWebsite() { });
            var obj = new crlWebsite() { ID = 0, Name = "[Tất cả]" };
            ListWeb.Add(obj);
            var ListGroup = db.crlNewsGroups.ToList();
            var obj2 = new crlNewsGroup() { ID = 0, Name = "[Tất cả]" };
            ListGroup.Add(obj2);
            lookNhomTin.DataSource = ListGroup;
            LookWeb.DataSource = ListWeb;
            itemWebSite.EditValue = 0;
            lookTT.DataSource = lookTrangThai.DataSource = db.crlNewsStatus;
            lookNV.DataSource = db.NhanViens.Select(p => new { p.MaNV, p.HoTen });
            LoadPermission();
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBC);
            SetDate(0);
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

        void DeleteData()
        {
            int[] indexs = grvNew.GetSelectedRows();

            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn tin cần xóa");
                return;
            }

            if (DialogBox.Question() == DialogResult.No)
                return;

            foreach (int i in indexs)
            {
                var objDC = db.crlNews.Single(p => p.ID == (int)grvNew.GetRowCellValue(i, "ID"));
                db.crlNews.DeleteOnSubmit(objDC);
            }
            db.SubmitChanges();

            DialogBox.Infomation("Đã xóa những dòng đã chọn");
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteData();
            LoadData();
        }

        private void SenToMG()
        {
            int[] indexs = grvNew.GetSelectedRows();

            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn tin cần đưa vào kho MG");
                return;
            }

            if (DialogBox.Question("Bạn có chắc muốn đưa số điện thoại của tin này vào kho [Môi Giới]") == DialogResult.No)
                return;
            var wait = DialogBox.WaitingForm();
            try
            {
                List<mglNguoiMoiGioi> ListMG = new List<mglNguoiMoiGioi>();
                var Now = db.GetSystemDate();
                var listNumber = db.mglNguoiMoiGiois.Select(p => p.SDT).ToList();
                foreach (var i in indexs)
                {
                    var Num1 = grvNew.GetRowCellValue(i, "DienThoaiKH");
                    var Num2 = grvNew.GetRowCellValue(i, "DiDongKH");
                    var Num3 = grvNew.GetRowCellValue(i, "DienThoaiCont");
                    if (Num1 != null && listNumber.Contains(Num1.ToString()) == false)
                    {
                        var obj = new mglNguoiMoiGioi();
                        obj.MaNV = Common.StaffID;
                        obj.NgayNhap = Now;
                        obj.SDT = Num1.ToString();
                        ListMG.Add(obj);
                    }
                    if (Num2 != null && listNumber.Contains(Num2.ToString()) == false)
                    {
                        var obj2 = new mglNguoiMoiGioi();
                        obj2.MaNV = Common.StaffID;
                        obj2.NgayNhap = Now;
                        obj2.SDT = Num1.ToString();
                        ListMG.Add(obj2);
                    }
                    if (Num3 != null && listNumber.Contains(Num3.ToString()) == false)
                    {
                        var obj = new mglNguoiMoiGioi();
                        obj.MaNV = Common.StaffID;
                        obj.NgayNhap = Now;
                        obj.SDT = Num3.ToString();
                        ListMG.Add(obj);
                    }
                }
                db.mglNguoiMoiGiois.InsertAllOnSubmit(ListMG);
                db.SubmitChanges();
                DialogBox.Infomation("Dữ liệu đã chuyển thành công!");
            }
            catch (Exception ex)
            {
                DialogBox.Error("Lỗi: " + ex.Message);
            }
            finally
            {
                wait.Close();
                wait.Dispose();
            }
        }

        private void itemMG_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SenToMG();
        }

        private void Process(byte? MaTT)
        {
            db = new MasterDataContext();
            int[] indexs = grvNew.GetSelectedRows();

            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn tin cần xử lý");
                return;
            }

            if (DialogBox.Question("Bạn có chắc muốn chuyển trạng thái của tin tức này?") == DialogResult.No)
                return;
            var wait = DialogBox.WaitingForm();
            var Now = db.GetSystemDate();
            var MaNV = Common.StaffID;
            try
            {
                int i = 0;
                while (i < indexs.Count())
                {
                    var obj = db.crlNews.FirstOrDefault(p => p.ID == (int?)grvNew.GetRowCellValue(indexs[i], "ID"));
                    if ((MaTT == 2 && (obj.MaTT > 1 && obj.MaTT < 7)) || (MaTT == 3 && obj.MaTT != 6) || (MaTT == 4 && obj.MaTT != 5) || ((MaTT == 5 || MaTT == 6) && obj.MaTT != 2))
                    {
                        i++;
                        return;
                    }
                    obj.MaTT = MaTT;
                    var objNote = new crlNewsNote();
                    objNote.MaTT = MaTT;
                    objNote.MaNV = MaNV;
                    objNote.NgayXL = Now;
                    objNote.NoiDung = "Chuyển trạng thái tin tức";
                    obj.crlNewsNotes.Add(objNote);
                    db.SubmitChanges();
                    i++;
                }
                DialogBox.Infomation("Dữ liệu đã được lưu!");
            }
            catch (Exception ex)
            {
                DialogBox.Error("Lỗi: " + ex.Message);
            }
            finally
            {
                wait.Close();
                wait.Dispose();
            }
           // gcNew.DataSource = null;
           // LoadData();
        }

        private void itemTinDangXL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Process(2);
        }

        private void tinMGCanDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Process(6);
        }

        private void itemTinCCCanDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Process(5);
        }

        private void itemTinMG_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Process(3);
            SenToMG();
            DeleteData();
        }

        private void TinMG_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Process(4);
        }

        private void grvNew_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvNew.FocusedRowHandle < 0)
            {
                gcNote.DataSource = null;
                return;
            }
            int? NewID = (int?)grvNew.GetFocusedRowCellValue("ID");
            gcNote.DataSource = db.crlNewsNotes.Where(p => p.NewsID == NewID).OrderByDescending(p => p.NgayXL);
        }

        private void itemTinTrung_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Process(7);
        }

        private void itemChuyenVeTinMoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Process(1);
        }
    }
}
