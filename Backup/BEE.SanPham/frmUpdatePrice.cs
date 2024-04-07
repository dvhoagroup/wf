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
using LinqToExcel;
using BEEREMA;

namespace BEE.SanPham
{
    public partial class frmUpdatePrice : DevExpress.XtraEditors.XtraForm
    {
        public frmUpdatePrice()
        {
            InitializeComponent();
        }

        public bool IsSave;

        MasterDataContext db = new MasterDataContext();

        private void frmImport_Load(object sender, EventArgs e)
        {
            
        }

        private void itemFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (OpenFileDialog file = new OpenFileDialog())
            {
                file.Filter = "(Excel file)|*.xls;*.xlsx";
                file.ShowDialog();
                if (file.FileName == "") return;
                var excel = new ExcelQueryFactory(file.FileName);
                var sheets = excel.GetWorksheetNames();
                cmbSheet.Items.Clear();
                foreach (string s in sheets)
                    cmbSheet.Items.Add(s.Trim('$'));
                itemSheet.EditValue = null;
                this.Tag = file.FileName;
            }
        }

        private void itemSheet_EditValueChanged(object sender, EventArgs e)
        {
            if (itemSheet.EditValue != null)
            {
                var wait = DialogBox.WaitingForm();
                try
                {
                    var excel = new ExcelQueryFactory(this.Tag.ToString());
                    var list = excel.Worksheet(itemSheet.EditValue.ToString()).Select(p => new
                    {
                        DuAn = p[0].ToString().Trim(),
                        Khu = p[1].ToString().Trim(),
                        KyHieu = p[4].ToString().Trim(),
                        MaSUN = p[5].ToString().Trim(),
                        //DienTichCH = p[8].ToString().Trim(),
                        GiaBan = p[17].ToString().Trim(),
                        TongGiaBan = p[18].ToString().Trim()
                    });

                    var ltSP = new List<bdsSanPham>();
                    foreach (var r in list)
                    {
                        try
                        {
                            var duAn = db.DuAns.FirstOrDefault(p => p.TenDA == r.DuAn);
                            var khu = db.Khus.FirstOrDefault(p => p.TenKhu == r.Khu & p.MaDA == duAn.MaDA);

                            bdsSanPham sp = db.bdsSanPhams.SingleOrDefault(p => p.KyHieuSUN == r.MaSUN & p.MaTT <= 2 & p.MaDA == duAn.MaDA & p.MaKhu == khu.MaKhu);
                            var objDC = db.pdcPhieuDatCocs.Where(p => p.pgcPhieuGiuCho.MaSP == sp.MaSP & p.MaTT == 1).Count();
                            var objMB = db.HopDongMuaBans.Where(p => p.pgcPhieuGiuCho.MaSP == sp.MaSP & p.MaTT == 1).Count();
                            if (objDC == 0 & objMB == 0)
                            {
                                sp.KyHieuSUN = r.MaSUN;
                                //sp.DienTichCH = Convert.ToDecimal(r.DienTichCH != "" ? Convert.ToDecimal(r.DienTichCH) : 0);
                                sp.GiaBan = Convert.ToDecimal(r.GiaBan != "" ? Convert.ToDecimal(r.GiaBan) : 0);
                                sp.TongGiaBan = Convert.ToDecimal(r.TongGiaBan != "" ? Convert.ToDecimal(r.TongGiaBan) : 0);
                                sp.PhiBaoTri = sp.TongGiaBan.Value * 0.02M;
                                sp.ThueVAT = sp.TongGiaBan * sp.TyLeVAT / 100;

                                ltSP.Add(sp);
                            }
                        }
                        catch { }
                    }
                    gcSP.DataSource = ltSP;
                    list = null;
                }
                catch { }
                finally
                {
                    wait.Close();
                }
            }
            else
            {
                gcSP.DataSource = null;
            }
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DialogBox.Question() == DialogResult.No) return;
            grvSP.DeleteSelectedRows();
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                var ltSP = (List<bdsSanPham>)gcSP.DataSource;
                db.SubmitChanges();

                gcSP.DataSource = null;
                this.IsSave = true;
                DialogBox.Infomation("Dữ liệu đã được lưu");
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            wait.Close();
        }

        private void itemDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}