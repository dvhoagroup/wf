using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
    public class bdsNotesTransferCls
    {
        public string MaBDS { get; set; }
        public int STT { get; set; }
        public int MaNVC { get; set; }
        public int MaNVOld { get; set; }
        public int MaNVNew { get; set; }
        public DateTime NgayChyen { get; set; }

        public bdsNotesTransferCls()
        {
        }

        public void Insert()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("bdsNotesTransfer_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaBDS", MaBDS);
            sqlCmd.Parameters.AddWithValue("@MaNVC", MaNVC);
            sqlCmd.Parameters.AddWithValue("@MaNVNew", MaNVNew);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("bdsNotesTransfer_getAll '" + MaBDS + "'", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }
    }
}