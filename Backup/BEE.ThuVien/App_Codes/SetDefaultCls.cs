using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
    public class SetDefaultCls
    {
        public byte SetID;
        public byte ThoiGian;
        public double SoTien;

        public SetDefaultCls()
        {
        }

        public SetDefaultCls(byte _SetID)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("SetDefault_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SetID", _SetID);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                SetID = byte.Parse(dread["SetID"].ToString());
                ThoiGian = byte.Parse(dread["ThoiGian"].ToString());
                SoTien = double.Parse(dread["SoTien"].ToString());
            }
            sqlCon.Close();
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("SetDefault_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SetID", SetID);
            sqlCmd.Parameters.AddWithValue("@ThoiGian", ThoiGian);
            sqlCmd.Parameters.AddWithValue("@SoTien", SoTien);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }
    }
}