using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using BEE.ThuVien;
using DevExpress.Utils.Drawing;
using DevExpress.XtraBars.Ribbon;
using System.Linq;
using BEEREMA;
using System.Net;
using System.Collections;

namespace BEE.HoatDong.MGL.Ban
{
    public partial class frmAnhbds : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        mglbcAnhbd objA;
        public int? ID { get; set; }
        public int? MaBC { get; set; }
        List<mglbcAnhbd> objAb;
        public frmAnhbds()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            try
            {
                objA = new mglbcAnhbd();
                if (this.MaBC != null)
                {
                    galleryControl1.Gallery.Groups[0].Items.Clear();
                    var objBC = db.mglbcBanChoThues.Single(p => p.MaBC == MaBC);
                    itemLink.EditValue = objBC.LinkAnh;
                    objAb = db.mglbcAnhbds.Where(p => p.MaBC == this.MaBC).ToList();
                    foreach (var i in objAb)
                    {
                        var filePath = db.tblConfigs.FirstOrDefault().FtpUrl + i.DuongDan;
                        var request = WebRequest.Create(filePath);
                        string mk = it.CommonCls.GiaiMa(db.tblConfigs.FirstOrDefault().FtpPass);
                        request.Credentials = new NetworkCredential(db.tblConfigs.FirstOrDefault().FtpUser, it.CommonCls.GiaiMa(db.tblConfigs.FirstOrDefault().FtpPass));
                        using (var response = request.GetResponse())
                        using (var stream = response.GetResponseStream())
                        using (var img = Image.FromStream(stream))
                        {
                            if (!System.IO.Directory.Exists("Images"))
                                System.IO.Directory.CreateDirectory("Images");
                            var pp = Application.StartupPath + "\\Images" + "/" + i.ID + "_" + DateTime.Now.ToString("ddyyyyMMHHmmssffffff") + ".jpg";
                            img.Save(pp, System.Drawing.Imaging.ImageFormat.Png);
                            using (var img1 = Image.FromFile(pp))
                            {
                                galleryControl1.Gallery.Groups[0].Items.Add(new DevExpress.XtraBars.Ribbon.GalleryItem(new Bitmap(img1), "", "") { Tag = i.ID });

                                if (i.MacDinh.GetValueOrDefault())
                                {
                                    picAnh.Image = new Bitmap(img1);
                                    picAnh.Tag = i.ID;
                                }
                                img.Dispose();
                                img1.Dispose();
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void frmAnhbds_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void itemTaiLen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            list.Clear();
            OpenFileDialog od = new OpenFileDialog();
            od.Multiselect = true;
            if (od.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (string fileName in od.FileNames)
                {
                    list.Add(fileName);
                }
            }
        }

        private void itemTaiXuong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                foreach (var p in objAb)
                {
                    if (p.ID == (int?)picAnh.Tag)
                    {
                        if (p.DuongDan == "")
                            return;
                        else
                        {
                            var frm = new FTP.frmDownloadFile();
                            frm.FileName = db.tblConfigs.FirstOrDefault().FtpUrl + p.DuongDan;
                            if (frm.SaveAs())
                                frm.ShowDialog();
                        }
                    }
                }
            }
            catch
            {
                DialogBox.Error("Có lỗi khi tải về!");
            }

        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.Question() == DialogResult.No) return;
            try
            {
                foreach (var p in objAb)
                {
                    if (p.ID == (int?)picAnh.Tag)
                    {
                        mglbcAnhbd objBC = db.mglbcAnhbds.Single(a => a.ID == p.ID);
                        db.mglbcAnhbds.DeleteOnSubmit(objBC);
                    }
                }
                db.SubmitChanges();
                DialogBox.Infomation("Đã xóa thành công. Vui lòng load lại form");
            }
            catch
            {
                DialogBox.Error("Có lỗi khi tải về!");
            }
            LoadData();
        }

        private void itemDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void galleryControl1_Gallery_ItemClick(object sender, DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs e)
        {
            picAnh.Image = e.Item.Image;
            picAnh.Tag = e.Item.Tag;
        }

        List<string> list = new List<string>();

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foreach (var item in list)
            {
                objA = new mglbcAnhbd();
                var frm = new FTP.frmUploadFile();
                frm.Folder = "documents/" + DateTime.Now.ToString("yyyy/MM/dd");
                frm.ClientPath = item + "";
                frm.ShowDialog();
                if (frm.DialogResult != DialogResult.OK) return;
                objA.DuongDan = frm.FileName;
                objA.MaBC = this.MaBC;
                db.mglbcAnhbds.InsertOnSubmit(objA);
            }

            var objBC = db.mglbcBanChoThues.Single(p => p.MaBC == MaBC);
            objBC.LinkAnh = (string)itemLink.EditValue;

            db.SubmitChanges();
            DialogBox.Infomation("Dữ liệu đã được lưu");
            LoadData();
        }

        private void itemMacDinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (picAnh.Tag == null)
                return;
            try
            {
                foreach (var p in objAb)
                {
                    if (p.ID == (int?)picAnh.Tag)
                    {
                        p.MacDinh = true;
                    }
                    else
                        p.MacDinh = false;

                    db.SubmitChanges();

                }
                DialogBox.Infomation("Ảnh này đã được thiết lập mặc định!");
            }
            catch
            {
                DialogBox.Error("Có lỗi khi thiết lập ảnh mặc định!");
            }

        }

        private void frmAnhbds_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                this.Controls.Clear();
                System.IO.DirectoryInfo di = new DirectoryInfo(Application.StartupPath + "\\Images");
                di.Delete(true);
            }
            catch { }
        }
    }

}