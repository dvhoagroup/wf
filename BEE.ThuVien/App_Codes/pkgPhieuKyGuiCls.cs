using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
    public class pkgPhieuKyGuiCls
    {
        public int MaPKG;
        public string SoPKG;
        public DateTime NgayKy;
        public KhachHangCls KhachHang = new KhachHangCls();
        public NhanVienCls NhanVien = new NhanVienCls();
        public string YeuCau;
        public string FileAttach, Template;
        public HopDongMuaBanCls HDMB = new HopDongMuaBanCls();
        public BatDongSanCls BDS = new BatDongSanCls();
        public pkgTinhTrangCls TinhTrang = new pkgTinhTrangCls();
        public NhanVienDaiLyCls NVDL = new NhanVienDaiLyCls();
        public DaiLyCls DaiLy = new DaiLyCls();

        public pkgPhieuKyGuiCls()
        {
        }

        public pkgPhieuKyGuiCls(int _MaPKG)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pkgPhieuKyGui_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPKG", _MaPKG);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                MaPKG = int.Parse(dread["MaPKG"].ToString());
                SoPKG = dread["SoPKG"] as string;
                NgayKy = (DateTime)dread["NgayKy"];
                KhachHang.MaKH = int.Parse(dread["MaKH"].ToString());
                NhanVien.MaNV = dread["MaNV"].ToString() == "" ? 0 : int.Parse(dread["MaNV"].ToString());
                YeuCau = dread["YeuCau"] as string;
                FileAttach = dread["FileAttach"] as string;
                HDMB.MaHDMB = int.Parse(dread["MaHDMB"].ToString());
                BDS.MaBDS = dread["MaBDS"] as string;
                TinhTrang.MaTT = dread["MaTT"].ToString() == "" ? (byte)0 : byte.Parse(dread["MaTT"].ToString());
                DaiLy.MaDL = dread["MaDL"].ToString() == "" ? 0 : int.Parse(dread["MaDL"].ToString());
                NVDL.MaNV = dread["MaNVDL"].ToString() == "" ? 0 : int.Parse(dread["MaNVDL"].ToString());
                Template = dread["Template"].ToString();
            }
            sqlCon.Close();
        }

        public void Insert()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pkgPhieuKyGui_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SoPKG", SoPKG);
            sqlCmd.Parameters.AddWithValue("@NgayKy", NgayKy);
            sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
            if (NhanVien.MaNV != 0)
                sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNV", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@YeuCau", YeuCau);
            sqlCmd.Parameters.AddWithValue("@FileAttach", FileAttach);
            sqlCmd.Parameters.AddWithValue("@MaHDMB", HDMB.MaHDMB);
            sqlCmd.Parameters.AddWithValue("@MaBDS", BDS.MaBDS);
            if (DaiLy.MaDL != 0)
                sqlCmd.Parameters.AddWithValue("@MaDL", DaiLy.MaDL);
            else
                sqlCmd.Parameters.AddWithValue("@MaDL", DBNull.Value);
            if (NVDL.MaNV != 0)
                sqlCmd.Parameters.AddWithValue("@MaNVDL", NVDL.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVDL", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@Template", Template);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pkgPhieuKyGui_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPKG", MaPKG);
            sqlCmd.Parameters.AddWithValue("@SoPKG", SoPKG);
            sqlCmd.Parameters.AddWithValue("@NgayKy", NgayKy);
            sqlCmd.Parameters.AddWithValue("@MaKH", KhachHang.MaKH);
            //if (NhanVien.MaNV != 0)
            //    sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            //else
            //    sqlCmd.Parameters.AddWithValue("@MaNV", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@YeuCau", YeuCau);
            sqlCmd.Parameters.AddWithValue("@FileAttach", FileAttach);
            sqlCmd.Parameters.AddWithValue("@MaHDMB", HDMB.MaHDMB);
            sqlCmd.Parameters.AddWithValue("@MaBDS", BDS.MaBDS);
            //if (DaiLy.MaDL != 0)
            //    sqlCmd.Parameters.AddWithValue("@MaDL", DaiLy.MaDL);
            //else
            //    sqlCmd.Parameters.AddWithValue("@MaNV", DBNull.Value);
            //if (NVDL.MaNV != 0)
            //    sqlCmd.Parameters.AddWithValue("@MaNVDL", NVDL.MaNV);
            //else
            //    sqlCmd.Parameters.AddWithValue("@MaNVDL", DBNull.Value);
            //sqlCmd.Parameters.AddWithValue("@MaTT", MaTT);
            sqlCmd.Parameters.AddWithValue("@Template", Template);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pkgPhieuKyGui_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable Select(DateTime _TuNgay, DateTime _DenNgay)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pkgPhieuKyGui_getByDate", sqlCon);
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
            SqlCommand sqlCmd = new SqlCommand("pkgPhieuKyGui_getByDateByStaff", sqlCon);
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
            SqlCommand sqlCmd = new SqlCommand("pkgPhieuKyGui_getByDateByGroup", sqlCon);
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
            SqlCommand sqlCmd = new SqlCommand("pkgPhieuKyGui_getByDateByDeparment", sqlCon);
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

        public string TaoSoPhieu()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pkgPhieuKyGui_TaoSoPKG", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@SoPKG", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return sqlCmd.Parameters["@SoPKG"].Value.ToString();
        }

        public DataTable ListInPKG()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pkgPhieuKyGui_list", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        } 

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pkgPhieuKyGui_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPKG", MaPKG);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public bool Top1NotConfirm()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pkgPhieuKyGui_Top1NotConfirm", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaBDS", BDS.MaBDS);
            sqlCmd.Parameters.Add("@Re", SqlDbType.Bit).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return bool.Parse(sqlCmd.Parameters["@Re"].Value.ToString());
        }
    }
}