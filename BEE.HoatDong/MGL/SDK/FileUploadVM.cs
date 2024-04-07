using System;
using System.Collections.Generic;

namespace BEE.HoatDong.MGL.SDK
{
    public class FileUploadVM
    {
        public string FileId { get; set; }
        /// <summary>
        /// Key duy nhat tren S3
        /// </summary>
        public string FileNameKeyOnS3 { get; set; }
        /// <summary>
        /// ten file without extension
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets the name of the file. (bao gom name va extension )
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// duoi file
        /// </summary>
        public string FileExtension { get; set; }
        /// <summary>
        /// Gets the size, in bytes, of the current file.
        /// 1kb = 1000 bytes, 1MB = 10^6 bytes
        /// </summary>
        public long? FileSize { get; set; }

        /// <summary>
        /// Quy dinh loai file hinh anh, video, doc...
        /// </summary>
        public int? FileType { get; set; }

        /// <summary>
        /// Gets the raw Content-Type header of the uploaded file. (vd: image/png)
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// File da duoc tai len s3
        /// </summary>
        public bool? Uploaded { get; set; }
        /// <summary>
        /// Link file tu s3 (co gtri khi upload thanh cong)
        /// </summary>
        public string UrlS3 { get; set; }

        /// <summary>
        /// Gets or sets thoi gian gui request, in coordinated universal time (UTC)
        /// </summary>
        public DateTime? DateTimeRequest { get; set; }
        /// <summary>
        /// ets or sets thoi gian phan hoi sau upload, in coordinated universal time (UTC)
        /// </summary>
        public DateTime? DateTimeResponse { get; set; }

        /// <summary>
        /// danh sach ca anh thumbnail
        /// </summary>
        public List<string> LstUrlThumbnail { get; set; }
    }

    public class ResponseMultiUploadFile
    {
        public int? Status { get; set; }
        public string messeger { get; set; }
        public int? Count { get; set; }
        public List<FileUploadVM> Files { get; set; }
    }
}