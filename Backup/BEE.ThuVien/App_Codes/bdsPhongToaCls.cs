using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
    public class bdsPhongToaCls
    {
        public BatDongSanCls BDS = new BatDongSanCls();
        public HopDongMuaBanCls HDMB = new HopDongMuaBanCls();
        public byte STT;
        public DateTime NgayPT;
        public string FileAttach;
        public string Template;
        public NhanVienCls NhanVien = new NhanVienCls();
        public bool Status;
        public KhachHangCls KhachHang = new KhachHangCls();
        public string DienGiai;
        public string SoHDTD;
        public DateTime NgayKyHDTD;
        public string SoHDTCTS;
        public DateTime NgayKyHDTCTS;
        public double SoTienVay;
        public int ThoiHanVay;
        public double LaiSuat;
        public NganHangCls NganHang = new NganHangCls();

        public bdsPhongToaCls()
        {
        }

        public bdsPhongToaCls(string _MaBDS, int _MaHDMB, byte _STT)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("bdsPhongToa_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaBDS", _MaBDS);
            sqlCmd.Parameters.AddWithValue("@MaHDMB", _MaHDMB);
            sqlCmd.Parameters.AddWithValue("@STT", _STT);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                BDS.MaBDS = dread["MaBDS"] as string;
                HDMB.MaHDMB = int.Parse(dread["MaHDMB"].ToString());
                STT = byte.Parse(dread["STT"].ToString());
                NgayPT = (DateTime)dread["NgayPT"];
                FileAttach = dread["FileAttach"] as string;
                Template = dread["Template"] as string;
                NhanVien.MaNV = int.Parse(dread["MaNV"].ToString());
                Status = (bool)dread["Status"];
                KhachHang.MaKH = int.Parse(dread["MaKH"].ToString());
                DienGiai = dread["DienGiai"] as string;
                SoHDTD = dread["SoHDTD"] as string;
                NgayKyHDTD = (DateTime)dread["NgayKyHDTD"];
                SoHDTCTS = dread["SoHDTCTS"] as string;
                NgayKyHDTCTS = (DateTime)dread["NgayKyHDTCTS"];
                SoTienVay = double.Parse(dread["SoTienVay"].ToString());
                ThoiHanVay = int.Parse(dread["ThoiHanVay"].ToString());
                LaiSuat = double.Parse(dread["LaiSuat"].ToString());
                NganHang.MaNH = byte.Parse(dread["MaNH"].ToString());
            }
            sqlCon.Close();
        }

        public void Insert()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("bdsPhongToa_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaBDS", BDS.MaBDS);
            sqlCmd.Parameters.AddWithValue("@MaHDMB", HDMB.MaHDMB);
            sqlCmd.Parameters.AddWithValue("@NgayPT", NgayPT);
            sqlCmd.Parameters.AddWithValue("@FileAttach", FileAttach);
            sqlCmd.Parameters.AddWithValue("@Template", Template);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCmd.Parameters.AddWithValue("@Status", Status);
            sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
            sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
            sqlCmd.Parameters.AddWithValue("@SoHDTD", SoHDTD);
            sqlCmd.Parameters.AddWithValue("@NgayKyHDTD", NgayKyHDTD);
            sqlCmd.Parameters.AddWithValue("@SoHDTCTS", SoHDTCTS);
            sqlCmd.Parameters.AddWithValue("@NgayKyHDTCTS", NgayKyHDTCTS);
            sqlCmd.Parameters.AddWithValue("@SoTienVay", SoTienVay);
            sqlCmd.Parameters.AddWithValue("@ThoiHanVay", ThoiHanVay);
            sqlCmd.Parameters.AddWithValue("@LaiSuat", LaiSuat);
            sqlCmd.Parameters.AddWithValue("@MaNH", NganHang.MaNH);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("bdsPhongToa_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaBDS", BDS.MaBDS);
            sqlCmd.Parameters.AddWithValue("@MaHDMB", HDMB.MaHDMB);
            sqlCmd.Parameters.AddWithValue("@STT", STT);
            sqlCmd.Parameters.AddWithValue("@NgayPT", NgayPT);
            sqlCmd.Parameters.AddWithValue("@FileAttach", FileAttach);
            if (Template != "")
                sqlCmd.Parameters.AddWithValue("@Template", Template);
            else
                sqlCmd.Parameters.AddWithValue("@Template", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCmd.Parameters.AddWithValue("@Status", Status);
            sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
            sqlCmd.Parameters.AddWithValue("@DienGiai", DienGiai);
            sqlCmd.Parameters.AddWithValue("@SoHDTD", SoHDTD);
            sqlCmd.Parameters.AddWithValue("@NgayKyHDTD", NgayKyHDTD);
            sqlCmd.Parameters.AddWithValue("@SoHDTCTS", SoHDTCTS);
            sqlCmd.Parameters.AddWithValue("@NgayKyHDTCTS", NgayKyHDTCTS);
            sqlCmd.Parameters.AddWithValue("@SoTienVay", SoTienVay);
            sqlCmd.Parameters.AddWithValue("@ThoiHanVay", ThoiHanVay);
            sqlCmd.Parameters.AddWithValue("@LaiSuat", LaiSuat);
            sqlCmd.Parameters.AddWithValue("@MaNH", NganHang.MaNH);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("bdsPhongToa_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("bdsPhongToa_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaBDS", BDS.MaBDS);
            sqlCmd.Parameters.AddWithValue("@MaHDMB", HDMB.MaHDMB);
            sqlCmd.Parameters.AddWithValue("@STT", STT);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }
    }
}