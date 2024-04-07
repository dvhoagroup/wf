using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
	public class dlYeuCauHoTroCls
	{
		public DaiLyCls DaiLy = new DaiLyCls();
		public int STT;
		public string TieuDe;
		public string NoiDung;
		public dlMucDoYeuCauCls MDYC = new dlMucDoYeuCauCls();
		public NhanVienDaiLyCls NVDL = new NhanVienDaiLyCls();
		public NhanVienCls NVDK = new NhanVienCls();
        public string MaBDS = "";
        public string Reply;

		public dlYeuCauHoTroCls()
		{
		}

		public dlYeuCauHoTroCls(int _MaDL, int _STT)
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("dlYeuCauHoTro_get", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaDL", _MaDL);
			sqlCmd.Parameters.AddWithValue("@STT", _STT);
			sqlCon.Open();
			SqlDataReader dread = sqlCmd.ExecuteReader();
			if (dread.Read())
			{
                DaiLy.MaDL = int.Parse(dread["MaDL"].ToString());
				STT = int.Parse(dread["STT"].ToString());
				TieuDe = dread["TieuDe"] as string;
				NoiDung = dread["NoiDung"] as string;
                MDYC.MaMDYC = byte.Parse(dread["MaMDYC"].ToString());
				NVDL.MaNV = int.Parse(dread["MaNVDL"].ToString());
                NVDK.MaNV = dread["MaNVDK"].ToString() == "" ? 0 : int.Parse(dread["MaNVDK"].ToString());
                MaBDS = dread["MaBDS"] as string;
                Reply = dread["Reply"] as string;
			}
			sqlCon.Close();
		}

		public void Insert()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("dlYeuCauHoTro_add", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaDL", DaiLy.MaDL);
			sqlCmd.Parameters.AddWithValue("@TieuDe", TieuDe);
			sqlCmd.Parameters.AddWithValue("@NoiDung", NoiDung);
            sqlCmd.Parameters.AddWithValue("@MaMDYC", MDYC.MaMDYC);
            sqlCmd.Parameters.AddWithValue("@MaNVDL", NVDL.MaNV);
            sqlCmd.Parameters.AddWithValue("@MaBDS", MaBDS);
            sqlCmd.Parameters.AddWithValue("@MaNVDK", NVDK.MaNV);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

		public void Update()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("dlYeuCauHoTro_update", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaDL", DaiLy.MaDL);
			sqlCmd.Parameters.AddWithValue("@STT", STT);
			sqlCmd.Parameters.AddWithValue("@TieuDe", TieuDe);
			sqlCmd.Parameters.AddWithValue("@NoiDung", NoiDung);
            sqlCmd.Parameters.AddWithValue("@MaMDYC", MDYC.MaMDYC);
            sqlCmd.Parameters.AddWithValue("@MaNVDL", NVDL.MaNV);
            sqlCmd.Parameters.AddWithValue("@MaBDS", MaBDS);
            sqlCmd.Parameters.AddWithValue("@MaNV", NVDK.MaNV);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

        public void TiepNhan()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("dlYeuCauHoTro_TiepNhan", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaDL", DaiLy.MaDL);
            sqlCmd.Parameters.AddWithValue("@STT", STT);
            sqlCmd.Parameters.AddWithValue("@Reply", Reply);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

		public DataTable Select()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlDataAdapter sqlDA = new SqlDataAdapter("dlYeuCauHoTro_getAll", sqlCon);
			DataSet dSet = new DataSet();
			sqlCon.Open();
			sqlDA.Fill(dSet);
			sqlCon.Close();
			return dSet.Tables[0];
		}

        public DataTable SelectByMaNVNew()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("dlYeuCauHoTro_getAllByMaNVNew " + NVDK.MaNV, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable Select(DateTime _TuNgay, DateTime _DenNgay)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("dlYeuCauHoTro_getAllByDate", sqlCon);
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
			SqlCommand sqlCmd = new SqlCommand("dlYeuCauHoTro_delete", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaDL", DaiLy.MaDL);
			sqlCmd.Parameters.AddWithValue("@STT", STT);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}
	}
}
