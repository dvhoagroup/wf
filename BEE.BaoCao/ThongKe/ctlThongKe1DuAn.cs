using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEE.ThuVien;
using BEE.ThuVien.Report;
using System.Linq;
using LandSoft;
namespace BEE.BaoCao.ThongKe
{
    public partial class ctlThong1KeDuAn : DevExpress.XtraEditors.XtraUserControl
    {
        public ctlThong1KeDuAn()
        {
            InitializeComponent();
            cmbKyBaoCao2.EditValueChanged += new EventHandler(cmbKyBaoCao2_EditValueChanged);
        }

        void cmbKyBaoCao2_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }
        void SetDate(int index)
        {
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Index = index;
            objKBC.SetToDate();
            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
        }
        private void Data_Nap()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                var tuNgay = (DateTime?)itemTuNgay.EditValue;
                var denNgay = (DateTime?)itemDenNgay.EditValue;
                var maDA = (int?)itemDuAn.EditValue;

                var rpt = new rptThong1KeDuAn(maDA, itemCongTrinhLK.EditValue.ToString() ,tuNgay, denNgay);
                rpt.CreateDocument();

                printControl1.PrintingSystem = rpt.PrintingSystem;
            }
            catch { }
            finally
            {
                wait.Close();
            }
        }
        private void itemNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Data_Nap();
        }

        private void ctlThongKeDuAn_Load(object sender, EventArgs e)
        {
            using (var db = new MasterDataContext())
            {
                var ltDuAn = db.DuAns.OrderByDescending(p => p.TenDA).Select(p => new DuAnItem(p.MaDA, p.TenDA)).ToList();
                lookDuAn.DataSource = ltDuAn;
            }

            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao2);

            SetDate(4); 
            Data_Nap();
        }

        private void itemDuAn_EditValueChanged(object sender, EventArgs e)
        {
            using (var db = new MasterDataContext()) {
                lookCongTrinhLK.DataSource = db.Khus.Where(p => p.MaDA == (int?)itemDuAn.EditValue).Select(p => new { 
                p.MaKhu,
                TenKhu = p.DienGiai!=null? p.DienGiai: p.TenKhu
                });
                itemCongTrinhLK.EditValue = null;
            }
        }
    }
}
