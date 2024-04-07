using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it.Agency
{
	public class NhanVienCls
	{
        public Int64 MaNV;
        public string MaSo;
        public string HoTen;
        public DateTime NgaySinh;
        public string Email;
        public string SoNoiBo;
        public string SoCMND;
        public string NgayCap;
        public string NoiCap;
        public string DiaChi;
        public string HoKhau;
        public PhongBanCls PhongBan = new PhongBanCls();
        public ChucVuCls ChucVu = new ChucVuCls();
        public NhomKinhDoanhCls NKD = new NhomKinhDoanhCls();
        public string MaTTNCN;
        public string SoTKNH;
        public NganHangCls NganHang = new NganHangCls();
        public string MatKhau;
        public bool Lock, IsCDT;
        public QuyDanhCls QuyDanh = new QuyDanhCls();
        public string DienThoai, DienThoai2,DienThoai3, KeyCode;
        public byte? MaTinhTrang;
        public DateTime? NgayVaoLam, NgayNghiViec;
        public int PerID;
        public int MaQL, MaQL2;
        public byte MaDM;
        public decimal Rose;
        public string DienThoaiNB, DiaChiLL, ImgAvatar, ImgSignature, Description;
        public byte? SoLanXoa;
        public bool ChangedPass;

        public NhanVienCls()
        {
        }

        public NhanVienCls(int _MaNV)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("NhanVien_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNV", _MaNV);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {

                MaNV = int.Parse(dread["MaNV"].ToString());
                MaTinhTrang = dread["MaTinhTrang"].ToString() == "" ? (byte)0 : byte.Parse(dread["MaTinhTrang"].ToString());
                MaSo = dread["MaSo"] as string;
                HoTen = dread["HoTen"] as string;
                if (dread["NgaySinh"].ToString() != "")
                    NgaySinh = (DateTime)dread["NgaySinh"];

                if (dread["NgayVaoLam"].ToString() != "")
                    NgayVaoLam = (DateTime)dread["NgayVaoLam"];

                if (dread["NgayNghiViec"].ToString() != "")
                    NgaySinh = (DateTime)dread["NgayNghiViec"];

                Email = dread["Email"] as string;
                SoNoiBo = dread["SoNoiBo"] as string;
                SoCMND = dread["SoCMND"] as string;
                NgayCap = dread["NgayCap"] as string;
                NoiCap = dread["NoiCap"] as string;
                DiaChi = dread["DiaChi"] as string;
                HoKhau = dread["HoKhau"] as string;
                DienThoaiNB = dread["DienThoaiNB"] as string;
                DiaChiLL = dread["DiaChiLL"] as string;
                SoLanXoa = dread["SoLanXoa"].ToString() == "" ? (byte)0 : byte.Parse(dread["SoLanXoa"].ToString());
                PhongBan.MaPB = dread["MaPB"].ToString() == "" ? (byte)0 : byte.Parse(dread["MaPB"].ToString());
                ChucVu.MaCV = dread["MaCV"].ToString() == "" ? (byte)0 : byte.Parse(dread["MaCV"].ToString());
                NKD.MaNKD = dread["MaNKD"].ToString() == "" ? (byte)0 : byte.Parse(dread["MaNKD"].ToString());
                MaDM = dread["MaDM"] != DBNull.Value ? (byte)dread["MaDM"] : (byte)0;
                MaTTNCN = dread["MaTTNCN"] as string;
                SoTKNH = dread["SoTKNH"] as string;
                NganHang.MaNH = dread["MaNH"].ToString() == "" ? (byte)0 : byte.Parse(dread["MaNH"].ToString());
                MatKhau = dread["MatKhau"] as string;
                Lock = (bool)dread["Lock"];
                IsCDT = (bool)dread["IsCDT"];
                QuyDanh.MaQD = dread["MaQD"].ToString() == "" ? (byte)0 : byte.Parse(dread["MaQD"].ToString());
                PerID = dread["PerID"].ToString() == "" ? 0 : int.Parse(dread["PerID"].ToString());
                DienThoai = dread["DienThoai"] as string;
                DienThoai2 = dread["DienThoai2"] as string;
                DienThoai3 = dread["DienThoai3"] as string;

                if (dread["MaQL"] != DBNull.Value)
                    MaQL = (int)dread["MaQL"];
                if (dread["MaQL2"] != DBNull.Value)
                    MaQL2 = (int)dread["MaQL2"];
                Rose = dread["Rose"].ToString() == "" ? 0 : decimal.Parse(dread["Rose"].ToString());
                ImgAvatar = dread["ImgAvatar"] as string;
                ImgSignature = dread["ImgSignature"] as string;
                Description = dread["Description"] as string;
            }
            sqlCon.Close();
        }

        public void Detail()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("NhanVien_detail", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNV", MaNV);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                MaSo = dread["MaSo"] as string;
                HoTen = dread["HoTen"] as string;
            }
            sqlCon.Close();
        }

        public void Insert()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("NhanVien_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaSo", MaSo);
            sqlCmd.Parameters.AddWithValue("@HoTen", HoTen);
            if (NgaySinh.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgaySinh", NgaySinh);
            else
                sqlCmd.Parameters.AddWithValue("@NgaySinh", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@Email", Email);
            sqlCmd.Parameters.AddWithValue("@SoNoiBo", SoNoiBo);
            sqlCmd.Parameters.AddWithValue("@SoCMND", SoCMND);
            sqlCmd.Parameters.AddWithValue("@NgayCap", NgayCap);
            sqlCmd.Parameters.AddWithValue("@NoiCap", NoiCap);
            sqlCmd.Parameters.AddWithValue("@DiaChi", DiaChi);
            sqlCmd.Parameters.AddWithValue("@HoKhau", HoKhau);
            sqlCmd.Parameters.AddWithValue("@MaPB", PhongBan.MaPB);
            sqlCmd.Parameters.AddWithValue("@MaCV", ChucVu.MaCV);
            sqlCmd.Parameters.AddWithValue("@MaNKD", NKD.MaNKD);
            sqlCmd.Parameters.AddWithValue("@MaTTNCN", MaTTNCN);
            sqlCmd.Parameters.AddWithValue("@SoTKNH", SoTKNH);
            sqlCmd.Parameters.AddWithValue("@MaNH", NganHang.MaNH);
            sqlCmd.Parameters.AddWithValue("@MatKhau", MatKhau);
            sqlCmd.Parameters.AddWithValue("@Lock", Lock);
            sqlCmd.Parameters.AddWithValue("@MaQD", QuyDanh.MaQD);
            sqlCmd.Parameters.AddWithValue("@DienThoai", DienThoai);
            sqlCmd.Parameters.AddWithValue("@DienThoaiNB", DienThoaiNB);
            sqlCmd.Parameters.AddWithValue("@DiaChiLL", DiaChiLL);

            if (MaQL > 0)
                sqlCmd.Parameters.AddWithValue("MaQL", MaQL);
            else
                sqlCmd.Parameters.AddWithValue("MaQL", DBNull.Value);
            if (MaQL2 > 0)
                sqlCmd.Parameters.AddWithValue("MaQL2", MaQL2);
            else
                sqlCmd.Parameters.AddWithValue("MaQL2", DBNull.Value);
            if (MaDM > 0)
                sqlCmd.Parameters.AddWithValue("MaDM", MaDM);
            else
                sqlCmd.Parameters.AddWithValue("MaDM", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@IsCDT", IsCDT);
            if (Rose > 0)
                sqlCmd.Parameters.AddWithValue("Rose", Rose);
            else
                sqlCmd.Parameters.AddWithValue("Rose", DBNull.Value);
            //sqlCmd.Parameters.AddWithValue("@ImgAvatar", ImgAvatar);
            //sqlCmd.Parameters.AddWithValue("@ImgSignature", ImgSignature);
            sqlCmd.Parameters.AddWithValue("@Description", Description);
            sqlCmd.Parameters.AddWithValue("@NgayVaoLam", NgayVaoLam);
          //  sqlCmd.Parameters.AddWithValue("@NgayNghiViec", NgayNghiViec);
            sqlCmd.Parameters.AddWithValue("@MaTinhTrang", MaTinhTrang);
            sqlCmd.Parameters.AddWithValue("@DienThoai2", DienThoai2);
            sqlCmd.Parameters.AddWithValue("@DienThoai3", DienThoai3);


            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Update(int staffIDModify)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("NhanVien_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNV", MaNV);
            sqlCmd.Parameters.AddWithValue("@MaSo", MaSo);
            sqlCmd.Parameters.AddWithValue("@HoTen", HoTen);
            if (NgaySinh.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgaySinh", NgaySinh);
            else
                sqlCmd.Parameters.AddWithValue("@NgaySinh", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@Email", Email);
            sqlCmd.Parameters.AddWithValue("@SoNoiBo", SoNoiBo);
            sqlCmd.Parameters.AddWithValue("@SoCMND", SoCMND);
            sqlCmd.Parameters.AddWithValue("@NgayCap", NgayCap);
            sqlCmd.Parameters.AddWithValue("@NoiCap", NoiCap);
            sqlCmd.Parameters.AddWithValue("@DiaChi", DiaChi);
            sqlCmd.Parameters.AddWithValue("@HoKhau", HoKhau);
            sqlCmd.Parameters.AddWithValue("@MaPB", PhongBan.MaPB);
            sqlCmd.Parameters.AddWithValue("@MaCV", ChucVu.MaCV);
            sqlCmd.Parameters.AddWithValue("@MaNKD", NKD.MaNKD);
            sqlCmd.Parameters.AddWithValue("@MaTTNCN", MaTTNCN);
            sqlCmd.Parameters.AddWithValue("@SoTKNH", SoTKNH);
            sqlCmd.Parameters.AddWithValue("@MaNH", NganHang.MaNH);
            sqlCmd.Parameters.AddWithValue("@Lock", Lock);
            sqlCmd.Parameters.AddWithValue("@MaQD", QuyDanh.MaQD);
            sqlCmd.Parameters.AddWithValue("@DienThoai", DienThoai);
            sqlCmd.Parameters.AddWithValue("@DienThoaiNB", DienThoaiNB);
            sqlCmd.Parameters.AddWithValue("@DiaChiLL", DiaChiLL);
            if (MaQL > 0)
                sqlCmd.Parameters.AddWithValue("MaQL", MaQL);
            else
                sqlCmd.Parameters.AddWithValue("MaQL", DBNull.Value);
            if (MaQL2 > 0)
                sqlCmd.Parameters.AddWithValue("MaQL2", MaQL2);
            else
                sqlCmd.Parameters.AddWithValue("MaQL2", DBNull.Value);
            if (MaDM > 0)
                sqlCmd.Parameters.AddWithValue("MaDM", MaDM);
            else
                sqlCmd.Parameters.AddWithValue("MaDM", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@IsCDT", IsCDT);
            if (Rose > 0)
                sqlCmd.Parameters.AddWithValue("Rose", Rose);
            else
                sqlCmd.Parameters.AddWithValue("Rose", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@MaNVCN", staffIDModify);
            //sqlCmd.Parameters.AddWithValue("@ImgAvatar", ImgAvatar);
            //sqlCmd.Parameters.AddWithValue("@ImgSignature", ImgSignature);
            sqlCmd.Parameters.AddWithValue("@Description", Description);
            sqlCmd.Parameters.AddWithValue("@NgayVaoLam", NgayVaoLam);
          //  sqlCmd.Parameters.AddWithValue("@NgayNghiViec", NgayNghiViec);
            sqlCmd.Parameters.AddWithValue("@MaTinhTrang", MaTinhTrang);
            sqlCmd.Parameters.AddWithValue("@DienThoai2", DienThoai2);
            sqlCmd.Parameters.AddWithValue("@DienThoai3", DienThoai3);

            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void UpdatePer()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("NhanVien_updatePer", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNV", MaNV);
            sqlCmd.Parameters.AddWithValue("@PerID", PerID);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

		public DataTable Select()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlDataAdapter sqlDA = new SqlDataAdapter("NhanVien_getAll_Agency", sqlCon);
			DataSet dSet = new DataSet();
			sqlCon.Open();
			sqlDA.Fill(dSet);
			sqlCon.Close();
			return dSet.Tables[0];
		}

        public DataTable SelectByPer()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("NhanVien_getByPer " + PerID, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectNotPer()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("NhanVien_NotPer", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectShow()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("NhanVien_getAllShow", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectShowAll()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("NhanVien_getAllShow2", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

		public void Delete()
		{
			SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
			SqlCommand sqlCmd = new SqlCommand("Agency_delete", sqlCon);
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Parameters.AddWithValue("@MaNV", MaNV);
			sqlCon.Open();
			sqlCmd.ExecuteNonQuery();
			sqlCon.Close();
		}

        public void UpdatePerID()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("NhanVien_updatePerID", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNV", MaNV);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void UpdateKeyCode()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("NhanVien_updateKeyCode", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNV", MaNV);
            sqlCmd.Parameters.AddWithValue("@KeyCode", KeyCode);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void ChangePassword(int staffID)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("NhanVien_ResetPass", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaSo", MaSo);
            sqlCmd.Parameters.AddWithValue("@MatKhau", MatKhau);
            sqlCmd.Parameters.AddWithValue("@MaNV", staffID);
            sqlCmd.Parameters.AddWithValue("@MaNVCN", MaNV);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void LockStaff(int staffIDModify)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("Agency_updateLock", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNV", MaNV);
            sqlCmd.Parameters.AddWithValue("@Lock", Lock);
            sqlCmd.Parameters.AddWithValue("@MaNVCN", staffIDModify);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void ResetPassword(int staffIDModify)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("NhanVien_resetPassword", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNV", MaNV);
            sqlCmd.Parameters.AddWithValue("@MatKhau", MatKhau);
            sqlCmd.Parameters.AddWithValue("@MaNVCN", staffIDModify);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public string TaoMaSo()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("NhanVien_TaoMaSo", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@MaSo", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return sqlCmd.Parameters["@MaSo"].Value.ToString();
        }

        public int Check()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("NhanVien_check", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@HoTen", HoTen);
            sqlCmd.Parameters.AddWithValue("@SoCMND", SoCMND);
            sqlCmd.Parameters.Add("@Re", SqlDbType.Int).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return int.Parse(sqlCmd.Parameters["@Re"].Value.ToString());
        }

        public string GetName()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("NhanVien_getName", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNV", MaNV);
            sqlCmd.Parameters.Add("@Re", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return sqlCmd.Parameters["@Re"].Value.ToString();
        }

        public DataTable SelectObjectAll()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("schNhanVien_select", sqlCon);
            DataSet dset = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dset);
            sqlCon.Close();
            return dset.Tables[0];
        }

        public DataTable SelectObjectByGroup()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("schNhanVien_getAllByGroup " + NKD.MaNKD, sqlCon);
            DataSet dset = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dset);
            sqlCon.Close();
            return dset.Tables[0];
        }

        public DataTable SelectObjectByDepartment()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("schNhanVien_getAllByDepartment " + PhongBan.MaPB, sqlCon);
            DataSet dset = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dset);
            sqlCon.Close();
            return dset.Tables[0];
        }

        public DataTable SelectObjectBy()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("schNhanVien_getAllByMaNV " + MaNV, sqlCon);
            DataSet dset = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dset);
            sqlCon.Close();
            return dset.Tables[0];
        }

        public DataTable SelectChoiceChange()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("NhanVien_choice", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }
	}
}
