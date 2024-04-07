using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
    public class hdMoiGioiCls
    {
        public int MaHDMG;
        public string SoPhieu;
        public DateTime NgayKy;
        public DateTime TuNgay;
        public DateTime DenNgay;
        public double HieuLuc;
        public string FileAttach;
        public DuAnCls DuAn = new DuAnCls();
        public DaiLyCls DaiLy = new DaiLyCls();
        public string Template;
        public int MaCT;
        public hdmgTinhTrangCls TinhTrang = new hdmgTinhTrangCls();
        public NhanVienCls NhanVien = new NhanVienCls();

        public hdMoiGioiCls()
        {
        }

        public hdMoiGioiCls(int _MaHDMG)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdMoiGioi_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDMG", _MaHDMG);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                MaHDMG = int.Parse(dread["MaHDMG"].ToString());
                SoPhieu = dread["SoPhieu"] as string;
                NgayKy = (DateTime)dread["NgayKy"];
                TuNgay = (DateTime)dread["TuNgay"];
                DenNgay = (DateTime)dread["DenNgay"];
                HieuLuc = double.Parse(dread["HieuLuc"].ToString());
                FileAttach = dread["FileAttach"] as string;
                DuAn.MaDA = int.Parse(dread["MaDA"].ToString());
                DaiLy.MaDL = int.Parse(dread["MaDL"].ToString());
                Template = dread["Template"] as string;
                MaCT = int.Parse(dread["MaCT"].ToString());
                TinhTrang.MaTT = byte.Parse(dread["MaTT"].ToString());
                NhanVien.MaNV = dread["MaNV"].ToString() == "" ? 0 : int.Parse(dread["MaNV"].ToString());
            }
            sqlCon.Close();
        }

        public void Insert()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdMoiGioi_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SoPhieu", SoPhieu);
            sqlCmd.Parameters.AddWithValue("@NgayKy", NgayKy);
            sqlCmd.Parameters.AddWithValue("@TuNgay", TuNgay);
            sqlCmd.Parameters.AddWithValue("@DenNgay", DenNgay);
            sqlCmd.Parameters.AddWithValue("@HieuLuc", HieuLuc);
            sqlCmd.Parameters.AddWithValue("@FileAttach", FileAttach);
            sqlCmd.Parameters.AddWithValue("@MaDA", DuAn.MaDA);
            sqlCmd.Parameters.AddWithValue("@MaDL", DaiLy.MaDL);
            sqlCmd.Parameters.AddWithValue("@Template", Template);
            sqlCmd.Parameters.AddWithValue("@MaCT", MaCT);
            if (NhanVien.MaNV != 0)
                sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNV", DBNull.Value);
            //sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.MaTT);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdMoiGioi_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDMG", MaHDMG);
            sqlCmd.Parameters.AddWithValue("@SoPhieu", SoPhieu);
            sqlCmd.Parameters.AddWithValue("@NgayKy", NgayKy);
            sqlCmd.Parameters.AddWithValue("@TuNgay", TuNgay);
            sqlCmd.Parameters.AddWithValue("@DenNgay", DenNgay);
            sqlCmd.Parameters.AddWithValue("@HieuLuc", HieuLuc);
            sqlCmd.Parameters.AddWithValue("@FileAttach", FileAttach);
            sqlCmd.Parameters.AddWithValue("@MaDA", DuAn.MaDA);
            sqlCmd.Parameters.AddWithValue("@MaDL", DaiLy.MaDL);
            //sqlCmd.Parameters.AddWithValue("@Template", Template);
            sqlCmd.Parameters.AddWithValue("@MaCT", MaCT);
            if (NhanVien.MaNV != 0)
                sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNV", DBNull.Value);
            //sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.MaTT);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("hdMoiGioi_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable Select(DateTime _TuNgay, DateTime _DenNgay)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdMoiGioi_getByDate", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TuNgay", _TuNgay);
            sqlCmd.Parameters.AddWithValue("@DenNgay", _DenNgay);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);

            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectByStaff(DateTime _TuNgay, DateTime _DenNgay, int _StaffID)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdMoiGioi_getByDateByStaff", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TuNgay", _TuNgay);
            sqlCmd.Parameters.AddWithValue("@DenNgay", _DenNgay);
            sqlCmd.Parameters.AddWithValue("@MaNV", _StaffID);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);

            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectByGroup(DateTime _TuNgay, DateTime _DenNgay, int _MaNV, byte _GroupID)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdMoiGioi_getByDateByGroup", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TuNgay", _TuNgay);
            sqlCmd.Parameters.AddWithValue("@DenNgay", _DenNgay);
            sqlCmd.Parameters.AddWithValue("@MaNV", _MaNV);
            sqlCmd.Parameters.AddWithValue("@GroupID", _GroupID);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);

            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectByDeparment(DateTime _TuNgay, DateTime _DenNgay, int _MaNV, byte _DepID)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdMoiGioi_getByDateByDeparment", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TuNgay", _TuNgay);
            sqlCmd.Parameters.AddWithValue("@DenNgay", _DenNgay);
            sqlCmd.Parameters.AddWithValue("@MaNV", _MaNV);
            sqlCmd.Parameters.AddWithValue("@DepID", _DepID);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);

            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdMoiGioi_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaHDMG", MaHDMG);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public string TaoSoPhieu()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("hdMoiGioi_TaoSoPhieu", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@SoPhieu", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return sqlCmd.Parameters["@SoPhieu"].Value.ToString();
        }
    }
}