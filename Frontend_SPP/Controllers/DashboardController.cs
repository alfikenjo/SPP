using Frontend_SPP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frontend_SPP.Common;
using System.Data;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.IO;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Localization;

namespace Frontend_SPP.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index(string msg)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
                return RedirectToAction("Index", "Home");

            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                throw new Exception("Invalid Authorization|window.location='/'");

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("msg")))
            {
                ViewBag.msg = HttpContext.Session.GetString("msg");
                HttpContext.Session.Remove("msg");
            }
                

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Email", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));
            DataTable dt = mssql.GetDataTable("sp_Get_Dashboard_By_Email", param);

            if (dt.Rows.Count == 1)
            {
                DataRow dr = dt.Rows[0];
                ViewBag.Terkirim = dr["Terkirim"];
                ViewBag.Diproses = dr["Diproses"];
                ViewBag.Selesai = dr["Selesai"];
                ViewBag.Ditolak = dr["Ditolak"];
            }
            else
            {
                ViewBag.Terkirim = 0;
                ViewBag.Diproses = 0;
                ViewBag.Selesai = 0;
                ViewBag.Ditolak = 0;

            }

            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var culture = rqf.RequestCulture.Culture.ToString().ToUpper();
            if (string.IsNullOrEmpty(culture) || culture == "IN")
                culture = "ID";

            HttpContext.Session.SetString("culture", culture.ToLower());

            ViewData["CurrentControllerName"] = "";
            ViewData["CurrentActionName"] = "Dashboard";
            ViewData["Username"] = HttpContext.Session.GetString("Email");
            return View();
        }        
    }
}
