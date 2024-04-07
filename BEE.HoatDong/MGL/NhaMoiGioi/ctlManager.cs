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

namespace BEE.HoatDong.MGL.NguoiMG
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
            o.AccessData.Form.FormID = 176;
            DataTable tblAction = o.SelectBy();
            itmThem.Enabled = false;
            itemSua.Enabled = false;
            itemXoa.Enabled = false;
            itemExport.Enabled = false;

            if (tblAction.Rows.Count > 0)
            {
                foreach (DataRow r in tblAction.Rows)
                {
                    switch (byte.Parse(r["FeatureID"].ToString()))
                    {
                        case 1:
                            itmThem.Enabled = true;
                            break;
                        case 2:
                            itemSua.Enabled = true;
                            break;
                        case 3:
                            itemXoa.Enabled = true;
                            break;
                        case 13:
                            itemExport.Enabled = true;
                            break;
                    }
                }
            }
        }

        int GetAccessData()
        {
            it.AccessDataCls o = new it.AccessDataCls(Common.PerID, 176);

            return o.SDB.SDBID;
        }

        void MoiGioi_Load()
        {
            var tuNgay = (DateTime?)itemTuNgay.EditValue ?? DateTime.Now;
            var denNgay = (DateTime?)itemDenNgay.EditValue ?? DateTime.Now;
            int MaNV = Common.StaffID;
            var wait = DialogBox.WaitingForm();
            try
            {
                if (itemTuNgay.EditValue == null || itemDenNgay.EditValue == null)
                {
                    gcNguoiMG.DataSource = null;
                    return;
                }
                gcNguoiMG.DataSource = db.mglNguoiMoiGiois.Where(p => SqlMethods.DateDiffDay(tuNgay, p.NgayNhap) >= 0 && SqlMethods.DateDiffDay(p.NgayNhap, denNgay) >= 0);

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

        private void MoiGioi_Edit()
        {
            using (var frm = new frmEdit())
            {
                frm.MaMG = (int)grvNguoiMG.GetFocusedRowCellValue("ID");
                frm.ShowDialog();
                if (frm.IsSave)
                {
                    MoiGioi_Load();
                }
            }

        }

        private void MuaThue_Delete()
        {
            var indexs = grvNguoiMG.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn nhà môi giới");
                return;
            }
            if (DialogBox.Question() == DialogResult.No) return;
            foreach (var i in indexs)
            {
                mglNguoiMoiGioi objMT = db.mglNguoiMoiGiois.Single(p => p.ID == (int)grvNguoiMG.GetRowCellValue(i, "ID"));
                db.mglNguoiMoiGiois.DeleteOnSubmit(objMT);
            }
            db.SubmitChanges();

            grvNguoiMG.DeleteSelectedRows();
        }

        private void ctlManager_Load(object sender, EventArgs e)
        {
            lookTrangThai.DataSource = db.mglmtTrangThais;
            lookNV.DataSource = db.NhanViens.Select(p => new { p.MaNV, p.HoTen });

            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);
            SetDate(0);
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            db = new MasterDataContext();
            MoiGioi_Load();
        }

        private void itmThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmEdit())
            {
                frm.ShowDialog();
                if (frm.IsSave)
                    MoiGioi_Load();
            }
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvNguoiMG.FocusedRowHandle < 0)
            {
                DialogBox.Error("Vui lòng chọn phiếu đăng ký");
                return;
            }
            MoiGioi_Edit();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MuaThue_Delete();
        }

        private void grvMuaThue_DoubleClick(object sender, EventArgs e)
        {
            if (grvNguoiMG.FocusedRowHandle < 0)
            {
                return;
            }

            MoiGioi_Edit();
        }

        private void grvMuaThue_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                MuaThue_Delete();
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
            MoiGioi_Load();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            MoiGioi_Load();
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            it.CommonCls.ExportExcel(gcNguoiMG);
        }

        private void itemImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmImport())
            {
                frm.ShowDialog();
            }
        }
    }
}
