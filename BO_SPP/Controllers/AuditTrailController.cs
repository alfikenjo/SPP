using BO_SPP.Common;
using BO_SPP.Models;
using ClosedXML.Extensions;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace BO_SPP.Controllers
{
    public class AuditTrailController : Controller
    {

        public IActionResult Index()
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                return RedirectToAction("Index", "Dashboard");

            ViewData["CurrentControllerName"] = "Administrator";
            ViewData["CurrentActionName"] = "Audit Trail";
            ViewData["Title"] = "Audit Trail";
            return View();
        }

        [HttpPost]
        public IActionResult GetAuditTrail()
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                DataTable dt = mssql.GetDataTable("SELECT dbo.Format24DateTime([Datetime]) [Datetime], Username, Menu, Halaman, Item, Action, [Description] FROM AuditTrail ORDER BY [Datetime] DESC");
                List<AuditTrail> MainData = new List<AuditTrail>();
                MainData = mssql.ConvertDataTable<AuditTrail>(dt);
                return Json(new { Error = false, Message = MainData });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpGet]
        public ActionResult Download()
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    return RedirectToAction("Index", "Dashboard");

                using var wb = GenerateExcelAuditTrail();
                return wb.Deliver(DateTime.Now.ToString("yyyyMMdd") + "_AuditTrail.xlsx");
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });

            }
        }

        private ClosedXML.Excel.XLWorkbook GenerateExcelAuditTrail()
        {
            DataTable dt = mssql.GetDataTable("SELECT dbo.Format24DateTime([Datetime]) [Datetime], Username, Menu, Halaman, Item, Action, [Description] FROM AuditTrail ORDER BY [Datetime] DESC");
            foreach (DataRow dr in dt.Rows)
            {
                dr["Username"] = !string.IsNullOrEmpty(dr["Username"].ToString()) ? aes.Dec(dr["Username"].ToString()) : "";
                dt.AcceptChanges();
            }

            var wb = new ClosedXML.Excel.XLWorkbook();
            var KP = dt;

            var ws1 = wb.AddWorksheet(KP, "User Audit Trail");

            ws1.Style.Alignment.Indent = 1; ws1.Style.Alignment.Vertical = ClosedXML.Excel.XLAlignmentVerticalValues.Center;

            ws1.Columns().AdjustToContents();

            return wb;
        }

        [HttpGet]
        public ActionResult DownloadPDF()
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    return RedirectToAction("Index", "Dashboard");

                DataTable dt = mssql.GetDataTable("SELECT dbo.Format24DateTime([Datetime]) [Datetime], Username, Menu, Halaman, Item, Action, [Description] FROM AuditTrail ORDER BY [Datetime] DESC");
                foreach (DataRow dr in dt.Rows)
                {
                    dr["Username"] = !string.IsNullOrEmpty(dr["Username"].ToString()) ? aes.Dec(dr["Username"].ToString()) : "";
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

        private void ExportToPdf(DataTable products)
        {

            if (products.Rows.Count > 0)
            {
                int pdfRowIndex = 1;
                string filename = DateTime.Now.ToString("yyyyMMdd") + "_AuditTrail";
                string filepath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\repo", filename + ".pdf");
                Document document = new Document();
                document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());

                var table = new PdfPTable(7); // table with 7 columns
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
                title = new Paragraph("SPP PTSMI - USER AUDIT TRAIL", titleFont);
                title.Alignment = Element.ALIGN_CENTER;
                document.Add(title);
                text = new Paragraph("Printed On: " + DateTime.Now.ToString("dd MMM yyyy HH:mm"), regularFont);
                text.Alignment = Element.ALIGN_CENTER;
                document.Add(text);

                document.Add(new Paragraph("\n\n"));

                table.AddCell(new Phrase("Timestamp", font1));
                table.AddCell(new Phrase("Username", font1));
                table.AddCell(new Phrase("Menu", font1));
                table.AddCell(new Phrase("Halaman", font1));
                table.AddCell(new Phrase("Item", font1));
                table.AddCell(new Phrase("Action", font1));
                table.AddCell(new Phrase("Description", font1));

                table.HeaderRows = 1;

                foreach (DataRow data in products.Rows)
                {
                    table.AddCell(new Phrase(data["Datetime"].ToString(), font2));
                    table.AddCell(new Phrase(data["Username"].ToString(), font2));
                    table.AddCell(new Phrase(data["Menu"].ToString(), font2));
                    table.AddCell(new Phrase(data["Halaman"].ToString(), font2));
                    table.AddCell(new Phrase(data["Item"].ToString(), font2));
                    table.AddCell(new Phrase(data["Action"].ToString(), font2));
                    table.AddCell(new Phrase(data["Description"].ToString(), font2));

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

    }
}
