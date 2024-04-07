using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
    public class hdmgQuaTrinhThucHienCls
    {
        public int MaHDMG;
        public byte Lan;
        public DateTime NgayTH;
        public hdmbTinhTrangCls TinhTrang = new hdmbTinhTrangCls();
        public string DienGiai;
        public NhanVienCls NhanVien = new NhanVienCls();

        public hdmgQuaTrinhThucHienCls()
        {
        }

        public hdmgQuaTrinhThucHienCls(int _MaHDMG, byte _Lan)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdmgQuaTrinhThucHien_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDMG", _MaHDMG);
            sqlCmd.Parameters.AddWithValue("@Lan", _Lan);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                MaHDMG = int.Parse(dread["MaHDMG"].ToString());
                Lan = byte.Parse(dread["Lan"].ToString());
                NgayTH = (DateTime)dread["NgayTH"];
                TinhTrang.MaTT = byte.Parse(dread["MaTT"].ToString());
                DienGiai = dread["DienGiai"] as string;
                NhanVien.MaNV = int.Parse(dread["MaNV"].ToString());
            }
            sqlCon.Close();
        }

        public void Insert()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdmgQuaTrinhThucHien_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDMG", MaHDMG);
            sqlCmd.Parameters.AddWithValue("@NgayTH", NgayTH);
            sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.MaTT);
            sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdmgQuaTrinhThucHien_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDMG", MaHDMG);
            sqlCmd.Parameters.AddWithValue("@Lan", Lan);
            sqlCmd.Parameters.AddWithValue("@NgayTH", NgayTH);
            sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.MaTT);
            sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("hdmgQuaTrinhThucHien_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectBy()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("hdmgQuaTrinhThucHien_getAllByMaHDMG " + MaHDMG, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdmgQuaTrinhThucHien_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDMG", MaHDMG);
            sqlCmd.Parameters.AddWithValue("@Lan", Lan);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }
    }
}