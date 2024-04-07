using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
//using Microsoft.Office.Interop.Word;
//using Microsoft.Office.Interop.Excel;
using System.IO;

namespace BEEREMA.Test
{
    public partial class ReadFileWord_frm : DevExpress.XtraEditors.XtraForm
    {
        public ReadFileWord_frm()
        {
            InitializeComponent();
        }

        private void ReadFileWord_frm_Load(object sender, EventArgs e)
        {
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //OpenFileDialog ofd = new OpenFileDialog();
            //if (ofd.ShowDialog() == DialogResult.OK)
            //{
            //    //StreamReader o = new StreamReader(ofd.FileName);
            //    //richTextBox1.LoadFile(ofd.FileName, RichTextBoxStreamType.UnicodePlainText);                
            //    ApplicationClass wordApp = new ApplicationClass();

            //    object wFileName = ofd.FileName;
            //    object wConfirmConversions = false;
            //    object wReadOnly = false;
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

        //private void ImportFileFromHDD()
        //{                        
        //    openFileDialog1.Filter = "RichText (*.rtf;*.doc)|*.rtf;*.doc"; 
        //    openFileDialog1.Filter += "|Text Files (*.txt;*.ini)|*.txt;*.ini"; 
        //    openFileDialog1.DefaultExt = ".rtf";                        
        //    openFileDialog1.Filter = "All Document Files|*.rtf;*.doc;*.txt;*.ini;*.html;*.htm|" +"Rich Text Files(*.rtf)|*.rtf|" +"Word Document Files(*.doc)|*.doc|" +"Text Files(*.txt)|*.txt|" +"HTML Files(*.html)|*.html|" +"All Files(*.*)|*.*";
        //    //openFileDialog1.Filter += "|All File (*.*)|*.*";                        
        //    openFileDialog1.FileName = "";                        
        //    openFileDialog1.FilterIndex = 0;                        
        //    openFileDialog1.InitialDirectory = "MyDocuments";           
        //    openFileDialog1.CheckFileExists = true;                      
        //    openFileDialog1.CheckPathExists = true;                      
        //    openFileDialog1.Multiselect = false;                  
        //    if (openFileDialog1.ShowDialog() == DialogResult.OK)     
        //    {                               
        //        if (openFileDialog1.FileNames.Length > 0)   
        //        {
        //            string exe = "";                     
        //            int p = openFileDialog1.FileNames[0].LastIndexOf(".");   
        //            if (p > -1)                                        
        //            {                                                
        //                exe = openFileDialog1.FileNames[0].Substring(p); 
        //                exe = exe.Trim();                              
        //            }                                        
        //            switch (exe)
        //            {
        //                case ".rtf":                        
        //                    richTextBox1.LoadFile(openFileDialog1.FileNames[0],RichTextBoxStreamType.RichText);                                                        break;                                                
        //                case ".txt":
        //                    if (DialogBox.Infomation(this, "Unicode Plain Text?", "Text", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)                                                        
        //                    {
        //                        richTextBox1.LoadFile(openFileDialog1.FileNames[0], RichTextBoxStreamType.UnicodePlainText); 
        //                    }                                                        
        //                    else             
        //                    {
        //                        richTextBox1.LoadFile(openFileDialog1.FileNames[0], RichTextBoxStreamType.PlainText);
        //                    } 
        //                    break;
        //                case ".ini":
        //                    if (DialogBox.Infomation(this, "Unicode Plain Text?", "Text", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) 
        //                    {
        //                        richTextBox1.LoadFile(openFileDialog1.FileNames[0], RichTextBoxStreamType.UnicodePlainText);
        //                    }
        //                    else {
        //                        richTextBox1.LoadFile(openFileDialog1.FileNames[0], RichTextBoxStreamType.PlainText);
        //                    } 
        //                    break;
        //                case ".doc": 
        //                   StreamReader sr1 = new StreamReader(openFileDialog1.FileName.Trim()); 
        //                   OfficeFileReader.OfficeFileReader objOFR = new OfficeFileReader.OfficeFileReader(); 
        //                   string output = ""; 
        //                   objOFR.GetText(openFileDialog1.FileName.Trim(), ref output);
        //                   richTextBox1.Text = output; break;
        //                default: 
        //                    Stream stream; 
        //                    stream = openFileDialog1.OpenFile(); 
        //                    StreamReader sr = new StreamReader(stream);
        //                    richTextBox1.Text = sr.ReadToEnd(); sr.Close(); break;
        //            }
        //        }
        //    }
        //}        
    }
    //public class OfficeFileReader
    //{
    //    public void GetText(String path, ref string text)
    //    // path is the path of the .doc, .xls or .ppt  file                
    //    // text is the variable in which all the extracted text will be stored                
    //    {
    //        String result = "";
    //        int count = 0;
    //        try
    //        {
    //            IFilter ifilt = (IFilter)(new CFilter());
    //            //System.Runtime.InteropServices.UCOMIPersistFile ipf = (System.Runtime.InteropServices.UCOMIPersistFile)(ifilt);
    //            System.Runtime.InteropServices.ComTypes.IPersistFile ipf = (System.Runtime.InteropServices.ComTypes.IPersistFile)(ifilt);
    //            ipf.Load(@path, 0);
    //            uint i = 0;
    //            STAT_CHUNK ps = new STAT_CHUNK();
    //            ifilt.Init(IFILTER_INIT.NONE, 0, null, ref i);
    //            int hr = 0;
    //            while (hr == 0)
    //            {
    //                ifilt.GetChunk(out ps);
    //                if (ps.flags == CHUNKSTATE.CHUNK_TEXT)
    //                {
    //                    uint pcwcBuffer = 1000;
    //                    int hr2 = 0;
    //                    while (hr2 == Constants.FILTER_S_LAST_TEXT || hr2 == 0)
    //                    {
    //                        try
    //                        {
    //                            pcwcBuffer = 1000;
    //                            System.Text.StringBuilder sbBuffer = new StringBuilder((int)pcwcBuffer);
    //                            hr2 = ifilt.GetText(ref pcwcBuffer, sbBuffer);
    //                            // Console.WriteLine(pcwcBuffer.ToString());   
    //                            if (hr2 >= 0) result += sbBuffer.ToString(0, (int)pcwcBuffer);
    //                            //textBox1.Text +="\n";                                         
    //                            // result += "#########################################";    
    //                            count++;
    //                        }
    //                        catch (System.Runtime.InteropServices.COMException myE)
    //                        {
    //                            Console.WriteLine(myE.Data + "\n" + myE.Message + "\n");
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //        catch (System.Runtime.InteropServices.COMException myE)
    //        {
    //            Console.WriteLine(myE.Data + "\n" + myE.Message + "\n");
    //        }
    //        text = result;
    //        //return count;         
    //        return;
    //    }
    //}
}