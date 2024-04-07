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

namespace BEE.QuangCao.Mail
{
    public partial class ctlReceive : XtraUserControl
    {
        MasterDataContext db = new MasterDataContext();
        public bool IsReminder = false, IsCare = false;
        public List<bdsSanPham> ListReminder;

        public ctlReceive()
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

        void Receive_Load()
        {
            var wait = DialogBox.WaitingForm();

            db = new MasterDataContext();
            DateTime tuNgay = itemTuNgay.EditValue != null ? (DateTime)itemTuNgay.EditValue : DateTime.Now.AddDays(-90);
            DateTime denNgay = itemDenNgay.EditValue != null ? (DateTime)itemDenNgay.EditValue : DateTime.Now;

            switch (GetAccessData())
            {
                case 1://Tat ca
                    gcReceive.DataSource = db.mailReceive_select(tuNgay, denNgay, 0, 0, 0);
                    break;
                case 2://Theo phong ban
                    gcReceive.DataSource = db.mailReceive_select(tuNgay, denNgay, 0, BEE.ThuVien.Common.DepartmentID, 0);
                    break;
                case 3://Theo nhom
                    gcReceive.DataSource = db.mailReceive_select(tuNgay, denNgay, BEE.ThuVien.Common.GroupID, 0, 0);
                    break;
                case 4://Theo nhan vien
                    gcReceive.DataSource = db.mailReceive_select(tuNgay, denNgay, 0, 0, BEE.ThuVien.Common.StaffID);
                    break;
                default:
                    gcReceive.DataSource = null;
                    break;
            }

            grvReceive.FocusedRowHandle = -1;

            wait.Close();
        }

        void Receive_Add()
        {
            frmReceive frm = new frmReceive();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
                Receive_Load();
        }

        void Receive_Edit()
        {
            int? receID = (int?)grvReceive.GetFocusedRowCellValue("ReceID");
            if (receID == null)
            {
                DialogBox.Error("Vui lòng chọn [Danh sách], xin cảm ơn.");
                return;
            }
            frmReceive frm = new frmReceive();
            frm.ReceID = receID;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
                Receive_Load();
        }

        void Receive_Delete()
        {
            try
            {
                var indexs = grvReceive.GetSelectedRows();
                if (indexs.Length <= 0)
                {
                    DialogBox.Error("Vui lòng chọn [Danh sách], xin cảm ơn.");
                    return;
                }

                if (DialogBox.Question() == DialogResult.No) return;

                //db = new MasterDataContext();
                foreach (var i in indexs)
                {
                    var objRece = db.mailReceives.Single(p => p.ReceID == (int?)grvReceive.GetRowCellValue(i, "ReceID"));
                    db.mailReceives.DeleteOnSubmit(objRece);
                }

                db.SubmitChanges();

                Receive_Load();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        void ReceiveList_Load()
        {
            int? receID = (int?)grvReceive.GetFocusedRowCellValue("ReceID");
            if (receID == null)
            {
                gcList.DataSource = null;
                return;
            }
            db = new MasterDataContext();
            gcList.DataSource = db.mailReceiveList_Select(receID);
        }

        void ReceiveList_Add()
        {
            int? receID = (int?)grvReceive.GetFocusedRowCellValue("ReceID");
            if (receID == null)
            {
                DialogBox.Error("Vui lòng chọn [Danh sách], xin cảm ơn.");
                return;
            }
            if (IsReminder)
            {
                frmConfirm f = new frmConfirm();
                f.parent = this.ParentForm;
                f.GroupID = receID.Value;
                f.ShowDialog();
                if (f.DialogResult == DialogResult.OK)
                {
                    SaveListOfRecipients(receID.Value);
                }
                ReceiveList_Load();
            }
            else
            {
                frmReceiveList frm = new frmReceiveList();
                frm.ReceID = receID.Value;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                    ReceiveList_Load();
            }
        }

        void SaveListOfRecipients(int groupID)
        {
            if (IsCare)
            {
                SetHappyBirthday setHB = db.SetHappyBirthdays.Single(p => p.SetID == 5);
                //add CareCustomer
                CareCustomer objCare;
                foreach (bdsSanPham pgc in ListReminder)
                {
                    objCare = new CareCustomer();
                    objCare.CustomerID = pgc.MaKH;
                    objCare.CateID = 2;//Gui mail
                    objCare.Description = setHB.NoiDung;
                    objCare.StaffID = BEE.ThuVien.Common.StaffID;
                    objCare.StatusID = 1;
                    db.CareCustomers.InsertOnSubmit(objCare);
                    db.SubmitChanges();

                    pgc.MaDA = objCare.KeyID;//vay muon
                }
            }

            mailReceiveList objLOR;
            foreach (bdsSanPham pgc in ListReminder)
            {
                objLOR = new mailReceiveList();
                objLOR.ReceID = groupID;
                objLOR.CusID = pgc.MaKH;
                if (IsCare)
                    objLOR.CareID = pgc.MaDA;
                db.mailReceiveLists.InsertOnSubmit(objLOR);
            }

            db.SubmitChanges();
        }

        void ReceiveList_Delete()
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
                    var objList = db.mailReceiveLists.Single(p => p.ListID == (int?)grvList.GetRowCellValue(i, "ListID"));
                    db.mailReceiveLists.DeleteOnSubmit(objList);
                }

                db.SubmitChanges();

                ReceiveList_Load();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        void LoadPermission()
        {
            var ltAction = db.ActionDatas.Where(p => p.PerID == BEE.ThuVien.Common.PerID & p.FormID == 92).Select(p => p.FeatureID).ToList();
            btnThem.Enabled = ltAction.Contains(1);
            btnSua.Enabled = ltAction.Contains(2);
            btnXoa.Enabled = ltAction.Contains(3);
        }

        private void ctlReceive_Load(object sender, EventArgs e)
        {
            LoadPermission();

            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);
            SetDate(4);

            if (!IsReminder)
                itemClose.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            Receive_Load();
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            Receive_Load();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            Receive_Load();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Receive_Load();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Receive_Add();
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Receive_Edit();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Receive_Delete();
        }

        private void grvReceive_DoubleClick(object sender, EventArgs e)
        {
            Receive_Edit();
        }

        private void grvReceive_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ReceiveList_Load();
        }

        private void grvReceive_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                Receive_Delete();
        }

        private void itemReceiveList_Add_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ReceiveList_Add();
        }

        private void itemReceiveList_Delete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ReceiveList_Delete();
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ParentForm.Close();
        }
    }
}