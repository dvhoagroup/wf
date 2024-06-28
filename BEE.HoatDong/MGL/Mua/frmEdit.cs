using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using System.Net.Mail;
using it;
using System.Threading;
using BEE.ThuVien;
using BEE.NghiepVuKhac;
using BEEREMA;
using System.Text.RegularExpressions;

namespace BEE.HoatDong.MGL.Mua
{
    public partial class frmEdit : DevExpress.XtraEditors.XtraForm
    {
        public int MaMT = 0;
        public bool IsSave;
        public int? MaKH { get; set; }
        private MasterDataContext db = new MasterDataContext();
        private mglmtMuaThue objMT;
        mglmtLichSu objLS = new mglmtLichSu();
        BEE.ThuVien.KhachHang objKH;
        public bool AllowSave = true;
        public bool DislayContac = true;
        public bool? NhuCau { get; set; }
        public bool isMua;
        public frmEdit()
        {
            InitializeComponent();
        }
        public void PhanQuyenThemSuaKH()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = Common.PerID;
            o.AccessData.Form.FormID = 9;
            DataTable tblAction = o.SelectBy();
            txtHoTenKH.Properties.Buttons[1].Visible = false;
            txtHoTenKH.Properties.Buttons[2].Visible = false;
            if (tblAction.Rows.Count > 0)
            {
                foreach (DataRow r in tblAction.Rows)
                {
                    switch (byte.Parse(r["FeatureID"].ToString()))
                    {
                        case 1:
                            txtHoTenKH.Properties.Buttons[1].Visible = true;
                            break;
                        case 2:
                            txtHoTenKH.Properties.Buttons[2].Visible = true;
                            break;

                    }
                }
            }
        }

        void LoadPermission()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = Common.PerID;
            if (objMT.IsMua == true)
            {
                o.AccessData.Form.FormID = 176;
            }
            else
            {
                o.AccessData.Form.FormID = 177;
            }

            DataTable tblAction = o.SelectBy();


            if (tblAction.Rows.Count > 0)
            {
                foreach (DataRow r in tblAction.Rows)
                {
                    switch (byte.Parse(r["FeatureID"].ToString()))
                    {

                        case 87://nhân viên quản lý
                            lookNhanVienQL.Enabled = true;
                            break;

                    }
                }
            }
        }
        public void createKyHieu()
        {

            try
            {
                //DateTime now = DateTime.Now;
                //DateTime dstart = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
                //DateTime dend = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);

                //var data = db.mglmtMuaThues.Where(o => o.NgayNhap >= dstart && o.NgayNhap <= dend).OrderByDescending(o => o.NgayNhap).FirstOrDefault();
                //string sophieu = "";
                //if (data != null)
                //{
                //    if (string.IsNullOrEmpty(data.SoDK))
                //    {
                //        sophieu = String.Format("{0:yyyyMMdd}", now) + "00001";

                //    }
                //    else
                //    {
                //        sophieu = NextID(data.SoDK, String.Format("{0:yyyyMMdd}", now));
                //    }
                //}
                //else
                //{
                //    sophieu = String.Format("{0:yyyyMMdd}", now) + "00001";
                //}

                //txtSoDK.Text = "DKMT-" + sophieu;

                var formatDate = db.ExecuteQuery<DateTime>("SELECT GETDATE()").FirstOrDefault();
                txtSoDK.Text = "DKMT-" + formatDate.ToString("yyyyMMddHHmmss");
               // txtKyHieu.Text = "BC-" + formatDate.ToString("yyyyMMddHHmmss");


            }
            catch (Exception ex)
            {
            }
        }

        public string NextID(string lastID, string prefixID)
        {
            if (lastID == "")
            {
                return prefixID + "00001";  // fixwidth default
            }
            var nextID = Int64.Parse(lastID.Remove(0, prefixID.Length)) + 1;
            int lengthNumerID = lastID.Length - prefixID.Length;
            string zeroNumber = "";
            for (int i = 1; i <= lengthNumerID; i++)
            {
                if (nextID < Math.Pow(10, i))
                {
                    for (int j = 1; j <= lengthNumerID - i; i++)
                    {
                        zeroNumber += "0";
                    }
                    return prefixID + zeroNumber + nextID.ToString();
                }
            }
            return prefixID + nextID;

        }
        private void frmEdit_Load(object sender, EventArgs e)
        {
            lookNhanVienQL.Enabled = false;
            lookNhanVienQL.Properties.DataSource = lookNVKD.Properties.DataSource = db.NhanViens.Where(p => p.MaTinhTrang == 1).Select(p => new { p.MaNV, p.HoTen });
            lookTinh.Properties.DataSource = db.Tinhs;
            lookLoaiBDS.Properties.DataSource = db.LoaiBDs;
            cmbHuongBC.Properties.DataSource = db.PhuongHuongs.ToList();
            cmbHuongCua.Properties.DataSource = db.PhuongHuongs.ToList();
            cmbPhapLy.Properties.DataSource = db.PhapLies;
            cmbTienIch.Properties.DataSource = db.TienIches;
            cmbLoaiDuong.Properties.DataSource = db.LoaiDuongs;
            lookNguon.Properties.DataSource = db.mglNguons;
            lookDuAn.Properties.DataSource = db.DuAns.Select(p => new { p.MaDA, p.TenDA });
            lookMucDich.Properties.DataSource = db.mglmtMuDichMTs;
            lookTrangThai.Properties.DataSource = db.mglmtTrangThais;
            lookTrangThaiHDMB.Properties.DataSource = db.mglbcTrangThaiHDMGs;
            itemLuu.Enabled = itemXoa.Enabled = itemSua.Enabled = AllowSave;
            lbDuong1.DataSource = db.Duongs.ToList();
            lookLoaiTien.Properties.DataSource = db.LoaiTiens;
            PhanQuyenThemSuaKH();
            if (MaMT > 0)
            {
                objMT = db.mglmtMuaThues.Single(p => p.MaMT == MaMT);
                LoadPermission();
                txtSoDK.EditValue = objMT.SoDK;
                dateNgayDK.EditValue = objMT.NgayDK;
                lookTrangThaiHDMB.EditValue = objMT.TrangThaiHDMG;
                lookNVKD.EditValue = objMT.MaNVKD;
                lookNhanVienQL.EditValue = objMT.MaNVKT;
                lookNguon.EditValue = objMT.MaNguon;
                lookTrangThai.EditValue = objMT.MaTT;
                //Nhu cau
                rdbLoaiTin.EditValue = objMT.IsMua;
                lookLoaiBDS.EditValue = objMT.MaLBDS;
                //Phap ly
                string phapLy = "";
                foreach (mglmtPhapLy p in objMT.mglmtPhapLies)
                    phapLy += p.MaPL + ", ";
                phapLy = phapLy.TrimEnd(' ').TrimEnd(',');
                cmbPhapLy.SetEditValue(phapLy);
                lookDuAn.EditValue = objMT.MaDA;
                //Tien ich
                string tienIch = "";
                foreach (mglmtTienIch t in objMT.mglmtTienIches)
                    tienIch += t.MaTI + ", ";
                tienIch = tienIch.TrimEnd(' ').TrimEnd(',');
                cmbTienIch.SetEditValue(tienIch);
                //Huong
                string huong = "";
                foreach (mglmtHuong h in objMT.mglmtHuongs)
                    huong += h.MaHuong + ", ";
                huong = huong.TrimEnd(' ').TrimEnd(',');
                cmbHuongCua.SetEditValue(huong);

                string huongBC = "";
                foreach (mglmtHuongBC h in objMT.mglmtHuongBCs)
                    huongBC += h.MaHuong + ", ";
                huongBC = huongBC.TrimEnd(' ').TrimEnd(',');
                cmbHuongBC.SetEditValue(huongBC);
                spinGiaDen.EditValue = objMT.GiaDen;
                txtGioiThieu.InnerHtml = objMT.GhiChu;
                spinDienTichTu.EditValue = objMT.DienTichTu;
                spinMatTienTu.EditValue = objMT.MatTienTu;
                spinMatTienDen.EditValue = objMT.MatTienDien;
                spinPMG.EditValue = objMT.PhiMG ?? 0;
                spinPhNguTu.EditValue = objMT.PhNguTu;
                spinPVSTu.EditValue = objMT.PVSTu;
                //Loai duong
                string duong = "";
                foreach (mglmtLoaiDuong d in objMT.mglmtLoaiDuongs)
                    duong += d.MaLD + ", ";
                duong = duong.TrimEnd(' ').TrimEnd(',');
                cmbLoaiDuong.SetEditValue(duong);
                spinDuongRongTu.EditValue = objMT.DuongRongTu;
                spinTangTu.EditValue = objMT.TangTu;
                spinTangDen.EditValue = objMT.TangDen;
                //Huyen
                lookTinh.ItemIndex = 0;
                lbHuyen2.DataSource = objMT.mglmtHuyens.Select(p => p.Huyen).ToList();
                ckbCanGoc.Checked = objMT.IsCanGoc ?? false;
                ckbCoThangMay.Checked = objMT.IsThangMay ?? false;
                ckbCoTangHam.Checked = objMT.IsTangHam ?? false;
                ckbDoOto.Checked = objMT.IsDeOto ?? false;
                lookMucDich.EditValue = objMT.MaMD;

                txtMoHInh.Text = objMT.MoHinh;
                lookLoaiTien.EditValue = objMT.MaLT;
                try
                {
                    lookTinh.EditValue = (byte?)objMT.MaTinh;
                }
                catch { }

                lbDuong2.DataSource = objMT.mglmtDuongs.Select(p => p.Duong).ToList();
                //Khach hang
                objKH = objMT.KhachHang;
                KhachHang_Load();



                #region phan quyen sdt
                try
                {
                    int? sbid;
                    if ((bool?)rdbLoaiTin.EditValue == true)
                    {
                        it.AccessDataCls o = new it.AccessDataCls(Common.PerID, 176);
                        sbid = o.SDB.SDBID;
                    }
                    else
                    {
                        it.AccessDataCls o = new it.AccessDataCls(Common.PerID, 177);
                        sbid = o.SDB.SDBID;

                    }

                    var obj = db.mglmtPhanQuyens.Single(p => p.MaNV == Common.StaffID);
                    switch (sbid)
                    {
                        case 1:
                            if (obj.DienThoai == false)
                            {
                                txtDiDong.Text = Common.Right(objKH.DiDong, 3);
                                txtDiDong2.Text = Common.Right(objKH.DiDong2, 3);
                                txtDiDong3.Text = Common.Right(objKH.DiDong3, 3);
                                txtDiDong4.Text = Common.Right(objKH.DiDong4, 3);
                            }
                            else if (obj.DienThoai3Dau == false)
                            {
                                txtDiDong.Text = Common.Right1(objKH.DiDong, 3);
                                txtDiDong2.Text = Common.Right1(objKH.DiDong2, 3);
                                txtDiDong3.Text = Common.Right1(objKH.DiDong3, 3);
                                txtDiDong4.Text = Common.Right1(objKH.DiDong4, 3);
                            }
                            else if (obj.DienThoaiAn == false)
                            {
                                txtDiDong.Text = null;
                                txtDiDong2.Text = null;
                                txtDiDong3.Text = null;
                                txtDiDong4.Text = null;
                            }
                            else
                            {
                                txtDiDong.Text = objKH.DiDong;
                                txtDiDong2.Text = objKH.DiDong2;
                                txtDiDong3.Text = objKH.DiDong3;
                                txtDiDong4.Text = objKH.DiDong4;

                            }
                            break;

                        case 2:
                            if (obj.DienThoai == false)
                            {
                                txtDiDong.Text = Common.Right(objKH.DiDong, 3);
                                txtDiDong2.Text = Common.Right(objKH.DiDong2, 3);
                                txtDiDong3.Text = Common.Right(objKH.DiDong3, 3);
                                txtDiDong4.Text = Common.Right(objKH.DiDong4, 3);
                            }
                            else if (obj.DienThoai3Dau == false)
                            {
                                txtDiDong.Text = Common.Right1(objKH.DiDong, 3);
                                txtDiDong2.Text = Common.Right1(objKH.DiDong2, 3);
                                txtDiDong3.Text = Common.Right1(objKH.DiDong3, 3);
                                txtDiDong4.Text = Common.Right1(objKH.DiDong4, 3);
                            }
                            else if (obj.DienThoaiAn == false)
                            {
                                txtDiDong.Text = null;
                                txtDiDong2.Text = null;
                                txtDiDong3.Text = null;
                                txtDiDong4.Text = null;
                            }
                            else
                            {
                                txtDiDong.Text = objKH.DiDong;
                                txtDiDong2.Text = objKH.DiDong2;
                                txtDiDong3.Text = objKH.DiDong3;
                                txtDiDong4.Text = objKH.DiDong4;

                            }

                            break;
                        case 3:
                            if (obj.DienThoai == false)
                            {
                                txtDiDong.Text = Common.Right(objKH.DiDong, 3);
                                txtDiDong2.Text = Common.Right(objKH.DiDong2, 3);
                                txtDiDong3.Text = Common.Right(objKH.DiDong3, 3);
                                txtDiDong4.Text = Common.Right(objKH.DiDong4, 3);
                            }
                            else if (obj.DienThoai3Dau == false)
                            {
                                txtDiDong.Text = Common.Right1(objKH.DiDong, 3);
                                txtDiDong2.Text = Common.Right1(objKH.DiDong2, 3);
                                txtDiDong3.Text = Common.Right1(objKH.DiDong3, 3);
                                txtDiDong4.Text = Common.Right1(objKH.DiDong4, 3);
                            }
                            else if (obj.DienThoaiAn == false)
                            {
                                txtDiDong.Text = null;
                                txtDiDong2.Text = null;
                                txtDiDong3.Text = null;
                                txtDiDong4.Text = null;
                            }
                            else
                            {
                                txtDiDong.Text = objKH.DiDong;
                                txtDiDong2.Text = objKH.DiDong2;
                                txtDiDong3.Text = objKH.DiDong3;
                                txtDiDong4.Text = objKH.DiDong4;

                            }

                            break;
                        case 4:
                            if (obj.DienThoai == false)
                            {
                                txtDiDong.Text = Common.Right(objKH.DiDong, 3);
                                txtDiDong2.Text = Common.Right(objKH.DiDong2, 3);
                                txtDiDong3.Text = Common.Right(objKH.DiDong3, 3);
                                txtDiDong4.Text = Common.Right(objKH.DiDong4, 3);
                            }
                            else if (obj.DienThoai3Dau == false)
                            {
                                txtDiDong.Text = Common.Right1(objKH.DiDong, 3);
                                txtDiDong2.Text = Common.Right1(objKH.DiDong2, 3);
                                txtDiDong3.Text = Common.Right1(objKH.DiDong3, 3);
                                txtDiDong4.Text = Common.Right1(objKH.DiDong4, 3);
                            }
                            else if (obj.DienThoaiAn == false)
                            {
                                txtDiDong.Text = null;
                                txtDiDong2.Text = null;
                                txtDiDong3.Text = null;
                                txtDiDong4.Text = null;
                            }
                            else
                            {
                                txtDiDong.Text = objKH.DiDong;
                                txtDiDong2.Text = objKH.DiDong2;
                                txtDiDong3.Text = objKH.DiDong3;
                                txtDiDong4.Text = objKH.DiDong4;

                            }
                            break;



                    }
                }
                catch (Exception)
                {

                    throw;
                }
                #endregion
            }
            else
            {
                dateNgayDK.EditValue = DateTime.Now;
                //lookNhanVienQL.EditValue = lookNVKD.EditValue = Common.StaffID;
                lookTrangThaiHDMB.EditValue = 2;
                lookNhanVienQL.Enabled = true;
                SetAddNew();

            }
            if (this.MaKH != null)
            {
                this.objKH = db.KhachHangs.SingleOrDefault(p => p.MaKH == this.MaKH);
                KhachHang_Load();
                PhanQuyenSDT();
            }
            if (this.MaMT == 0)
            {
                SetAddNew();
            }
        }

        public void PhanQuyenSDT()
        {
            try
            {
                int? sbid;
                if ((bool?)rdbLoaiTin.EditValue == true)
                {
                    it.AccessDataCls o = new it.AccessDataCls(Common.PerID, 176);
                    sbid = o.SDB.SDBID;
                }
                else
                {
                    it.AccessDataCls o = new it.AccessDataCls(Common.PerID, 177);
                    sbid = o.SDB.SDBID;

                }

                var obj = db.mglmtPhanQuyens.Single(p => p.MaNV == Common.StaffID);
                switch (sbid)
                {
                    case 1:
                        if (obj.DienThoai == false)
                        {
                            txtDiDong.Text = Common.Right(objKH.DiDong, 3);
                            txtDiDong2.Text = Common.Right(objKH.DiDong2, 3);
                            txtDiDong3.Text = Common.Right(objKH.DiDong3, 3);
                            txtDiDong4.Text = Common.Right(objKH.DiDong4, 3);
                        }
                        else if (obj.DienThoai3Dau == false)
                        {
                            txtDiDong.Text = Common.Right1(objKH.DiDong, 3);
                            txtDiDong2.Text = Common.Right1(objKH.DiDong2, 3);
                            txtDiDong3.Text = Common.Right1(objKH.DiDong3, 3);
                            txtDiDong4.Text = Common.Right1(objKH.DiDong4, 3);
                        }
                        else if (obj.DienThoaiAn == false)
                        {
                            txtDiDong.Text = null;
                            txtDiDong2.Text = null;
                            txtDiDong3.Text = null;
                            txtDiDong4.Text = null;
                        }
                        else
                        {
                            txtDiDong.Text = objKH.DiDong;
                            txtDiDong2.Text = objKH.DiDong2;
                            txtDiDong3.Text = objKH.DiDong3;
                            txtDiDong4.Text = objKH.DiDong4;

                        }
                        break;

                    case 2:
                        if (obj.DienThoai == false)
                        {
                            txtDiDong.Text = Common.Right(objKH.DiDong, 3);
                            txtDiDong2.Text = Common.Right(objKH.DiDong2, 3);
                            txtDiDong3.Text = Common.Right(objKH.DiDong3, 3);
                            txtDiDong4.Text = Common.Right(objKH.DiDong4, 3);
                        }
                        else if (obj.DienThoai3Dau == false)
                        {
                            txtDiDong.Text = Common.Right1(objKH.DiDong, 3);
                            txtDiDong2.Text = Common.Right1(objKH.DiDong2, 3);
                            txtDiDong3.Text = Common.Right1(objKH.DiDong3, 3);
                            txtDiDong4.Text = Common.Right1(objKH.DiDong4, 3);
                        }
                        else if (obj.DienThoaiAn == false)
                        {
                            txtDiDong.Text = null;
                            txtDiDong2.Text = null;
                            txtDiDong3.Text = null;
                            txtDiDong4.Text = null;
                        }
                        else
                        {
                            txtDiDong.Text = objKH.DiDong;
                            txtDiDong2.Text = objKH.DiDong2;
                            txtDiDong3.Text = objKH.DiDong3;
                            txtDiDong4.Text = objKH.DiDong4;

                        }

                        break;
                    case 3:
                        if (obj.DienThoai == false)
                        {
                            txtDiDong.Text = Common.Right(objKH.DiDong, 3);
                            txtDiDong2.Text = Common.Right(objKH.DiDong2, 3);
                            txtDiDong3.Text = Common.Right(objKH.DiDong3, 3);
                            txtDiDong4.Text = Common.Right(objKH.DiDong4, 3);
                        }
                        else if (obj.DienThoai3Dau == false)
                        {
                            txtDiDong.Text = Common.Right1(objKH.DiDong, 3);
                            txtDiDong2.Text = Common.Right1(objKH.DiDong2, 3);
                            txtDiDong3.Text = Common.Right1(objKH.DiDong3, 3);
                            txtDiDong4.Text = Common.Right1(objKH.DiDong4, 3);
                        }
                        else if (obj.DienThoaiAn == false)
                        {
                            txtDiDong.Text = null;
                            txtDiDong2.Text = null;
                            txtDiDong3.Text = null;
                            txtDiDong4.Text = null;
                        }
                        else
                        {
                            txtDiDong.Text = objKH.DiDong;
                            txtDiDong2.Text = objKH.DiDong2;
                            txtDiDong3.Text = objKH.DiDong3;
                            txtDiDong4.Text = objKH.DiDong4;

                        }

                        break;
                    case 4:
                        if (obj.DienThoai == false)
                        {
                            txtDiDong.Text = Common.Right(objKH.DiDong, 3);
                            txtDiDong2.Text = Common.Right(objKH.DiDong2, 3);
                            txtDiDong3.Text = Common.Right(objKH.DiDong3, 3);
                            txtDiDong4.Text = Common.Right(objKH.DiDong4, 3);
                        }
                        else if (obj.DienThoai3Dau == false)
                        {
                            txtDiDong.Text = Common.Right1(objKH.DiDong, 3);
                            txtDiDong2.Text = Common.Right1(objKH.DiDong2, 3);
                            txtDiDong3.Text = Common.Right1(objKH.DiDong3, 3);
                            txtDiDong4.Text = Common.Right1(objKH.DiDong4, 3);
                        }
                        else if (obj.DienThoaiAn == false)
                        {
                            txtDiDong.Text = null;
                            txtDiDong2.Text = null;
                            txtDiDong3.Text = null;
                            txtDiDong4.Text = null;
                        }
                        else
                        {
                            txtDiDong.Text = objKH.DiDong;
                            txtDiDong2.Text = objKH.DiDong2;
                            txtDiDong3.Text = objKH.DiDong3;
                            txtDiDong4.Text = objKH.DiDong4;

                        }
                        break;



                }
            }
            catch (Exception)
            {

                throw;
            }


        }

        void SetEnableControl(bool enabel)
        {
            grKhachHang.Enabled = enabel;
            grPhieuDK.Enabled = enabel;
            grBDS.Enabled = enabel;
            //nut chuc nang
            itemThem.Enabled = !enabel;
            itemSua.Enabled = !enabel;
            itemLuu.Enabled = enabel;
            itemHoan.Enabled = enabel;
            itemXoa.Enabled = this.MaMT != 0;
            itemIn.Enabled = this.MaMT != 0;
        }

        void SetAddNew()
        {
            objMT = new mglmtMuaThue();
            createKyHieu();
            this.MaMT = 0;
            string soDK = "";
            //  db.mglmtMuaThue_TaoSoPhieu(ref soDK);
            //  txtSoDK.EditValue = soDK;
            itemDinhKem.Tag = null;
            //Khach hang
            if (this.MaKH == null)
                KhachHang_AddNew();
            //Bat dong san
            spinGiaDen.EditValue = 0;
            spinDienTichTu.EditValue = 0;
            spinMatTienDen.EditValue = 0;
            spinMatTienTu.EditValue = 0;
            spinPhNguTu.EditValue = 0;
            spinDuongRongTu.EditValue = 0;
            lookLoaiBDS.EditValue = null;
            rdbLoaiTin.EditValue = this.NhuCau;
            cmbHuongCua.SetEditValue(null);
            cmbPhapLy.SetEditValue(null);
            cmbLoaiDuong.SetEditValue(null);
            cmbTienIch.SetEditValue(null);
            lookTinh.ItemIndex = 0;
            lbHuyen2.DataSource = objMT.mglmtHuyens.Select(p => p.Huyen).ToList();
            lbDuong2.DataSource = objMT.mglmtDuongs.Select(p => p.Duong).ToList();

        }

        void KhachHang_Load()
        {
            dateNgaySinh.EditValue = objKH.NgaySinh;
            txtNoiSinh.EditValue = objKH.NguyenQuan;
            txtSoCMND.EditValue = objKH.SoCMND;
            dateNgayCap.EditValue = objKH.NgayCap;
            txtNoiCap.EditValue = objKH.NoiCap;
            txtThuongTru.EditValue = objKH.ThuongTru;
            if (objKH.Xa != null)
            {
                btnDCTT.EditValue = string.Format("{0}, {1}, {2}", objKH.Xa.TenXa,
                    objKH.Xa.Huyen.TenHuyen, objKH.Xa.Huyen.Tinh.TenTinh);
            }
            txtDiaChiLienHe.EditValue = objKH.DiaChi;
            if (objKH.Xa1 != null)
            {
                btnDCLH.EditValue = string.Format("{0}, {1}, {2}", objKH.Xa1.TenXa,
                    objKH.Xa1.Huyen.TenHuyen, objKH.Xa1.Huyen.Tinh.TenTinh);
            }
            if (DislayContac)
            {
                txtHoTenKH.EditValue = objKH.HoKH + " " + objKH.TenKH;
                txtDienThoaiCD.EditValue = objKH.DienThoaiCT;
                txtDiDong.EditValue = objKH.DiDong;
                txtDiDong2.Text = objKH.DiDong;
                txtDiDong3.Text = objKH.DiDong3;
                txtDiDong4.Text = objKH.DiDong4;
                txtEmail.EditValue = objKH.Email;

                try
                {
                    if (objKH.MaKH != objMT.MaKH)
                        objLS.TenKH = objKH.HoKH + " " + objKH.TenKH;
                }
                catch
                {
                }


                if (txtDienThoaiCD.EditValue != objKH.DienThoaiCT)
                    objLS.SoDienThoai = txtDienThoaiCD.Text;
                if (txtEmail.EditValue != objKH.Email)
                    objLS.Email = txtEmail.Text;
                if (txtDiaChiLienHe.EditValue != objKH.DiaChi)
                    objLS.DiaChi = txtDiaChiLienHe.Text;
            }

        }

        private void KhachHang_AddNew()
        {
            objKH = new BEE.ThuVien.KhachHang();
            objKH.IsPersonal = true;
            txtHoTenKH.EditValue = null;
            txtSoCMND.EditValue = null;
            dateNgayCap.EditValue = null;
            txtNoiCap.EditValue = null;
            dateNgaySinh.EditValue = null;
            txtNoiSinh.EditValue = null;
            txtDienThoaiCD.EditValue = null;
            txtDiDong.EditValue = null;
            txtEmail.EditValue = null;
            txtThuongTru.EditValue = null;
            btnDCTT.EditValue = null;
            txtDiaChiLienHe.EditValue = null;
            btnDCLH.EditValue = null;
        }

        private void btnDCTT_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            SelectPosition_frm frm = new SelectPosition_frm();
            frm.MaXa = objKH.MaXa.GetValueOrDefault();
            frm.ShowDialog();
            if (frm.MaXa != 0)
            {
                objKH.Xa = db.Xas.Single(p => p.MaXa == frm.MaXa);
                btnDCTT.Text = frm.Result;
            }
        }

        private void btnDCLH_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            SelectPosition_frm frm = new SelectPosition_frm();
            frm.MaXa = objKH.MaXa2.GetValueOrDefault();
            frm.ShowDialog();
            if (frm.MaXa != 0)
            {
                objKH.Xa1 = db.Xas.Single(p => p.MaXa == frm.MaXa);
                btnDCLH.Text = frm.Result;
            }
        }

        private void picAddressKH_Click(object sender, EventArgs e)
        {
            txtDiaChiLienHe.Text = txtThuongTru.Text;
            btnDCLH.Text = btnDCTT.Text;
            objKH.Xa1 = objKH.Xa;
        }

        private void txtHoTenKH_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            switch (e.Button.Index)
            {
                case 0:
                    KhachHang.Find_frm frm = new KhachHang.Find_frm();
                    frm.LoaiNC = 2;
                    frm.ShowDialog();
                    if (frm.MaKH != 0)
                    {
                        objKH = db.KhachHangs.Single(p => p.MaKH == frm.MaKH);
                        KhachHang_Load();
                        return;
                    }
                    break;
                case 1:
                    KhachHang_AddNew();
                    KhachHang.KhachHang_frm frmkh = new KhachHang.KhachHang_frm();
                    frmkh.ShowDialog();
                    if (frmkh.MaKH != 0)
                    {
                        objKH = db.KhachHangs.Single(p => p.MaKH == frmkh.MaKH);
                        KhachHang_Load();
                        return;
                    }
                    break;
                case 2:
                    KhachHang.KhachHang_frm frmSKH = new KhachHang.KhachHang_frm();
                    frmSKH.MaKH = objKH.MaKH;
                    frmSKH.ShowDialog();
                    if (frmSKH.MaKH != 0)
                    {
                        objKH = db.KhachHangs.Single(p => p.MaKH == frmSKH.MaKH);
                        KhachHang_Load();
                        return;
                    }
                    break;
            }
        }

        private void lookTinh_EditValueChanged(object sender, EventArgs e)
        {
            lbHuyen1.DataSource = db.Huyens.Where(p => p.MaTinh == (byte)lookTinh.EditValue).ToList();
        }

        private void Huyen_Add()
        {
            List<Huyen> listHuyen1 = (List<Huyen>)lbHuyen1.DataSource;
            List<Huyen> listHuyen2 = (List<Huyen>)lbHuyen2.DataSource;

            List<Huyen> select = new List<Huyen>();
            foreach (Huyen h in lbHuyen1.SelectedItems)
            {
                select.Add(h);
            }

            foreach (Huyen h in select)
            {
                if (listHuyen2.IndexOf(h) < 0)
                    listHuyen2.Add(h);
                listHuyen1.Remove(h);
            }

            lbHuyen1.Refresh();
            lbHuyen2.Refresh();
        }

        private void Huyen_Remove()
        {
            List<Huyen> listHuyen1 = (List<Huyen>)lbHuyen1.DataSource;
            List<Huyen> listHuyen2 = (List<Huyen>)lbHuyen2.DataSource;

            List<Huyen> select = new List<Huyen>();
            foreach (Huyen h in lbHuyen2.SelectedItems)
            {
                select.Add(h);
            }

            foreach (Huyen h in select)
            {
                if (listHuyen1.IndexOf(h) < 0)
                    listHuyen1.Add(h);
                listHuyen2.Remove(h);
            }

            lbHuyen1.Refresh();
            lbHuyen2.Refresh();
        }

        private void btnThemHuyen_Click(object sender, EventArgs e)
        {
            Huyen_Add();
        }

        private void btnThemHuyenTatCa_Click(object sender, EventArgs e)
        {
            lbHuyen1.SelectAll();
            Huyen_Add();
        }

        private void btnXoaHuyen_Click(object sender, EventArgs e)
        {
            Huyen_Remove();
        }

        private void btnXoaHuyenTatCa_Click(object sender, EventArgs e)
        {
            lbHuyen2.SelectAll();
            Huyen_Remove();
        }

        private void itemThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetAddNew();
            SetEnableControl(true);
        }

        private void itemSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetEnableControl(true);
        }

        private void itemXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.IsSave = true;
            SetAddNew();
            SetEnableControl(true);
        }

        void Save()
        {
            #region rang buoc
            if (txtSoDK.Text.Trim() == "")
            {
                DialogBox.Error("Vui lòng nhập số đăng ký");
                txtSoDK.Focus();
                return;
            }
            else
            {
                var count = db.mglmtMuaThues.Where(p => p.SoDK == txtSoDK.Text.Trim() & p.MaMT != MaMT).Count();
                if (count > 0)
                {
                    DialogBox.Error("Số đăng ký <" + txtSoDK.Text + "> đã có trong hệ thống. Vui lòng nhập lại.");
                    txtSoDK.Focus();
                    return;
                }
            }


            if (lookLoaiTien.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn loại tiền!");
                lookLoaiTien.Focus();
                return;
            }

            if (lbHuyen2.Text == "")
            {
                DialogBox.Error("Vui lòng nhập khu vực muốn đăng ký!");
                lbHuyen2.Focus();
                return;
            }

            if (spinGiaDen.Value <= 0)
            {
                DialogBox.Error("Vui lòng nhập mức giá!");
                spinGiaDen.Focus();
                return;
            }
            if (lookTrangThai.EditValue == null)
            {
                DialogBox.Infomation("Vui lòng nhập trạng thái!");
                lookTrangThai.Focus();
                return;
            }

            //if (txtHoTenKH.Text.Trim() == "")
            //{
            //    DialogBox.Error("Vui lòng nhập [Khách hàng]");
            //    txtHoTenKH.Focus();
            //    return;
            //}

            //if (txtSoCMND.Text.Trim() != "")
            //{
            //    var count = db.KhachHangs.Where(p => p.SoCMND == txtSoCMND.Text.Trim() & p.MaKH != objKH.MaKH).Count();
            //    if (count > 0)
            //    {
            //        DialogBox.Error("Số CMND <" + txtSoCMND.Text + "> đã có trong hệ thống. Vui lòng nhập lại số CMND. Xin cảm ơn");
            //        txtSoCMND.Focus();
            //        return;
            //    }
            //}

            if (lookLoaiBDS.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn loại BĐS");
                lookLoaiBDS.Focus();
                return;
            }

            if (lookNhanVienQL.EditValue == null & lookNVKD.EditValue == null)
            {
                DialogBox.Error("Vui lòng chọn loại Nhân viên");
                return;
            }

            #endregion

            UpdateLichSu();
            objMT.SoDK = txtSoDK.Text;
            objMT.NgayDK = dateNgayDK.DateTime;
            objMT.NgayCN = db.GetSystemDate();
            objMT.TrangThaiHDMG = (byte?)lookTrangThaiHDMB.EditValue;
            objMT.MaNVKD = (int)lookNVKD.EditValue;
            //objMT.MaNVKT = Common.StaffID;
            objMT.MaNVKT = (int)lookNhanVienQL.EditValue;
            objMT.MaNguon = (short?)lookNguon.EditValue;
            objMT.MaTT = (byte?)lookTrangThai.EditValue;
            //Nhu cau
            objMT.IsMua = (bool)rdbLoaiTin.EditValue;
            objMT.MaLBDS = (short?)lookLoaiBDS.EditValue;
            objMT.MoHinh = txtMoHInh.Text;
            objMT.MaTTD = 2;
            //Phap ly
            objMT.mglmtPhapLies.Clear();
            string[] ps = cmbPhapLy.Properties.GetCheckedItems().ToString().Split(',');
            if (ps[0] != "")
            {
                foreach (var i in ps)
                {
                    mglmtPhapLy objPL = new mglmtPhapLy();
                    objPL.MaPL = byte.Parse(i);
                    objMT.mglmtPhapLies.Add(objPL);
                }
            }
            objMT.MaDA = (int?)lookDuAn.EditValue;
            //Tien ich
            objMT.mglmtTienIches.Clear();
            string[] ts = cmbTienIch.Properties.GetCheckedItems().ToString().Split(',');
            if (ts[0] != "")
            {
                foreach (var i in ts)
                {
                    mglmtTienIch objTI = new mglmtTienIch();
                    objTI.MaTI = byte.Parse(i);
                    objMT.mglmtTienIches.Add(objTI);
                }
            }
            //HuongCua
            objMT.mglmtHuongs.Clear();
            string[] hs = cmbHuongCua.Properties.GetCheckedItems().ToString().Split(',');
            if (hs[0] != "")
            {
                foreach (var i in hs)
                {
                    mglmtHuong objHuong = new mglmtHuong();
                    objHuong.MaHuong = byte.Parse(i);
                    objMT.mglmtHuongs.Add(objHuong);
                }
            }
            //HuongBC
            objMT.mglmtHuongBCs.Clear();
            string[] hsbc = cmbHuongBC.Properties.GetCheckedItems().ToString().Split(',');
            if (hsbc[0] != "")
            {
                foreach (var i in hsbc)
                {
                    mglmtHuongBC objHuongBC = new mglmtHuongBC();
                    objHuongBC.MaHuong = byte.Parse(i);
                    objMT.mglmtHuongBCs.Add(objHuongBC);
                }
            }
            objMT.PhiMG = spinPMG.Value;
            objMT.GiaDen = spinGiaDen.Value;

            #region ghi lich su
            if (objMT.MaMT != 0)
            {
                if (txtGioiThieu.Text != objMT.GhiChu)
                    objLS.GhiChu = txtGioiThieu.InnerHtml;
                if ((int?)lookNhanVienQL.EditValue != objMT.MaNVKT)
                    objLS.CanBoLV = lookNhanVienQL.Text;
                if (txtGioiThieu.InnerHtml != objMT.GhiChu || (int?)lookNhanVienQL.EditValue != objMT.MaNVKT)
                {
                    objLS.MaMT = objMT.MaMT;
                    objLS.MaNVS = Common.StaffID;
                    objLS.NgaySua = DateTime.Now;
                    db.mglmtLichSus.InsertOnSubmit(objLS);
                }
            }
            else
            {
                objLS.GhiChu = txtGioiThieu.InnerHtml;
                objLS.CanBoLV = lookNhanVienQL.Text;
                objLS.MaMT = objMT.MaMT;
                objLS.MaNVS = Common.StaffID;
                objLS.NgaySua = DateTime.Now;
                db.mglmtLichSus.InsertOnSubmit(objLS);
            }


            #endregion

            objMT.GhiChu = txtGioiThieu.InnerHtml;
            objMT.DienTichTu = spinDienTichTu.Value;
            objMT.MatTienTu = (byte?)spinMatTienTu.Value;
            objMT.MatTienTu = (byte?)spinMatTienTu.Value;
            objMT.MatTienDien = (byte?)spinMatTienDen.Value;
            objMT.PhNguTu = (byte?)spinPhNguTu.Value;
            objMT.PVSTu = (byte?)spinPVSTu.Value;
            //duong
            string ListDuong = "";
            objMT.mglmtDuongs.Clear();
            for (int i = 0; i < lbDuong2.ItemCount; i++)
            {
                int maDuong = (int)lbDuong2.GetItemValue(i);
                mglmtDuong objDuong = new mglmtDuong();
                objDuong.MaDuong = maDuong;
                ListDuong += lbDuong2.GetDisplayItemValue(i) + ", ";
                objMT.mglmtDuongs.Add(objDuong);
            }
            //
            objMT.MaTinh = (byte?)lookTinh.EditValue;
            objMT.MaLT = (byte?)lookLoaiTien.EditValue;
            //Loai duong
            objMT.mglmtLoaiDuongs.Clear();
            string[] ds = cmbLoaiDuong.Properties.GetCheckedItems().ToString().Split(',');
            if (ds[0] != "")
            {
                foreach (var i in ds)
                {
                    mglmtLoaiDuong objLD = new mglmtLoaiDuong();
                    objLD.MaLD = byte.Parse(i);
                    objMT.mglmtLoaiDuongs.Add(objLD);
                }
            }
            objMT.DuongRongTu = spinDuongRongTu.Value;
            try
            {
                objMT.TangTu = (byte?)spinTangTu.EditValue;
            }
            catch 
            {

            }
            try
            {
                objMT.TangDen = (byte?)spinTangDen.EditValue;
            }
            catch 
            {

            }
         
            objMT.HuongBC = cmbHuongBC.Text;
            objMT.HuongCua = cmbHuongCua.Text;
            objMT.TienIch = cmbTienIch.Text;
            string ListHuyen = "";
            //Huyen
            objMT.mglmtHuyens.Clear();
            for (int i = 0; i < lbHuyen2.ItemCount; i++)
            {
                short? maHuyen = (short?)lbHuyen2.GetItemValue(i);
                mglmtHuyen objHuyen = new mglmtHuyen();
                objHuyen.MaHuyen = maHuyen;
                ListHuyen += lbHuyen2.GetDisplayItemValue(i) + ", ";
                objMT.mglmtHuyens.Add(objHuyen);
            }
            objMT.KhuVuc = ListHuyen;
            objMT.IsCanGoc = ckbCanGoc.Checked;
            objMT.IsDeOto = ckbDoOto.Checked;
            objMT.IsThangMay = ckbCoThangMay.Checked;
            objMT.IsTangHam = ckbCoTangHam.Checked;
            objMT.MaMD = lookMucDich.EditValue != null ? (short?)Convert.ToInt16(lookMucDich.EditValue) : null;

            /// objMT.MaKH = objKH.MaKH;

            //Khach hang
            //string hoTenKH = txtHoTenKH.Text.Trim();
            //objKH.HoKH = hoTenKH.LastIndexOf(' ') > 0 ? hoTenKH.Substring(0, hoTenKH.LastIndexOf(' ')) : "";
            //objKH.TenKH = hoTenKH.Substring(hoTenKH.LastIndexOf(' ') + 1);
            //objKH.SoCMND = txtSoCMND.Text;
            //if (dateNgayCap.Text != "")
            //    objKH.NgayCap = dateNgayCap.DateTime;
            //objKH.NoiCap = txtNoiCap.Text;
            //if (dateNgaySinh.Text != "")
            //    objKH.NgaySinh = dateNgaySinh.DateTime;
            //objKH.NguyenQuan = txtNoiSinh.Text;
            //objKH.DienThoaiCT = txtDienThoaiCD.Text;
            //objKH.DiDong = txtDiDong.Text;
            //objKH.DiDong2 = txtDiDong2.Text;
            //objKH.DiDong3 = txtDiDong3.Text;
            //objKH.DiDong4 = txtDiDong4.Text;
            //objKH.Email = txtEmail.Text;
            //objKH.ThuongTru = txtThuongTru.Text;
            //objKH.DiaChi = txtDiaChiLienHe.Text;
            //objKH.MaNV = (int)lookNVKD.EditValue;


            objMT.KhachHang = objKH;
            if (this.MaMT == 0)
            {
                objMT.NgayNhap = DateTime.Now;
                objMT.MaNVN = Common.StaffID;
                db.mglmtMuaThues.InsertOnSubmit(objMT);

            }
            db.SubmitChanges();

            this.MaMT = objMT.MaMT;
            SaveLichSu();
            this.IsSave = true;
            SetEnableControl(false);
        }

        private void itemLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save();
        }

        private void ThreadSendMail()
        {
            var objCOmpany = db.Companies.FirstOrDefault();
            var objListMailTo = db.mglmMailDangKyNhans.Where(p => p.IsThemKH == true);
            if (objListMailTo == null)
                return;
            var objConfig = db.mailConfigs.FirstOrDefault(p => p.IsNoiBo == true);
            if (objConfig == null)
                return;
            foreach (var m in objListMailTo)
            {
                MailProviderCls objMail = new MailProviderCls();
                var objMailForm = new MailAddress(objConfig.Email, objConfig.Username);
                objMail.MailAddressFrom = objMailForm;
                var objMailTo = new MailAddress(m.NhanVien.Email, m.NhanVien.HoTen);
                objMail.MailAddressTo = objMailTo;
                objMail.SmtpServer = objConfig.Server;
                objMail.EnableSsl = objConfig.EnableSsl.Value;
                objMail.Port = objConfig.Port ?? 465;
                objMail.PassWord = EncDec.Decrypt(objConfig.Password);
                objMail.Subject = objCOmpany == null ? "Nhân viên (nhập mới/chỉnh sửa) nhu cầu mua, thuê." : string.Format("Nhân viên công ty {0} (nhập mới/chỉnh sửa) nhu cầu mua thuê.", objCOmpany.TenCT);
                string Content = string.Format(" Nhân viên xử lý : {0} \r\n Nội dung công việc: Thêm mới/chỉnh sửa nhu cầu {1} BDS  \r\n Số đăng ký: {2} \r\n Loại BDS: {3} \r\n Ngày xử lý: {4:dd/MM/yyyy-hh:mm:ss tt}",
                    objMT.NhanVien.HoTen, objMT.IsMua == true ? "Mua" : "Thuê", objMT.SoDK, objMT.LoaiBD.TenLBDS, objMT.NgayCN);
                objMail.Content = Content;
                objMail.SendMailV3();
            }
        }

        private void itemHoan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetEnableControl(false);
        }

        private void itemDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void lookDuAn_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                lookDuAn.EditValue = null;
        }

        private void frmEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (txtSoDK.Text != objMT.SoDK || (DateTime?)dateNgayDK.EditValue != objMT.NgayDK ||
                (byte?)lookTrangThaiHDMB.EditValue != objMT.TrangThaiHDMG || (int?)lookNVKD.EditValue != objMT.MaNVKD ||
                (int?)lookNhanVienQL.EditValue != objMT.MaNVKT || (short?)lookNguon.EditValue != objMT.MaNguon ||
                (byte?)lookTrangThai.EditValue != objMT.MaTT || (bool?)rdbLoaiTin.EditValue != objMT.IsMua ||
                (short?)lookLoaiBDS.EditValue != objMT.MaLBDS || (int?)lookDuAn.EditValue != objMT.MaDA ||
                (decimal?)spinGiaDen.EditValue != objMT.GiaDen || txtGioiThieu.InnerHtml != objMT.GhiChu ||

                (decimal?)spinDienTichTu.EditValue != objMT.DienTichTu || Convert.ToByte(spinMatTienTu.EditValue) != objMT.MatTienTu ||
                Convert.ToByte(spinMatTienDen.EditValue) != objMT.MatTienDien || (decimal?)spinPMG.EditValue != objMT.PhiMG
                || Convert.ToByte(spinPhNguTu.EditValue) != objMT.PhNguTu ||
                Convert.ToByte(spinPVSTu.EditValue) != objMT.PVSTu || (decimal?)spinDuongRongTu.EditValue != objMT.DuongRongTu ||
                Convert.ToByte(spinTangTu.EditValue == null ? 0 : spinTangTu.EditValue) != objMT.TangTu || ckbCanGoc.Checked != objMT.IsCanGoc ||
                ckbCoThangMay.Checked != objMT.IsThangMay || ckbCoTangHam.Checked != objMT.IsTangHam ||
                ckbDoOto.Checked != objMT.IsDeOto || (short?)lookMucDich.EditValue != objMT.MaMD ||
                (byte?)lookLoaiTien.EditValue != objMT.MaLT || (byte?)lookTinh.EditValue != objMT.MaTinh)
                {
                    if (DialogBox.Question("Bạn có muốn lưu không") == DialogResult.No) return;
                    Save();
                }
            }
            catch { }

        }

        private void lookLoaiTien_EditValueChanged(object sender, EventArgs e)
        {
            spinTyGia.EditValue = lookLoaiTien.GetColumnValue("TyGia");
        }

        private void Duong_Add()
        {
            List<Duong> listDuong1 = (List<Duong>)lbDuong1.DataSource;
            List<Duong> listDuong2 = (List<Duong>)lbDuong2.DataSource;

            List<Duong> select = new List<Duong>();
            foreach (Duong h in lbDuong1.SelectedItems)
            {
                select.Add(h);
            }

            foreach (Duong h in select)
            {
                if (listDuong2.IndexOf(h) < 0)
                    listDuong2.Add(h);
                listDuong1.Remove(h);
            }

            lbDuong1.Refresh();
            lbDuong2.Refresh();
        }

        private void btnThemDuong_Click(object sender, EventArgs e)
        {
            Duong_Add();
        }

        private void Duong_Remove()
        {
            List<Duong> listDuong1 = (List<Duong>)lbDuong1.DataSource;
            List<Duong> listDuong2 = (List<Duong>)lbDuong2.DataSource;

            List<Duong> select = new List<Duong>();
            foreach (Duong h in lbDuong2.SelectedItems)
            {
                select.Add(h);
            }

            foreach (Duong h in select)
            {
                if (listDuong1.IndexOf(h) < 0)
                    listDuong1.Add(h);
                listDuong2.Remove(h);
            }

            lbDuong1.Refresh();
            lbDuong2.Refresh();
        }

        private void btnXoaDuong_Click(object sender, EventArgs e)
        {
            Duong_Remove();
        }

        private void btnThemDuongTatCa_Click(object sender, EventArgs e)
        {
            lbDuong1.SelectAll();
            Duong_Add();
        }

        private void btnXoaDuongTatCa_Click(object sender, EventArgs e)
        {
            lbDuong2.SelectAll();
            Duong_Remove();
        }

        private void txtSearchDuong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TimKiemDuong();
            }
        }
        private void TimKiemDuong()
        {
            //lbDuong1.DataSource = db.Duongs;
            lbDuong1.DataSource = null;

            var searchText = txtSearchDuong.Text;
            var keyWords = searchText.Split(' ');
            var query = from rows in db.Duongs.AsEnumerable()
                        where keyWords.Any(k => ConvertToUnSign(rows.TenDuong.ToString().ToLower().Trim()).Contains(ConvertToUnSign(k.ToString().ToLower().Trim())))
                        select rows;

            if (query.Count() > 0)
            {
                lbDuong1.DataSource = query.ToList();
            }

        }

        public static string ConvertToUnSign(string text)
        {

            for (int i = 33; i < 48; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            for (int i = 58; i < 65; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            for (int i = 91; i < 97; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            for (int i = 123; i < 127; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");

            string strFormD = text.Normalize(System.Text.NormalizationForm.FormD);

            return regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');

        }

        private void txtSearchDuong_EditValueChanged(object sender, EventArgs e)
        {
            lbDuong1.DataSource = db.Duongs.Where(p => p.TenDuong.Contains(txtSearchDuong.Text)).ToList();
        }

        private void SaveLichSu()
        {
            var objLSAdd = new mglmtHistoryChange();
            objLSAdd.SoDK = txtSoDK.Text;
            objLSAdd.NgayDK = dateNgayDK.DateTime;
            objLSAdd.NgayCN = db.GetSystemDate();
            objLSAdd.TrangThaiHDMG = (byte?)lookTrangThaiHDMB.EditValue;
            objLSAdd.MaNVKD = (int)lookNVKD.EditValue;
            objLSAdd.MaNVKT = (int)lookNhanVienQL.EditValue;
            objLSAdd.MaNguon = (short?)lookNguon.EditValue;
            objLSAdd.MaTT = (byte?)lookTrangThai.EditValue;
            objLSAdd.IsMua = (bool)rdbLoaiTin.EditValue;
            objLSAdd.MaLBDS = (short?)lookLoaiBDS.EditValue;
            objLSAdd.MoHinh = txtMoHInh.Text;
            objLSAdd.MaTTD = 2;
            objLSAdd.MaDA = (int?)lookDuAn.EditValue;
            objLSAdd.PhiMG = spinPMG.Value;
            objLSAdd.GiaDen = spinGiaDen.Value;
            objLSAdd.GhiChu = txtGioiThieu.InnerHtml;
            objLSAdd.DienTichTu = spinDienTichTu.Value;
            objLSAdd.MatTienTu = Convert.ToByte(spinMatTienTu.Value);
            objLSAdd.MatTienDien = Convert.ToByte(spinMatTienDen.Value);
            objLSAdd.PhNguTu = Convert.ToByte(spinPhNguTu.Value);
            objLSAdd.PVSTu = Convert.ToByte(spinPVSTu.Value);
            objLSAdd.MaTinh = (byte?)lookTinh.EditValue;
            objLSAdd.MaLT = (byte?)lookLoaiTien.EditValue;
            objLSAdd.DuongRongTu = spinDuongRongTu.Value;
            objLSAdd.TangTu = Convert.ToByte(spinTangTu.EditValue);
            objLSAdd.TangDen = Convert.ToByte(spinTangDen.EditValue);
            objLSAdd.HuongBC = cmbHuongBC.Text;
            objLSAdd.HuongCua = cmbHuongCua.Text;
            objLSAdd.TienIch = cmbTienIch.Text;
            objLSAdd.IsCanGoc = ckbCanGoc.Checked;
            objLSAdd.IsDeOto = ckbDoOto.Checked;
            objLSAdd.IsThangMay = ckbCoThangMay.Checked;
            objLSAdd.IsTangHam = ckbCoTangHam.Checked;
            objLSAdd.MaMD = lookMucDich.EditValue != null ? (short?)Convert.ToInt16(lookMucDich.EditValue) : null;
            objLSAdd.MaKH = objKH.MaKH;

            //Phap ly
            objLSAdd.PhapLys = cmbPhapLy.Text;
            //duong
            string ListDuong = "";
            for (int i = 0; i < lbDuong2.ItemCount; i++)
            {
                ListDuong += lbDuong2.GetDisplayItemValue(i) + ", ";
            }

            objLSAdd.Duong = ListDuong;
            //Loai duong           
            objLSAdd.LoaiDuongs = cmbLoaiDuong.Text;
            string ListHuyen = "";
            //Huyen
            for (int i = 0; i < lbHuyen2.ItemCount; i++)
            {
                ListHuyen += lbHuyen2.GetDisplayItemValue(i) + ", ";
            }
            objLSAdd.MaMT = this.MaMT;
            objLSAdd.KhuVuc = ListHuyen;
            objLSAdd.NgaySua = DateTime.Now;
            objLSAdd.MaNVS = Common.StaffID;
            db.mglmtHistoryChanges.InsertOnSubmit(objLSAdd);
            db.SubmitChanges();

        }

        private void UpdateLichSu()
        {
            if (this.MaMT != 0)
            {
                var objLSNew = new mglmtHistoryChange();
                bool isChange = false;
                var objMT = db.mglmtMuaThues.FirstOrDefault(p => p.MaMT == MaMT);

                if (objMT.SoDK != txtSoDK.Text)
                {
                    isChange = true;
                    objLSNew.SoDK = txtSoDK.Text;
                }
                if (objMT.NgayDK != (DateTime)dateNgayDK.EditValue)
                {
                    isChange = true;
                    objLSNew.NgayDK = dateNgayDK.DateTime;
                }
                if (objMT.TrangThaiHDMG != (byte?)lookTrangThaiHDMB.EditValue)
                {
                    isChange = true;
                    objLSNew.TrangThaiHDMG = (byte?)lookTrangThaiHDMB.EditValue;
                }
                if (objMT.MaNVKD != (int)lookNVKD.EditValue)
                {
                    isChange = true;
                    objLSNew.MaNVKD = (int)lookNVKD.EditValue;
                }
                if (objMT.MaNVKT != (int)lookNhanVienQL.EditValue)
                {
                    isChange = true;
                    objLSNew.MaNVKT = (int)lookNhanVienQL.EditValue;
                }
                if (objMT.MaNguon != (short?)lookNguon.EditValue)
                {
                    isChange = true;
                    objLSNew.MaNguon = (short?)lookNguon.EditValue;
                }
                if (objMT.MaTT != (byte?)lookTrangThai.EditValue)
                {
                    isChange = true;
                    objLSNew.MaTT = (byte?)lookTrangThai.EditValue;
                }

                if (objMT.IsMua != (bool)rdbLoaiTin.EditValue)
                {
                    isChange = true;
                    objLSNew.IsMua = (bool)rdbLoaiTin.EditValue;
                }
                if (objMT.MaLBDS != (short?)lookLoaiBDS.EditValue)
                {
                    isChange = true;
                    objLSNew.MaLBDS = (short?)lookLoaiBDS.EditValue;
                }

                if (objMT.MoHinh != txtMoHInh.Text)
                {
                    isChange = true;
                    objLSNew.MoHinh = txtMoHInh.Text;
                }
                if (objMT.MaDA != (int?)lookDuAn.EditValue)
                {
                    isChange = true;
                    objLSNew.MaDA = (int?)lookDuAn.EditValue;
                }
                if (objMT.PhiMG != spinPMG.Value)
                {
                    isChange = true;
                    objLSNew.PhiMG = spinPMG.Value;
                }
                if (objMT.GiaDen != spinGiaDen.Value)
                {
                    isChange = true;
                    objLSNew.GiaDen = spinGiaDen.Value;
                }
                if (objMT.GhiChu != txtGioiThieu.InnerHtml)
                {
                    isChange = true;
                    objLSNew.GhiChu = txtGioiThieu.InnerHtml;
                }

                if (objMT.DienTichTu != spinDienTichTu.Value)
                {
                    isChange = true;
                    objLSNew.DienTichTu = spinDienTichTu.Value;
                }
                if (objMT.MatTienTu != Convert.ToByte(spinMatTienTu.Value))
                {
                    isChange = true;
                    objLSNew.MatTienTu = Convert.ToByte(spinMatTienTu.Value);
                }
                if (objMT.MatTienDien != Convert.ToByte(spinMatTienDen.Value))
                {
                    isChange = true;
                    objLSNew.MatTienDien = Convert.ToByte(spinMatTienDen.Value);
                }
                if (objMT.PhNguTu != Convert.ToByte(spinPhNguTu.Value))
                {
                    isChange = true;
                    objLSNew.PhNguTu = Convert.ToByte(spinPhNguTu.Value);
                }
                if (objMT.PVSTu != Convert.ToByte(spinPVSTu.Value))
                {
                    isChange = true;
                    objLSNew.PVSTu = Convert.ToByte(spinPVSTu.Value);
                }
                if (objMT.MaTinh != (byte?)lookTinh.EditValue)
                {
                    isChange = true;
                    objLSNew.MaTinh = (byte?)lookTinh.EditValue;
                }
                if (objMT.MaLT != (byte?)lookLoaiTien.EditValue)
                {
                    isChange = true;
                    objLSNew.MaLT = (byte?)lookLoaiTien.EditValue;
                }
                if (objMT.DuongRongTu != spinDuongRongTu.Value)
                {
                    isChange = true;
                    objLSNew.DuongRongTu = spinDuongRongTu.Value;
                }
                if (objMT.TangTu != Convert.ToByte(spinTangTu.EditValue))
                {
                    isChange = true;
                    objLSNew.TangTu = Convert.ToByte(spinTangTu.EditValue);
                }
                if (objMT.TangDen != Convert.ToByte(spinTangDen.EditValue))
                {
                    isChange = true;
                    objLSNew.TangDen = Convert.ToByte(spinTangDen.EditValue);
                }
                if (objMT.IsCanGoc != ckbCanGoc.Checked)
                {
                    isChange = true;
                    objLSNew.IsCanGoc = ckbCanGoc.Checked;
                }
                if (objMT.IsDeOto != ckbDoOto.Checked)
                {
                    isChange = true;
                    objLSNew.IsDeOto = ckbDoOto.Checked;
                }
                if (objMT.IsThangMay != ckbCoThangMay.Checked)
                {
                    isChange = true;
                    objLSNew.IsThangMay = ckbCoThangMay.Checked;
                }
                if (objMT.IsTangHam != ckbCoTangHam.Checked)
                {
                    isChange = true;
                    objLSNew.IsTangHam = ckbCoTangHam.Checked;
                }
                if (objMT.MaMD != (short)lookMucDich.EditValue)
                {
                    isChange = true;
                    objLSNew.MaMD = lookMucDich.EditValue != null ? (short?)Convert.ToInt16(lookMucDich.EditValue) : null;
                }
                if (objMT.MaKH != objKH.MaKH)
                {
                    isChange = true;
                    objLSNew.MaKH = objKH.MaKH;
                }

                //1
                var lstHuongBc = objMT.mglmtHuongBCs.Select(p => p.PhuongHuong.TenPhuongHuong);
                foreach (var item in cmbHuongBC.Text.Split(','))
                {
                    if (!lstHuongBc.Contains(item.Trim()))
                    {
                        objLSNew.HuongBC = cmbHuongBC.Text;
                    }
                }
                //2
                var lstPhapLy = objMT.mglmtPhapLies.Select(p => p.PhapLy.TenPL);
                foreach (var item in cmbPhapLy.Text.Split(','))
                {
                    if (!lstPhapLy.Contains(item.Trim()))
                    {
                        objLSNew.PhapLys = cmbPhapLy.Text;
                    }
                }
                //3
                var lstTienIch = objMT.mglmtTienIches.Select(p => p.TienIch.TenTienIch);
                foreach (var item in cmbTienIch.Text.Split(','))
                {
                    if (!lstTienIch.Contains(item.Trim()))
                    {
                        objLSNew.TienIch = cmbTienIch.Text;
                    }
                }
                //4
                var lstLoaiDuong = objMT.mglmtLoaiDuongs.Select(p => p.LoaiDuong.TenLD);
                foreach (var item in cmbLoaiDuong.Text.Split(','))
                {
                    if (!lstLoaiDuong.Contains(item.Trim()))
                    {
                        objLSNew.LoaiDuongs = cmbLoaiDuong.Text;
                    }
                }
                //5
                var lstHuong = objMT.mglmtHuongs.Select(p => p.PhuongHuong.TenPhuongHuong);
                foreach (var item in cmbHuongCua.Text.Split(','))
                {
                    if (!lstHuong.Contains(item.Trim()))
                    {
                        objLSNew.HuongCua = cmbHuongCua.Text;
                    }
                }
                //6
                //duong
                string duongs = "";
                var lstDuong = objMT.mglmtDuongs.Select(p => p.Duong.TenDuong).ToList();
                for (int i = 0; i < lbDuong2.ItemCount; i++)
                {
                    duongs += lbDuong2.GetDisplayItemValue(i) + ", ";
                    if (!lstDuong.Contains(lbDuong2.GetDisplayItemValue(i)))
                    {
                        objLSNew.Duong = cmbHuongCua.Text;
                    }
                }
                //7
                //Huyen
                string ListHuyen = "";
                var lstHuyen = objMT.mglmtHuyens.Select(p => p.Huyen.TenHuyen).ToList();
                for (int i = 0; i < lbHuyen2.ItemCount; i++)
                {
                    ListHuyen += lbHuyen2.GetDisplayItemValue(i) + ", ";
                    if (!lstHuyen.Contains(lbHuyen2.GetDisplayItemValue(i)))
                    {
                        objLSNew.KhuVuc = ListHuyen;
                    }
                }

                if (isChange)
                {
                    objLSNew.MaNVS = Common.StaffID;
                    objLSNew.NgaySua = DateTime.Now;
                    objLSNew.MaMT = objMT.MaMT;
                    db.mglmtHistoryChanges.InsertOnSubmit(objLSNew);
                }
            }
        }
    }
}