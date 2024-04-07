using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
    public class hdmbThanhLyCls
    {
        public int MaHDMB;
        public KhachHangCls KhachHang = new KhachHangCls();
        public string MaBDS, NguoiDeNghi;
        public int MaPGC;
        public DateTime NgayTL, NgayDeNghi;
        public string FileAttach;
        public string NoiDung, Template, SoBienBan;
        public int MaNV, ThoiGianTra;
        public double SoTien, ChiPhi;

        public hdmbThanhLyCls()
        {
        }

        public hdmbThanhLyCls(int _MaHDMB)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdmbThanhLy_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDMB", _MaHDMB);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                MaHDMB = int.Parse(dread["MaHDMB"].ToString());
                KhachHang.MaKH = int.Parse(dread["MaKH"].ToString());
                MaBDS = dread["MaBDS"] as string;
                if (dread["MaPGC"].ToString() != "")
                    MaPGC = int.Parse(dread["MaPGC"].ToString());
                NgayTL = (DateTime)dread["NgayTL"];
                NgayDeNghi = (DateTime)dread["NgayDeNghi"];
                NguoiDeNghi = dread["NguoiDeNghi"] as string;
                FileAttach = dread["FileAttach"] as string;
                NoiDung = dread["NoiDung"] as string;
                MaNV = int.Parse(dread["MaNV"].ToString());
                SoBienBan = dread["SoBienBan"].ToString();
                SoTien = double.Parse(dread["SoTien"].ToString());
                ChiPhi = double.Parse(dread["ChiPhi"].ToString());
                ThoiGianTra = int.Parse(dread["ThoiGianTra"].ToString());
                Template = dread["Template"].ToString();
            }
            sqlCon.Close();
        }

        public void Insert()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdmbThanhLy_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDMB", MaHDMB);
            sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
            sqlCmd.Parameters.AddWithValue("@MaBDS", MaBDS);
            if (MaPGC != 0)
                sqlCmd.Parameters.AddWithValue("@MaPGC", MaPGC);
            else
                sqlCmd.Parameters.AddWithValue("@MaPGC", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@NgayTL", NgayTL);
            sqlCmd.Parameters.AddWithValue("@FileAttach", FileAttach);
            sqlCmd.Parameters.AddWithValue("@NoiDung", NoiDung);
            sqlCmd.Parameters.AddWithValue("@MaNV", MaNV);
            sqlCmd.Parameters.AddWithValue("@SoTien", SoTien);
            sqlCmd.Parameters.AddWithValue("@ChiPhi", ChiPhi);
            sqlCmd.Parameters.AddWithValue("@SoBienBan", SoBienBan);
            sqlCmd.Parameters.AddWithValue("@Template", Template);
            sqlCmd.Parameters.AddWithValue("@ThoiGianTra", ThoiGianTra);
            sqlCmd.Parameters.AddWithValue("@NguoiDeNghi", NguoiDeNghi);
            sqlCmd.Parameters.AddWithValue("@NgayDeNghi", NgayDeNghi);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdmbThanhLy_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDMB", MaHDMB);
            sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
            sqlCmd.Parameters.AddWithValue("@MaBDS", MaBDS);
            if (MaPGC != 0)
                sqlCmd.Parameters.AddWithValue("@MaPGC", MaPGC);
            else
                sqlCmd.Parameters.AddWithValue("@MaPGC", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@NgayTL", NgayTL);
            sqlCmd.Parameters.AddWithValue("@FileAttach", FileAttach);
            sqlCmd.Parameters.AddWithValue("@NoiDung", NoiDung);
            sqlCmd.Parameters.AddWithValue("@MaNV", MaNV);
            sqlCmd.Parameters.AddWithValue("@SoTien", SoTien);
            sqlCmd.Parameters.AddWithValue("@ChiPhi", ChiPhi);
            sqlCmd.Parameters.AddWithValue("@SoBienBan", SoBienBan);
            sqlCmd.Parameters.AddWithValue("@ThoiGianTra", ThoiGianTra);
            sqlCmd.Parameters.AddWithValue("@NguoiDeNghi", NguoiDeNghi);
            sqlCmd.Parameters.AddWithValue("@NgayDeNghi", NgayDeNghi);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("hdmbThanhLy_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable Select(int _MaHDMB)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("hdmbThanhLy_getBy " + _MaHDMB, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdmbThanhLy_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDMB", MaHDMB);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public string TaoSoPhieu()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdmbThanhLy_TaoSoBienBan", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@SoBienBan", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return sqlCmd.Parameters["@SoBienBan"].Value.ToString();
        }
    }
}