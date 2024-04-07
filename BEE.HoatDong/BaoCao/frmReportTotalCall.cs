using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEE.ThuVien;
using BEEREMA;
using System.Linq;
using System.IO;
using System.Diagnostics;

namespace BEE.HoatDong.BaoCao
{
    public partial class frmReportTotalCall : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public frmReportTotalCall()
        {
            InitializeComponent();
        }
        void LoadPermission()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = Common.PerID;
            o.AccessData.Form.FormID = 213;
            DataTable tblAction = o.SelectBy();
            itemExport.Enabled = false;

            if (tblAction.Rows.Count > 0)
            {
                foreach (DataRow r in tblAction.Rows)
                {
                    switch (byte.Parse(r["FeatureID"].ToString()))
                    {
                        case 13:
                            itemExport.Enabled = true;
                            break;
                    }
                }
            }
        }
        int GetAccessData()
        {
            it.AccessDataCls o = new it.AccessDataCls(Common.PerID, 213);

            return o.SDB.SDBID;
        }

        void SetDate(int index)
        {
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
        }

        private void frmReportTotalCall_Load(object sender, EventArgs e)
        {
            cboNhanVien.DataSource = db.NhanViens.Select(p => new { p.MaNV, p.HoTen });

            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);
            SetDate(4);
            LoadPermission();
            itemTuNgay.EditValue = new DateTime(2012, 5, 28);
            grvReportCall.BestFitColumns();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            db = new MasterDataContext();
            MuaThue_Load();
        }

        void MuaThue_Load()
        {
            var tuNgay = (DateTime?)itemTuNgay.EditValue ?? new DateTime(2012, 5, 28);
            var denNgay = (DateTime?)itemDenNgay.EditValue ?? DateTime.Now;
            var strMaNv = (itemNhanVien.EditValue ?? "").ToString().Replace(" ", "");
            var arrManv = "," + strMaNv + ",";
            int MaNV = Common.StaffID;
            var wait = DialogBox.WaitingForm();
            try
            {
                if (itemTuNgay.EditValue == null || itemDenNgay.EditValue == null)
                {
                    gcReportCall.DataSource = null;
                    return;
                }
                switch (GetAccessData())
                {
                    case 1://Tat ca
                        gcReportCall.DataSource = db.rpReportTotalTimeCall(tuNgay, denNgay, arrManv, -1, -1, -1);
                        break;
                    case 2://Theo phong ban 
                        gcReportCall.DataSource = db.rpReportTotalTimeCall(tuNgay, denNgay, arrManv, -1, -1, Common.DepartmentID);
                        break;
                    case 3://Theo nhom
                        gcReportCall.DataSource = db.rpReportTotalTimeCall(tuNgay, denNgay, arrManv, -1, Common.GroupID, -1);
                        break;
                    case 4://Theo nhan vien
                        gcReportCall.DataSource = db.rpReportTotalTimeCall(tuNgay, denNgay, arrManv, MaNV, -1, -1);
                        break;
                    default:
                        gcReportCall.DataSource = null;
                        break;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
            finally
            {
                wait.Close();
            }

        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var saveFile = new SaveFileDialog())
            {
                string filePath = $"Báo cáo tổng hợp cuộc gọi_{DateTime.Now.ToString("ddMMyyyyHHmm")}.xls";
                saveFile.Filter = $"Excel (2003)(.xls)|*.xls";
                saveFile.FileName = filePath;
                if (saveFile.ShowDialog() != DialogResult.Cancel)
                {
                    var fileExTen = new FileInfo(filePath).Extension;
                    switch (fileExTen)
                    {
                        case ".xls":
                            gcReportCall.ExportToXls(filePath);
                            break;
                    }

                    Process.Start(filePath);
                }
            }
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }
    }
}