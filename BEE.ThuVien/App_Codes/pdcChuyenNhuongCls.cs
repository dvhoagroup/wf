using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
    public class pdcChuyenNhuongCls
    {
        public int MaPCN;
        public string SoPCN;
        public DateTime NgayKy;
        public pdcPhieuDatCocCls PDC = new pdcPhieuDatCocCls();
        public KhachHangCls KHCN = new KhachHangCls();
        public KhachHangCls KHNCN = new KhachHangCls();
        public string SoPhieuThu;
        public DateTime NgayThu;
        public string GiayNopTien, Template, FileAttach;
        public hdTinhTrangCls TinhTrang = new hdTinhTrangCls();
        public NhanVienCls NVKD = new NhanVienCls();
        public NhanVienCls NVKT = new NhanVienCls();
        public DaiLyCls DaiLy = new DaiLyCls();
        public NhanVienDaiLyCls NVDL = new NhanVienDaiLyCls();

        public pdcChuyenNhuongCls()
        {
        }

        public pdcChuyenNhuongCls(int _MaPCN)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdcChuyenNhuong_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPCN", _MaPCN);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                MaPCN = int.Parse(dread["MaPCN"].ToString());
                SoPCN = dread["SoPCN"] as string;
                NgayKy = (DateTime)dread["NgayKy"];
                PDC.MaPDC = int.Parse(dread["MaPDC"].ToString());
                KHCN.MaKH = int.Parse(dread["MaKHA"].ToString());
                KHNCN.MaKH = int.Parse(dread["MaKHB"].ToString());
                SoPhieuThu = dread["SoPhieuThu"] as string;
                //Template = dread["Template"] as string;
                FileAttach = dread["FileAttach"] as string;
                if (dread["NgayThu"].ToString() != "")
                    NgayThu = (DateTime)dread["NgayThu"];
                GiayNopTien = dread["GiayNopTien"] as string;
                TinhTrang.MaTT = byte.Parse(dread["MaTT"].ToString());
            }
            sqlCon.Close();
        }

        public void Insert()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdcChuyenNhuong_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SoPCN", SoPCN);
            sqlCmd.Parameters.AddWithValue("@NgayKy", NgayKy);
            sqlCmd.Parameters.AddWithValue("@MaPDC", PDC.MaPDC);
            sqlCmd.Parameters.AddWithValue("@MaKHA", KHCN.MaKH);
            sqlCmd.Parameters.AddWithValue("@MaKHB", KHNCN.MaKH);
            sqlCmd.Parameters.AddWithValue("@SoPhieuThu", SoPhieuThu);
            if (NgayThu.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayThu", NgayThu);
            else
                sqlCmd.Parameters.AddWithValue("@NgayThu", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@GiayNopTien", GiayNopTien);
            sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.MaTT);
            //sqlCmd.Parameters.AddWithValue("@Template", Template);
            sqlCmd.Parameters.AddWithValue("@FileAttach", FileAttach);
            sqlCmd.Parameters.AddWithValue("@MaNVKT", NVKT.MaNV);
            sqlCmd.Parameters.AddWithValue("@MaNVKD", NVKD.MaNV);

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdcChuyenNhuong_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPCN", MaPCN);
            sqlCmd.Parameters.AddWithValue("@SoPCN", SoPCN);
            sqlCmd.Parameters.AddWithValue("@NgayKy", NgayKy);
            sqlCmd.Parameters.AddWithValue("@MaPDC", PDC.MaPDC);
            sqlCmd.Parameters.AddWithValue("@MaKHA", KHCN.MaKH);
            sqlCmd.Parameters.AddWithValue("@MaKHB", KHNCN.MaKH);
            sqlCmd.Parameters.AddWithValue("@SoPhieuThu", SoPhieuThu);
            if (NgayThu.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayThu", NgayThu);
            else
                sqlCmd.Parameters.AddWithValue("@NgayThu", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@GiayNopTien", GiayNopTien);
            sqlCmd.Parameters.AddWithValue("@FileAttach", FileAttach);
            
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Confirm()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdcChuyenNhuong_confirm", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPCN", MaPCN);
            sqlCmd.Parameters.AddWithValue("@MaTT", TinhTrang.MaTT);

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public bool Check()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdcChuyenNhuong_check", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaBDS", PDC.MaBDS);
            sqlCmd.Parameters.Add("@Re", SqlDbType.Bit).Direction = ParameterDirection.Output;

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return bool.Parse(sqlCmd.Parameters["@Re"].Value.ToString());
        }

        public void UpdateTemplate()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdcChuyenNhuong_updateTemplate", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPCN", MaPCN);
            sqlCmd.Parameters.AddWithValue("@Template", Template);

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pdcChuyenNhuong_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectBy()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pdcChuyenNhuong_getByMaPDC " + PDC.MaPDC, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable Select(DateTime _TuNgay, DateTime _DenNgay)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdcChuyenNhuong_getByDate", sqlCon);
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

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdcChuyenNhuong_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaPCN", MaPCN);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public string TaoSoPhieu()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pdcChuyenNhuong_TaoSoPCN", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@SoPCN", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return sqlCmd.Parameters["@SoPCN"].Value.ToString();
        }
    }
}