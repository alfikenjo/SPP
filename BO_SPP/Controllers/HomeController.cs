using BO_SPP.Common;
using BO_SPP.Models;
using FluentFTP;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;

namespace BO_SPP.Controllers
{
    public class HomeController : Controller
    {
        static readonly string ftpAddress = ConfigurationManager.AppSetting["FileConfiguration:ftpAddress"];
        static readonly string ftpUsername = ConfigurationManager.AppSetting["FileConfiguration:ftpUsername"];
        static readonly string ftpPassword = ConfigurationManager.AppSetting["FileConfiguration:ftpPassword"];
        static readonly string FolderPath = ConfigurationManager.AppSetting["FileConfiguration:FolderPath"];

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Dashboard");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        [DisableRequestSizeLimit]
        public IActionResult GetFile(string Filename, string Extension)
        {
            try
            {
                if (string.IsNullOrEmpty(StringCipher.Decrypt(HttpContext.Session.GetString("Email"))))
                    return RedirectToAction("Index", "Dashboard");

                if (!Helper.GrantedFile(Filename, StringCipher.Decrypt(HttpContext.Session.GetString("Email"))))
                    throw new Exception("Request is denied, you are not authorized to access this page");

                byte[] bytes;
                using (var conn = new FtpClient(ftpAddress, ftpUsername, ftpPassword))
                {
                    conn.Connect();
                    // open an read-only stream to the file
                    using (var istream = conn.OpenRead(FolderPath + Filename))
                    {
                        try
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                istream.CopyTo(memoryStream);
                                bytes = memoryStream.ToArray();
                            }
                        }
                        finally
                        {
                            istream.Close();
                        }
                    }
                }

                MemoryStream ms = new MemoryStream(bytes);
                Response.Headers.Add("Content-Disposition", "attachment; filename=" + Filename);
                return File(ms, "application/" + Extension.Replace(".", ""));
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }



        }

        [HttpGet]
        [DisableRequestSizeLimit]
        public IActionResult ShowFile(string Filename, string Extension)
        {
            try
            {
                if (string.IsNullOrEmpty(StringCipher.Decrypt(HttpContext.Session.GetString("Email"))))
                    return RedirectToAction("Index", "Dashboard");

                if (!Helper.GrantedFile(Filename, StringCipher.Decrypt(HttpContext.Session.GetString("Email"))))
                    throw new Exception("Request is denied, you are not authorized to access this page");

                byte[] bytes;
                using (var conn = new FtpClient(ftpAddress, ftpUsername, ftpPassword))
                {
                    conn.Connect();
                    // open an read-only stream to the file
                    using (var istream = conn.OpenRead(FolderPath + Filename))
                    {
                        try
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                istream.CopyTo(memoryStream);
                                bytes = memoryStream.ToArray();
                            }
                        }
                        finally
                        {
                            istream.Close();
                        }
                    }
                }

                MemoryStream ms = new MemoryStream(bytes);
                Response.Headers.Add("Content-Disposition", "inline; filename=" + Filename);
                return File(ms, "application/" + Extension.Replace(".", ""));
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }


        }

        [HttpGet]
        [DisableRequestSizeLimit]
        public IActionResult sef(string Filename, string Extension)
        {

            if (Extension.Length > 0)
                Extension = Extension.Trim().ToLower().Replace(".", "");

            try
            {
                if (string.IsNullOrEmpty(StringCipher.Decrypt(HttpContext.Session.GetString("Email"))))
                    return RedirectToAction("Index", "Dashboard");

                if (!Helper.GrantedFile(Filename, StringCipher.Decrypt(HttpContext.Session.GetString("Email"))))
                    throw new Exception("Request is denied, you are not authorized to access this page");

                string mimeType = MimeMapping.GetMimeMapping(Filename + "." + Extension);
                MemoryStream ms = new MemoryStream(Helper.FileDecryptionToByte(Filename));
                Response.Headers.Add("Content-Disposition", "inline; filename=" + Filename + "." + Extension);
                return File(ms, mimeType);
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpGet]
        [DisableRequestSizeLimit]
        public IActionResult gef(string Filename, string Extension)
        {

            if (Extension.Length > 0)
                Extension = Extension.Trim().ToLower().Replace(".", "");

            try
            {
                if (string.IsNullOrEmpty(StringCipher.Decrypt(HttpContext.Session.GetString("Email"))))
                    return RedirectToAction("Index", "Dashboard");

                if (!Helper.GrantedFile(Filename, StringCipher.Decrypt(HttpContext.Session.GetString("Email"))))
                    throw new Exception("Request is denied, you are not authorized to access this page");

                string mimeType = MimeMapping.GetMimeMapping(Filename + "." + Extension);
                MemoryStream ms = new MemoryStream(Helper.FileDecryptionToByte(Filename));
                Response.Headers.Add("Content-Disposition", "attachment; filename=" + Filename + "." + Extension);
                return File(ms, mimeType);
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }
    }
}
