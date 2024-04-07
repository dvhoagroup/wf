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
    public partial class ctlThongKeDuAn : DevExpress.XtraEditors.XtraUserControl
    {
        int MaRP;
        /// <summary>
        /// 1 : Thống kê số lượng, 2 : Doanh Thu , 3 : Tung dự án
        /// </summary>
        /// <param name="MaRP"></param>
        /// 
        public ctlThongKeDuAn(int _MaRP)
        {
            InitializeComponent();
            MaRP = _MaRP;
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
                var listCT = itemCongTrinhDuAn.EditValue == null ? "" : itemCongTrinhDuAn.EditValue.ToString().Replace(" ", "");
                    switch (MaRP)
                    {
                        case 1: var rpt = new rptThongKeDuAn(maDA, listCT, tuNgay, denNgay);
                            rpt.CreateDocument();
                            printControl1.PrintingSystem = rpt.PrintingSystem;
                            break;
                        case 2: var ds = new rptDoanhSoDuAn(maDA, listCT, tuNgay, denNgay);
                            ds.CreateDocument();
                            printControl1.PrintingSystem = ds.PrintingSystem;
                            break;
                        case 3: var td = new rptThong1KeDuAn(maDA, listCT, tuNgay, denNgay);
                            td.CreateDocument();
                            printControl1.PrintingSystem = td.PrintingSystem;
                            break;
                    }
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
            using (var db = new MasterDataContext())
            {
                lookCongTrinh.DataSource = db.Khus.Where(p => p.MaDA == (int?)itemDuAn.EditValue).Select(p => new
                {
                    p.MaKhu,
                    TenKhu = p.DienGiai!=null? p.DienGiai: p.TenKhu
                });
                itemCongTrinhDuAn.EditValue = null;
            }
        }
    }
}
