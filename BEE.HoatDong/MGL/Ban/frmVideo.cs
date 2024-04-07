using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using BEE.ThuVien;
using BEEREMA;

namespace BEE.HoatDong.MGL.Ban
{
    public partial class frmVideo : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public int? MaBC { get; set; }
        ThuVien.mglbcBanChoThue objBC = new mglbcBanChoThue();
        void LoadPermission()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = Common.PerID;
            o.AccessData.Form.FormID = 214;
            DataTable tblAction = o.SelectBy();
            itemTaiLen.Enabled = false;
            itemDownLoad.Enabled = false;
            itemDelete.Enabled = false;

            if (tblAction.Rows.Count > 0)
            {
                foreach (DataRow r in tblAction.Rows)
                {
                    switch (byte.Parse(r["FeatureID"].ToString()))
                    {
                        case 3:
                            itemDelete.Enabled = true;
                            break;
                        case 89:
                            itemTaiLen.Enabled = true;
                            break;
                        case 90:
                            itemDownLoad.Enabled = true;
                            break;
                    }
                }
            }
        }

        int GetAccessData()
        {
            it.AccessDataCls o = new it.AccessDataCls(Common.PerID, 214);

            return o.SDB.SDBID;
        }
        public frmVideo()
        {
            InitializeComponent();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // objBC.VideoLink = (string)txtLink.EditValue;
            //db.SubmitChanges();
            DialogBox.Infomation("Dữ liệu đã được lưu");
            this.Close();
        }

        private void itemGo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void frmVideo_KeyDown(object sender, KeyEventArgs e)
        {


        }

        private void frmVideo_Load(object sender, EventArgs e)
        {
            LoadPermission();
            LoadData();
        }
        void LoadData()
        {
            if (this.MaBC != null)
            {
                switch (GetAccessData())
                {
                    case 1://Tat ca
                        gcVideo.DataSource = db.mglbcVideobds.Where(p => p.MaBC == this.MaBC);
                        break;
                    case 2://Theo phong ban 
                        gcVideo.DataSource = (from p in db.mglbcVideobds
                                              from bc in db.mglbcBanChoThues.Where(bc => bc.MaBC == p.MaBC)
                                              from nc in db.NhanViens.Where(nv => nv.MaNV == bc.MaNVN)
                                              where p.MaBC == this.MaBC && nc.MaPB == Common.DepartmentID
                                              select p).ToList();
                        break;
                    case 3://Theo nhom
                        gcVideo.DataSource = (from p in db.mglbcVideobds
                                              from bc in db.mglbcBanChoThues.Where(bc => bc.MaBC == p.MaBC)
                                              from nc in db.NhanViens.Where(nv => nv.MaNV == bc.MaNVN)
                                              where p.MaBC == this.MaBC && nc.MaNKD == Common.GroupID
                                              select p).ToList();
                        break;
                    case 4://Theo nhan vien
                        gcVideo.DataSource = (from p in db.mglbcVideobds
                                              from bc in db.mglbcBanChoThues.Where(bc => bc.MaBC == p.MaBC)
                                              from nc in db.NhanViens.Where(nv => nv.MaNV == bc.MaNVN)
                                              where p.MaBC == this.MaBC && nc.MaNV == Common.StaffID
                                              select p).ToList();
                        break;
                    default:
                        gcVideo.DataSource = null;
                        break;
                }

            }
        }
        private void btnPlay_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (MasterDataContext db = new MasterDataContext())
            {
                var objcf = db.tblConfigs.FirstOrDefault();
                var path = grvVideo.GetFocusedRowCellValue("DuongDan").ToString();
                var url = objcf.WebUrl + path;
                var frm = new frmViewVideo();
                frm.FileName = url;
                frm.ShowDialog();
            }

        }

        private void itemTaiLen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new FTP.frmUploadFile();
            frm.Type = 2;
            if (frm.SelectFile(false))
            {
                var namePath = frm.ClientPath;
                frm.Folder = "documents/video";
                frm.ShowDialog();
                if (frm.DialogResult != DialogResult.OK) return;

                var obj = new mglbcVideobd();
                obj.MaBC = this.MaBC;
                obj.DuongDan = frm.FileName;
                obj.Position = "1";
                obj.MacDinh = false;
                db.mglbcVideobds.InsertOnSubmit(obj);
                db.SubmitChanges();
            }
            frm.Dispose();
            LoadData();
        }

        private void itemDownLoad_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grvVideo.GetFocusedRowCellValue("DuongDan") == null)
            {
                DialogBox.Warning("Bạn chưa chọn video cần tải xuống.");
                return;
            }
            var frm = new FTP.frmDownloadFile();
            frm.FileName = grvVideo.GetFocusedRowCellValue("DuongDan").ToString();
            if (frm.SaveAs())
                frm.ShowDialog();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = grvVideo.GetSelectedRows();
            if (indexs.Length <= 0)
            {
                DialogBox.Error("Vui lòng chọn video cần xóa");
                return;
            }

            if (DialogBox.Question() == DialogResult.No) return;
            List<string> files = new List<string>();
            foreach (var i in indexs)
            {
                var video = db.mglbcVideobds.FirstOrDefault(p => p.ID == (int)grvVideo.GetRowCellValue(i, "ID"));
                if (video.DuongDan != null)
                    files.Add(video.DuongDan);
                db.mglbcVideobds.DeleteOnSubmit(video);
            }
            db.SubmitChanges();

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

            DialogBox.Infomation("Đã xóa video thành công!");

            LoadData();
        }
    }
}