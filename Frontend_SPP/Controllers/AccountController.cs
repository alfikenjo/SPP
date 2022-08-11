using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Frontend_SPP.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Frontend_SPP.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Text;
using Microsoft.AspNetCore.Identity;
using ClosedXML.Extensions;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Ganss.XSS;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Localization;

namespace Frontend_SPP.Controllers
{

    public class AccountController : Controller
    {
        private HtmlSanitizer sani = new HtmlSanitizer();

        public IActionResult Signout()
        {
            var culture = string.IsNullOrEmpty(HttpContext.Session.GetString("culture")) ? "id" : HttpContext.Session.GetString("culture").ToLower();
            ViewData["Fullname"] = null;
            ViewData["Roles"] = null;
            ViewData["Unit"] = null;
            HttpContext.Session.Clear();

            Helper.RecordAuditTrail(StringCipher.Decrypt(HttpContext.Session.GetString("Email")), "Website SPP", "Log Out", "", "Log out", "");

            return RedirectToAction("Index", "Home", new { culture = culture });
        }

        public IActionResult RenewPassword(string ID)
        {
            ViewBag.isValid = false;
            DataTable DT = mssql.GetDataTable("SELECT A.ID, B.UserID, B.Username, B.Fullname, LEFT(A.Status, 8) [V_Status] FROM tblT_User_Password_Forgotten A JOIN tblM_User B ON A.UserID = B.UserID WHERE A.ID = '" + ID + "'");
            if (DT.Rows.Count == 1)
                ViewBag.isValid = true;

            if (!string.IsNullOrEmpty(StringCipher.Decrypt(HttpContext.Session.GetString("Email"))))
                return RedirectToAction("Index", "Home");

            ViewData["Fullname"] = null;
            ViewData["Roles"] = null;
            ViewData["Unit"] = null;

            HttpContext.Session.Clear();

            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var culture = rqf.RequestCulture.Culture.ToString().ToUpper();
            if ((string.IsNullOrEmpty(culture) || culture == "IN"))
                culture = "ID";

            HttpContext.Session.SetString("culture", culture.ToLower());
            return View();
        }


        #region MyProfile

        public IActionResult MyProfile()
        {
            if (string.IsNullOrEmpty(StringCipher.Decrypt(HttpContext.Session.GetString("Email"))))
                return RedirectToAction("Index", "Home");

            DataRow dr_Login = mssql.GetDataRow("SELECT COUNT(*) [Jumlah] FROM tblT_User_Login WHERE UserID = '" + StringCipher.Decrypt(HttpContext.Session.GetString("UserID")) + "' AND ID = '" + StringCipher.Decrypt(HttpContext.Session.GetString("SessionID")) + "' AND isActive = 1");
            if (int.Parse(dr_Login["Jumlah"].ToString()) == 0)
                return RedirectToAction("Index", "Home");

            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var culture = rqf.RequestCulture.Culture.ToString().ToUpper();
            if ((string.IsNullOrEmpty(culture) || culture == "IN") && string.IsNullOrEmpty(HttpContext.Session.GetString("culture")))
                culture = "ID";
            else
                HttpContext.Session.SetString("culture", culture.ToLower());

            ViewData["CurrentControllerName"] = "";
            ViewData["CurrentActionName"] = "My Profile";
            return View();
        }

        [HttpPost]
        public IActionResult GetMyProfile()
        {
            try
            {
                if (string.IsNullOrEmpty(StringCipher.Decrypt(HttpContext.Session.GetString("Email"))))
                    throw new Exception("Invalid Authorization|window.location='../'");

                DataRow dr_Login = mssql.GetDataRow("SELECT COUNT(*) [Jumlah] FROM tblT_User_Login WHERE UserID = '" + StringCipher.Decrypt(HttpContext.Session.GetString("UserID")) + "' AND ID = '" + StringCipher.Decrypt(HttpContext.Session.GetString("SessionID")) + "' AND isActive = 1");
                if (int.Parse(dr_Login["Jumlah"].ToString()) == 0)
                    return RedirectToAction("Index", "Home");

                string UserID = StringCipher.Decrypt(HttpContext.Session.GetString("UserID"));
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@UserID", UserID));
                DataTable dt = mssql.GetDataTable("sp_Get_User_By_UserID", param);

                List<tblM_User> MainData = new List<tblM_User>();
                MainData = mssql.ConvertDataTable<tblM_User>(dt);

                for (int i = 0; i < MainData.Count; i++)
                {
                    MainData[i].UserID = StringCipher.Encrypt(MainData[i].UserID) + "|" + HttpContext.Session.GetString("SessionID");

                    var Img = MainData[i].Img;
                    if (!string.IsNullOrEmpty(Img))
                    {
                        MainData[i].Img = Helper.GetBinaryImageEncrypted(MainData[i].Img, MainData[i].Ekstension);
                    }
                }

                return Json(new { Error = false, Message = MainData });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public IActionResult UpdateMyProfile([FromForm] tblM_User Model)
        {
            try
            {
                if (string.IsNullOrEmpty(StringCipher.Decrypt(HttpContext.Session.GetString("Email"))))
                    throw new Exception("Invalid Authorization|window.location='../'");

                DataRow dr_Login = mssql.GetDataRow("SELECT COUNT(*) [Jumlah] FROM tblT_User_Login WHERE UserID = '" + StringCipher.Decrypt(HttpContext.Session.GetString("UserID")) + "' AND ID = '" + StringCipher.Decrypt(HttpContext.Session.GetString("SessionID")) + "' AND isActive = 1");
                if (int.Parse(dr_Login["Jumlah"].ToString()) == 0)
                    return RedirectToAction("Index", "Home");

                string FotoFilename = "", FotoFileExtension = "";
                #region Foto
                if (Model.Foto != null)
                {
                    string OriginalFilename = Model.Foto.FileName.ToLower();
                    if (OriginalFilename.ToLower().Contains("php") || OriginalFilename.ToLower().Contains("aspx") || OriginalFilename.ToLower().Contains("exe") || OriginalFilename.ToLower().Contains("dll") || OriginalFilename.ToLower().Contains("js") || OriginalFilename.ToLower().Contains("css") || OriginalFilename.ToLower().Contains("html"))
                        throw new Exception("Failed, Permission denied while uploading the file or attachment");

                    List<IFormFile> files = new List<IFormFile>();
                    files.Add(Model.Foto);
                    foreach (IFormFile file in files)
                    {
                        if (file.Length > 0)
                        {
                            FotoFileExtension = System.IO.Path.GetExtension(file.FileName).Trim();
                            FotoFilename = StringCipher.Decrypt(HttpContext.Session.GetString("UserID"));

                            if (FotoFileExtension.ToLower() == ".jpg" || FotoFileExtension.ToLower() == ".png" || FotoFileExtension.ToLower() == ".jpeg")
                            {
                                var filePath = Path.GetTempFileName();
                                using (var stream = System.IO.File.Create(filePath))
                                {
                                    file.CopyTo(stream);

                                    string upload = Helper.UploadFTPWithEcryption(file, FotoFilename, FotoFileExtension);
                                    if (upload != "success")
                                    {
                                        FotoFilename = "";
                                        FotoFileExtension = "";
                                        throw new Exception(upload);
                                    }
                                    else
                                    {
                                        string imgPath = Helper.GetBinaryImageEncrypted(FotoFilename, FotoFileExtension);
                                        HttpContext.Session.SetString("img", imgPath);
                                    }
                                }
                            }
                            else
                            {
                                throw new Exception("Failed, Image not valid");
                            }
                        }
                        else
                        {
                            throw new Exception("Failed, Permission denied while uploading the file or attachment");
                        }
                    }
                }
                #endregion Foto

                #region Save User

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@UserID", StringCipher.Decrypt(HttpContext.Session.GetString("UserID"))));
                param.Add(new SqlParameter("@Fullname", sani.Sanitize(Model.Fullname)));
                param.Add(new SqlParameter("@Mobile", sani.Sanitize(Model.Mobile)));
                param.Add(new SqlParameter("@Address", sani.Sanitize(Model.Address)));
                param.Add(new SqlParameter("@Gender", sani.Sanitize(Model.Gender)));
                param.Add(new SqlParameter("@NIP", sani.Sanitize(Model.NIP)));
                param.Add(new SqlParameter("@Jabatan", sani.Sanitize(Model.Jabatan)));
                param.Add(new SqlParameter("@Img", FotoFilename));
                param.Add(new SqlParameter("@CreatedBy", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));

                mssql.ExecuteNonQuery("spUpdateMyProfile", param);
                #endregion Save User

                if (string.IsNullOrEmpty(sani.Sanitize(Model.Fullname)))
                    HttpContext.Session.SetString("fn", StringCipher.Decrypt(HttpContext.Session.GetString("Email")));
                else
                    HttpContext.Session.SetString("fn", sani.Sanitize(Model.Fullname));

                return Json(new { Error = false, Message = "" });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult DeleteFotoProfil(tblM_User Model)
        {
            try
            {
                if (string.IsNullOrEmpty(StringCipher.Decrypt(HttpContext.Session.GetString("Email"))))
                    throw new Exception("Invalid Authorization|window.location='../'");

                DataRow dr_Login = mssql.GetDataRow("SELECT COUNT(*) [Jumlah] FROM tblT_User_Login WHERE UserID = '" + StringCipher.Decrypt(HttpContext.Session.GetString("UserID")) + "' AND ID = '" + StringCipher.Decrypt(HttpContext.Session.GetString("SessionID")) + "' AND isActive = 1");
                if (int.Parse(dr_Login["Jumlah"].ToString()) == 0)
                    return RedirectToAction("Index", "Home");

                string UserID = StringCipher.Decrypt(HttpContext.Session.GetString("UserID"));
                DataRow dr = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM tblM_User WHERE UserID = '" + UserID + "' AND Img IS NOT NULL");
                int exist = int.Parse(dr["Count"].ToString());
                if (exist == 1)
                {
                    mssql.ExecuteNonQuery("UPDATE tblM_User SET Img = NULL WHERE UserID = '" + UserID + "'");
                    HttpContext.Session.SetString("img", "../image/default_avatar.png");

                }
                return Json(new { Error = false, Message = exist });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult UpdateEmail(tblM_User Model)
        {
            try
            {
                if (string.IsNullOrEmpty(StringCipher.Decrypt(HttpContext.Session.GetString("Email"))))
                    throw new Exception("Invalid Authorization|window.location='../'");

                DataRow dr_Login = mssql.GetDataRow("SELECT COUNT(*) [Jumlah] FROM tblT_User_Login WHERE UserID = '" + StringCipher.Decrypt(HttpContext.Session.GetString("UserID")) + "' AND ID = '" + StringCipher.Decrypt(HttpContext.Session.GetString("SessionID")) + "' AND isActive = 1");
                if (int.Parse(dr_Login["Jumlah"].ToString()) == 0)
                    return RedirectToAction("Index", "Home");

                string OldMobile = "";
                DataTable dtOld = mssql.GetDataTable("SELECT Mobile FROM tblM_User WHERE UserID = '" + StringCipher.Decrypt(HttpContext.Session.GetString("UserID")) + "' AND Mobile_Verification = 1");
                if (dtOld.Rows.Count == 1)
                    OldMobile = dtOld.Rows[0]["Mobile"].ToString();

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@UserID", StringCipher.Decrypt(HttpContext.Session.GetString("UserID"))));
                param.Add(new SqlParameter("@Mobile", sani.Sanitize(Model.Mobile)));
                param.Add(new SqlParameter("@CreatedBy", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));

                mssql.ExecuteNonQuery("spUpdateAccount", param);

                string Mobile = sani.Sanitize(Model.Mobile);
                string PhoneChanged = "";
                if (Mobile.Length >= 8 && Mobile.Length <= 15 && Mobile != OldMobile)
                {
                    int OTPTimes = 0;
                    if (!string.IsNullOrEmpty(HttpContext.Session.GetString("OTPTimes")))
                        OTPTimes = int.Parse(HttpContext.Session.GetString("OTPTimes"));

                    string UserID = StringCipher.Decrypt(HttpContext.Session.GetString("UserID"));
                    string New_OTP_ID = Guid.NewGuid().ToString();

                    Random generator = new Random();
                    string OTP = generator.Next(0, 1000000).ToString("D6");

                    mssql.ExecuteNonQuery("DELETE FROM tblT_OTP WHERE UserID = '" + UserID + "'");
                    mssql.ExecuteNonQuery("INSERT INTO tblT_OTP (ID, UserID, Mobile, OTP, Status) VALUES ('" + New_OTP_ID + "', '" + UserID + "', '" + Mobile + "', '" + OTP + "', 'Not verified')");

                    //SEND OTP VIA SMS GATEWAY
                    var culture = string.IsNullOrEmpty(HttpContext.Session.GetString("culture")) ? "id" : HttpContext.Session.GetString("culture").ToLower();
                    string SMS_Body = "Kode OTP : " + OTP + "\n\nHubungi Administrator SPP bila Anda tidak merasa meminta OTP ini.";
                    if (culture == "en")
                        SMS_Body = "Your OTP : " + OTP + "\n\nContact the WBS Helpdesk if you don't think you're asking for this OTP.";

                    string SMS_Respon = Helper.SendSMSSingle(SMS_Body, Mobile);
                    if (SMS_Respon == "Success")
                    {
                        mssql.ExecuteNonQuery("UPDATE tblT_OTP SET SMS_Status = 'Sent on " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.000") + "' WHERE ID = '" + New_OTP_ID + "'");
                        OTPTimes = OTPTimes + 1;
                        HttpContext.Session.SetString("OTPTimes", OTPTimes.ToString());
                        HttpContext.Session.SetString("ReqTimes", DateTime.Now.ToString());
                        if (OTPTimes == 3)
                        {
                            HttpContext.Session.Remove("OTPTimes");
                            HttpContext.Session.Remove("ReqTimes");
                            HttpContext.Session.SetString("OTPLock", DateTime.Now.ToString());
                        }
                        else
                            HttpContext.Session.Remove("OTPLock");
                    }

                    else
                        throw new Exception(SMS_Respon);

                    PhoneChanged = Mobile;
                }

                return Json(new { Error = false, Message = PhoneChanged });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult ChangePassword(tblM_User Model)
        {
            try
            {
                if (string.IsNullOrEmpty(StringCipher.Decrypt(HttpContext.Session.GetString("Email"))))
                    throw new Exception("Invalid Authorization|window.location='../'");

                DataRow dr_Login = mssql.GetDataRow("SELECT COUNT(*) [Jumlah] FROM tblT_User_Login WHERE UserID = '" + StringCipher.Decrypt(HttpContext.Session.GetString("UserID")) + "' AND ID = '" + StringCipher.Decrypt(HttpContext.Session.GetString("SessionID")) + "' AND isActive = 1");
                if (int.Parse(dr_Login["Jumlah"].ToString()) == 0)
                    return RedirectToAction("Index", "Home");

                string CurrentPassword = Model.CurrentPassword;
                string NewPassword = sani.Sanitize(Model.NewPassword);
                string NewPasswordVerifiy = sani.Sanitize(Model.NewPasswordVerifiy);
                string Username = StringCipher.Decrypt(HttpContext.Session.GetString("Email"));
                string UserID = StringCipher.Decrypt(HttpContext.Session.GetString("UserID"));

                var culture = string.IsNullOrEmpty(HttpContext.Session.GetString("culture")) ? "id" : HttpContext.Session.GetString("culture").ToLower();
                if (NewPassword != NewPasswordVerifiy)
                    if (culture == "en")
                        throw new Exception("Password does not match, please check again");
                    else
                        throw new Exception("Password baru tidak cocok, mohon periksa kembali");

                bool containsInt = NewPasswordVerifiy.Any(char.IsDigit);
                bool containsLower = NewPasswordVerifiy.Any(char.IsLower);
                bool containsUpper = NewPasswordVerifiy.Any(char.IsUpper);
                bool containsMinLen = NewPasswordVerifiy.Trim().Length >= 8;

                bool n_containsInt = NewPassword.Any(char.IsDigit);
                bool n_containsLower = NewPassword.Any(char.IsLower);
                bool n_containsUpper = NewPassword.Any(char.IsUpper);
                bool n_containsMinLen = NewPassword.Trim().Length >= 8;

                if (!containsInt || !n_containsInt)
                    throw new Exception("Password must contain atleast a number, please check again");
                if (!containsLower || !n_containsLower)
                    throw new Exception("Password must contain atleast a lowercase letter, please check again");
                if (!containsUpper || !n_containsUpper)
                    throw new Exception("Password must contain atleast a CAPITAL (uppercase) letter, please check again");
                if (!containsMinLen || !n_containsMinLen)
                    throw new Exception("Password is minimun 8 characters, please check again");

                string ProvidedPassword = CurrentPassword;
                DataTable DT = mssql.GetDataTable("SELECT * FROM tblM_User WHERE UserID = '" + UserID + "' AND ISNULL(isActive, 0) = 1");
                if (DT.Rows.Count == 1)
                {
                    if (!PassHash.VerifyHashedPassword(DT.Rows[0]["PasswordHash"].ToString(), ProvidedPassword))
                    {
                        throw new Exception("Invalid Current Password");
                    }

                    string HashPassword = PassHash.HashPassword(NewPasswordVerifiy);

                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(new SqlParameter("@UserID", UserID));
                    param.Add(new SqlParameter("@PasswordHash", HashPassword));
                    param.Add(new SqlParameter("@CreatedBy", HttpContext.Session.GetString("Email")));

                    mssql.ExecuteNonQuery("spChangePassword", param);
                }
                else
                    throw new Exception("User not found or inactive");

                return Json(new { Error = false, Message = "" });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }
        #endregion MyProfile

        //[HttpPost]
        //public JsonResult GetUserFullname(string Prefix)
        //{
        //    List<SqlParameter> param = new List<SqlParameter>();
        //    param.Add(new SqlParameter("@UserID", StringCipher.Decrypt(HttpContext.Session.GetString("UserID"))));
        //    param.Add(new SqlParameter("@Prefix", Prefix));
        //    DataTable dt = mssql.GetDataTable("sp_GetUserFullname", param);

        //    List<tblM_User> MainData = new List<tblM_User>();
        //    MainData = mssql.ConvertDataTable<tblM_User>(dt);
        //    return Json(new { Error = false, Message = MainData });
        //}

        //[HttpPost]
        //public JsonResult GetUserInformation(string Fullname)
        //{
        //    List<SqlParameter> param = new List<SqlParameter>();
        //    param.Add(new SqlParameter("@UserID", StringCipher.Decrypt(HttpContext.Session.GetString("UserID"))));
        //    param.Add(new SqlParameter("@Fullname", Fullname));
        //    DataTable dt = mssql.GetDataTable("sp_GetUserInformation", param);

        //    List<tblM_User> MainData = new List<tblM_User>();
        //    MainData = mssql.ConvertDataTable<tblM_User>(dt);
        //    return Json(new { Error = false, Message = MainData });
        //}

        [HttpPost]
        public IActionResult Register(tblM_User Model)
        {
            try
            {
                string Fullname = sani.Sanitize(Model.Fullname);
                string Email = sani.Sanitize(Model.Email);
                string Mobile = sani.Sanitize(Model.Mobile);
                string Register_Password = sani.Sanitize(Model.Register_Password);
                string Register_Password_Reentered = sani.Sanitize(Model.Register_Password_Reentered);
                string captcha = sani.Sanitize(Model.captcha);

                if (Register_Password != Register_Password_Reentered)
                    throw new Exception("Re-entered Password  did not match");

                bool containsInt = Register_Password.Any(char.IsDigit);
                bool containsLower = Register_Password.Any(char.IsLower);
                bool containsUpper = Register_Password.Any(char.IsUpper);
                bool containsMinLen = Register_Password.Trim().Length >= 8;

                bool n_containsInt = Register_Password_Reentered.Any(char.IsDigit);
                bool n_containsLower = Register_Password_Reentered.Any(char.IsLower);
                bool n_containsUpper = Register_Password_Reentered.Any(char.IsUpper);
                bool n_containsMinLen = Register_Password_Reentered.Trim().Length >= 8;

                if (!containsInt || !n_containsInt)
                    throw new Exception("Password must contain atleast a number, please check again");
                if (!containsLower || !n_containsLower)
                    throw new Exception("Password must contain atleast a lowercase letter, please check again");
                if (!containsUpper || !n_containsUpper)
                    throw new Exception("Password must contain atleast a CAPITAL (uppercase) letter, please check again");
                if (!containsMinLen || !n_containsMinLen)
                    throw new Exception("Password is minimun 8 characters, please check again");

                string current_captcha = HttpContext.Session.GetString("captcha");
                if (captcha != current_captcha)
                    throw new Exception("Invalid Captcha");

                var culture = string.IsNullOrEmpty(HttpContext.Session.GetString("culture")) ? "id" : HttpContext.Session.GetString("culture").ToLower();
                DataRow dr_IsExist = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM tblM_User WHERE Email = '" + Email + "' AND ISNULL(isActive, 0) = 1");
                if (int.Parse(dr_IsExist["Count"].ToString()) > 0)
                {
                    if (culture == "en")
                        throw new Exception("The email has been registered, please login using your account");
                    else
                        throw new Exception("Email sudah terdaftar sebelumnya, silahkan login menggunakan akun yang Anda miliki");
                }


                if (!string.IsNullOrEmpty(Mobile))
                {
                    DataRow dr_IsExist_Mobile = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM tblM_User WHERE Mobile = '" + Mobile + "' AND ISNULL(isActive, 0) = 1");
                    if (int.Parse(dr_IsExist_Mobile["Count"].ToString()) > 0)
                        if (culture == "en")
                            throw new Exception("The mobile number has been previously registered, please login using your account");
                        else
                            throw new Exception("Nomor Handphone sudah terdaftar sebelumnya, silahkan login menggunakan akun yang Anda miliki");
                }

                string HashPassword = PassHash.HashPassword(Register_Password);
                string New_User_Verification_ID = Guid.NewGuid().ToString();
                Model.UserID = Guid.NewGuid().ToString();
                Model.isActive = 0;

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@UserID", Model.UserID));
                param.Add(new SqlParameter("@New_User_Verification_ID", New_User_Verification_ID));
                param.Add(new SqlParameter("@Fullname", sani.Sanitize(Fullname)));
                param.Add(new SqlParameter("@Email", sani.Sanitize(Email)));
                param.Add(new SqlParameter("@Mobile", sani.Sanitize(Mobile)));
                param.Add(new SqlParameter("@PasswordHash", HashPassword));
                param.Add(new SqlParameter("@isActive", Model.isActive));
                param.Add(new SqlParameter("@CreatedBy", sani.Sanitize(Email)));

                mssql.ExecuteNonQuery("sp_Register_User", param);

                string Subject = "Aktivasi akun SPP - PTSMI";
                Helper.SendMail(Email, Subject, MailComposer.compose_mail_body_register(Email, New_User_Verification_ID));
                mssql.ExecuteNonQuery("UPDATE tblT_New_User_Verification SET Verification_Mail_Status = 'Sent on " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.000") + "' WHERE ID = '" + New_User_Verification_ID + "'");

                return Json(new { Error = false, Message = "" });

            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult VerifikasiByEmail(string ID)
        {
            try
            {
                string VerifikasiID = sani.Sanitize(ID);
                string Hasil = "";
                var culture = string.IsNullOrEmpty(HttpContext.Session.GetString("culture")) ? "id" : HttpContext.Session.GetString("culture").ToLower();

                DataTable DT = mssql.GetDataTable("SELECT * FROM vw_User_Verification_Status WHERE ID = '" + VerifikasiID + "'");
                if (DT.Rows.Count == 1)
                {
                    if (DT.Rows[0]["V_Status"].ToString() == "Verified" || DT.Rows[0]["Link_Status"].ToString() == "Expired")
                    {
                        if (culture == "en")
                            throw new Exception("The verification link has expired");
                        else
                            throw new Exception("Link verifikasi sudah kadaluarsa!");
                    }
                    else
                    {
                        string UserID = DT.Rows[0]["UserID"].ToString();
                        mssql.ExecuteNonQuery("UPDATE tblM_User SET isActive = 1, Mail_Verification = 1, LastPasswordChanged = GETDATE(), UpdatedBy = Email, UpdatedOn = GETDATE() WHERE isActive = 0 AND UserID = '" + UserID + "'");
                        mssql.ExecuteNonQuery("UPDATE tblT_New_User_Verification SET Status = 'Verified On " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.000") + "' WHERE ID = '" + VerifikasiID + "'");
                        string Role = "Pelapor";
                        DataTable dtRole = mssql.GetDataTable("SELECT ID FROM tblM_Role WHERE Name = '" + Role + "'");
                        if (dtRole.Rows.Count == 1)
                            mssql.ExecuteNonQuery("INSERT INTO tblT_UserInRole (UserID, RoleID, CreatedBy) VALUES ('" + UserID + "', '" + dtRole.Rows[0]["ID"] + "', 'Online-Register')");

                        Hasil = "Berhasil";
                    }
                }
                else
                {
                    if (culture == "en")
                        throw new Exception("The link invalid or has expired");
                    else
                        throw new Exception("URL/Link verifikasi akun tidak valid atau sudah kadaluarsa");
                }
                return Json(new { Error = false, Message = Hasil });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Login(tblM_User Model)
        {
            int failedAttemp = 0;
            string msg = "";

            try
            {

                string Email = sani.Sanitize(Model.Email);
                string Password = sani.Sanitize(Model.PasswordHash);

                if (HttpContext.Session.GetInt32("failedAttemp") != null)
                    failedAttemp = int.Parse(HttpContext.Session.GetInt32("failedAttemp").ToString());

                if (string.IsNullOrEmpty(Email))
                {
                    msg += "Email cannot be empty;";
                    failedAttemp++;
                    HttpContext.Session.SetInt32("failedAttemp", failedAttemp);
                    throw new Exception(msg);
                }

                if (Password == null)
                {
                    msg += "Password cannot be empty;";
                    failedAttemp++;
                    HttpContext.Session.SetInt32("failedAttemp", failedAttemp);
                    throw new Exception(msg);
                }

                if (failedAttemp == 3)
                {
                    failedAttemp = 0;
                    HttpContext.Session.Remove("failedAttemp");

                    string captcha = sani.Sanitize(Model.captcha);
                    string current_captcha = HttpContext.Session.GetString("captcha");
                    if (captcha != current_captcha)
                    {
                        failedAttemp = 3;
                        HttpContext.Session.SetInt32("failedAttemp", failedAttemp);
                        msg = "Invalid Captcha";
                        throw new Exception(msg);
                    }

                }

                string ProvidedPassword = Password;
                DataRow drUserNonAktif = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM tblM_User WHERE Email = '" + Email + "' AND ISNULL(isActive, 0) = 0");
                int UserNonAktif = int.Parse(drUserNonAktif["Count"].ToString());
                DataTable DT = mssql.GetDataTable("SELECT * FROM tblM_User WHERE Email = '" + Email + "' AND ISNULL(isActive, 0) = 1");
                if (DT.Rows.Count == 1)
                {
                    if (!PassHash.VerifyHashedPassword(DT.Rows[0]["PasswordHash"].ToString(), ProvidedPassword))
                    {
                        failedAttemp++;
                        HttpContext.Session.SetInt32("failedAttemp", failedAttemp);
                        msg = "Wrong Password";
                        throw new Exception(msg);
                    }

                    string SessionID = Guid.NewGuid().ToString();
                    mssql.ExecuteNonQuery("UPDATE tblT_User_Login SET isActive = 0, Last_Logout_DateTime = GETDATE() WHERE isActive = 1 AND UserID = '" + DT.Rows[0]["UserID"].ToString() + "'");
                    mssql.ExecuteNonQuery("INSERT INTO tblT_User_Login (ID, UserID, isActive, Last_Login_DateTime) VALUES ('" + SessionID + "', '" + DT.Rows[0]["UserID"].ToString() + "', 1, GETDATE())");

                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(new SqlParameter("@UserID", DT.Rows[0]["UserID"].ToString()));
                    DataTable dt_User = mssql.GetDataTable("sp_Get_User_By_UserID", param);

                    HttpContext.Session.SetString("UserID", StringCipher.Encrypt(DT.Rows[0]["UserID"].ToString()));
                    HttpContext.Session.SetString("Username", StringCipher.Encrypt(Email));
                    HttpContext.Session.SetString("Email", StringCipher.Encrypt(Email));
                    HttpContext.Session.SetString("fn", !string.IsNullOrEmpty(DT.Rows[0]["Fullname"].ToString()) ? DT.Rows[0]["Fullname"].ToString() : Email);
                    HttpContext.Session.SetString("fr", dt_User.Rows[0]["Roles"].ToString());
                    HttpContext.Session.SetString("SessionID", StringCipher.Encrypt(SessionID));

                    if (string.IsNullOrEmpty(dt_User.Rows[0]["Img"].ToString()))
                        HttpContext.Session.SetString("img", "../image/default_avatar.png");
                    else
                    {
                        string imgPath = Helper.GetBinaryImageEncrypted(dt_User.Rows[0]["Img"].ToString(), dt_User.Rows[0]["Ekstension"].ToString());
                        HttpContext.Session.SetString("img", imgPath);
                    }

                    ViewData["Fullname"] = DT.Rows[0]["Fullname"].ToString();
                    ViewData["Roles"] = dt_User.Rows[0]["Roles"].ToString();

                    Helper.RecordAuditTrail(Email, "Web Portal SPP", "Login", "", "LOGIN", "");

                    failedAttemp = 0;
                    HttpContext.Session.Remove("failedAttemp");
                    HttpContext.Session.Remove("captcha");

                    return Json(new { Error = false, Message = "", culture = HttpContext.Session.GetString("culture") });
                }
                else if (UserNonAktif == 1)
                {
                    failedAttemp++;
                    HttpContext.Session.SetInt32("failedAttemp", failedAttemp);
                    msg = "You haven't activated your account, please check your email to get the previous activation link";
                    throw new Exception(msg);
                }
                else
                {
                    failedAttemp++;
                    HttpContext.Session.SetInt32("failedAttemp", failedAttemp);
                    msg = "Your Email not found or not registered";
                    throw new Exception(msg);
                }


            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message, failedAttemp = failedAttemp });
            }
        }

        [HttpPost]
        public IActionResult SubmitForgotPasswordByEmail(tblM_User Model)
        {
            try
            {
                string Email = sani.Sanitize(Model.Email);
                var culture = string.IsNullOrEmpty(HttpContext.Session.GetString("culture")) ? "id" : HttpContext.Session.GetString("culture").ToLower();
                DataTable dtUser = mssql.GetDataTable("SELECT UserID, Email, Fullname FROM tblM_User WHERE Email = '" + Email + "' AND ISNULL(isActive, 0) = 1 AND ISNULL(Mail_Verification, 0) = 1");
                if (dtUser.Rows.Count != 1)
                {
                    if (culture == "en")
                        throw new Exception("Email not found or inactive, you can use an email address that is already active");
                    else
                        throw new Exception("Email tidak ditemukan atau non-aktif, Anda dapat menggunakan alamat email yang sudah aktif pada aplikasi SPP PT SMI");
                }


                string UserID = dtUser.Rows[0]["UserID"].ToString();
                string New_User_Password_Forgotten_ID = Guid.NewGuid().ToString();

                mssql.ExecuteNonQuery("INSERT INTO tblT_User_Password_Forgotten (ID, UserID, Email, Status) VALUES ('" + New_User_Password_Forgotten_ID + "', '" + UserID + "', '" + Email + "', 'Not verified')");

                string Subject = "Verifikasi Lupa Password SPP - PTSMI";
                Helper.SendMail(Email, Subject, MailComposer.compose_mail_body_password_reset(New_User_Password_Forgotten_ID));
                mssql.ExecuteNonQuery("UPDATE tblT_User_Password_Forgotten SET Mail_Status = 'Sent on " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.000") + "' WHERE ID = '" + New_User_Password_Forgotten_ID + "'");

                return Json(new { Error = false, Message = "" });

            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult SubmitForgotPasswordByHP(tblM_User Model)
        {
            try
            {
                int OTPTimes = 0;
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("OTPTimes")))
                    OTPTimes = int.Parse(HttpContext.Session.GetString("OTPTimes"));

                string Mobile = sani.Sanitize(Model.Mobile);
                var culture = string.IsNullOrEmpty(HttpContext.Session.GetString("culture")) ? "id" : HttpContext.Session.GetString("culture").ToLower();
                DataTable dtUser = mssql.GetDataTable("SELECT UserID, Email, Fullname FROM tblM_User WHERE Mobile = '" + Mobile + "' AND ISNULL(isActive, 0) = 1 AND ISNULL(Mobile_Verification, 0) = 1");
                if (dtUser.Rows.Count == 0)
                {
                    if (culture == "en")
                        throw new Exception("Mobile number is not found or inactive, you can use an active mobile number or email address");
                    else
                        throw new Exception("Nomor handphone tidak ditemukan atau non-aktif, Anda dapat menggunakan nomor handphone atau alamat email yang sudah aktif pada aplikasi SPP PT SMI");
                }

                else if (dtUser.Rows.Count > 1)
                {
                    if (culture == "en")
                        throw new Exception("The mobile phone number is detected to be used by more than 1 account, please contact the PT SMI WBS helpdesk");
                    else
                        throw new Exception("Sistem mendeteksi Nomor Handphone ini digunakan oleh lebih dari 1 akun, silahkan menghubungi layanan helpdesk SPP PT SMI");
                }

                if (HttpContext.Session.GetString("ReqTimes") != null)
                {
                    DataTable DTreq = mssql.GetDataTable("SELECT [Request_OTP],[Submit_OTP] FROM [TblM_Config]");
                    int ReqOTP = Convert.ToInt32(DTreq.Rows[0]["Request_OTP"].ToString());
                    int submitOTP = Convert.ToInt32(DTreq.Rows[0]["Submit_OTP"].ToString());
                    DateTime current = DateTime.Now;
                    DateTime Reqlocked = Convert.ToDateTime(HttpContext.Session.GetString("ReqTimes").ToString()).AddSeconds(ReqOTP);
                    string ReqLockedUntil = Reqlocked.ToString("HH:mm:ss");
                    if (current < Reqlocked)
                        if (culture == "en")
                            throw new Exception("Sorry, please wait for the next " + ReqOTP + " second (until " + ReqLockedUntil + ") to using OTP");
                        else
                            throw new Exception("Maaf,  silahkan menunggu selama " + ReqOTP + " detik kedepan (hingga " + ReqLockedUntil + ") untuk dapat menggunakan OTP");


                }

                if (HttpContext.Session.GetString("OTPLock") != null)
                {
                    DataTable DTreq = mssql.GetDataTable("SELECT [Request_OTP],[Submit_OTP] FROM [TblM_Config]");
                    int ReqOTP = Convert.ToInt32(DTreq.Rows[0]["Request_OTP"].ToString());
                    int submitOTP = Convert.ToInt32(DTreq.Rows[0]["Submit_OTP"].ToString());
                    DateTime current = DateTime.Now;
                    DateTime locked = Convert.ToDateTime(HttpContext.Session.GetString("OTPLock").ToString()).AddMinutes(submitOTP);
                    string LockedUntil = locked.ToString("HH:mm:ss");

                    if (current < locked)
                        if (culture == "en")
                            throw new Exception("Sorry, you have used OTP for 3 (three) times, please wait for the next " + submitOTP + " minutes (until " + LockedUntil + ") to using OTP");
                        else
                            throw new Exception("Maaf, Anda sudah menggunakan OTP sebanyak 3 (tiga) kali, silahkan menunggu selama " + submitOTP + " menit kedepan (hingga " + LockedUntil + ") untuk dapat menggunakan OTP");
                }

                string UserID = dtUser.Rows[0]["UserID"].ToString();
                string New_User_Password_Forgotten_ID = Guid.NewGuid().ToString();

                Random generator = new Random();
                string OTP = generator.Next(0, 1000000).ToString("D6");

                mssql.ExecuteNonQuery("DELETE FROM tblT_User_Password_Forgotten WHERE UserID = '" + UserID + "'");
                mssql.ExecuteNonQuery("INSERT INTO tblT_User_Password_Forgotten (ID, UserID, Email, Remarks, Status) VALUES ('" + New_User_Password_Forgotten_ID + "', '" + UserID + "', '" + Mobile + "', '" + OTP + "', 'Not verified')");

                //SEND OTP VIA SMS GATEWAY
                string SMS_Body = "Kode OTP : " + OTP + "\n\nHubungi Administrator SPP bila Anda tidak merasa meminta OTP ini.";
                if (culture == "en")
                    SMS_Body = "Your OTP : " + OTP + "\n\nContact the WBS Helpdesk if you don't think you're asking for this OTP.";


                string SMS_Respon = Helper.SendSMSSingle(SMS_Body, Mobile);
                if (SMS_Respon == "Success")
                {
                    mssql.ExecuteNonQuery("UPDATE tblT_User_Password_Forgotten SET Mail_Status = 'Sent on " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.000") + "' WHERE ID = '" + New_User_Password_Forgotten_ID + "'");
                    OTPTimes = OTPTimes + 1;
                    HttpContext.Session.SetString("OTPTimes", OTPTimes.ToString());
                    HttpContext.Session.SetString("ReqTimes", DateTime.Now.ToString());
                    if (OTPTimes == 3)
                    {
                        HttpContext.Session.Remove("OTPTimes");
                        HttpContext.Session.Remove("ReqTimes");
                        HttpContext.Session.SetString("OTPLock", DateTime.Now.ToString());
                    }
                    else
                        HttpContext.Session.Remove("OTPLock");
                }

                else
                    throw new Exception(SMS_Respon);

                return Json(new { Error = false, Message = New_User_Password_Forgotten_ID });

            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult SubmitOTP(tblM_User Model)
        {
            try
            {
                var culture = string.IsNullOrEmpty(HttpContext.Session.GetString("culture")) ? "id" : HttpContext.Session.GetString("culture").ToLower();
                string OTP = sani.Sanitize(Model.OTP);
                string New_User_Password_Forgotten_ID = sani.Sanitize(Model.New_User_Password_Forgotten_ID.ToString());

                DataTable dtUser = mssql.GetDataTable("SELECT * FROM tblT_User_Password_Forgotten WHERE ID = '" + New_User_Password_Forgotten_ID + "' AND Status = 'Not verified' AND Remarks = '" + OTP + "'");
                if (dtUser.Rows.Count != 1)
                {
                    if (culture == "en")
                        throw new Exception("The OTP is invalid or has expired, you can resend the OTP");
                    else
                        throw new Exception("Kode OTP tidak valid atau sudah kadaluarsa, Anda dapat mengirim ulang kode OTP");
                }


                mssql.ExecuteNonQuery("UPDATE tblT_User_Password_Forgotten SET Status = 'Verified on " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.000") + "' WHERE ID = '" + New_User_Password_Forgotten_ID + "' AND Status = 'Not verified' AND Remarks = '" + OTP + "'");

                return Json(new { Error = false, Message = New_User_Password_Forgotten_ID });

            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult SubmitRenewPassword(tblM_User Model)
        {
            try
            {
                string ID = sani.Sanitize(Model.ID.ToString());
                string Renew_Password = sani.Sanitize(Model.Renew_Password);
                string Renew_Password_Reentered = sani.Sanitize(Model.Renew_Password_Reentered);
                var culture = string.IsNullOrEmpty(HttpContext.Session.GetString("culture")) ? "id" : HttpContext.Session.GetString("culture").ToLower();

                if (Renew_Password != Renew_Password_Reentered)
                    throw new Exception("Re-entered Password did not match");

                DataTable DT = mssql.GetDataTable("SELECT A.ID, B.UserID, B.Username, B.Fullname, LEFT(A.Status, 8) [V_Status] FROM tblT_User_Password_Forgotten A JOIN tblM_User B ON A.UserID = B.UserID WHERE A.ID = '" + ID + "'");
                if (DT.Rows.Count != 1)
                {
                    if (culture == "en")
                        throw new Exception("Verification link is invalid or has expired");
                    else
                        throw new Exception("Link verifikasi tidak valid atau sudah kadaluarsa");
                }


                string UserID = DT.Rows[0]["userID"].ToString();
                string HashPassword = PassHash.HashPassword(Renew_Password);

                mssql.ExecuteNonQuery("UPDATE tblM_User SET PasswordHash = '" + HashPassword + "', LastPasswordChanged = GETDATE(), UpdatedOn = GETDATE() WHERE UserID = '" + UserID + "'");
                mssql.ExecuteNonQuery("DELETE FROM tblT_User_Password_Forgotten WHERE UserID = '" + UserID + "'");

                return Json(new { Error = false, Message = "" });

            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult ResendOTP(tblM_User Model)
        {
            try
            {
                int OTPTimes = 0;
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("OTPTimes")))
                    OTPTimes = int.Parse(HttpContext.Session.GetString("OTPTimes"));

                var culture = string.IsNullOrEmpty(HttpContext.Session.GetString("culture")) ? "id" : HttpContext.Session.GetString("culture").ToLower();
                string Old_User_Password_Forgotten_ID = sani.Sanitize(Model.New_User_Password_Forgotten_ID.ToString());

                string New_User_Password_Forgotten_ID = Guid.NewGuid().ToString();

                DataTable DT = mssql.GetDataTable("SELECT A.ID, B.UserID, B.Username, B.Fullname, LEFT(A.Status, 8) [V_Status], B.Mobile FROM tblT_User_Password_Forgotten A JOIN tblM_User B ON A.UserID = B.UserID WHERE A.ID = '" + Old_User_Password_Forgotten_ID + "' AND A.Status = 'Not Verified'");
                if (DT.Rows.Count != 1)
                {
                    if (culture == "en")
                        throw new Exception("Request is declined");
                    else
                        throw new Exception("Proses ditolak");
                }

                if (HttpContext.Session.GetString("ReqTimes") != null)
                {
                    DataTable DTreq = mssql.GetDataTable("SELECT [Request_OTP],[Submit_OTP] FROM [TblM_Config]");
                    int ReqOTP = Convert.ToInt32(DTreq.Rows[0]["Request_OTP"].ToString());
                    int submitOTP = Convert.ToInt32(DTreq.Rows[0]["Submit_OTP"].ToString());
                    DateTime current = DateTime.Now;
                    DateTime Reqlocked = Convert.ToDateTime(HttpContext.Session.GetString("ReqTimes").ToString()).AddSeconds(ReqOTP);
                    string ReqLockedUntil = Reqlocked.ToString("HH:mm:ss");
                    if (current < Reqlocked)
                        if (culture == "en")
                            throw new Exception("Sorry, please wait for the next " + ReqOTP + " second (until " + ReqLockedUntil + ") to using OTP");
                        else
                            throw new Exception("Maaf,  silahkan menunggu selama " + ReqOTP + " detik kedepan (hingga " + ReqLockedUntil + ") untuk dapat menggunakan OTP");

                   
                }

                if (HttpContext.Session.GetString("OTPLock") != null)
                {
                    DataTable DTreq = mssql.GetDataTable("SELECT [Request_OTP],[Submit_OTP] FROM [TblM_Config]");
                    int ReqOTP = Convert.ToInt32(DTreq.Rows[0]["Request_OTP"].ToString());
                    int submitOTP = Convert.ToInt32(DTreq.Rows[0]["Submit_OTP"].ToString());
                    DateTime current = DateTime.Now;
                    DateTime locked = Convert.ToDateTime(HttpContext.Session.GetString("OTPLock").ToString()).AddMinutes(submitOTP);
                    string LockedUntil = locked.ToString("HH:mm:ss");
                   
                  if (current < locked)
                        if (culture == "en")
                            throw new Exception("Sorry, you have used OTP for 3 (three) times, please wait for the next "+submitOTP+" minutes (until " + LockedUntil + ") to using OTP");
                        else
                            throw new Exception("Maaf, Anda sudah menggunakan OTP sebanyak 3 (tiga) kali, silahkan menunggu selama " + submitOTP + " menit kedepan (hingga " + LockedUntil + ") untuk dapat menggunakan OTP");
                }

                string Mobile = DT.Rows[0]["Mobile"].ToString();
                string UserID = DT.Rows[0]["UserID"].ToString();

                Random generator = new Random();
                string OTP = generator.Next(0, 1000000).ToString("D6");

                mssql.ExecuteNonQuery("DELETE FROM tblT_User_Password_Forgotten WHERE UserID = '" + UserID + "'");
                mssql.ExecuteNonQuery("INSERT INTO tblT_User_Password_Forgotten (ID, UserID, Email, Remarks, Status) VALUES ('" + New_User_Password_Forgotten_ID + "', '" + UserID + "', '" + Mobile + "', '" + OTP + "', 'Not verified')");

                //SEND OTP VIA SMS GATEWAY

                string SMS_Body = "Kode OTP : " + OTP + "\n\nHubungi Administrator SPP bila Anda tidak merasa meminta OTP ini.";
                if (culture == "en")
                    SMS_Body = "Your OTP : " + OTP + "\n\nContact the WBS Helpdesk if you don't think you're asking for this OTP.";

                string SMS_Respon = Helper.SendSMSSingle(SMS_Body, Mobile);
                if (SMS_Respon == "Success")
                {

                    mssql.ExecuteNonQuery("UPDATE tblT_User_Password_Forgotten SET Mail_Status = 'Sent on " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.000") + "' WHERE ID = '" + New_User_Password_Forgotten_ID + "'");
                    OTPTimes = OTPTimes + 1;
                    HttpContext.Session.SetString("OTPTimes", OTPTimes.ToString());
                    HttpContext.Session.SetString("ReqTimes", DateTime.Now.ToString());
                    if (OTPTimes == 3)
                    {
                        HttpContext.Session.Remove("OTPTimes");
                        HttpContext.Session.Remove("ReqTimes");
                        HttpContext.Session.SetString("OTPLock", DateTime.Now.ToString());
                    }
                    else
                        HttpContext.Session.Remove("OTPLock");

                }
                else
                    throw new Exception(SMS_Respon);

                return Json(new { Error = false, Message = New_User_Password_Forgotten_ID });

            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult SubmitOTPProfile(tblM_User Model)
        {
            try
            {
                string OTP = sani.Sanitize(Model.OTP);
                string UserID = StringCipher.Decrypt(HttpContext.Session.GetString("UserID"));
                var culture = string.IsNullOrEmpty(HttpContext.Session.GetString("culture")) ? "id" : HttpContext.Session.GetString("culture").ToLower();

                DataTable dtUser = mssql.GetDataTable("SELECT * FROM tblT_OTP WHERE UserID = '" + UserID + "' AND OTP = '" + OTP + "'");
                if (dtUser.Rows.Count != 1)
                {
                    if (culture == "en")
                        throw new Exception("The OTP is invalid or has expired, you can resend the OTP");
                    else
                        throw new Exception("Kode OTP tidak valid atau sudah kadaluarsa, Anda dapat mengirim ulang kode OTP");
                }

                mssql.ExecuteNonQuery("UPDATE tblM_User SET Mobile = '" + dtUser.Rows[0]["Mobile"] + "', Mobile_Verification = 1, MobileTemp = NULL WHERE UserID = '" + UserID + "'");
                mssql.ExecuteNonQuery("DELETE FROM tblT_OTP WHERE UserID = '" + UserID + "' AND AND OTP = '" + OTP + "'");

                return Json(new { Error = false, Message = "" });

            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult ResendOTPProfile(tblM_User Model)
        {
            try
            {
                int OTPTimes = 0;
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("OTPTimes")))
                    OTPTimes = int.Parse(HttpContext.Session.GetString("OTPTimes"));


                var culture = string.IsNullOrEmpty(HttpContext.Session.GetString("culture")) ? "id" : HttpContext.Session.GetString("culture").ToLower();

                string Mobile = sani.Sanitize(Model.Mobile);
                string PhoneChanged = "";
                if (Mobile.Length >= 8 && Mobile.Length <= 15)
                {
                    if (HttpContext.Session.GetString("ReqTimes") != null)
                    {
                        DataTable DTreq = mssql.GetDataTable("SELECT [Request_OTP],[Submit_OTP] FROM [TblM_Config]");
                        int ReqOTP = Convert.ToInt32(DTreq.Rows[0]["Request_OTP"].ToString());
                        int submitOTP = Convert.ToInt32(DTreq.Rows[0]["Submit_OTP"].ToString());
                        DateTime current = DateTime.Now;
                        DateTime Reqlocked = Convert.ToDateTime(HttpContext.Session.GetString("ReqTimes").ToString()).AddSeconds(ReqOTP);
                        string ReqLockedUntil = Reqlocked.ToString("HH:mm:ss");
                        if (current < Reqlocked)
                            if (culture == "en")
                                throw new Exception("Sorry, please wait for the next " + ReqOTP + " second (until " + ReqLockedUntil + ") to using OTP");
                            else
                                throw new Exception("Maaf,  silahkan menunggu selama " + ReqOTP + " detik kedepan (hingga " + ReqLockedUntil + ") untuk dapat menggunakan OTP");


                    }

                    if (HttpContext.Session.GetString("OTPLock") != null)
                    {
                        DataTable DTreq = mssql.GetDataTable("SELECT [Request_OTP],[Submit_OTP] FROM [TblM_Config]");
                        int ReqOTP = Convert.ToInt32(DTreq.Rows[0]["Request_OTP"].ToString());
                        int submitOTP = Convert.ToInt32(DTreq.Rows[0]["Submit_OTP"].ToString());
                        DateTime current = DateTime.Now;
                        DateTime locked = Convert.ToDateTime(HttpContext.Session.GetString("OTPLock").ToString()).AddMinutes(submitOTP);
                        string LockedUntil = locked.ToString("HH:mm:ss");

                        if (current < locked)
                            if (culture == "en")
                                throw new Exception("Sorry, you have used OTP for 3 (three) times, please wait for the next " + submitOTP + " minutes (until " + LockedUntil + ") to using OTP");
                            else
                                throw new Exception("Maaf, Anda sudah menggunakan OTP sebanyak 3 (tiga) kali, silahkan menunggu selama " + submitOTP + " menit kedepan (hingga " + LockedUntil + ") untuk dapat menggunakan OTP");
                    }

                    string UserID = StringCipher.Decrypt(HttpContext.Session.GetString("UserID"));
                    string New_OTP_ID = Guid.NewGuid().ToString();

                    Random generator = new Random();
                    string OTP = generator.Next(0, 1000000).ToString("D6");

                    mssql.ExecuteNonQuery("DELETE FROM tblT_OTP WHERE UserID = '" + UserID + "'");
                    mssql.ExecuteNonQuery("INSERT INTO tblT_OTP (ID, UserID, Mobile, OTP, Status) VALUES ('" + New_OTP_ID + "', '" + UserID + "', '" + Mobile + "', '" + OTP + "', 'Not verified')");

                    //SEND OTP VIA SMS GATEWAY
                    string SMS_Body = "Kode OTP : " + OTP + "\n\nHubungi Administrator SPP bila Anda tidak merasa meminta OTP ini.";
                    if (culture == "en")
                        SMS_Body = "Your OTP : " + OTP + "\n\nContact the WBS Helpdesk if you don't think you're asking for this OTP.";

                    string SMS_Respon = Helper.SendSMSSingle(SMS_Body, Mobile);
                    if (SMS_Respon == "Success")
                    {
                        mssql.ExecuteNonQuery("UPDATE tblT_OTP SET SMS_Status = 'Sent on " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.000") + "' WHERE ID = '" + New_OTP_ID + "'");
                        OTPTimes = OTPTimes + 1;
                        HttpContext.Session.SetString("OTPTimes", OTPTimes.ToString());
                        HttpContext.Session.SetString("ReqTimes", DateTime.Now.ToString());
                        if (OTPTimes == 3)
                        {
                            HttpContext.Session.Remove("OTPTimes");
                            HttpContext.Session.Remove("ReqTimes");
                            HttpContext.Session.SetString("OTPLock", DateTime.Now.ToString());
                        }
                        else
                            HttpContext.Session.Remove("OTPLock");
                    }
                    else
                        throw new Exception(SMS_Respon);

                    PhoneChanged = Mobile;
                }

                return Json(new { Error = false, Message = PhoneChanged });

            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }
    }
}
