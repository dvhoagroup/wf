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
    public partial class frmImportGCN : DevExpress.XtraEditors.XtraForm
    {
        public frmImportGCN()
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
                        KyHieu = p[0].ToString().Trim(),
                        MaLo = p[1].ToString().Trim(),
                        SoVaoSoGCN = p[2].ToString().Trim(),
                        GCNQSDD = p[3].ToString().Trim(),
                        NgayKyGCN = p[4].ToString().Trim(),
                        SoThua = p[5].ToString().Trim(),
                        DiaChiNha = p[6].ToString().Trim(),
                        TinhTrangXD = p[7].ToString().Trim(),
                        NhomKH = p[8].ToString().Trim(),
                        DienTichKV = p[9].ToString().Trim(),
                        DonGiaKV = p[10].ToString().Trim(),
                        ThanhTienKV = p[11].ToString().Trim(),
                        DienTichXD = p[12].ToString().Trim(),
                        DonGiaXD = p[13].ToString().Trim(),
                        ThanhTienXD = p[14].ToString().Trim()
                    });

                    wait.Hide();
                    var ltSP = new List<bdsSanPham>();
                    foreach (var r in list)
                    {
                        try
                        {
                            bdsSanPham sp = db.bdsSanPhams.Single(p => p.KyHieu == r.KyHieu && p.MaLo == r.MaLo);
                            sp.KyHieu = r.KyHieu;
                            sp.MaLo = r.MaLo;
                            if (r.SoVaoSoGCN != "")
                                sp.SoVaoSoGCN = r.SoVaoSoGCN;
                            if (r.GCNQSDD != "")
                                sp.GCNQSDD = r.GCNQSDD;
                            if (r.TinhTrangXD != "")
                                sp.TinhTrangXD = r.TinhTrangXD;
                            if(r.NhomKH != "")
                                sp.NhomKH = r.NhomKH;
                            if (r.SoThua != "")
                                sp.SoThua = r.SoThua;
                            if (r.DiaChiNha != "")
                                sp.DiaChiNha = r.DiaChiNha;
                            if (r.NgayKyGCN != "")
                            {
                                try
                                {
                                    sp.NgayKyGCN = Convert.ToDateTime(r.NgayKyGCN);
                                }
                                catch { }
                            }
                            if (r.DienTichKV != "")
                                sp.DienTichKV = Convert.ToDecimal(r.DienTichKV);
                            if (r.DonGiaKV != "")
                                sp.DonGiaKV = Convert.ToDecimal(r.DonGiaKV);
                            if (r.ThanhTienKV != "")
                                sp.ThanhTienKV = Convert.ToDecimal(r.ThanhTienKV);

                            if (r.DienTichXD != "")
                                sp.DienTichXD = Convert.ToDecimal(r.DienTichXD);
                            if (r.DonGiaXD != "")
                                sp.DonGiaXD = Convert.ToDecimal(r.DonGiaXD);
                            if (r.ThanhTienXD != "")
                                sp.ThanhTienXD = Convert.ToDecimal(r.ThanhTienXD);

                            sp.ThanhTien = (sp.ThanhTienKV ?? 0) + (sp.ThanhTienXD ?? 0) + (sp.ThanhTienHM ?? 0);

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
                DialogBox.Infomation("Dữ liệu đã được lưu.");
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