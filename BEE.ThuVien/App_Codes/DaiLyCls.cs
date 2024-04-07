using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
	public class DaiLyCls
	{
		public int MaDL;
		public string TenDL;
		public string DienThoai;
		public string Fax;
		public string DiaChi;
		public string MaSoThue;
		public string Email;
		public string Website;
		public string NguoiDaiDien;
		public string ChucVu;
		public string NguoiLienHe;
		public string DienThoaiNLH;
		public string GiayPhepKD, MaSo;
		public DateTime NgayCap;
		public bool HinhThuc;
        public NhanVienCls NhanVien = new NhanVienCls();
        public int MaNVDL;
        public int ParentID;

		public DaiLyCls()
		{
		}

		public DaiLyCls(int _MaDL)
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("DaiLy_get", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaDL", _MaDL);
			sqlCon.Open();
			SqlDataReader dread = sqlCmd.ExecuteReader();
			if (dread.Read())
			{
				MaDL = int.Parse(dread["MaDL"].ToString());
				TenDL = dread["TenDL"] as string;
				DienThoai = dread["DienThoai"] as string;
				Fax = dread["Fax"] as string;
				DiaChi = dread["DiaChi"] as string;
				MaSoThue = dread["MaSoThue"] as string;
				Email = dread["Email"] as string;
				Website = dread["Website"] as string;
				NguoiDaiDien = dread["NguoiDaiDien"] as string;
				ChucVu = dread["ChucVu"] as string;
				NguoiLienHe = dread["NguoiLienHe"] as string;
				DienThoaiNLH = dread["DienThoaiNLH"] as string;
				GiayPhepKD = dread["GiayPhepKD"] as string;
                if (dread["NgayCap"].ToString() != "")
                    NgayCap = (DateTime)dread["NgayCap"];
				HinhThuc = (bool)dread["HinhThuc"];
                MaSo = dread["MaSo"] as string;
                NhanVien.MaNV = dread["MaNVDK"].ToString() == "" ? 0 : int.Parse(dread["MaNVDK"].ToString());
                ParentID = dread["ParentID"].ToString() == "" ? 0 : int.Parse(dread["ParentID"].ToString());
                MaNVDL = dread["MaNVNPP"].ToString() == "" ? 0 : int.Parse(dread["MaNVNPP"].ToString());
			}
			sqlCon.Close();
		}

		public void Insert()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("DaiLy_add", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@TenDL", TenDL);
			sqlCmd.Parameters.AddWithValue("@DienThoai", DienThoai);
			sqlCmd.Parameters.AddWithValue("@Fax", Fax);
			sqlCmd.Parameters.AddWithValue("@DiaChi", DiaChi);
			sqlCmd.Parameters.AddWithValue("@MaSoThue", MaSoThue);
			sqlCmd.Parameters.AddWithValue("@Email", Email);
			sqlCmd.Parameters.AddWithValue("@Website", Website);
			sqlCmd.Parameters.AddWithValue("@NguoiDaiDien", NguoiDaiDien);
			sqlCmd.Parameters.AddWithValue("@ChucVu", ChucVu);
			sqlCmd.Parameters.AddWithValue("@NguoiLienHe", NguoiLienHe);
			sqlCmd.Parameters.AddWithValue("@DienThoaiNLH", DienThoaiNLH);
			sqlCmd.Parameters.AddWithValue("@GiayPhepKD", GiayPhepKD);
            if (NgayCap.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayCap", NgayCap);
            else
                sqlCmd.Parameters.AddWithValue("@NgayCap", DBNull.Value);
            if(NhanVien.MaNV !=0)
                sqlCmd.Parameters.AddWithValue("@MaNVDK", NhanVien.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVDK", DBNull.Value);
            if(ParentID !=0)
                sqlCmd.Parameters.AddWithValue("@ParentID", ParentID);
            else
                sqlCmd.Parameters.AddWithValue("@ParentID", DBNull.Value);
            if (MaNVDL != 0)
                sqlCmd.Parameters.AddWithValue("@MaNVNPP", MaNVDL);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVNPP", DBNull.Value);
			sqlCmd.Parameters.AddWithValue("@HinhThuc", HinhThuc);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

		public void Update()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("DaiLy_update", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaDL", MaDL);
			sqlCmd.Parameters.AddWithValue("@TenDL", TenDL);
			sqlCmd.Parameters.AddWithValue("@DienThoai", DienThoai);
			sqlCmd.Parameters.AddWithValue("@Fax", Fax);
			sqlCmd.Parameters.AddWithValue("@DiaChi", DiaChi);
			sqlCmd.Parameters.AddWithValue("@MaSoThue", MaSoThue);
			sqlCmd.Parameters.AddWithValue("@Email", Email);
			sqlCmd.Parameters.AddWithValue("@Website", Website);
			sqlCmd.Parameters.AddWithValue("@NguoiDaiDien", NguoiDaiDien);
			sqlCmd.Parameters.AddWithValue("@ChucVu", ChucVu);
			sqlCmd.Parameters.AddWithValue("@NguoiLienHe", NguoiLienHe);
			sqlCmd.Parameters.AddWithValue("@DienThoaiNLH", DienThoaiNLH);
			sqlCmd.Parameters.AddWithValue("@GiayPhepKD", GiayPhepKD);
            if (NgayCap.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayCap", NgayCap);
            else
                sqlCmd.Parameters.AddWithValue("@NgayCap", DBNull.Value);
            if (NhanVien.MaNV != 0)
                sqlCmd.Parameters.AddWithValue("@MaNVDK", NhanVien.MaNV);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVDK", DBNull.Value);
            if (ParentID != 0)
                sqlCmd.Parameters.AddWithValue("@ParentID", ParentID);
            else
                sqlCmd.Parameters.AddWithValue("@ParentID", DBNull.Value);
            if (MaNVDL != 0)
                sqlCmd.Parameters.AddWithValue("@MaNVNPP", MaNVDL);
            else
                sqlCmd.Parameters.AddWithValue("@MaNVNPP", DBNull.Value);
			sqlCmd.Parameters.AddWithValue("@HinhThuc", HinhThuc);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

		public DataTable Select()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlDataAdapter sqlDA = new SqlDataAdapter("DaiLy_getByNPP", sqlCon);
			DataSet dSet = new DataSet();
			sqlCon.Open();
			sqlDA.Fill(dSet);
			sqlCon.Close();
			return dSet.Tables[0];
		}

        public DataTable SelectByNPP()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("DaiLy_getByMaNPP " + MaDL, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectNPP()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("DaiLy_getAllNPP", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectShow()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("DaiLy_getAllShow", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectShow2()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("DaiLy_getAllShow2", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectShow3()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("DaiLy_getAllShow3", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectNPPShow()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("DaiLy_getAllNPPShow", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectNPPShow2()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("DaiLy_getAllNPPShow2", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

		public void Delete()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("DaiLy_delete", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaDL", MaDL);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}
	}
}