﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Net;
using System.Linq;
using BEE.ThuVien;
using System.Drawing.Imaging;
using System.IO;

namespace BEE.FTP
{
    public partial class frmUploadFile : DevExpress.XtraEditors.XtraForm
    {
        public frmUploadFile()
        {
            InitializeComponent();

            this.Shown += new EventHandler(frmUploadFile_Shown);
        }

        string ConvertNoSign(string url)
        {
            string temp = url;
            string ext = temp.Substring(temp.LastIndexOf('.'));
            string main = url.Substring(0, url.LastIndexOf('.'));

            url = it.CommonCls.TiegVietKhongDauURL(main) + ext;

            return url;
        }

        void UploadImage()
        {
            try
            {
                this.Update();
                //

                //Image img;
                //it.ResizeImage objResize = new it.ResizeImage(ClientPath);
                //img = objResize.ResizeImages(new Size(1024, 768));
                Image img = Image.FromFile(ClientPath);

                string Path = Application.StartupPath + "\\temp.jpeg";
                img.Save(Path, ImageFormat.Jpeg);

                var _FileInfo = new System.IO.FileInfo(Path);
                System.IO.FileStream _FileStream = _FileInfo.OpenRead();

                //
                var ftp = new FtpClient();
                //
                ftp.Url = this.Folder;
                ftp.MakeDirectory();
                //
                this.FileName = ClientPath.Substring(ClientPath.LastIndexOf(@"\") + 1);
                this.FileName = FileName.Insert(FileName.LastIndexOf('.'), DateTime.Now.ToFileTime().ToString());
                this.FileName = this.Folder + "/" + ConvertNoSign(this.FileName);
                ftp.Url = this.FileName;
                var _Stream = ftp.UploadFile(_FileInfo.Length);
                //
                long len = _FileStream.Length;
                long index = 0;
                progress.Position = 0;
                lblPross.Text = string.Format("Đã tải lên {0} trên tổng số {1} bytes...", index, len);
                this.Update();
                // Read from the file stream 2kb at a time
                int buffLength = 2048;
                byte[] buff = new byte[buffLength];
                int contentLen = _FileStream.Read(buff, 0, buffLength);
                // Till Stream content ends
                while (contentLen != 0)
                {
                    // Write Content from the file stream to the FTP Upload Stream
                    _Stream.Write(buff, 0, contentLen);
                    //
                    index += contentLen;
                    progress.Position = Convert.ToInt32(index / len);
                    lblPross.Text = string.Format("Đã tải lên {0} trên tổng số {1} bytes...", index, len);
                    this.Update();
                    //
                    contentLen = _FileStream.Read(buff, 0, buffLength);
                }

                // Close the file stream and the Request Stream
                _Stream.Close();
                _Stream.Dispose();
                _FileStream.Close();
                _FileStream.Dispose();

                FileSize = Convert.ToInt32(len / 1024);
                if (FileSize <= 0) FileSize = 1;

                this.WebUrl = ftp.WebUrl.Trim('/') + "/" + this.FileName;

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.Message, "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void UploadOther()
        {
            try
            {
                this.Update();
                //
                var _FileInfo = new System.IO.FileInfo(ClientPath);

                System.IO.FileStream _FileStream = _FileInfo.OpenRead();
                //
                var ftp = new FtpClient();
                //
                ftp.Url = this.Folder;
                ftp.MakeDirectory();
                //
                this.FileName = ClientPath.Substring(ClientPath.LastIndexOf(@"\") + 1);
                this.FileName = FileName.Insert(FileName.LastIndexOf('.'), DateTime.Now.ToFileTime().ToString());
                this.FileName = this.Folder + "/" + ConvertNoSign(this.FileName);
                ftp.Url = this.FileName;
                var _Stream = ftp.UploadFile(_FileInfo.Length);
                //
                long len = _FileStream.Length;
                long index = 0;
                progress.Position = 0;
                lblPross.Text = string.Format("Đã tải lên {0} trên tổng số {1} bytes...", index, len);
                this.Update();
                // Read from the file stream 2kb at a time
                int buffLength = 2048;
                byte[] buff = new byte[buffLength];
                int contentLen = _FileStream.Read(buff, 0, buffLength);
                // Till Stream content ends
                while (contentLen != 0)
                {
                    // Write Content from the file stream to the FTP Upload Stream
                    _Stream.Write(buff, 0, contentLen);
                    //
                    index += contentLen;
                    progress.Position = Convert.ToInt32(index / len);
                    lblPross.Text = string.Format("Đã tải lên {0} trên tổng số {1} bytes...", index, len);
                    this.Update();
                    //
                    contentLen = _FileStream.Read(buff, 0, buffLength);
                }

                // Close the file stream and the Request Stream
                _Stream.Close();
                _Stream.Dispose();
                _FileStream.Close();
                _FileStream.Dispose();

                FileSize = Convert.ToInt32(len / 1024);
                if (FileSize <= 0) FileSize = 1;

                this.WebUrl = ftp.WebUrl.Trim('/') + "/" + this.FileName;

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.Message, "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void frmUploadFile_Shown(object sender, EventArgs e)
        {
            if (IsImage)
                UploadImage();
            else
                UploadOther();
        }

        public string ClientPath { get; set; }

        public string FileName { get; set; }

        public int FileSize { get; set; }

        public string Folder { get; set; }

        public string WebUrl { get; set; }

        bool IsImage { get; set; }
        public int Type { get; set; }

        public bool SelectFile(bool isImg)
        {
            IsImage = isImg;
            var file = new OpenFileDialog();
            if (isImg)
                file.Filter = "Image (.jpg, .gif, .png)|*.jpg;*.gif;*.png";
            else if (Type == 2) file.Filter = "Video (.FLV, .MP4, .MOV, .WMV, .AVI, .VOB, .AVC, .3GP, .HEVC, .F4V, .MKV, .TS, .MPEG-2, .WEBM)|*.flv;*.mp4;*.mov;*.wmv;*.avi;*.vob;*.avc;*.3gp;*.hevc;*.f4v;*.mkv;*.ts;*.mpeg-2;*.webm";
            else
                file.Filter = "Word(.doc, .docx)|*.doc;*.docx|Excel(.xls,.xlsx)|*.xls;*.xlsx|Winrar(.rar, .zip)|*.rar;*.zip|Video(FLV)|*.flv|Flash|*.swf|All file|*.*";

            if (file.ShowDialog() == DialogResult.OK)
            {
                this.ClientPath = file.FileName;
                this.FileName = ClientPath.Substring(ClientPath.LastIndexOf(@"\") + 1);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void UploadFile_frm_Load(object sender, EventArgs e)
        {

        }
    }
}