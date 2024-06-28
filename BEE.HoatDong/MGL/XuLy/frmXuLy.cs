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

namespace BEE.HoatDong.MGL.XuLy
{
    public partial class frmXuLy : DevExpress.XtraEditors.XtraForm
    {
        public int? maSP { get; set; }
        public frmXuLy()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                using (var db = new MasterDataContext())
                {
                    var obj = new mglspNhatKyXuLy();
                    var objSP = db.mglBCSanPhams.FirstOrDefault(p => p.ID == maSP);
                    var objCV = db.mglBCCongViecs.FirstOrDefault(p => p.ID == objSP.MaCV);
                    objCV.NgayXuLy = db.GetSystemDate();
                    db.mglspNhatKyXuLies.InsertOnSubmit(obj);
                    obj.MaTT = (int?)lookTrangThai.EditValue;
                    obj.MaSP = maSP;
                    obj.MaNV = Common.StaffID;
                    obj.NgayXL = (DateTime?)dateNgayXL.EditValue;
                    obj.NoiDung = txtNoiDung.Text.Trim();
                    db.SubmitChanges();

                    DialogBox.Infomation("Dữ liệu đã được lưu!");
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                DialogBox.Infomation("Lưu bị lỗi: " + ex.Message);
            }
            finally
            {
                wait.Close();
            }
        }

        private void frmXuLy_Load(object sender, EventArgs e)
        {
            using (var db = new MasterDataContext())
            {
                lookTrangThai.Properties.DataSource = db.mglbcTrangThaiXLs;
                dateNgayXL.EditValue = DateTime.Now;
                var objSP = db.mglBCSanPhams.FirstOrDefault(p => p.ID == maSP);
                lookTrangThai.EditValue = objSP.MaTT;
            }
        }

        private void btntestdata_Click(object sender, EventArgs e)
        {
            MasterDataContext db = new MasterDataContext();
            var obj = db.mglbcPhanQuyens.Single(p => p.MaNV == Common.StaffID);
            var tuNgay = DateTime.Now.AddYears(-3);
            var denNgay = DateTime.Now;

            var arrMaTT = "," + ",";

            var arrMaNV = "," + ",";
            int MaNV = Common.StaffID;
            var wait = DialogBox.WaitingForm();
            try
            {
                

                var data = db.gdGetManagerGiaoDichV2(tuNgay, denNgay, MaNV, true, -1, -1, -1, arrMaTT, arrMaNV).Select(p => new
                {
                    p.Code
                }).AsEnumerable().ToList();




            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            finally
            {
                wait.Close();
            }


        }
    }
}