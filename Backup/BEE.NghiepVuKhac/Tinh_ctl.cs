using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Linq;
using BEEREMA;

namespace BEE.NghiepVuKhac
{
    public partial class Tinh_ctl : DevExpress.XtraEditors.XtraUserControl
    {
        public Tinh_ctl()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateUserControl(this, barManager1);
        }

        void LoadData()
        {
            it.TinhCls o = new it.TinhCls();
            gridControl1.DataSource = o.Select();
        }

        void LoadPermission()
        {
            it.ActionDataCls o = new it.ActionDataCls();
            o.AccessData.Per.PerID = BEE.ThuVien.Common.PerID;
            o.AccessData.Form.FormID = 36;
            DataTable tblAction = o.SelectBy();
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            if (tblAction.Rows.Count > 0)
            {
                foreach (DataRow r in tblAction.Rows)
                {
                    switch (byte.Parse(r["FeatureID"].ToString()))
                    {
                        case 1:
                            btnThem.Enabled = true;
                            break;
                        case 2:
                            btnSua.Enabled = true;
                            break;
                        case 3:
                            btnXoa.Enabled = true;
                            break;
                    }
                }
            }
        }

        private void Huong_ctl_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadPermission();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (btnSua.Enabled)
                Edit();
        }

        private void btnNap_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Tinh_frm frm = new Tinh_frm();
            frm.ShowDialog();
            if (frm.IsUpdate)
                LoadData();
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colMaHuong) != null)
            {
                if (DialogBox.Question("Bạn có chắc chắn muốn xóa tỉnh: <" + gridView1.GetFocusedRowCellValue(colTenHuong).ToString() + "> ra khỏi hệ thống không?") == DialogResult.Yes)
                {
                    try
                    {
                        it.TinhCls o = new it.TinhCls();
                        o.MaTinh = byte.Parse(gridView1.GetFocusedRowCellValue(colMaHuong).ToString());
                        o.Delete();
                        gridView1.DeleteSelectedRows();
                    }
                    catch
                    {
                        DialogBox.Infomation("Xóa không thành công vì Tỉnh: <" + gridView1.GetFocusedRowCellValue(colTenHuong).ToString() + "> đã được sử dụng. Vui lòng kiểm tra lại.");
                    }
                }
            }
            else
                DialogBox.Infomation("Vui lòng chọn tỉnh cần xóa. Xin cảm ơn");
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Edit();
        }

        private void btnNap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        void Edit()
        {
            if (gridView1.GetFocusedRowCellValue(colMaHuong) != null)
            {
                Tinh_frm frm = new Tinh_frm();
                frm.KeyID = byte.Parse(gridView1.GetFocusedRowCellValue(colMaHuong).ToString());
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadData();
            }
            else
                DialogBox.Infomation("Vui lòng chọn tỉnh cần sửa. Xin cảm ơn");
        }

        private void itemUpdate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var wait = DialogBox.WaitingForm();
            using (BEE.ThuVien.MasterDataContext db = new BEE.ThuVien.MasterDataContext())
            {                
                DataTable tblTinh = Table("select MaTinh, TenTinh from Tinh");
                foreach (DataRow r in tblTinh.Rows)
                {
                    var tinhs = db.Tinhs.Where(p => p.MaTinh == Convert.ToByte(r["MaTinh"]));
                    if (tinhs.Count() <= 0)
                    {
                        var tinh = new BEE.ThuVien.Tinh();
                        tinh.MaTinh = Convert.ToByte(r["MaTinh"]);
                        tinh.TenTinh = r["TenTinh"].ToString();
                        db.Tinhs.InsertOnSubmit(tinh);

                        //huyen
                        DataTable tblHuyen = Table("select MaHuyen, TenHuyen, MaTinh from Huyen where MaTinh = " + r["MaTinh"].ToString());
                        foreach (DataRow rh in tblHuyen.Rows)
                        {
                            var huyen = new BEE.ThuVien.Huyen();
                            huyen.MaHuyen = Convert.ToInt16(rh["MaHuyen"]);
                            huyen.TenHuyen = rh["TenHuyen"].ToString();
                            huyen.MaTinh = Convert.ToByte(r["MaTinh"]);
                            db.Huyens.InsertOnSubmit(huyen);

                            //xa
                            DataTable tblXa = Table("select MaXa, TenXa, MaHuyen from Xa where MaHuyen = " + huyen.MaHuyen);
                            foreach (DataRow rx in tblXa.Rows)
                            {
                                var xa = new BEE.ThuVien.Xa();
                                xa.MaXa = Convert.ToInt32(rx["MaXa"]);
                                xa.TenXa = rx["TenXa"].ToString();
                                xa.MaHuyen = huyen.MaHuyen;
                                db.Xas.InsertOnSubmit(xa);
                            }
                        }
                        db.SubmitChanges();
                    }
                }
            }
            wait.Close();
        }

        public static DataTable Table(string Str)
        {
            SqlConnection SqlConn = new SqlConnection("Data Source=27.0.14.84;Initial Catalog=edenreal_db;Persist Security Info=True;User ID=edenreal_login;Password=edr-$%^987");
            SqlDataAdapter Ad;
            DataTable Dt = new DataTable();
            try
            {
                Ad = new SqlDataAdapter(Str, SqlConn);
                SqlConn.Open();
                Ad.Fill(Dt);
                SqlConn.Close();
            }
            catch
            {
                SqlConn.Close();
            }
            return Dt;
        }  
    }
}
