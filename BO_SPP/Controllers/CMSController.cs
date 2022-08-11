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
    public class CMSController : Controller
    {
        private HtmlSanitizer sani = new HtmlSanitizer();

        public IActionResult Banner()
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                return RedirectToAction("Index", "Dashboard");

            ViewData["CurrentControllerName"] = "CMS";
            ViewData["CurrentActionName"] = "Banner";
            ViewData["Title"] = "CMS Banner";

            ViewData["Email"] = StringCipher.Decrypt(HttpContext.Session.GetString("Email"));

            ViewBag.Email = StringCipher.Decrypt(HttpContext.Session.GetString("Email"));
            ViewBag.Role = HttpContext.Session.GetString("fr");

            return View();
        }

        [HttpPost]
        public IActionResult GetBanner()
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='/'");

                DataTable dt = mssql.GetDataTable("EXEC sp_Get_Banner");
                List<tblT_Banner> MainData = new List<tblT_Banner>();
                MainData = mssql.ConvertDataTable<tblT_Banner>(dt);
                for (int i = 0; i < MainData.Count; i++)
                {
                    MainData[i].ID = StringCipher.Encrypt(MainData[i].ID) + "|" + HttpContext.Session.GetString("SessionID");

                    string Filename = MainData[i].Filename;
                    if (!string.IsNullOrEmpty(Filename))
                        MainData[i].Filename = Helper.GetBinaryImageEncrypted(Filename, MainData[i].Ekstension);
                }
                return Json(new { Error = false, Message = MainData });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult GetBannerByID(string ID)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='/'");

                string _ID = StringCipher.Decrypt(sani.Sanitize(ID).Split("|")[0]);
                string SessionIDDesc = sani.Sanitize(ID).Split("|")[1];

                if (SessionIDDesc != HttpContext.Session.GetString("SessionID"))
                    throw new Exception("Invalid Authorization|window.location='/'");

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", _ID));
                DataTable dt = mssql.GetDataTable("sp_Get_Banner_ByID", param);
                if (dt.Rows.Count != 1)
                    throw new Exception("Invalid request");

                List<tblT_Banner> MainData = new List<tblT_Banner>();
                MainData = mssql.ConvertDataTable<tblT_Banner>(dt);

                for (int i = 0; i < MainData.Count; i++)
                {
                    MainData[i].ID = StringCipher.Encrypt(MainData[i].ID) + "|" + HttpContext.Session.GetString("SessionID");
                    MainData[i].ID_IN = StringCipher.Encrypt(MainData[i].ID_IN) + "|" + HttpContext.Session.GetString("SessionID");

                    string Filename_ID = MainData[i].Filename_ID;
                    if (!string.IsNullOrEmpty(Filename_ID))
                        MainData[i].Filename_ID = Helper.GetBinaryImageEncrypted(Filename_ID, MainData[i].Ekstension_ID);

                    string Filename_EN = MainData[i].Filename_EN;
                    if (!string.IsNullOrEmpty(Filename_EN))
                        MainData[i].Filename_EN = Helper.GetBinaryImageEncrypted(Filename_EN, MainData[i].Ekstension_EN);
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
        public IActionResult SaveBanner([FromForm] tblT_Banner Model)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='/'");

                string ID = sani.Sanitize(Model.ID);
                string _ID = Guid.NewGuid().ToString();

                if (!string.IsNullOrEmpty(ID))
                {
                    _ID = StringCipher.Decrypt(ID.Split("|")[0]);
                    string SessionIDDesc = ID.Split("|")[1];

                    if (SessionIDDesc != HttpContext.Session.GetString("SessionID"))
                        throw new Exception("Invalid Authorization|window.location='/'");
                }

                if (Model.Upload_ID == null && sani.Sanitize(Model.Action) == "add")
                    throw new Exception("Gambar Banner tidak valid|document.getElementById('nav_IN').click(); document.getElementById('fu_Filename_ID').focus()");

                if (!string.IsNullOrEmpty(sani.Sanitize(Model.Title_ID)) && string.IsNullOrEmpty(sani.Sanitize(Model.Title_EN)))
                    throw new Exception("Title versi English tidak valid|document.getElementById('nav_EN').click(); document.getElementById('Title_EN').focus()");

                if (!string.IsNullOrEmpty(sani.Sanitize(Model.SubTitle_ID)) && string.IsNullOrEmpty(sani.Sanitize(Model.SubTitle_EN)))
                    throw new Exception("Sub-Title versi English tidak valid|document.getElementById('nav_EN').click(); document.getElementById('SubTitle_EN').focus()");

                if (!string.IsNullOrEmpty(sani.Sanitize(Model.LabelTombol_ID)) && string.IsNullOrEmpty(sani.Sanitize(Model.LabelTombol_EN)))
                    throw new Exception("Label Tombol versi English tidak valid|document.getElementById('nav_EN').click(); document.getElementById('LabelTombol_EN').focus()");

                if (!string.IsNullOrEmpty(sani.Sanitize(Model.Link_ID)) && string.IsNullOrEmpty(sani.Sanitize(Model.Link_EN)))
                    throw new Exception("URL Tombol versi English tidak valid|document.getElementById('nav_EN').click(); document.getElementById('Link_EN').focus()");

                #region Save Banner_ID

                string Filename_ID = "", Ekstension_ID = "";
                #region Upload_ID
                if (Model.Upload_ID != null)
                {
                    List<IFormFile> files = new List<IFormFile>();
                    files.Add(Model.Upload_ID);
                    foreach (IFormFile file in files)
                    {
                        if (file.Length > 0)
                        {
                            Ekstension_ID = System.IO.Path.GetExtension(file.FileName).Trim();
                            Filename_ID = "Banner_ID_" + _ID;

                            var filePath = Path.GetTempFileName();
                            using (var stream = System.IO.File.Create(filePath))
                            {
                                file.CopyTo(stream);

                                string upload = Helper.UploadFTPWithEcryption(file, Filename_ID, Ekstension_ID);
                                if (upload != "success")
                                {
                                    Filename_ID = "";
                                    Ekstension_ID = "";
                                    throw new Exception(upload);
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Failed, Permission denied while uploading the file or attachment");
                        }
                    }
                }
                #endregion

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", _ID));
                param.Add(new SqlParameter("@Action", sani.Sanitize(Model.Action)));

                param.Add(new SqlParameter("@Lang", "ID"));
                param.Add(new SqlParameter("@Filename", Filename_ID));
                param.Add(new SqlParameter("@Ekstension", Ekstension_ID));
                param.Add(new SqlParameter("@Title", sani.Sanitize(Model.Title_ID)));
                param.Add(new SqlParameter("@Title_Color", sani.Sanitize(Model.Title_ID_Color)));
                param.Add(new SqlParameter("@SubTitle", sani.Sanitize(Model.SubTitle_ID)));
                param.Add(new SqlParameter("@SubTitle_Color", sani.Sanitize(Model.SubTitle_ID_Color)));
                param.Add(new SqlParameter("@LabelTombol", sani.Sanitize(Model.LabelTombol_ID)));
                param.Add(new SqlParameter("@Link", sani.Sanitize(Model.Link_ID)));

                param.Add(new SqlParameter("@Status", sani.Sanitize(Model.Status)));
                param.Add(new SqlParameter("@CreatedBy", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));

                mssql.ExecuteNonQuery("sp_Save_Banner", param);

                #endregion Save Banner_ID

                #region Save Banner_EN

                string Filename_EN = Filename_ID, Ekstension_EN = Ekstension_ID;
                #region Upload_EN
                if (Model.Upload_EN != null)
                {
                    List<IFormFile> files = new List<IFormFile>();
                    files.Add(Model.Upload_EN);
                    foreach (IFormFile file in files)
                    {
                        if (file.Length > 0)
                        {
                            Ekstension_EN = System.IO.Path.GetExtension(file.FileName).Trim();
                            Filename_EN = "Banner_EN_" + _ID;

                            var filePath = Path.GetTempFileName();
                            using (var stream = System.IO.File.Create(filePath))
                            {
                                file.CopyTo(stream);

                                string upload = Helper.UploadFTPWithEcryption(file, Filename_EN, Ekstension_EN);
                                if (upload != "success")
                                {
                                    Filename_EN = Filename_ID;
                                    Ekstension_EN = Ekstension_ID;
                                    throw new Exception(upload);
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Failed, Permission denied while uploading the file or attachment");
                        }
                    }
                }
                #endregion

                List<SqlParameter> param_EN = new List<SqlParameter>();
                param_EN.Add(new SqlParameter("@ID", _ID));
                param_EN.Add(new SqlParameter("@Action", sani.Sanitize(Model.Action)));

                param_EN.Add(new SqlParameter("@Lang", "EN"));
                param_EN.Add(new SqlParameter("@Filename", Filename_EN));
                param_EN.Add(new SqlParameter("@Ekstension", Ekstension_EN));
                param_EN.Add(new SqlParameter("@Title", sani.Sanitize(Model.Title_EN)));
                param_EN.Add(new SqlParameter("@Title_Color", sani.Sanitize(Model.Title_EN_Color)));
                param_EN.Add(new SqlParameter("@SubTitle", sani.Sanitize(Model.SubTitle_EN)));
                param_EN.Add(new SqlParameter("@SubTitle_Color", sani.Sanitize(Model.SubTitle_EN_Color)));
                param_EN.Add(new SqlParameter("@LabelTombol", sani.Sanitize(Model.LabelTombol_EN)));
                param_EN.Add(new SqlParameter("@Link", sani.Sanitize(Model.Link_EN)));

                param_EN.Add(new SqlParameter("@Status", sani.Sanitize(Model.Status)));
                param_EN.Add(new SqlParameter("@CreatedBy", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));

                mssql.ExecuteNonQuery("sp_Save_Banner", param_EN);

                #endregion Save Banner ID

                return Json(new { Error = false, Message = "" });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        public IActionResult TentangSPP()
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                return RedirectToAction("Index", "Dashboard");

            ViewData["CurrentControllerName"] = "CMS";
            ViewData["CurrentActionName"] = "Tentang SPP";
            ViewData["Title"] = "CMS Tentang SPP";

            ViewData["Email"] = StringCipher.Decrypt(HttpContext.Session.GetString("Email"));

            ViewBag.Email = StringCipher.Decrypt(HttpContext.Session.GetString("Email"));
            ViewBag.Role = HttpContext.Session.GetString("fr");

            return View();
        }

        public IActionResult PilarSPP()
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                return RedirectToAction("Index", "Dashboard");

            ViewData["CurrentControllerName"] = "CMS";
            ViewData["CurrentActionName"] = "Tiga Pilar SPP";
            ViewData["Title"] = "CMS Tiga Pilar SPP";

            ViewData["Email"] = StringCipher.Decrypt(HttpContext.Session.GetString("Email"));

            ViewBag.Email = StringCipher.Decrypt(HttpContext.Session.GetString("Email"));
            ViewBag.Role = HttpContext.Session.GetString("fr");

            return View();
        }

        public IActionResult JenisPelanggaran()
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                return RedirectToAction("Index", "Dashboard");

            ViewData["CurrentControllerName"] = "CMS";
            ViewData["CurrentActionName"] = "Jenis Pelanggaran";
            ViewData["Title"] = "CMS Jenis Pelanggaran";

            ViewData["Email"] = StringCipher.Decrypt(HttpContext.Session.GetString("Email"));

            ViewBag.Email = StringCipher.Decrypt(HttpContext.Session.GetString("Email"));
            ViewBag.Role = HttpContext.Session.GetString("fr");

            return View();
        }

        public IActionResult MediaPenyampaian()
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                return RedirectToAction("Index", "Dashboard");

            ViewData["CurrentControllerName"] = "CMS";
            ViewData["CurrentActionName"] = "Media Penyampaian";
            ViewData["Title"] = "CMS Media Penyampaian";

            ViewData["Email"] = StringCipher.Decrypt(HttpContext.Session.GetString("Email"));

            ViewBag.Email = StringCipher.Decrypt(HttpContext.Session.GetString("Email"));
            ViewBag.Role = HttpContext.Session.GetString("fr");

            return View();
        }

        public IActionResult FAQ()
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                return RedirectToAction("Index", "Dashboard");

            ViewData["CurrentControllerName"] = "CMS";
            ViewData["CurrentActionName"] = "FAQ";
            ViewData["Title"] = "CMS FAQ";

            ViewData["Email"] = StringCipher.Decrypt(HttpContext.Session.GetString("Email"));

            ViewBag.Email = StringCipher.Decrypt(HttpContext.Session.GetString("Email"));
            ViewBag.Role = HttpContext.Session.GetString("fr");

            return View();
        }

        public IActionResult ContactUs()
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                return RedirectToAction("Index", "Dashboard");

            ViewData["CurrentControllerName"] = "CMS";
            ViewData["CurrentActionName"] = "Contact Us";
            ViewData["Title"] = "CMS Contact Us";

            ViewData["Email"] = StringCipher.Decrypt(HttpContext.Session.GetString("Email"));

            ViewBag.Email = StringCipher.Decrypt(HttpContext.Session.GetString("Email"));
            ViewBag.Role = HttpContext.Session.GetString("fr");

            return View();
        }

        public IActionResult SyaratDanKetentuan()
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                return RedirectToAction("Index", "Dashboard");

            ViewData["CurrentControllerName"] = "CMS";
            ViewData["CurrentActionName"] = "Syarat dan Ketentuan";
            ViewData["Title"] = "CMS Syarat dan Ketentuan";

            ViewData["Email"] = StringCipher.Decrypt(HttpContext.Session.GetString("Email"));

            ViewBag.Email = StringCipher.Decrypt(HttpContext.Session.GetString("Email"));
            ViewBag.Role = HttpContext.Session.GetString("fr");

            return View();
        }

        [HttpPost]
        public IActionResult Get_Single_CMS(string Tipe, string Lang)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='/'");

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@Tipe", sani.Sanitize(Tipe)));
                param.Add(new SqlParameter("@Lang", sani.Sanitize(Lang)));
                DataTable dt = mssql.GetDataTable("sp_Get_Single_CMS", param);
                int RowCount = dt.Rows.Count;
                List<tblT_CMS> MainData = new List<tblT_CMS>();
                MainData = mssql.ConvertDataTable<tblT_CMS>(dt);
                for (int i = 0; i < MainData.Count; i++)
                {
                    MainData[i].ID = StringCipher.Encrypt(MainData[i].ID) + "|" + HttpContext.Session.GetString("SessionID");
                    MainData[i].ID_IN = StringCipher.Encrypt(MainData[i].ID_IN) + "|" + HttpContext.Session.GetString("SessionID");

                    string Filename = MainData[i].Filename;
                    if (!string.IsNullOrEmpty(Filename))
                    {
                        string Ekstension = MainData[i].Ekstension.ToLower();
                        if (Ekstension == ".jpg" || Ekstension == ".jpeg" || Ekstension == ".png")
                            MainData[i].Filename = Helper.GetBinaryImageEncrypted(Filename, MainData[i].Ekstension);
                    }

                    string Filename1 = MainData[i].Filename1;
                    if (!string.IsNullOrEmpty(Filename1))
                    {
                        string Ekstension1 = MainData[i].Ekstension1.ToLower();
                        if (Ekstension1 == ".jpg" || Ekstension1 == ".jpeg" || Ekstension1 == ".png")
                            MainData[i].Filename1 = Helper.GetBinaryImageEncrypted(Filename1, MainData[i].Ekstension1);
                    }

                }
                return Json(new { Error = false, Message = MainData, RowCount = RowCount.ToString() });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message, RowCount = "0" });
            }
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public IActionResult Save_CMS([FromForm] tblT_CMS Model)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='/'");

                string ID = sani.Sanitize(Model.ID);
                string _ID = Guid.NewGuid().ToString();

                if (!string.IsNullOrEmpty(sani.Sanitize(Model.Title_ID)) && string.IsNullOrEmpty(sani.Sanitize(Model.Title_EN)))
                    throw new Exception("Title versi English tidak valid|document.getElementById('nav_EN').click(); document.getElementById('Title_EN').focus()");

                if (!string.IsNullOrEmpty(sani.Sanitize(Model.SubTitle_ID)) && string.IsNullOrEmpty(sani.Sanitize(Model.SubTitle_EN)))
                    throw new Exception("Sub-Title versi English tidak valid|document.getElementById('nav_EN').click(); document.getElementById('SubTitle_EN').focus()");

                if (!string.IsNullOrEmpty(sani.Sanitize(Model.LabelTombol_ID)) && string.IsNullOrEmpty(sani.Sanitize(Model.LabelTombol_EN)))
                    throw new Exception("Label Tombol versi English tidak valid|document.getElementById('nav_EN').click(); document.getElementById('LabelTombol_EN').focus()");

                if (!string.IsNullOrEmpty(sani.Sanitize(Model.Description_ID)) && string.IsNullOrEmpty(sani.Sanitize(Model.Description_EN)))
                    throw new Exception("Deskripsi versi English tidak valid|document.getElementById('nav_EN').click(); document.getElementById('Description_EN').focus()");

                if (!string.IsNullOrEmpty(sani.Sanitize(Model.Link_ID)) && string.IsNullOrEmpty(sani.Sanitize(Model.Link_EN)))
                    throw new Exception("URL Tombol versi English tidak valid|document.getElementById('nav_EN').click(); document.getElementById('Link_EN').focus()");

                if (!string.IsNullOrEmpty(ID))
                {
                    _ID = StringCipher.Decrypt(ID.Split("|")[0]);
                    string SessionIDDesc = ID.Split("|")[1];

                    if (SessionIDDesc != HttpContext.Session.GetString("SessionID"))
                        throw new Exception("Invalid Authorization|window.location='/'");
                }

                string Tipe = sani.Sanitize(Model.Tipe);

                if (sani.Sanitize(Model.Action) == "hapus")
                {
                    DataTable dtJenisPelanggaran = mssql.GetDataTable("SELECT TOP 1 * FROM tblT_CMS A JOIN tblT_Dumas B ON A.GridTitle = B.Jenis_Pelanggaran WHERE A.ID = '" + _ID + "' AND A.Tipe = 'Jenis Pelanggaran'");
                    if (dtJenisPelanggaran.Rows.Count > 0)
                        throw new Exception("Maaf, Jenis Pelanggan [" + dtJenisPelanggaran.Rows[0]["GridTitle"].ToString() + "] tidak dapat dihapus karena sudah digunakan pada Pengaduan");
                }


                #region Save_ID

                #region Upload_ID
                string Filename_ID = "", Ekstension_ID = "";
                if (Model.Upload_ID != null)
                {
                    List<IFormFile> files = new List<IFormFile>();
                    files.Add(Model.Upload_ID);
                    foreach (IFormFile file in files)
                    {
                        if (file.Length > 0)
                        {
                            Ekstension_ID = System.IO.Path.GetExtension(file.FileName).Trim();
                            Filename_ID = Tipe.Trim().Replace(" ", "") + "_fn_ID_" + _ID;

                            var filePath = Path.GetTempFileName();
                            using (var stream = System.IO.File.Create(filePath))
                            {
                                file.CopyTo(stream);

                                string upload = Helper.UploadFTPWithEcryption(file, Filename_ID, Ekstension_ID);
                                if (upload != "success")
                                {
                                    Filename_ID = "";
                                    Ekstension_ID = "";
                                    throw new Exception(upload);
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Failed, Permission denied while uploading the file or attachment");
                        }
                    }
                }
                #endregion

                #region Upload1_ID
                string Filename1_ID = "", Ekstension1_ID = "";
                if (Model.Upload1_ID != null)
                {
                    List<IFormFile> files = new List<IFormFile>();
                    files.Add(Model.Upload1_ID);
                    foreach (IFormFile file in files)
                    {
                        if (file.Length > 0)
                        {
                            Ekstension1_ID = System.IO.Path.GetExtension(file.FileName).Trim();
                            Filename1_ID = Tipe.Trim().Replace(" ", "") + "_fn1_ID_" + _ID;

                            var filePath = Path.GetTempFileName();
                            using (var stream = System.IO.File.Create(filePath))
                            {
                                file.CopyTo(stream);

                                string upload = Helper.UploadFTPWithEcryption(file, Filename1_ID, Ekstension1_ID);
                                if (upload != "success")
                                {
                                    Filename1_ID = "";
                                    Ekstension1_ID = "";
                                    throw new Exception(upload);
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Failed, Permission denied while uploading the file or attachment");
                        }
                    }
                }
                #endregion

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", _ID));
                param.Add(new SqlParameter("@Action", sani.Sanitize(Model.Action)));

                param.Add(new SqlParameter("@Tipe", sani.Sanitize(Model.Tipe)));
                param.Add(new SqlParameter("@Lang", "ID"));
                param.Add(new SqlParameter("@Title", sani.Sanitize(Model.Title_ID)));
                param.Add(new SqlParameter("@SubTitle", sani.Sanitize(Model.SubTitle_ID)));
                param.Add(new SqlParameter("@GridTitle", sani.Sanitize(Model.GridTitle_ID)));
                param.Add(new SqlParameter("@GridContent", sani.Sanitize(Model.GridContent_ID)));
                param.Add(new SqlParameter("@LabelTombol", sani.Sanitize(Model.LabelTombol_ID)));
                param.Add(new SqlParameter("@Link", sani.Sanitize(Model.Link_ID)));
                param.Add(new SqlParameter("@Description", sani.Sanitize(Model.Description_ID)));
                param.Add(new SqlParameter("@Filename", Filename_ID));
                param.Add(new SqlParameter("@Ekstension", Ekstension_ID));
                param.Add(new SqlParameter("@Filename1", Filename1_ID));
                param.Add(new SqlParameter("@Ekstension1", Ekstension1_ID));
                param.Add(new SqlParameter("@Value1", sani.Sanitize(Model.Value1_ID)));
                param.Add(new SqlParameter("@Value2", sani.Sanitize(Model.Value2_ID)));
                param.Add(new SqlParameter("@Value3", sani.Sanitize(Model.Value3_ID)));
                param.Add(new SqlParameter("@Value4", sani.Sanitize(Model.Value4_ID)));
                param.Add(new SqlParameter("@Value5", sani.Sanitize(Model.Value5_ID)));
                param.Add(new SqlParameter("@Value6", sani.Sanitize(Model.Value6_ID)));
                param.Add(new SqlParameter("@Value7", sani.Sanitize(Model.Value7_ID)));
                param.Add(new SqlParameter("@Value8", sani.Sanitize(Model.Value8_ID)));
                param.Add(new SqlParameter("@Value9", sani.Sanitize(Model.Value9_ID)));
                param.Add(new SqlParameter("@Status", sani.Sanitize(Model.Status)));
                param.Add(new SqlParameter("@CreatedBy", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));

                mssql.ExecuteNonQuery("sp_Save_CMS", param);

                #endregion Save_ID

                #region Save_EN

                #region Upload_EN
                string Filename_EN = Filename_ID, Ekstension_EN = Ekstension_ID;
                if (Model.Upload_EN != null)
                {
                    List<IFormFile> files = new List<IFormFile>();
                    files.Add(Model.Upload_EN);
                    foreach (IFormFile file in files)
                    {
                        if (file.Length > 0)
                        {
                            Ekstension_EN = System.IO.Path.GetExtension(file.FileName).Trim();
                            Filename_EN = Tipe.Trim().Replace(" ", "") + "_fn_EN_" + _ID;

                            var filePath = Path.GetTempFileName();
                            using (var stream = System.IO.File.Create(filePath))
                            {
                                file.CopyTo(stream);

                                string upload = Helper.UploadFTPWithEcryption(file, Filename_EN, Ekstension_EN);
                                if (upload != "success")
                                {
                                    Filename_EN = Filename_ID;
                                    Ekstension_EN = Ekstension_ID;
                                    throw new Exception(upload);
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Failed, Permission denied while uploading the file or attachment");
                        }
                    }
                }
                #endregion

                #region Upload1_EN
                string Filename1_EN = Filename1_ID, Ekstension1_EN = Ekstension1_ID;
                if (Model.Upload1_EN != null)
                {
                    List<IFormFile> files = new List<IFormFile>();
                    files.Add(Model.Upload1_EN);
                    foreach (IFormFile file in files)
                    {
                        if (file.Length > 0)
                        {
                            Ekstension1_EN = System.IO.Path.GetExtension(file.FileName).Trim();
                            Filename1_EN = Tipe.Trim().Replace(" ", "") + "_fn1_EN_" + _ID;

                            var filePath = Path.GetTempFileName();
                            using (var stream = System.IO.File.Create(filePath))
                            {
                                file.CopyTo(stream);

                                string upload = Helper.UploadFTPWithEcryption(file, Filename1_EN, Ekstension1_EN);
                                if (upload != "success")
                                {
                                    Filename1_EN = Filename1_ID;
                                    Ekstension1_EN = Ekstension1_ID;
                                    throw new Exception(upload);
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Failed, Permission denied while uploading the file or attachment");
                        }
                    }
                }
                #endregion

                List<SqlParameter> param_EN = new List<SqlParameter>();
                param_EN.Add(new SqlParameter("@ID", _ID));
                param_EN.Add(new SqlParameter("@Action", sani.Sanitize(Model.Action)));

                param_EN.Add(new SqlParameter("@Tipe", sani.Sanitize(Model.Tipe)));
                param_EN.Add(new SqlParameter("@Lang", "EN"));
                param_EN.Add(new SqlParameter("@Title", sani.Sanitize(Model.Title_EN)));
                param_EN.Add(new SqlParameter("@SubTitle", sani.Sanitize(Model.SubTitle_EN)));
                param_EN.Add(new SqlParameter("@GridTitle", sani.Sanitize(Model.GridTitle_EN)));
                param_EN.Add(new SqlParameter("@GridContent", sani.Sanitize(Model.GridContent_EN)));
                param_EN.Add(new SqlParameter("@LabelTombol", sani.Sanitize(Model.LabelTombol_EN)));
                param_EN.Add(new SqlParameter("@Link", sani.Sanitize(Model.Link_EN)));
                param_EN.Add(new SqlParameter("@Description", sani.Sanitize(Model.Description_EN)));
                param_EN.Add(new SqlParameter("@Filename", Filename_EN));
                param_EN.Add(new SqlParameter("@Ekstension", Ekstension_EN));
                param_EN.Add(new SqlParameter("@Filename1", Filename1_EN));
                param_EN.Add(new SqlParameter("@Ekstension1", Ekstension1_EN));
                param_EN.Add(new SqlParameter("@Value1", sani.Sanitize(Model.Value1_EN)));
                param_EN.Add(new SqlParameter("@Value2", sani.Sanitize(Model.Value2_EN)));
                param_EN.Add(new SqlParameter("@Value3", sani.Sanitize(Model.Value3_EN)));
                param_EN.Add(new SqlParameter("@Value4", sani.Sanitize(Model.Value4_EN)));
                param_EN.Add(new SqlParameter("@Value5", sani.Sanitize(Model.Value5_EN)));
                param_EN.Add(new SqlParameter("@Value6", sani.Sanitize(Model.Value6_EN)));
                param_EN.Add(new SqlParameter("@Value7", sani.Sanitize(Model.Value7_EN)));
                param_EN.Add(new SqlParameter("@Value8", sani.Sanitize(Model.Value8_EN)));
                param_EN.Add(new SqlParameter("@Value9", sani.Sanitize(Model.Value9_EN)));
                param_EN.Add(new SqlParameter("@Status", sani.Sanitize(Model.Status)));
                param_EN.Add(new SqlParameter("@CreatedBy", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));

                mssql.ExecuteNonQuery("sp_Save_CMS", param_EN);

                #endregion Save_EN

                return Json(new { Error = false, Message = "" });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Get_Multiple_CMS(string Tipe, string Lang)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='/'");

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@Tipe", sani.Sanitize(Tipe)));
                param.Add(new SqlParameter("@Lang", sani.Sanitize(Lang)));
                DataTable dt = mssql.GetDataTable("sp_Get_Single_CMS", param);
                int RowCount = dt.Rows.Count;
                List<tblT_CMS> MainData = new List<tblT_CMS>();
                MainData = mssql.ConvertDataTable<tblT_CMS>(dt);
                for (int i = 0; i < MainData.Count; i++)
                {
                    MainData[i].ID = StringCipher.Encrypt(MainData[i].ID) + "|" + HttpContext.Session.GetString("SessionID");
                    MainData[i].ID_IN = StringCipher.Encrypt(MainData[i].ID_IN) + "|" + HttpContext.Session.GetString("SessionID");

                    string Filename = MainData[i].Filename;
                    if (!string.IsNullOrEmpty(Filename))
                    {
                        string Ekstension = MainData[i].Ekstension.ToLower();
                        if (Ekstension == ".jpg" || Ekstension == ".jpeg" || Ekstension == ".png")
                            MainData[i].Filename = Helper.GetBinaryImageEncrypted(Filename, MainData[i].Ekstension);
                    }

                    string Filename1 = MainData[i].Filename1;
                    if (!string.IsNullOrEmpty(Filename1))
                    {
                        string Ekstension1 = MainData[i].Ekstension1.ToLower();
                        if (Ekstension1 == ".jpg" || Ekstension1 == ".jpeg" || Ekstension1 == ".png")
                            MainData[i].Filename1 = Helper.GetBinaryImageEncrypted(Filename1, MainData[i].Ekstension1);
                    }

                }
                return Json(new { Error = false, Message = MainData, RowCount = RowCount.ToString() });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message, RowCount = "0" });
            }
        }

        [HttpPost]
        public IActionResult Get_CMS_By_ID(string ID, string Lang)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='/'");

                string _ID = StringCipher.Decrypt(sani.Sanitize(ID).Split("|")[0]);
                string SessionIDDesc = sani.Sanitize(ID).Split("|")[1];

                if (SessionIDDesc != HttpContext.Session.GetString("SessionID"))
                    throw new Exception("Invalid Authorization|window.location='/'");

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", _ID));
                param.Add(new SqlParameter("@Lang", sani.Sanitize(Lang)));
                DataTable dt = mssql.GetDataTable("sp_Get_CMS_By_ID", param);
                List<tblT_CMS> MainData = new List<tblT_CMS>();
                MainData = mssql.ConvertDataTable<tblT_CMS>(dt);
                for (int i = 0; i < MainData.Count; i++)
                {
                    MainData[i].ID = StringCipher.Encrypt(MainData[i].ID) + "|" + HttpContext.Session.GetString("SessionID");
                    MainData[i].ID_IN = StringCipher.Encrypt(MainData[i].ID_IN) + "|" + HttpContext.Session.GetString("SessionID");

                    string Filename = MainData[i].Filename;
                    if (!string.IsNullOrEmpty(Filename))
                    {
                        string Ekstension = MainData[i].Ekstension.ToLower();
                        if (Ekstension == ".jpg" || Ekstension == ".jpeg" || Ekstension == ".png")
                            MainData[i].Filename = Helper.GetBinaryImageEncrypted(Filename, MainData[i].Ekstension);
                    }

                    string Filename1 = MainData[i].Filename1;
                    if (!string.IsNullOrEmpty(Filename1))
                    {
                        string Ekstension1 = MainData[i].Ekstension1.ToLower();
                        if (Ekstension1 == ".jpg" || Ekstension1 == ".jpeg" || Ekstension1 == ".png")
                            MainData[i].Filename1 = Helper.GetBinaryImageEncrypted(Filename1, MainData[i].Ekstension1);
                    }

                }
                return Json(new { Error = false, Message = MainData });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message, RowCount = "0" });
            }
        }

    }
}
