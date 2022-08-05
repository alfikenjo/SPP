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

namespace Frontend_SPP.Controllers
{
    public class KuesionerController : Controller
    {
        private HtmlSanitizer sani = new HtmlSanitizer();

        [HttpPost]
        public IActionResult Get_Aktif_Kuesioner()
        {
            try
            {
                if (string.IsNullOrEmpty(StringCipher.Decrypt(HttpContext.Session.GetString("Email"))))
                    return RedirectToAction("Index", "Home");

                DataTable dt = mssql.GetDataTable("sp_Get_Aktif_Kuesioner");
                List<KuesionerDetail> MainData = new List<KuesionerDetail>();
                MainData = mssql.ConvertDataTable<KuesionerDetail>(dt);
                for (int i = 0; i < MainData.Count; i++)
                {
                    MainData[i].IDKuesioner = StringCipher.Encrypt(MainData[i].IDKuesioner) + "|" + HttpContext.Session.GetString("SessionID");
                    MainData[i].ID = StringCipher.Encrypt(MainData[i].ID) + "|" + HttpContext.Session.GetString("SessionID");
                    MainData[i].IDHeader = StringCipher.Encrypt(MainData[i].IDHeader) + "|" + HttpContext.Session.GetString("SessionID");
                }
                return Json(new { Error = false, Message = MainData });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Get_Kuesioner_Detail_By_IDHeader(string IDHeader)
        {
            try
            {
                if (string.IsNullOrEmpty(StringCipher.Decrypt(HttpContext.Session.GetString("Email"))))
                    return RedirectToAction("Index", "Home");

                IDHeader = sani.Sanitize(IDHeader);

                string _ID = StringCipher.Decrypt(IDHeader.Split("|")[0]);
                string SessionIDDesc = IDHeader.Split("|")[1];

                if (SessionIDDesc != HttpContext.Session.GetString("SessionID"))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@IDHeader", _ID));
                DataTable dt = mssql.GetDataTable("sp_Get_Kuesioner_Detail_By_IDHeader", param);
                List<KuesionerDetail> MainData = new List<KuesionerDetail>();
                MainData = mssql.ConvertDataTable<KuesionerDetail>(dt);
                for (int i = 0; i < MainData.Count; i++)
                {
                    MainData[i].ID = StringCipher.Encrypt(MainData[i].ID) + "|" + HttpContext.Session.GetString("SessionID");
                    MainData[i].IDHeader = StringCipher.Encrypt(MainData[i].IDHeader) + "|" + HttpContext.Session.GetString("SessionID");
                }
                return Json(new { Error = false, Message = MainData });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult Save_KuesionerValue(KuesionerValue Model)
        {
            try
            {
                if (string.IsNullOrEmpty(StringCipher.Decrypt(HttpContext.Session.GetString("Email"))))
                    return RedirectToAction("Index", "Home");

                string SanitizedID = sani.Sanitize(Model.IDPengaduan);
                string ID = Guid.NewGuid().ToString();
                string IDPengaduan = StringCipher.Decrypt(SanitizedID.Split("|")[0]);
                if (SanitizedID.Split("|")[1] != HttpContext.Session.GetString("SessionID"))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                #region Save

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@ID", ID));
                param.Add(new SqlParameter("@IDPengaduan", IDPengaduan));
                param.Add(new SqlParameter("@Title", sani.Sanitize(Model.Title)));
                param.Add(new SqlParameter("@Num", Model.Num));
                param.Add(new SqlParameter("@InputType", sani.Sanitize(Model.InputType)));
                param.Add(new SqlParameter("@Label", sani.Sanitize(Model.Label)));
                param.Add(new SqlParameter("@Required", Model.Required));
                param.Add(new SqlParameter("@Options", sani.Sanitize(Model.Options)));
                param.Add(new SqlParameter("@InputValue", sani.Sanitize(Model.InputValue)));
                param.Add(new SqlParameter("@CreatedBy", StringCipher.Decrypt(HttpContext.Session.GetString("Email"))));

                mssql.ExecuteNonQuery("sp_Save_KuesionerValue", param);
                #endregion Save

                return Json(new { Error = false, Message = "" });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Get_KuesionerValue_By_IDPengaduan(string IDPengaduan)
        {
            try
            {
                if (string.IsNullOrEmpty(StringCipher.Decrypt(HttpContext.Session.GetString("Email"))))
                    return RedirectToAction("Index", "Home");

                IDPengaduan = sani.Sanitize(IDPengaduan);

                string _ID = StringCipher.Decrypt(IDPengaduan.Split("|")[0]);
                string SessionIDDesc = IDPengaduan.Split("|")[1];

                if (SessionIDDesc != HttpContext.Session.GetString("SessionID"))
                    throw new Exception("Invalid Authorization|window.location='../Account/Signin'");

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@IDPengaduan", _ID));
                DataTable dt = mssql.GetDataTable("sp_Get_KuesionerValue_By_IDPengaduan", param);
                List<KuesionerValue> MainData = new List<KuesionerValue>();
                MainData = mssql.ConvertDataTable<KuesionerValue>(dt);
                for (int i = 0; i < MainData.Count; i++)
                {
                    MainData[i].ID = StringCipher.Encrypt(MainData[i].ID) + "|" + HttpContext.Session.GetString("SessionID");
                    MainData[i].IDPengaduan = StringCipher.Encrypt(MainData[i].IDPengaduan) + "|" + HttpContext.Session.GetString("SessionID");
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
