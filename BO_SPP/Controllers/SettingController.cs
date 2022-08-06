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
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;

namespace BO_SPP.Controllers
{

    public class SettingController : Controller
    {
        private HtmlSanitizer sani = new HtmlSanitizer();

        public IActionResult FileEkstensionFilter()
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                return RedirectToAction("Index", "Dashboard");

            ViewData["CurrentControllerName"] = "Setting";
            ViewData["CurrentActionName"] = "File Ekstension Filter";
            ViewData["Title"] = "Setting > Ekstension Filter";


            List<Referensi> MainData = new List<Referensi>();
            DataTable dtFileUpload = mssql.GetDataTable("SELECT TOP 1 *, 'Last Updated by: ' + ISNULL(Updated_By, Created_By) + ' ' + dbo.Format24DateTime(ISNULL(Updated_On, Created_On)) [UpdatedOn] FROM tblM_Referensi WHERE Type = 'Max Upload Size'");
            if (dtFileUpload.Rows.Count == 1)
                MainData = mssql.ConvertDataTable<Referensi>(dtFileUpload);

            ViewBag.MaxUploadSize = MainData.FirstOrDefault();
            return View();
        }

        [HttpPost]
        public IActionResult GetFileEkstensionFilter()
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='/'");

                DataTable dt = mssql.GetDataTable("sp_Get_FileEkstensionFilter");
                List<FileEkstensionFilter> MainData = new List<FileEkstensionFilter>();
                MainData = mssql.ConvertDataTable<FileEkstensionFilter>(dt);
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
        public IActionResult GetFileEkstensionFilterByID(string ID)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='/'");

                ID = sani.Sanitize(ID);

                string _ID = StringCipher.Decrypt(ID.Split("|")[0]);
                string SessionIDDesc = ID.Split("|")[1];

                if (SessionIDDesc != HttpContext.Session.GetString("SessionID"))
                    throw new Exception("Invalid Authorization|window.location='/'");

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", _ID));
                DataTable dt = mssql.GetDataTable("sp_Get_FileEkstensionFilterByID", param);
                List<FileEkstensionFilter> MainData = new List<FileEkstensionFilter>();
                MainData = mssql.ConvertDataTable<FileEkstensionFilter>(dt);
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
        public ActionResult SaveFileEkstensionFilter(FileEkstensionFilter Model)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='/'");

                string Action = sani.Sanitize(Model.Action);
                string SanitizedID = sani.Sanitize(Model.ID);
                string ID = Guid.NewGuid().ToString();
                if (Action == "add")
                    Model.ID = SanitizedID;

                if (Action != "add")
                {
                    ID = StringCipher.Decrypt(SanitizedID.Split("|")[0]);
                    if (SanitizedID.Split("|")[1] != HttpContext.Session.GetString("SessionID"))
                        throw new Exception("Invalid Authorization|window.location='/'");
                }

                string FileEkstensionFilter = string.Empty;
                if (Action == "edit" || Action == "hapus")
                {
                    DataRow drOld = mssql.GetDataRow("SELECT Name FROM FileEkstensionFilter WHERE ID = '" + ID + "'");
                    FileEkstensionFilter = drOld["Name"].ToString();
                }

                else if (Action == "add")
                {
                    DataRow drIsExist = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM FileEkstensionFilter WHERE Name = '" + sani.Sanitize(Model.Name) + "'");
                    if (int.Parse(drIsExist["Count"].ToString()) > 0)
                        throw new Exception("Proses Ditolak, Ekstension [" + sani.Sanitize(Model.Name) + "] sudah terdaftar sebelumnya");
                }
                else if (Action == "edit")
                {
                    DataRow drIsExist = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM FileEkstensionFilter WHERE Name = '" + sani.Sanitize(Model.Name) + "' AND ID <> '" + ID + "'");
                    if (int.Parse(drIsExist["Count"].ToString()) > 0)
                        throw new Exception("Proses Ditolak, Ekstension  [" + sani.Sanitize(Model.Name) + "] sudah terdaftar sebelumnya");
                }

                #region Save

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@Action", Action));
                param.Add(new SqlParameter("@ID", ID));
                param.Add(new SqlParameter("@Name", sani.Sanitize(Model.Name)));
                param.Add(new SqlParameter("@CreatedBy", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));

                mssql.ExecuteNonQuery("sp_Save_FileEkstensionFilter", param);
                #endregion Save

                return Json(new { Error = false, Message = "" });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        public IActionResult Notification()
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                return RedirectToAction("Index", "Dashboard");

            ViewData["CurrentControllerName"] = "Setting";
            ViewData["CurrentActionName"] = "Notification";
            ViewData["Title"] = "Notification Center";

            ViewData["Email"] = StringCipher.Decrypt(HttpContext.Session.GetString("Email"));

            ViewBag.Email = StringCipher.Decrypt(HttpContext.Session.GetString("Email"));
            ViewBag.Role = HttpContext.Session.GetString("fr");

            return View();
        }

        [HttpPost]
        public ActionResult GetNotificationSetting()
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='/'");

                DataTable dtNotificationSetting = mssql.GetDataTable("EXEC sp_Get_NotificationSetting");
                List<NotificationSetting> NotificationSetting = new List<NotificationSetting>();
                NotificationSetting = mssql.ConvertDataTable<NotificationSetting>(dtNotificationSetting);

                return Json(new { Error = false, Message = NotificationSetting });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult SaveNotificationSetting(NotificationSetting Model)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='/'");

                #region Save           

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@SMTPAddress", sani.Sanitize(Model.SMTPAddress)));
                param.Add(new SqlParameter("@SMTPPort", sani.Sanitize(Model.SMTPPort)));
                param.Add(new SqlParameter("@EmailAddress", sani.Sanitize(Model.EmailAddress)));
                param.Add(new SqlParameter("@Password", sani.Sanitize(Model.Password)));
                param.Add(new SqlParameter("@SenderName", sani.Sanitize(Model.SenderName)));
                param.Add(new SqlParameter("@EnableSSL", Model.EnableSSL));
                param.Add(new SqlParameter("@UseDefaultCredentials", Model.UseDefaultCredentials));
                param.Add(new SqlParameter("@UseAsync", Model.UseAsync));
                param.Add(new SqlParameter("@EnableServices", Model.EnableServices));

                param.Add(new SqlParameter("@NewUser", Model.NewUser));
                param.Add(new SqlParameter("@NewRoleAssignment", Model.NewRoleAssignment));
                param.Add(new SqlParameter("@UserPasswordReset", Model.UserPasswordReset));
                param.Add(new SqlParameter("@Messaging", Model.Messaging));
                param.Add(new SqlParameter("@ReminderServices", Model.ReminderServices));

                param.Add(new SqlParameter("@CreatedBy", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));

                mssql.ExecuteNonQuery("sp_SaveNotificationSetting", param);
                #endregion Save

                return Json(new { Error = false, Message = "" });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        public IActionResult EmailSetting()
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                return RedirectToAction("Index", "Dashboard");

            ViewData["CurrentControllerName"] = "Setting";
            ViewData["CurrentActionName"] = "Email Template";
            ViewData["Title"] = "Setting > Email Template";
            return View();
        }

        [HttpPost]
        public IActionResult GetEmailSetting()
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='/'");

                DataTable dt = mssql.GetDataTable("EXEC sp_Get_EmailSetting");
                List<EmailSetting> MainData = new List<EmailSetting>();
                MainData = mssql.ConvertDataTable<EmailSetting>(dt);
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
        public IActionResult GetEmailSettingByID(string ID)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='/'");

                ID = sani.Sanitize(ID);

                string _ID = StringCipher.Decrypt(ID.Split("|")[0]);
                string SessionIDDesc = ID.Split("|")[1];

                if (SessionIDDesc != HttpContext.Session.GetString("SessionID"))
                    throw new Exception("Invalid Authorization|window.location='/'");

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", _ID));
                DataTable dt = mssql.GetDataTable("sp_Get_EmailSetting_By_ID", param);
                List<EmailSetting> MainData = new List<EmailSetting>();
                MainData = mssql.ConvertDataTable<EmailSetting>(dt);
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
        public ActionResult SaveEmailSetting(EmailSetting Model)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='/'");

                string Action = sani.Sanitize(Model.Action);
                string SanitizedID = sani.Sanitize(Model.ID);
                string ID = Guid.NewGuid().ToString();

                ID = StringCipher.Decrypt(SanitizedID.Split("|")[0]);
                if (SanitizedID.Split("|")[1] != HttpContext.Session.GetString("SessionID"))
                    throw new Exception("Invalid Authorization|window.location='/'");

                #region Save

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@Action", Action));
                param.Add(new SqlParameter("@ID", ID));
                param.Add(new SqlParameter("@Tipe", sani.Sanitize(Model.Tipe)));
                param.Add(new SqlParameter("@Subject", sani.Sanitize(Model.Subject)));
                param.Add(new SqlParameter("@Konten", Model.Konten));
                param.Add(new SqlParameter("@Status", sani.Sanitize(Model.Status)));
                param.Add(new SqlParameter("@CreatedBy", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));

                mssql.ExecuteNonQuery("sp_Save_EmailSetting", param);
                #endregion Save

                return Json(new { Error = false, Message = "" });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult SaveReferensi(Referensi Model)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='/'");

                #region Save

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@Action", sani.Sanitize(Model.Action)));
                param.Add(new SqlParameter("@ID", Model.ID));
                param.Add(new SqlParameter("@Type", sani.Sanitize(Model.Type)));
                param.Add(new SqlParameter("@Value", sani.Sanitize(Model.Value)));
                param.Add(new SqlParameter("@Description", sani.Sanitize(Model.Description)));
                param.Add(new SqlParameter("@CreatedBy", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));

                mssql.ExecuteNonQuery("sp_Save_tblM_Referensi", param);
                #endregion Save

                return Json(new { Error = false, Message = "" });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult CheckFileEkstension(string Eks)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='/'");

                int isValid = 0;
                DataRow dr = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM FileEkstensionFilter WHERE Name = '" + sani.Sanitize(Eks) + "'");
                isValid = int.Parse(dr["Count"].ToString());

                int MaxUploadSize = 1;
                DataTable dtFileUpload = mssql.GetDataTable("SELECT TOP 1 * FROM tblM_Referensi WHERE Type = 'Max Upload Size'");
                if (dtFileUpload.Rows.Count == 1)
                    MaxUploadSize = int.Parse(dtFileUpload.Rows[0]["Value"].ToString());

                return Json(new { Error = false, Message = isValid, MaxUploadSize = MaxUploadSize });
            }
            catch (Exception)
            {
                return Json(new { Error = true, Message = 0 });
            }
        }
    }


}
