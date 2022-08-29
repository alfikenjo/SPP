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
using Frontend_SPP.Resources.Home;
using System.Data;
using System.Data.SqlClient;
using Ganss.XSS;
using FluentFTP;
using System.IO;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using System.Threading;
using System.Drawing;

namespace Frontend_SPP.Controllers
{
    public class HomeController : Controller
    {
        static readonly string ftpAddress = ConfigurationManager.AppSetting["FileConfiguration:ftpAddress"];
        static readonly string ftpUsername = ConfigurationManager.AppSetting["FileConfiguration:ftpUsername"];
        static readonly string ftpPassword = ConfigurationManager.AppSetting["FileConfiguration:ftpPassword"];
        static readonly string FolderPath = ConfigurationManager.AppSetting["FileConfiguration:FolderPath"];

        private HtmlSanitizer sani = new HtmlSanitizer();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [DisableRequestSizeLimit]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("LoginLock") != null)
            {
                DateTime current = DateTime.Now;
                DateTime locked = Convert.ToDateTime(HttpContext.Session.GetString("LoginLock").ToString()).AddSeconds(10);
                if (current > locked)
                {
                    HttpContext.Session.Remove("LoginLock");
                    HttpContext.Session.Remove("failedAttemp");
                    ViewBag.LoginLock = false;
                }
                else
                    ViewBag.LoginLock = true;
            }
            else
            {
                ViewBag.LoginLock = false;
            }

            ViewData["Fullname"] = null;
            ViewData["Roles"] = null;
            ViewData["Unit"] = null;

            HttpContext.Session.Remove("UserID");
            HttpContext.Session.Remove("Username");
            HttpContext.Session.Remove("Email");
            HttpContext.Session.Remove("fn");
            HttpContext.Session.Remove("fr");
            HttpContext.Session.Remove("SessionID");
            HttpContext.Session.Remove("Img");

            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var culture = rqf.RequestCulture.Culture.ToString().ToUpper();
            if ((string.IsNullOrEmpty(culture) || culture == "IN"))
                culture = "ID";

            HttpContext.Session.SetString("culture", culture.ToLower());

            #region Banner
            List<SqlParameter> paramBanner = new List<SqlParameter>();
            paramBanner.Add(new SqlParameter("@Lang", culture));
            DataTable dtBanner = mssql.GetDataTable("sp_Get_Banner_Aktif", paramBanner);
            List<tblT_Banner> MainData = new List<tblT_Banner>();
            MainData = mssql.ConvertDataTable<tblT_Banner>(dtBanner);
            for (int i = 0; i < MainData.Count; i++)
            {
                MainData[i].ID = StringCipher.Encrypt(MainData[i].ID);

                string Filename = MainData[i].Filename;
                if (!string.IsNullOrEmpty(Filename))
                    MainData[i].Filename = Helper.GetBinaryImageEncrypted(Filename, MainData[i].Ekstension);
            }

            ViewBag.Banner = MainData.FirstOrDefault();
            #endregion Banner

            #region TentangSPP
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Tipe", sani.Sanitize("Tentang SPP")));
            param.Add(new SqlParameter("@Lang", sani.Sanitize(culture)));
            DataTable dt = mssql.GetDataTable("sp_Get_Single_CMS", param);
            List<tblT_CMS> TentangSPP = new List<tblT_CMS>();
            TentangSPP = mssql.ConvertDataTable<tblT_CMS>(dt);
            for (int i = 0; i < TentangSPP.Count; i++)
            {
                TentangSPP[i].ID = StringCipher.Encrypt(TentangSPP[i].ID) + "|" + HttpContext.Session.GetString("SessionID");
                TentangSPP[i].ID_IN = StringCipher.Encrypt(TentangSPP[i].ID_IN) + "|" + HttpContext.Session.GetString("SessionID");

                string Filename = TentangSPP[i].Filename;
                if (!string.IsNullOrEmpty(Filename))
                {
                    string Ekstension = TentangSPP[i].Ekstension.ToLower();
                    if (Ekstension == ".jpg" || Ekstension == ".jpeg" || Ekstension == ".png")
                        TentangSPP[i].Filename = Helper.GetBinaryImageEncrypted(Filename, TentangSPP[i].Ekstension);
                }

                string Filename1 = TentangSPP[i].Filename1;
                if (!string.IsNullOrEmpty(Filename1))
                {
                    string Ekstension1 = TentangSPP[i].Ekstension1.ToLower();
                    if (Ekstension1 == ".jpg" || Ekstension1 == ".jpeg" || Ekstension1 == ".png")
                        TentangSPP[i].Filename1 = Helper.GetBinaryImageEncrypted(Filename1, TentangSPP[i].Ekstension1);
                }

            }
            ViewBag.TentangSPP = TentangSPP.FirstOrDefault();
            #endregion TentangSPP

            #region MediaPenyampaian
            List<SqlParameter> paramMediaPenyampaian = new List<SqlParameter>();
            paramMediaPenyampaian.Add(new SqlParameter("@Tipe", sani.Sanitize("Media Penyampaian")));
            paramMediaPenyampaian.Add(new SqlParameter("@Lang", sani.Sanitize(culture)));
            DataTable dtMediaPenyampaian = mssql.GetDataTable("sp_Get_Single_CMS", paramMediaPenyampaian);
            List<tblT_CMS> MediaPenyampaian = new List<tblT_CMS>();
            MediaPenyampaian = mssql.ConvertDataTable<tblT_CMS>(dtMediaPenyampaian);
            for (int i = 0; i < MediaPenyampaian.Count; i++)
            {
                MediaPenyampaian[i].ID = StringCipher.Encrypt(MediaPenyampaian[i].ID) + "|" + HttpContext.Session.GetString("SessionID");
                MediaPenyampaian[i].ID_IN = StringCipher.Encrypt(MediaPenyampaian[i].ID_IN) + "|" + HttpContext.Session.GetString("SessionID");

                string Filename = MediaPenyampaian[i].Filename;
                if (!string.IsNullOrEmpty(Filename))
                {
                    string Ekstension = MediaPenyampaian[i].Ekstension.ToLower();
                    if (Ekstension == ".jpg" || Ekstension == ".jpeg" || Ekstension == ".png")
                        MediaPenyampaian[i].Filename = Helper.GetBinaryImageEncrypted(Filename, MediaPenyampaian[i].Ekstension);
                }

                string Filename1 = MediaPenyampaian[i].Filename1;
                if (!string.IsNullOrEmpty(Filename1))
                {
                    string Ekstension1 = MediaPenyampaian[i].Ekstension1.ToLower();
                    if (Ekstension1 == ".jpg" || Ekstension1 == ".jpeg" || Ekstension1 == ".png")
                        MediaPenyampaian[i].Filename1 = Helper.GetBinaryImageEncrypted(Filename1, MediaPenyampaian[i].Ekstension);
                }

            }
            ViewBag.MediaPenyampaian = MediaPenyampaian.FirstOrDefault();
            #endregion MediaPenyampaian

            #region ContactUs
            List<SqlParameter> paramContactUs = new List<SqlParameter>();
            paramContactUs.Add(new SqlParameter("@Tipe", sani.Sanitize("Contact Us")));
            paramContactUs.Add(new SqlParameter("@Lang", sani.Sanitize(culture)));
            DataTable dtContactUs = mssql.GetDataTable("sp_Get_Single_CMS", paramContactUs);
            List<tblT_CMS> ContactUs = new List<tblT_CMS>();
            ContactUs = mssql.ConvertDataTable<tblT_CMS>(dtContactUs);
            for (int i = 0; i < ContactUs.Count; i++)
            {
                ContactUs[i].ID = StringCipher.Encrypt(ContactUs[i].ID) + "|" + HttpContext.Session.GetString("SessionID");
                ContactUs[i].ID_IN = StringCipher.Encrypt(ContactUs[i].ID_IN) + "|" + HttpContext.Session.GetString("SessionID");

                string Filename = ContactUs[i].Filename;
                if (!string.IsNullOrEmpty(Filename))
                {
                    string Ekstension = ContactUs[i].Ekstension.ToLower();
                    if (Ekstension == ".jpg" || Ekstension == ".jpeg" || Ekstension == ".png")
                        ContactUs[i].Filename = Helper.GetBinaryImageEncrypted(Filename, ContactUs[i].Ekstension);
                }

                string Filename1 = ContactUs[i].Filename1;
                if (!string.IsNullOrEmpty(Filename1))
                {
                    string Ekstension1 = ContactUs[i].Ekstension1.ToLower();
                    if (Ekstension1 == ".jpg" || Ekstension1 == ".jpeg" || Ekstension1 == ".png")
                        ContactUs[i].Filename1 = Helper.GetBinaryImageEncrypted(Filename1, ContactUs[i].Ekstension1);
                }

            }
            ViewBag.ContactUs = ContactUs.FirstOrDefault();
            #endregion ContactUs

            #region JenisPelanggaran
            List<SqlParameter> paramJenisPelanggaran = new List<SqlParameter>();
            paramJenisPelanggaran.Add(new SqlParameter("@Tipe", sani.Sanitize("Jenis Pelanggaran")));
            paramJenisPelanggaran.Add(new SqlParameter("@Lang", sani.Sanitize(culture)));
            DataTable dtJenisPelanggaran = mssql.GetDataTable("sp_Get_Single_CMS", paramJenisPelanggaran);
            List<tblT_CMS> JenisPelanggaran = new List<tblT_CMS>();
            JenisPelanggaran = mssql.ConvertDataTable<tblT_CMS>(dtJenisPelanggaran);
            for (int i = 0; i < JenisPelanggaran.Count; i++)
            {
                JenisPelanggaran[i].ID = StringCipher.Encrypt(JenisPelanggaran[i].ID) + "|" + HttpContext.Session.GetString("SessionID");
                JenisPelanggaran[i].ID_IN = StringCipher.Encrypt(JenisPelanggaran[i].ID_IN) + "|" + HttpContext.Session.GetString("SessionID");
            }
            ViewBag.JenisPelanggaran = JenisPelanggaran.ToList().OrderBy(ord => ord.s_UpdatedOn);
            #endregion JenisPelanggaran

            #region HeaderJenisPelanggaran
            List<SqlParameter> paramHeaderJenisPelanggaran = new List<SqlParameter>();
            paramHeaderJenisPelanggaran.Add(new SqlParameter("@Tipe", sani.Sanitize("Header Jenis Pelanggaran")));
            paramHeaderJenisPelanggaran.Add(new SqlParameter("@Lang", sani.Sanitize(culture)));
            DataTable dtHeaderJenisPelanggaran = mssql.GetDataTable("sp_Get_Single_CMS", paramHeaderJenisPelanggaran);
            List<tblT_CMS> HeaderJenisPelanggaran = new List<tblT_CMS>();
            HeaderJenisPelanggaran = mssql.ConvertDataTable<tblT_CMS>(dtHeaderJenisPelanggaran);
            for (int i = 0; i < HeaderJenisPelanggaran.Count; i++)
            {
                HeaderJenisPelanggaran[i].ID = StringCipher.Encrypt(HeaderJenisPelanggaran[i].ID) + "|" + HttpContext.Session.GetString("SessionID");
                HeaderJenisPelanggaran[i].ID_IN = StringCipher.Encrypt(HeaderJenisPelanggaran[i].ID_IN) + "|" + HttpContext.Session.GetString("SessionID");
            }
            ViewBag.HeaderJenisPelanggaran = HeaderJenisPelanggaran.FirstOrDefault();
            #endregion HeaderJenisPelanggaran

            #region PilarSPP
            List<SqlParameter> paramPilarSPP = new List<SqlParameter>();
            paramPilarSPP.Add(new SqlParameter("@Tipe", sani.Sanitize("Pilar SPP")));
            paramPilarSPP.Add(new SqlParameter("@Lang", sani.Sanitize(culture)));
            DataTable dtPilarSPP = mssql.GetDataTable("sp_Get_Single_CMS", paramPilarSPP);
            List<tblT_CMS> PilarSPP = new List<tblT_CMS>();
            PilarSPP = mssql.ConvertDataTable<tblT_CMS>(dtPilarSPP);
            for (int i = 0; i < PilarSPP.Count; i++)
            {
                PilarSPP[i].ID = StringCipher.Encrypt(PilarSPP[i].ID) + "|" + HttpContext.Session.GetString("SessionID");
                PilarSPP[i].ID_IN = StringCipher.Encrypt(PilarSPP[i].ID_IN) + "|" + HttpContext.Session.GetString("SessionID");
            }
            ViewBag.PilarSPP = PilarSPP.ToList().Take(3).OrderBy(ord => ord.s_UpdatedOn);
            #endregion PilarSPP

            #region HeaderPilarSPP
            List<SqlParameter> paramHeaderPilarSPP = new List<SqlParameter>();
            paramHeaderPilarSPP.Add(new SqlParameter("@Tipe", sani.Sanitize("Header Pilar SPP")));
            paramHeaderPilarSPP.Add(new SqlParameter("@Lang", sani.Sanitize(culture)));
            DataTable dtHeaderPilarSPP = mssql.GetDataTable("sp_Get_Single_CMS", paramHeaderPilarSPP);
            List<tblT_CMS> HeaderPilarSPP = new List<tblT_CMS>();
            HeaderPilarSPP = mssql.ConvertDataTable<tblT_CMS>(dtHeaderPilarSPP);
            for (int i = 0; i < HeaderPilarSPP.Count; i++)
            {
                HeaderPilarSPP[i].ID = StringCipher.Encrypt(HeaderPilarSPP[i].ID) + "|" + HttpContext.Session.GetString("SessionID");
                HeaderPilarSPP[i].ID_IN = StringCipher.Encrypt(HeaderPilarSPP[i].ID_IN) + "|" + HttpContext.Session.GetString("SessionID");
            }
            ViewBag.HeaderPilarSPP = HeaderPilarSPP.FirstOrDefault();
            #endregion HeaderPilarSPP

            #region FAQ
            List<SqlParameter> paramFAQ = new List<SqlParameter>();
            paramFAQ.Add(new SqlParameter("@Tipe", sani.Sanitize("FAQ")));
            paramFAQ.Add(new SqlParameter("@Lang", sani.Sanitize(culture)));
            DataTable dtFAQ = mssql.GetDataTable("sp_Get_Single_CMS", paramFAQ);
            List<tblT_CMS> FAQ = new List<tblT_CMS>();
            FAQ = mssql.ConvertDataTable<tblT_CMS>(dtFAQ);
            for (int i = 0; i < FAQ.Count; i++)
            {
                FAQ[i].ID = StringCipher.Encrypt(FAQ[i].ID) + "|" + HttpContext.Session.GetString("SessionID");
                FAQ[i].ID_IN = StringCipher.Encrypt(FAQ[i].ID_IN) + "|" + HttpContext.Session.GetString("SessionID");
            }
            ViewBag.FAQ = FAQ.ToList().OrderBy(ord => ord.s_UpdatedOn);
            #endregion FAQ

            #region HeaderFAQ
            List<SqlParameter> paramHeaderFAQ = new List<SqlParameter>();
            paramHeaderFAQ.Add(new SqlParameter("@Tipe", sani.Sanitize("Header FAQ")));
            paramHeaderFAQ.Add(new SqlParameter("@Lang", sani.Sanitize(culture)));
            DataTable dtHeaderFAQ = mssql.GetDataTable("sp_Get_Single_CMS", paramHeaderFAQ);
            List<tblT_CMS> HeaderFAQ = new List<tblT_CMS>();
            HeaderFAQ = mssql.ConvertDataTable<tblT_CMS>(dtHeaderFAQ);
            for (int i = 0; i < HeaderFAQ.Count; i++)
            {
                HeaderFAQ[i].ID = StringCipher.Encrypt(HeaderFAQ[i].ID) + "|" + HttpContext.Session.GetString("SessionID");
                HeaderFAQ[i].ID_IN = StringCipher.Encrypt(HeaderFAQ[i].ID_IN) + "|" + HttpContext.Session.GetString("SessionID");
            }
            ViewBag.HeaderFAQ = HeaderFAQ.FirstOrDefault();
            #endregion HeaderFAQ

            #region Chart_3
            List<SqlParameter> paramChart_3 = new List<SqlParameter>();
            paramChart_3.Add(new SqlParameter("@StartYear", 2020));
            paramChart_3.Add(new SqlParameter("@EndYear", 2030));
            paramChart_3.Add(new SqlParameter("@StartMonth", 1));
            paramChart_3.Add(new SqlParameter("@EndMonth", 12));
            paramChart_3.Add(new SqlParameter("@Lang", culture));
            DataTable dtChart_3 = mssql.GetDataTable("sp_get_DetailChart_3", paramChart_3);

            List<Model_Laporan> Chart_3 = new List<Model_Laporan>();
            Chart_3 = mssql.ConvertDataTable<Model_Laporan>(dtChart_3);
            ViewBag.Chart_3 = Chart_3.ToList().OrderByDescending(ord => ord.Jumlah);
            #endregion Chart_3

            #region SyaratDanKetentuan
            List<SqlParameter> paramSyaratDanKetentuan = new List<SqlParameter>();
            paramSyaratDanKetentuan.Add(new SqlParameter("@Tipe", sani.Sanitize("Syarat dan Ketentuan")));
            paramSyaratDanKetentuan.Add(new SqlParameter("@Lang", sani.Sanitize(culture)));
            DataTable dtSyaratDanKetentuan = mssql.GetDataTable("sp_Get_Single_CMS", paramSyaratDanKetentuan);
            List<tblT_CMS> SyaratDanKetentuan = new List<tblT_CMS>();
            SyaratDanKetentuan = mssql.ConvertDataTable<tblT_CMS>(dtSyaratDanKetentuan);
            for (int i = 0; i < SyaratDanKetentuan.Count; i++)
            {
                SyaratDanKetentuan[i].ID = StringCipher.Encrypt(SyaratDanKetentuan[i].ID) + "|" + HttpContext.Session.GetString("SessionID");
                SyaratDanKetentuan[i].ID_IN = StringCipher.Encrypt(SyaratDanKetentuan[i].ID_IN) + "|" + HttpContext.Session.GetString("SessionID");
            }
            ViewBag.SyaratDanKetentuan = SyaratDanKetentuan.FirstOrDefault();
            #endregion SyaratDanKetentuan

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult FillCaptcha()
        {
            try
            {
                Random random = new Random();
                string combination = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                StringBuilder captcha = new StringBuilder();
                string ImageUrl = "";
                for (int i = 0; i < 6; i++)
                {
                    captcha.Append(combination[random.Next(combination.Length)]);
                    HttpContext.Session.SetString("captcha", captcha.ToString());
                    ImageUrl = Helper.GetImageCaptcha(DateTime.Now.Ticks.ToString(), captcha.ToString());
                }
                return Json(new { Error = false, Message = ImageUrl });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetFile(string Filename, string Extension)
        {
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

        [HttpGet]
        [DisableRequestSizeLimit]
        public IActionResult ShowFile(string Filename, string Extension)
        {
            try
            {
                if (string.IsNullOrEmpty(StringCipher.Decrypt(HttpContext.Session.GetString("Email"))))
                    return RedirectToAction("Index", "Home");

                int Authenticated = 0;
                string Email = StringCipher.Decrypt(HttpContext.Session.GetString("Email"));

                if (!Helper.CheckUserActive(Email))
                    throw new Exception("Request is denied, you are not authorized to access this page");

                DataRow drFilePengaduan = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM tblT_File_Evidence WHERE FileEvidence = '" + Filename + "' AND CreatedBy = '" + Email + "';");
                Authenticated = Authenticated + int.Parse(drFilePengaduan["Count"].ToString());

                DataRow drFileIdentitas = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM tblT_Dumas_Detail WHERE FileIdentitas = '" + Filename + "' AND CreatedBy = '" + Email + "';");
                Authenticated = Authenticated + int.Parse(drFileIdentitas["Count"].ToString());

                DataRow drFileTanggapanPelapor = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM tblT_Tanggapan WHERE TipePengirim = 'Pelapor' AND Email = '" + Email + "' AND FileLampiran = '" + Filename + "'");
                Authenticated = Authenticated + int.Parse(drFileTanggapanPelapor["Count"].ToString());

                DataRow drFileTanggapanAdminSPP = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM tblT_Tanggapan A JOIN tblT_Dumas B ON A.IDPengaduan = B.ID WHERE B.Email = '" + Email + "' AND A.FileLampiran = '" + Filename + "'");
                Authenticated = Authenticated + int.Parse(drFileTanggapanAdminSPP["Count"].ToString());

                //DataRow drFileCMS = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM tblT_CMS WHERE Filename = '" + Filename + "' OR Filename1 = '" + Filename + "'");
                //Authenticated = Authenticated + int.Parse(drFileCMS["Count"].ToString());

                if (Authenticated == 0)
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
        public IActionResult PublicFile(string Filename, string Extension)
        {
            try
            {
                if (Extension.Length > 0)
                    Extension = Extension.Trim().ToLower().Replace(".", "");

                int Authenticated = 0;

                DataRow drFileCMS = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM tblT_CMS WHERE Filename = '" + Filename + "' OR Filename1 = '" + Filename + "'");
                Authenticated = Authenticated + int.Parse(drFileCMS["Count"].ToString());

                if (Authenticated == 0)
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

        [HttpPost]
        public IActionResult Get_Year_Dumas()
        {
            try
            {
                DataTable dt = mssql.GetDataTable("EXEC sp_Get_Year_Dumas");
                List<Referensi> MainData = new List<Referensi>();
                MainData = mssql.ConvertDataTable<Referensi>(dt);
                return Json(new { Error = false, Message = MainData });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Get_Pengaduan_Status_by_Period(int start_year, int start_month, int end_year, int end_month)
        {
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@StartYear", start_year));
                param.Add(new SqlParameter("@StartMonth", start_month));
                param.Add(new SqlParameter("@EndYear", end_year));
                param.Add(new SqlParameter("@EndMonth", end_month));
                DataTable dt = mssql.GetDataTable("sp_Get_Pengaduan_Status_by_Period", param);
                List<Referensi> MainData = new List<Referensi>();
                MainData = mssql.ConvertDataTable<Referensi>(dt);
                return Json(new { Error = false, Message = MainData });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult get_Chart_2_old(int start_year, int end_year, int start_month, int end_month)
        {
            //var culture = HttpContext.Session.GetString("culture");
            try
            {
                //string JSONresult;
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@StartYear", start_year));
                param.Add(new SqlParameter("@EndYear", end_year));
                param.Add(new SqlParameter("@StartMonth", start_month));
                param.Add(new SqlParameter("@EndMonth", end_month));
                DataTable dt = mssql.GetDataTable("sp_get_DetailChart_2", param);

                List<ModelChart2> MainData = new List<ModelChart2>();
                MainData = mssql.ConvertDataTable<ModelChart2>(dt);
                return Json(new { Error = false, Message = MainData });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult get_Chart_3_old(int start_year, int end_year, int start_month, int end_month)
        {
            //var culture = HttpContext.Session.GetString("culture");
            try
            {
                //string JSONresult;
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@StartYear", start_year));
                param.Add(new SqlParameter("@EndYear", end_year));
                param.Add(new SqlParameter("@StartMonth", start_month));
                param.Add(new SqlParameter("@EndMonth", end_month));
                DataTable dt = mssql.GetDataTable("sp_get_DetailChart_3", param);

                List<Model_Laporan> MainData = new List<Model_Laporan>();
                MainData = mssql.ConvertDataTable<Model_Laporan>(dt);
                return Json(new { Error = false, Message = MainData });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult get_Chart_2(int start_year, int end_year, int start_month, int end_month)
        {
            //var culture = HttpContext.Session.GetString("culture");
            try
            {
                //string JSONresult;
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@StartYear", start_year));
                param.Add(new SqlParameter("@EndYear", end_year));
                param.Add(new SqlParameter("@StartMonth", start_month));
                param.Add(new SqlParameter("@EndMonth", end_month));
                DataTable dt = mssql.GetDataTable("sp_get_DetailChart_2", param);

                List<ModelChart2> MainData = new List<ModelChart2>();
                MainData = mssql.ConvertDataTable<ModelChart2>(dt);
                return Json(new { Error = false, Message = MainData });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult get_Chart_3(int start_year, int end_year, int start_month, int end_month)
        {
            var culture = HttpContext.Session.GetString("culture");
            if (string.IsNullOrEmpty(culture))
                culture = "ID";
            try
            {
                //string JSONresult;
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@StartYear", start_year));
                param.Add(new SqlParameter("@EndYear", end_year));
                param.Add(new SqlParameter("@StartMonth", start_month));
                param.Add(new SqlParameter("@EndMonth", end_month));
                param.Add(new SqlParameter("@Lang", culture));
                DataTable dt = mssql.GetDataTable("sp_get_DetailChart_3", param);

                List<Model_Laporan> MainData = new List<Model_Laporan>();
                MainData = mssql.ConvertDataTable<Model_Laporan>(dt);
                ViewBag.Chart_3 = MainData.ToList().OrderByDescending(ord => ord.Jumlah);
                return Json(new { Error = false, Message = MainData });
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

            try
            {
                if (string.IsNullOrEmpty(StringCipher.Decrypt(HttpContext.Session.GetString("Email"))))
                    return RedirectToAction("Index", "Dashboard");

                int Authenticated = 0;
                string Email = StringCipher.Decrypt(HttpContext.Session.GetString("Email"));

                if (!Helper.CheckUserActive(Email))
                    throw new Exception("Request is denied, you are not authorized to access this page");

                DataRow drFilePengaduan = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM tblT_File_Evidence WHERE FileEvidence = '" + Filename + "' AND CreatedBy = '" + Email + "';");
                Authenticated = Authenticated + int.Parse(drFilePengaduan["Count"].ToString());

                DataRow drFilePengaduanByAdmin = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM tblT_File_Evidence A JOIN tblT_Dumas B ON A.ID_Header = B.ID WHERE A.FileEvidence = '" + Filename + "' AND B.Email = '" + Email + "';");
                Authenticated = Authenticated + int.Parse(drFilePengaduanByAdmin["Count"].ToString());

                DataRow drFileIdentitas = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM tblT_Dumas_Detail WHERE FileIdentitas = '" + Filename + "' AND CreatedBy = '" + Email + "';");
                Authenticated = Authenticated + int.Parse(drFileIdentitas["Count"].ToString());

                DataRow drFileTanggapanPelapor = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM tblT_Tanggapan WHERE TipePengirim = 'Pelapor' AND Email = '" + Email + "' AND FileLampiran = '" + Filename + "'");
                Authenticated = Authenticated + int.Parse(drFileTanggapanPelapor["Count"].ToString());

                DataRow drFileTanggapanAdminSPP = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM tblT_Tanggapan A JOIN tblT_Dumas B ON A.IDPengaduan = B.ID AND A.TipePengirim IN ('Admin SPP', 'Pelapor') WHERE B.Email = '" + Email + "' AND A.FileLampiran = '" + Filename + "'");
                Authenticated = Authenticated + int.Parse(drFileTanggapanAdminSPP["Count"].ToString());

                if (Authenticated == 0)
                    throw new Exception("Request is denied, you are not authorized to access this page");

                if (Filename.Contains("."))
                {
                    string[] FileNames = Filename.Split(".");
                    if (FileNames.Length == 2)
                        Filename = FileNames[0].Trim();
                }

                if (Extension.Length > 0)
                    Extension = Extension.Trim().ToLower().Replace(".", "");


                string mimeType = MimeMapping.GetMimeMapping(Filename + "." + Extension);
                MemoryStream ms = new MemoryStream(Helper.FileDecryptionToByte(Filename));
                Response.Headers.Add("Content-Disposition", "inline; filename=" + Filename + "." + Extension);
                return File(ms, mimeType);
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("msg", ex.Message);
                return RedirectToAction("Index", "Dashboard");

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
