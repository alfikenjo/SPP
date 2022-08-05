using BO_SPP.Common;
using BO_SPP.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Data.SqlClient;

namespace BO_SPP.Controllers
{

    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(ILogger<DashboardController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
                return RedirectToAction("Signin", "Account");

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("SessionID")) || string.IsNullOrEmpty(HttpContext.Session.GetString("UserID")))
                return RedirectToAction("Signin", "Account");
            else
            {
                DataRow dr_Login = mssql.GetDataRow("SELECT COUNT(*) [Jumlah] FROM tblT_User_Login WHERE UserID = '" + StringCipher.Decrypt(HttpContext.Session.GetString("UserID")) + "' AND ID = '" + StringCipher.Decrypt(HttpContext.Session.GetString("SessionID")) + "' AND isActive = 1");
                if (int.Parse(dr_Login["Jumlah"].ToString()) == 0)
                    return RedirectToAction("Signin", "Account");
            }

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("msg")))
            {
                ViewBag.msg = HttpContext.Session.GetString("msg");
                HttpContext.Session.Remove("msg");
            }

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Email", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));
            DataTable dt = mssql.GetDataTable("sp_Get_Dashboard_Internal_By_Email", param);

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

            ViewData["CurrentControllerName"] = "";
            ViewData["CurrentActionName"] = "Dashboard";
            ViewData["Title"] = "Dashboard";
            ViewData["Email"] = StringCipher.Decrypt(HttpContext.Session.GetString("Email"));
            ViewBag.Role = HttpContext.Session.GetString("fr");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
