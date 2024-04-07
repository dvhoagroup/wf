using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BEE.ThuVien;
using System.Threading;

namespace BEEREMA.Product
{
    public partial class frmFind : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        byte SDBID = 6;
        public int? ProductID { get; set; }
        public string ProductCode { get; set; }

        public frmFind()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this, barManager1);
        }

        private void Permission()
        {
            try
            {
                this.SDBID = db.AccessDatas.Single(p => p.FormID == 4 & p.PerID == Common.PerID).SDBID;
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        void SanPham_Load()
        {
            var wait = DialogBox.WaitingForm();

            try
            {
                short maLBDS = itemLoaiBDS.EditValue != null ? (short)itemLoaiBDS.EditValue : (short)-1;
                int maDA = itemDuAn.EditValue != null ? (int)itemDuAn.EditValue : -1;
                int maKhu = -1;
                int maPK = -1;
                int maNV = Common.StaffID;

                switch (this.SDBID)
                {
                    case 1://Tat ca
                        gcSP.DataSource = db.bdsSanPham_Select(maLBDS, maDA, maKhu, maPK, 0, 0, 0, maNV, -1);
                        break;
                    case 2://Theo phong
                        gcSP.DataSource = db.bdsSanPham_Select(maLBDS, maDA, maKhu, maPK, Common.DepartmentID, 0, 0, maNV, -1);
                        break;
                    case 3://Theo nhom
                        gcSP.DataSource = db.bdsSanPham_Select(maLBDS, maDA, maKhu, maPK, 0, Common.GroupID, 0, maNV, -1);
                        break;
                    case 4://Theo nhan vien
                        gcSP.DataSource = db.bdsSanPham_Select(maLBDS, maDA, maKhu, maPK, 0, 0, maNV, maNV, -1);
                        break;
                    default:
                        gcSP.DataSource = null;
                        break;
                }
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }

            wait.Close();
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SanPham_Load();
        }

        private void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void frmDistribute_Load(object sender, EventArgs e)
        {
            Permission();

            var listDuAn = db.DuAn_getList();
            lookDuAn.DataSource = listDuAn;
            lookDaAn2.DataSource = listDuAn;
            var listLoaiBDS = db.LoaiBDs.ToList();

            lookLoaiBDS.DataSource = listLoaiBDS;
            lookHuong.DataSource = db.PhuongHuongs;
            lookPhapLy.DataSource = db.PhapLies;
            lookLoaiDuong.DataSource = db.LoaiDuongs;
            lookTrangThai.DataSource = db.bdsTrangThais;
        }

        private void itemSelect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SelectItem();
        }

        void SelectItem()
        {
            if (grvSP.FocusedRowHandle < 0)
            {
                DialogBox.Infomation("Vui lòng chọn [Sản phẩm], xin cảm ơn.");
                return;
            }

            ProductID = (int?)grvSP.GetFocusedRowCellValue("MaSP");
            ProductCode = grvSP.GetFocusedRowCellValue("KyHieuSALE").ToString();
            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void frmFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SelectItem();
        }
    }
}
