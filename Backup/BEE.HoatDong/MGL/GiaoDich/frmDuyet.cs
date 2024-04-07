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

namespace BEE.HoatDong.MGL.GiaoDich
{
    public partial class frmDuyet : DevExpress.XtraEditors.XtraForm
    {
        public int ID, MaGD;
        public byte MaTT;

        MasterDataContext db = new MasterDataContext();
        mglgdNhatKyXuLy objNhatKy;

        public frmDuyet()
        {
            InitializeComponent();
        }

        private void frmDuyet_Load(object sender, EventArgs e)
        {
            if (this.ID != 0)
            {
                objNhatKy = db.mglgdNhatKyXuLies.Single(p => p.ID == this.ID);
                txtLyDo.Text = objNhatKy.DienGiai;
            }
            else
            {
                objNhatKy = new mglgdNhatKyXuLy();
            }
        }

        private void btnThucHien_Click(object sender, EventArgs e)
        {
            objNhatKy.DienGiai = txtLyDo.Text;
            if (this.ID == 0)
            {
                objNhatKy.NgayXL = DateTime.Now;
                objNhatKy.MaTT = this.MaTT;
                objNhatKy.MaNV = Common.StaffID;
                objNhatKy.mglgdGiaoDich = db.mglgdGiaoDiches.Single(p => p.MaGD == this.MaGD);
                objNhatKy.mglgdGiaoDich.MaTT = this.MaTT;
                switch (this.MaTT)
                {
                    case 2: //Duyet                        
                        objNhatKy.mglgdGiaoDich.mglbcBanChoThue.MaTT = 4;
                        objNhatKy.mglgdGiaoDich.mglbcBanChoThue.TrangThaiHDMG = 2;
                        objNhatKy.mglgdGiaoDich.mglmtMuaThue.MaTT = 4;
                        objNhatKy.mglgdGiaoDich.mglmtMuaThue.TrangThaiHDMG = 2;
                        break;
                    case 3: //Không duyệt                        
                        objNhatKy.mglgdGiaoDich.mglbcBanChoThue.MaTT = 0;
                        objNhatKy.mglgdGiaoDich.mglmtMuaThue.MaTT = 0;
                        break;
                    case 4: //Da thu tien
                    case 5: //Chua thu tien
                        //objNhatKy.mglgdGiaoDich.mglbcBanChoThue.MaTT = 2;
                        //objNhatKy.mglgdGiaoDich.mglmtMuaThue.MaTT = 3;
                        break;
                    case 6: //Giao dich thanh cong--ngừng giao dịch
                        objNhatKy.mglgdGiaoDich.mglbcBanChoThue.MaTT = 3;
                        objNhatKy.mglgdGiaoDich.mglmtMuaThue.MaTT = 3;
                        break;
                }
            }
            db.SubmitChanges();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}