using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using BEE.THUVIEN;using BEE;using BEE.DULIEU;
using BEEREM;

namespace BEE.SoQuy.PhieuChiBanHang
{
    public partial class ctlManager : BControl
    {
        MasterDataContext db = new MasterDataContext();

        public ctlManager()
        {
            InitializeComponent();

            var ltDuAn = db.DuAn_getList().ToList();
            lookDuAn.DataSource = ltDuAn;
            lookDuAn2.DataSource = ltDuAn;

            BEE.KyBaoCaoCls objKBC = new BEE.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);

            TranslateUserControl(this, barManager1);
        }

        void LoadPermission()
        {
            BEE.ActionDataCls o = new BEE.ActionDataCls();
            o.AccessData.Per.PerID = BEE.THUVIEN.Common.PerID;
            o.AccessData.Form.FormID = 34;
            DataTable tblAction = o.SelectBy();
            itemSua.Enabled = false;
            itemXoa.Enabled = false;
            itemIn.Enabled = false;
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
                        case 4:
                            itemIn.Enabled = true;
                            break;
                    }
                }
            }
        }

        int GetAccessData()
        {
            BEE.AccessDataCls o = new BEE.AccessDataCls(BEE.THUVIEN.Common.PerID, 27);

            return o.SDB.SDBID;
        }

        void PhieuChi_Load()
        {
            var wait = BEE.DialogBox.WaitingForm();

            try
            {
                DateTime tuNgay = itemTuNgay.EditValue != null ? (DateTime)itemTuNgay.EditValue : DateTime.Now.AddDays(-90);
                DateTime denNgay = itemDenNgay.EditValue != null ? (DateTime)itemDenNgay.EditValue : DateTime.Now;
                int maDA = itemDuAn.EditValue != null ? (int)itemDuAn.EditValue : -1;
                switch (GetAccessData())
                {
                    case 1://Tat ca
                        gcPhieuChi.DataSource = db.pgcPhieuChi_Select(tuNgay, denNgay, maDA, 0, 0, 0);
                        break;
                    case 2://Theo phong ban
                        gcPhieuChi.DataSource = db.pgcPhieuChi_Select(tuNgay, denNgay, maDA, (byte)BEE.THUVIEN.Common.MaPB, 0, 0);
                        break;
                    case 3://Theo nhom
                        gcPhieuChi.DataSource = db.pgcPhieuChi_Select(tuNgay, denNgay, maDA, 0, (byte)BEE.THUVIEN.Common.MaNKD, 0);
                        break;
                    case 4://Theo nhan vien
                        gcPhieuChi.DataSource = db.pgcPhieuChi_Select(tuNgay, denNgay, maDA, 0, 0, BEE.THUVIEN.Common.MaNV);
                        break;
                    default:
                        gcPhieuChi.DataSource = null;
                        break;
                }
            }
            catch { }

            wait.Close();
        }

        void PhieuChi_Edit()
        {
            int? maPC = (int?)grvPhieuChi.GetFocusedRowCellValue("MaPC");
            if (maPC == null)
            {
                BEE.DialogBox.Error("Vui lòng chọn phiếu chi");
                return;
            }

            frmEdit frm = new frmEdit();
            frm.MaPC = maPC;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
                PhieuChi_Load();
        }

        void PhieuChi_Delete()
        {
            var indexs = grvPhieuChi.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                BEE.DialogBox.Error("Vui lòng chọn phiếu chi");
                return;
            }
            if (BEE.DialogBox.Question() == DialogResult.No) return;
            foreach (var i in indexs)
            {
                pgcPhieuChi objPC = db.pgcPhieuChis.Single(p => p.MaPC == (int)grvPhieuChi.GetRowCellValue(i, "MaPC"));
                db.pgcPhieuChis.DeleteOnSubmit(objPC);
            }
            db.SubmitChanges();
            PhieuChi_Load();
        }

        void SetDate(int index)
        {
            BEE.KyBaoCaoCls objKBC = new BEE.KyBaoCaoCls();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValueChanged -= new EventHandler(itemDenNgay_EditValueChanged);
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
            itemTuNgay.EditValueChanged += new EventHandler(itemDenNgay_EditValueChanged);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            PhieuChi_Load();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            PhieuChi_Load();
        }

        private void itemDuAn_EditValueChanged(object sender, EventArgs e)
        {
            PhieuChi_Load();
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void ctlManager_Load(object sender, EventArgs e)
        {
            SetDate(0);
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PhieuChi_Load();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PhieuChi_Edit();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PhieuChi_Delete();
        }

        private void grvPhieuChi_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                PhieuChi_Delete();
            }
        }

        private void grvPhieuChi_DoubleClick(object sender, EventArgs e)
        {
            PhieuChi_Edit();
        }

        private void itemIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var indexs = grvPhieuChi.GetSelectedRows();
            //if (indexs.Length <= 0)
            //{
            //    BEE.DialogBox.Error("Vui lòng chọn phiếu chi");
            //    return;
            //}

            //var ltPhieuChi = new int[indexs.Length];
            //for (int i = 0; i < indexs.Length; i++)
            //    ltPhieuChi[i] = (int)grvPhieuChi.GetRowCellValue(indexs[i], "MaPC");

            //var rptPhieuChi = new rptDetail(ltPhieuChi);
            //rptPhieuChi.ShowPreviewDialog();
            if (grvPhieuChi.FocusedRowHandle < 0)
            {
                BEE.DialogBox.Infomation("Vui lòng chọn phiếu chi");
                return;
            }

            using (var rpt = new BEE.SoQuy.PhieuChiBanHang.rptDetail2(Convert.ToInt32(grvPhieuChi.GetFocusedRowCellValue("MaPC"))))
            {
                rpt.ShowPreviewDialog();
            }
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BEE.CommonCls.ExportExcel(gcPhieuChi);
        }
    }
}
