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
using ClosedXML.Extensions;

namespace BO_SPP.Controllers
{
    public class LaporanController : Controller
    {
        private HtmlSanitizer sani = new HtmlSanitizer();

        public IActionResult Index()
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                return RedirectToAction("Index", "Dashboard");

            ViewData["CurrentControllerName"] = "Laporan";
            ViewData["CurrentActionName"] = "";
            ViewData["Title"] = "Laporan Pengaduan";
            ViewData["Email"] = StringCipher.Decrypt(HttpContext.Session.GetString("Email"));

            ViewBag.Email = StringCipher.Decrypt(HttpContext.Session.GetString("Email"));
            ViewBag.Role = HttpContext.Session.GetString("fr");

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

            string Start = DateTime.Now.ToString("yyyy-MM-dd");
            string End = DateTime.Now.ToString("yyyy-MM-dd");

            DataTable dtPeriode = mssql.GetDataTable("SELECT TOP 1 dbo.FormatDate_yyyyMMdd(CreatedOn) [CreatedOn] FROM tblT_Dumas WHERE Nomor IS NOT NULL ORDER BY CreatedOn ASC");
            if (dtPeriode.Rows.Count == 1)
                Start = dtPeriode.Rows[0]["CreatedOn"].ToString();

            ViewBag.Start = Start;
            ViewBag.End = End;

            return View();
        }

        public IActionResult Overdue()
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                return RedirectToAction("Index", "Dashboard");

            ViewData["CurrentControllerName"] = "Laporan";
            ViewData["CurrentActionName"] = "";
            ViewData["Title"] = "Laporan Overdue";
            ViewData["Email"] = StringCipher.Decrypt(HttpContext.Session.GetString("Email"));

            ViewBag.Email = StringCipher.Decrypt(HttpContext.Session.GetString("Email"));
            ViewBag.Role = HttpContext.Session.GetString("fr");

            return View();
        }

        [HttpPost]
        public IActionResult Get_Laporan_1(string Start, string End)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='/'");

                string From = Convert.ToDateTime(sani.Sanitize(Start)).ToString("yyyy-MM-dd");
                string To = Convert.ToDateTime(sani.Sanitize(End)).ToString("yyyy-MM-dd");

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@Email", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));
                param.Add(new SqlParameter("@Start", From));
                param.Add(new SqlParameter("@End", To));
                DataTable dt = mssql.GetDataTable("sp_Get_Laporan_1", param);
                List<Laporan> MainData = new List<Laporan>();
                MainData = mssql.ConvertDataTable<Laporan>(dt);
                return Json(new { Error = false, Message = MainData });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Get_Laporan_2(string Start, string End)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='/'");

                string From = Convert.ToDateTime(sani.Sanitize(Start)).ToString("yyyy-MM-dd");
                string To = Convert.ToDateTime(sani.Sanitize(End)).ToString("yyyy-MM-dd");

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@Email", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));
                param.Add(new SqlParameter("@Start", From));
                param.Add(new SqlParameter("@End", To));
                DataTable dt = mssql.GetDataTable("sp_Get_Laporan_2", param);
                List<Laporan> MainData = new List<Laporan>();
                MainData = mssql.ConvertDataTable<Laporan>(dt);
                return Json(new { Error = false, Message = MainData });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpGet]
        public ActionResult Download(string Start, string End)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    return RedirectToAction("Index", "Dashboard");

                using var wb = GenerateExcel(Start, End);
                return wb.Deliver(DateTime.Now.ToString("yyyyMMdd") + "_Laporan_SPP.xlsx");
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        private ClosedXML.Excel.XLWorkbook GenerateExcel(string Start, string End)
        {
            string From = Convert.ToDateTime(sani.Sanitize(Start)).ToString("yyyy-MM-dd");
            string To = Convert.ToDateTime(sani.Sanitize(End)).ToString("yyyy-MM-dd");

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Email", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));
            param.Add(new SqlParameter("@Start", From));
            param.Add(new SqlParameter("@End", To));
            DataTable dt = mssql.GetDataTable("sp_Get_Laporan_1", param);

            List<SqlParameter> param2 = new List<SqlParameter>();
            param2.Add(new SqlParameter("@Email", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));
            param2.Add(new SqlParameter("@Start", From));
            param2.Add(new SqlParameter("@End", To));
            DataTable dt2 = mssql.GetDataTable("sp_Get_Laporan_2", param2);

            var wb = new ClosedXML.Excel.XLWorkbook();
            var KP = dt;
            var KP2 = dt2;

            var ws1 = wb.AddWorksheet(KP, "Rekapitulasi Pengaduan");
            ws1.Style.Alignment.Indent = 1; ws1.Style.Alignment.Vertical = ClosedXML.Excel.XLAlignmentVerticalValues.Center;
            ws1.Columns().AdjustToContents();

            var ws2 = wb.AddWorksheet(KP2, "Rekap by Delegator");
            ws2.Style.Alignment.Indent = 1; ws1.Style.Alignment.Vertical = ClosedXML.Excel.XLAlignmentVerticalValues.Center;
            ws2.Columns().AdjustToContents();

            return wb;
        }

        [HttpPost]
        public IActionResult Get_Overdue_90()
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='/'");

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@Email", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));                
                DataTable dt = mssql.GetDataTable("sp_Get_Overdue_90", param);
                List<OverdueReport> MainData = new List<OverdueReport>();
                MainData = mssql.ConvertDataTable<OverdueReport>(dt);
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
        public IActionResult Get_Overdue_14()
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='/'");

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@Email", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));
                DataTable dt = mssql.GetDataTable("sp_Get_Overdue_14", param);
                List<OverdueReport> MainData = new List<OverdueReport>();
                MainData = mssql.ConvertDataTable<OverdueReport>(dt);
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
    }
}
