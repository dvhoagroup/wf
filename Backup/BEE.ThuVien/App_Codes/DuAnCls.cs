using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace it
{
    public class DuAnCls
    {
        public int MaDA;
        public string TenDA;
        public string TenThuongMai;
        public string DiaChi;
        public XaCls Xa = new XaCls();
        public NhanVienCls NhanVien = new NhanVienCls();
        public LoaiDACls LoaiDA = new LoaiDACls();
        public string AttachFile;
        public string TomTat, TenVietTat, Template, SoGiayPhep, NoiCap;
        public DateTime NgayCap;
        public double DienTich;
        public string CodeSUN;
        public bool IsProject;
        public int AmountColumn;
        public int WidthCell;

        public DuAnCls()
        {
        }

        public DuAnCls(int _MaDA)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("DuAn_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaDA", _MaDA);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                MaDA = int.Parse(dread["MaDA"].ToString());
                TenDA = dread["TenDA"] as string;
                TenThuongMai = dread["TenThuongMai"] as string;
                DiaChi = dread["DiaChi"] as string;
                Xa.MaXa = int.Parse(dread["MaXa"].ToString());
                NhanVien.MaNV = int.Parse(dread["MaNV"].ToString());
                LoaiDA.MaLoaiDA = byte.Parse(dread["MaLoaiDA"].ToString());
                AttachFile = dread["AttachFile"] as string;
                TomTat = dread["TomTat"] as string;
                TenVietTat = dread["TenVietTat"] as string;
                SoGiayPhep = dread["SoGiayPhep"] as string;
                NoiCap = dread["NoiCap"] as string;
                if(dread["NgayCap"].ToString() != "")
                    NgayCap = DateTime.Parse(dread["NgayCap"].ToString());
                DienTich = dread["DienTich"].ToString() != "" ? double.Parse(dread["DienTich"].ToString()) : 0;
                IsProject = dread["IsProject"] as bool? ?? false;
                AmountColumn = dread["AmountColumn"].ToString() == "" ? 0 : int.Parse(dread["AmountColumn"].ToString());
                WidthCell = dread["WidthCell"].ToString() == "" ? 0 : int.Parse(dread["WidthCell"].ToString());
            }
            sqlCon.Close();
        }

        public void Insert()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("DuAn_add", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TenDA", TenDA);
            sqlCmd.Parameters.AddWithValue("@TenThuongMai", TenThuongMai);
            sqlCmd.Parameters.AddWithValue("@DiaChi", DiaChi);
            sqlCmd.Parameters.AddWithValue("@MaXa", Xa.MaXa);
            sqlCmd.Parameters.AddWithValue("@MaLoaiDA", LoaiDA.MaLoaiDA);
            sqlCmd.Parameters.AddWithValue("@AttachFile", AttachFile);
            sqlCmd.Parameters.AddWithValue("@TomTat", TomTat);
            sqlCmd.Parameters.AddWithValue("@TenVietTat", TenVietTat);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCmd.Parameters.AddWithValue("@SoGiayPhep", SoGiayPhep);
            sqlCmd.Parameters.AddWithValue("@NoiCap", NoiCap);
            sqlCmd.Parameters.AddWithValue("@CodeSUN", CodeSUN);
            sqlCmd.Parameters.AddWithValue("@IsProject", IsProject);
            sqlCmd.Parameters.AddWithValue("@AmountColumn", AmountColumn);
            sqlCmd.Parameters.AddWithValue("@WidthCell", WidthCell);
            if(NgayCap.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayCap", NgayCap);
            else
                sqlCmd.Parameters.AddWithValue("@NgayCap", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@DienTich", DienTich);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void Update()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("DuAn_update", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaDA", MaDA);
            sqlCmd.Parameters.AddWithValue("@TenDA", TenDA);
            sqlCmd.Parameters.AddWithValue("@TenThuongMai", TenThuongMai);
            sqlCmd.Parameters.AddWithValue("@DiaChi", DiaChi);
            sqlCmd.Parameters.AddWithValue("@MaXa", Xa.MaXa);
            sqlCmd.Parameters.AddWithValue("@MaLoaiDA", LoaiDA.MaLoaiDA);
            sqlCmd.Parameters.AddWithValue("@AttachFile", AttachFile);
            sqlCmd.Parameters.AddWithValue("@TomTat", TomTat);
            sqlCmd.Parameters.AddWithValue("@MaNV", NhanVien.MaNV);
            sqlCmd.Parameters.AddWithValue("@TenVietTat", TenVietTat);
            sqlCmd.Parameters.AddWithValue("@SoGiayPhep", SoGiayPhep);
            sqlCmd.Parameters.AddWithValue("@NoiCap", NoiCap);
            sqlCmd.Parameters.AddWithValue("@CodeSUN", CodeSUN);
            sqlCmd.Parameters.AddWithValue("@IsProject", IsProject);
            sqlCmd.Parameters.AddWithValue("@AmountColumn", AmountColumn);
            sqlCmd.Parameters.AddWithValue("@WidthCell", WidthCell);
            if(NgayCap.Year != 1)
                sqlCmd.Parameters.AddWithValue("@NgayCap", NgayCap);
            else
                sqlCmd.Parameters.AddWithValue("@NgayCap", DBNull.Value);
            sqlCmd.Parameters.AddWithValue("@DienTich", DienTich);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public void UpdateTemplate()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("DuAn_updateTemplate", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaDA", MaDA);
            sqlCmd.Parameters.AddWithValue("@Template", Template);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public DataTable Select()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("DuAn_getAll", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectShow()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("DuAn_getAllShow", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectShowByLoaiDA()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("DuAn_getAllShowByMaLoaiDA " + LoaiDA.MaLoaiDA, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectShowByLoaiDA2()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("DuAn_getAllShowByMaLoaiDA2 " + LoaiDA.MaLoaiDA, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectAll()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("DuAn_getAll2", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectAll2()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("DuAn_getAll3", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public DataTable SelectShow2()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("DuAn_getAllShow2", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void Detail(int _MaDA)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("DuAn_get", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaDA", _MaDA);
            sqlCon.Open();
            SqlDataReader dread = sqlCmd.ExecuteReader();
            if (dread.Read())
            {
                MaDA = int.Parse(dread["MaDA"].ToString());
                TenDA = dread["TenDA"] as string;
                TenThuongMai = dread["TenThuongMai"] as string;
                DiaChi = dread["DiaChi"] as string;
                Xa.MaXa = int.Parse(dread["MaXa"].ToString());
                NhanVien.MaNV = int.Parse(dread["MaNV"].ToString());
                LoaiDA.MaLoaiDA = byte.Parse(dread["MaLoaiDA"].ToString());
                AttachFile = dread["AttachFile"] as string;
            }
            sqlCon.Close();
        }

        public int GetMaDAByTVT(string _TenVietTat)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("DuAn_getByTenVietTat", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@TenVietTat", _TenVietTat);
            sqlCmd.Parameters.Add("@MaDA", SqlDbType.Int).Direction = ParameterDirection.Output;
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();

            return int.Parse(sqlCmd.Parameters["@MaDA"].Value.ToString());
        }

        public DataTable List()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("DuAn_list", sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0];
        }

        public void Delete()
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlCommand sqlCmd = new SqlCommand("DuAn_delete", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@MaDA", MaDA);
            sqlCon.Open();
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
        }

        public static DataRow GetTemplate(string _MaBDS, byte _MaBM)
        {
            SqlConnection sqlCon = new SqlConnection(CommonCls.Conn);
            SqlDataAdapter sqlDA = new SqlDataAdapter("DuAn_BieuMau_getByMaBDS '" + _MaBDS + "'," + _MaBM, sqlCon);
            DataSet dSet = new DataSet();
            sqlCon.Open();
            sqlDA.Fill(dSet);
            sqlCon.Close();
            return dSet.Tables[0].Rows[0];
        }

        public static DataTable getList()
        {
            return SqlCommon.getData("select MaDA, TenDA from DuAn order by TenDA");
        }
    }
}