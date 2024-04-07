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
using BEEREMA;

namespace BEE.NgonNgu
{
    public partial class frmTranslate : DevExpress.XtraEditors.XtraForm
    {
        MasterDataContext db = new MasterDataContext();

        public frmTranslate()
        {
            InitializeComponent();

            this.Load += new EventHandler(frmTranslate_Load);

            itemDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemDelete_ItemClick);
            itemSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemSave_ItemClick);
            itemClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemClose_ItemClick);
            itemAddnew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemAddnew_ItemClick);
            gvTrans.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(gvTrans_InvalidRowException);
            gvTrans.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(gvTrans_ValidateRow);
        }

        void gvTrans_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var dicValue = gvTrans.GetRowCellValue(e.RowHandle, "DicValue") as string;
            if (dicValue == null && dicValue.Trim() == "")
            {
                e.ErrorText = "Vui lòng nhập từ điển tiếng việt";
                e.Valid = false;
                return;
            }

            //for (int i = 0; i < gvTrans.RowCount; i++)
            //{
            //    if (i == e.RowHandle) continue;
            //    if (dicValue == gvTrans.GetRowCellValue(i, "DicValue") as string)
            //    {
            //        e.ErrorText = "Từ điển đã tồn tại";
            //        e.Valid = false;
            //        return;
            //    }
            //}
        }

        void gvTrans_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            DialogBox.Error(e.ErrorText);
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        void itemAddnew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var ltTrans = (List<DicItem>)gcTrans.DataSource;
            ltTrans.Add(new DicItem());
            gcTrans.DataSource = ltTrans;
            gcTrans.RefreshDataSource();

            gvTrans.Focus();
            gvTrans.FocusedRowHandle = ltTrans.Count - 1;
            gvTrans.FocusedColumn = gvTrans.Columns[0]; 
        }

        void itemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var indexs = gvTrans.GetSelectedRows();
            if (indexs.Length == 0)
            {
                DialogBox.Error("Vui lòng chọn dòng cần xóa");
                return;
            }

            if (DialogBox.Question() == System.Windows.Forms.DialogResult.No) return;

            var wait = DialogBox.WaitingForm();

            try
            {
                foreach (var i in indexs)
                {
                    var dicID = (int)gvTrans.GetRowCellValue(i, "DicID");
                    if (dicID != 0)
                    {
                        var objDic = db.lgDictionaries.Single(p => p.ID == dicID);
                        db.lgDictionaries.DeleteOnSubmit(objDic);
                    }
                }

                db.SubmitChanges();

                gvTrans.DeleteSelectedRows();
                gvTrans.RefreshData();

                DialogBox.Infomation("Dữ liệu đã được xóa");
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

        void itemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        void itemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var ltTrans = (List<DicItem>)gcTrans.DataSource;

            if (ltTrans.Count == 0)
            {
                DialogBox.Error("Vui lòng nhập từ điển");
                return;
            }

            var wait = DialogBox.WaitingForm();

            try
            {
                gvTrans.RefreshData();

                var ltTranDB = db.lgTranslates.Where(p => p.LangID == Language.LangID).Select(p => p.TranValue).ToList();
                var ltDicDB = db.lgDictionaries.Select(p => p.DicValue).ToList();

                foreach (var i in ltTrans)
                {
                    if (i.DicValue == null || i.DicValue.Trim() == "") continue;

                    lgDictionary objDic = null;
                    
                    if (i.DicID == null)
                    {
                        objDic = new lgDictionary();
                        objDic.DicValue = i.DicValue.Trim();
                        db.lgDictionaries.InsertOnSubmit(objDic);
                    }
                    else
                    {
                        if (ltDicDB.Where(p => p == i.DicValue.Trim()).Count() == 0)
                        {
                            objDic = db.lgDictionaries.Single(p => p.ID == i.DicID);
                            objDic.DicValue = i.DicValue.Trim();
                        }
                    }

                    if (i.TranValue != null && i.TranValue.Trim() != "")
                    {
                        lgTranslate objTran;
                        if (i.TranID == null)
                        {
                            objTran = new lgTranslate();
                            objTran.LangID = Language.LangID;
                            if (i.DicID != null)
                            {
                                objTran.DicID = i.DicID;
                            }
                            else
                            {
                                objTran.lgDictionary = objDic;
                            }
                            objTran.TranValue = i.TranValue.Trim();
                            db.lgTranslates.InsertOnSubmit(objTran);
                        }
                        else
                        {
                            if (ltTranDB.Where(p => p == i.TranValue.Trim()).Count() == 0)
                            {
                                objTran = db.lgTranslates.Single(p => p.ID == i.TranID);
                                objTran.TranValue = i.TranValue.Trim();
                            }
                        }
                    }
                    else if (i.TranID != null)
                    {
                        var objTran = db.lgTranslates.Single(p => p.ID == i.TranID);
                        db.lgTranslates.DeleteOnSubmit(objTran);
                    }
                }

                db.SubmitChanges();

                DialogBox.Infomation("Dữ liệu đã được lưu");
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

        void frmTranslate_Load(object sender, EventArgs e)
        {
            try
            {
                colTran.Caption = (from l in db.lgLanguages where l.ID == Language.LangID select l.LangName).First();

                gcTrans.DataSource = (from d in db.lgDictionaries
                                      join t in
                                          (from tr in db.lgTranslates
                                           where tr.LangID == Language.LangID
                                           select new { tr.ID, tr.TranValue, tr.DicID })
                                        on d.ID equals t.DicID into tran
                                      from t in tran.DefaultIfEmpty()
                                      orderby d.DicValue
                                      select new DicItem()
                                      {
                                          DicID = d.ID,
                                          DicValue = d.DicValue,
                                          TranID = t.ID,
                                          TranValue = t.TranValue
                                      }).ToList();
            }
            catch (Exception ex)
            {
                DialogBox.Error(ex.Message);
            }
            
        }
    }
}