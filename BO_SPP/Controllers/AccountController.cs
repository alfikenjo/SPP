using BO_SPP.Common;
using BO_SPP.Models;
using ClosedXML.Extensions;
using Ganss.XSS;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BO_SPP.Controllers
{

    public class AccountController : Controller
    {
        private HtmlSanitizer sani = new HtmlSanitizer();

        public IActionResult Signin(string ID)
        {
            ViewData["Fullname"] = null;
            ViewData["Roles"] = null;
            ViewData["Title"] = "Login Page";
            HttpContext.Session.Clear();

            if (!string.IsNullOrEmpty(ID))
                HttpContext.Session.SetString("IDPengaduan", ID);

            //aes.EncryptDatabase();

            return View();
        }

        public IActionResult Signout()
        {
            ViewData["Fullname"] = null;
            ViewData["Roles"] = null;
            ViewData["Unit"] = null;
            Helper.RecordAuditTrail(StringCipher.Decrypt(HttpContext.Session.GetString("Email")), "BO SPP", "Log Out", "", "Log out", "");
            HttpContext.Session.Clear();
            return RedirectToAction("Signin", "Account");
        }

        #region Role
        public IActionResult Role()
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                return RedirectToAction("Signin", "Account");

            ViewData["CurrentControllerName"] = "User Management";
            ViewData["CurrentActionName"] = "Role";
            ViewData["Title"] = "Role Management";
            return View();
        }

        [HttpPost]
        public IActionResult GetRole()
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                DataTable dt = mssql.GetDataTable("sp_Get_Role");
                List<tblM_Role> MainData = new List<tblM_Role>();
                MainData = mssql.ConvertDataTable<tblM_Role>(dt);
                for (int i = 0; i < MainData.Count; i++)
                {
                    MainData[i].ID = StringCipher.Encrypt(MainData[i].ID) + "|" + HttpContext.Session.GetString("SessionID");
                }
                return Json(new { Error = false, Message = MainData });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Get_ddl_Role()
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                DataTable dt = mssql.GetDataTable("sp_Get_Role");
                List<tblM_Role> MainData = new List<tblM_Role>();
                MainData = mssql.ConvertDataTable<tblM_Role>(dt);
                return Json(new { Error = false, Message = MainData });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult GetRoleByID(string ID)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                ID = sani.Sanitize(ID);

                string _ID = StringCipher.Decrypt(ID.Split("|")[0]);
                string SessionIDDesc = ID.Split("|")[1];

                if (SessionIDDesc != HttpContext.Session.GetString("SessionID"))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", _ID));
                DataTable dt = mssql.GetDataTable("sp_Get_RoleByID", param);
                List<tblM_Role> MainData = new List<tblM_Role>();
                MainData = mssql.ConvertDataTable<tblM_Role>(dt);
                for (int i = 0; i < MainData.Count; i++)
                {
                    MainData[i].ID = StringCipher.Encrypt(MainData[i].ID) + "|" + HttpContext.Session.GetString("SessionID");
                }
                return Json(new { Error = false, Message = MainData });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult SaveRole(tblM_Role Model)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                string Action = sani.Sanitize(Model.Action);
                string SanitizedID = sani.Sanitize(Model.ID);
                string ID = Guid.NewGuid().ToString();
                if (Action == "add")
                    Model.ID = SanitizedID;

                if (Action != "add")
                {
                    ID = StringCipher.Decrypt(SanitizedID.Split("|")[0]);
                    if (SanitizedID.Split("|")[1] != HttpContext.Session.GetString("SessionID"))
                        throw new Exception("Invalid Authorization|window.location='../Account/Signin'");
                }

                string Role = string.Empty;
                if (Action == "edit" || Action == "hapus")
                {
                    DataRow drOld = mssql.GetDataRow("SELECT Name FROM tblM_Role WHERE ID = '" + ID + "'");
                    Role = drOld["Name"].ToString();
                }

                if (Action == "hapus")
                {
                    if (Role.ToLower() == "system administrator" || Role.ToLower() == "delegator" || Role.ToLower() == "pelapor")
                        throw new Exception("Maaf Role " + Role + " tidak diijinkan untuk dihapus dari sistem");

                    DataRow drCountMember = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM tblT_UserInRole WHERE RoleID = '" + ID + "'");
                    if (int.Parse(drCountMember["Count"].ToString()) > 0)
                        throw new Exception("Maaf Role " + Role + " ini tidak dapat dihapus karena masih digunakan oleh akun pengguna");
                }
                else if (Action == "add")
                {
                    DataRow drIsExist = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM tblM_Role WHERE Name = '" + sani.Sanitize(Model.Name) + "'");
                    if (int.Parse(drIsExist["Count"].ToString()) > 0)
                        throw new Exception("Proses Ditolak, Role dengan nama [" + sani.Sanitize(Model.Name) + "] sudah terdaftar sebelumnya");
                }
                else if (Action == "edit")
                {
                    DataRow drIsExist = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM tblM_Role WHERE Name = '" + sani.Sanitize(Model.Name) + "' AND ID <> '" + ID + "'");
                    if (int.Parse(drIsExist["Count"].ToString()) > 0)
                        throw new Exception("Proses Ditolak, Role dengan nama [" + sani.Sanitize(Model.Name) + "] sudah terdaftar sebelumnya");
                }

                #region Save

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@Action", Action));
                param.Add(new SqlParameter("@ID", ID));
                param.Add(new SqlParameter("@Name", sani.Sanitize(Model.Name)));
                param.Add(new SqlParameter("@Description", sani.Sanitize(Model.Description)));
                param.Add(new SqlParameter("@Status", Model.Status));
                param.Add(new SqlParameter("@CreatedBy", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));

                mssql.ExecuteNonQuery("sp_Save_Role", param);
                #endregion Save

                return Json(new { Error = false, Message = "" });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }
        #endregion Role

        #region User

        public IActionResult UserList()
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                return RedirectToAction("Index", "Dashboard");

            ViewData["CurrentControllerName"] = "User Management";
            ViewData["CurrentActionName"] = "User";
            ViewData["Title"] = "User Management";
            return View();
        }

        [HttpPost]
        public IActionResult GetUser(string isActive, string Roles, string Delegator)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                isActive = sani.Sanitize(isActive);
                Roles = sani.Sanitize(Roles);
                Delegator = sani.Sanitize(Delegator);

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@isActive", isActive));
                param.Add(new SqlParameter("@Roles", Roles));
                param.Add(new SqlParameter("@DelegatorID", Delegator));
                DataTable dt = mssql.GetDataTable("sp_Get_User", param);
                List<tblM_User> MainData = new List<tblM_User>();
                MainData = mssql.ConvertDataTable<tblM_User>(dt);

                for (int i = 0; i < MainData.Count; i++)
                {
                    MainData[i].UserID = StringCipher.Encrypt(MainData[i].UserID) + "|" + HttpContext.Session.GetString("SessionID");

                    if (!string.IsNullOrEmpty(MainData[i].Img))
                        MainData[i].Img = Helper.GetBinaryImageEncrypted(MainData[i].Img, string.IsNullOrEmpty(MainData[i].Ekstension) ? "jpg" : MainData[i].Ekstension);
                }

                return Json(new { Error = false, Message = MainData });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult GetUserByID(string UserID)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                UserID = sani.Sanitize(UserID);
                string _ID = StringCipher.Decrypt(UserID.Split("|")[0]);
                string SessionIDDesc = UserID.Split("|")[1];

                if (SessionIDDesc != HttpContext.Session.GetString("SessionID"))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@UserID", _ID));
                DataTable dt = mssql.GetDataTable("sp_Get_User_By_UserID", param);

                List<tblM_User> MainData = new List<tblM_User>();
                MainData = mssql.ConvertDataTable<tblM_User>(dt);

                for (int i = 0; i < MainData.Count; i++)
                {
                    MainData[i].UserID = StringCipher.Encrypt(MainData[i].UserID) + "|" + HttpContext.Session.GetString("SessionID");

                    if (!string.IsNullOrEmpty(MainData[i].Img))
                        MainData[i].Img = Helper.GetBinaryImageEncrypted(MainData[i].Img, string.IsNullOrEmpty(MainData[i].Ekstension) ? "jpg" : MainData[i].Ekstension);
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
        public IActionResult SaveUser([FromForm] tblM_User Model)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                string Action = sani.Sanitize(Model.Action);
                string HashPassword = "";
                string _UserID = Guid.NewGuid().ToString();
                Model.UserID = sani.Sanitize(Model.UserID);
                string enc_Email = sani.Sanitize(Model.enc_Email);
                if (Action == "add")
                {
                    HashPassword = PassHash.HashPassword(enc_Email);

                    DataRow drIsExist = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM tblM_User WHERE Email = '" + enc_Email + "'");
                    if (int.Parse(drIsExist["Count"].ToString()) > 0)
                        throw new Exception("Proses Ditolak, Akun dengan alamat email [" + aes.Dec(enc_Email) + "] sudah terdaftar sebelumnya");
                }
                else
                {
                    _UserID = StringCipher.Decrypt(Model.UserID.Split("|")[0]);
                    string SessionIDDesc = Model.UserID.Split("|")[1];

                    if (SessionIDDesc != HttpContext.Session.GetString("SessionID"))
                        throw new Exception("Invalid Authorization|window.location='../Account/Signin'");
                }

                if (Action == "hapus")
                {
                    string CurrentUserID = StringCipher.Decrypt(HttpContext.Session.GetString("UserID").ToLower());
                    if (_UserID.ToLower() == CurrentUserID)
                        throw new Exception("Ditolak, Anda tidak dapat menghapus akun yang Anda miliki");
                }

                if (Action == "edit")
                {
                    DataRow drIsExist = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM tblM_User WHERE Email = '" + enc_Email + "' AND UserID <> '" + _UserID + "' AND IsDeleted = 0");
                    if (int.Parse(drIsExist["Count"].ToString()) > 0)
                        throw new Exception("Proses Ditolak, Akun dengan alamat email [" + aes.Dec(enc_Email) + "] sudah terdaftar sebelumnya");
                }


                string FotoFilename = "", FotoFileExtension = "";
                #region Foto
                if (Model.Foto != null)
                {
                    List<IFormFile> files = new List<IFormFile>();
                    files.Add(Model.Foto);
                    foreach (IFormFile file in files)
                    {
                        if (file.Length > 0)
                        {
                            FotoFileExtension = System.IO.Path.GetExtension(file.FileName).Trim();
                            FotoFilename = _UserID;

                            if (FotoFileExtension.ToLower() == ".jpg" || FotoFileExtension.ToLower() == ".png" || FotoFileExtension.ToLower() == ".jpeg")
                            {
                                string upload = Helper.UploadFTPWithEcryption(file, FotoFilename, FotoFileExtension);
                                if (upload != "success")
                                {
                                    FotoFilename = "";
                                    FotoFileExtension = "";
                                    throw new Exception(upload);
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

                string Fullname = sani.Sanitize(Model.enc_Fullname);
                string Mobile = sani.Sanitize(Model.enc_Mobile);
                string Address = sani.Sanitize(Model.enc_Address);
                string NIP = sani.Sanitize(Model.enc_NIP);
                string Jabatan = sani.Sanitize(Model.enc_Jabatan);
                string Divisi = sani.Sanitize(Model.enc_Divisi);

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@Action", Action));
                param.Add(new SqlParameter("@UserID", _UserID));
                param.Add(new SqlParameter("@PasswordHash", HashPassword));
                param.Add(new SqlParameter("@Fullname", Fullname));
                param.Add(new SqlParameter("@Email", enc_Email));
                param.Add(new SqlParameter("@Mobile", Mobile));
                param.Add(new SqlParameter("@Address", Address));
                param.Add(new SqlParameter("@Gender", sani.Sanitize(Model.Gender)));
                param.Add(new SqlParameter("@NIP", NIP));
                param.Add(new SqlParameter("@Jabatan", Jabatan));
                param.Add(new SqlParameter("@Divisi", Divisi));
                param.Add(new SqlParameter("@ID_Roles", sani.Sanitize(Model.ID_Roles)));
                param.Add(new SqlParameter("@Img", FotoFilename));
                param.Add(new SqlParameter("@Ekstension", FotoFileExtension));
                param.Add(new SqlParameter("@isActive", Model.isActive));
                param.Add(new SqlParameter("@CreatedBy", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));

                mssql.ExecuteNonQuery("sp_Save_User", param);
                #endregion Save User

                DataTable dtResult = mssql.GetDataTable("SELECT * FROM vw_UserInRole WHERE UserID = '" + _UserID + "'");
                if (Action == "add" && dtResult.Rows.Count > 0)
                {
                    #region Notifikasi_Akun_Baru                    
                    string Roles = "";
                    foreach (DataRow dr in dtResult.Rows)
                    {
                        Roles += dr["Role"].ToString() + ";";
                    }
                    Helper.SendMail(aes.Dec(enc_Email), "Back Office SPP PTSMI - Selamat Bergabung", MailComposer.compose_mail_body_new_user_admin(aes.Dec(Fullname), aes.Dec(enc_Email), Roles));
                    #endregion Notifikasi_Akun_Baru  
                }

                return Json(new { Error = false, Message = Model.UserID });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult Download(string isActive, string Roles)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    return RedirectToAction("Index", "Dashboard");

                using var wb = GenerateExcelUserList(isActive, Roles);
                return wb.Deliver(DateTime.Now.ToString("yyyyMMdd") + "_DaftarUser.xlsx");
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        private ClosedXML.Excel.XLWorkbook GenerateExcelUserList(string isActive, string Roles)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@isActive", isActive));
            param.Add(new SqlParameter("@Roles", Roles));
            param.Add(new SqlParameter("@CreatedBy", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));
            param.Add(new SqlParameter("@Tipe", "Download PDF"));
            DataTable dt = mssql.GetDataTable("sp_Download_User", param);
            foreach (DataRow dr in dt.Rows)
            {
                dr["Fullname"] = !string.IsNullOrEmpty(dr["Fullname"].ToString()) ? aes.Dec(dr["Fullname"].ToString()) : "";
                dr["Mobile"] = !string.IsNullOrEmpty(dr["Mobile"].ToString()) ? aes.Dec(dr["Mobile"].ToString()) : "";
                dr["Email"] = !string.IsNullOrEmpty(dr["Email"].ToString()) ? aes.Dec(dr["Email"].ToString()) : "";
                dr["NIP"] = !string.IsNullOrEmpty(dr["NIP"].ToString()) ? aes.Dec(dr["NIP"].ToString()) : "";
                dr["Jabatan"] = !string.IsNullOrEmpty(dr["Jabatan"].ToString()) ? aes.Dec(dr["Jabatan"].ToString()) : "";
                dr["UpdatedBy"] = !string.IsNullOrEmpty(dr["UpdatedBy"].ToString()) ? aes.Dec(dr["UpdatedBy"].ToString()) : "";
                dt.AcceptChanges();
            }

            var wb = new ClosedXML.Excel.XLWorkbook();
            var KP = dt;

            var ws1 = wb.AddWorksheet(KP, "Daftar User");

            ws1.Style.Alignment.Indent = 1; ws1.Style.Alignment.Vertical = ClosedXML.Excel.XLAlignmentVerticalValues.Center;

            ws1.Columns().AdjustToContents();

            return wb;
        }

        #endregion User

        #region MyProfile

        public IActionResult MyProfile()
        {
            if (string.IsNullOrEmpty(StringCipher.Decrypt(HttpContext.Session.GetString("Email"))))
                return RedirectToAction("Signin", "Account");

            DataRow dr_Login = mssql.GetDataRow("SELECT COUNT(*) [Jumlah] FROM tblT_User_Login WHERE UserID = '" + StringCipher.Decrypt(HttpContext.Session.GetString("UserID")) + "' AND ID = '" + StringCipher.Decrypt(HttpContext.Session.GetString("SessionID")) + "' AND isActive = 1");
            if (int.Parse(dr_Login["Jumlah"].ToString()) == 0)
                return RedirectToAction("Signin", "Account");

            ViewData["CurrentControllerName"] = "";
            ViewData["CurrentActionName"] = "My Profile";
            ViewData["Title"] = "My Profile";
            return View();
        }

        [HttpPost]
        public IActionResult GetMyProfile()
        {
            try
            {
                if (string.IsNullOrEmpty(StringCipher.Decrypt(HttpContext.Session.GetString("Email"))))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                DataRow dr_Login = mssql.GetDataRow("SELECT COUNT(*) [Jumlah] FROM tblT_User_Login WHERE UserID = '" + StringCipher.Decrypt(HttpContext.Session.GetString("UserID")) + "' AND ID = '" + StringCipher.Decrypt(HttpContext.Session.GetString("SessionID")) + "' AND isActive = 1");
                if (int.Parse(dr_Login["Jumlah"].ToString()) == 0)
                    return RedirectToAction("Signin", "Account");

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
                        MainData[i].Img = Helper.GetBinaryImageEncrypted(MainData[i].Img, string.IsNullOrEmpty(MainData[i].Ekstension) ? "jpg" : MainData[i].Ekstension);
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
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                DataRow dr_Login = mssql.GetDataRow("SELECT COUNT(*) [Jumlah] FROM tblT_User_Login WHERE UserID = '" + StringCipher.Decrypt(HttpContext.Session.GetString("UserID")) + "' AND ID = '" + StringCipher.Decrypt(HttpContext.Session.GetString("SessionID")) + "' AND isActive = 1");
                if (int.Parse(dr_Login["Jumlah"].ToString()) == 0)
                    return RedirectToAction("Signin", "Account");

                Model.UserID = StringCipher.Decrypt(HttpContext.Session.GetString("UserID"));


                string FotoFilename = "", FotoFileExtension = "";
                #region Foto
                if (Model.Foto != null)
                {
                    List<IFormFile> files = new List<IFormFile>();
                    files.Add(Model.Foto);
                    foreach (IFormFile file in files)
                    {
                        if (file.Length > 0)
                        {
                            FotoFileExtension = System.IO.Path.GetExtension(file.FileName).Trim();
                            FotoFilename = sani.Sanitize(Model.UserID).ToString();

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

                string Fullname = sani.Sanitize(Model.enc_Fullname);
                string Address = sani.Sanitize(Model.enc_Address);
                string NIP = sani.Sanitize(Model.enc_NIP);
                string Jabatan = sani.Sanitize(Model.enc_Jabatan);
                string Divisi = sani.Sanitize(Model.enc_Divisi);

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@UserID", Model.UserID));
                param.Add(new SqlParameter("@Fullname", Fullname));
                param.Add(new SqlParameter("@Address", Address));
                param.Add(new SqlParameter("@Gender", Model.Gender));
                param.Add(new SqlParameter("@NIP", NIP));
                param.Add(new SqlParameter("@Jabatan", Jabatan));
                param.Add(new SqlParameter("@Divisi", Divisi));
                param.Add(new SqlParameter("@Img", FotoFilename));
                param.Add(new SqlParameter("@Ekstension", FotoFileExtension));
                param.Add(new SqlParameter("@CreatedBy", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));

                string asd = Fullname;
                mssql.ExecuteNonQuery("spUpdateMyProfileInternal", param);
                #endregion Save User

                HttpContext.Session.SetString("fn", aes.Dec(Fullname));


                return Json(new { Error = false, Message = Model.UserID });
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
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                DataRow dr_Login = mssql.GetDataRow("SELECT COUNT(*) [Jumlah] FROM tblT_User_Login WHERE UserID = '" + StringCipher.Decrypt(HttpContext.Session.GetString("UserID")) + "' AND ID = '" + StringCipher.Decrypt(HttpContext.Session.GetString("SessionID")) + "' AND isActive = 1");
                if (int.Parse(dr_Login["Jumlah"].ToString()) == 0)
                    return RedirectToAction("Signin", "Account");

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
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserID")))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                int OTPTimes = 0;
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("OTPTimes")))
                    OTPTimes = int.Parse(HttpContext.Session.GetString("OTPTimes"));

                DataRow dr_Login = mssql.GetDataRow("SELECT COUNT(*) [Jumlah] FROM tblT_User_Login WHERE UserID = '" + StringCipher.Decrypt(HttpContext.Session.GetString("UserID")) + "' AND ID = '" + StringCipher.Decrypt(HttpContext.Session.GetString("SessionID")) + "' AND isActive = 1");
                if (int.Parse(dr_Login["Jumlah"].ToString()) == 0)
                    return RedirectToAction("Signin", "Account");

                var EmailNotification = Model.EmailNotification;

                string OldMobile = "";
                DataTable dtOld = mssql.GetDataTable("SELECT Mobile FROM tblM_User WHERE UserID = '" + StringCipher.Decrypt(HttpContext.Session.GetString("UserID")) + "' AND Mobile_Verification = 1");
                if (dtOld.Rows.Count == 1)
                    OldMobile = !string.IsNullOrEmpty(dtOld.Rows[0]["Mobile"].ToString()) ? aes.Dec(dtOld.Rows[0]["Mobile"].ToString()) : "";

                string Mobile = sani.Sanitize(Model.enc_Mobile);
                if (aes.Dec(Mobile).Length >= 8 && aes.Dec(Mobile).Length <= 15 && aes.Dec(Mobile) != OldMobile)
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
                            throw new Exception("Maaf, Anda sudah menggunakan OTP sebanyak 3 (tiga) kali, silahkan menunggu selama " + submitOTP + " menit kedepan (hingga " + LockedUntil + ") untuk dapat menggunakan OTP");
                    }
                }

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@UserID", StringCipher.Decrypt(HttpContext.Session.GetString("UserID"))));
                param.Add(new SqlParameter("@Mobile", sani.Sanitize(Model.enc_Mobile)));
                param.Add(new SqlParameter("@EmailNotification", Model.EmailNotification));
                param.Add(new SqlParameter("@CreatedBy", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));

                mssql.ExecuteNonQuery("spUpdateAccountInternal", param);

                
                string PhoneChanged = "";
                if (aes.Dec(Mobile).Length >= 8 && aes.Dec(Mobile).Length <= 15 && aes.Dec(Mobile) != OldMobile)
                {                                            
                    string UserID = StringCipher.Decrypt(HttpContext.Session.GetString("UserID"));
                    string New_OTP_ID = Guid.NewGuid().ToString();

                    Random generator = new Random();
                    string OTP = generator.Next(0, 1000000).ToString("D6");

                    mssql.ExecuteNonQuery("DELETE FROM tblT_OTP WHERE UserID = '" + UserID + "'");
                    mssql.ExecuteNonQuery("INSERT INTO tblT_OTP (ID, UserID, Mobile, OTP, Status) VALUES ('" + New_OTP_ID + "', '" + UserID + "', '" + aes.Dec(Mobile) + "', '" + OTP + "', 'Not verified')");

                    //SEND OTP VIA SMS GATEWAY
                    string SMS_Body = "Kode OTP : " + OTP + "\n\nHubungi Administrator SPP PT SMI bila Anda tidak merasa meminta OTP ini.";

                    string SMS_Respon = Helper.SendSMSSingle(SMS_Body, aes.Dec(Mobile));
                    if (SMS_Respon == "Success")
                    {
                        HttpContext.Session.Remove("SubmitOTPAttempt");
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
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                DataRow dr_Login = mssql.GetDataRow("SELECT COUNT(*) [Jumlah] FROM tblT_User_Login WHERE UserID = '" + StringCipher.Decrypt(HttpContext.Session.GetString("UserID")) + "' AND ID = '" + StringCipher.Decrypt(HttpContext.Session.GetString("SessionID")) + "' AND isActive = 1");
                if (int.Parse(dr_Login["Jumlah"].ToString()) == 0)
                    return RedirectToAction("Signin", "Account");

                string CurrentPassword = sani.Sanitize(Model.CurrentPassword);
                string NewPassword = sani.Sanitize(Model.NewPassword);
                string NewPasswordVerifiy = sani.Sanitize(Model.NewPasswordVerifiy);
                string Email = StringCipher.Decrypt(HttpContext.Session.GetString("Email"));

                if (NewPassword != NewPasswordVerifiy)
                    throw new Exception("Password baru tidak cocok, mohon periksa kembali");

                string ProvidedPassword = CurrentPassword;
                DataTable DT = mssql.GetDataTable("SELECT * FROM tblM_User WHERE Email = '" + Email + "' AND ISNULL(isActive, 0) = 1");
                if (DT.Rows.Count == 1)
                {
                    if (!PassHash.VerifyHashedPassword(DT.Rows[0]["PasswordHash"].ToString(), ProvidedPassword))
                    {
                        throw new Exception("Invalid Current Password");
                    }

                    string HashPassword = PassHash.HashPassword(NewPasswordVerifiy);

                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(new SqlParameter("@UserID", StringCipher.Decrypt(HttpContext.Session.GetString("UserID"))));
                    param.Add(new SqlParameter("@PasswordHash", HashPassword));
                    param.Add(new SqlParameter("@CreatedBy", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));

                    mssql.ExecuteNonQuery("spChangePassword", param);
                }
                else
                    throw new Exception("User not found or inactive");

                return Json(new { Error = false, Message = Model.UserID });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }
        #endregion MyProfile

        #region UserManagement

        [HttpPost]
        public IActionResult FillCaptcha()
        {
            try
            {
                Random random = new Random();
                string combination = "0123456789ABCDEFGHIJKLMNPQRSTUVWXYZabcdefghjkmnpqrstuvwxyz";
                StringBuilder captcha = new StringBuilder();
                string ImageUrl = "";
                for (int i = 0; i < 6; i++)
                {
                    captcha.Append(combination[random.Next(combination.Length)]);
                    HttpContext.Session.SetString("captcha", captcha.ToString());
                    ImageUrl = Helper.GetImageCaptcha(DateTime.Now.Ticks.ToString(), captcha.ToString());
                }
                Thread.Sleep(1000);
                return Json(new { Error = false, Message = ImageUrl });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        [SupportedOSPlatform("windows")]
        public IActionResult signin(string email, string password, string captcha)
        {
            string domain = ".\\";
            email = sani.Sanitize(email);
            password = sani.Sanitize(password);
            captcha = sani.Sanitize(captcha);
            string username = email.Trim().Replace("@ptsmi.co.id", "");
            string encrypted_email = aes.Enc(email);
            string ByPass = ConfigurationManager.AppSetting["FileConfiguration:ByPass"];
            if (ByPass == "1")
            {
                DataTable DT = mssql.GetDataTable("SELECT * FROM tblM_User WHERE Email = '" + encrypted_email + "' AND ISNULL(isActive, 0) = 1");
                if (DT.Rows.Count != 1)
                {
                    ViewBag.error = "Invalid BP Account";
                    return View("Signin");
                }

                string SessionID = Guid.NewGuid().ToString();
                mssql.ExecuteNonQuery("UPDATE tblT_User_Login SET isActive = 0, Last_Logout_DateTime = GETDATE() WHERE isActive = 1 AND UserID = '" + DT.Rows[0]["UserID"].ToString() + "'");
                mssql.ExecuteNonQuery("INSERT INTO tblT_User_Login (ID, UserID, isActive, Last_Login_DateTime) VALUES ('" + SessionID + "', '" + DT.Rows[0]["UserID"].ToString() + "', 1, GETDATE())");

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@UserID", DT.Rows[0]["UserID"].ToString()));
                DataTable dt_User = mssql.GetDataTable("sp_Get_User_By_UserID", param);

                HttpContext.Session.SetString("UserID", StringCipher.Encrypt(DT.Rows[0]["UserID"].ToString()));
                HttpContext.Session.SetString("Email", StringCipher.Encrypt(encrypted_email));
                HttpContext.Session.SetString("fn", !string.IsNullOrEmpty(DT.Rows[0]["Fullname"].ToString()) ? aes.Dec(DT.Rows[0]["Fullname"].ToString()) : "");
                HttpContext.Session.SetString("fr", dt_User.Rows[0]["Roles"].ToString());

                if (dt_User.Rows[0]["Roles"].ToString().Contains("Admin SPP"))
                    HttpContext.Session.SetString("spp", StringCipher.Encrypt(Guid.NewGuid().ToString()));
                else
                    HttpContext.Session.SetString("spp", "");

                HttpContext.Session.SetString("SessionID", StringCipher.Encrypt(SessionID));

                if (string.IsNullOrEmpty(dt_User.Rows[0]["Img"].ToString()))
                    HttpContext.Session.SetString("img", "../image/default_avatar.png");
                else
                {
                    string imgPath = Helper.GetBinaryImageEncrypted(dt_User.Rows[0]["Img"].ToString(), dt_User.Rows[0]["Ekstension"].ToString());
                    HttpContext.Session.SetString("img", imgPath);
                }

                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("IDPengaduan")))
                    return RedirectToAction("PengaduanForm", "Pengaduan", new { action = "view", ID = HttpContext.Session.GetString("IDPengaduan") });
                else
                    return RedirectToAction("Index", "Dashboard");
            }

            email = email.Trim().Replace("@ptsmi.co.id", "") + "@ptsmi.co.id";
            if (email == null || password == null)
            {
                ViewBag.error = "Invalid Credential";
                return View("Signin");
            }
            encrypted_email = aes.Enc(email);

            string current_captcha = HttpContext.Session.GetString("captcha");
            if (captcha != current_captcha)
            {
                ViewBag.error = "Invalid Credential - Captcha";
                return View("Signin");
            }
            else
            {
                //string ProvidedPassword = password;
                DataTable DT = mssql.GetDataTable("SELECT * FROM tblM_User WHERE Email = '" + encrypted_email + "' AND ISNULL(isActive, 0) = 1");
                if (DT.Rows.Count == 1)
                {
                    string AD_Authentication = LDAP.VerifyAccount(username, password, domain);
                    //bool NonADAuth = PassHash.VerifyHashedPassword(DT.Rows[0]["PasswordHash"].ToString(), ProvidedPassword);
                    if (AD_Authentication != "1")
                    {
                        ViewBag.error = AD_Authentication;
                        return View("Signin");
                    }
                    else
                    {
                        DataRow drPelapor = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM vw_UserInRole WHERE Email = '" + encrypted_email + "' AND [Role] = 'Pelapor'");
                        if (int.Parse(drPelapor["Count"].ToString()) > 0)
                        {
                            ViewBag.error = "Invalid Credential - not valid internal register";
                            return View("Signin");
                        }

                        string SessionID = Guid.NewGuid().ToString();
                        mssql.ExecuteNonQuery("UPDATE tblT_User_Login SET isActive = 0, Last_Logout_DateTime = GETDATE() WHERE isActive = 1 AND UserID = '" + DT.Rows[0]["UserID"].ToString() + "'");
                        mssql.ExecuteNonQuery("INSERT INTO tblT_User_Login (ID, UserID, isActive, Last_Login_DateTime) VALUES ('" + SessionID + "', '" + DT.Rows[0]["UserID"].ToString() + "', 1, GETDATE())");

                        List<SqlParameter> param = new List<SqlParameter>();
                        param.Add(new SqlParameter("@UserID", DT.Rows[0]["UserID"].ToString()));
                        DataTable dt_User = mssql.GetDataTable("sp_Get_User_By_UserID", param);

                        HttpContext.Session.SetString("UserID", StringCipher.Encrypt(DT.Rows[0]["UserID"].ToString()));
                        HttpContext.Session.SetString("Email", StringCipher.Encrypt(encrypted_email));
                        HttpContext.Session.SetString("fn", !string.IsNullOrEmpty(DT.Rows[0]["Fullname"].ToString()) ? aes.Dec(DT.Rows[0]["Fullname"].ToString()) : "");
                        HttpContext.Session.SetString("fr", dt_User.Rows[0]["Roles"].ToString());

                        if (dt_User.Rows[0]["Roles"].ToString().Contains("Admin SPP"))
                            HttpContext.Session.SetString("spp", StringCipher.Encrypt(Guid.NewGuid().ToString()));
                        else
                            HttpContext.Session.SetString("spp", "");

                        HttpContext.Session.SetString("SessionID", StringCipher.Encrypt(SessionID));

                        if (string.IsNullOrEmpty(dt_User.Rows[0]["Img"].ToString()))
                            HttpContext.Session.SetString("img", "../image/default_avatar.png");
                        else
                        {
                            string imgPath = Helper.GetBinaryImageEncrypted(dt_User.Rows[0]["Img"].ToString(), dt_User.Rows[0]["Ekstension"].ToString());
                            HttpContext.Session.SetString("img", imgPath);
                        }

                        Helper.RecordAuditTrail(encrypted_email, "BO SPP", "Login", "", "LOGIN", "Success");

                        if (!string.IsNullOrEmpty(HttpContext.Session.GetString("IDPengaduan")))
                            return RedirectToAction("PengaduanForm", "Pengaduan", new { action = "view", ID = HttpContext.Session.GetString("IDPengaduan") });
                        else
                            return RedirectToAction("Index", "Dashboard");
                    }
                }
                else
                {
                    ViewBag.error = "Invalid Credential - username / password is incorrect";
                    return View("Signin");
                }
            }
        }

        #endregion UserManagement

        [HttpPost]
        public IActionResult GetAccountByUserID()
        {
            try
            {
                if (string.IsNullOrEmpty(StringCipher.Decrypt(HttpContext.Session.GetString("Email"))))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                DataRow dr_Login = mssql.GetDataRow("SELECT COUNT(*) [Jumlah] FROM tblT_User_Login WHERE UserID = '" + StringCipher.Decrypt(HttpContext.Session.GetString("UserID")) + "' AND ID = '" + StringCipher.Decrypt(HttpContext.Session.GetString("SessionID")) + "' AND isActive = 1");
                if (int.Parse(dr_Login["Jumlah"].ToString()) == 0)
                    return RedirectToAction("Signin", "Account");

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@UserID", StringCipher.Decrypt(HttpContext.Session.GetString("UserID"))));
                DataTable dt = mssql.GetDataTable("sp_GetAccountByUserID", param);

                if (dt.Rows.Count == 0)
                    throw new Exception("Role not found");

                List<Account> MainData = new List<Account>();
                MainData = mssql.ConvertDataTable<Account>(dt);
                return Json(new { Error = false, Message = MainData });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult DownloadPDF(string isActive, string Roles)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    return RedirectToAction("Index", "Dashboard");

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@isActive", isActive));
                param.Add(new SqlParameter("@Roles", Roles));
                param.Add(new SqlParameter("@CreatedBy", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));
                param.Add(new SqlParameter("@Tipe", "Download PDF"));
                DataTable dt = mssql.GetDataTable("sp_Download_User", param);
                foreach (DataRow dr in dt.Rows)
                {
                    dr["Fullname"] = !string.IsNullOrEmpty(dr["Fullname"].ToString()) ? aes.Dec(dr["Fullname"].ToString()) : "";
                    dr["Mobile"] = !string.IsNullOrEmpty(dr["Mobile"].ToString()) ? aes.Dec(dr["Mobile"].ToString()) : "";
                    dr["Email"] = !string.IsNullOrEmpty(dr["Email"].ToString()) ? aes.Dec(dr["Email"].ToString()) : "";
                    dr["NIP"] = !string.IsNullOrEmpty(dr["NIP"].ToString()) ? aes.Dec(dr["NIP"].ToString()) : "";
                    dr["Jabatan"] = !string.IsNullOrEmpty(dr["Jabatan"].ToString()) ? aes.Dec(dr["Jabatan"].ToString()) : "";
                    dr["UpdatedBy"] = !string.IsNullOrEmpty(dr["UpdatedBy"].ToString()) ? aes.Dec(dr["UpdatedBy"].ToString()) : "";
                    dt.AcceptChanges();
                }


                ExportToPdf(dt);
                return null;
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }

        }

        private void ExportToPdf(DataTable datas)
        {

            if (datas.Rows.Count > 0)
            {
                int pdfRowIndex = 1;
                string filename = DateTime.Now.ToString("yyyyMMdd") + "_DaftarUser";
                string filepath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\repo", filename + ".pdf");
                Document document = new Document();
                document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());

                var table = new PdfPTable(11); // table with 11 columns
                PdfPCell cell = new PdfPCell();
                table.WidthPercentage = 100; //table width to 100per

                FileStream fs = new FileStream(filepath, FileMode.Create);
                PageEventHelper pageEventHelper = new PageEventHelper();
                PdfWriter writer = PdfWriter.GetInstance(document, fs);
                writer.PageEvent = pageEventHelper;
                document.Open();

                Font font1 = FontFactory.GetFont("Segoe UI", 9);
                Font font2 = FontFactory.GetFont("Segoe UI", 8);

                Font titleFont = FontFactory.GetFont("Segoe UI", 12);
                Font regularFont = FontFactory.GetFont("Segoe UI", 10);
                Paragraph title;
                Paragraph text;
                title = new Paragraph("BO SPP - DAFTAR USER", titleFont);
                title.Alignment = Element.ALIGN_CENTER;
                document.Add(title);
                text = new Paragraph("Printed On: " + DateTime.Now.ToString("dd MMM yyyy HH:mm"), regularFont);
                text.Alignment = Element.ALIGN_CENTER;
                document.Add(text);

                document.Add(new Paragraph("\n\n"));

                table.AddCell(new Phrase("Fullname", font1));
                table.AddCell(new Phrase("Mobile", font1));
                table.AddCell(new Phrase("Email", font1));
                table.AddCell(new Phrase("NIP", font1));
                table.AddCell(new Phrase("Jabatan", font1));
                table.AddCell(new Phrase("isActive", font1));
                table.AddCell(new Phrase("Roles", font1));
                table.AddCell(new Phrase("Updated On", font1));
                table.AddCell(new Phrase("Updated By", font1));
                table.AddCell(new Phrase("Status", font1));
                table.AddCell(new Phrase("Delegators", font1));

                table.HeaderRows = 1;

                foreach (DataRow data in datas.Rows)
                {
                    table.AddCell(new Phrase(data["Fullname"].ToString(), font2));
                    table.AddCell(new Phrase(data["Mobile"].ToString(), font2));
                    table.AddCell(new Phrase(data["Email"].ToString(), font2));
                    table.AddCell(new Phrase(data["NIP"].ToString(), font2));
                    table.AddCell(new Phrase(data["Jabatan"].ToString(), font2));
                    table.AddCell(new Phrase(data["isActive"].ToString(), font2));
                    table.AddCell(new Phrase(data["Roles"].ToString(), font2));
                    table.AddCell(new Phrase(data["UpdatedOn"].ToString(), font2));
                    table.AddCell(new Phrase(data["UpdatedBy"].ToString(), font2));
                    table.AddCell(new Phrase(data["Status"].ToString(), font2));
                    table.AddCell(new Phrase(data["Delegators"].ToString(), font2));

                    pdfRowIndex++;
                }

                document.Add(table);
                document.Close();
                document.CloseDocument();
                document.Dispose();
                writer.Close();
                writer.Dispose();
                fs.Close();
                fs.Dispose();

                FileStream sourceFile = new FileStream(filepath, FileMode.Open);
                float fileSize = 0;
                fileSize = sourceFile.Length;
                byte[] getContent = new byte[Convert.ToInt32(Math.Truncate(fileSize))];
                sourceFile.Read(getContent, 0, Convert.ToInt32(sourceFile.Length));
                sourceFile.Close();
                Response.Clear();
                Response.Headers.Clear();
                Response.ContentType = "application/pdf";
                Response.Headers.Add("Content-Length", getContent.Length.ToString());
                Response.Headers.Add("Content-Disposition", "attachment; filename=" + filename + ".pdf;");
                Response.Body.WriteAsync(getContent);
                Response.Body.Flush();
            }
        }

        [HttpPost]
        public IActionResult SubmitOTPProfile(tblM_User Model)
        {
            try
            {
                int SubmitOTPAttempt = 0;
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SubmitOTPAttempt")))
                    SubmitOTPAttempt = int.Parse(HttpContext.Session.GetString("SubmitOTPAttempt"));               

                string OTP = sani.Sanitize(Model.OTP);
                string UserID = StringCipher.Decrypt(HttpContext.Session.GetString("UserID"));

                if (HttpContext.Session.GetString("SubmitTimes") != null)
                {
                    DataTable DTreq = mssql.GetDataTable("SELECT [Request_OTP],[Submit_OTP] FROM [TblM_Config]");
                    int ReqOTP = Convert.ToInt32(DTreq.Rows[0]["Request_OTP"].ToString());
                    int submitOTP = Convert.ToInt32(DTreq.Rows[0]["Submit_OTP"].ToString());
                    DateTime current = DateTime.Now;
                    DateTime Reqlocked = Convert.ToDateTime(HttpContext.Session.GetString("SubmitTimes").ToString()).AddSeconds(ReqOTP);
                    string ReqLockedUntil = Reqlocked.ToString("HH:mm:ss");
                    if (current < Reqlocked)
                        throw new Exception("Maaf, silahkan menunggu selama " + ReqOTP + " detik kedepan (hingga " + ReqLockedUntil + ") untuk mencoba kembali input OTP");
                }

                DataTable dtUser = mssql.GetDataTable("SELECT * FROM tblT_OTP WHERE UserID = '" + UserID + "' AND OTP = '" + OTP + "'");
                if (dtUser.Rows.Count != 1)
                {
                    SubmitOTPAttempt = SubmitOTPAttempt + 1;
                    HttpContext.Session.SetString("SubmitOTPAttempt", SubmitOTPAttempt.ToString());
                    HttpContext.Session.SetString("SubmitTimes", DateTime.Now.ToString());
                    if (SubmitOTPAttempt == 3)
                    {
                        HttpContext.Session.Remove("SubmitOTPAttempt");
                        HttpContext.Session.Remove("SubmitTimes");
                        throw new Exception("Maaf, Anda telah gagal menggunakan OTP sebanyak 3 (tiga) kali, verifikasi OTP dibatalkan, silahkan mencoba kembali");
                    }

                    throw new Exception("Kode OTP tidak valid atau sudah kadaluarsa, Anda memiliki " + (3 - SubmitOTPAttempt) + " kesempatan lagi atau Anda dapat mengirim ulang kode OTP");
                }
                else
                {
                    HttpContext.Session.Remove("SubmitTimes");
                    HttpContext.Session.Remove("SubmitOTPAttempt");                    
                }

                mssql.ExecuteNonQuery("UPDATE tblM_User SET Mobile = '" + aes.Enc(dtUser.Rows[0]["Mobile"].ToString()) + "', Mobile_Verification = 1, MobileTemp = NULL WHERE UserID = '" + UserID + "'");
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
                HttpContext.Session.Remove("SubmitOTPAttempt");

                int OTPTimes = 0;
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("OTPTimes")))
                    OTPTimes = int.Parse(HttpContext.Session.GetString("OTPTimes"));

                string Mobile = sani.Sanitize(aes.Dec(Model.enc_Mobile));
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
                            throw new Exception("Maaf, Anda sudah menggunakan OTP sebanyak 3 (tiga) kali, silahkan menunggu selama " + submitOTP + " menit kedepan (hingga " + LockedUntil + ") untuk dapat menggunakan OTP");
                    }

                    string UserID = StringCipher.Decrypt(HttpContext.Session.GetString("UserID"));
                    string New_OTP_ID = Guid.NewGuid().ToString();

                    Random generator = new Random();
                    string OTP = generator.Next(0, 1000000).ToString("D6");

                    mssql.ExecuteNonQuery("DELETE FROM tblT_OTP WHERE UserID = '" + UserID + "'");
                    mssql.ExecuteNonQuery("INSERT INTO tblT_OTP (ID, UserID, Mobile, OTP, Status) VALUES ('" + New_OTP_ID + "', '" + UserID + "', '" + Mobile + "', '" + OTP + "', 'Not verified')");

                    //SEND OTP VIA SMS GATEWAY
                    string SMS_Body = "Kode OTP : " + OTP + "\n\nHubungi Administrator SPP PT SMI bila Anda tidak merasa meminta OTP ini.";

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
        [SupportedOSPlatform("windows")]
        public IActionResult GetADUserByUsername(string username)
        {
            try
            {
                username = ".\\" + sani.Sanitize(username.Trim().Replace("@ptsmi.co.id", ""));
                ActiveDirectoryViewModel advm = LDAP.GetPersonalInformation(username);

                DataTable dt = mssql.ObjectToData(advm);
                var testRow = dt.Rows[0];

                List<ActiveDirectoryViewModel> MainData = new List<ActiveDirectoryViewModel>();
                MainData = mssql.ConvertDataTable<ActiveDirectoryViewModel>(dt);

                return Json(new { Error = false, Message = MainData });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        [SupportedOSPlatform("windows")]
        public IActionResult GetLDAPAccByEmail(string email)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                email = sani.Sanitize(email).Trim();

                //DataRow drAkunPelapor = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM tblM_User WHERE ISNULL(IsDeleted, 0) = 0 AND Email = '"+ email +"' AND UserID IN (SELECT DISTINCT A.UserID FROM vw_UserInRole A WHERE A.[Role] = 'Pelapor' AND A.UserID = UserID);");
                //DataRow drAkun = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM tblM_User WHERE ISNULL(IsDeleted, 0) = 0 AND Email = '" + email + "'");
                //if (int.Parse(drAkunPelapor["Count"].ToString()) > 0)
                //    throw new Exception("Already Registered Pelapor");
                //else if (int.Parse(drAkun["Count"].ToString()) > 0)
                //    throw new Exception("Already Registered");

                string username = ".\\" + sani.Sanitize(email.Trim().Replace("@ptsmi.co.id", ""));
                ActiveDirectoryViewModel advm = LDAP.GetPersonalInformation(username);

                DataTable dt = mssql.ObjectToData(advm);

                List<ActiveDirectoryViewModel> MainData = new List<ActiveDirectoryViewModel>();
                MainData = mssql.ConvertDataTable<ActiveDirectoryViewModel>(dt);

                return Json(new { Error = false, Message = MainData });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        [SupportedOSPlatform("windows")]
        public IActionResult GetLDAPAccByEmailByDelegatorID(string email, string DelegatorID)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                email = sani.Sanitize(email).Trim();
                string enc_email = aes.Enc(email);

                string ID = StringCipher.Decrypt(sani.Sanitize(DelegatorID).Split("|")[0]);
                if (sani.Sanitize(DelegatorID).Split("|")[1] != HttpContext.Session.GetString("SessionID"))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                DataRow drMemberDelegator = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM tblT_UserInDelegator A JOIN tblM_User B ON A.UserID = B.UserID AND A.DelegatorID = '" + ID + "' AND B.Email = '" + enc_email + "'");
                if (int.Parse(drMemberDelegator["Count"].ToString()) > 0)
                    throw new Exception("Already Registered Pelapor");

                string username = ".\\" + sani.Sanitize(email.Trim().Replace("@ptsmi.co.id", ""));
                ActiveDirectoryViewModel advm = LDAP.GetPersonalInformation(username);

                DataTable dt = mssql.ObjectToData(advm);

                List<ActiveDirectoryViewModel> MainData = new List<ActiveDirectoryViewModel>();
                MainData = mssql.ConvertDataTable<ActiveDirectoryViewModel>(dt);

                return Json(new { Error = false, Message = MainData });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult InviteMemberDelegator(tblM_User Model)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                string _UserID = Guid.NewGuid().ToString();
                Model.DelegatorID = sani.Sanitize(Model.DelegatorID);

                string DelegatorID = StringCipher.Decrypt(Model.DelegatorID.Split("|")[0]);
                string SessionIDDesc = Model.DelegatorID.Split("|")[1];

                if (SessionIDDesc != HttpContext.Session.GetString("SessionID"))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                string Email = sani.Sanitize(Model.enc_Email);
                string Fullname = sani.Sanitize(Model.enc_Fullname);
                string Mobile = sani.Sanitize(Model.enc_Mobile);
                string Address = sani.Sanitize(Model.enc_Address);
                string NIP = sani.Sanitize(Model.enc_NIP);
                string Jabatan = sani.Sanitize(Model.enc_Jabatan);
                string Divisi = sani.Sanitize(Model.enc_Divisi);

                DataRow drIsExist = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM tblM_User WHERE Email = '" + Email + "' AND ISNULL(isDeleted, 0) = 0");
                if (int.Parse(drIsExist["Count"].ToString()) == 0)
                {
                    #region Save User
                    string FotoFilename = "";
                    string HashPassword = PassHash.HashPassword(Email);
                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(new SqlParameter("@Action", "add"));
                    param.Add(new SqlParameter("@UserID", _UserID));
                    param.Add(new SqlParameter("@PasswordHash", HashPassword));
                    param.Add(new SqlParameter("@Fullname", Fullname));
                    param.Add(new SqlParameter("@Email", Email));
                    param.Add(new SqlParameter("@Mobile", Mobile));
                    param.Add(new SqlParameter("@Address", Address));
                    param.Add(new SqlParameter("@Gender", ""));
                    param.Add(new SqlParameter("@NIP", ""));
                    param.Add(new SqlParameter("@Jabatan", Jabatan));
                    param.Add(new SqlParameter("@Divisi", Divisi));
                    param.Add(new SqlParameter("@ID_Roles", ""));
                    param.Add(new SqlParameter("@Img", FotoFilename));
                    param.Add(new SqlParameter("@isActive", 1));
                    param.Add(new SqlParameter("@CreatedBy", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));

                    mssql.ExecuteNonQuery("sp_Save_User", param);
                    #endregion Save User

                    DataTable dtResult = mssql.GetDataTable("SELECT * FROM tblM_User WHERE UserID = '" + _UserID + "'");
                    if (dtResult.Rows.Count > 0)
                    {
                        #region Notifikasi_Akun_Baru                    
                        string Roles = "Delegator";
                        Helper.SendMail(aes.Dec(Email), "Back Office SPP PTSMI - Selamat Bergabung", MailComposer.compose_mail_body_new_user_admin(aes.Dec(Fullname), aes.Dec(Email), Roles));
                        #endregion Notifikasi_Akun_Baru  
                    }
                }
                else
                {
                    DataTable dtUser = mssql.GetDataTable("SELECT TOP 1 UserID FROM tblM_User WHERE Email = '" + Email + "' AND ISNULL(isDeleted, 0) = 0");
                    if (dtUser.Rows.Count > 0)
                    {
                        _UserID = dtUser.Rows[0]["UserID"].ToString();
                        DataRow drMemberExist = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM tblT_UserInDelegator A JOIN tblM_Delegator B ON A.DelegatorID = B.ID WHERE A.UserID = '" + _UserID + "' AND A.DelegatorID = '" + DelegatorID + "'");
                        if (int.Parse(drMemberExist["Count"].ToString()) > 0)
                            throw new Exception("Email ini sudah terdaftar sebagai member pada Grup Delegator ini");
                    }
                }

                #region JoinToGrup

                List<SqlParameter> param1 = new List<SqlParameter>();
                param1.Add(new SqlParameter("@Action", "add"));
                param1.Add(new SqlParameter("@ID", Guid.NewGuid().ToString()));
                param1.Add(new SqlParameter("@UserID", _UserID));
                param1.Add(new SqlParameter("@DelegatorID", DelegatorID));
                param1.Add(new SqlParameter("@CreatedBy", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));

                mssql.ExecuteNonQuery("sp_Save_MemberDelegator", param1);

                int RowsAffected = 0;
                DataTable dtDel = mssql.GetDataTable("SELECT B.Name [DelegatorName] FROM tblT_UserInDelegator A JOIN tblM_Delegator B ON A.DelegatorID = B.ID WHERE A.UserID = '" + _UserID + "' AND A.DelegatorID = '" + DelegatorID + "'");
                if (dtDel.Rows.Count == 1)
                {
                    #region Notifikasi_ke_Member_Baru
                    try
                    {
                        string Subject = "SPP - PTSMI [Join Grup Delegator]";
                        DataTable dt_Email = mssql.GetDataTable("SELECT Email, Fullname FROM tblM_User WHERE UserID = '" + _UserID + "'");
                        foreach (DataRow dr in dt_Email.Rows)
                        {
                            string s_Email = aes.Dec(dr["Email"].ToString());
                            string s_Fullname = aes.Dec(dr["Fullname"].ToString());
                            string DelegatorName = dtDel.Rows[0]["DelegatorName"].ToString();
                            RowsAffected = Helper.SendMail(s_Email, Subject, MailComposer.compose_mail_body_member_delegator_baru(s_Email, s_Fullname, DelegatorName));
                        }
                    }
                    catch (Exception) { RowsAffected = 0; }
                    #endregion Notifikasi_ke_Member_Baru
                }

                #endregion JoinToGrup

                return Json(new { Error = false, Message = RowsAffected });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

    }


}
