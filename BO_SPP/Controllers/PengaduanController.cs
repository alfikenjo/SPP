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
    public class PengaduanController : Controller
    {
        private HtmlSanitizer sani = new HtmlSanitizer();

        public IActionResult DaftarPengaduan()
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                return RedirectToAction("Index", "Dashboard");

            ViewData["CurrentControllerName"] = "Pengaduan";
            ViewData["CurrentActionName"] = "Daftar Pengaduan";
            ViewData["Title"] = "Daftar Pengaduan";

            ViewData["Email"] = StringCipher.Decrypt(aes.Dec(HttpContext.Session.GetString("Email")));

            ViewBag.Email = StringCipher.Decrypt(aes.Dec(HttpContext.Session.GetString("Email")));
            ViewBag.Role = HttpContext.Session.GetString("fr");

            return View();
        }

        public IActionResult PengaduanForm(string ID)
        {
            if (string.IsNullOrEmpty(StringCipher.Decrypt(HttpContext.Session.GetString("Email"))))
            {
                if (!string.IsNullOrEmpty(ID))
                    return RedirectToAction("Signin", "Account", new { ID = ID });
                else
                    return RedirectToAction("Signin", "Account");
            }

            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                return RedirectToAction("Index", "Dashboard");

            ViewData["CurrentControllerName"] = "";
            ViewData["CurrentActionName"] = "Form Pengaduan";
            ViewData["Title"] = "Formulir Pengaduan";
            ViewData["Email"] = StringCipher.Decrypt(aes.Dec(HttpContext.Session.GetString("Email")));

            ViewBag.Email = StringCipher.Decrypt(aes.Dec(HttpContext.Session.GetString("Email")));
            ViewBag.Role = HttpContext.Session.GetString("fr");

            return View();
        }

        [HttpPost]
        public IActionResult GetPengaduanByEmail(string Status, string Jenis_Pelanggaran)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@Email", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));
                param.Add(new SqlParameter("@Status", sani.Sanitize(Status)));
                param.Add(new SqlParameter("@Jenis_Pelanggaran", sani.Sanitize(Jenis_Pelanggaran)));
                DataTable dt = mssql.GetDataTable("sp_Get_Dumas_by_Email", param);
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
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                string _ID = StringCipher.Decrypt(sani.Sanitize(ID).Split("|")[0]);
                string SessionIDDesc = sani.Sanitize(ID).Split("|")[1];

                if (SessionIDDesc != HttpContext.Session.GetString("SessionID"))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");



                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", _ID));
                DataTable dt = mssql.GetDataTable("sp_Get_Dumas_by_ID", param);

                if (dt.Rows.Count != 1)
                    throw new Exception("Invalid request|window.location='../Pengaduan/DaftarPengaduan'");

                if (!Helper.GrantedPengaduan(_ID, StringCipher.Decrypt(HttpContext.Session.GetString("Email")), HttpContext.Session.GetString("fr")))
                {
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");
                }

                List<Pengaduan> MainData = new List<Pengaduan>();
                MainData = mssql.ConvertDataTable<Pengaduan>(dt);

                for (int i = 0; i < MainData.Count; i++)
                {
                    MainData[i].ID = StringCipher.Encrypt(MainData[i].ID) + "|" + HttpContext.Session.GetString("SessionID");

                    if (MainData[i].PenyaluranByDate != null)
                        MainData[i].PenyaluranByDate = "Direspon oleh Admin SPP: " + MainData[i].PenyaluranBy + " @" + MainData[i].PenyaluranByDate;

                    if (MainData[i].TindakLanjutByDate != null)
                        MainData[i].TindakLanjutByDate = "Ditindaklanjuti oleh Grup Delegator: " + MainData[i].DelegatorName + " / " + MainData[i].TindakLanjutBy + " @" + MainData[i].TindakLanjutByDate;

                    if (MainData[i].ResponByDate != null)
                        MainData[i].ResponByDate = "Diselesaikan oleh Admin SPP: " + MainData[i].ResponBy + " @" + MainData[i].ResponByDate;

                    string FilePenyaluran = MainData[i].Keterangan_Penyaluran_Filename;
                    if (!string.IsNullOrEmpty(FilePenyaluran))
                        MainData[i].FilepathFilePenyaluran = Helper.GetBinaryImageEncrypted(FilePenyaluran, MainData[i].Keterangan_Penyaluran_Ekstension);

                    string FilePemeriksaan = MainData[i].Keterangan_Pemeriksaan_Filename;
                    if (!string.IsNullOrEmpty(FilePemeriksaan))
                        MainData[i].FilepathFilePemeriksaan = Helper.GetBinaryImageEncrypted(FilePemeriksaan, MainData[i].Keterangan_Pemeriksaan_Ekstension);

                    string FileKonfirmasi = MainData[i].Keterangan_Konfirmasi_Filename;
                    if (!string.IsNullOrEmpty(FileKonfirmasi))
                        MainData[i].FilepathFileKonfirmasi = Helper.GetBinaryImageEncrypted(FileKonfirmasi, MainData[i].Keterangan_Konfirmasi_Ekstension);

                    string FileRespon = MainData[i].Keterangan_Respon_Filename;
                    if (!string.IsNullOrEmpty(FileRespon))
                        MainData[i].FilepathFileRespon = Helper.GetBinaryImageEncrypted(FileRespon, MainData[i].Keterangan_Respon_Ekstension);
                }

                return Json(new { Error = false, Message = MainData });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [DisableRequestSizeLimit]
        [HttpPost]
        public IActionResult SavePengaduan([FromForm] Pengaduan Model)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                string ID = sani.Sanitize(Model.ID);
                string _ID = "";

                if (!string.IsNullOrEmpty(ID))
                {
                    _ID = StringCipher.Decrypt(ID.Split("|")[0]);
                    string SessionIDDesc = ID.Split("|")[1];

                    if (SessionIDDesc != HttpContext.Session.GetString("SessionID"))
                        throw new Exception("Invalid Authorization|window.location='../Account/Signin'");
                }

                if (string.IsNullOrEmpty(_ID))
                    throw new Exception("Ditolak, Anda belum menginput identitas pihak yang terlapor, mohon periksa kembali");

                DataRow drDetailPengaduan = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM tblT_Dumas_Detail WHERE ID_Header = '" + _ID + "'");
                if (int.Parse(drDetailPengaduan["Count"].ToString()) == 0)
                    throw new Exception("Ditolak, Anda belum menginput identitas pihak yang terlapor, mohon periksa kembali");

                string enc_Email = sani.Sanitize(Model.enc_Email);
                string enc_Handphone = sani.Sanitize(Model.enc_Handphone);
                string enc_TempatKejadian = sani.Sanitize(Model.enc_TempatKejadian);
                string enc_Kronologi = sani.Sanitize(Model.enc_Kronologi);
                string enc_PenyaluranBy = sani.Sanitize(Model.enc_PenyaluranBy);
                string enc_TindakLanjutBy = sani.Sanitize(Model.enc_TindakLanjutBy);
                string enc_ResponBy = sani.Sanitize(Model.enc_ResponBy);
                string enc_CreatedBy = sani.Sanitize(Model.enc_CreatedBy);
                string enc_UpdatedBy = sani.Sanitize(Model.enc_UpdatedBy);
                string enc_ProsesBy = sani.Sanitize(Model.enc_ProsesBy);
                string enc_Jenis_Pelanggaran = sani.Sanitize(Model.enc_Jenis_Pelanggaran);
                string enc_Keterangan_Penyaluran = sani.Sanitize(Model.enc_Keterangan_Penyaluran);
                string enc_Keterangan_Pemeriksaan = sani.Sanitize(Model.enc_Keterangan_Pemeriksaan);
                string enc_Keterangan_Konfirmasi = sani.Sanitize(Model.enc_Keterangan_Konfirmasi);
                string enc_Keterangan_Respon = sani.Sanitize(Model.enc_Keterangan_Respon);

                if (!string.IsNullOrEmpty(enc_Email))
                {
                    DataRow drCekIsExistEmailAdmin = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM vw_UserInRole WHERE Email = '" + enc_Email + "' AND Role IN ('Admin SPP', 'System Administrator', 'Delegator')");
                    if (int.Parse(drCekIsExistEmailAdmin["Count"].ToString()) > 0)
                        throw new Exception("Ditolak, Email pelapor sudah terdaftar sebagai petugas Admin, mohon periksa kembali");
                }


                #region UploadFileEvidence
                if (Model.UploadFileEvidence != null)
                {
                    foreach (IFormFile file in Model.UploadFileEvidence)
                    {
                        if (file.Length > 0)
                        {
                            string FileEvidence = "", FileEvidence_Ekstension = "";
                            string newFileID = Guid.NewGuid().ToString();
                            FileEvidence_Ekstension = System.IO.Path.GetExtension(file.FileName).Trim().ToString();
                            FileEvidence = newFileID.ToString();
                            var filePath = Path.GetTempFileName();
                            using (var stream = System.IO.File.Create(filePath))
                            {
                                file.CopyTo(stream);

                                string upload = Helper.UploadFTPWithEcryption(file, FileEvidence, FileEvidence_Ekstension);
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
                param.Add(new SqlParameter("@Sumber", sani.Sanitize(Model.Sumber)));
                param.Add(new SqlParameter("@Nomor", Nomor));
                param.Add(new SqlParameter("@Email", enc_Email));
                param.Add(new SqlParameter("@Handphone", enc_Handphone));
                param.Add(new SqlParameter("@Jenis_Pelanggaran", enc_Jenis_Pelanggaran));
                param.Add(new SqlParameter("@TempatKejadian", enc_TempatKejadian));
                param.Add(new SqlParameter("@WaktuKejadian", WaktuKejadian));
                param.Add(new SqlParameter("@Kronologi", enc_Kronologi));
                param.Add(new SqlParameter("@Status", Status));
                param.Add(new SqlParameter("@CreatedBy", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));

                mssql.ExecuteNonQuery("sp_Kirim_Dumas", param);

                #endregion Save Pengaduan

                #region Notifikasi_ke_Admin_Pusat
                string Subject = "SPP - PTSMI [Pengaduan Baru]";
                DataTable dt_Email_Admin_Pusat = mssql.GetDataTable("sp_Get_Email_Admin_Pusat");
                foreach (DataRow dr in dt_Email_Admin_Pusat.Rows)
                {
                    string EmailPelapor = !string.IsNullOrEmpty(enc_Email) ? aes.Dec(enc_Email) : "not available";
                    string EmailAdminPusat = aes.Dec(dr["Email"].ToString());
                    string Fullname = !string.IsNullOrEmpty(dr["Fullname"].ToString()) ? aes.Dec(dr["Fullname"].ToString()) : "";
                    Helper.SendMail(EmailAdminPusat, Subject, MailComposer.compose_mail_body_kirim_dumas(_ID, Fullname, Nomor, EmailPelapor, TanggalKirim));
                }
                #endregion Notifikasi_ke_Admin_Pusat

                #region Notifikasi_ke_Pelapor
                if (!string.IsNullOrEmpty(enc_Email))
                {
                    string EmailUser = enc_Email;
                    string NewRandomPassword = Helper.RandomString(8);
                    string HashPassword = PassHash.HashPassword(NewRandomPassword);
                    string New_User_Verification_ID = Guid.NewGuid().ToString();
                    string Mobile = enc_Handphone;

                    DataRow drRegistered = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM vw_UserInRole WHERE Email = '" + EmailUser + "' AND Role = 'Pelapor' AND ISNULL(isActive, 0) = 1");
                    int isRegistered = int.Parse(drRegistered["Count"].ToString());
                    if (isRegistered == 0)
                    {
                        if (!string.IsNullOrEmpty(Mobile))
                        {
                            DataRow dr_IsExist_Mobile = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM tblM_User WHERE Mobile = '" + Mobile + "' AND ISNULL(isActive, 0) = 1");
                            if (int.Parse(dr_IsExist_Mobile["Count"].ToString()) > 0)
                                Mobile = "";
                        }

                        string UserID = Guid.NewGuid().ToString();
                        int isActive = 0;

                        List<SqlParameter> paramRegister = new List<SqlParameter>();
                        paramRegister.Add(new SqlParameter("@UserID", UserID));
                        paramRegister.Add(new SqlParameter("@New_User_Verification_ID", New_User_Verification_ID));
                        paramRegister.Add(new SqlParameter("@Fullname", ""));
                        paramRegister.Add(new SqlParameter("@Email", EmailUser));
                        paramRegister.Add(new SqlParameter("@Mobile", Mobile));
                        paramRegister.Add(new SqlParameter("@PasswordHash", HashPassword));
                        paramRegister.Add(new SqlParameter("@isActive", isActive));
                        paramRegister.Add(new SqlParameter("@CreatedBy", EmailUser));

                        mssql.ExecuteNonQuery("sp_Register_User", paramRegister);
                    }

                    if (Helper.SendMail(aes.Dec(EmailUser), Subject, MailComposer.compose_mail_body_kirim_dumas_pengadu(aes.Dec(EmailUser), Nomor, TanggalKirim, isRegistered, NewRandomPassword, New_User_Verification_ID)) == 1)
                    {
                        mssql.ExecuteNonQuery("UPDATE tblT_New_User_Verification SET Verification_Mail_Status = 'Sent on " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.000") + "' WHERE ID = '" + New_User_Verification_ID + "'");
                    }
                }

                #endregion Notifikasi_ke_Pelapor

                return Json(new { Error = false, Message = Nomor });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [DisableRequestSizeLimit]
        [HttpPost]
        public IActionResult SaveDetailPengaduan([FromForm] PengaduanDetail Model)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                if (string.IsNullOrEmpty(sani.Sanitize(Model.Nama)) && sani.Sanitize(Model.Action) != "hapus")
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
                        throw new Exception("Invalid Authorization|window.location='../Account/Signin'");
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
                        throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                }

                string FileIdentitas = "", FileIdentitas_Ekstension = "";
                #region UploadFileIdentitas
                if (Model.UploadFileIdentitas != null)
                {
                    List<IFormFile> files = new List<IFormFile>();
                    files.Add(Model.UploadFileIdentitas);
                    foreach (IFormFile file in files)
                    {
                        if (file.Length > 0)
                        {
                            FileIdentitas_Ekstension = System.IO.Path.GetExtension(file.FileName).Trim();
                            FileIdentitas = ID;

                            var filePath = Path.GetTempFileName();
                            using (var stream = System.IO.File.Create(filePath))
                            {
                                file.CopyTo(stream);

                                string upload = Helper.UploadFTPWithEcryption(file, FileIdentitas, FileIdentitas_Ekstension);
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
                param.Add(new SqlParameter("@Email", sani.Sanitize(Model.enc_Email)));
                param.Add(new SqlParameter("@Handphone", sani.Sanitize(Model.enc_Handphone)));
                param.Add(new SqlParameter("@Nama", sani.Sanitize(Model.enc_Nama)));
                param.Add(new SqlParameter("@NomorHandphone", sani.Sanitize(Model.enc_NomorHandphone)));
                param.Add(new SqlParameter("@Departemen", sani.Sanitize(Model.enc_Departemen)));
                param.Add(new SqlParameter("@Jabatan", sani.Sanitize(Model.enc_Jabatan)));
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
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                string ID = sani.Sanitize(ID_Header);

                string _ID = StringCipher.Decrypt(ID.Split("|")[0]);
                string SessionIDDesc = ID.Split("|")[1];

                if (SessionIDDesc != HttpContext.Session.GetString("SessionID"))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID_Header", _ID));
                DataTable dt = mssql.GetDataTable("sp_Get_Detail_PengaduanByIDHeader", param);
                List<PengaduanDetail> MainData = new List<PengaduanDetail>();
                MainData = mssql.ConvertDataTable<PengaduanDetail>(dt);
                for (int i = 0; i < MainData.Count; i++)
                {
                    MainData[i].ID = StringCipher.Encrypt(MainData[i].ID) + "|" + HttpContext.Session.GetString("SessionID");
                    var FileIdentitas = MainData[i].FileIdentitas;
                    if (!string.IsNullOrEmpty(FileIdentitas))
                    {
                        MainData[i].FilepathFileIdentitas = Helper.GetBinaryImageEncrypted(MainData[i].FileIdentitas, MainData[i].FileIdentitas_Ekstension);
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
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                ID = sani.Sanitize(ID);

                string _ID = StringCipher.Decrypt(ID.Split("|")[0]);
                string SessionIDDesc = ID.Split("|")[1];

                if (SessionIDDesc != HttpContext.Session.GetString("SessionID"))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                if (!Helper.GrantedPengaduan(_ID, StringCipher.Decrypt(HttpContext.Session.GetString("Email")), HttpContext.Session.GetString("fr")))
                {
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");
                }


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
                        MainData[i].FilepathFileIdentitas = Helper.GetBinaryImageEncrypted(MainData[i].FileIdentitas, MainData[i].FileIdentitas_Ekstension);
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
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                string ID = sani.Sanitize(ID_Header);

                string _ID = StringCipher.Decrypt(ID.Split("|")[0]);
                string SessionIDDesc = ID.Split("|")[1];

                if (SessionIDDesc != HttpContext.Session.GetString("SessionID"))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

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
                        MainData[i].FilepathFileEvidence = Helper.GetBinaryImageEncrypted(MainData[i].FileEvidence, MainData[i].FileEvidence_Ekstension);
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
        public IActionResult ReadTanggapanPelapor(string ID_Pengaduan)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                string _ID = StringCipher.Decrypt(sani.Sanitize(ID_Pengaduan).Split("|")[0]);
                mssql.ExecuteNonQuery("UPDATE tblT_Tanggapan SET IsRead = 1, ReadOn = GETDATE() WHERE IDPengaduan = '" + _ID + "' AND TipePengirim = 'Pelapor' AND IsRead = 0");
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
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                string ID = sani.Sanitize(ID_Pengaduan);

                string _ID = StringCipher.Decrypt(ID.Split("|")[0]);
                string SessionIDDesc = ID.Split("|")[1];

                if (SessionIDDesc != HttpContext.Session.GetString("SessionID"))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@IDPengaduan", _ID));
                DataTable dt = mssql.GetDataTable("sp_Get_TanggapanByIDPengaduan", param);
                List<MTanggapan> MainData = new List<MTanggapan>();
                MainData = mssql.ConvertDataTable<MTanggapan>(dt);

                for (int i = 0; i < MainData.Count; i++)
                {
                    MainData[i].IDPengaduan = StringCipher.Encrypt(MainData[i].IDPengaduan) + "|" + HttpContext.Session.GetString("SessionID");
                    var FileLampiran = MainData[i].FileLampiran;
                    if (!string.IsNullOrEmpty(FileLampiran))
                    {
                        string FileLampiran_Ekstension = MainData[i].FileLampiran_Ekstension.ToLower();
                        if (FileLampiran_Ekstension == ".jpg" || FileLampiran_Ekstension == ".jpeg" || FileLampiran_Ekstension == ".png")
                            MainData[i].FilepathFileLampiran = Helper.GetBinaryImageEncrypted(MainData[i].FileLampiran, MainData[i].FileLampiran_Ekstension);
                    }
                }

                return Json(new { Error = false, Message = MainData });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [DisableRequestSizeLimit]
        [HttpPost]
        public IActionResult KirimTanggapanAdminSPP([FromForm] MTanggapan Model)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                string IDPengaduan = sani.Sanitize(Model.IDPengaduan);

                string _ID = StringCipher.Decrypt(IDPengaduan.Split("|")[0]);
                string SessionIDDesc = IDPengaduan.Split("|")[1];

                if (SessionIDDesc != HttpContext.Session.GetString("SessionID"))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                Model.ID = Guid.NewGuid().ToString();

                string FileLampiran = "", FileLampiran_Ekstension = "";
                #region UploadFileLampiran
                if (Model.UploadFileLampiran != null)
                {
                    List<IFormFile> files = new List<IFormFile>();
                    files.Add(Model.UploadFileLampiran);
                    foreach (IFormFile file in files)
                    {
                        if (file.Length > 0)
                        {
                            FileLampiran_Ekstension = System.IO.Path.GetExtension(file.FileName).Trim();
                            FileLampiran = Model.ID.ToString();

                            var filePath = Path.GetTempFileName();
                            using (var stream = System.IO.File.Create(filePath))
                            {
                                file.CopyTo(stream);

                                string upload = Helper.UploadFTPWithEcryption(file, FileLampiran, FileLampiran_Ekstension);
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

                string enc_Tanggapan = sani.Sanitize(Model.enc_Tanggapan);

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", Model.ID));
                param.Add(new SqlParameter("@JenisPengaduan", "Dumas"));
                param.Add(new SqlParameter("@IDPengaduan", _ID));
                param.Add(new SqlParameter("@TipePengirim", sani.Sanitize(Model.TipePengirim)));
                param.Add(new SqlParameter("@Email", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));
                param.Add(new SqlParameter("@Tanggapan", enc_Tanggapan));
                param.Add(new SqlParameter("@FileLampiran", FileLampiran));
                param.Add(new SqlParameter("@FileLampiran_Ekstension", FileLampiran_Ekstension));
                param.Add(new SqlParameter("@CreatedBy", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));

                mssql.ExecuteNonQuery("sp_Kirim_Tanggapan_Admin_SPP", param);

                #endregion Save Pengaduan

                #region Notifikasi_ke_Pelapor
                int EmailSent = 0;
                DataTable dt = mssql.GetDataTable("SELECT * FROM tblT_Dumas WHERE ID = '" + _ID + "'");
                string Nomor = dt.Rows[0]["Nomor"].ToString();
                foreach (DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["Email"].ToString()))
                    {
                        string EmailPelapor = aes.Dec(dr["Email"].ToString());
                        EmailSent = Helper.SendMail(EmailPelapor, "SPP PTSMI - Respon Tanggapan Pengaduan", MailComposer.compose_mail_body_tanggapan_ke_pelapor(Nomor));
                    }
                }
                #endregion Notifikasi_ke_Pelapor  

                return Json(new { Error = false, Message = EmailSent });
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
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                DataRow dr_Login = mssql.GetDataRow("SELECT COUNT(*) [Jumlah] FROM tblT_User_Login WHERE UserID = '" + StringCipher.Decrypt(HttpContext.Session.GetString("UserID")) + "' AND ID = '" + StringCipher.Decrypt(HttpContext.Session.GetString("SessionID")) + "' AND isActive = 1");
                if (int.Parse(dr_Login["Jumlah"].ToString()) == 0)
                    return RedirectToAction("Signout", "Account");

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
        [DisableRequestSizeLimit]
        public IActionResult SavePenyaluran([FromForm] Pengaduan Model)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                string ID = sani.Sanitize(Model.ID);

                string _ID = StringCipher.Decrypt(ID.Split("|")[0]);
                string SessionIDDesc = ID.Split("|")[1];

                if (SessionIDDesc != HttpContext.Session.GetString("SessionID"))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                if (sani.Sanitize(Model.Status) == "Diproses" && string.IsNullOrEmpty(sani.Sanitize(Model.DelegatorID)))
                    throw new Exception("Ditolak, Anda belum memilih grup delegator, mohon periksa kembali");

                if (sani.Sanitize(Model.Status) == "Diproses" && string.IsNullOrEmpty(sani.Sanitize(Model.Jenis_Pelanggaran)))
                    throw new Exception("Maaf, Anda perlu menentukan Jenis Pelanggaran untuk pengaduan ini, mohon periksa kembali");

                string Keterangan_Penyaluran_Filename = "", Keterangan_Penyaluran_Ekstension = "";
                #region Upload
                if (Model.Upload != null)
                {
                    List<IFormFile> files = new List<IFormFile>();
                    files.Add(Model.Upload);
                    foreach (IFormFile file in files)
                    {
                        if (file.Length > 0)
                        {
                            Keterangan_Penyaluran_Ekstension = System.IO.Path.GetExtension(file.FileName).Trim();
                            Keterangan_Penyaluran_Filename = "Penyaluran_" + _ID;

                            var filePath = Path.GetTempFileName();
                            using (var stream = System.IO.File.Create(filePath))
                            {
                                file.CopyTo(stream);

                                string upload = Helper.UploadFTPWithEcryption(file, Keterangan_Penyaluran_Filename, Keterangan_Penyaluran_Ekstension);
                                if (upload != "success")
                                {
                                    Keterangan_Penyaluran_Filename = "";
                                    Keterangan_Penyaluran_Ekstension = "";
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

                #region Save

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", _ID));
                param.Add(new SqlParameter("@Status", sani.Sanitize(Model.Status)));
                param.Add(new SqlParameter("@DelegatorID", sani.Sanitize(Model.DelegatorID)));
                param.Add(new SqlParameter("@Jenis_Pelanggaran", sani.Sanitize(Model.enc_Jenis_Pelanggaran)));
                param.Add(new SqlParameter("@Keterangan_Penyaluran", sani.Sanitize(Model.enc_Keterangan_Penyaluran)));
                param.Add(new SqlParameter("@Keterangan_Penyaluran_Filename", Keterangan_Penyaluran_Filename));
                param.Add(new SqlParameter("@Keterangan_Penyaluran_Ekstension", Keterangan_Penyaluran_Ekstension));
                param.Add(new SqlParameter("@CreatedBy", aes.Enc(HttpContext.Session.GetString("fn"))));
                mssql.ExecuteNonQuery("sp_Save_Penyaluran", param);

                #endregion Save

                #region Notifikasi_ke_Delegator
                int EmailSent = 0;
                string Subject = "SPP - PTSMI [Pengaduan Baru]";
                List<SqlParameter> paramEmail = new List<SqlParameter>();
                paramEmail.Add(new SqlParameter("@IDPengaduan", _ID));
                DataTable dt_Email_Delegator = mssql.GetDataTable("sp_Get_Email_Delegator", paramEmail);
                foreach (DataRow dr in dt_Email_Delegator.Rows)
                {
                    string EmailDelegator = aes.Dec(dr["Email"].ToString());
                    string Fullname = aes.Dec(dr["Fullname"].ToString());
                    string Nomor = dr["Nomor"].ToString();
                    string EmailPelapor = !string.IsNullOrEmpty(dr["EmailPelapor"].ToString()) ? aes.Dec(dr["EmailPelapor"].ToString()) : "";
                    string DelegatorName = dr["DelegatorName"].ToString();
                    string TanggalKirim = dr["TanggalKirim"].ToString();
                    EmailSent = Helper.SendMail(EmailDelegator, Subject, MailComposer.compose_mail_body_submit_delegasi(_ID, Fullname, Nomor, EmailPelapor, DelegatorName, TanggalKirim));
                }
                #endregion Notifikasi_ke_Delegator

                return Json(new { Error = false, Message = EmailSent });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public IActionResult Save_TindakLanjut([FromForm] Pengaduan Model)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                string ID = sani.Sanitize(Model.ID);

                string _ID = StringCipher.Decrypt(ID.Split("|")[0]);
                string SessionIDDesc = ID.Split("|")[1];

                if (SessionIDDesc != HttpContext.Session.GetString("SessionID"))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                if (string.IsNullOrEmpty(sani.Sanitize(Model.Keterangan_Pemeriksaan)))
                    throw new Exception("Maaf Anda belum mengisikan Keterangan Pemeriksaan");

                if (string.IsNullOrEmpty(sani.Sanitize(Model.Keterangan_Konfirmasi)))
                    throw new Exception("Maaf Anda belum mengisikan Keterangan Konfirmasi");

                string Keterangan_Pemeriksaan_Filename = "", Keterangan_Pemeriksaan_Ekstension = "";
                #region UploadPemeriksaan
                if (Model.UploadPemeriksaan != null)
                {
                    List<IFormFile> files = new List<IFormFile>();
                    files.Add(Model.UploadPemeriksaan);
                    foreach (IFormFile file in files)
                    {
                        if (file.Length > 0)
                        {
                            Keterangan_Pemeriksaan_Ekstension = System.IO.Path.GetExtension(file.FileName).Trim();
                            Keterangan_Pemeriksaan_Filename = "Pemeriksaan_" + _ID;

                            var filePath = Path.GetTempFileName();
                            using (var stream = System.IO.File.Create(filePath))
                            {
                                file.CopyTo(stream);

                                string UploadPemeriksaan = Helper.UploadFTPWithEcryption(file, Keterangan_Pemeriksaan_Filename, Keterangan_Pemeriksaan_Ekstension);
                                if (UploadPemeriksaan != "success")
                                {
                                    Keterangan_Pemeriksaan_Filename = "";
                                    Keterangan_Pemeriksaan_Ekstension = "";
                                    throw new Exception(UploadPemeriksaan);
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

                string Keterangan_Konfirmasi_Filename = "", Keterangan_Konfirmasi_Ekstension = "";
                #region UploadKonfirmasi
                if (Model.UploadKonfirmasi != null)
                {
                    List<IFormFile> files = new List<IFormFile>();
                    files.Add(Model.UploadKonfirmasi);
                    foreach (IFormFile file in files)
                    {
                        if (file.Length > 0)
                        {
                            Keterangan_Konfirmasi_Ekstension = System.IO.Path.GetExtension(file.FileName).Trim();
                            Keterangan_Konfirmasi_Filename = "Konfirmasi_" + _ID;

                            var filePath = Path.GetTempFileName();
                            using (var stream = System.IO.File.Create(filePath))
                            {
                                file.CopyTo(stream);

                                string UploadKonfirmasi = Helper.UploadFTPWithEcryption(file, Keterangan_Konfirmasi_Filename, Keterangan_Konfirmasi_Ekstension);
                                if (UploadKonfirmasi != "success")
                                {
                                    Keterangan_Konfirmasi_Filename = "";
                                    Keterangan_Konfirmasi_Ekstension = "";
                                    throw new Exception(UploadKonfirmasi);
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

                #region Save

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", _ID));
                param.Add(new SqlParameter("@Status", sani.Sanitize(Model.Status)));
                param.Add(new SqlParameter("@Keterangan_Pemeriksaan", sani.Sanitize(Model.enc_Keterangan_Pemeriksaan)));
                param.Add(new SqlParameter("@Keterangan_Pemeriksaan_Filename", Keterangan_Pemeriksaan_Filename));
                param.Add(new SqlParameter("@Keterangan_Pemeriksaan_Ekstension", Keterangan_Pemeriksaan_Ekstension));
                param.Add(new SqlParameter("@Keterangan_Konfirmasi", sani.Sanitize(Model.enc_Keterangan_Konfirmasi)));
                param.Add(new SqlParameter("@Keterangan_Konfirmasi_Filename", Keterangan_Konfirmasi_Filename));
                param.Add(new SqlParameter("@Keterangan_Konfirmasi_Ekstension", Keterangan_Konfirmasi_Ekstension));
                param.Add(new SqlParameter("@CreatedBy", HttpContext.Session.GetString("fn")));
                mssql.ExecuteNonQuery("sp_Save_TindakLanjut", param);

                #endregion Save

                #region Notifikasi_ke_Admin_SPP
                int EmailSent = 0;
                string Subject = "SPP - PTSMI [Hasil Tindak Lanjut Delegator]";
                List<SqlParameter> paramEmail = new List<SqlParameter>();
                paramEmail.Add(new SqlParameter("@IDPengaduan", _ID));
                DataTable dt_Email_Admin_SPP = mssql.GetDataTable("sp_Get_Email_Admin_SPP", paramEmail);
                foreach (DataRow dr in dt_Email_Admin_SPP.Rows)
                {
                    string EmailAdminSPP = aes.Dec(dr["Email"].ToString());
                    string Fullname = !string.IsNullOrEmpty(dr["Fullname"].ToString()) ? aes.Dec(dr["Fullname"].ToString()) : "";
                    string Nomor = dr["Nomor"].ToString();
                    string EmailPelapor = !string.IsNullOrEmpty(dr["EmailPelapor"].ToString()) ? aes.Dec(dr["EmailPelapor"].ToString()) : "";
                    string DelegatorName = dr["DelegatorName"].ToString();
                    string TanggalKirim = dr["TanggalKirim"].ToString();
                    EmailSent = Helper.SendMail(EmailAdminSPP, Subject, MailComposer.compose_mail_body_submit_dari_delegator_ke_admin_spp(_ID, Fullname, Nomor, EmailPelapor, DelegatorName, TanggalKirim, sani.Sanitize(Model.Status)));
                }
                #endregion Notifikasi_ke_Admin_SPP

                return Json(new { Error = false, Message = EmailSent });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public IActionResult SaveRespon([FromForm] Pengaduan Model)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                string ID = sani.Sanitize(Model.ID);

                string _ID = StringCipher.Decrypt(ID.Split("|")[0]);
                string SessionIDDesc = ID.Split("|")[1];

                if (SessionIDDesc != HttpContext.Session.GetString("SessionID"))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                string Keterangan_Respon_Filename = "", Keterangan_Respon_Ekstension = "";
                #region Upload
                if (Model.Upload != null)
                {
                    List<IFormFile> files = new List<IFormFile>();
                    files.Add(Model.Upload);
                    foreach (IFormFile file in files)
                    {
                        if (file.Length > 0)
                        {
                            Keterangan_Respon_Ekstension = System.IO.Path.GetExtension(file.FileName).Trim();
                            Keterangan_Respon_Filename = "Respon_" + _ID;

                            var filePath = Path.GetTempFileName();
                            using (var stream = System.IO.File.Create(filePath))
                            {
                                file.CopyTo(stream);

                                string upload = Helper.UploadFTPWithEcryption(file, Keterangan_Respon_Filename, Keterangan_Respon_Ekstension);
                                if (upload != "success")
                                {
                                    Keterangan_Respon_Filename = "";
                                    Keterangan_Respon_Ekstension = "";
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

                #region Save

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", _ID));
                param.Add(new SqlParameter("@Status", sani.Sanitize(Model.Status)));
                param.Add(new SqlParameter("@Keterangan_Respon", sani.Sanitize(Model.enc_Keterangan_Respon)));
                param.Add(new SqlParameter("@Keterangan_Respon_Filename", Keterangan_Respon_Filename));
                param.Add(new SqlParameter("@Keterangan_Respon_Ekstension", Keterangan_Respon_Ekstension));
                param.Add(new SqlParameter("@CreatedBy", HttpContext.Session.GetString("fn")));
                mssql.ExecuteNonQuery("sp_Save_Respon", param);

                #endregion Save

                #region Notifikasi_ke_Pelapor
                int EmailSent = 0;
                string Subject = "SPP - PTSMI [Pengaduan Baru]";
                DataTable dt_Email_Pelapor = mssql.GetDataTable("SELECT Email, Nomor, dbo.FormatDate_Indonesia_Lengkap(CreatedOn) [TanggalKirim] FROM tblT_Dumas WHERE ID = '" + _ID + "'");
                foreach (DataRow dr in dt_Email_Pelapor.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["Email"].ToString()))
                    {
                        string EmailPelapor = aes.Dec(dr["Email"].ToString());
                        string Nomor = dr["Nomor"].ToString();
                        string TanggalKirim = dr["TanggalKirim"].ToString();
                        EmailSent = Helper.SendMail(EmailPelapor, Subject, MailComposer.compose_mail_body_kirim_respon_ke_pelapor(_ID, Nomor, TanggalKirim, sani.Sanitize(Model.Status)));
                    }
                }
                #endregion Notifikasi_ke_Pelapor

                return Json(new { Error = false, Message = EmailSent });
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
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                string DelegatorID = "";
                DataTable dt = mssql.GetDataTable("SELECT * FROM tblT_Dumas WHERE ID = '" + sani.Sanitize(ID) + "'");
                if (dt.Rows.Count == 1)
                {
                    DelegatorID = StringCipher.Encrypt(dt.Rows[0]["ID"].ToString()) + "|" + HttpContext.Session.GetString("SessionID");
                }

                return Json(new { Error = false, Message = DelegatorID });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult GetTanggapanInternalByIDPengaduan(string ID_Pengaduan, string TipePengirim)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                string ID = sani.Sanitize(ID_Pengaduan);

                string _ID = StringCipher.Decrypt(ID.Split("|")[0]);
                string SessionIDDesc = ID.Split("|")[1];

                if (SessionIDDesc != HttpContext.Session.GetString("SessionID"))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@IDPengaduan", _ID));
                param.Add(new SqlParameter("@TipePengirim", sani.Sanitize(TipePengirim)));
                DataTable dt = mssql.GetDataTable("sp_Get_TanggapanInternalByIDPengaduan", param);
                List<MTanggapan> MainData = new List<MTanggapan>();
                MainData = mssql.ConvertDataTable<MTanggapan>(dt);

                for (int i = 0; i < MainData.Count; i++)
                {
                    MainData[i].IDPengaduan = StringCipher.Encrypt(MainData[i].IDPengaduan) + "|" + HttpContext.Session.GetString("SessionID");
                    var FileLampiran = MainData[i].FileLampiran;
                    if (!string.IsNullOrEmpty(FileLampiran))
                    {
                        string FileLampiran_Ekstension = MainData[i].FileLampiran_Ekstension.ToLower();
                        if (FileLampiran_Ekstension == ".jpg" || FileLampiran_Ekstension == ".jpeg" || FileLampiran_Ekstension == ".png")
                            MainData[i].FilepathFileLampiran = Helper.GetBinaryImageEncrypted(MainData[i].FileLampiran, MainData[i].FileLampiran_Ekstension);
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
        [DisableRequestSizeLimit]
        public IActionResult KirimTanggapanInternal([FromForm] MTanggapan Model)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                string IDPengaduan = sani.Sanitize(Model.IDPengaduan);

                string _ID = StringCipher.Decrypt(IDPengaduan.Split("|")[0]);
                string SessionIDDesc = IDPengaduan.Split("|")[1];

                if (SessionIDDesc != HttpContext.Session.GetString("SessionID"))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                Model.ID = Guid.NewGuid().ToString();

                string FileLampiran = "", FileLampiran_Ekstension = "";
                #region UploadFileLampiran
                if (Model.UploadFileLampiran != null)
                {
                    List<IFormFile> files = new List<IFormFile>();
                    files.Add(Model.UploadFileLampiran);
                    foreach (IFormFile file in files)
                    {
                        if (file.Length > 0)
                        {
                            FileLampiran_Ekstension = System.IO.Path.GetExtension(file.FileName).Trim();
                            FileLampiran = Model.ID.ToString();

                            var filePath = Path.GetTempFileName();
                            using (var stream = System.IO.File.Create(filePath))
                            {
                                file.CopyTo(stream);

                                string upload = Helper.UploadFTPWithEcryption(file, FileLampiran, FileLampiran_Ekstension);
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
                string Tanggapan = sani.Sanitize(Model.enc_Tanggapan);

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", Model.ID));
                param.Add(new SqlParameter("@JenisPengaduan", "Dumas"));
                param.Add(new SqlParameter("@IDPengaduan", _ID));
                param.Add(new SqlParameter("@TipePengirim", sani.Sanitize(Model.TipePengirim)));
                param.Add(new SqlParameter("@Email", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));
                param.Add(new SqlParameter("@Tanggapan", Tanggapan));
                param.Add(new SqlParameter("@FileLampiran", FileLampiran));
                param.Add(new SqlParameter("@FileLampiran_Ekstension", FileLampiran_Ekstension));
                param.Add(new SqlParameter("@CreatedBy", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));

                mssql.ExecuteNonQuery("sp_Kirim_Tanggapan_Internal", param);

                int EmailSent = 0;
                DataTable dtDumas = mssql.GetDataTable("SELECT * FROM tblT_Dumas WHERE ID = '" + _ID + "'");
                string Nomor = dtDumas.Rows[0]["Nomor"].ToString();

                #endregion Save Pengaduan

                if (sani.Sanitize(Model.TipePengirim) == "Internal - Delegator")
                {
                    #region Notifikasi_ke_Admin_Pusat
                    DataTable dt_Email_Admin_Pusat = mssql.GetDataTable("sp_Get_Email_Admin_Pusat");
                    foreach (DataRow dr in dt_Email_Admin_Pusat.Rows)
                    {
                        string EmailAdminPusat = aes.Dec(dr["Email"].ToString());
                        string Fullname = aes.Dec(dr["Fullname"].ToString());
                        EmailSent = Helper.SendMail(EmailAdminPusat, "SPP PTSMI - Respon Tanggapan Pengaduan", MailComposer.compose_mail_body_tanggapan_ke_Admin_SPP(ID_Pengaduan, Fullname, Nomor));
                    }
                    #endregion Notifikasi_ke_Admin_Pusat  
                }
                else if (sani.Sanitize(Model.TipePengirim) == "Internal - Admin SPP")
                {
                    #region Notifikasi_ke_Delegator
                    List<SqlParameter> paramDel = new List<SqlParameter>();
                    paramDel.Add(new SqlParameter("@IDPengaduan", _ID));
                    DataTable dt_Email_Delegator = mssql.GetDataTable("sp_Get_Email_Delegator", paramDel);
                    foreach (DataRow dr in dt_Email_Delegator.Rows)
                    {
                        string EmailDelegator = aes.Dec(dr["Email"].ToString());
                        string Fullname = aes.Dec(dr["Fullname"].ToString());
                        EmailSent = Helper.SendMail(EmailDelegator, "SPP PTSMI - Respon Tanggapan Pengaduan", MailComposer.compose_mail_body_tanggapan_ke_Delegator(ID_Pengaduan, Fullname, Nomor));
                    }
                    #endregion Notifikasi_ke_Delegator  
                }


                return Json(new { Error = false, Message = EmailSent });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult ReadTanggapanInternal(string ID_Pengaduan, string TipePengirim)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                string _ID = StringCipher.Decrypt(sani.Sanitize(ID_Pengaduan).Split("|")[0]);
                mssql.ExecuteNonQuery("UPDATE tblT_Tanggapan SET IsRead = 1, ReadOn = GETDATE() WHERE IDPengaduan = '" + _ID + "' AND TipePengirim = '" + sani.Sanitize(TipePengirim) + "' AND IsRead = 0");
                return Json(new { Error = false, Message = 1 });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Get_Msg_Tanggapan_Internal_by_Email()
        {
            int RowCount = 0;
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                DataRow dr_Login = mssql.GetDataRow("SELECT COUNT(*) [Jumlah] FROM tblT_User_Login WHERE UserID = '" + StringCipher.Decrypt(HttpContext.Session.GetString("UserID")) + "' AND ID = '" + StringCipher.Decrypt(HttpContext.Session.GetString("SessionID")) + "' AND isActive = 1");
                if (int.Parse(dr_Login["Jumlah"].ToString()) == 0)
                    return RedirectToAction("Signout", "Account");

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@Email", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));
                DataTable dt = mssql.GetDataTable("sp_Get_Msg_Tanggapan_Internal_by_Email", param);
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
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@Tipe", "Jenis Pelanggaran"));
                param.Add(new SqlParameter("@Lang", "ID"));
                DataTable dt = mssql.GetDataTable("sp_Get_Single_CMS", param);
                List<Dropdown> MainData = new List<Dropdown>();
                MainData = mssql.ConvertDataTable<Dropdown>(dt);
                return Json(new { Error = false, Message = MainData });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

    }
}
