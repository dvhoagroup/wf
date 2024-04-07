using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using LandSoft.Library;
using System.Data.Linq.SqlClient;
namespace LandSoft.CongVan.Send
{
    public partial class ctlManager : DevExpress.XtraEditors.XtraUserControl
    {
        MasterDataContext db = new MasterDataContext();
        bool first = true;
        public ctlManager()
        {
            InitializeComponent();
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);
            itemThem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemThem_ItemClick);
            NguoiGui.DataSource = db.NhanViens;
            lookLoaiCV.DataSource = db.LoaiCongVans;
            lookTrinhTrang.DataSource = db.cvdTrinhTrangs;
            lookNhanVien1.DataSource = db.NhanViens;
            lookTrinhTrang1.DataSource = db.cvdTrinhTrangs;

            Permission();
        }

        void Permission()
        {
            try
            {
                var listAction = db.ActionDatas.Where(p => p.FormID == 134 & p.PerID == Properties.Settings.Default.PerID)
                    .Select(p => p.FeatureID).ToList();
                itemThem.Enabled = listAction.Contains(1);
                itemSua.Enabled = listAction.Contains(2);
                itemXoa.Enabled = listAction.Contains(3);
                itemExport.Enabled = listAction.Contains(46);
                itemIn.Enabled = listAction.Contains(4);
                itemProcess.Enabled = listAction.Contains(45);
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        void LoadData()
        {
            var wait = DialogBox.WaitingForm();
            db = new MasterDataContext();
            try
            {
                DateTime tuNgay = itemTuNgay.EditValue != null ? (DateTime)itemTuNgay.EditValue : DateTime.Now.AddDays(-90);
                DateTime denNgay = itemDenNgay.EditValue != null ? (DateTime)itemDenNgay.EditValue : DateTime.Now;
                int maDA = itemDuAn.EditValue != null ? (int)itemDuAn.EditValue : -1;
                switch (it.CommonCls.GetAccessData(Properties.Settings.Default.PerID, 134))
                {
                    case 1:
                        gcCongVan.DataSource = db.CongVanDi_getDate(tuNgay, denNgay, 0, 0, 0);
                        break;
                    case 2://Theo phong ban
                        gcCongVan.DataSource = db.CongVanDi_getDate(tuNgay, denNgay, Properties.Settings.Default.DepartmentID, 0, 0);
                        break;
                    case 3://Theo nhom
                        gcCongVan.DataSource = db.CongVanDi_getDate(tuNgay, denNgay, 0, Properties.Settings.Default.GroupID, 0);
                        break;
                    case 4://Theo nhan vien
                        gcCongVan.DataSource = db.CongVanDi_getDate(tuNgay, denNgay, 0, 0, Properties.Settings.Default.StaffID);
                        break;
                    default:
                        gcCongVan.DataSource = null;
                        break;
                }
            }
            catch { }
            finally
            {
                wait.Close(); 
                wait.Dispose();
            }
        }

        void Edit()
        {
            if (gvCongVan.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn [Công văn], xin cảm ơn");
                return;
            }

            frmEdit frm = new frmEdit();
            frm.ID = (int)gvCongVan.GetFocusedRowCellValue("ID");
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK) {
                int temp = gvCongVan.FocusedRowHandle;
                LoadData();
                Clicks();
                gvCongVan.FocusedRowHandle = temp; 
            }
        }

        void Delete()
        {
            var indexs = gvCongVan.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn [Công văn], xin cảm ơn");
                return;
            }
            if (DialogBox.Question() == DialogResult.No) return;
            using (var db = new MasterDataContext())
            {
                try
                {
                    foreach (var i in indexs)
                    {
                        var bg = db.CongVanDis.Single(p => p.ID == (int)gvCongVan.GetRowCellValue(i, "ID"));
                        db.CongVanDis.DeleteOnSubmit(bg);
                    }
                    db.SubmitChanges();  
                    LoadData();
                }
                catch {
                    DialogBox.Error("Không thể xóa [Công văn] này!");
                }
            }

        }

        void Clicks()
        {
            if (gvCongVan.FocusedRowHandle >= 0)
            {
                var MaCV = (int?)gvCongVan.GetFocusedRowCellValue("ID");
                switch  (xtraTabControl1.SelectedTabPageIndex)
                {
                    case 0:
                    gcNhatKy.DataSource = db.cvNhatKyXuLies.Where(p => p.MaCV == MaCV).OrderByDescending(p => p.NgayXL).AsEnumerable()
                        .Select((p, index) => new
                        {
                            STT = index + 1,
                            p.ID,
                            p.MaCV,
                            p.MaNV,
                            p.MaTT,
                            p.DienGiai,
                            p.NgayXL,
                            p.NhanVien,
                            p.TienDo
                        }).ToList();
                    break;
                    case 1:
                        ctlTaiLieu1.FormID = 134;
                        ctlTaiLieu1.LinkID = MaCV;
                        ctlTaiLieu1.MaNV = (int?)gvCongVan.GetFocusedRowCellValue("MaNV");
                        ctlTaiLieu1.TaiLieu_Load();
                    break;
                }
            }
            else
            {
                gcNhatKy.DataSource = null;
                ctlTaiLieu1.TaiLieu_Remove();
            }
        }

        void SetDate(int index)
        {
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemDenNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemDenNgay_EditValueChanged);
        }
        void Export()
        {
            var frm = new System.Windows.Forms.SaveFileDialog();
            frm.Filter = "Excel|*.xls";
            frm.FileName = "TK Danh sach cong van di -- " + DateTime.Now.ToLongDateString();
            if(frm.ShowDialog() == DialogResult.OK)            
            {
                 gcCongVan.ExportToXls(frm.FileName);
                
                if (DialogBox.Question("Đã xử lý xong, bạn có muốn xem lại không?") == System.Windows.Forms.DialogResult.Yes)
                    System.Diagnostics.Process.Start(frm.FileName);
            }
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            if(!first) LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (!first) LoadData();
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void ctlManager_Load(object sender, EventArgs e)
        {
            SetDate(0);

            LoadData();

            first = false;
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }
        void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new frmEdit();
            frm.ShowDialog();
            frm.ID = null;
            if (frm.DialogResult == DialogResult.OK)
            {
                LoadData();
                
            }
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Edit();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Delete();
        }

        private void gvCongVan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                Delete();
            }
        }

        private void gvCongVan_DoubleClick(object sender, EventArgs e)
        {
            Edit();
        }

        private void itemIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = gvCongVan.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn [Công văn], xin cảm ơn");
                return;
            }

            var ltPhieuThu = new int[indexs.Length];
            for (int i = 0; i < indexs.Length; i++)
                ltPhieuThu[i] = (int)gvCongVan.GetRowCellValue(indexs[i], "ID");

            //var rptPhieuThu = new rptDetail(ltPhieuThu);
            //rptPhieuThu.ShowPreviewDialog();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Export();
        }


        private void gvCongVan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Clicks();
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            Clicks();
        }

        private void itemProcess_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvCongVan.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn [Công văn], xin cảm ơn");
                return;
            }

            var frm = new frmProcess();
            frm.ID = (int)gvCongVan.GetFocusedRowCellValue("ID");
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                int temp = gvCongVan.FocusedRowHandle;
                LoadData();
                gvCongVan.FocusedRowHandle = temp;
                Clicks();
            }
        }

        private void gvNhatKy_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            //if (e.Column.Caption == "F")
            //{
            //    var wait = DialogBox.WaitingForm();
            //    try
            //    {
            //        if (gvNhatKy.GetFocusedRowCellValue("FileAttach").ToString() == "") return;
            //        var frm = new FTP.frmDownloadFile();
            //        frm.FileName = gvNhatKy.GetFocusedRowCellValue("FileAttach").ToString();
            //        frm.ShowDialog();
            //    }
            //    catch { }
            //    finally
            //    {
            //        wait.Close();
            //        wait.Dispose();
            //    }
            //}
        }

        private void gcCongVan_DoubleClick(object sender, EventArgs e)
        {
            if (!itemSua.Enabled) return;
            Edit();
        }
    }
}
