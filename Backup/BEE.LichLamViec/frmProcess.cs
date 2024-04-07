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

namespace BEE.LichLamViec
{
    public partial class frmProcess : DevExpress.XtraEditors.XtraForm
    {
        public int MaLH { get; set; }
        public int MaNVu { get; set; }
        public int MaNV { get; set; }

        public frmProcess()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmProcess_Load(object sender, EventArgs e)
        {            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (memoComment.Text.Trim() == "")
            {
                DialogBox.Warning("Vui lòng nhập [Ghi chú], xin cảm ơn.");
                memoComment.Focus();
                return;
            }

            try
            {
                using (var db = new MasterDataContext())
                {
                    if (MaLH != null)
                    {
                        var objLS = new lhLichSu();

                        objLS.MaLH = MaLH;
                        objLS.MaNV = Common.StaffID;
                        objLS.NgayNhap = DateTime.Now;
                        objLS.DienGiai = memoComment.Text;

                        db.lhLichSus.InsertOnSubmit(objLS);
                        db.SubmitChanges();
                    }
                    else
                    {
                        if (MaNVu != null)
                            db.NhiemVu_LichSu_add(MaNVu, Common.StaffID, memoComment.Text, null, null, null);
                    }
                }

                DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }
    }
}