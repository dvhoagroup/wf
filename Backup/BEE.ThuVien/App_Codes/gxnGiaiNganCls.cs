using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
    public class gxnGiaiNganCls
    {
        public int MaGXN;
        public string SoGXN;
        public DateTime NgayXN;
        public int MaKH;
        public int MaHDMB;
        public int MaHDGV;
        public double SoTien;

        public gxnGiaiNganCls()
        {
        }

        public gxnGiaiNganCls(int _MaGXN)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("gxnGiaiNgan_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaGXN", _MaGXN);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                MaGXN = int.Parse(dread["MaGXN"].ToString());
                SoGXN = dread["SoGXN"] as string;
                NgayXN = (DateTime)dread["NgayXN"];
                MaKH = int.Parse(dread["MaKH"].ToString());
                MaHDMB = int.Parse(dread["MaHDMB"].ToString());
                MaHDGV = int.Parse(dread["MaHDGV"].ToString());
                SoTien = double.Parse(dread["SoTien"].ToString());
            }
            sqlCon.Close();
        }

        public void Insert()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("gxnGiaiNgan_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SoGXN", SoGXN);
            sqlCmd.Parameters.AddWithValue("@NgayXN", NgayXN);
            sqlCmd.Parameters.AddWithValue("@MaKH", MaKH);
            sqlCmd.Parameters.AddWithValue("@MaHDMB", MaHDMB);
            sqlCmd.Parameters.AddWithValue("@MaHDGV", MaHDGV);
            sqlCmd.Parameters.AddWithValue("@SoTien", SoTien);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("gxnGiaiNgan_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaGXN", MaGXN);
            sqlCmd.Parameters.AddWithValue("@SoGXN", SoGXN);
            sqlCmd.Parameters.AddWithValue("@NgayXN", NgayXN);
            sqlCmd.Parameters.AddWithValue("@MaKH", MaKH);
            sqlCmd.Parameters.AddWithValue("@MaHDMB", MaHDMB);
            sqlCmd.Parameters.AddWithValue("@MaHDGV", MaHDGV);
            sqlCmd.Parameters.AddWithValue("@SoTien", SoTien);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("gxnGiaiNgan_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("gxnGiaiNgan_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaGXN", MaGXN);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }
    }
}