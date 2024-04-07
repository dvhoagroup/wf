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
        public frmVideo()
        {
            InitializeComponent();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            objBC.VideoLink = (string)txtLink.EditValue;
            db.SubmitChanges();
            DialogBox.Infomation("Dữ liệu đã được lưu");
            this.Close();
        }

        private void itemGo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            shockFlash.Movie = ((string)txtLink.EditValue).Replace("watch?v=", "/v/");
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void frmVideo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)        
            shockFlash.Movie = ((string)txtLink.EditValue).Replace("watch?v=", "/v/");
        
        }

        private void frmVideo_Load(object sender, EventArgs e)
        {
            if (this.MaBC != null)
            {
                objBC = db.mglbcBanChoThues.Single(p => p.MaBC == this.MaBC);
                txtLink.EditValue = objBC.VideoLink;
            }
        }
    }
}