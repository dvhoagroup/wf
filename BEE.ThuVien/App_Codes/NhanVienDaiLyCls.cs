using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
    public class NhanVienDaiLyCls
    {
        public int MaNV;
        public string MaSo;
        public string HoTen;
        public DateTime NgaySinh;
        public string Email;
        public string SoCMND;
        public DateTime NgayCap;
        public string NoiCap;
        public string DiaChi;
        public string HoKhau;
        public PhongBanCls PhongBan = new PhongBanCls();
        public ChucVuCls ChucVu = new ChucVuCls();
        public NhomKinhDoanhCls NhomKD = new NhomKinhDoanhCls();
        public string MaTTNCN;
        public string SoTKNH;
        public NganHangCls NganHang = new NganHangCls();
        public string MatKhau;
        public bool Lock;
        public DaiLyCls DaiLy = new DaiLyCls();
        public QuyDanhCls QuyDanh = new QuyDanhCls();
        public string DienThoai;
        public PermissionsCls Per = new PermissionsCls();

        public NhanVienDaiLyCls()
        {
        }

        public NhanVienDaiLyCls(int _MaNV)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("NhanVienDaiLy_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNV", _MaNV);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                MaNV = int.Parse(dread["MaNV"].ToString());
                MaSo = dread["MaSo"] as string;
                HoTen = dread["HoTen"] as string;
                if (dread["NgaySinh"].ToString() != "")
                    NgaySinh = (DateTime)dread["NgaySinh"];
                Email = dread["Email"] as string;
                SoCMND = dread["SoCMND"] as string;
                if (dread["NgayCap"].ToString() != "")
                    NgayCap = (DateTime)dread["NgayCap"];
                NoiCap = dread["NoiCap"] as string;
                DiaChi = dread["DiaChi"] as string;
                HoKhau = dread["HoKhau"] as string;
                PhongBan.MaPB = byte.Parse(dread["MaPB"].ToString());
                ChucVu.MaCV = byte.Parse(dread["MaCV"].ToString());
                NhomKD.MaNKD = byte.Parse(dread["MaNKD"].ToString());
                MaTTNCN = dread["MaTTNCN"] as string;
                SoTKNH = dread["SoTKNH"] as string;
                NganHang.MaNH = byte.Parse(dread["MaNH"].ToString());
                MatKhau = dread["MatKhau"] as string;
                Lock = (bool)dread["Lock"];
                DaiLy.MaDL = int.Parse(dread["MaDL"].ToString());
                QuyDanh.MaQD = byte.Parse(dread["MaQD"].ToString());
                DienThoai = dread["DienThoai"] as string;
                Per.PerID = dread["PerID"].ToString() == "" ? 0 : int.Parse(dread["PerID"].ToString());
            }
            sqlCon.Close();
        }

        public void Insert()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("NhanVienDaiLy_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaSo", MaSo);
            sqlCmd.Parameters.AddWithValue("@HoTen", HoTen);
            if (NgaySinh.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgaySinh", NgaySinh);
            else
                sqlCmd.Parameters.AddWithValue("@NgaySinh", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@Email", Email);
            sqlCmd.Parameters.AddWithValue("@SoCMND", SoCMND);
            if (NgayCap.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayCap", NgayCap);
            else
                sqlCmd.Parameters.AddWithValue("@NgayCap", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@NoiCap", NoiCap);
            sqlCmd.Parameters.AddWithValue("@DiaChi", DiaChi);
            sqlCmd.Parameters.AddWithValue("@HoKhau", HoKhau);
            sqlCmd.Parameters.AddWithValue("@MaPB", PhongBan.MaPB);
            sqlCmd.Parameters.AddWithValue("@MaCV", ChucVu.MaCV);
            sqlCmd.Parameters.AddWithValue("@MaNKD", NhomKD.MaNKD);
            sqlCmd.Parameters.AddWithValue("@MaTTNCN", MaTTNCN);
            sqlCmd.Parameters.AddWithValue("@SoTKNH", SoTKNH);
            sqlCmd.Parameters.AddWithValue("@MaNH", NganHang.MaNH);
            sqlCmd.Parameters.AddWithValue("@MatKhau", MatKhau);
            sqlCmd.Parameters.AddWithValue("@Lock", Lock);
            sqlCmd.Parameters.AddWithValue("@MaDL", DaiLy.MaDL);
            sqlCmd.Parameters.AddWithValue("@MaQD", QuyDanh.MaQD);
            sqlCmd.Parameters.AddWithValue("@DienThoai", DienThoai);
            if (Per.PerID != 0)
                sqlCmd.Parameters.AddWithValue("@PerID", Per.PerID);
            else
                sqlCmd.Parameters.AddWithValue("@PerID", DBNull.Value);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("NhanVienDaiLy_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNV", MaNV);
            sqlCmd.Parameters.AddWithValue("@MaSo", MaSo);
            sqlCmd.Parameters.AddWithValue("@HoTen", HoTen);
            if (NgaySinh.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgaySinh", NgaySinh);
            else
                sqlCmd.Parameters.AddWithValue("@NgaySinh", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@Email", Email);
            sqlCmd.Parameters.AddWithValue("@SoCMND", SoCMND);
            if (NgayCap.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayCap", NgayCap);
            else
                sqlCmd.Parameters.AddWithValue("@NgayCap", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@NoiCap", NoiCap);
            sqlCmd.Parameters.AddWithValue("@DiaChi", DiaChi);
            sqlCmd.Parameters.AddWithValue("@HoKhau", HoKhau);
            sqlCmd.Parameters.AddWithValue("@MaPB", PhongBan.MaPB);
            sqlCmd.Parameters.AddWithValue("@MaCV", ChucVu.MaCV);
            sqlCmd.Parameters.AddWithValue("@MaNKD", NhomKD.MaNKD);
            sqlCmd.Parameters.AddWithValue("@MaTTNCN", MaTTNCN);
            sqlCmd.Parameters.AddWithValue("@SoTKNH", SoTKNH);
            sqlCmd.Parameters.AddWithValue("@MaNH", NganHang.MaNH);
            //sqlCmd.Parameters.AddWithValue("@MatKhau", MatKhau);
            sqlCmd.Parameters.AddWithValue("@Lock", Lock);
            sqlCmd.Parameters.AddWithValue("@MaDL", DaiLy.MaDL);
            sqlCmd.Parameters.AddWithValue("@MaQD", QuyDanh.MaQD);
            sqlCmd.Parameters.AddWithValue("@DienThoai", DienThoai);
            if (Per.PerID != 0)
                sqlCmd.Parameters.AddWithValue("@PerID", Per.PerID);
            else
                sqlCmd.Parameters.AddWithValue("@PerID", DBNull.Value);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("NhanVienDaiLy_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectByMaDL()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("NhanVienDaiLy_getAllByMaDL " + DaiLy.MaDL, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectShow()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("NhanVienDaiLy_getAllShow", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectNPPShow()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("NhanVienDaiLy_getAllNPPShow", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("NhanVienDaiLy_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNV", MaNV);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void ChangePassword()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("NhanVienDaiLy_ResetPass", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaSo", MaSo);
            sqlCmd.Parameters.AddWithValue("@MatKhau", MatKhau);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void LockStaff()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("NhanVienDaiLy_updateLock", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNV", MaNV);
            sqlCmd.Parameters.AddWithValue("@Lock", Lock);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public string TaoMaSo()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("NhanVienDaiLy_TaoMaSo", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@MaSo", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return sqlCmd.Parameters["@MaSo"].Value.ToString();
        }

        public DataTable SelectNotPer()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("NhanVienDaiLy_NotPer", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectByPer()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("NhanVienDaiLy_getByPer " + Per.PerID, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void UpdatePerID()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("NhanVienDaiLy_updatePerID", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNV", MaNV);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void UpdatePer()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("NhanVienDaiLy_updatePer", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaNV", MaNV);
            sqlCmd.Parameters.AddWithValue("@PerID", Per.PerID);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable SelectChoiceChange()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("aStaff_choice", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }
    }
}