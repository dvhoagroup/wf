using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.SqlClient;
using BEE.ThuVien;

namespace BEE.NgonNgu
{
    public class DicItem
    {
        public int? DicID { get; set; }
        public string DicValue { get; set; }
        public long? TranID { get; set; }
        public string TranValue { get; set; }
    }

    public class DictionaryNew : List<string>
    {
        private List<string> ltDic;

        public DictionaryNew()
        {
            try
            {
                using (MasterDataContext db = new MasterDataContext())
                {
                    ltDic = db.lgDictionaries.Select(p => p.DicValue.ToLower()).Distinct().ToList();
                }
            }
            catch { }
        }

        public void AddDicValue(string value)
        {
            try
            {
                if (Language.lData.Where(p => p.TranValue == value).Count() > 0) return;

                if (this.Where(p => p == value).Count() > 0) return;

                this.Add(value);
            }
            catch { }
        }

        public void Save()
        {
            try
            {
                using (MasterDataContext db = new MasterDataContext())
                {
                    foreach (var s in this)
                    {
                        if (ltDic.Where(p => p == s.ToLower()).Count() == 0)
                        {
                            var objDic = new lgDictionary();
                            objDic.DicValue = s;
                            db.lgDictionaries.InsertOnSubmit(objDic);
                            ltDic.Add(s.ToLower());
                        }
                    }

                    db.SubmitChanges();

                    this.Clear();
                }
            }
            catch { }
        }
    }

    public class Language
    {
        public static short LangID
        {
            get
            {
                return Properties.Settings.Default.LangID;
            }
            set
            {
                try
                {
                    Properties.Settings.Default.LangID = value;
                    Properties.Settings.Default.Save();

                    if (value != 1)
                    {
                        using (MasterDataContext db = new MasterDataContext())
                        {
                            lData = (from t in db.lgTranslates
                                     join d in db.lgDictionaries on t.DicID equals d.ID
                                     where t.LangID == Language.LangID
                                     select new DicItem() { DicValue = d.DicValue, TranValue = t.TranValue }).ToList();
                        }
                    }
                }
                catch { }
            }
        }

        public static List<DicItem> lData { get; set; }

        private static List<System.Windows.Forms.Control> ListControl;

        private static DictionaryNew DicNew = new DictionaryNew();

        public static void TranslateControl(DevExpress.XtraEditors.XtraForm form, DevExpress.XtraBars.BarManager InputBarManager = null, DevExpress.XtraBars.Ribbon.RibbonControl ribbon = null)
        {
            try
            {
                if (Properties.Settings.Default.LangID == 1) return;

                try
                {
                    if (form.Name.ToUpper() != "frmMain".ToUpper())
                        form.Text = lData.FirstOrDefault(p => p.DicValue.Trim().ToUpper() == form.Text.ToUpper()).TranValue;
                }
                catch { }

                ListControl = new List<System.Windows.Forms.Control>();

                foreach (var item in form.Controls)
                {
                    GetAllControls(item);
                }

                foreach (var item in ListControl)
                {
                    ChangeControls(item);
                }

                #region BarManager
                if (InputBarManager != null)
                {
                    //BarButtonItem
                    foreach (var item in InputBarManager.Items)
                    {
                        if (item is DevExpress.XtraBars.BarButtonItem)
                        {
                            var name = item as DevExpress.XtraBars.BarButtonItem;
                            try
                            {
                                name.Caption = lData.FirstOrDefault(p => p.DicValue.Trim().ToUpper() == name.Caption.Trim().ToUpper()).TranValue;
                            }
                            catch
                            {
                                //DicNew.AddDicValue(name.Caption);
                            }
                        }
                    }

                    //BarEditItem
                    foreach (var item in InputBarManager.Items)
                    {
                        if (item is DevExpress.XtraBars.BarEditItem)
                        {
                            var name = item as DevExpress.XtraBars.BarEditItem;
                            try
                            {
                                name.Caption = lData.FirstOrDefault(p => p.DicValue.Trim().ToUpper() == name.Caption.Trim().ToUpper()).TranValue;
                            }
                            catch
                            {
                                //DicNew.AddDicValue(name.Caption);
                            }

                            try
                            {
                                var sub = name.Edit as DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit;
                                try
                                {
                                    sub.NullText = lData.FirstOrDefault(p => p.DicValue.Trim().ToUpper() == sub.NullText.Trim().ToUpper()).TranValue;
                                }
                                catch { //DicNew.AddDicValue(sub.NullText); 
                                }
                            }
                            catch { }
                        }
                    }

                    //barsubitem
                    foreach (var item in InputBarManager.Items)
                    {
                        if (item is DevExpress.XtraBars.BarSubItem)
                        {
                            var name = item as DevExpress.XtraBars.BarSubItem;
                            try
                            {
                                name.Caption = lData.FirstOrDefault(p => p.DicValue.Trim().ToUpper() == name.Caption.Trim().ToUpper()).TranValue;
                                foreach (DevExpress.XtraBars.LinkPersistInfo link in name.LinksPersistInfo)
                                {
                                    if (link.UserCaption != null && link.UserCaption != "")
                                    {
                                        try
                                        {
                                            link.UserCaption = lData.FirstOrDefault(p => p.DicValue.Trim().ToUpper() == link.UserCaption.Trim().ToUpper()).TranValue;
                                        }
                                        catch { }
                                    }
                                }
                            }
                            catch
                            {
                                //DicNew.AddDicValue(name.Caption);
                            }
                        }
                    }

                    //Link in bar
                    foreach (DevExpress.XtraBars.Bar bar in InputBarManager.Bars)
                    {
                        foreach (DevExpress.XtraBars.LinkPersistInfo link in bar.LinksPersistInfo)
                        {
                            if (link.UserCaption != null && link.UserCaption != "")
                            {
                                try
                                {
                                    link.UserCaption = lData.FirstOrDefault(p => p.DicValue.Trim().ToUpper() == link.UserCaption.Trim().ToUpper()).TranValue;
                                }
                                catch { }
                            }
                        }
                    }
                }
                #endregion

                #region Ribbon
                if (ribbon != null)
                {
                    //BarButtonItem
                    foreach (var item in (ribbon as DevExpress.XtraBars.Ribbon.RibbonControl).Items)
                    {
                        if (item is DevExpress.XtraBars.BarButtonItem)
                        {
                            var name = item as DevExpress.XtraBars.BarButtonItem;
                            try
                            {
                                name.Caption = lData.FirstOrDefault(p => p.DicValue.Trim().ToUpper() == name.Caption.Trim().ToUpper()).TranValue;
                            }
                            catch
                            {
                                //DicNew.AddDicValue(name.Caption);
                            }
                        }

                        //barsubitem
                        if (item is DevExpress.XtraBars.BarSubItem)
                        {
                            var name = item as DevExpress.XtraBars.BarSubItem;
                            try
                            {
                                name.Caption = lData.FirstOrDefault(p => p.DicValue.Trim().ToUpper() == name.Caption.Trim().ToUpper()).TranValue;
                            }
                            catch
                            {
                                //DicNew.AddDicValue(name.Caption);
                            }
                        }
                    }
                    //page
                    foreach (var page in ribbon.Pages)
                    {
                        if (page is DevExpress.XtraBars.Ribbon.RibbonPage)
                        {
                            var name = page as DevExpress.XtraBars.Ribbon.RibbonPage;
                            try
                            {
                                name.Text = lData.FirstOrDefault(p => p.DicValue.Trim().ToUpper() == name.Text.Trim().ToUpper()).TranValue.ToUpper();
                            }
                            catch
                            {
                                //DicNew.AddDicValue(name.Text);
                            }
                        }
                    }

                    //PageGroup
                    foreach (DevExpress.XtraBars.Ribbon.RibbonPage page in ribbon.Pages)
                    {
                        foreach (var pageGroup in page.Groups)
                        {
                            if (pageGroup is DevExpress.XtraBars.Ribbon.RibbonPageGroup)
                            {
                                var name = pageGroup as DevExpress.XtraBars.Ribbon.RibbonPageGroup;
                                try
                                {
                                    name.Text = lData.FirstOrDefault(p => p.DicValue.Trim().ToUpper() == name.Text.Trim().ToUpper()).TranValue;
                                }
                                catch
                                {
                                    //DicNew.AddDicValue(name.Text);
                                }
                            }
                        }
                    }
                }
                #endregion

                //Luu tu dien moi
               // DicNew.Save();
            }
            catch
            {
            }
        }

        public static void TranslateGridControl(DevExpress.XtraGrid.GridControl gc)
        {
            try
            {
                if (Properties.Settings.Default.LangID == 1) return;

                ChangeGridControls(gc);
            }
            catch
            {}
        }

        public static void TranslateUserControl(DevExpress.XtraEditors.XtraUserControl form, DevExpress.XtraBars.BarManager InputBarManager = null, DevExpress.XtraBars.Ribbon.RibbonControl ribbon = null)
        {
            try
            {
                if (Properties.Settings.Default.LangID == 1) return;

                try
                {
                    if (form.Name.ToUpper() != "frmMain".ToUpper())
                        form.Text = lData.FirstOrDefault(p => p.DicValue.Trim().ToUpper() == form.Text.ToUpper()).TranValue;
                }
                catch { }

                ListControl = new List<System.Windows.Forms.Control>();

                foreach (var item in form.Controls)
                {
                    GetAllControls(item);
                }

                foreach (var item in ListControl)
                {
                    ChangeControls(item);
                }

                #region BarManager
                if (InputBarManager != null)
                {
                    //BarButtonItem
                    foreach (var item in InputBarManager.Items)
                    {
                        if (item is DevExpress.XtraBars.BarButtonItem)
                        {
                            var name = item as DevExpress.XtraBars.BarButtonItem;
                            try
                            {
                                name.Caption = lData.FirstOrDefault(p => p.DicValue.Trim().ToUpper() == name.Caption.Trim().ToUpper()).TranValue;
                            }
                            catch
                            {
                                //DicNew.AddDicValue(name.Caption);
                            }
                        }
                    }

                    //BarEditItem
                    foreach (var item in InputBarManager.Items)
                    {
                        if (item is DevExpress.XtraBars.BarEditItem)
                        {
                            var name = item as DevExpress.XtraBars.BarEditItem;
                            try
                            {
                                name.Caption = lData.FirstOrDefault(p => p.DicValue.Trim().ToUpper() == name.Caption.Trim().ToUpper()).TranValue;
                            }
                            catch
                            {
                                //DicNew.AddDicValue(name.Caption);
                            }

                            try
                            {
                                var sub = name.Edit as DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit;
                                try
                                {
                                    sub.NullText = lData.FirstOrDefault(p => p.DicValue.Trim().ToUpper() == sub.NullText.Trim().ToUpper()).TranValue;
                                }
                                catch { //DicNew.AddDicValue(sub.NullText);
                                }
                            }
                            catch { }
                        }
                    }

                    //barsubitem
                    foreach (var item in InputBarManager.Items)
                    {
                        if (item is DevExpress.XtraBars.BarSubItem)
                        {
                            var name = item as DevExpress.XtraBars.BarSubItem;
                            try
                            {
                                name.Caption = lData.FirstOrDefault(p => p.DicValue.Trim().ToUpper() == name.Caption.Trim().ToUpper()).TranValue;
                                foreach (DevExpress.XtraBars.LinkPersistInfo link in name.LinksPersistInfo)
                                {
                                    if (link.UserCaption != null && link.UserCaption != "")
                                    {
                                        try
                                        {
                                            link.UserCaption = lData.FirstOrDefault(p => p.DicValue.Trim().ToUpper() == link.UserCaption.Trim().ToUpper()).TranValue;
                                        }
                                        catch { }
                                    }
                                }
                            }
                            catch
                            {
                                //DicNew.AddDicValue(name.Caption);
                            }
                        }
                    }

                    //Link in bar
                    foreach (DevExpress.XtraBars.Bar bar in InputBarManager.Bars)
                    {
                        foreach (DevExpress.XtraBars.LinkPersistInfo link in bar.LinksPersistInfo)
                        {
                            if (link.UserCaption != null && link.UserCaption != "")
                            {
                                try
                                {
                                    link.UserCaption = lData.FirstOrDefault(p => p.DicValue.Trim().ToUpper() == link.UserCaption.Trim().ToUpper()).TranValue;
                                }
                                catch { }
                            }
                        }
                    }
                }
                #endregion

                #region Ribbon
                if (ribbon != null)
                {
                    //BarButtonItem
                    foreach (var item in (ribbon as DevExpress.XtraBars.Ribbon.RibbonControl).Items)
                    {
                        if (item is DevExpress.XtraBars.BarButtonItem)
                        {
                            var name = item as DevExpress.XtraBars.BarButtonItem;
                            try
                            {
                                name.Caption = lData.FirstOrDefault(p => p.DicValue.Trim().ToUpper() == name.Caption.Trim().ToUpper()).TranValue;
                            }
                            catch
                            {
                                //DicNew.AddDicValue(name.Caption);
                            }
                        }

                        //barsubitem
                        if (item is DevExpress.XtraBars.BarSubItem)
                        {
                            var name = item as DevExpress.XtraBars.BarSubItem;
                            try
                            {
                                name.Caption = lData.FirstOrDefault(p => p.DicValue.Trim().ToUpper() == name.Caption.Trim().ToUpper()).TranValue;
                            }
                            catch
                            {
                                //DicNew.AddDicValue(name.Caption);
                            }
                        }
                    }

                    //page
                    foreach (var page in ribbon.Pages)
                    {
                        if (page is DevExpress.XtraBars.Ribbon.RibbonPage)
                        {
                            var name = page as DevExpress.XtraBars.Ribbon.RibbonPage;
                            try
                            {
                                name.Text = lData.FirstOrDefault(p => p.DicValue.Trim().ToUpper() == name.Text.Trim().ToUpper()).TranValue.ToUpper();
                            }
                            catch
                            {
                                //DicNew.AddDicValue(name.Text);
                            }
                        }
                    }

                    //PageGroup
                    foreach (DevExpress.XtraBars.Ribbon.RibbonPage page in ribbon.Pages)
                    {
                        foreach (var pageGroup in page.Groups)
                        {
                            if (pageGroup is DevExpress.XtraBars.Ribbon.RibbonPageGroup)
                            {
                                var name = pageGroup as DevExpress.XtraBars.Ribbon.RibbonPageGroup;
                                try
                                {
                                    name.Text = lData.FirstOrDefault(p => p.DicValue.Trim().ToUpper() == name.Text.Trim().ToUpper()).TranValue;
                                }
                                catch
                                {
                                    //DicNew.AddDicValue(name.Text);
                                }
                            }
                        }
                    }
                }
                #endregion

                //Luu tu dien moi
                //DicNew.Save();
            }
            catch
            {
            }
        }

        private static void ChangeControls(object item)
        {
            #region Language Control
            //label
            if (item is DevExpress.XtraEditors.LabelControl)
            {
                var name = item as DevExpress.XtraEditors.LabelControl;
                try
                {
                    int length = name.Text.Trim().Length;
                    if (name.Text.Trim().Substring(length - 1, 1) == ":")
                    {
                        name.Text = name.Text.Trim().Substring(0, length - 1);
                        name.Text = lData.FirstOrDefault(p => p.DicValue.Trim().ToUpper() == name.Text.Trim().ToUpper()).TranValue + ":";
                    }
                    else
                    {
                        name.Text = lData.FirstOrDefault(p => p.DicValue.Trim().ToUpper() == name.Text.Trim().ToUpper()).TranValue;
                    }
                }
                catch
                {
                    //DicNew.AddDicValue(name.Text);
                }
            }

            //button
            if (item is DevExpress.XtraEditors.SimpleButton)
            {
                var name = item as DevExpress.XtraEditors.SimpleButton;
                try
                {
                    name.Text = lData.FirstOrDefault(p => p.DicValue.Trim().ToUpper() == name.Text.Trim().ToUpper()).TranValue;
                }
                catch
                {
                    //DicNew.AddDicValue(name.Text);
                }
            }

            //HyperLinkEdit
            if (item is DevExpress.XtraEditors.HyperLinkEdit)
            {
                var name = item as DevExpress.XtraEditors.HyperLinkEdit;
                try
                {
                    name.Text = lData.FirstOrDefault(p => p.DicValue.Trim().ToUpper() == name.Text.Trim().ToUpper()).TranValue;
                }
                catch
                {
                    //DicNew.AddDicValue(name.Text);
                }
            }

            //checkEdit
            if (item is DevExpress.XtraEditors.CheckEdit)
            {
                var name = item as DevExpress.XtraEditors.CheckEdit;
                try
                {
                    name.Text = lData.FirstOrDefault(p => p.DicValue.Trim().ToUpper() == name.Text.Trim().ToUpper()).TranValue;
                }
                catch
                {
                    //DicNew.AddDicValue(name.Text);
                }
            }

            //tab
            if (item is DevExpress.XtraTab.XtraTabPage)
            {
                var name = item as DevExpress.XtraTab.XtraTabPage;
                try
                {
                    name.Text = lData.FirstOrDefault(p => p.DicValue.Trim().ToUpper() == name.Text.Trim().ToUpper()).TranValue;
                }
                catch
                {
                    //DicNew.AddDicValue(name.Text);
                }
            }

            //groupcontrol
            if (item is DevExpress.XtraEditors.GroupControl)
            {
                var name = item as DevExpress.XtraEditors.GroupControl;
                try
                {
                    name.Text = lData.FirstOrDefault(p => p.DicValue.Trim().ToUpper() == name.Text.Trim().ToUpper()).TranValue;
                }
                catch
                {
                    //DicNew.AddDicValue(name.Text);
                }
            }

            //groupbox
            if (item is System.Windows.Forms.GroupBox)
            {
                var name = item as System.Windows.Forms.GroupBox;
                try
                {
                    name.Text = lData.FirstOrDefault(p => p.DicValue.Trim().ToUpper() == name.Text.Trim().ToUpper()).TranValue;
                }
                catch
                {
                    //DicNew.AddDicValue(name.Text);
                }
            }

            //radio
            if (item is DevExpress.XtraEditors.RadioGroup)
            {
                var rdb = item as DevExpress.XtraEditors.RadioGroup;
                foreach (var radio in rdb.Properties.Items)
                {
                    var name = radio as DevExpress.XtraEditors.Controls.RadioGroupItem;

                    try
                    {
                        name.Description = lData.FirstOrDefault(p => p.DicValue.Trim().ToUpper() == name.Description.Trim().ToUpper()).TranValue;
                    }
                    catch
                    {
                        //DicNew.AddDicValue(name.Description);
                    }
                }
            }

            //GridControl
            if (item is DevExpress.XtraGrid.GridControl)
            {
                var grid = item as DevExpress.XtraGrid.GridControl;
                foreach (DevExpress.XtraGrid.Columns.GridColumn col in ((DevExpress.XtraGrid.Views.Base.ColumnView)grid.Views[0]).Columns)
                {
                    try
                    {
                        col.Caption = lData.FirstOrDefault(p => p.DicValue.Trim().ToUpper() == col.Caption.Trim().ToUpper()).TranValue;
                    }
                    catch
                    {
                        //DicNew.AddDicValue(col.Caption);
                    }
                }

                try
                {
                    foreach (DevExpress.XtraGrid.Views.BandedGrid.GridBand col in ((DevExpress.XtraGrid.Views.BandedGrid.BandedGridView)grid.Views[0]).Bands)
                    {
                        try
                        {
                            col.Caption = lData.FirstOrDefault(p => p.DicValue.Trim().ToUpper() == col.Caption.Trim().ToUpper()).TranValue;
                        }
                        catch
                        {
                            //DicNew.AddDicValue(col.Caption);
                        }
                    }
                }
                catch { }
            }

            //VGridControl
            if (item is DevExpress.XtraVerticalGrid.VGridControl)
            {
                var grid = item as DevExpress.XtraVerticalGrid.VGridControl;
                foreach (DevExpress.XtraVerticalGrid.Rows.BaseRow col in grid.Rows)
                {
                    try
                    {
                        col.Properties.Caption = lData.FirstOrDefault(p => p.DicValue.Trim().ToUpper() == col.Properties.Caption.Trim().ToUpper()).TranValue;
                    }
                    catch
                    {
                        //DicNew.AddDicValue(col.Properties.Caption);
                    }
                }
            }

            //nav
            if (item is DevExpress.XtraNavBar.NavBarControl)
            {
                var navControl = item as DevExpress.XtraNavBar.NavBarControl;
                foreach (var nav in navControl.Groups)
                {
                    if (nav is DevExpress.XtraNavBar.NavBarGroup)
                    {
                        var name = nav as DevExpress.XtraNavBar.NavBarGroup;
                        try
                        {
                            name.Caption = lData.FirstOrDefault(p => p.DicValue.Trim().ToLower() == name.Caption.Trim().ToLower()).TranValue;
                            //string caption = name.Caption.Trim().ToUpper().Substring(0, name.Caption.Trim().ToUpper().LastIndexOf(")")).Trim();
                            //name.Caption = lData.FirstOrDefault(p => p.DicValue.Trim().ToUpper() == caption).TranValue;
                            //name.Caption = lData.Where(p => SqlMethods.Like(p.DicValue.Trim().ToUpper(), caption + "%")).FirstOrDefault(p => p.DicValue.Trim().ToUpper() == caption).TranValue;
                        }
                        catch
                        {
                            //DicNew.AddDicValue(name.Caption);
                        }
                    }
                }

                foreach (var nav in navControl.Items)
                {
                    if (nav is DevExpress.XtraNavBar.NavBarItem)
                    {
                        var name = nav as DevExpress.XtraNavBar.NavBarItem;
                        try
                        {
                            name.Caption = lData.FirstOrDefault(p => p.DicValue.Trim().ToLower() == name.Caption.Trim().ToLower()).TranValue;
                        }
                        catch
                        {
                            //DicNew.AddDicValue(name.Caption);
                        }
                    }
                }
            }
            
            #endregion
        }

        private static void ChangeGridControls(object item)
        {
            #region Language Control
            //GridControl
            if (item is DevExpress.XtraGrid.GridControl)
            {
                var grid = item as DevExpress.XtraGrid.GridControl;
                foreach (DevExpress.XtraGrid.Columns.GridColumn col in ((DevExpress.XtraGrid.Views.Base.ColumnView)grid.Views[0]).Columns)
                {
                    try
                    {
                        col.Caption = lData.FirstOrDefault(p => p.DicValue.Trim().ToUpper() == col.Caption.Trim().ToUpper()).TranValue;
                    }
                    catch
                    {
                        //DicNew.AddDicValue(col.Caption);
                    }
                }

                try
                {
                    foreach (DevExpress.XtraGrid.Views.BandedGrid.GridBand col in ((DevExpress.XtraGrid.Views.BandedGrid.BandedGridView)grid.Views[0]).Bands)
                    {
                        try
                        {
                            col.Caption = lData.FirstOrDefault(p => p.DicValue.Trim().ToUpper() == col.Caption.Trim().ToUpper()).TranValue;
                        }
                        catch
                        {
                            //DicNew.AddDicValue(col.Caption);
                        }
                    }
                }
                catch { }
            }
            #endregion
        }

        private static void GetAllControls(object control)
        {
            ListControl.Add((System.Windows.Forms.Control)control);
            foreach (System.Windows.Forms.Control ctl in ((System.Windows.Forms.Control)control).Controls)
            {
                ListControl.Add(ctl);
                GetAllControls(ctl);
            }
        }

        public static string ConvertLanguage(string str)
        {
            try { 
                return lData.FirstOrDefault(p => p.DicValue.Trim().ToUpper() == str.Trim().ToUpper()).TranValue; 
            }
            catch { }
            return str;
        }
    }
}
