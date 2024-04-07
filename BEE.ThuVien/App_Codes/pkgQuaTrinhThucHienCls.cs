using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
    public class pkgQuaTrinhThucHienCls
    {
        public int MaPKG;
        public byte Lan;
        public DateTime NgayTH;
        public pkgTinhTrangCls TinhTrang = new pkgTinhTrangCls();
        public string DienGiai;
        public NhanVienCls NhanVien = new NhanVienCls();

        public pkgQuaTrinhThucHienCls()
        {
        }

        public pkgQuaTrinhThucHienCls(int _MaPKG, byte _Lan)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pkgQuaTrinhThucHien_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPKG", _MaPKG);
            sqlCmd.Parameters.AddWithValue("@Lan", _Lan);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                MaPKG = int.Parse(dread["MaPKG"].ToString());
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
            SqlCommand sqlCmd = new SqlCommand("pkgQuaTrinhThucHien_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPKG", MaPKG);
            sqlCmd.Parameters.AddWithValue("@NgayTH", DateTime.Now);
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
            SqlDataAdapter sqlDA = new SqlDataAdapter("pkgQuaTrinhThucHien_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable Select(int _MaPKG)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pkgQuaTrinhThucHien_getByMaPKG " + _MaPKG, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pkgQuaTrinhThucHien_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPKG", MaPKG);
            sqlCmd.Parameters.AddWithValue("@Lan", Lan);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }
    }
}