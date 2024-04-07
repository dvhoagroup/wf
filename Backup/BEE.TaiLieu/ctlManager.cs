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
    public partial class ctlManager : DevExpress.XtraEditors.XtraUserControl
    {
        public ctlManager()
        {
            InitializeComponent();
            Permission();
        }

        MasterDataContext db = new MasterDataContext();
        byte SDBID = 6;

        void Permission()
        {
            try
            {
                var listAction = db.ActionDatas.Where(p => p.FormID == 99 & p.PerID == BEE.ThuVien.Common.PerID)
                    .Select(p => p.FeatureID).ToList();
                itemThem.Enabled = listAction.Contains(1);
                itemSua.Enabled = listAction.Contains(2);
                itemXoa.Enabled = listAction.Contains(3);
                itemXem.Enabled = listAction.Contains(34);
                itemTaiVe.Enabled = listAction.Contains(38);
                this.SDBID = db.AccessDatas.Single(p => p.FormID == 99 & p.PerID == BEE.ThuVien.Common.PerID).SDBID;
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        void TaiLieu_Load()
        {
            var wait = DialogBox.WaitingForm();

            try
            {
                DateTime tuNgay = itemTuNgay.EditValue != null ? (DateTime)itemTuNgay.EditValue : DateTime.Now.AddDays(-90);
                DateTime denNgay = itemDenNgay.EditValue != null ? (DateTime)itemDenNgay.EditValue : DateTime.Now;
                switch (this.SDBID)
                {
                    case 1://Tat ca
                        gcTaiLieu.DataSource = db.docTaiLieu_Select(tuNgay, denNgay, 0, 0, 0, Common.StaffID);
                        break;
                    case 2://Theo phong ban
                        gcTaiLieu.DataSource = db.docTaiLieu_Select(tuNgay, denNgay, BEE.ThuVien.Common.DepartmentID, 0, 0, Common.StaffID);
                        break;
                    case 3://Theo nhom
                        gcTaiLieu.DataSource = db.docTaiLieu_Select(tuNgay, denNgay, 0, BEE.ThuVien.Common.GroupID, 0, Common.StaffID);
                        break;
                    case 4://Theo nhan vien
                        gcTaiLieu.DataSource = db.docTaiLieu_Select(tuNgay, denNgay, 0, 0, BEE.ThuVien.Common.StaffID, Common.StaffID);
                        break;
                    default:
                        gcTaiLieu.DataSource = null;
                        break;
                }
            }
            catch { }

            wait.Close();
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

        private void ctlManager_Load(object sender, EventArgs e)
        {
            BEE.NgonNgu.Language.TranslateUserControl(this, barManager1);

            Permission();

            lookLoaiTL.DataSource = db.docLoaiTaiLieus.OrderBy(p => p.STT);
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBC);
            SetDate(0);
        }

        private void itemTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            TaiLieu_Load();
        }

        private void itemDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            TaiLieu_Load();
        }

        private void cmbKyBC_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TaiLieu_Load();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new frmEdit();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK) TaiLieu_Load();
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var maTL = (int?)grvTaiLieu.GetFocusedRowCellValue("MaTL");
            if (maTL == null)
            {
                DialogBox.Error("Vui lòng chọn tài liệu");
                return;
            }
            var frm = new frmEdit();
            frm.MaTL = maTL;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK) TaiLieu_Load();
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (BEE.ThuVien.Common.PerID == 1)
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

        private void txtXem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvTaiLieu.GetFocusedRowCellValue("DuongDan") == null) return;
            var frm = new FTP.frmDownloadFile();
            frm.FileName = grvTaiLieu.GetFocusedRowCellValue("DuongDan").ToString();
            frm.ShowDialog();
        }

        private void txtTaiVe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvTaiLieu.GetFocusedRowCellValue("DuongDan") == null) return;
            var frm = new FTP.frmDownloadFile();
            frm.FileName = grvTaiLieu.GetFocusedRowCellValue("DuongDan").ToString();
            if (frm.SaveAs())
                frm.ShowDialog();
        }

        private void itemNhanVienXem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var id = (int?)grvTaiLieu.GetFocusedRowCellValue("MaTL");
            if (id == null)
            {
                DialogBox.Error("Vui lòng chọn tài liệu");
                return;
            }
            var frm = new frmChuyenNhanVien();
            frm.MaTL = id;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK) TaiLieu_Load();
        }
    }
}
