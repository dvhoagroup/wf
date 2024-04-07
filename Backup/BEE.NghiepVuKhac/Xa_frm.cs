using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;
using BEEREMA;
using BEE.ThuVien;

namespace BEE.NghiepVuKhac
{
    public partial class Xa_frm : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();
        public int KeyID = 0;
        public bool IsUpdate = false;
        public byte MaTinh = 0;
        public short MaHuyen = 0;
        public Xa_frm()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Huong_frm_Load(object sender, EventArgs e)
        {
            if (KeyID != 0)
            {
                LoadTinh();
                //lookUpTinh.ItemIndex = 0;
                //MaTinh = byte.Parse(lookUpTinh.EditValue.ToString());

                LoadHuyen();
                //lookUpHuyen.ItemIndex = 0;
                
                //it.XaCls o = new it.XaCls(KeyID);
                var objX = db.Xas.Single(p => p.MaXa == this.KeyID);
                txtTenHuong.Text = objX.TenXa;
                lookUpHuyen.EditValue = objX.MaHuyen;
                //txtTenHuong.Text = o.TenXa;
                //lookUpHuyen.EditValue = o.Huyen.MaHuyen;
                lookUpTinh.EditValue = this.MaTinh;//lookUpHuyen.GetColumnValue("MaTinh");
            }
            else
            {
                LoadTinh();
                lookUpTinh.EditValue = MaTinh;
                LoadHuyen();
                lookUpHuyen.ItemIndex = 0;
            }
            txtTenHuong.Focus();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtTenHuong.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập tên huyện. Xin cảm ơn");
                txtTenHuong.Focus();
                return;
            }

            if (lookUpHuyen.Text == "")
            {
                DialogBox.Infomation("Vui lòng nhập chọn huyện. Xin cảm ơn");
                lookUpHuyen.Focus();
                return;
            }

            it.XaCls o = new it.XaCls();
            o.TenXa = txtTenHuong.Text;
            o.Huyen.MaHuyen = short.Parse(lookUpHuyen.EditValue.ToString());
            
            if (KeyID != 0)
            {
                o.MaXa = KeyID;
                o.Update();
            }
            else
                o.Insert();

            IsUpdate = true;
            DialogBox.Infomation("Dữ liệu đã cập nhật thành công.");
            this.Close();
        }
         
        private void lookUpTinh_EditValueChanged(object sender, EventArgs e)
        {
            this.MaTinh = (byte)lookUpTinh.EditValue;
            lookUpHuyen.Properties.DataSource = db.Huyens.Where(p => p.MaTinh == this.MaTinh).OrderBy(p => p.TenHuyen).ToList();
            //lookUpHuyen.ItemIndex = 0;
        }

        void LoadTinh()
        {
            it.TinhCls objTinh = new it.TinhCls();
            lookUpTinh.Properties.DataSource = objTinh.Select();
        }

        private void lookUpTinh_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                Tinh_frm frm = new Tinh_frm();
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadTinh();
            }
        }

        void LoadHuyen()
        {
            it.HuyenCls objHuyen = new it.HuyenCls();
            lookUpHuyen.Properties.DataSource = objHuyen.Select(MaTinh);
        }

        private void lookUpHuyen_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                Huyen_frm frm = new Huyen_frm();
                frm.MaTinh = byte.Parse(lookUpTinh.EditValue.ToString());
                frm.ShowDialog();
                if (frm.IsUpdate)
                    LoadHuyen();
            }
        }
    }
}