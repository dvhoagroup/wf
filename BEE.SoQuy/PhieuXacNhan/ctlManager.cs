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

namespace BEE.SoQuy.PhieuXacNhan
{
    public partial class ctlManager : DevExpress.XtraEditors.XtraUserControl
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
        }

        void LoadPermission()
        {
            BEE.ActionDataCls o = new BEE.ActionDataCls();
            o.AccessData.Per.PerID = BEE.THUVIEN.Common.PerID;
            o.AccessData.Form.FormID = 29;
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
            BEE.AccessDataCls o = new BEE.AccessDataCls(BEE.THUVIEN.Common.PerID, 29);

            return o.SDB.SDBID;
        }

        void LoadData()
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
                        gcPhieuTT.DataSource = db.pgcPhieuThanhToan_Select(tuNgay, denNgay, maDA, 0, 0, 0);
                        break;
                    case 2://Theo phong ban
                        gcPhieuTT.DataSource = db.pgcPhieuThanhToan_Select(tuNgay, denNgay, maDA, (byte)BEE.THUVIEN.Common.MaPB, 0, 0);
                        break;
                    case 3://Theo nhom
                        gcPhieuTT.DataSource = db.pgcPhieuThanhToan_Select(tuNgay, denNgay, maDA, 0, (byte)BEE.THUVIEN.Common.MaNKD, 0);
                        break;
                    case 4://Theo nhan vien
                        gcPhieuTT.DataSource = db.pgcPhieuThanhToan_Select(tuNgay, denNgay, maDA, 0, 0, BEE.THUVIEN.Common.MaNV);
                        break;
                    default:
                        gcPhieuTT.DataSource = null;
                        break;
                }
            }
            catch { }

            wait.Close();
        }

        void Edit()
        {
            if (grvPhieuTT.FocusedRowHandle < 0)
            {
                BEE.DialogBox.Error("Vui lòng chọn [Phiếu thanh toán], xin cảm ơn.");
                return;
            }

            frmEdit frm = new frmEdit();
            frm.MaPTT = (int)grvPhieuTT.GetFocusedRowCellValue("MaPTT");
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
                LoadData();
        }

        void Delete()
        {
            var indexs = grvPhieuTT.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                BEE.DialogBox.Error("Vui lòng chọn [Phiếu thanh toán], xin cảm ơn.");
                return;
            }
            if (BEE.DialogBox.Question() == DialogResult.No) return;
            foreach (var i in indexs)
            {
                pgcPhieuThu objPT = db.pgcPhieuThus.Single(p => p.MaPT == (int)grvPhieuTT.GetRowCellValue(i, "MaPTT"));
                db.pgcPhieuThus.DeleteOnSubmit(objPT);
            }
            db.SubmitChanges();
            LoadData();
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
            LoadData();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemDuAn_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
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
            LoadData();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Edit();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Delete();
        }

        private void grvPhieuThu_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                Delete();
            }
        }

        private void grvPhieuThu_DoubleClick(object sender, EventArgs e)
        {
            Edit();
        }

        private void itemIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvPhieuTT.FocusedRowHandle < 0)
            {
                BEE.DialogBox.Error("Vui lòng chọn [Phiếu thanh toán], xin cảm ơn.");
                return;
            }

            
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BEE.CommonCls.ExportExcel(gcPhieuTT);
        }
    }
}
