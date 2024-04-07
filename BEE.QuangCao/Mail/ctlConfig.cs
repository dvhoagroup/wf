using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using BEE.ThuVien;
using BEEREMA;

namespace BEE.QuangCao.Mail
{
    public partial class ctlConfig : UserControl
    {
        MasterDataContext db = new MasterDataContext();

        public ctlConfig()
        {
            InitializeComponent();
        }

        void Config_Load()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                db = new MasterDataContext();
                switch (GetAccessData())
                {
                    case 1://Tat ca
                        gcConfig.DataSource = (from c in db.mailConfigs
                                               join nv in db.NhanViens on c.StaffID equals nv.MaNV
                                               select new
                                               {
                                                   c.ID,
                                                   c.Email,
                                                   c.Server,
                                                   c.EnableSsl,
                                                   c.SendMax,
                                                   c.NhanVien.HoTen,
                                                   c.DateModify,
                                                   c.Port,
                                                   c.Username
                                               }).ToList();
                        break;
                    case 2://Theo phong ban
                        //gcDoanhNghiep.DataSource = o.SelectComByDeparment(Common.StaffID, Common.DepartmentID);
                        gcConfig.DataSource = (from c in db.mailConfigs
                                               join nv in db.NhanViens on c.StaffID equals nv.MaNV
                                               where  nv.MaPB== Common.DepartmentID
                                               select new
                                               {
                                                   c.ID,
                                                   c.Email,
                                                   c.Server,
                                                   c.EnableSsl,
                                                   c.SendMax,
                                                   c.NhanVien.HoTen,
                                                   c.DateModify,
                                                   c.Port,
                                                   c.Username
                                               }).ToList();
                        break;
                    case 3://Theo nhom
                        //gcDoanhNghiep.DataSource = o.SelectComByGroup(Common.StaffID, Common.GroupID);
                        gcConfig.DataSource = (from c in db.mailConfigs
                                               join nv in db.NhanViens on c.StaffID equals nv.MaNV
                                               where nv.MaNKD == Common.GroupID
                                               select new
                                               {
                                                   c.ID,
                                                   c.Email,
                                                   c.Server,
                                                   c.EnableSsl,
                                                   c.SendMax,
                                                   c.NhanVien.HoTen,
                                                   c.DateModify,
                                                   c.Port,
                                                   c.Username
                                               }).ToList();
                        break;
                    case 4://Theo nhan vien
                        //gcDoanhNghiep.DataSource = o.SelectComByStaff(Common.StaffID);
                        gcConfig.DataSource = (from c in db.mailConfigs
                                               join nv in db.NhanViens on c.StaffID equals nv.MaNV
                                               where nv.MaNV == Common.StaffID
                                               select new
                                               {
                                                   c.ID,
                                                   c.Email,
                                                   c.Server,
                                                   c.EnableSsl,
                                                   c.SendMax,
                                                   c.NhanVien.HoTen,
                                                   c.DateModify,
                                                   c.Port,
                                                   c.Username
                                               }).ToList();
                        break;
                    default:
                        gcConfig.DataSource = null;
      
                        break;
                }

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
        int GetAccessData()
        {
            it.AccessDataCls o = new it.AccessDataCls(Common.PerID, 95);

            return o.SDB.SDBID;
        }
        void Config_Add()
        {
            frmConfig frm = new frmConfig();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                Config_Load();
            }
        }

        void Config_Edit()
        {
            int? MailID = (int?)grvConfig.GetFocusedRowCellValue("ID");
            if (MailID == null)
            {
                DialogBox.Error("Vui lòng chọn email");
                return;
            }

            frmConfig frm = new frmConfig();
            frm.MailID = MailID;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                Config_Load();
            }
        }

        void Config_Delete()
        {
            try
            {
                var indexs = grvConfig.GetSelectedRows();
                if (indexs.Length <= 0)
                {
                    DialogBox.Error("Vui lòng chọn email");
                    return;
                }

                foreach (var i in indexs)
                {
                    var objConfig = db.mailConfigs.Single(p => p.ID == (int)grvConfig.GetRowCellValue(i, "ID"));
                    db.mailConfigs.DeleteOnSubmit(objConfig);
                }

                db.SubmitChanges();

                Config_Load();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
        }

        private void ctlConfig_Load(object sender, EventArgs e)
        {
            Config_Load();
        }

        private void itemRefesh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Config_Load();
        }

        private void itemAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Config_Add();
        }

        private void itemEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Config_Edit();
        }

        private void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Config_Delete();
        }

        private void grvConfig_DoubleClick(object sender, EventArgs e)
        {
            Config_Edit();
        }

        private void grvConfig_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                Config_Delete();
        }
    }
}
