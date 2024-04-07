using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
    public class pdcThanhLyCls
    {
        public int MaPDC;
        public string SoBienBan;
        public DateTime NgayTL;
        public KhachHangCls KhachHang = new KhachHangCls();
        public BatDongSanCls BDS = new BatDongSanCls();
        public double TienCoc;
        public double TienPhatCoc;
        public int ThoiHan;
        public string DienGiai;
        public string Template, FileAttach;
        public NhanVienCls NhanVien = new NhanVienCls();

        public pdcThanhLyCls()
        {
        }

        public pdcThanhLyCls(int _MaPDC)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdcThanhLy_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPDC", _MaPDC);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                MaPDC = int.Parse(dread["MaPDC"].ToString());
                SoBienBan = dread["SoBienBan"] as string;
                NgayTL = (DateTime)dread["NgayTL"];
                KhachHang.MaKH = int.Parse(dread["MaKH"].ToString());
                BDS.MaBDS = dread["MaBDS"] as string;
                TienCoc = double.Parse(dread["TienCoc"].ToString());
                TienPhatCoc = double.Parse(dread["TienPhatCoc"].ToString());
                ThoiHan = int.Parse(dread["ThoiHan"].ToString());
                DienGiai = dread["DienGiai"] as string;
                Template = dread["Template"] as string;
                FileAttach = dread["FileAttach"] as string;
                NhanVien.MaNV = int.Parse(dread["MaNV"].ToString());
            }
            sqlCon.Close();
        }

        public void Insert()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdcThanhLy_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPDC", MaPDC);
            sqlCmd.Parameters.AddWithValue("@SoBienBan", SoBienBan);
            sqlCmd.Parameters.AddWithValue("@NgayTL", NgayTL);
            sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
            sqlCmd.Parameters.AddWithValue("@MaBDS", BDS.MaBDS);
            sqlCmd.Parameters.AddWithValue("@TienCoc", TienCoc);
            sqlCmd.Parameters.AddWithValue("@TienPhatCoc", TienPhatCoc);
            sqlCmd.Parameters.AddWithValue("@ThoiHan", ThoiHan);
            sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
            sqlCmd.Parameters.AddWithValue("@Template", Template);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCmd.Parameters.AddWithValue("@FileAttach", FileAttach);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdcThanhLy_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPDC", MaPDC);
            sqlCmd.Parameters.AddWithValue("@SoBienBan", SoBienBan);
            sqlCmd.Parameters.AddWithValue("@NgayTL", NgayTL);
            sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
            sqlCmd.Parameters.AddWithValue("@MaBDS", BDS.MaBDS);
            sqlCmd.Parameters.AddWithValue("@TienCoc", TienCoc);
            sqlCmd.Parameters.AddWithValue("@TienPhatCoc", TienPhatCoc);
            sqlCmd.Parameters.AddWithValue("@ThoiHan", ThoiHan);
            sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCmd.Parameters.AddWithValue("@FileAttach", FileAttach);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pdcThanhLy_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable Select(int _MaPDC)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pdcThanhLy_getBy " + _MaPDC, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdcThanhLy_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPDC", MaPDC);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public string TaoSoPhieu()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdcThanhLy_TaoSoBienBan", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@SoBienBan", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return sqlCmd.Parameters["@SoBienBan"].Value.ToString();
        }
    }
}