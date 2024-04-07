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

namespace BEE.DuAn.SetMail
{
    public partial class frmTrangThai : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public frmTrangThai()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);

            this.Load += new EventHandler(frmTrangThai_Load);
            this.btnLuu.Click += new EventHandler(btnLuu_Click);
            this.btnHuy.Click += new EventHandler(btnHuy_Click);
        }

        void frmTrangThai_Load(object sender, EventArgs e)
        {
            gcTT.DataSource = db.daCaiDat_HinhThucMails;
        }

        void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                db.SubmitChanges();
                this.Close();
            }
            catch(Exception ex){
                DialogBox.Error(ex.Message);
            }
        }

        void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (grvTT.FocusedRowHandle < 0)
                return;
            string MaHT = grvTT.GetFocusedRowCellValue("MaHT").ToString();
            db.daCaiDat_HinhThucMails.DeleteOnSubmit(db.daCaiDat_HinhThucMails.Single(p => p.MaHT==MaHT));
            grvTT.DeleteSelectedRows();
            db.SubmitChanges();
        }

    }
}