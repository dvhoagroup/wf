using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Linq.SqlClient;
using BEE.ThuVien;
using DevExpress.XtraEditors;
using BEEREMA;

namespace BEE.QuangCao.SMS
{
    public partial class ctlGroupReceivesSMS : DevExpress.XtraEditors.XtraUserControl
    {
        MasterDataContext db = new MasterDataContext();
        public List<bdsSanPham> ListReminder;
        public bool IsRemider = false, IsCare = false;

        public ctlGroupReceivesSMS()
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
            it.AccessDataCls o = new it.AccessDataCls(BEE.ThuVien.Common.PerID, 87);

            return o.SDB.SDBID;
        }

        void GroupReceive_Load()
        {
            var wait = DialogBox.WaitingForm();
            DateTime tuNgay = itemTuNgay.EditValue != null ? (DateTime)itemTuNgay.EditValue : DateTime.Now.AddDays(-90);
            DateTime denNgay = itemDenNgay.EditValue != null ? (DateTime)itemDenNgay.EditValue : DateTime.Now;
            db = new MasterDataContext();
            switch (GetAccessData())
            {
                case 1://Tat ca
                    gcGroupReceive.DataSource = db.SMSGroupReceives_select(tuNgay, denNgay, 0, 0, 0);
                    break;
                case 2://Theo phong ban
                    gcGroupReceive.DataSource = db.SMSGroupReceives_select(tuNgay, denNgay, 0, BEE.ThuVien.Common.DepartmentID, 0);
                    break;
                case 3://Theo nhom
                    gcGroupReceive.DataSource = db.SMSGroupReceives_select(tuNgay, denNgay, BEE.ThuVien.Common.GroupID, 0, 0);
                    break;
                case 4://Theo nhan vien
                    gcGroupReceive.DataSource = db.SMSGroupReceives_select(tuNgay, denNgay, 0, 0, BEE.ThuVien.Common.StaffID);
                    break;
                default:
                    gcGroupReceive.DataSource = null;
                    break;
            }

            grvGroup.FocusedRowHandle = -1;
            wait.Close();
        }

        void GroupReceive_Add()
        {
            frmGroupReceive frm = new frmGroupReceive();
            frm.IsRemider = IsRemider;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
                GroupReceive_Load();
        }

        void GroupReceive_Edit()
        {
            int? groupID = (int?)grvGroup.GetFocusedRowCellValue("GroupID");
            if (groupID == null)
            {
                DialogBox.Error("Vui lòng chọn [Danh sách], xin cảm ơn.");
                return;
            }
            frmGroupReceive frm = new frmGroupReceive();
            frm.GroupID = groupID;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
                GroupReceive_Load();
        }

        void GroupReceive_Delete()
        {
            try
            {
                var indexs = grvGroup.GetSelectedRows();
                if (indexs.Length <= 0)
                {
                    DialogBox.Error("Vui lòng chọn [Danh sách], xin cảm ơn.");
                    return;
                }

                if (DialogBox.Question() == DialogResult.No) return;

                foreach (var i in indexs)
                {
                    var objGR = db.SMSGroupReceives.Single(p => p.GroupID == (int?)grvGroup.GetRowCellValue(i, "GroupID"));
                    db.SMSGroupReceives.DeleteOnSubmit(objGR);
                }

                db.SubmitChanges();

                GroupReceive_Load();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        void ListOfRecipient_Load()
        {
            int? groupID = (int?)grvGroup.GetFocusedRowCellValue("GroupID");
            if (groupID == null)
            {
                gcListOfRecipient.DataSource = null;
                return;
            }

            db = new MasterDataContext();
            gcListOfRecipient.DataSource = db.SMSListOfRecipients_getByGroupID(groupID);
        }

        void ListOfRecipient_Add()
        {
            int? groupID = (int?)grvGroup.GetFocusedRowCellValue("GroupID");
            if (groupID == null)
            {
                DialogBox.Error("Vui lòng chọn [Danh sách], xin cảm ơn.");
                return;
            }

            if (IsRemider)
            {
                frmConfirm f = new frmConfirm();
                f.parent = this.ParentForm;
                f.ShowDialog();
                if (f.DialogResult == DialogResult.OK)
                {
                    SaveListOfRecipients(groupID.Value);

                    ListOfRecipient_Load();
                }
            }
            else
            {
                frmSelectObject frm = new frmSelectObject();
                frm.GroupID = groupID.Value;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    ListOfRecipient_Load();
            }
        }

        void SaveListOfRecipients(int groupID)
        {
            if (IsCare)
            {
                SetHappyBirthday setHB = db.SetHappyBirthdays.Single(p => p.SetID == 6);
                //add CareCustomer
                CareCustomer objCare;
                foreach (bdsSanPham pgc in ListReminder)
                {
                    objCare = new CareCustomer();
                    objCare.CustomerID = pgc.MaKH;
                    objCare.CateID = 3;
                    objCare.Description = setHB.NoiDung;
                    objCare.StaffID = BEE.ThuVien.Common.StaffID;
                    objCare.StatusID = 1;
                    db.CareCustomers.InsertOnSubmit(objCare);
                    db.SubmitChanges();

                    pgc.MaDA = objCare.KeyID;//vay muon
                }
            }

            SMSListOfRecipient objLOR;
            foreach (bdsSanPham pgc in ListReminder)
            {
                objLOR = new SMSListOfRecipient();
                objLOR.GroupID = groupID;
                objLOR.CustomerID = pgc.MaKH;
                if (IsCare)
                    objLOR.CareID = pgc.MaDA;
                db.SMSListOfRecipients.InsertOnSubmit(objLOR);
            }

            db.SubmitChanges();
        }

        void ListOfRecipient_Delete()
        {
            try
            {
                var indexs = grvList.GetSelectedRows();
                if (indexs.Length <= 0)
                {
                    DialogBox.Error("Vui lòng chọn [Người nhận], xin cảm ơn.");
                    return;
                }

                foreach (var i in indexs)
                {
                    var objList = db.SMSListOfRecipients.Single(p => p.ListID == (int?)grvList.GetRowCellValue(i, "ListID"));
                    db.SMSListOfRecipients.DeleteOnSubmit(objList);
                }

                db.SubmitChanges();

                ListOfRecipient_Load();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        void LoadPermission()
        {
            var ltAction = db.ActionDatas.Where(p => p.PerID == BEE.ThuVien.Common.PerID & p.FormID == 87).Select(p => p.FeatureID).ToList();
            btnThem.Enabled = ltAction.Contains(1);
            btnSua.Enabled = ltAction.Contains(2);
            btnXoa.Enabled = ltAction.Contains(3);
        }

        private void ctlTemplates_Load(object sender, EventArgs e)
        {
            LoadPermission();

            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);
            SetDate(4);

            GroupReceive_Load();

            if (!IsRemider)
                itemClose.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            GroupReceive_Load();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            GroupReceive_Load();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GroupReceive_Load();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GroupReceive_Add();
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GroupReceive_Edit();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GroupReceive_Delete();
        }

        private void grvGroup_DoubleClick(object sender, EventArgs e)
        {
            GroupReceive_Edit();
        }

        private void grvGroup_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ListOfRecipient_Load();
        }

        private void itemListReceives_Add_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListOfRecipient_Add();
        }

        private void itemListReceives_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListOfRecipient_Delete();
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ParentForm.Close();
        }
    }
}