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
    public partial class ctlThongKeDuAnNam : DevExpress.XtraEditors.XtraUserControl
    {
        int MaRP;
        /// <summary>
        /// 1 : Thống kê doanh so theo năm
        /// </summary>
        /// <param name="MaRP"></param>
        /// 
        public ctlThongKeDuAnNam(int _MaRP)
        {
            InitializeComponent();
            MaRP = _MaRP;
        }

        private void Data_Nap()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                var year = Convert.ToInt16(itemNam.EditValue);
                var maDA = (int?)itemDuAn.EditValue;
                var listCT = itemCongTrinhDuAn.EditValue == null ? "" : itemCongTrinhDuAn.EditValue.ToString().Replace(" ", "");
                    switch (MaRP)
                    {
                        case 1: var rpt = new rptDoanhSoDuAnTheoNam(maDA, listCT, year);
                            rpt.CreateDocument();
                            printControl1.PrintingSystem = rpt.PrintingSystem;
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
            itemNam.EditValue = DateTime.Now.Year;
            Data_Nap();
        }

        private void itemDuAn_EditValueChanged(object sender, EventArgs e)
        {
            using (var db = new MasterDataContext())
            {
                lookCongTrinh.DataSource = db.Khus.Where(p => p.MaDA == (int?)itemDuAn.EditValue).Select(p => new
                {
                    p.MaKhu,
                    TenKhu = p.DienGiai != null ? p.TenKhu + "  " + p.DienGiai : p.TenKhu
                }).ToList();
                itemCongTrinhDuAn.EditValue = null;
            }
        }

        private void itemCongTrinhDuAn_EditValueChanged(object sender, EventArgs e)
        {
            Data_Nap();
        }
    }
}
