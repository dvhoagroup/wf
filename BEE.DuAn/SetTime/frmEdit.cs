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

namespace BEE.DuAn.SetTime
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {

        public int? MaCD { get; set; }
        public bool IsSave { get; set; }
        MasterDataContext db = new MasterDataContext();
        daCaiDatTime objCDT;
        public frmEdit()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
        }

        void LoadData()
        {
            lookDuAn.Properties.DataSource = db.DuAns.Select(p => new { p.MaDA,p.TenDA});
            lookDVTKoTien.Properties.DataSource = db.daDonViTimes.Select(p => new { p.ID, p.TenDVT});
            lookDVTTonPhi.Properties.DataSource = db.daDonViTimes.Select(p => new { p.ID, p.TenDVT });
            if (MaCD == null)
            {
                lookDuAn.EditValue = null;
                lookDVTKoTien.EditValue = null;
                lookDVTTonPhi.EditValue = null;
                txtTenMuc.Text = "";
                dateDenNgay.EditValue = DateTime.Now;
                dateTuNgay.EditValue = DateTime.Now;
                spinGiuMienPhi.EditValue = 0;
                spinGiuTonPhi.EditValue = 0;
                spinSoNguoiGiu.EditValue = 0;
                spinTienDatCoc.EditValue = 0;
            }
            else
            {
                objCDT = db.daCaiDatTimes.Single(p => p.ID == MaCD);

                lookDuAn.EditValue = objCDT.MaDA;
                lookDVTKoTien.EditValue = (int?)objCDT.DVTKoTien;
                lookDVTTonPhi.EditValue = (int?)objCDT.DVTCoTien;
                txtTenMuc.Text = objCDT.TenMuc;
                dateDenNgay.EditValue = (DateTime?)objCDT.DenNgay;
                dateTuNgay.EditValue = (DateTime?)objCDT.TuNgay;
                spinGiuMienPhi.EditValue = (int?)objCDT.TimeGiuChoKoTien;
                spinGiuTonPhi.EditValue = (int?)objCDT.TimeGiuChoCoTien;
                spinSoNguoiGiu.EditValue = (int?)objCDT.SoNguoiGiu;
                spinTienDatCoc.EditValue = (decimal?)objCDT.TienDatCoc;
            }
        }
        void SaveData()
        {
            //Rang Buoc
            if (lookDuAn.EditValue == null)
            {
                DialogBox.Warning("Bạn cần chọn dự án để thiết lập thời gian. Xin cảm ơn!");
                lookDuAn.Focus();
                return;
            }

            var wait = DialogBox.WaitingForm();
            try
            {
                if (MaCD == null)
                {
                    objCDT = new daCaiDatTime();
                    objCDT.MaNV = Common.StaffID;
                    objCDT.NgayTao = DateTime.Now;
                    db.daCaiDatTimes.InsertOnSubmit(objCDT);
                }
                else
                {
                    objCDT = db.daCaiDatTimes.Single(p => p.ID == MaCD);
                    objCDT.MaNVCN = Common.StaffID;
                    objCDT.NgayCN = DateTime.Now;
                    //LS
                    daCaiDatTimeL objLS = new daCaiDatTimeL();
                    objLS.MaNVSua = Common.StaffID;
                    objLS.NgayCN = DateTime.Now;
                    objCDT.daCaiDatTimeLs.Add(objLS);
                }
                objCDT.MaDA = (int?)lookDuAn.EditValue;
                objCDT.TenMuc = txtTenMuc.Text.Trim();
                objCDT.TuNgay = (DateTime?)dateTuNgay.EditValue;
                objCDT.DenNgay = (DateTime?)dateDenNgay.EditValue;
                objCDT.TimeGiuChoCoTien = Convert.ToInt32(spinGiuTonPhi.Value);
                objCDT.TimeGiuChoKoTien = Convert.ToInt32(spinGiuMienPhi.EditValue);
                objCDT.DVTCoTien = (int)lookDVTTonPhi.EditValue;
                objCDT.DVTKoTien = (int)lookDVTKoTien.EditValue;
                objCDT.TienDatCoc = (decimal?)spinTienDatCoc.EditValue;
                objCDT.SoNguoiGiu = Convert.ToInt32(spinSoNguoiGiu.EditValue);

                db.SubmitChanges();
                this.IsSave = true;
            }
            catch { }
            finally
            {
                wait.Close();
                this.Close();
            }
         

        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            SaveData();
        }

  
    }
}