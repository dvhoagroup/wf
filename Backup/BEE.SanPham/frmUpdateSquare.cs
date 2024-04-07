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
    public partial class frmUpdateSquare : DevExpress.XtraEditors.XtraForm
    {
        public frmUpdateSquare()
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
                        DienTichCH = p[8].ToString().Trim(),
                        DienTichTT = p[44].ToString().Trim(),
                    });

                    var ltSP = new List<bdsSanPham>();
                    foreach (var r in list)
                    {
                        try
                        {
                            var duAn = db.DuAns.FirstOrDefault(p => p.TenDA == r.DuAn);
                            var khu = db.Khus.FirstOrDefault(p => p.TenKhu == r.Khu & p.MaDA == duAn.MaDA);

                            bdsSanPham sp = db.bdsSanPhams.SingleOrDefault(p => p.KyHieuSUN == r.MaSUN & p.MaDA == duAn.MaDA & p.MaKhu == khu.MaKhu);
                            sp.KyHieuSUN = r.MaSUN;
                            sp.DienTichCH = Convert.ToDecimal(r.DienTichCH != "" ? Convert.ToDecimal(r.DienTichCH) : 0);
                            sp.DienTichTT = Convert.ToDecimal(r.DienTichTT != "" ? Convert.ToDecimal(r.DienTichTT) : 0);

                            ltSP.Add(sp);
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