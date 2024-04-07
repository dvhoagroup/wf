using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEE.ThuVien;
using System.Linq;
using BEEREMA;

namespace BEE.NghiepVuKhac.Rate
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db;
        public int? KeyID { get; set; }
        public byte MaLT { get; set; }
        ltTyGia objTG;

        public frmEdit()
        {
            InitializeComponent();

            db = new MasterDataContext();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (dateThoiDiem.DateTime.Year == 1)
            {
                DialogBox.Infomation("Vui lòng nhập [Thời điểm], xin cảm ơn.");
                dateThoiDiem.Focus();
                return;
            }

            if (spinMuaVao.Value == 0)
            {
                DialogBox.Infomation("Vui lòng nhập [Tỷ giá Mua vào], xin cảm ơn.");
                spinMuaVao.Focus();
                return;
            }

            if (spinBanRa.Value == 0)
            {
                DialogBox.Infomation("Vui lòng nhập [Tỷ giá Bán ra], xin cảm ơn.");
                spinBanRa.Focus();
                return;
            }

            if (KeyID == null)
            {
                objTG = new ltTyGia();
                objTG.MaNV = BEE.ThuVien.Common.StaffID;
                db.ltTyGias.InsertOnSubmit(objTG);
            }
            else
            {
                objTG.MaNVCN = BEE.ThuVien.Common.StaffID;
            }

            objTG.BanRa = spinBanRa.Value;
            objTG.DienGiai = txtDienGiai.Text;
            objTG.MaLT = MaLT;
            objTG.MuaVao = spinMuaVao.Value;
            objTG.LaiSuatBR = (decimal?)spinBanRaLS.EditValue;
            objTG.LaiSuatMV = (decimal?)spinMuaVaoLS.EditValue;
            objTG.NgayNhap = dateThoiDiem.DateTime;

            try
            {
                db.SubmitChanges();

                DialogBox.Infomation();
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch { DialogBox.Infomation("Đã xảy ra lỗi. Vui lòng kiểm tra lại, xin cảm ơn."); }

            this.Close();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            if (KeyID != null)
            {
                try
                {
                    objTG = db.ltTyGias.Single(p => p.ID == KeyID);
                    txtDienGiai.Text = objTG.DienGiai;
                    spinMuaVao.EditValue = objTG.MuaVao;
                    spinBanRa.EditValue = objTG.BanRa;
                    spinMuaVaoLS.EditValue = objTG.LaiSuatMV ?? 0;
                    spinBanRaLS.EditValue = objTG.LaiSuatBR ?? 0;
                    dateThoiDiem.EditValue = objTG.NgayNhap;
                    MaLT = objTG.MaLT ?? 1;
                }
                catch { this.Close(); }
            }
            else
                dateThoiDiem.EditValue = DateTime.Now;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}