using System;
using System.Data;
using System.Data.SqlClient;

namespace it
{
    public class ChuyenQuyenBDSCls
    {
        public string MaCQ { get; set; }
        public string MaBDS { get; set; }
        public int MaNVCu { get; set; }
        public int MaNVMoi { get; set; }
        public int MaNV { get; set; }
        public string LyDo { get; set; }

        public void Insert()
        {
            using (SqlCommand sqlCmd = new SqlCommand("ChuyenQuyenBDS_Insert"))
            {
                sqlCmd.Parameters.AddWithValue("MaBDS", MaBDS);
                sqlCmd.Parameters.AddWithValue("MaNVCu", MaNVCu);
                sqlCmd.Parameters.AddWithValue("MaNVMoi", MaNVMoi);
                sqlCmd.Parameters.AddWithValue("MaNV", MaNV);
                sqlCmd.Parameters.AddWithValue("LyDo", LyDo);
                SqlCommon.exeCuteNonQueryPro(sqlCmd);
            }
        }

        public void Update()
        {
            using (SqlCommand sqlCmd = new SqlCommand("ChuyenQuyenBDS_Update"))
            {
                sqlCmd.Parameters.AddWithValue("MaCQ", MaCQ);
                sqlCmd.Parameters.AddWithValue("MaNV", MaNV);
                sqlCmd.Parameters.AddWithValue("LyDo", LyDo);
                SqlCommon.exeCuteNonQueryPro(sqlCmd);
            }
        }

        public DataRow getDetail()
        {
            return SqlCommon.getData("ChuyenQuyenBDS_getDetail N'" + MaCQ + "'").Rows[0];
        }

        public static DataTable getAll(string maBDS)
        {
            return SqlCommon.getData("ChuyenQuyenBDS_getAll N'" + maBDS + "'");
        }
    }
}
