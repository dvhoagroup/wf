using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
    public class DuAn_TienDoCls
    {
        public int MaDA;
        public byte DotTT;
        public byte STT;
        public int MaNV;
        public DateTime NgayCN, NgayHT;
        public string FileAttach, DienGiai;

        public DuAn_TienDoCls()
        {
        }

        public DuAn_TienDoCls(int _MaDA, byte _DotTT, byte _STT)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("DuAn_TienDo_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaDA", _MaDA);
            sqlCmd.Parameters.AddWithValue("@DotTT", _DotTT);
            sqlCmd.Parameters.AddWithValue("@STT", _STT);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                MaDA = int.Parse(dread["MaDA"].ToString());
                DotTT = byte.Parse(dread["DotTT"].ToString());
                STT = byte.Parse(dread["STT"].ToString());
                MaNV = int.Parse(dread["MaNV"].ToString());
                if (dread["NgayCN"].ToString() != "")
                    NgayCN = (DateTime)dread["NgayCN"];
                if (dread["NgayHT"].ToString() != "")
                    NgayHT = (DateTime)dread["NgayHT"];
                FileAttach = dread["FileAttach"] as string;
                DienGiai = dread["DienGiai"] as string;
            }
            sqlCon.Close();
        }

        public void Insert()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("DuAn_TienDo_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaDA", MaDA);
            sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
            sqlCmd.Parameters.AddWithValue("@MaNV", MaNV);
            sqlCmd.Parameters.AddWithValue("@NgayCN", NgayCN);
            sqlCmd.Parameters.AddWithValue("@NgayHT", NgayHT);
            sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
            sqlCmd.Parameters.AddWithValue("@FileAttach", FileAttach);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("DuAn_TienDo_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaDA", MaDA);
            sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
            sqlCmd.Parameters.AddWithValue("@STT", STT);
            sqlCmd.Parameters.AddWithValue("@MaNV", MaNV);
            sqlCmd.Parameters.AddWithValue("@NgayCN", NgayCN);
            sqlCmd.Parameters.AddWithValue("@NgayHT", NgayHT);
            sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
            sqlCmd.Parameters.AddWithValue("@FileAttach", FileAttach);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("DuAn_TienDo_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectBy()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("DuAn_TienDo_getByMaDA_DotTT " + MaDA + "," + DotTT, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("DuAn_TienDo_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaDA", MaDA);
            sqlCmd.Parameters.AddWithValue("@DotTT", DotTT);
            sqlCmd.Parameters.AddWithValue("@STT", STT);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }
    }
}