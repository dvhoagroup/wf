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
using BEEREMA;

namespace BEE.CongCu
{
    public partial class frmTemplate : DevExpress.XtraEditors.XtraForm
    {
        public int? ID { get; set; }
        public int? MaPGC { get; set; }
        public int? MaLGD { get; set; }
        public int? MaLNV { get; set; }
        public int? MaTTac { get; set; }
        public byte? KeyBG { get; set; }
        public string SoPhieu = "";

        MasterDataContext db = new MasterDataContext();
        pgcBieuMau objBM;

        public frmTemplate()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
        }

        private void frmTemplate_Load(object sender, EventArgs e)
        {
            int maDA;
            if (this.ID != null)
            {
                objBM = db.pgcBieuMaus.Single(p => p.ID == this.ID);
                txtSoPhieu.EditValue = objBM.SoPhieu;
                dateNgayKy.EditValue = objBM.NgayKy;
                spinLien.EditValue = objBM.Lien;
                spinLan.EditValue = objBM.Lan;
                maDA = objBM.pgcPhieuGiuCho.bdsSanPham.MaDA.Value;
                lookBieuMau.EditValue = objBM.MaBM;
                txtDienGiai.EditValue = objBM.DienGiai;
                MaLNV = objBM.MaLGD;
                KeyBG = objBM.KeyBG;
                MaLGD = objBM.MaLGD;
            }
            else
            {
                var objPGC = db.pgcPhieuGiuChos.Single(p => p.MaPGC == this.MaPGC);
                objBM = new pgcBieuMau();
                objBM.pgcPhieuGiuCho = objPGC;
                maDA = objPGC.bdsSanPham.MaDA.Value;
                switch (this.MaLGD)
                {
                    case 1:
                        txtSoPhieu.EditValue = objPGC.SoPhieu;
                        dateNgayKy.EditValue = objPGC.NgayKy;
                        break;
                    case 2:
                        txtSoPhieu.EditValue = objPGC.pdcPhieuDatCoc.SoPhieu;
                        dateNgayKy.EditValue = objPGC.pdcPhieuDatCoc.NgayKy;
                        break;
                    case 3:
                        txtSoPhieu.EditValue = objPGC.vvbhHopDong.SoHDVV;
                        dateNgayKy.EditValue = objPGC.vvbhHopDong.NgayKy;
                        break;
                    case 4:
                        txtSoPhieu.EditValue = objPGC.HopDongMuaBan.SoHDMB;
                        dateNgayKy.EditValue = objPGC.HopDongMuaBan.NgayKy;
                        break;
                    case 5:
                        txtSoPhieu.EditValue = objPGC.bgbhBanGiao.SoBG;
                        dateNgayKy.EditValue = objPGC.bgbhBanGiao.NgayBG;
                        break;
                    case 6:
                        txtSoPhieu.EditValue = objPGC.tlbhThanhLy.SoTL;
                        dateNgayKy.EditValue = objPGC.tlbhThanhLy.NgayTL;
                        break;
                    default :
                        txtSoPhieu.EditValue = SoPhieu;
                        dateNgayKy.EditValue = DateTime.Now;
                        break;
                }
            }

            lookBieuMau.Properties.DataSource = db.daBieuMaus.Where(p => p.MaDA == maDA & !p.Khoa.GetValueOrDefault()).Select(p => new { p.MaBM, p.TenBM }).ToList();// & p.MaLBM == MaLNV).Select(p => new { p.MaBM, p.TenBM }).ToList();
            lookBieuThuc.DataSource = db.daBieuThucs.Where(p => p.MaLBT == 96).Select(p => new { p.MaBT, p.TenBT, p.KyHieu, p.DienGiai }).ToList();
            gcKhac.DataSource = objBM.pgcbmThongTinKhacs;

            //lookUpNghiepVu.Properties.DataSource = db.nvNghiepVus;
            //lookUpNghiepVu.EditValue = MaLNV;
            //lookUpThaoTac.Properties.DataSource = db.nvFormActions.Where(p => p.FormID == MaLNV).Select(p => new { p.nvThaoTac.Name, p.nvThaoTac.ID, p.No });
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                #region Rang buoc
                if (txtSoPhieu.Text.Trim() == "")
                {
                    DialogBox.Error("Vui lòng nhập [Số phiếu], xin cảm ơn.");
                    txtSoPhieu.Focus();
                    return;
                }
                if (dateNgayKy.EditValue == null)
                {
                    DialogBox.Error("Vui lòng nhập [Ngày ký], xin cảm ơn.");
                    dateNgayKy.Focus();
                    return;
                }
                if (lookBieuMau.EditValue == null)
                {
                    DialogBox.Error("Vui lòng chọn [Biểu mẫu], xin cảm ơn.");
                    lookBieuMau.Focus();
                    return;
                }
                #endregion

                //check
                //var max = db.gdNghiepVus.Where(p => p.MaLNV == MaLNV & p.MaPGC == MaPGC).Max(p => p.STT) ?? 0;
                //var objNext = db.nvFormActions.Where(p => p.FormID == MaLNV & p.No > max).OrderBy(p => p.No).FirstOrDefault();
                //if (objNext != null)
                //{
                //    if (max > Convert.ToInt32(lookUpThaoTac.GetColumnValue("No")))
                //    {
                //        if (DialogBox.Question(string.Format("Bước tiếp theo [{0}].\r\nBạn có muốn tiếp tục không?", objNext.nvThaoTac.Name)) == System.Windows.Forms.DialogResult.No)
                //            return;
                //        else
                //            goto doo;
                //    }
                //    else
                //    {
                //        if (objNext.No == Convert.ToInt32(lookUpThaoTac.GetColumnValue("No")))
                //            goto doo;
                //        else
                //        {
                //            if (DialogBox.Question(string.Format("Bước tiếp theo [{0}].\r\nBạn có muốn tiếp tục không?", objNext.nvThaoTac.Name)) == System.Windows.Forms.DialogResult.No)
                //                return;
                //            else
                //                goto doo;
                //        }
                //    }
                //}

                //Check duplicate
                if (db.pgcBieuMaus.Where(p => p.MaBM == (int)lookBieuMau.EditValue & p.MaPGC == MaPGC).Count() > 0)
                {
                    DialogBox.Infomation("This template really exist on the system.");
                    return;
                }
            //doo:
                objBM.SoPhieu = txtSoPhieu.Text;
                objBM.NgayKy = dateNgayKy.DateTime;
                objBM.Lien = Convert.ToByte(spinLien.Value);
                objBM.Lan = Convert.ToByte(spinLan.Value);
                objBM.MaBM = (int)lookBieuMau.EditValue;
                objBM.DienGiai = txtDienGiai.Text;
                objBM.MaNV = BEE.ThuVien.Common.StaffID;
                objBM.NgayCN = DateTime.Now;
                objBM.MaLGD = MaLGD;
                objBM.IsTemplate = true;
                objBM.KeyBG = KeyBG;

                db.SubmitChanges();

                //db.gdNghiepVu_add(MaPGC, MaLNV, Convert.ToInt32(lookUpThaoTac.EditValue), BEE.ThuVien.Common.StaffID);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grvKhac_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            if (grvKhac.GetRowCellValue(e.RowHandle, "MaBT") == null)
            {
                e.ErrorText = "Vui lòng chọn [Ký hiệu], xin cảm ơn.";
                e.Valid = false;
            }
        }

        private void grvKhac_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            DialogBox.Error(e.ErrorText);
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void lookUpNghiepVu_EditValueChanged(object sender, EventArgs e)
        {
            //lookBieuMau.Properties.DataSource = db.daBieuMaus.Where(p => p.MaLBM == MaLNV).Select(p => new { 
            //    p.MaBM,
            //    p.MaTTac,
            //    p.TenBM
            //});
            //lookUpThaoTac.Properties.DataSource = db.nvFormActions.Where(p => p.FormID == MaLGD);
        }

        private void lookUpThaoTac_EditValueChanged(object sender, EventArgs e)
        {
            //lookUpThaoTac.Tag = Convert.ToBoolean(lookUpThaoTac.GetColumnValue("IsTemplate"));
        }

        private void lookBieuMau_EditValueChanged(object sender, EventArgs e)
        {
            //lookUpThaoTac.EditValue = Convert.ToInt32(lookBieuMau.GetColumnValue("MaTTac"));
        }
    }
}