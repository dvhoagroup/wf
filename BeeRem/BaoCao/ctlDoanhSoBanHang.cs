using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace BEEREMA.BaoCao
{
    public partial class ctlDoanhSoBanHang : DevExpress.XtraEditors.XtraUserControl
    {
        int _Type;
        public ctlDoanhSoBanHang(int _type)
        {
            InitializeComponent();
            _Type = _type;
        }

        private void SetDate(int index)
        {
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Index = index;
            objKBC.SetToDate();

            itemTuNgay.EditValue = objKBC.DateFrom;
            itemDenNgay.EditValue = objKBC.DateTo;
        }

        private void BaoCao_Nap()
        {
            var wait = DialogBox.WaitingForm();
            try
            {
                var tuNgay = (DateTime?)itemTuNgay.EditValue;
                var denNgay = (DateTime?)itemDenNgay.EditValue;

                switch (_Type)
                { 
                    case 1:
                        var rpt = new rptDoanhSoBanHangTH(tuNgay, denNgay);
                        rpt.CreateDocument();

                        printControl1.PrintingSystem = rpt.PrintingSystem;
                        break;
                    case 2:
                        var rpt2 = new rptDoanhSoBanHangTheoSan(tuNgay, denNgay);
                        rpt2.CreateDocument();

                        printControl1.PrintingSystem = rpt2.PrintingSystem;
                        break;
                    default:
                        break;
                }
                
            }
            catch { }
            finally
            {
                wait.Close();
            }
        }

        private void ctlSanPhamTon_Load(object sender, EventArgs e)
        {
            it.KyBaoCaoCls objKBC = new it.KyBaoCaoCls();
            objKBC.Initialize(cmbKyBaoCao);

            SetDate(4);

            BaoCao_Nap();
        }

        private void cmbKyBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            SetDate((sender as ComboBoxEdit).SelectedIndex);
        }

        private void itemRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaoCao_Nap();
        }
    }
}