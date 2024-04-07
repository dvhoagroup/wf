using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
//using Microsoft.Office.Interop.Word;

namespace BEE.NghiepVuKhac
{
    public partial class Review_frm : DevExpress.XtraEditors.XtraForm
    {
        public string Content = "";
        public Review_frm()
        {
            InitializeComponent();

            BEE.NgonNgu.Language.TranslateControl(this);
        }

        private void Review_frm_Load(object sender, EventArgs e)
        {
            richEditControl1.RtfText = Content;
            this.TopMost = true;
            this.TopMost = false;
            //OpenFileDialog ofd = new OpenFileDialog();
            //if (ofd.ShowDialog() == DialogResult.OK)
            //{
            //    //StreamReader o = new StreamReader(ofd.FileName);
            //    //richTextBox1.LoadFile(ofd.FileName, RichTextBoxStreamType.UnicodePlainText);                
            //    ApplicationClass wordApp = new ApplicationClass();

            //    object wFileName = ofd.FileName;
            //    object wConfirmConversions = true;
            //    object wReadOnly = true;
            //    object wAddToRecentFiles = true;
            //    object wPasswordDocument = "";
            //    object wPasswordTemplate = "";
            //    object wRevert = true;
            //    object wWritePasswordDocument = "";
            //    object wWritePasswordTemplate = "";
            //    object wFormat = WdOpenFormat.wdOpenFormatAuto;
            //    object wEncoding = Microsoft.Office.Core.MsoEncoding.msoEncodingAutoDetect;
            //    object wVisible = false;
            //    object wOpenAndRepair = true;
            //    object wDocumentDirection = WdDocumentDirection.wdRightToLeft;
            //    object wNoEncodingDialog = false;
            //    object wXMLTransform = System.Reflection.Missing.Value;

            //    object nullobj = System.Reflection.Missing.Value;
            //    wordApp.Visible = false;
            //    Document doc = wordApp.Documents.Open(ref wFileName, ref wConfirmConversions, ref nullobj, ref wAddToRecentFiles, ref wPasswordDocument, ref wPasswordTemplate, ref wRevert, ref wWritePasswordDocument, ref wWritePasswordTemplate, ref wFormat, ref wEncoding, ref wVisible, ref wOpenAndRepair, ref wDocumentDirection, ref wNoEncodingDialog, ref wXMLTransform);
            //    doc.ActiveWindow.Selection.WholeStory();
            //    doc.ActiveWindow.Selection.Copy();
            //    IDataObject data = Clipboard.GetDataObject();
            //    richEditControl1.RtfText = data.GetData(DataFormats.Rtf).ToString();
            //}
        }
    }
}