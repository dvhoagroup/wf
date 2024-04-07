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

namespace BEE.HoatDong.BaoCao
{
    public partial class ctlManager : DevExpress.XtraEditors.XtraUserControl
    {
        MasterDataContext db;

        public ctlManager()
        {
            InitializeComponent();
        }

        void BaoCao_Load()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                gcBaoCao.DataSource = db.rpReports;

            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            wait.Close();
        }

        private void ctlManager_Load(object sender, EventArgs e)
        {
            db = new MasterDataContext();
            var lstNV = db.NhanViens.Select(p => new { p.MaNV, p.HoTen });
            sLookUpNhanVien.DataSource = chkNhanVien.DataSource = lstNV;

            BaoCao_Load();
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaoCao_Load();
        }

        private void itemPreview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var code = grvBaoCao.GetFocusedRowCellValue("Code")?.ToString();
            switch (code)
            {
                case "001":
                    using (var frm = new BEE.HoatDong.BaoCao.frmReportCall())
                    {
                        frm.ShowDialog();
                    }
                    break;
                case "002":
                    using (var frm = new BEE.HoatDong.BaoCao.frmReportTotalCall())
                    {
                        frm.ShowDialog();
                    }
                    break;
                case "003":
                    using (var frm = new BEE.HoatDong.BaoCao.frmReportDealProcessing())
                    {
                        frm.ShowDialog();
                    }
                    break;
                case "004":
                    using (var frm = new BEE.HoatDong.BaoCao.frmReportTotalDealProcessing())
                    {
                        frm.ShowDialog();
                    }
                    break;
            }
        }
    }
}
