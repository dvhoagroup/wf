using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
    public class DuAn_LichThanhToanCls
    {
        public int MaDA;
        public byte DotTT;
        public byte TyLeTT;
        public DateTime NgayTT;
        public string DienGiai;
        public int SoNgay;
        public int SoThang;

        public DuAn_LichThanhToanCls()
        {
        }

        public DuAn_LichThanhToanCls(int _MaDA, byte _DotTT)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("DuAn_LichThanhToan_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaDA", _MaDA);
            sqlCmd.Parameters.AddWithValue("@DotTT", _DotTT);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                MaDA = int.Parse(dread["MaDA"].ToString());
                DotTT = byte.Parse(dread["DotTT"].ToString());
                TyLeTT = byte.Parse(dread["TyLeTT"].ToString());
                if (dread["NgayTT"].ToString() != "")
                    NgayTT = (DateTime)dread["NgayTT"];
                DienGiai = dread["DienGiai"] as string;
                SoNgay = int.Parse(dread["SoNgay"].ToString());
                SoThang = int.Parse(dread["SoThang"].ToString());
            }
            sqlCon.Close();
        }

        public void Insert()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("DuAn_LichThanhToan_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TyLeTT", TyLeTT);
            if (NgayTT.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayTT", NgayTT);
            else
                sqlCmd.Parameters.AddWithValue("@NgayTT", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
            sqlCmd.Parameters.AddWithValue("@SoNgay", SoNgay);
            sqlCmd.Parameters.AddWithValue("@SoThang", SoThang);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("DuAn_LichThanhToan_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaDA", MaDA);
            sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
            sqlCmd.Parameters.AddWithValue("@TyLeTT", TyLeTT);
            if (NgayTT.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayTT", NgayTT);
            else
                sqlCmd.Parameters.AddWithValue("@NgayTT", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
            sqlCmd.Parameters.AddWithValue("@SoNgay", SoNgay);
            sqlCmd.Parameters.AddWithValue("@SoThang", SoThang);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("DuAn_LichThanhToan_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectBY()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("DuAn_LichThanhToan_getByMaDA " + MaDA, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("DuAn_LichThanhToan_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaDA", MaDA);
            sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }
    }
}