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

namespace BEE.TaiLieu
{
    public partial class ctlTaiLieu : DevExpress.XtraEditors.XtraUserControl
    {
        public ctlTaiLieu()
        {
            InitializeComponent();
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
                    var listAction = db.ActionDatas.Where(p => p.FormID == 99 & p.PerID == BEE.ThuVien.Common.PerID)
                        .Select(p => p.FeatureID).ToList();
                    itemThem.Enabled = listAction.Contains(1);
                    itemSua.Enabled = listAction.Contains(2);
                    itemXoa.Enabled = listAction.Contains(3);
                    itemXem.Enabled = listAction.Contains(34);
                    itemTaiVe.Enabled = listAction.Contains(38);
                    var access = db.AccessDatas.Where(p => p.FormID == 99 & p.PerID == BEE.ThuVien.Common.PerID)
                        .Select(p => p.SDBID).ToList();
                    if (access.Count > 0)
                        this.SDBID = access[0];
                }
            }
            catch
            { }
        }

        public void TaiLieu_Load()
        {
            using (var db = new MasterDataContext())
            {
                switch (this.SDBID)
                {
                    case 1://Tat ca
                        gcTaiLieu.DataSource = db.docTaiLieu_SelectByLink(FormID, LinkID, 0, 0, 0);
                        break;
                    case 2://Theo phong ban
                        gcTaiLieu.DataSource = db.docTaiLieu_SelectByLink(FormID, LinkID, BEE.ThuVien.Common.DepartmentID, 0, 0);
                        break;
                    case 3://Theo nhom
                        gcTaiLieu.DataSource = db.docTaiLieu_SelectByLink(FormID, LinkID, 0, BEE.ThuVien.Common.GroupID, 0);
                        break;
                    case 4://Theo nhan vien
                        gcTaiLieu.DataSource = db.docTaiLieu_SelectByLink(FormID, LinkID, 0, 0, BEE.ThuVien.Common.StaffID);
                        break;
                    default:
                        gcTaiLieu.DataSource = null;
                        break;
                }
            }
        }

        public void TaiLieu_Remove()
        {
            gcTaiLieu.DataSource = null;
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            it.AccessDataCls o = new it.AccessDataCls(Common.PerID, 191);

            var per = o.SDB.SDBID;
            switch (per)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    var frm = new frmEdit();
                    frm.FormID = this.FormID;
                    frm.LinkID = this.LinkID;
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK) TaiLieu_Load();
                    break;
                default:
                    DialogBox.Infomation("[Hợp đồng] này không do bạn quản lý.\r\nVui lòng kiểm tra lại, xin cảm ơn.");
                    break;
            }
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.MaNV == BEE.ThuVien.Common.StaffID | BEE.ThuVien.Common.PerID == 1)
            {
                var maTL = (int?)grvTaiLieu.GetFocusedRowCellValue("MaTL");
                if (maTL == null)
                {
                    DialogBox.Error("Vui lòng chọn [Tài liệu], xin cảm ơn.");
                    return;
                }
                var frm = new frmEdit();
                frm.MaTL = maTL;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK) TaiLieu_Load();
            }
            else
            {
                DialogBox.Infomation("[Hợp đồng] này không do bạn quản lý.\r\nVui lòng kiểm tra lại, xin cảm ơn.");
            }
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.MaNV == BEE.ThuVien.Common.StaffID | BEE.ThuVien.Common.PerID == 1)
            {
                try
                {
                    var indexs = grvTaiLieu.GetSelectedRows();
                    if (indexs.Length <= 0)
                    {
                        DialogBox.Error("Vui lòng chọn [Tài liệu], xin cảm ơn.");
                        return;
                    }
                    if (DialogBox.Question() == DialogResult.No) return;
                    List<string> files = new List<string>();
                    using (var db = new MasterDataContext())
                    {
                        foreach (var i in indexs)
                        {
                            var objDoc = db.docTaiLieus.Single(p => p.MaTL == (int)grvTaiLieu.GetRowCellValue(i, "MaTL"));
                            if (objDoc.DuongDan != null)
                                files.Add(objDoc.DuongDan);
                            db.docTaiLieus.DeleteOnSubmit(objDoc);
                        }
                        db.SubmitChanges();
                    }

                    var cmd = new FTP.FtpClient();
                    foreach (var url in files)
                    {
                        cmd.Url = url;
                        try
                        {
                            cmd.DeleteFile();
                        }
                        catch { }
                    }

                    TaiLieu_Load();
                }
                catch (Exception ex)
                {
                    DialogBox.Error(ex.Message);
                }
            }
            else
            {
                DialogBox.Infomation("[Hợp đồng] này không do bạn quản lý.\r\nVui lòng kiểm tra lại, xin cảm ơn.");
            }
        }

        private void itemXem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvTaiLieu.GetFocusedRowCellValue("DuongDan") == null) return;
            var frm = new FTP.frmDownloadFile();
            frm.FileName = grvTaiLieu.GetFocusedRowCellValue("DuongDan").ToString();
            frm.ShowDialog();
        }

        private void itemTaiVe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvTaiLieu.GetFocusedRowCellValue("DuongDan") == null) return;
            var frm = new FTP.frmDownloadFile();
            frm.FileName = grvTaiLieu.GetFocusedRowCellValue("DuongDan").ToString();
            if (frm.SaveAs())
                frm.ShowDialog();
        }

        private void ctlTaiLieu_Load(object sender, EventArgs e)
        {
            BEE.NgonNgu.Language.TranslateUserControl(this, barManager1);

            Permission();
        }
    }
}
