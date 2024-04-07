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
    public partial class ctlLichSuGiaoDich : DevExpress.XtraEditors.XtraUserControl
    {
        public ctlLichSuGiaoDich()
        {
            InitializeComponent();
        }

        private void SetDate(int index)
        {
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
        }

        private void BaoCao_Nap()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                var tuNgay = (DateTime?)itemTuNgay.EditValue;
                var denNgay = (DateTime?)itemDenNgay.EditValue;
                var maDA = (int?)itemDuAn.EditValue;

                var rpt = new rptLichSuGiaoDich(tuNgay, denNgay, maDA);
                rpt.CreateDocument();

                printControl1.PrintingSystem = rpt.PrintingSystem;
            }
            catch { }
            finally
            {
                wait.Close();
            }
        }

        private void ctlPhanTichTuoiNo_Load(object sender, EventArgs e)
        {
            using (var db = new MasterDataContext())
            {
                var ltDuAn = db.DuAns.OrderByDescending(p => p.TenDA).Select(p => new DuAnItem(p.MaDA, p.TenDA)).ToList();
                ltDuAn.Insert(0, new DuAnItem(-1, "Tất cả"));
                lookDuAn.DataSource = ltDuAn;
            }

            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);

            SetDate(4);

            BaoCao_Nap();
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }
        
        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaoCao_Nap();
        }
    }
}
