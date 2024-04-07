using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using BEE.ThuVien;

namespace BEE.ThuVien
{
    public partial class ctlManager : DevExpress.XtraEditors.XtraUserControl
    {
       MasterDataContext db = new MasterDataContext();
        bool IsLoaded = false;
        byte SDBID = 6;

        public ctlManager()
        {
            InitializeComponent();

            var ltDuAn = db.DuAn_getList().ToList();
            lookDuAn.DataSource = ltDuAn;

            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);
        }
                
        void LoadPermission()
        {
            try
            {
                using (var db = new MasterDataContext())
                {
                    var ltAction = db.ActionDatas.Where(p => p.PerID == ThuVien.Common.PerID & p.FormID == 153).Select(p => p.FeatureID).ToList();
                    //itemThem.Enabled = ltAction.Contains(1);
                    itemSua.Enabled = ltAction.Contains(2);
                    itemXoa.Enabled = ltAction.Contains(3);
                    itemIn.Enabled = ltAction.Contains(4);
                    
                    //
                    var ltAccess = db.AccessDatas.Where(p => p.PerID == ThuVien.Common.PerID & p.FormID == 30).Select(p => p.SDBID).ToList();
                    if (ltAccess.Count > 0)
                        this.SDBID = ltAccess[0];
                }
            }
            catch { }
        }

        void LoadData() { }

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

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            if(IsLoaded) LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (IsLoaded) LoadData();
        }

        private void itemDuAn_EditValueChanged(object sender, EventArgs e)
        {
            if (IsLoaded) LoadData();
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void ctlManager_Load(object sender, EventArgs e)
        {            
            SetDate(0);

            LoadData();
            IsLoaded = true;
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if(itemSua.Enabled) Edit();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Delete();
        }

        private void grvPhieuThu_KeyUp(object sender, KeyEventArgs e)
        {
            if (!itemXoa.Enabled) return;
            
            if (e.KeyCode == Keys.Delete)
            {
                //Delete();
            }
        }

        private void grvPhieuThu_DoubleClick(object sender, EventArgs e)
        {
            //if(itemSua.Enabled) Edit();
        }

        private void itemIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (grvPhieuTT.FocusedRowHandle < 0)
            //{
            //    DialogBox.Error("Vui lòng chọn [Phiếu xác nhận], xin cảm ơn.");
            //    return;
            //}

            //var rpt = new rptTemplate((int)grvPhieuTT.GetFocusedRowCellValue("MaPGC"));
            //rpt.ShowPreviewDialog();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //it.CommonCls.ExportExcel(gcPhieuTT);
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}
