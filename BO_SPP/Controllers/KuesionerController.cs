using BO_SPP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO_SPP.Common;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using Ganss.XSS;

namespace BO_SPP.Controllers
{
    public class KuesionerController : Controller
    {
        private HtmlSanitizer sani = new HtmlSanitizer();

        public IActionResult DaftarKuesioner()
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                return RedirectToAction("Index", "Dashboard");

            ViewData["CurrentControllerName"] = "Kuesioner";
            ViewData["CurrentActionName"] = "Daftar Kuesioner";
            ViewData["Title"] = "Daftar Kuesioner Pengaduan";

            ViewData["Email"] = StringCipher.Decrypt(aes.Dec(HttpContext.Session.GetString("Email")));

            ViewBag.Email = StringCipher.Decrypt(aes.Dec(HttpContext.Session.GetString("Email")));
            ViewBag.Role = HttpContext.Session.GetString("fr");

            return View();
        }

        public IActionResult FormBuilder()
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                return RedirectToAction("Index", "Dashboard");

            ViewData["CurrentControllerName"] = "Kuesioner";
            ViewData["CurrentActionName"] = "Daftar Kuesioner";
            ViewData["Title"] = "Kuesioner Builder Forms";

            ViewData["Email"] = StringCipher.Decrypt(aes.Dec(HttpContext.Session.GetString("Email")));

            ViewBag.Email = StringCipher.Decrypt(aes.Dec(HttpContext.Session.GetString("Email")));
            ViewBag.Role = HttpContext.Session.GetString("fr");

            return View();
        }

        [HttpPost]
        public IActionResult Get_Kuesioner()
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                DataTable dt = mssql.GetDataTable("sp_Get_Kuesioner");
                List<Kuesioner> MainData = new List<Kuesioner>();
                MainData = mssql.ConvertDataTable<Kuesioner>(dt);
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
        public IActionResult Get_Kuesioner_Detail_By_IDHeader(string IDHeader)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                IDHeader = sani.Sanitize(IDHeader);

                string _ID = StringCipher.Decrypt(IDHeader.Split("|")[0]);
                string SessionIDDesc = IDHeader.Split("|")[1];

                if (SessionIDDesc != HttpContext.Session.GetString("SessionID"))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@IDHeader", _ID));
                DataTable dt = mssql.GetDataTable("sp_Get_Kuesioner_Detail_By_IDHeader", param);
                List<KuesionerDetail> MainData = new List<KuesionerDetail>();
                MainData = mssql.ConvertDataTable<KuesionerDetail>(dt);
                for (int i = 0; i < MainData.Count; i++)
                {
                    MainData[i].ID = StringCipher.Encrypt(MainData[i].ID) + "|" + HttpContext.Session.GetString("SessionID");
                    MainData[i].IDHeader = StringCipher.Encrypt(MainData[i].IDHeader) + "|" + HttpContext.Session.GetString("SessionID");
                }
                return Json(new { Error = false, Message = MainData });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Get_Kuesioner_Detail_Options_By_IDHeader(string IDHeader)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                IDHeader = sani.Sanitize(IDHeader);

                string _ID = StringCipher.Decrypt(IDHeader.Split("|")[0]);
                string SessionIDDesc = IDHeader.Split("|")[1];

                if (SessionIDDesc != HttpContext.Session.GetString("SessionID"))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@IDHeader", _ID));
                DataTable dt = mssql.GetDataTable("sp_Get_Kuesioner_Detail_Options_By_IDHeader", param);
                List<KuesionerDetailOptions> MainData = new List<KuesionerDetailOptions>();
                MainData = mssql.ConvertDataTable<KuesionerDetailOptions>(dt);
                return Json(new { Error = false, Message = MainData });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult SaveKuesioner(Kuesioner Model)
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

                #region Save

                string StartDate = !string.IsNullOrEmpty(sani.Sanitize(Model.StartDate)) ? Convert.ToDateTime(sani.Sanitize(Model.StartDate)).ToString("yyyy-MM-dd") : "";
                string EndDate = !string.IsNullOrEmpty(sani.Sanitize(Model.EndDate)) ? Convert.ToDateTime(sani.Sanitize(Model.EndDate)).ToString("yyyy-MM-dd") : "";

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@Action", Action));
                param.Add(new SqlParameter("@ID", ID));
                param.Add(new SqlParameter("@Title", sani.Sanitize(Model.Title)));
                param.Add(new SqlParameter("@StartDate", StartDate));
                param.Add(new SqlParameter("@EndDate", EndDate));
                param.Add(new SqlParameter("@Status", sani.Sanitize(Model.Status)));
                param.Add(new SqlParameter("@CreatedBy", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));

                mssql.ExecuteNonQuery("sp_Save_Kuesioner", param);
                #endregion Save

                return Json(new { Error = false, Message = StringCipher.Encrypt(ID) + '|' + HttpContext.Session.GetString("SessionID") });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult SaveKuesionerDetail(KuesionerDetail Model)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                string SanitizedID = sani.Sanitize(Model.IDHeader);
                string ID = Guid.NewGuid().ToString();
                string IDHeader = StringCipher.Decrypt(SanitizedID.Split("|")[0]);
                if (SanitizedID.Split("|")[1] != HttpContext.Session.GetString("SessionID"))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                #region Save

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", ID));
                param.Add(new SqlParameter("@IDHeader", IDHeader));
                param.Add(new SqlParameter("@Num", Model.Num));
                param.Add(new SqlParameter("@InputType", sani.Sanitize(Model.InputType)));
                param.Add(new SqlParameter("@Label", sani.Sanitize(Model.Label)));
                param.Add(new SqlParameter("@Required", Model.Required));
                param.Add(new SqlParameter("@CreatedBy", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));

                mssql.ExecuteNonQuery("sp_Save_KuesionerDetail", param);
                #endregion Save

                return Json(new { Error = false, Message = StringCipher.Encrypt(ID) + '|' + HttpContext.Session.GetString("SessionID") });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult Save_KuesionerDetailOptions(KuesionerDetailOptions Model)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                string SanitizedID = sani.Sanitize(Model.IDHeader);
                string ID = Guid.NewGuid().ToString();
                string IDHeader = StringCipher.Decrypt(SanitizedID.Split("|")[0]);
                if (SanitizedID.Split("|")[1] != HttpContext.Session.GetString("SessionID"))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                #region Save

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", ID));
                param.Add(new SqlParameter("@IDHeader", IDHeader));
                param.Add(new SqlParameter("@Num", Model.Num));
                param.Add(new SqlParameter("@Options", sani.Sanitize(Model.Options).Trim()));
                param.Add(new SqlParameter("@CreatedBy", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));

                mssql.ExecuteNonQuery("sp_Save_KuesionerDetailOptions", param);
                #endregion Save

                return Json(new { Error = false, Message = StringCipher.Encrypt(ID) + '|' + HttpContext.Session.GetString("SessionID") });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Get_Aktif_Kuesioner()
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                DataTable dt = mssql.GetDataTable("sp_Get_Aktif_Kuesioner");
                List<KuesionerDetail> MainData = new List<KuesionerDetail>();
                MainData = mssql.ConvertDataTable<KuesionerDetail>(dt);
                for (int i = 0; i < MainData.Count; i++)
                {
                    MainData[i].IDKuesioner = StringCipher.Encrypt(MainData[i].IDKuesioner) + "|" + HttpContext.Session.GetString("SessionID");
                    MainData[i].ID = StringCipher.Encrypt(MainData[i].ID) + "|" + HttpContext.Session.GetString("SessionID");
                    MainData[i].IDHeader = StringCipher.Encrypt(MainData[i].IDHeader) + "|" + HttpContext.Session.GetString("SessionID");
                }
                return Json(new { Error = false, Message = MainData });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult Save_KuesionerValue(KuesionerValue Model)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                string SanitizedID = sani.Sanitize(Model.IDPengaduan);
                string ID = Guid.NewGuid().ToString();
                string IDPengaduan = StringCipher.Decrypt(SanitizedID.Split("|")[0]);
                if (SanitizedID.Split("|")[1] != HttpContext.Session.GetString("SessionID"))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                #region Save

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", ID));
                param.Add(new SqlParameter("@IDPengaduan", IDPengaduan));
                param.Add(new SqlParameter("@Title", sani.Sanitize(Model.Title)));
                param.Add(new SqlParameter("@Num", Model.Num));
                param.Add(new SqlParameter("@InputType", sani.Sanitize(Model.InputType)));
                param.Add(new SqlParameter("@Label", sani.Sanitize(Model.Label)));
                param.Add(new SqlParameter("@Required", Model.Required));
                param.Add(new SqlParameter("@Options", sani.Sanitize(Model.Options)));
                param.Add(new SqlParameter("@InputValue", sani.Sanitize(Model.InputValue)));
                param.Add(new SqlParameter("@CreatedBy", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));

                mssql.ExecuteNonQuery("sp_Save_KuesionerValue", param);
                #endregion Save

                return Json(new { Error = false, Message = StringCipher.Encrypt(ID) + '|' + HttpContext.Session.GetString("SessionID") });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Get_KuesionerValue_By_IDPengaduan(string IDPengaduan)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                IDPengaduan = sani.Sanitize(IDPengaduan);

                string _ID = StringCipher.Decrypt(IDPengaduan.Split("|")[0]);
                string SessionIDDesc = IDPengaduan.Split("|")[1];

                if (SessionIDDesc != HttpContext.Session.GetString("SessionID"))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@IDPengaduan", _ID));
                DataTable dt = mssql.GetDataTable("sp_Get_KuesionerValue_By_IDPengaduan", param);
                List<KuesionerValue> MainData = new List<KuesionerValue>();
                MainData = mssql.ConvertDataTable<KuesionerValue>(dt);
                for (int i = 0; i < MainData.Count; i++)
                {
                    MainData[i].ID = StringCipher.Encrypt(MainData[i].ID) + "|" + HttpContext.Session.GetString("SessionID");
                    MainData[i].IDPengaduan = StringCipher.Encrypt(MainData[i].IDPengaduan) + "|" + HttpContext.Session.GetString("SessionID");
                }
                return Json(new { Error = false, Message = MainData });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }
    }
}
