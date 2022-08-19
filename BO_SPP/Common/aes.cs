using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace BO_SPP.Common
{
    public class aes
    {
        private static string key = "fae23ddb95d946e9a04a6ffba5a3c3fb";

        public static string Enc(string plainText)
        {
            try
            {
                if (plainText == null)
                    return null;

                if (plainText == "")
                    return "";

                byte[] iv = new byte[16];
                byte[] array;
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(key);
                    aes.IV = iv;
                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                            {
                                streamWriter.Write(plainText);
                            }
                            array = memoryStream.ToArray();
                        }
                    }
                }
                return Convert.ToBase64String(array);
            }
            catch
            {
                return null;
            }
        }

        public static string Dec(string cipherText)
        {
            try
            {
                if (cipherText == null)
                    return null;

                if (cipherText == "")
                    return "";

                byte[] iv = new byte[16];
                byte[] buffer = Convert.FromBase64String(cipherText);
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(key);//I have already defined "Key" in the above EncryptString function
                    aes.IV = iv;
                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                    using (MemoryStream memoryStream = new MemoryStream(buffer))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                            {
                                return streamReader.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        public static void EncryptDatabase()
        {
            DataTable tblM_User = mssql.GetDataTable("SELECT * FROM tblM_User");
            foreach (DataRow dr in tblM_User.Rows)
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@UserID", dr["UserID"].ToString()));
                param.Add(new SqlParameter("@Fullname", string.IsNullOrEmpty(dr["Fullname"].ToString()) ? "" : aes.Enc(dr["Fullname"].ToString())));
                param.Add(new SqlParameter("@Email", string.IsNullOrEmpty(dr["Email"].ToString()) ? "" : aes.Enc(dr["Email"].ToString())));
                param.Add(new SqlParameter("@Mobile", string.IsNullOrEmpty(dr["Mobile"].ToString()) ? "" : aes.Enc(dr["Mobile"].ToString())));
                param.Add(new SqlParameter("@MobileTemp", string.IsNullOrEmpty(dr["MobileTemp"].ToString()) ? "" : aes.Enc(dr["MobileTemp"].ToString())));
                param.Add(new SqlParameter("@Address", string.IsNullOrEmpty(dr["Address"].ToString()) ? "" : aes.Enc(dr["Address"].ToString())));
                param.Add(new SqlParameter("@NIP", string.IsNullOrEmpty(dr["NIP"].ToString()) ? "" : aes.Enc(dr["NIP"].ToString())));
                param.Add(new SqlParameter("@Jabatan", string.IsNullOrEmpty(dr["Jabatan"].ToString()) ? "" : aes.Enc(dr["Jabatan"].ToString())));
                param.Add(new SqlParameter("@Divisi", string.IsNullOrEmpty(dr["Divisi"].ToString()) ? "" : aes.Enc(dr["Divisi"].ToString())));
                param.Add(new SqlParameter("@CreatedBy", string.IsNullOrEmpty(dr["CreatedBy"].ToString()) ? "" : aes.Enc(dr["CreatedBy"].ToString())));
                param.Add(new SqlParameter("@UpdatedBy", string.IsNullOrEmpty(dr["UpdatedBy"].ToString()) ? "" : aes.Enc(dr["UpdatedBy"].ToString())));
                param.Add(new SqlParameter("@DeletedBy", string.IsNullOrEmpty(dr["DeletedBy"].ToString()) ? "" : aes.Enc(dr["DeletedBy"].ToString())));
                mssql.ExecuteNonQuery("sp_Encrypt_tblM_User", param);
            }

            DataTable tblT_Dumas = mssql.GetDataTable("SELECT * FROM tblT_Dumas WHERE Nomor IS NOT NULL");
            foreach (DataRow dr in tblT_Dumas.Rows)
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", dr["ID"].ToString()));
                param.Add(new SqlParameter("@Email", string.IsNullOrEmpty(dr["Email"].ToString()) ? "" : aes.Enc(dr["Email"].ToString())));
                param.Add(new SqlParameter("@Handphone", string.IsNullOrEmpty(dr["Handphone"].ToString()) ? "" : aes.Enc(dr["Handphone"].ToString())));
                param.Add(new SqlParameter("@TempatKejadian", string.IsNullOrEmpty(dr["TempatKejadian"].ToString()) ? "" : aes.Enc(dr["TempatKejadian"].ToString())));
                param.Add(new SqlParameter("@Kronologi", string.IsNullOrEmpty(dr["Kronologi"].ToString()) ? "" : aes.Enc(dr["Kronologi"].ToString())));
                param.Add(new SqlParameter("@PenyaluranBy", string.IsNullOrEmpty(dr["PenyaluranBy"].ToString()) ? "" : aes.Enc(dr["PenyaluranBy"].ToString())));
                param.Add(new SqlParameter("@TindakLanjutBy", string.IsNullOrEmpty(dr["TindakLanjutBy"].ToString()) ? "" : aes.Enc(dr["TindakLanjutBy"].ToString())));
                param.Add(new SqlParameter("@ResponBy", string.IsNullOrEmpty(dr["ResponBy"].ToString()) ? "" : aes.Enc(dr["ResponBy"].ToString())));
                param.Add(new SqlParameter("@CreatedBy", string.IsNullOrEmpty(dr["CreatedBy"].ToString()) ? "" : aes.Enc(dr["CreatedBy"].ToString())));
                param.Add(new SqlParameter("@UpdatedBy", string.IsNullOrEmpty(dr["UpdatedBy"].ToString()) ? "" : aes.Enc(dr["UpdatedBy"].ToString())));
                param.Add(new SqlParameter("@ProsesBy", string.IsNullOrEmpty(dr["ProsesBy"].ToString()) ? "" : aes.Enc(dr["ProsesBy"].ToString())));
                param.Add(new SqlParameter("@Jenis_Pelanggaran", string.IsNullOrEmpty(dr["Jenis_Pelanggaran"].ToString()) ? "" : aes.Enc(dr["Jenis_Pelanggaran"].ToString())));
                param.Add(new SqlParameter("@Keterangan_Penyaluran", string.IsNullOrEmpty(dr["Keterangan_Penyaluran"].ToString()) ? "" : aes.Enc(dr["Keterangan_Penyaluran"].ToString())));
                param.Add(new SqlParameter("@Keterangan_Pemeriksaan", string.IsNullOrEmpty(dr["Keterangan_Pemeriksaan"].ToString()) ? "" : aes.Enc(dr["Keterangan_Pemeriksaan"].ToString())));
                param.Add(new SqlParameter("@Keterangan_Konfirmasi", string.IsNullOrEmpty(dr["Keterangan_Konfirmasi"].ToString()) ? "" : aes.Enc(dr["Keterangan_Konfirmasi"].ToString())));
                param.Add(new SqlParameter("@Keterangan_Respon", string.IsNullOrEmpty(dr["Keterangan_Respon"].ToString()) ? "" : aes.Enc(dr["Keterangan_Respon"].ToString())));

                mssql.ExecuteNonQuery("sp_Encrypt_tblT_Dumas", param);
            }

            DataTable tblT_Dumas_Detail = mssql.GetDataTable("SELECT * FROM tblT_Dumas_Detail");
            foreach (DataRow dr in tblT_Dumas_Detail.Rows)
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", dr["ID"].ToString()));
                param.Add(new SqlParameter("@Nama", string.IsNullOrEmpty(dr["Nama"].ToString()) ? "" : aes.Enc(dr["Nama"].ToString())));
                param.Add(new SqlParameter("@NomorHandphone", string.IsNullOrEmpty(dr["NomorHandphone"].ToString()) ? "" : aes.Enc(dr["NomorHandphone"].ToString())));
                param.Add(new SqlParameter("@Departemen", string.IsNullOrEmpty(dr["Departemen"].ToString()) ? "" : aes.Enc(dr["Departemen"].ToString())));
                param.Add(new SqlParameter("@Jabatan", string.IsNullOrEmpty(dr["Jabatan"].ToString()) ? "" : aes.Enc(dr["Jabatan"].ToString())));
                param.Add(new SqlParameter("@CreatedBy", string.IsNullOrEmpty(dr["CreatedBy"].ToString()) ? "" : aes.Enc(dr["CreatedBy"].ToString())));

                mssql.ExecuteNonQuery("sp_Encrypt_tblT_Dumas_Detail", param);
            }

            DataTable tblT_Tanggapan = mssql.GetDataTable("SELECT * FROM tblT_Tanggapan");
            foreach (DataRow dr in tblT_Tanggapan.Rows)
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", dr["ID"].ToString()));
                param.Add(new SqlParameter("@Email", string.IsNullOrEmpty(dr["Email"].ToString()) ? "" : aes.Enc(dr["Email"].ToString())));
                param.Add(new SqlParameter("@Nama", string.IsNullOrEmpty(dr["Nama"].ToString()) ? "" : aes.Enc(dr["Nama"].ToString())));
                param.Add(new SqlParameter("@Tanggapan", string.IsNullOrEmpty(dr["Tanggapan"].ToString()) ? "" : aes.Enc(dr["Tanggapan"].ToString())));
                param.Add(new SqlParameter("@CreatedBy", string.IsNullOrEmpty(dr["CreatedBy"].ToString()) ? "" : aes.Enc(dr["CreatedBy"].ToString())));
                mssql.ExecuteNonQuery("sp_Encrypt_tblT_Tanggapan", param);
            }

            DataTable AuditTrail = mssql.GetDataTable("SELECT * FROM AuditTrail");
            foreach (DataRow dr in AuditTrail.Rows)
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", dr["ID"].ToString()));
                param.Add(new SqlParameter("@Username", string.IsNullOrEmpty(dr["Username"].ToString()) ? "" : aes.Enc(dr["Username"].ToString())));
                mssql.ExecuteNonQuery("sp_Encrypt_AuditTrail", param);
            }

            DataTable dt = mssql.GetDataTable("SELECT * FROM tblM_Role");
            foreach (DataRow dr in dt.Rows)
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", dr["ID"].ToString()));
                param.Add(new SqlParameter("@CreatedBy", string.IsNullOrEmpty(dr["CreatedBy"].ToString()) ? "" : aes.Enc(dr["CreatedBy"].ToString())));
                param.Add(new SqlParameter("@UpdatedBy", string.IsNullOrEmpty(dr["UpdatedBy"].ToString()) ? "" : aes.Enc(dr["UpdatedBy"].ToString())));
                mssql.ExecuteNonQuery("sp_Encrypt_tblM_Role", param);
            }

            DataTable tblT_EmailSetting = mssql.GetDataTable("SELECT * FROM tblT_EmailSetting");
            foreach (DataRow dr in tblT_EmailSetting.Rows)
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", dr["ID"].ToString()));
                param.Add(new SqlParameter("@CreatedBy", string.IsNullOrEmpty(dr["CreatedBy"].ToString()) ? "" : aes.Enc(dr["CreatedBy"].ToString())));
                param.Add(new SqlParameter("@UpdatedBy", string.IsNullOrEmpty(dr["UpdatedBy"].ToString()) ? "" : aes.Enc(dr["UpdatedBy"].ToString())));
                mssql.ExecuteNonQuery("sp_Encrypt_tblT_EmailSetting", param);
            }

            DataTable NotificationSetting = mssql.GetDataTable("SELECT * FROM NotificationSetting");
            foreach (DataRow dr in NotificationSetting.Rows)
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", dr["ID"].ToString()));
                param.Add(new SqlParameter("@SMTPAddress", string.IsNullOrEmpty(dr["SMTPAddress"].ToString()) ? "" : aes.Enc(dr["SMTPAddress"].ToString())));
                param.Add(new SqlParameter("@SMTPPort", string.IsNullOrEmpty(dr["SMTPPort"].ToString()) ? "" : aes.Enc(dr["SMTPPort"].ToString())));
                param.Add(new SqlParameter("@EmailAddress", string.IsNullOrEmpty(dr["EmailAddress"].ToString()) ? "" : aes.Enc(dr["EmailAddress"].ToString())));
                param.Add(new SqlParameter("@Password", string.IsNullOrEmpty(dr["Password"].ToString()) ? "" : aes.Enc(dr["Password"].ToString())));
                param.Add(new SqlParameter("@SenderName", string.IsNullOrEmpty(dr["SenderName"].ToString()) ? "" : aes.Enc(dr["SenderName"].ToString())));
                param.Add(new SqlParameter("@CreatedBy", string.IsNullOrEmpty(dr["CreatedBy"].ToString()) ? "" : aes.Enc(dr["CreatedBy"].ToString())));
                param.Add(new SqlParameter("@UpdatedBy", string.IsNullOrEmpty(dr["UpdatedBy"].ToString()) ? "" : aes.Enc(dr["UpdatedBy"].ToString())));
                mssql.ExecuteNonQuery("sp_Encrypt_NotificationSetting", param);
            }

            DataTable FileEkstensionFilter = mssql.GetDataTable("SELECT * FROM FileEkstensionFilter");
            foreach (DataRow dr in FileEkstensionFilter.Rows)
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", dr["ID"].ToString()));
                param.Add(new SqlParameter("@CreatedBy", string.IsNullOrEmpty(dr["CreatedBy"].ToString()) ? "" : aes.Enc(dr["CreatedBy"].ToString())));
                param.Add(new SqlParameter("@UpdatedBy", string.IsNullOrEmpty(dr["UpdatedBy"].ToString()) ? "" : aes.Enc(dr["UpdatedBy"].ToString())));
                mssql.ExecuteNonQuery("sp_Encrypt_FileEkstensionFilter", param);
            }

            DataTable tblT_CMS = mssql.GetDataTable("SELECT * FROM tblT_CMS");
            foreach (DataRow dr in tblT_CMS.Rows)
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", dr["ID"].ToString()));
                param.Add(new SqlParameter("@CreatedBy", string.IsNullOrEmpty(dr["CreatedBy"].ToString()) ? "" : aes.Enc(dr["CreatedBy"].ToString())));
                param.Add(new SqlParameter("@UpdatedBy", string.IsNullOrEmpty(dr["UpdatedBy"].ToString()) ? "" : aes.Enc(dr["UpdatedBy"].ToString())));
                mssql.ExecuteNonQuery("sp_Encrypt_tblT_CMS", param);
            }

            DataTable tblT_Banner = mssql.GetDataTable("SELECT * FROM tblT_Banner");
            foreach (DataRow dr in tblT_Banner.Rows)
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", dr["ID"].ToString()));
                param.Add(new SqlParameter("@CreatedBy", string.IsNullOrEmpty(dr["CreatedBy"].ToString()) ? "" : aes.Enc(dr["CreatedBy"].ToString())));
                param.Add(new SqlParameter("@UpdatedBy", string.IsNullOrEmpty(dr["UpdatedBy"].ToString()) ? "" : aes.Enc(dr["UpdatedBy"].ToString())));
                mssql.ExecuteNonQuery("sp_Encrypt_tblT_Banner", param);
            }

            DataTable tblM_Delegator = mssql.GetDataTable("SELECT * FROM tblM_Delegator");
            foreach (DataRow dr in tblM_Delegator.Rows)
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", dr["ID"].ToString()));
                param.Add(new SqlParameter("@CreatedBy", string.IsNullOrEmpty(dr["CreatedBy"].ToString()) ? "" : aes.Enc(dr["CreatedBy"].ToString())));
                param.Add(new SqlParameter("@UpdatedBy", string.IsNullOrEmpty(dr["UpdatedBy"].ToString()) ? "" : aes.Enc(dr["UpdatedBy"].ToString())));
                mssql.ExecuteNonQuery("sp_Encrypt_tblM_Delegator", param);
            }

            DataTable tblT_UserInDelegator = mssql.GetDataTable("SELECT * FROM tblT_UserInDelegator");
            foreach (DataRow dr in tblT_UserInDelegator.Rows)
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", dr["ID"].ToString()));
                param.Add(new SqlParameter("@CreatedBy", string.IsNullOrEmpty(dr["CreatedBy"].ToString()) ? "" : aes.Enc(dr["CreatedBy"].ToString())));
                mssql.ExecuteNonQuery("sp_Encrypt_tblT_UserInDelegator", param);
            }

            DataTable Kuesioner = mssql.GetDataTable("SELECT * FROM Kuesioner");
            foreach (DataRow dr in Kuesioner.Rows)
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", dr["ID"].ToString()));
                param.Add(new SqlParameter("@CreatedBy", string.IsNullOrEmpty(dr["CreatedBy"].ToString()) ? "" : aes.Enc(dr["CreatedBy"].ToString())));
                param.Add(new SqlParameter("@UpdatedBy", string.IsNullOrEmpty(dr["UpdatedBy"].ToString()) ? "" : aes.Enc(dr["UpdatedBy"].ToString())));
                mssql.ExecuteNonQuery("sp_Encrypt_Kuesioner", param);
            }

            DataTable KuesionerDetail = mssql.GetDataTable("SELECT * FROM KuesionerDetail");
            foreach (DataRow dr in KuesionerDetail.Rows)
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", dr["ID"].ToString()));
                param.Add(new SqlParameter("@CreatedBy", string.IsNullOrEmpty(dr["CreatedBy"].ToString()) ? "" : aes.Enc(dr["CreatedBy"].ToString())));
                param.Add(new SqlParameter("@UpdatedBy", string.IsNullOrEmpty(dr["UpdatedBy"].ToString()) ? "" : aes.Enc(dr["UpdatedBy"].ToString())));
                mssql.ExecuteNonQuery("sp_Encrypt_KuesionerDetail", param);
            }

            DataTable tblT_User_Password_Forgotten = mssql.GetDataTable("SELECT * FROM tblT_User_Password_Forgotten");
            foreach (DataRow dr in tblT_User_Password_Forgotten.Rows)
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", dr["ID"].ToString()));
                param.Add(new SqlParameter("@Email", string.IsNullOrEmpty(dr["Email"].ToString()) ? "" : aes.Enc(dr["Email"].ToString())));
                param.Add(new SqlParameter("@CreatedBy", string.IsNullOrEmpty(dr["CreatedBy"].ToString()) ? "" : aes.Enc(dr["CreatedBy"].ToString())));
                param.Add(new SqlParameter("@UpdatedBy", string.IsNullOrEmpty(dr["UpdatedBy"].ToString()) ? "" : aes.Enc(dr["UpdatedBy"].ToString())));
                mssql.ExecuteNonQuery("sp_Encrypt_tblT_User_Password_Forgotten", param);
            }

            DataTable tblT_Dumas_Detail_2 = mssql.GetDataTable("SELECT * FROM tblT_Dumas_Detail WHERE CreatedBy LIKE '%@%'");
            foreach (DataRow dr in tblT_Dumas_Detail_2.Rows)
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", dr["ID"].ToString()));
                param.Add(new SqlParameter("@CreatedBy", string.IsNullOrEmpty(dr["CreatedBy"].ToString()) ? "" : aes.Enc(dr["CreatedBy"].ToString())));
                mssql.ExecuteNonQuery("sp_Encrypt_tblT_Dumas_Detail_2", param);
            }

            DataTable KuesionerValue = mssql.GetDataTable("SELECT * FROM KuesionerValue WHERE CreatedBy LIKE '%@%'");
            foreach (DataRow dr in KuesionerValue.Rows)
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", dr["ID"].ToString()));
                param.Add(new SqlParameter("@CreatedBy", string.IsNullOrEmpty(dr["CreatedBy"].ToString()) ? "" : aes.Enc(dr["CreatedBy"].ToString())));
                mssql.ExecuteNonQuery("sp_Encrypt_KuesionerValue", param);
            }

            DataTable tblM_Referensi = mssql.GetDataTable("SELECT * FROM tblM_Referensi WHERE Updated_By LIKE '%@%'");
            foreach (DataRow dr in tblM_Referensi.Rows)
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", int.Parse(dr["ID"].ToString())));
                param.Add(new SqlParameter("@Updated_By", aes.Enc(dr["Updated_By"].ToString())));
                mssql.ExecuteNonQuery("sp_Encrypt_tblM_Referensi", param);
            }

            DataTable tblT_File_Evidence = mssql.GetDataTable("SELECT * FROM tblT_File_Evidence WHERE CreatedBy LIKE '%@%'");
            foreach (DataRow dr in tblT_File_Evidence.Rows)
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", dr["ID"].ToString()));
                param.Add(new SqlParameter("@CreatedBy", string.IsNullOrEmpty(dr["CreatedBy"].ToString()) ? "" : aes.Enc(dr["CreatedBy"].ToString())));
                mssql.ExecuteNonQuery("sp_Encrypt_tblT_File_Evidence", param);
            }

            DataTable tblT_UserInRole = mssql.GetDataTable("SELECT * FROM tblT_UserInRole WHERE CreatedBy LIKE '%@%'");
            foreach (DataRow dr in tblT_UserInRole.Rows)
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", dr["ID"].ToString()));
                param.Add(new SqlParameter("@CreatedBy", string.IsNullOrEmpty(dr["CreatedBy"].ToString()) ? "" : aes.Enc(dr["CreatedBy"].ToString())));
                mssql.ExecuteNonQuery("sp_Encrypt_tblT_UserInRole", param);
            }

            DataTable tblT_New_User_Verification = mssql.GetDataTable("SELECT * FROM tblT_New_User_Verification WHERE CreatedBy LIKE '%@%' OR Email LIKE '%@%'");
            foreach (DataRow dr in tblT_New_User_Verification.Rows)
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", dr["ID"].ToString()));
                param.Add(new SqlParameter("@Email", string.IsNullOrEmpty(dr["Email"].ToString()) ? "" : aes.Enc(dr["Email"].ToString())));
                param.Add(new SqlParameter("@CreatedBy", string.IsNullOrEmpty(dr["CreatedBy"].ToString()) ? "" : aes.Enc(dr["CreatedBy"].ToString())));
                mssql.ExecuteNonQuery("sp_Encrypt_tblT_New_User_Verification", param);
            }
        }

    }
}
