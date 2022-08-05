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
using System.Data.SqlClient;
using System.Data;
using System.IO;
using Ganss.XSS;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;

namespace Frontend_SPP.Controllers
{
    public class PengaduanController : Controller
    {

        private IWebHostEnvironment Environment;

        public PengaduanController(IWebHostEnvironment _environment)
        {
            Environment = _environment;
        }

        private HtmlSanitizer sani = new HtmlSanitizer();

        public IActionResult DaftarPengaduan()
        {
            if (string.IsNullOrEmpty(StringCipher.Decrypt(HttpContext.Session.GetString("Email"))))
                return RedirectToAction("Index", "Dashboard");


            ViewData["CurrentControllerName"] = "";
            ViewData["CurrentActionName"] = "Daftar Pengaduan";
            ViewData["Email"] = StringCipher.Decrypt(HttpContext.Session.GetString("Email"));

            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var culture = rqf.RequestCulture.Culture.ToString().ToUpper();
            if ((string.IsNullOrEmpty(culture) || culture == "IN") && string.IsNullOrEmpty(HttpContext.Session.GetString("culture")))
                culture = "ID";
            else
                HttpContext.Session.SetString("culture", culture.ToLower());

            return View();
        }

        public IActionResult PengaduanForm(string ID)
        {
            if (string.IsNullOrEmpty(StringCipher.Decrypt(HttpContext.Session.GetString("Email"))))
                return RedirectToAction("Index", "Dashboard");

            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var culture = rqf.RequestCulture.Culture.ToString().ToUpper();
            if ((string.IsNullOrEmpty(culture) || culture == "IN") && string.IsNullOrEmpty(HttpContext.Session.GetString("culture")))
                culture = "ID";
            else
                HttpContext.Session.SetString("culture", culture.ToLower());

            HttpContext.Session.SetString("culture", culture.ToLower());

            ViewData["CurrentControllerName"] = "";
            ViewData["CurrentActionName"] = "Form Pengaduan";
            ViewData["Email"] = StringCipher.Decrypt(HttpContext.Session.GetString("Email"));

            DataTable dtPelapor = mssql.GetDataTable("SELECT * FROM tblM_User WHERE Email = '" + StringCipher.Decrypt(HttpContext.Session.GetString("Email")) + "'");
            if (dtPelapor.Rows.Count == 1)
            {
                DataRow drPelapor = dtPelapor.Rows[0];
                ViewBag.Fullname = drPelapor["Fullname"].ToString();
                ViewBag.Email = drPelapor["Email"].ToString();
                ViewBag.Mobile = "";

                if (!string.IsNullOrEmpty(drPelapor["Mobile"].ToString()) && int.Parse(drPelapor["Mobile_Verification"].ToString()) == 1)
                    ViewBag.Mobile = drPelapor["Mobile"].ToString();

                if (!string.IsNullOrEmpty(ID))
                {
                    DataRow drowner = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM tblT_Dumas WHERE ID = '" + sani.Sanitize(ID) + "' AND Email = '" + StringCipher.Decrypt(HttpContext.Session.GetString("Email")) + "'");
                    if (int.Parse(drowner["Count"].ToString()) == 0)
                    {
                        HttpContext.Session.SetString("msg", "Invalid Authorization");
                        return RedirectToAction("Index", "Dashboard");
                    }

                }

            }

            string FileEkstensionFilter = "";
            DataTable dt_FileEkstensionFilter = mssql.GetDataTable("SELECT Name FROM FileEkstensionFilterDummy ORDER BY Name");
            foreach (DataRow dr in dt_FileEkstensionFilter.Rows)
            {
                string Eks = dr["Name"].ToString().Replace(".", "").Trim();
                FileEkstensionFilter += Eks + "/";
            }
            if (FileEkstensionFilter.Length > 0)
                FileEkstensionFilter = FileEkstensionFilter.Substring(0, FileEkstensionFilter.Length - 1);
            ViewBag.FileEkstensionFilter = FileEkstensionFilter;


            return View();
        }

        [HttpPost]
        public IActionResult GetPengaduanByEmail(string Status)
        {
            try
            {
                if (string.IsNullOrEmpty(StringCipher.Decrypt(HttpContext.Session.GetString("Email"))))
                    return RedirectToAction("Index", "Dashboard");

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@Email", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));
                param.Add(new SqlParameter("@Status", Status));
                DataTable dt = mssql.GetDataTable("sp_Get_Dumas_by_Email_Pelapor", param);
                List<Pengaduan> MainData = new List<Pengaduan>();
                MainData = mssql.ConvertDataTable<Pengaduan>(dt);
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
        public IActionResult GetPengaduanByID(string ID)
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
                DataTable dt = mssql.GetDataTable("sp_Get_Dumas_by_ID", param);

                List<Pengaduan> MainData = new List<Pengaduan>();
                MainData = mssql.ConvertDataTable<Pengaduan>(dt);

                for (int i = 0; i < MainData.Count; i++)
                {
                    MainData[i].ID = StringCipher.Encrypt(MainData[i].ID) + "|" + HttpContext.Session.GetString("SessionID");

                    string FilePenyaluran = MainData[i].Keterangan_Penyaluran_Filename;
                    if (!string.IsNullOrEmpty(FilePenyaluran))
                        MainData[i].FilepathFilePenyaluran = Helper.GetBinaryImage(FilePenyaluran, MainData[i].Keterangan_Penyaluran_Ekstension);

                    string FilePemeriksaan = MainData[i].Keterangan_Pemeriksaan_Filename;
                    if (!string.IsNullOrEmpty(FilePemeriksaan))
                        MainData[i].FilepathFilePemeriksaan = Helper.GetBinaryImage(FilePemeriksaan, MainData[i].Keterangan_Pemeriksaan_Ekstension);

                    string FileKonfirmasi = MainData[i].Keterangan_Konfirmasi_Filename;
                    if (!string.IsNullOrEmpty(FileKonfirmasi))
                        MainData[i].FilepathFileKonfirmasi = Helper.GetBinaryImage(FileKonfirmasi, MainData[i].Keterangan_Konfirmasi_Ekstension);

                    string FileRespon = MainData[i].Keterangan_Respon_Filename;
                    if (!string.IsNullOrEmpty(FileRespon))
                        MainData[i].FilepathFileRespon = Helper.GetBinaryImage(FileRespon, MainData[i].Keterangan_Respon_Ekstension);
                }

                return Json(new { Error = false, Message = MainData });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult SavePengaduan(Pengaduan Model)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='/'");

                string ID = sani.Sanitize(Model.ID);
                string _ID = "";

                if (!string.IsNullOrEmpty(ID))
                {
                    _ID = StringCipher.Decrypt(ID.Split("|")[0]);
                    string SessionIDDesc = ID.Split("|")[1];

                    if (SessionIDDesc != HttpContext.Session.GetString("SessionID"))
                        throw new Exception("Invalid Authorization|window.location='/'");
                }

                if (string.IsNullOrEmpty(_ID))
                    if (HttpContext.Session.GetString("culture") == "EN")
                        throw new Exception("Declined, You need to entry the reported party identiry, please check again");
                    else
                        throw new Exception("Ditolak, Anda belum menginput identitas pihak yang terlapor, mohon periksa kembali");

                DataRow drDetailPengaduan = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM tblT_Dumas_Detail WHERE ID_Header = '" + _ID + "'");
                if (int.Parse(drDetailPengaduan["Count"].ToString()) == 0)
                    if (HttpContext.Session.GetString("culture") == "EN")
                        throw new Exception("Declined, You need to entry the reported party identiry, please check again");
                    else
                        throw new Exception("Ditolak, Anda belum menginput identitas pihak yang terlapor, mohon periksa kembali");

                #region UploadFileEvidence
                if (Model.UploadFileEvidence != null)
                {

                    //List<IFormFile> files = new List<IFormFile>();
                    //files.Add(Model.UploadFileEvidence);
                    foreach (IFormFile file in Model.UploadFileEvidence)
                    {
                        string OriginalFilename = file.FileName.ToLower();
                        if (OriginalFilename.ToLower().Contains("php") || OriginalFilename.ToLower().Contains("aspx") || OriginalFilename.ToLower().Contains("exe") || OriginalFilename.ToLower().Contains("dll") || OriginalFilename.ToLower().Contains("js") || OriginalFilename.ToLower().Contains("css") || OriginalFilename.ToLower().Contains("html"))
                            throw new Exception("Failed, Permission denied while uploading the file or attachment");

                        if (file.Length > 0)
                        {
                            string FileEvidence = "", FileEvidence_Ekstension = "";
                            string newFileID = Guid.NewGuid().ToString();
                            FileEvidence_Ekstension = System.IO.Path.GetExtension(file.FileName).Trim().ToString();
                            FileEvidence = newFileID.ToString() + FileEvidence_Ekstension.ToString();
                            var filePath = Path.GetTempFileName();
                            using (var stream = System.IO.File.Create(filePath))
                            {
                                file.CopyToAsync(stream);

                                string upload = Helper.UploadFTP(stream, FileEvidence, FileEvidence_Ekstension);
                                if (upload != "success")
                                {
                                    FileEvidence = "";
                                    FileEvidence_Ekstension = "";
                                    throw new Exception(upload);
                                }

                                mssql.ExecuteNonQuery("INSERT INTO tblT_File_Evidence (ID_Header, FileEvidence, FileEvidence_Ekstension, CreatedBy) VALUES ('" + _ID + "', '" + FileEvidence + "', '" + FileEvidence_Ekstension + "', '" + StringCipher.Decrypt(HttpContext.Session.GetString("Email")) + "')");
                                FileEvidence = "";
                                FileEvidence_Ekstension = "";
                            }
                        }
                        else
                        {
                            throw new Exception("Failed, Permission denied while uploading the file or attachment");
                        }

                    }
                }
                #endregion

                #region Save Pengaduan

                DataRow drNomor = mssql.GetDataRow("SELECT dbo.Generate_Nomor_Dumas(GETDATE()) [Nomor]");
                string Nomor = drNomor["Nomor"].ToString();

                string Status = "Terkirim";
                string TanggalKirim = DateTime.Now.ToString("dd MMM yyyy HH:mm");
                DateTime WaktuKejadian = Convert.ToDateTime(Model.WaktuKejadian);

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", _ID));
                param.Add(new SqlParameter("@Sumber", "Portal SPP"));
                param.Add(new SqlParameter("@Nomor", Nomor));
                param.Add(new SqlParameter("@Email", sani.Sanitize(Model.Email)));
                param.Add(new SqlParameter("@Handphone", sani.Sanitize(Model.Handphone)));
                param.Add(new SqlParameter("@Jenis_Pelanggaran", sani.Sanitize(Model.Jenis_Pelanggaran)));
                param.Add(new SqlParameter("@TempatKejadian", sani.Sanitize(Model.TempatKejadian)));
                param.Add(new SqlParameter("@WaktuKejadian", WaktuKejadian));
                param.Add(new SqlParameter("@Kronologi", sani.Sanitize(Model.Kronologi)));
                param.Add(new SqlParameter("@Status", Status));
                param.Add(new SqlParameter("@CreatedBy", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));

                mssql.ExecuteNonQuery("sp_Kirim_Dumas", param);

                #endregion Save Pengaduan

                #region Notifikasi_ke_Admin_Pusat
                string Subject = "SPP - PTSMI [Pengaduan Baru]";
                DataTable dt_Email_Admin_Pusat = mssql.GetDataTable("sp_Get_Email_Admin_Pusat");
                foreach (DataRow dr in dt_Email_Admin_Pusat.Rows)
                {
                    string EmailAdminPusat = dr["Email"].ToString();
                    string Fullname = dr["Fullname"].ToString();
                    Helper.SendMail(EmailAdminPusat, Subject, MailComposer.compose_mail_body_kirim_dumas(_ID, Fullname, Nomor, sani.Sanitize(Model.Email), TanggalKirim));
                }
                #endregion Notifikasi_ke_Admin_Pusat

                #region Notifikasi_ke_Pelapor
                DataRow drUser = mssql.GetDataRow("SELECT Email FROM tblM_User WHERE Email = '" + StringCipher.Decrypt(HttpContext.Session.GetString("Email")) + "'");
                string EmailUser = drUser["Email"].ToString();
                Helper.SendMail(EmailUser, Subject, MailComposer.compose_mail_body_kirim_dumas_pengadu(EmailUser, Nomor, TanggalKirim));
                #endregion Notifikasi_ke_Pelapor

                return Json(new { Error = false, Message = Nomor });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult SaveDetailPengaduan(PengaduanDetail Model)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='/'");

                if (string.IsNullOrEmpty(sani.Sanitize(Model.Nama)) && sani.Sanitize(Model.Action) != "hapus")
                    if (HttpContext.Session.GetString("culture") == "EN")
                        throw new Exception("Declined, The name of the reported party is empty, please check again");
                    else
                        throw new Exception("Ditolak, mohon mengisi Nama pihak yang dilaporkan");

                Model.ID = sani.Sanitize(Model.ID);
                Model.ID_Header = sani.Sanitize(Model.ID_Header);

                string Action = sani.Sanitize(Model.Action);
                string ID = Guid.NewGuid().ToString();
                string ID_Header = "";

                if (!string.IsNullOrEmpty(Model.ID_Header))
                {
                    ID_Header = StringCipher.Decrypt(Model.ID_Header.Split("|")[0]);
                    string SessionIDDesc2 = Model.ID_Header.Split("|")[1];

                    if (SessionIDDesc2 != HttpContext.Session.GetString("SessionID"))
                        throw new Exception("Invalid Authorization|window.location='/'");
                }

                if (Action == "add")
                {
                    Model.ID = ID;
                }
                else
                {
                    ID = StringCipher.Decrypt(Model.ID.Split("|")[0]);
                    string SessionIDDesc = Model.ID.Split("|")[1];

                    if (SessionIDDesc != HttpContext.Session.GetString("SessionID"))
                        throw new Exception("Invalid Authorization|window.location='/'");
                }

                string FileIdentitas = "", FileIdentitas_Ekstension = "";
                #region UploadFileIdentitas
                if (Model.UploadFileIdentitas != null)
                {
                    string OriginalFilename = Model.UploadFileIdentitas.FileName.ToLower();
                    if (OriginalFilename.ToLower().Contains("php") || OriginalFilename.ToLower().Contains("aspx") || OriginalFilename.ToLower().Contains("exe") || OriginalFilename.ToLower().Contains("dll") || OriginalFilename.ToLower().Contains("js") || OriginalFilename.ToLower().Contains("css") || OriginalFilename.ToLower().Contains("html"))
                        throw new Exception("Failed, Permission denied while uploading the file or attachment");

                    List<IFormFile> files = new List<IFormFile>();
                    files.Add(Model.UploadFileIdentitas);
                    foreach (IFormFile file in files)
                    {
                        if (file.Length > 0)
                        {
                            FileIdentitas_Ekstension = System.IO.Path.GetExtension(file.FileName).Trim();
                            FileIdentitas = ID + FileIdentitas_Ekstension;

                            var filePath = Path.GetTempFileName();
                            using (var stream = System.IO.File.Create(filePath))
                            {
                                file.CopyToAsync(stream);

                                string upload = Helper.UploadFTP(stream, FileIdentitas, FileIdentitas_Ekstension);
                                if (upload != "success")
                                {
                                    FileIdentitas = "";
                                    FileIdentitas_Ekstension = "";
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

                #region Save Pengaduan Detail

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@Action", sani.Sanitize(Model.Action)));
                param.Add(new SqlParameter("@ID", ID));
                param.Add(new SqlParameter("@ID_Header", ID_Header));
                param.Add(new SqlParameter("@Email", sani.Sanitize(Model.Email)));
                param.Add(new SqlParameter("@Handphone", sani.Sanitize(Model.Handphone)));
                param.Add(new SqlParameter("@Nama", sani.Sanitize(Model.Nama)));
                param.Add(new SqlParameter("@NomorHandphone", sani.Sanitize(Model.NomorHandphone)));
                param.Add(new SqlParameter("@Departemen", sani.Sanitize(Model.Departemen)));
                param.Add(new SqlParameter("@Jabatan", sani.Sanitize(Model.Jabatan)));
                param.Add(new SqlParameter("@FileIdentitas", FileIdentitas));
                param.Add(new SqlParameter("@FileIdentitas_Ekstension", FileIdentitas_Ekstension));
                param.Add(new SqlParameter("@CreatedBy", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));

                DataTable dtResult = mssql.GetDataTable("sp_Save_Dumas_Detail", param);

                #endregion Save Pengaduan Detail

                return Json(new { Error = false, Message = StringCipher.Encrypt(dtResult.Rows[0]["ID_Header"].ToString()) + '|' + HttpContext.Session.GetString("SessionID") });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult GetDetailPengaduanByIDHeader(string ID_Header)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='/'");

                string ID = sani.Sanitize(ID_Header);

                string _ID = StringCipher.Decrypt(ID.Split("|")[0]);
                string SessionIDDesc = ID.Split("|")[1];

                if (SessionIDDesc != HttpContext.Session.GetString("SessionID"))
                    throw new Exception("Invalid Authorization|window.location='/'");

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID_Header", _ID));
                DataTable dt = mssql.GetDataTable("sp_Get_Detail_PengaduanByIDHeader", param);
                List<PengaduanDetail> MainData = new List<PengaduanDetail>();
                MainData = mssql.ConvertDataTable<PengaduanDetail>(dt);
                for (int i = 0; i < MainData.Count; i++)
                {
                    MainData[i].ID = StringCipher.Encrypt(MainData[i].ID) + "|" + HttpContext.Session.GetString("SessionID");
                    MainData[i].ID_Header = StringCipher.Encrypt(MainData[i].ID_Header) + "|" + HttpContext.Session.GetString("SessionID");
                    var FileIdentitas = MainData[i].FileIdentitas;
                    if (!string.IsNullOrEmpty(FileIdentitas))
                    {
                        MainData[i].FilepathFileIdentitas = Helper.GetBinaryImage(MainData[i].FileIdentitas, MainData[i].FileIdentitas_Ekstension);
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
        public IActionResult GetDetailPengaduanByID(string ID)
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
                DataTable dt = mssql.GetDataTable("sp_Get_DetailPengaduanByID", param);
                List<PengaduanDetail> MainData = new List<PengaduanDetail>();
                MainData = mssql.ConvertDataTable<PengaduanDetail>(dt);
                for (int i = 0; i < MainData.Count; i++)
                {
                    MainData[i].ID = StringCipher.Encrypt(MainData[i].ID) + "|" + HttpContext.Session.GetString("SessionID");
                    MainData[i].ID_Header = StringCipher.Encrypt(MainData[i].ID_Header) + "|" + HttpContext.Session.GetString("SessionID");
                    var FileIdentitas = MainData[i].FileIdentitas;
                    if (!string.IsNullOrEmpty(FileIdentitas))
                    {
                        MainData[i].FilepathFileIdentitas = Helper.GetBinaryImage(MainData[i].FileIdentitas, MainData[i].FileIdentitas_Ekstension);
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
        public IActionResult GetFileEvidenceByIDHeader(string ID_Header)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='/'");

                string ID = sani.Sanitize(ID_Header);

                string _ID = StringCipher.Decrypt(ID.Split("|")[0]);
                string SessionIDDesc = ID.Split("|")[1];

                if (SessionIDDesc != HttpContext.Session.GetString("SessionID"))
                    throw new Exception("Invalid Authorization|window.location='/'");

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID_Header", _ID));
                DataTable dt = mssql.GetDataTable("sp_Get_FileEvidenceByIDHeader", param);
                List<M_FileEvidence> MainData = new List<M_FileEvidence>();
                MainData = mssql.ConvertDataTable<M_FileEvidence>(dt);
                for (int i = 0; i < MainData.Count; i++)
                {
                    MainData[i].ID = StringCipher.Encrypt(MainData[i].ID) + "|" + HttpContext.Session.GetString("SessionID");
                    MainData[i].ID_Header = StringCipher.Encrypt(MainData[i].ID_Header) + "|" + HttpContext.Session.GetString("SessionID");
                    var FileEvidence = MainData[i].FileEvidence;
                    if (!string.IsNullOrEmpty(FileEvidence))
                    {
                        MainData[i].FilepathFileEvidence = Helper.GetBinaryImage(MainData[i].FileEvidence, MainData[i].FileEvidence_Ekstension);
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
        public IActionResult ReadTanggapanAdminSPP(string ID_Pengaduan)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='/'");

                string _ID = StringCipher.Decrypt(sani.Sanitize(ID_Pengaduan).Split("|")[0]);
                mssql.ExecuteNonQuery("UPDATE tblT_Tanggapan SET IsRead = 1, ReadOn = GETDATE() WHERE IDPengaduan = '" + _ID + "' AND TipePengirim = 'Admin SPP' AND IsRead = 0");
                return Json(new { Error = false, Message = 1 });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult GetTanggapanByIDPengaduan(string ID_Pengaduan)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='/'");

                string ID = sani.Sanitize(ID_Pengaduan);

                string _ID = StringCipher.Decrypt(ID.Split("|")[0]);
                string SessionIDDesc = ID.Split("|")[1];

                if (SessionIDDesc != HttpContext.Session.GetString("SessionID"))
                    throw new Exception("Invalid Authorization|window.location='/'");


                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@IDPengaduan", _ID));
                DataTable dt = mssql.GetDataTable("sp_Get_TanggapanByIDPengaduan", param);
                List<MTanggapan> MainData = new List<MTanggapan>();
                MainData = mssql.ConvertDataTable<MTanggapan>(dt);

                for (int i = 0; i < MainData.Count; i++)
                {
                    MainData[i].ID = StringCipher.Encrypt(MainData[i].ID) + "|" + HttpContext.Session.GetString("SessionID");

                    var FileLampiran = MainData[i].FileLampiran;
                    if (!string.IsNullOrEmpty(FileLampiran))
                    {
                        string FileLampiran_Ekstension = MainData[i].FileLampiran_Ekstension.ToLower();
                        if (FileLampiran_Ekstension == ".jpg" || FileLampiran_Ekstension == ".jpeg" || FileLampiran_Ekstension == ".png")
                            MainData[i].FilepathFileLampiran = Helper.GetBinaryImage(MainData[i].FileLampiran, MainData[i].FileLampiran_Ekstension);
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
        [RequestFormLimits(MultipartBodyLengthLimit = 2147483647)]

        public ActionResult KirimTanggapanPelapor(MTanggapan Model)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='/'");

                Model.ID = Guid.NewGuid().ToString();
                string ID = sani.Sanitize(Model.IDPengaduan);
                string _ID = StringCipher.Decrypt(ID.Split("|")[0]);
                string SessionIDDesc = ID.Split("|")[1];

                if (SessionIDDesc != HttpContext.Session.GetString("SessionID"))
                    throw new Exception("Invalid Authorization|window.location='/'");

                string FileLampiran = "", FileLampiran_Ekstension = "";
                #region UploadFileLampiran
                if (Model.UploadFileLampiran != null)
                {
                    string OriginalFilename = Model.UploadFileLampiran.FileName.ToLower();
                    if (OriginalFilename.ToLower().Contains("php") || OriginalFilename.ToLower().Contains("aspx") || OriginalFilename.ToLower().Contains("exe") || OriginalFilename.ToLower().Contains("dll") || OriginalFilename.ToLower().Contains("js") || OriginalFilename.ToLower().Contains("css") || OriginalFilename.ToLower().Contains("html"))
                        throw new Exception("Failed, Permission denied while uploading the file or attachment");

                    List<IFormFile> files = new List<IFormFile>();
                    files.Add(Model.UploadFileLampiran);
                    //_ = UploadFileAsync(files, Model.ID.ToString());
                    foreach (IFormFile file in files)
                    {
                        if (file.Length > 0)
                        {
                            FileLampiran_Ekstension = System.IO.Path.GetExtension(file.FileName).Trim();
                            FileLampiran = Model.ID + FileLampiran_Ekstension;

                            var filePath = Path.GetTempFileName();
                            using (var stream = System.IO.File.Create(filePath))
                            {
                                file.CopyToAsync(stream);

                                string upload = Helper.UploadFTP(stream, FileLampiran, FileLampiran_Ekstension);
                                if (upload != "success")
                                {
                                    FileLampiran = "";
                                    FileLampiran_Ekstension = "";
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

                #region Kirim Tanggapan

                string ID_Pengaduan = _ID;
                string Tanggapan = sani.Sanitize(Model.Tanggapan);

                DataTable dt = mssql.GetDataTable("SELECT * FROM tblT_Dumas WHERE ID = '" + _ID + "'");
                string Nomor = dt.Rows[0]["Nomor"].ToString();
                string Nama_Pelapor = dt.Rows[0]["Email"].ToString();
                string Email_Pelapor = dt.Rows[0]["Email"].ToString();

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", Model.ID));
                param.Add(new SqlParameter("@JenisPengaduan", "Dumas"));
                param.Add(new SqlParameter("@IDPengaduan", ID_Pengaduan));
                param.Add(new SqlParameter("@TipePengirim", Model.TipePengirim));
                param.Add(new SqlParameter("@Email", Email_Pelapor));
                param.Add(new SqlParameter("@Tanggapan", Tanggapan));
                param.Add(new SqlParameter("@FileLampiran", FileLampiran));
                param.Add(new SqlParameter("@FileLampiran_Ekstension", FileLampiran_Ekstension));
                //param.Add(new SqlParameter("@JenisPelanggaran", Model.JenisPelanggaran));
                param.Add(new SqlParameter("@CreatedBy", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));

                mssql.ExecuteNonQuery("sp_Kirim_Tanggapan_Pelapor", param);

                #endregion Save Pengaduan

                #region Notifikasi_ke_Admin_Pusat
                DataTable dt_Email_Admin_Pusat = mssql.GetDataTable("sp_Get_Email_Admin_Pusat");
                foreach (DataRow dr in dt_Email_Admin_Pusat.Rows)
                {
                    string EmailAdminPusat = dr["Email"].ToString();
                    string Fullname = dr["Fullname"].ToString();
                    Helper.SendMail(EmailAdminPusat, "SPP PTSMI - Respon Tanggapan Pengaduan", MailComposer.compose_mail_body_tanggapan_ke_petugas(ID_Pengaduan, Fullname, Nomor));
                }
                #endregion Notifikasi_ke_Admin_Pusat  

                return Json(new { Error = false, Message = "" });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Get_Msg_Tanggapan_by_Email()
        {
            int RowCount = 0;
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='/'");

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@Email", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));
                DataTable dt = mssql.GetDataTable("sp_Get_Msg_Tanggapan_by_Email", param);
                RowCount = int.Parse(dt.Rows.Count.ToString());
                List<MTanggapan> MainData = new List<MTanggapan>();
                MainData = mssql.ConvertDataTable<MTanggapan>(dt);
                return Json(new { Error = false, Message = MainData, RowCount = dt.Rows.Count.ToString() });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message, RowCount = RowCount });
            }
        }

        [HttpPost]
        public IActionResult Get_Jenis_Pelanggaran()
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='/'");

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@Tipe", "Jenis Pelanggaran"));
                param.Add(new SqlParameter("@Lang", "ID"));
                DataTable dt = mssql.GetDataTable("sp_Get_Single_CMS", param);
                List<Dropdown2> MainData = new List<Dropdown2>();
                MainData = mssql.ConvertDataTable<Dropdown2>(dt);
                return Json(new { Error = false, Message = MainData });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult SetSessionIDPengaduan(string ID)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='/'");

                string IDPengaduan = ""; // + "|" + HttpContext.Session.GetString("SessionID");
                DataTable dt = mssql.GetDataTable("SELECT * FROM tblT_Dumas WHERE ID = '" + sani.Sanitize(ID) + "'");
                if (dt.Rows.Count == 1)
                {
                    string EmailPelapor = dt.Rows[0]["Email"].ToString();
                    if (StringCipher.Decrypt(HttpContext.Session.GetString("Email")) != EmailPelapor)
                        throw new Exception("Invalid Authorization|window.location='/'");

                    IDPengaduan = StringCipher.Encrypt(dt.Rows[0]["ID"].ToString()) + "|" + HttpContext.Session.GetString("SessionID");
                }

                return Json(new { Error = false, Message = IDPengaduan });
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
                int isValid = 0;
                DataRow dr = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM FileEkstensionFilter WHERE Name = '" + sani.Sanitize(Eks) + "'");
                isValid = int.Parse(dr["Count"].ToString());
                return Json(new { Error = false, Message = isValid });
            }
            catch (Exception)
            {
                return Json(new { Error = true, Message = 0 });
            }
        }
    }
}
