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

namespace BEE.NhanVien.Agency
{
    public partial class frmProcess : DevExpress.XtraEditors.XtraForm
    {
        public Int64 MaNV { get; set; }
        public int Ktra { get; set; }

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
            try
            {
                using (var db = new MasterDataContext())
                {
                    if (MaNV != 0)
                    {
                        switch (Ktra)
                        {
                            case 0:
                                it.Agency.NhanVienCls o = new it.Agency.NhanVienCls();
                                o.MaNV = MaNV;
                                o.Lock = true;
                                o.LockStaff(Common.StaffID);

                                
                                break;
                            case 1:
                                it.Agency.NhanVienCls p = new it.Agency.NhanVienCls();
                                p.MaNV = MaNV;
                                p.Lock = false;
                                p.LockStaff(Common.StaffID);
                                break;
                            case 2:

                                break;
                        }

                        NhanVien_LichSu objHis = new NhanVien_LichSu();
                        objHis.MaNV = Common.StaffID;
                        objHis.GhiChu = memoComment.Text.Trim();
                        try
                        {
                            objHis.RefID = Convert.ToInt32(MaNV);
                        }
                        catch
                        {
                        }
                      

                        db.NhanVien_LichSus.InsertOnSubmit(objHis);
                        db.SubmitChanges();
                        
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