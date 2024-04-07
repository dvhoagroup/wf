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

namespace BEEREMA.Project
{
    public partial class frmApprove : DevExpress.XtraEditors.XtraForm
    {
        public int? ProjectID { get; set; }
        public bool IsApprove { get; set; }

        public frmApprove()
        {
            InitializeComponent();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = new MasterDataContext())
                {
                    var obj = db.DuAns.SingleOrDefault(p => p.MaDA == ProjectID);
                    if (obj.IsApprove.GetValueOrDefault() != IsApprove)
                    {
                        obj.IsApprove = IsApprove;
                        obj.ApproverID = Common.StaffID;
                        obj.DateOfApproval = db.GetSystemDate();
                        obj.Description = txtDienGiai.Text;

                        //
                        var objLS = new daLichSu();
                        objLS.DienGiai = txtDienGiai.Text;
                        objLS.IsApprove = IsApprove;
                        objLS.DuAn = obj;
                        objLS.MaNV = Common.StaffID;
                        db.daLichSus.InsertOnSubmit(objLS);

                        db.SubmitChanges();
                        DialogBox.Infomation();
                        DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                DialogBox.Error("Đã có lỗi xảy ra. Code: " + ex.Message);
            }
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}