using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
    public class khNotesTransferCls
    {
        public int MaKH { get; set; }
        public int STT { get; set; }
        public int MaNVC { get; set; }
        public int MaNVOld { get; set; }
        public int MaNVNew { get; set; }
        public bool IsAgent { get; set; }
        public DateTime NgayChyen { get; set; }

        public khNotesTransferCls()
        {
        }

        public void Insert()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("khNotesTransfer_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaKH", MaKH);
            sqlCmd.Parameters.AddWithValue("@MaNVC", MaNVC);
            sqlCmd.Parameters.AddWithValue("@MaNVNew", MaNVNew);
            sqlCmd.Parameters.AddWithValue("@IsAgent", IsAgent);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("khNotesTransfer_getAll " + MaKH, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }
    }
}