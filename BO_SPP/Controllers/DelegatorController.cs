using BO_SPP.Common;
using BO_SPP.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Ganss.XSS;

namespace BO_SPP.Controllers
{
    public class DelegatorController : Controller
    {
        private HtmlSanitizer sani = new HtmlSanitizer();

        public IActionResult DaftarDelegator()
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                return RedirectToAction("Index", "Dashboard");

            ViewData["CurrentControllerName"] = "Manajemen Delegator";
            ViewData["CurrentActionName"] = "Daftar Delegator";
            ViewData["Title"] = "Daftar Delegator";
            return View();
        }

        public IActionResult MemberDelegator()
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                return RedirectToAction("Index", "Dashboard");

            string DelegatorID = HttpContext.Session.GetString("DelegatorID");
            string DelegatorName = string.Empty;
            DataTable dt = mssql.GetDataTable("SELECT Name FROM tblM_Delegator WHERE ID = '" + DelegatorID + "'");
            if (dt.Rows.Count == 1)
                DelegatorName = dt.Rows[0]["Name"].ToString();

            ViewBag.NamaDelegator = DelegatorName;

            ViewData["CurrentControllerName"] = "Manajemen Delegator";
            ViewData["CurrentActionName"] = "Member Delegator";
            ViewData["Title"] = "Member Delegator";
            return View();
        }

        public IActionResult AddMemberDelegator()
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                return RedirectToAction("Index", "Dashboard");

            string DelegatorID = HttpContext.Session.GetString("DelegatorID");
            string DelegatorName = string.Empty;
            DataTable dt = mssql.GetDataTable("SELECT Name FROM tblM_Delegator WHERE ID = '" + DelegatorID + "'");
            if (dt.Rows.Count == 1)
                DelegatorName = dt.Rows[0]["Name"].ToString();

            ViewBag.NamaDelegator = DelegatorName;

            ViewData["CurrentControllerName"] = "Manajemen Delegator";
            ViewData["CurrentActionName"] = "Add Member Delegator";
            ViewData["Title"] = "Add Member Delegator";
            return View();
        }

        [HttpPost]
        public IActionResult Get_Delegator()
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='/'");

                DataTable dt = mssql.GetDataTable("sp_Get_Delegator");
                List<tblM_Delegator> MainData = new List<tblM_Delegator>();
                MainData = mssql.ConvertDataTable<tblM_Delegator>(dt);
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
        public IActionResult Get_ddl_Delegator()
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='/'");

                DataTable dt = mssql.GetDataTable("sp_Get_Delegator");
                List<tblM_Delegator> MainData = new List<tblM_Delegator>();
                MainData = mssql.ConvertDataTable<tblM_Delegator>(dt);
                return Json(new { Error = false, Message = MainData });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Get_DelegatorByID(string ID)
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
                DataTable dt = mssql.GetDataTable("sp_Get_DelegatorByID", param);
                List<tblM_Delegator> MainData = new List<tblM_Delegator>();
                MainData = mssql.ConvertDataTable<tblM_Delegator>(dt);
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
        public ActionResult SaveDelegator(tblM_Delegator Model)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='/'");

                string Action = Model.Action;
                string SanitizedID = sani.Sanitize(Model.ID);
                string ID = Guid.NewGuid().ToString();
                if (Action != "add")
                {
                    ID = StringCipher.Decrypt(SanitizedID.Split("|")[0]);
                    if (SanitizedID.Split("|")[1] != HttpContext.Session.GetString("SessionID"))
                        throw new Exception("Invalid Authorization|window.location='/'");
                }

                if (Action == "hapus")
                {
                    DataRow drCountMember = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM tblT_UserInDelegator WHERE DelegatorID = '" + ID + "'");
                    if (int.Parse(drCountMember["Count"].ToString()) > 0)
                        throw new Exception("Maaf Grup Delegator ini tidak dapat dihapus karena masih memiliki [" + int.Parse(drCountMember["Count"].ToString()) + "] Anggota");

                    DataRow drCountPengaduan = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM tblT_Dumas WHERE DelegatorID = '" + ID + "'");
                    if (int.Parse(drCountPengaduan["Count"].ToString()) > 0)
                        throw new Exception("Maaf Grup Delegator ini tidak dapat dihapus karena sudah pernah menangani pengaduan");
                }

                if (Action == "add")
                {
                    DataRow drIsExist = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM tblM_Delegator WHERE Name = '" + sani.Sanitize(Model.Name) + "'");
                    if (int.Parse(drIsExist["Count"].ToString()) > 0)
                        throw new Exception("Proses Ditolak, Grup Delegator sudah terdaftar sebelumnya");
                }
                else if (Action == "edit")
                {
                    DataRow drIsExist = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM tblM_Delegator WHERE Name = '" + sani.Sanitize(Model.Name) + "' AND ID <> '" + ID + "'");
                    if (int.Parse(drIsExist["Count"].ToString()) > 0)
                        throw new Exception("Proses Ditolak, Grup Delegator sudah terdaftar sebelumnya");
                }

                #region Save

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@Action", sani.Sanitize(Action)));
                param.Add(new SqlParameter("@ID", ID));
                param.Add(new SqlParameter("@Name", sani.Sanitize(Model.Name)));
                param.Add(new SqlParameter("@Description", sani.Sanitize(Model.Description)));
                param.Add(new SqlParameter("@isActive", Model.isActive));
                param.Add(new SqlParameter("@CreatedBy", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));

                int RowsAffected = mssql.ExecuteNonQuery("sp_Save_Delegator", param);
                #endregion Save

                return Json(new { Error = false, Message = RowsAffected });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Get_MemberDelegator(string DelegatorID)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='/'");

                if (string.IsNullOrEmpty(DelegatorID))
                    throw new Exception("Invalid request|window.location='../Delegator/DaftarDelegator'");

                DelegatorID = sani.Sanitize(DelegatorID);
                string _ID = StringCipher.Decrypt(DelegatorID.Split("|")[0]);
                string SessionIDDesc = DelegatorID.Split("|")[1];

                if (SessionIDDesc != HttpContext.Session.GetString("SessionID"))
                    throw new Exception("Invalid Authorization|window.location='/'");

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@DelegatorID", _ID));
                DataTable dt = mssql.GetDataTable("sp_Get_MemberDelegator", param);
                List<tblT_UserInDelegator> MainData = new List<tblT_UserInDelegator>();
                MainData = mssql.ConvertDataTable<tblT_UserInDelegator>(dt);

                for (int i = 0; i < MainData.Count; i++)
                {
                    MainData[i].ID = StringCipher.Encrypt(MainData[i].ID) + "|" + HttpContext.Session.GetString("SessionID");
                    MainData[i].UserID = StringCipher.Encrypt(MainData[i].UserID) + "|" + HttpContext.Session.GetString("SessionID");

                    if (!string.IsNullOrEmpty(MainData[i].Img))
                        MainData[i].Img = Helper.GetBinaryImage(MainData[i].Img, "png");
                }

                return Json(new { Error = false, Message = MainData });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult SaveMemberDelegator(tblT_UserInDelegator Model)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='/'");

                string Action = sani.Sanitize(Model.Action);
                string ActionID = sani.Sanitize(Model.ID);
                string UserIDOk = StringCipher.Decrypt(ActionID.Split("|")[0]);


                Model.UserID = sani.Sanitize(Model.UserID);
                Model.DelegatorID = sani.Sanitize(Model.DelegatorID);

                string UserID = Guid.NewGuid().ToString();
                string DelegatorID = Guid.NewGuid().ToString();

                string ID = Guid.NewGuid().ToString();
                if (Action == "add")
                {
                    UserID = StringCipher.Decrypt(Model.UserID.Split("|")[0]);
                    string SessionIDDesc1 = Model.UserID.Split("|")[1];
                    if (SessionIDDesc1 != HttpContext.Session.GetString("SessionID"))
                        throw new Exception("Invalid Authorization|window.location='/'");

                    DataRow drisExistAdminSPP = mssql.GetDataRow("SELECT COUNT(*) [Count] FROM vw_UserInRole WHERE UserID = '" + UserID + "' AND [Role] = 'Admin SPP'");
                    if (int.Parse(drisExistAdminSPP["Count"].ToString()) > 0)
                        throw new Exception("Maaf, Sistem mendeteksi akun ini sudah terdaftar sebagai Admin SPP, Admin SPP tidak dapat merangkap fungsi sebagai Anggota Delegator");

                    DelegatorID = StringCipher.Decrypt(Model.DelegatorID.Split("|")[0]);
                    string SessionIDDesc2 = Model.DelegatorID.Split("|")[1];
                    if (SessionIDDesc2 != HttpContext.Session.GetString("SessionID"))
                        throw new Exception("Invalid Authorization|window.location='/'");
                }
                else if (Action == "hapus")
                {
                    DataRow drCountMember = mssql.GetDataRow("select COUNT(*) [Count] from  tblT_UserInDelegator a inner join tblT_Dumas b on a.DelegatorID = b.DelegatorID where a.DelegatorID = (SELECT DelegatorID FROM tblT_UserInDelegator WHERE  ID = '" + UserIDOk + "') and b.Status not in ('Selesai')");
                    if (int.Parse(drCountMember["Count"].ToString()) > 1)
                        throw new Exception("Maaf User Delegator tidak boleh kosong masih memiliki [" + int.Parse(drCountMember["Count"].ToString()) + "] Aduan");

                    string SanitizedID = sani.Sanitize(Model.ID);
                    ID = StringCipher.Decrypt(SanitizedID.Split("|")[0]);
                    if (SanitizedID.Split("|")[1] != HttpContext.Session.GetString("SessionID"))
                        throw new Exception("Invalid Authorization|window.location='/'");
                }

                #region Save

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@Action", sani.Sanitize(Action)));
                param.Add(new SqlParameter("@ID", ID));
                param.Add(new SqlParameter("@UserID", UserID));
                param.Add(new SqlParameter("@DelegatorID", DelegatorID));
                param.Add(new SqlParameter("@CreatedBy", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));

                int RowsAffected = mssql.ExecuteNonQuery("sp_Save_MemberDelegator", param);

                DataTable dtResult = mssql.GetDataTable("SELECT B.Name [DelegatorName] FROM tblT_UserInDelegator A JOIN tblM_Delegator B ON A.DelegatorID = B.ID WHERE A.UserID = '" + UserID + "' AND A.DelegatorID = '" + DelegatorID + "'");
                if (Action == "add" && dtResult.Rows.Count == 1)
                {
                    #region Notifikasi_ke_Member_Baru
                    try
                    {
                        string Subject = "SPP - PTSMI [Join Grup Delegator]";
                        DataTable dt_Email = mssql.GetDataTable("SELECT Email, Fullname FROM tblM_User WHERE UserID = '" + UserID + "'");
                        foreach (DataRow dr in dt_Email.Rows)
                        {
                            string Email = dr["Email"].ToString();
                            string Fullname = dr["Fullname"].ToString();
                            string DelegatorName = dtResult.Rows[0]["DelegatorName"].ToString();
                            RowsAffected = Helper.SendMail(Email, Subject, MailComposer.compose_mail_body_member_delegator_baru(Email, Fullname, DelegatorName));
                        }
                    }
                    catch (Exception) { RowsAffected = 0; }
                    #endregion Notifikasi_ke_Member_Baru
                }

                #endregion Save

                return Json(new { Error = false, Message = RowsAffected });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Get_CalonMemberDelegator(string DelegatorID)
        {
            try
            {
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                if (!Helper.AuthorizedByUsername(HttpContext.Session.GetString("SessionID"), HttpContext.Session.GetString("UserID"), controllerName, actionName, null))
                    throw new Exception("Invalid Authorization|window.location='/'");

                if (string.IsNullOrEmpty(DelegatorID))
                    throw new Exception("Invalid request|window.location='../Delegator/DaftarDelegator'");

                DelegatorID = sani.Sanitize(DelegatorID);
                string _ID = StringCipher.Decrypt(DelegatorID.Split("|")[0]);
                string SessionIDDesc = DelegatorID.Split("|")[1];

                if (SessionIDDesc != HttpContext.Session.GetString("SessionID"))
                    throw new Exception("Invalid Authorization|window.location='/'");

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@DelegatorID", _ID));
                DataTable dt = mssql.GetDataTable("sp_Get_CalonMemberDelegator", param);
                List<tblT_UserInDelegator> MainData = new List<tblT_UserInDelegator>();
                MainData = mssql.ConvertDataTable<tblT_UserInDelegator>(dt);

                for (int i = 0; i < MainData.Count; i++)
                {
                    MainData[i].UserID = StringCipher.Encrypt(MainData[i].UserID) + "|" + HttpContext.Session.GetString("SessionID");
                    if (!string.IsNullOrEmpty(MainData[i].Img))
                        MainData[i].Img = Helper.GetBinaryImage(MainData[i].Img, "png");
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
