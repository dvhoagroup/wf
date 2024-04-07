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
    public partial class ctlPhanTichTuoiNo : DevExpress.XtraEditors.XtraUserControl
    {
        int? loaiBaoCao;
        public ctlPhanTichTuoiNo(int? loaiBC)
        {
            InitializeComponent();
            loaiBaoCao = loaiBC;
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

                

                switch (loaiBaoCao)
                {
                    case 1:
                        var rpt = new rptPhanTichTuoiNo(tuNgay, denNgay, maDA);
                        rpt.CreateDocument();
                        printControl1.PrintingSystem = rpt.PrintingSystem;
                        break;
                    case 2:
                        var rpt2 = new rptTongHopCongNo(tuNgay, denNgay, maDA);
                        rpt2.CreateDocument();
                        printControl1.PrintingSystem = rpt2.PrintingSystem;
                        break;
                    default:
                        var rpt3 = new rptPhanTichTuoiNo(tuNgay, denNgay, maDA);
                        rpt3.CreateDocument();
                        printControl1.PrintingSystem = rpt3.PrintingSystem;
                        break;
                }
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
