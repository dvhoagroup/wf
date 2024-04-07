using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
    public class pdkgdQuaTrinhThucHienCls
    {
        public int MaGD;
        public byte Lan;
        public DateTime NgayTH;
        public pdkgdTinhTrangCls TinhTrang = new pdkgdTinhTrangCls();
        public string DienGiai;
        public NhanVienCls NhanVien = new NhanVienCls();

        public pdkgdQuaTrinhThucHienCls()
        {
        }

        public void Insert()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdkgdQuaTrinhThucHien_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaGD", MaGD);
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
            SqlDataAdapter sqlDA = new SqlDataAdapter("pdkgdQuaTrinhThucHien_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable Select(int _MaGD)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pdkgdQuaTrinhThucHien_getByMaGD " + _MaGD, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }
    }
}