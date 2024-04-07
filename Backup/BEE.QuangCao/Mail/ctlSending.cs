using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BEE.ThuVien;
using DevExpress.XtraEditors;
using System.Threading;
using System.Data.Linq.SqlClient;
using BEEREMA;

namespace BEE.QuangCao.Mail
{
    public partial class ctlSending : XtraUserControl
    {
        MasterDataContext db = new MasterDataContext();

        public ctlSending()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateUserControl(this, barManager1);
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

        int GetAccessData()
        {
            it.AccessDataCls o = new it.AccessDataCls(BEE.ThuVien.Common.PerID, 92);

            return o.SDB.SDBID;
        }

        void Sending_Load()
        {
            var wait = DialogBox.WaitingForm();

            db = new MasterDataContext();
            DateTime tuNgay = itemTuNgay.EditValue != null ? (DateTime)itemTuNgay.EditValue : DateTime.Now.AddDays(-90);
            DateTime denNgay = itemDenNgay.EditValue != null ? (DateTime)itemDenNgay.EditValue : DateTime.Now;

            switch (GetAccessData())
            {
                case 1://Tat ca
                    gcSending.DataSource = db.mailSending_select(tuNgay, denNgay, 0, 0, 0);
                    break;
                case 2://Phong ban
                    gcSending.DataSource = db.mailSending_select(tuNgay, denNgay, 0, BEE.ThuVien.Common.DepartmentID, 0);
                    break;
                case 3://Nhom
                    gcSending.DataSource = db.mailSending_select(tuNgay, denNgay, BEE.ThuVien.Common.GroupID, 0, 0);
                    break;
                case 4://Nhan vien
                    gcSending.DataSource = db.mailSending_select(tuNgay, denNgay, 0, 0, BEE.ThuVien.Common.StaffID);
                    break;
                default:
                    gcSending.DataSource = null;
                    break;
            }

            grvSending.FocusedRowHandle = -1;

            wait.Close();
        }

        void Sending_Add()
        {
            frmSending frm = new frmSending();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
                Sending_Load();
        }

        void Sending_Edit()
        {
            int? sendID = (int?)grvSending.GetFocusedRowCellValue("SendID");
            if (sendID == null)
            {
                DialogBox.Error("Vui lòng chọn [Việc gửi], xin cảm ơn.");
                return;
            }
            frmSending frm = new frmSending();
            frm.SendID = sendID;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
                Sending_Load();
        }

        void Sending_Delete()
        {
            try
            {
                var indexs = grvSending.GetSelectedRows();
                if (indexs.Length <= 0)
                {
                    DialogBox.Error("Vui lòng chọn [Việc gửi], xin cảm ơn.");
                    return;
                }

                if (DialogBox.Question() == DialogResult.No) return;
                
                foreach (var i in indexs)
                {
                    var objSend = db.mailSendings.Single(p => p.SendID == (int?)grvSending.GetRowCellValue(i, "SendID"));
                    db.mailSendings.DeleteOnSubmit(objSend);
                }
                db.SubmitChanges();
                Sending_Load();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        void Sending_Focused()
        {
            int? sendID = (int?)grvSending.GetFocusedRowCellValue("SendID");
            if (sendID == null)
            {
                switch (tabMain.SelectedTabPageIndex)
                {
                    case 0: gcNotSend.DataSource = null; break;
                    case 1: gcSuccess.DataSource = null; break;
                    case 2: gcFails.DataSource = null; break;
                }
                return;
            }
            switch (tabMain.SelectedTabPageIndex)
            {
                case 0:
                    MailList_Load(sendID.Value, 1); 
                    break;
                case 1:
                    MailList_Load(sendID.Value, 2); 
                    break;
                case 2:
                    MailList_Load(sendID.Value, 3); 
                    break;
                case 3:
                    var objSend = db.mailSendings.Single(p => p.SendID == sendID);
                    htmlContent.DocumentText = objSend.Contents;
                    break;
            }
        }

        void MailList_Load(int sendID, byte status)
        {
            db = new MasterDataContext();
            var listMail = db.mailSendingLists.Where(p => p.SendID == sendID & p.Status == status)
                        .OrderByDescending(p => p.FullName)
                        .AsEnumerable()
                        .Select((p, index) => new
                        {
                            STT = index + 1,
                            p.ID,
                            p.ListID,
                            p.DateSend,
                            p.Vocative,
                            p.FullName,
                            p.BirthDate,
                            p.Phone,
                            p.Email,
                            p.HomeAddress,
                            p.JobTitle,
                            p.Department,
                            p.CompanyName,
                            p.BusinessAddress
                        }).ToList();
            switch (status)
            {
                case 1: gcNotSend.DataSource = listMail; break;
                case 2: gcSuccess.DataSource = listMail; break;
                case 3: gcFails.DataSource = listMail; break;
            }
        }

        void LoadPermission()
        {
            var ltAction = db.ActionDatas.Where(p => p.PerID == BEE.ThuVien.Common.PerID & p.FormID == 91).Select(p => p.FeatureID).ToList();
            btnThem.Enabled = ltAction.Contains(1);
            btnSua.Enabled = ltAction.Contains(2);
            btnXoa.Enabled = ltAction.Contains(3);
        }

        private void ctlSending_Load(object sender, EventArgs e)
        {
            LoadPermission();

            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);
            SetDate(4);

            Sending_Load();
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            Sending_Load();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            Sending_Load();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Sending_Load();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Sending_Add();
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Sending_Edit();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Sending_Delete();
        }

        private void itemAction_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = grvSending.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn [Việc gửi], xin cảm ơn.");
                return;
            }

            foreach (var i in indexs)
            {
                var objSend = db.mailSendings.Single(p => p.SendID == (int?)grvSending.GetRowCellValue(i, "SendID"));
                objSend.Active = (e.Item.Caption == "Thực hiện");
            }

            db.SubmitChanges();

            if (e.Item.Caption == "Tạm dừng")
            {
                e.Item.ImageIndex = 5;
                e.Item.Caption = "Thực hiện";
            }
            else
            {
                e.Item.ImageIndex = 4;
                e.Item.Caption = "Tạm dừng";
            }

            Sending_Load();
        }   

        private void grvSending_DoubleClick(object sender, EventArgs e)
        {
            Sending_Edit();
        }

        private void grvSending_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                Sending_Delete();
        }  

        private void grvSending_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Sending_Focused();

            var IsActive = (bool?)grvSending.GetFocusedRowCellValue("Active");
            if (IsActive.GetValueOrDefault())
            {
                itemAction.ImageIndex = 4;
                itemAction.Caption = "Tạm dừng";
            }
            else
            {
                itemAction.ImageIndex = 5;
                itemAction.Caption = "Thực hiện";
            }
        }

        private void tabMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            Sending_Focused();
        }

        private void itemExportDetail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvSending.FocusedRowHandle < 0)
            {
                DialogBox.Infomation("Vui lòng chọn [Việc gửi mail], xin cảm ơn.");
                return;
            }

            db = new MasterDataContext();
            var listMail = db.mailSendingLists.Where(p => p.SendID == (int?)grvSending.GetFocusedRowCellValue("SendID"))
                        .OrderByDescending(p => p.FullName)
                        .AsEnumerable()
                        .Select((p, index) => new
                        {
                            STT = index + 1,
                            p.ID,
                            p.ListID,
                            p.DateSend,
                            p.Vocative,
                            p.FullName,
                            p.BirthDate,
                            p.Phone,
                            p.Email,
                            p.HomeAddress,
                            p.JobTitle,
                            p.Department,
                            Status = p.Status == 1 ? "Chờ gửi" : (p.Status == 2 ? "Đã gửi" : "Không gửi được")
                        }).ToList();

            DateTime? tuNgay = (DateTime?)grvSending.GetFocusedRowCellValue("DateSend");
            var rpt = new Report.Mail.rptDetail(tuNgay, grvSending.GetFocusedRowCellValue("Title").ToString(), listMail);
            rpt.ShowPreviewDialog();
        }

        private void itemExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            it.CommonCls.ExportExcel(gcSending);
        }
    }
}