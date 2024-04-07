using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
    public class pgdNhatKyXuLyCls
    {
        public pdkGiaoDichCls GiaoDich = new pdkGiaoDichCls();
        public int STT;
        public DateTime NgayXL;
        public string TieuDe;
        public string NoiDung;
        public string KetQua;
        public NhanVienCls NVPT = new NhanVienCls();
        public NhanVienCls NVXL = new NhanVienCls();
        public PhuongThucXuLyCls PhuongThuc = new PhuongThucXuLyCls();

        public pgdNhatKyXuLyCls()
        {
        }

        public pgdNhatKyXuLyCls(int _MaGD, int _STT)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgdNhatKyXuLy_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaGD", _MaGD);
            sqlCmd.Parameters.AddWithValue("@STT", _STT);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                GiaoDich.MaGD = int.Parse(dread["MaGD"].ToString());
                STT = int.Parse(dread["STT"].ToString());
                NgayXL = (DateTime)dread["NgayXL"];
                TieuDe = dread["TieuDe"] as string;
                NoiDung = dread["NoiDung"] as string;
                KetQua = dread["KetQua"] as string;
                NVPT.MaNV = int.Parse(dread["MaNVPT"].ToString());
                NVXL.MaNV = int.Parse(dread["MaNVXL"].ToString());
                PhuongThuc.MaPT = byte.Parse(dread["MaPT"].ToString());
            }
            sqlCon.Close();
        }

        public void Insert()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgdNhatKyXuLy_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaGD", GiaoDich.MaGD);            
            sqlCmd.Parameters.AddWithValue("@NgayXL", NgayXL);
            sqlCmd.Parameters.AddWithValue("@TieuDe", TieuDe);
            sqlCmd.Parameters.AddWithValue("@NoiDung", NoiDung);
            sqlCmd.Parameters.AddWithValue("@KetQua", KetQua);
            sqlCmd.Parameters.AddWithValue("@MaNVPT", NVPT.MaNV);
            sqlCmd.Parameters.AddWithValue("@MaNVXL", NVXL.MaNV);
            sqlCmd.Parameters.AddWithValue("@MaPT", PhuongThuc.MaPT);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgdNhatKyXuLy_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaGD", GiaoDich.MaGD);
            sqlCmd.Parameters.AddWithValue("@STT", STT);
            sqlCmd.Parameters.AddWithValue("@NgayXL", NgayXL);
            sqlCmd.Parameters.AddWithValue("@TieuDe", TieuDe);
            sqlCmd.Parameters.AddWithValue("@NoiDung", NoiDung);
            sqlCmd.Parameters.AddWithValue("@KetQua", KetQua);
            sqlCmd.Parameters.AddWithValue("@MaNVPT", NVPT.MaNV);
            sqlCmd.Parameters.AddWithValue("@MaNVXL", NVXL.MaNV);
            sqlCmd.Parameters.AddWithValue("@MaPT", PhuongThuc.MaPT);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pgdNhatKyXuLy_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectBy()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("pgdNhatKyXuLy_getMaGD " + GiaoDich.MaGD, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void Reply()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgdNhatKyXuLy_reply", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaGD", GiaoDich.MaGD);
            sqlCmd.Parameters.AddWithValue("@STT", STT);
            sqlCmd.Parameters.AddWithValue("@KetQua", KetQua);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("pgdNhatKyXuLy_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaGD", GiaoDich.MaGD);
            sqlCmd.Parameters.AddWithValue("@STT", STT);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }
    }
}