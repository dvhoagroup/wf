using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BEEREMA;

namespace BEE.HoatDong.PhanQuyen
{
    public partial class AddFeature_frm : DevExpress.XtraEditors.XtraForm
    {
        public int FeatureID = 0;
        public int FormID = 0;
        public bool IsUpdate = false;
        public string FormName = "";
        public AddFeature_frm()
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
            txtTenHuong.Text = FormName;
            it.FeaturesCls o = new it.FeaturesCls();
            lookUpFeature.Properties.DataSource = o.Select();
            lookUpFeature.EditValue = FeatureID;
            txtTenHuong.Focus();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtTenHuong.Text == "")
            {
                DialogBox.Infomation("Vui lòng chọn tính năng. Xin cảm ơn");
                txtTenHuong.Focus();
                return;
            }

            if (lookUpFeature.Text == "")
            {
                DialogBox.Infomation("Vui lòng chọn tính năng. Xin cảm ơn");
                lookUpFeature.Focus();
                return;
            }
            it.FormFeaturesCls o = new it.FormFeaturesCls();
            o.FormID = FormID;
            o.FeatureID = byte.Parse(lookUpFeature.EditValue.ToString());

            //if (FormID != 0)
            //{
            //    o.FormID = FormID;
            //    o.Update();
            //}
            //else
            try
            {
                o.Insert();
            }
            catch
            {
                DialogBox.Infomation("Tính năng <" + lookUpFeature.Text + "> đã được thêm cho form này rồi. Vui lòng kiểm tra lại, xin cảm ơn.");
                return;
            }
            
            IsUpdate = true;
            //DialogBox.Infomation("Dữ liệu đã cập nhật thành công.");
            this.Close();
        }
    }
}