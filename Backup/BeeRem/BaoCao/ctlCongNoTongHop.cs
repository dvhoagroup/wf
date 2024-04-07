using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using BEE.ThuVien;
using BEE.ThuVien.Report;

namespace BEEREMA.BaoCao
{
    public partial class ctlCongNoTongHop : DevExpress.XtraEditors.XtraUserControl
    {
        public ctlCongNoTongHop()
        {
            InitializeComponent();
        }

        private void BaoCao_Nap()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                var denNgay = (DateTime?)itemDenNgay.EditValue;
                if (dateDenNgay == null)
                {
                    DialogBox.Error("Vui lòng nhập [Ngày đến]");
                    return;
                }

                var rpt = new rptCongNoTongHop(denNgay);
                rpt.CreateDocument();

                printControl1.PrintingSystem = rpt.PrintingSystem;
            }
            catch { }
            finally
            {
                wait.Close();
            }
        }

        private void ctlCongNoTongHop_Load(object sender, EventArgs e)
        {
            itemDenNgay.EditValue = DateTime.Now;
            BaoCao_Nap();
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaoCao_Nap();
        }
    }
}
