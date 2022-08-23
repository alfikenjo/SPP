using FluentFTP;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Frontend_SPP.Models;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Text.RegularExpressions;
using RestSharp;
using Newtonsoft.Json;

namespace Frontend_SPP.Common
{
    public class Helper
    {
        static readonly string ftpAddress = ConfigurationManager.AppSetting["FileConfiguration:ftpAddress"];
        static readonly string ftpUsername = ConfigurationManager.AppSetting["FileConfiguration:ftpUsername"];
        static readonly string ftpPassword = ConfigurationManager.AppSetting["FileConfiguration:ftpPassword"];
        static readonly string FolderPath = ConfigurationManager.AppSetting["FileConfiguration:FolderPath"];

        static readonly string SMTPAddress = ConfigurationManager.AppSetting["FileConfiguration:SMTPAddress"];
        static readonly string SMTPPort = ConfigurationManager.AppSetting["FileConfiguration:SMTPPort"];
        static readonly string EmailAddress = ConfigurationManager.AppSetting["FileConfiguration:EmailAddress"];
        static readonly string SenderName = ConfigurationManager.AppSetting["FileConfiguration:SenderName"];
        static readonly string Password = ConfigurationManager.AppSetting["FileConfiguration:Password"];

        static readonly string APIKEY = ConfigurationManager.AppSetting["FileConfiguration:SMSVIRO_APIKEY"];
        static readonly string URL = ConfigurationManager.AppSetting["FileConfiguration:URL_SMSVIRO_SINGLE"];

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "@ABCDEFGHJKMNPQRSTUVWXYZabcdefghjkmnpqrstuvwxy23456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GetBinaryImage(string FileName, string FileExtension)
        {
            string Result;
            try
            {
                using (var conn = new FtpClient(ftpAddress, ftpUsername, ftpPassword))
                {
                    conn.Connect();

                    // open an read-only stream to the file
                    using (var istream = conn.OpenRead(FolderPath + FileName))
                    {
                        try
                        {
                            byte[] bytes;
                            using (var memoryStream = new MemoryStream())
                            {
                                istream.CopyTo(memoryStream);
                                bytes = memoryStream.ToArray();
                            }

                            string base64 = Convert.ToBase64String(bytes);
                            string contenttype = "image";
                            if (FileExtension == "pdf" || FileExtension == ".pdf")
                                contenttype = "application";
                            Result = "data:" + contenttype + "/" + FileExtension + ";base64," + base64;
                        }
                        finally
                        {
                            istream.Close();
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                Result = "failed, " + exc.Message;
            }

            return Result;
        }

        public static string UploadFTP(Stream stream, string Filename, string Ekstension)
        {
            string result = "";
            try
            {
                if (Ekstension.Length > 0)
                    Ekstension = Ekstension.Trim().ToLower().Replace(".", "");

                long MaxUploadSize = 1;
                DataTable dtFileUpload = mssql.GetDataTable("SELECT TOP 1 * FROM tblM_Referensi WHERE Type = 'Max Upload Size'");
                if (dtFileUpload.Rows.Count == 1)
                    MaxUploadSize = int.Parse(dtFileUpload.Rows[0]["Value"].ToString());

                long size = stream.Length / 1024;
                if (size > (MaxUploadSize * 1000))
                    throw new Exception("Declined, file upload cannot exceed " + MaxUploadSize + " MB");

                bool ValidEkstension = false;
                DataTable dt_FileEkstensionFilter = mssql.GetDataTable("SELECT Name FROM FileEkstensionFilter ORDER BY Name");
                foreach (DataRow dr in dt_FileEkstensionFilter.Rows)
                {
                    string Eks = dr["Name"].ToString().Replace(".", "").Trim();
                    string FileEkstensionFilter = Eks;

                    if (Ekstension == Eks.ToLower())
                        ValidEkstension = true;
                }

                if (!ValidEkstension)
                    throw new Exception("Failed, file or attachment is not valid");

                if (stream.Length == 0)
                    throw new Exception("Failed, file or attachment is not valid");

                using (var ftp = new FtpClient(ftpAddress, ftpUsername, ftpPassword))
                {
                    ftp.Connect();
                    ftp.Upload(stream, FolderPath + Filename, FtpRemoteExists.Overwrite, true);
                    result = "success";
                }
            }
            catch (Exception exc)
            {
                result = exc.Message;
            }
            return result;
        }

        public static string GetImageCaptcha(string key, string captcha)
        {
            string Result;
            try
            {
                int height = 30;
                int width = 100;
                Bitmap bmp = new Bitmap(width, height);
                RectangleF rectf = new RectangleF(10, 5, 0, 0);
                Graphics g = Graphics.FromImage(bmp);
                g.Clear(Color.White);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawString(captcha.ToString(), new Font("Thaoma", 12, FontStyle.Italic), Brushes.Chocolate, rectf);
                g.DrawRectangle(new Pen(Color.Blue), 1, 1, width - 2, height - 2);
                g.Flush();

                //var bitmap = new Bitmap(@"c:\Documente und Einstellungen\daniel.hilgarth\Desktop\Unbenannt.bmp");

                ImageCodecInfo jpgEncoder = ImageCodecInfo.GetImageEncoders().Single(x => x.FormatDescription == "JPEG");
                Encoder encoder2 = System.Drawing.Imaging.Encoder.Quality;
                EncoderParameters parameters = new System.Drawing.Imaging.EncoderParameters(1);
                EncoderParameter parameter = new EncoderParameter(encoder2, 50L);
                parameters.Param[0] = parameter;

                System.IO.Stream stream = new MemoryStream();
                bmp.Save(stream, jpgEncoder, parameters);
                //bitmap.Save(@"C:\Temp\TestJPEG.jpg", jpgEncoder, parameters);

                var bytes = ((MemoryStream)stream).ToArray();
                System.IO.Stream inputStream = new MemoryStream(bytes);
                //Bitmap fromDisk = new Bitmap(@"C:\Temp\TestJPEG.jpg");
                Bitmap fromStream = new Bitmap(inputStream);

                string base64 = Convert.ToBase64String(bytes);
                Result = "data:image/jpg;base64," + base64;

                //try
                //{
                //    byte[] bytes;
                //    using (var memoryStream = new MemoryStream())
                //    {
                //        istream.CopyTo(memoryStream);
                //        bytes = memoryStream.ToArray();
                //    }

                //    string base64 = Convert.ToBase64String(bytes);
                //    Result = "data:image/" + FileExtension + ";base64," + base64;
                //}
                //finally
                //{
                //    Console.WriteLine();
                //    istream.Close();
                //}
            }
            catch (Exception exc)
            {
                Result = "failed, " + exc.Message;
            }

            return Result;
        }

        public static int SendMail(string Email, string Subject, string MailBody)
        {
            try
            {
                DataRow drEmailNotification = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM tblM_User WHERE Email = '" + aes.Enc(Email) + "' AND ISNULL(EmailNotification, 0) = 1");
                if (int.Parse(drEmailNotification["Count"].ToString()) == 0)
                    throw new Exception("Notification is not aktif");

                string _SMTPAddress = SMTPAddress;
                string _SMTPPort = SMTPPort;
                string _SenderName = SenderName;
                string _EmailAddress = EmailAddress;
                string _Password = Password;
                bool _EnableSsl = true;
                bool _UseDefaultCredentials = true;

                DataTable dtNotification = mssql.GetDataTable("SELECT * FROM NotificationSetting");
                if (dtNotification.Rows.Count == 1)
                {
                    DataRow dr = dtNotification.Rows[0];
                    _SMTPAddress = aes.Dec(dr["SMTPAddress"].ToString());
                    _SMTPPort = aes.Dec(dr["SMTPPort"].ToString());
                    _SenderName = aes.Dec(dr["SenderName"].ToString());
                    _EmailAddress = aes.Dec(dr["EmailAddress"].ToString());
                    _Password = aes.Dec(dr["Password"].ToString());
                    _EnableSsl = bool.Parse(dr["EnableSSL"].ToString());
                    _UseDefaultCredentials = bool.Parse(dr["UseDefaultCredentials"].ToString());
                }

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(_EmailAddress, _SenderName);
                mail.To.Add(new MailAddress(Email));
                mail.Subject = Subject;
                mail.Body = MailBody;
                mail.IsBodyHtml = true;
                SmtpClient client = new SmtpClient(_SMTPAddress, int.Parse(_SMTPPort));
                client.Credentials = new System.Net.NetworkCredential(_EmailAddress, _Password);
                client.EnableSsl = _EnableSsl;
                client.UseDefaultCredentials = _UseDefaultCredentials;
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                client.Send(mail);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }        

        public static void RecordAuditTrail(string Username, string Menu, string Halaman, string Item, string Action, string Description)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Username", Username));
            param.Add(new SqlParameter("@Menu", Menu));
            param.Add(new SqlParameter("@Halaman", Halaman));
            param.Add(new SqlParameter("@Item", Item));
            param.Add(new SqlParameter("@Action", Action));
            param.Add(new SqlParameter("@Description", Description));
            mssql.ExecuteNonQuery("sp_RecordAuditTrail", param);

        }

        public static bool AuthorizedByUsername(string SessionID, string UserID, string Controller, string Act, string SMV)
        {
            bool isAuthorized = false;
            try
            {
                if (string.IsNullOrEmpty(SessionID) || string.IsNullOrEmpty(UserID))
                    return false;

                SessionID = StringCipher.Decrypt(SessionID);
                UserID = StringCipher.Decrypt(UserID);

                string Roles = "";

                DataRow dr_Login = mssql.GetDataRow("SELECT COUNT(*) [Jumlah] FROM tblT_User_Login WHERE UserID = '" + UserID + "' AND ID = '" + SessionID + "' AND isActive = 1");
                if (int.Parse(dr_Login["Jumlah"].ToString()) == 0)
                    return false;

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@UserID", UserID));
                DataTable dt = mssql.GetDataTable("sp_GetAccountByUserID", param);
                foreach (DataRow dr in dt.Rows)
                {
                    Roles += dr["Role"] + ";";
                }

                if (Controller == "Account")
                {
                    isAuthorized = Roles.Contains("Pelapor");
                }
                else if (Controller == "Dashboard")
                {
                    isAuthorized = Roles.Contains("Pelapor");
                }
                else if (Controller == "Pengaduan")
                {
                    isAuthorized = Roles.Contains("Pelapor");
                }
                else if (Controller == "Kuesioner")
                {
                    isAuthorized = Roles.Contains("Pelapor");
                }
                return isAuthorized;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static string SendSMSMultiple(string message, List<string> phoneNumbers)
        {
            try
            {
                string error_message = string.Empty;
                string list = string.Empty;

                foreach (string parameterName in phoneNumbers)
                {
                    list += "\"" + parameterName + "\",";
                }
                var combined = string.Join(",", phoneNumbers);

                var pesan = Regex.Replace(message, @"\r|\n", "\n");
                var pesan2 = Regex.Replace(pesan, @"\n\n", "\\n");

                var client = new RestClient("" + URL + "");
                //var client = new RestClient("https://api.smsviro.com/restapi/sms/1/text/single");
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;


                RestRequest request = new RestRequest();
                request.Method = Method.Post;
                request.AddHeader("accept", "application/json");
                request.AddHeader("content-type", "application/json");
                request.AddHeader("authorization", "App " + APIKEY + "");
                request.AddParameter("application/json", "{\"from\":\"PT SMI\", \"to\":" + list + "\"text\":\"" + pesan2 + "\"}",
                ParameterType.RequestBody);
                RestResponse response = client.Execute(request);

                var resp = JsonConvert.DeserializeObject(response.Content);

                string errors = response.ErrorMessage;

                if (response.IsSuccessful == true)
                {
                    error_message = "Success"; //+ response.Content;
                }
                else
                {
                    error_message = string.Format("Message = {0}", errors);
                }

                return error_message;
            }
            catch (Exception e)
            {
                return string.Format(@"Error Message: {0}.{1}Error Stacktrace: {2}", e.Message, Environment.NewLine, e.StackTrace);
            }
        }

        public static string SendSMSSingle(string message, string phoneNumber)
        {
            try
            {
                string error_message = string.Empty;

                string firstDigit = phoneNumber.Substring(0, 1);
                if (firstDigit != "0")
                    phoneNumber = "+62" + phoneNumber;

                var pesan = Regex.Replace(message, @"\r|\n", "\n");
                var pesan2 = Regex.Replace(pesan, @"\n\n", "\\n");

                var client = new RestClient(URL);
                //var client = new RestClient("https://api.smsviro.com/restapi/sms/1/text/single");
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                RestRequest request = new RestRequest();
                request.Method = Method.Post;
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("authorization", "App " + APIKEY + "");
                request.AddParameter("application/json", "{\"from\":\"PT SMI\", \"to\":\"" + phoneNumber + "\",\"text\":\"" + pesan2 + "\"}", ParameterType.RequestBody);
                RestResponse response = client.Execute(request);

                var resp = JsonConvert.DeserializeObject(response.Content);

                string errors = response.ErrorMessage;

                if (response.IsSuccessful == true)
                {
                    error_message = "Success"; //+ response.Content;
                }
                else
                {
                    error_message = response.Content;
                }

                return error_message;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public static bool CheckUserActive(string Email)
        {
            DataRow drIsUserActive = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM tblM_User WHERE Email = '" + Email + "' AND IsActive = 1");
            return int.Parse(drIsUserActive["Count"].ToString()) > 0;
        }

        public static string UploadFTPWithEcryption(IFormFile file, string Filename, string Ekstension)
        {
            string result = "";
            try
            {
                if (Ekstension.Length > 0)
                    Ekstension = Ekstension.Trim().ToLower().Replace(".", "");

                bool ValidEkstension = false;
                DataTable dt_FileEkstensionFilter = mssql.GetDataTable("SELECT Name FROM FileEkstensionFilter ORDER BY Name");
                foreach (DataRow dr in dt_FileEkstensionFilter.Rows)
                {
                    string Eks = dr["Name"].ToString().Replace(".", "").Trim();
                    string FileEkstensionFilter = Eks;

                    if (Ekstension == Eks.ToLower())
                        ValidEkstension = true;
                }

                if (!ValidEkstension)
                    throw new Exception("Failed, file or attachment is not valid");

                long MaxUploadSize = 1;
                DataTable dtFileUpload = mssql.GetDataTable("SELECT TOP 1 * FROM tblM_Referensi WHERE Type = 'Max Upload Size'");
                if (dtFileUpload.Rows.Count == 1)
                    MaxUploadSize = int.Parse(dtFileUpload.Rows[0]["Value"].ToString());

                using (var original_ms = new MemoryStream())
                {
                    file.CopyTo(original_ms);
                    long size = original_ms.Length / 1024;
                    if (size > (MaxUploadSize * 1000))
                        throw new Exception("Declined, file upload cannot exceed " + MaxUploadSize + " MB");

                    if (original_ms.Length == 0)
                        throw new Exception("Failed, file or attachment is not valid");
                }


                string EncryptionKey = "MAKV2SPBNI99212";
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

                using (var encryptedStream = new MemoryStream())
                {
                    using (Aes encryptor = Aes.Create())
                    {
                        encryptor.Key = pdb.GetBytes(32);
                        encryptor.IV = pdb.GetBytes(16);
                        using (CryptoStream cryptoStream = new CryptoStream(encryptedStream, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            using (var ms = new MemoryStream())
                            {
                                file.CopyTo(ms);
                                var fileBytes = ms.ToArray();

                                using (var originalByteStream = new MemoryStream(fileBytes))
                                {
                                    int data;
                                    while ((data = originalByteStream.ReadByte()) != -1)
                                        cryptoStream.WriteByte((byte)data);
                                }
                            }
                        }
                    }

                    var encryptedBytes = encryptedStream.ToArray();
                    Stream st = new MemoryStream(encryptedBytes);

                    using (var ftp = new FtpClient(ftpAddress, ftpUsername, ftpPassword))
                    {
                        ftp.Connect();
                        ftp.Upload(st, FolderPath + Filename, FtpRemoteExists.Overwrite, true);
                        result = "success";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }

        public static string FileDecryption(string FileName, string FileExtension)
        {
            if (FileExtension.Length > 0)
                FileExtension = FileExtension.Trim().ToLower().Replace(".", "");

            string Result;
            string EncryptionKey = "MAKV2SPBNI99212";
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            try
            {
                using (var conn = new FtpClient(ftpAddress, ftpUsername, ftpPassword))
                {
                    conn.Connect();

                    using (var istream = conn.OpenRead(FolderPath + FileName))
                    {
                        try
                        {
                            byte[] bytes;
                            using (var memoryStream = new MemoryStream())
                            {
                                istream.CopyTo(memoryStream);
                                bytes = memoryStream.ToArray();
                            }

                            using (var encryptedStream = new MemoryStream(bytes))
                            {
                                using (var decryptedStream = new MemoryStream())
                                {
                                    using (Aes encryptor = Aes.Create())
                                    {
                                        encryptor.Key = pdb.GetBytes(32);
                                        encryptor.IV = pdb.GetBytes(16);
                                        using (var decryptor = encryptor.CreateDecryptor())
                                        {
                                            using (var cryptoStream = new CryptoStream(encryptedStream, decryptor, CryptoStreamMode.Read))
                                            {
                                                int data;

                                                while ((data = cryptoStream.ReadByte()) != -1)
                                                    decryptedStream.WriteByte((byte)data);
                                            }
                                        }
                                    }

                                    decryptedStream.Position = 0;
                                    var decryptedBytes = decryptedStream.ToArray();

                                    string base64 = Convert.ToBase64String(decryptedBytes);
                                    string contenttype = "image";
                                    if (FileExtension == "pdf")
                                        contenttype = "application";
                                    Result = "data:" + contenttype + "/" + FileExtension + ";base64," + base64;
                                }
                            }
                        }
                        finally
                        {
                            istream.Close();
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                Result = "failed, " + exc.Message;
            }

            return Result;
        }

        public static byte[] FileDecryptionToByte(string FileName)
        {
            if (FileName.Contains("."))
            {
                string[] FileNames = FileName.Split(".");
                if (FileNames.Length == 2)
                    FileName = FileNames[0].Trim();
            }

            byte[] st;
            string EncryptionKey = "MAKV2SPBNI99212";
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            using (var conn = new FtpClient(ftpAddress, ftpUsername, ftpPassword))
            {
                conn.Connect();

                using (var istream = conn.OpenRead(FolderPath + FileName))
                {
                    try
                    {
                        byte[] bytes;
                        using (var memoryStream = new MemoryStream())
                        {
                            istream.CopyTo(memoryStream);
                            bytes = memoryStream.ToArray();
                        }

                        using (var encryptedStream = new MemoryStream(bytes))
                        {
                            using (var decryptedStream = new MemoryStream())
                            {
                                using (Aes encryptor = Aes.Create())
                                {
                                    encryptor.Key = pdb.GetBytes(32);
                                    encryptor.IV = pdb.GetBytes(16);
                                    using (var decryptor = encryptor.CreateDecryptor())
                                    {
                                        using (var cryptoStream = new CryptoStream(encryptedStream, decryptor, CryptoStreamMode.Read))
                                        {
                                            int data;

                                            while ((data = cryptoStream.ReadByte()) != -1)
                                                decryptedStream.WriteByte((byte)data);
                                        }
                                    }
                                }

                                decryptedStream.Position = 0;
                                st = decryptedStream.ToArray();
                                return st;
                            }
                        }
                    }
                    finally
                    {
                        istream.Close();
                    }
                }
            }

        }

        public static string GetBinaryImageEncrypted(string FileName, string FileExtension)
        {
            FileName = FileName.Replace(FileExtension, "");

            if (FileExtension.Length > 0)
                FileExtension = FileExtension.Trim().ToLower().Replace(".", "");

            string Result;
            string EncryptionKey = "MAKV2SPBNI99212";
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

            try
            {
                using (var conn = new FtpClient(ftpAddress, ftpUsername, ftpPassword))
                {
                    conn.Connect();

                    // open an read-only stream to the file
                    using (var istream = conn.OpenRead(FolderPath + FileName))
                    {
                        try
                        {
                            byte[] bytes;
                            using (var memoryStream = new MemoryStream())
                            {
                                istream.CopyTo(memoryStream);
                                bytes = memoryStream.ToArray();
                            }

                            using (var encryptedStream = new MemoryStream(bytes))
                            {
                                using (var decryptedStream = new MemoryStream())
                                {
                                    using (Aes encryptor = Aes.Create())
                                    {
                                        encryptor.Key = pdb.GetBytes(32);
                                        encryptor.IV = pdb.GetBytes(16);
                                        using (var decryptor = encryptor.CreateDecryptor())
                                        {
                                            using (var cryptoStream = new CryptoStream(encryptedStream, decryptor, CryptoStreamMode.Read))
                                            {
                                                int data;

                                                while ((data = cryptoStream.ReadByte()) != -1)
                                                    decryptedStream.WriteByte((byte)data);
                                            }
                                        }
                                    }

                                    decryptedStream.Position = 0;
                                    var decryptedBytes = decryptedStream.ToArray();
                                    string base64 = Convert.ToBase64String(decryptedBytes);
                                    string mimeType = MimeMapping.GetMimeMapping(FileName + "." + FileExtension);
                                    Result = "data:" + mimeType + ";base64," + base64;
                                }
                            }

                        }
                        finally
                        {
                            istream.Close();
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                Result = "failed, " + exc.Message;
            }

            return Result;
        }

        public static string EncryptFile(Stream file, string Filename, string Ekstension)
        {
            string result = "";
            try
            {
                Filename = Filename.Replace(Ekstension, "");

                if (Ekstension.Length > 0)
                    Ekstension = Ekstension.Trim().ToLower().Replace(".", "");

                string EncryptionKey = "MAKV2SPBNI99212";
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

                using (var encryptedStream = new MemoryStream())
                {
                    using (Aes encryptor = Aes.Create())
                    {
                        encryptor.Key = pdb.GetBytes(32);
                        encryptor.IV = pdb.GetBytes(16);
                        using (CryptoStream cryptoStream = new CryptoStream(encryptedStream, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            using (var ms = new MemoryStream())
                            {
                                file.CopyTo(ms);
                                var fileBytes = ms.ToArray();

                                using (var originalByteStream = new MemoryStream(fileBytes))
                                {
                                    int data;
                                    while ((data = originalByteStream.ReadByte()) != -1)
                                        cryptoStream.WriteByte((byte)data);
                                }
                            }
                        }
                    }

                    var encryptedBytes = encryptedStream.ToArray();
                    Stream st = new MemoryStream(encryptedBytes);

                    using (var ftp = new FtpClient(ftpAddress, ftpUsername, ftpPassword))
                    {
                        ftp.Connect();
                        ftp.Upload(st, FolderPath + Filename, FtpRemoteExists.Overwrite, true);
                        result = "success";
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }
    }
}
