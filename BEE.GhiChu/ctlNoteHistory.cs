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
using BEEREMA;

namespace BEE.GhiChu
{
    public partial class ctlNoteHistory : DevExpress.XtraEditors.XtraUserControl
    {
        public ctlNoteHistory()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateUserControl(this, barManager1);

            this.Dock = DockStyle.Fill;

            Permission();
        }

        public int? LinkID { get; set; }
        public int? FormID { get; set; }
        public int? MaNV { get; set; }
        byte SDBID = 6;

        void Permission()
        {
            try
            {
                using (var db = new MasterDataContext())
                {
                    var listAction = db.ActionDatas.Where(p => p.FormID == 104 & p.PerID == BEE.ThuVien.Common.PerID)
                        .Select(p => p.FeatureID).ToList();
                    itemThem.Enabled = listAction.Contains(1);
                    itemSua.Enabled = listAction.Contains(2);
                    itemXoa.Enabled = listAction.Contains(3);
                    var access = db.AccessDatas.Where(p => p.FormID == 104 & p.PerID == BEE.ThuVien.Common.PerID)
                        .Select(p => p.SDBID).ToList();
                    if (access.Count > 0)
                        this.SDBID = access[0];
                }
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        public void NoteHistory_Load()
        {
            using (var db = new MasterDataContext())
            {
                switch (this.SDBID)
                {
                    case 1://Tat ca
                        gcNotes.DataSource = db.NoteHistory_SelectByLink(FormID, LinkID, 0, 0, 0);
                        break;
                    case 2://Theo phong ban
                        gcNotes.DataSource = db.NoteHistory_SelectByLink(FormID, LinkID, BEE.ThuVien.Common.DepartmentID, 0, 0);
                        break;
                    case 3://Theo nhom
                        gcNotes.DataSource = db.NoteHistory_SelectByLink(FormID, LinkID, 0, BEE.ThuVien.Common.GroupID, 0);
                        break;
                    case 4://Theo nhan vien
                        gcNotes.DataSource = db.NoteHistory_SelectByLink(FormID, LinkID, 0, 0, BEE.ThuVien.Common.StaffID);
                        break;
                    default:
                        gcNotes.DataSource = null;
                        break;
                }
            }
        }

        public void NoteHistory_Remove()
        {
            gcNotes.DataSource = null;
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.MaNV != BEE.ThuVien.Common.StaffID) return;

            var frm = new frmEdit();
            frm.FormID = this.FormID;
            frm.LinkID = this.LinkID;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK) NoteHistory_Load();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)grvNotes.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn ghi chú");
                return;
            }
            var frm = new frmEdit();
            frm.ID = id;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK) NoteHistory_Load();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var indexs = grvNotes.GetSelectedRows();
                if (indexs.Length <= 0)
                {
                    DialogBox.Error("Vui lòng chọn ghi chú");
                    return;
                }
                if (DialogBox.Question() == DialogResult.No) return;

                using (var db = new MasterDataContext())
                {
                    foreach (var i in indexs)
                    {
                        var objDoc = db.NoteHistories.Single(p => p.ID == (int)grvNotes.GetRowCellValue(i, "ID"));
                        db.NoteHistories.DeleteOnSubmit(objDoc);
                    }
                    db.SubmitChanges();
                }
                NoteHistory_Load();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }
    }
}
