using FluentFTP;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using BO_SPP.Models;
using System.DirectoryServices;
using System.Runtime.Versioning;

namespace BO_SPP.Common
{
    public class LDAP
    {
        static readonly string connStr = ConfigurationManager.AppSetting["FileConfiguration:ADConnectionString"];
        static readonly string adminUser = ConfigurationManager.AppSetting["FileConfiguration:ADUsername"];
        static readonly string adminPassword = ConfigurationManager.AppSetting["FileConfiguration:ADPassword"];

        [SupportedOSPlatform("windows")]
        public static ActiveDirectoryViewModel GetPersonalInformation(string username, string domain)
        {
            //const int ADS_UF_LOCKOUT = 0x00000010;
            ActiveDirectoryViewModel advm = new ActiveDirectoryViewModel();
            try
            {
                if (!String.IsNullOrEmpty(username))
                {
                    string dom = "";
                    DirectoryEntry entry = getAdminDirectoryEntry(domain.ToLower(), ref dom);
                    DirectorySearcher dSearch = new DirectorySearcher(entry);

                    if (username.Contains('@'))
                    {
                        dSearch.Filter = "(mail=*" + username + ")";
                    }
                    else
                    {
                        dSearch.Filter = "(samaccountname=" + username + ")";
                    }
                    SearchResult res = dSearch.FindOne();
                    if (res == null)
                    {
                        dSearch.Filter = "(name=*" + username + "*)";
                        res = dSearch.FindOne();
                    }
                    DirectoryEntry directoryEntry = res.GetDirectoryEntry();
                    advm = new ActiveDirectoryViewModel();
                    advm.Email = directoryEntry.Properties["mail"].Count <= 0 ? String.Empty : directoryEntry.Properties["mail"][0].ToString();
                    advm.AccountName = directoryEntry.Properties["samaccountname"].Count <= 0 ? String.Empty : dom + "\\" + directoryEntry.Properties["samaccountname"][0].ToString();
                    advm.Name = directoryEntry.Properties["name"].Count <= 0 ? String.Empty : directoryEntry.Properties["name"][0].ToString();
                    advm.Department = directoryEntry.Properties["department"].Count <= 0 ? String.Empty : directoryEntry.Properties["department"][0].ToString();
                    advm.Location = directoryEntry.Properties["physicalDeliveryOfficeName"].Count <= 0 ? String.Empty : directoryEntry.Properties["physicalDeliveryOfficeName"][0].ToString();
                    advm.Title = directoryEntry.Properties["title"].Count <= 0 ? String.Empty : directoryEntry.Properties["title"][0].ToString();
                    //advm.IsLocked = Convert.ToBoolean(directoryEntry.InvokeGet("IsAccountLocked"));
                }
                return advm;
            }
            catch (Exception ex)
            {
                throw new Exception("getIEGAccountInformation " + ex.Message);
            }
        }

        [SupportedOSPlatform("windows")]
        public static ActiveDirectoryViewModel GetPersonalInformation(string username)
        {
            //only when disconnected with AD/testing only
            //if (true)
            //    return new ActiveDirectoryViewModel
            //    {
            //        Email = "anonym@mail.com"
            //    };

            //const int ADS_UF_LOCKOUT = 0x00000010;
            ActiveDirectoryViewModel advm = null;
            try
            {
                if (!String.IsNullOrEmpty(username) && username.Contains("\\"))
                {
                    string dom = "";
                    string domain = username.Split('\\')[0];
                    username = username.Split('\\')[1];
                    DirectoryEntry entry = getAdminDirectoryEntry(domain.ToLower(), ref dom);
                    DirectorySearcher dSearch = new DirectorySearcher(entry);
                    dSearch.Filter = "(samaccountname=" + username + ")";
                    SearchResult res = dSearch.FindOne();
                    if (res == null)
                    {
                        dSearch.Filter = "(name=*" + username + "*)";
                        res = dSearch.FindOne();
                    }
                    if (res != null)
                    {
                        DirectoryEntry directoryEntry = res.GetDirectoryEntry();
                        //for (int i = 0; i <= directoryEntry.Properties.PropertyNames.Count; i++) {
                        //    string value = directoryEntry.Properties[]
                        //    Console.WriteLine(value);
                        //}

                        advm = new ActiveDirectoryViewModel();
                        advm.Email = directoryEntry.Properties["mail"].Count <= 0 ? String.Empty : directoryEntry.Properties["mail"][0].ToString();
                        advm.AccountName = directoryEntry.Properties["samaccountname"].Count <= 0 ? String.Empty : dom + "\\" + directoryEntry.Properties["samaccountname"][0].ToString();
                        advm.Name = directoryEntry.Properties["name"].Count <= 0 ? String.Empty : directoryEntry.Properties["name"][0].ToString();
                        advm.Department = directoryEntry.Properties["department"].Count <= 0 ? String.Empty : directoryEntry.Properties["department"][0].ToString();
                        advm.Location = directoryEntry.Properties["physicalDeliveryOfficeName"].Count <= 0 ? String.Empty : directoryEntry.Properties["physicalDeliveryOfficeName"][0].ToString();
                        advm.Title = directoryEntry.Properties["title"].Count <= 0 ? String.Empty : directoryEntry.Properties["title"][0].ToString();
                        advm.Mobile = directoryEntry.Properties["mobile"].Count <= 0 ? String.Empty : directoryEntry.Properties["mobile"][0].ToString();
                        //advm.IsLocked = Convert.ToBoolean(directoryEntry.InvokeGet("IsAccountLocked"));
                    }
                }
                return advm;
            }
            catch (Exception)
            {
                return advm;
            }
        }

        [SupportedOSPlatform("windows")]
        public static IEnumerable<ActiveDirectoryViewModel> GetListPersonalInformations(string username, string domain)
        {
            //const int ADS_UF_LOCKOUT = 0x00000010;
            List<ActiveDirectoryViewModel> ListADUser = new List<ActiveDirectoryViewModel>();
            try
            {
                if (!String.IsNullOrEmpty(username))
                {
                    string dom = "";
                    DirectoryEntry entry = getAdminDirectoryEntry(domain.ToLower(), ref dom);
                    DirectorySearcher dSearch = new DirectorySearcher(entry);

                    if (username.Contains('@'))
                    {
                        dSearch.Filter = "(mail=*" + username + ")";
                    }
                    else
                    {
                        dSearch.Filter = "(samaccountname=" + username + ")";
                    }
                    SearchResultCollection resCollection = dSearch.FindAll();
                    if (resCollection == null || resCollection.Count == 0)
                    {
                        dSearch.Filter = "(name=*" + username + "*)";
                        resCollection = dSearch.FindAll();
                    }
                    for (int i = 0; i < resCollection.Count; i++)
                    {
                        DirectoryEntry directoryEntry = resCollection[i].GetDirectoryEntry();
                        ActiveDirectoryViewModel advm = new ActiveDirectoryViewModel();
                        advm.Email = directoryEntry.Properties["mail"].Count <= 0 ? String.Empty : directoryEntry.Properties["mail"][0].ToString();
                        advm.AccountName = directoryEntry.Properties["samaccountname"].Count <= 0 ? String.Empty : dom + "\\" + directoryEntry.Properties["samaccountname"][0].ToString();
                        advm.Name = directoryEntry.Properties["name"].Count <= 0 ? String.Empty : directoryEntry.Properties["name"][0].ToString();
                        advm.Department = directoryEntry.Properties["department"].Count <= 0 ? String.Empty : directoryEntry.Properties["department"][0].ToString();
                        advm.Location = directoryEntry.Properties["physicalDeliveryOfficeName"].Count <= 0 ? String.Empty : directoryEntry.Properties["physicalDeliveryOfficeName"][0].ToString();
                        advm.Title = directoryEntry.Properties["title"].Count <= 0 ? String.Empty : directoryEntry.Properties["title"][0].ToString();
                        //advm.IsLocked = Convert.ToBoolean(directoryEntry.InvokeGet("IsAccountLocked"));
                        ListADUser.Add(advm);
                    }
                }
                return ListADUser;
            }
            catch (Exception ex)
            {
                throw new Exception("getIEGAccountInformation " + ex.Message);
            }
        }

        [SupportedOSPlatform("windows")]
        public static string VerifyAccount(string username, string password, string domain)
        {
            string Authenticated = "";
            try
            {
                //var acc = username.Split('\\');

                //string connStr = string.Empty; //ConfigurationManager.AppSettings["ADConnectionString"];
                //string adminUser = string.Empty;//ConfigurationManager.AppSettings["ADUsername"];
                //string adminPassword = string.Empty;//ConfigurationManager.AppSettings["ADPassword"];

                //using (MainEntities ent = new MainEntities())
                //{
                //    var encrypt = ConfigurationManager.AppSettings["Connection"];
                //    var strConn = AESCryptoWithBCHelper.DecryptAESBounceCastle(encrypt);
                //    ent.Database.Connection.ConnectionString = strConn.Replace("\0", string.Empty);

                //    var query = @"Select 
                //                    Description,
                //                    Value
                //                   From TblConfigure";
                //    var item = ent.Database.SqlQuery<TblConfigureViewModel>(query).ToList();

                //    connStr = item.Where(x => x.Description.Equals("ADConnectionString", StringComparison.CurrentCultureIgnoreCase)).Select(x => x.Value).FirstOrDefault().ToString();
                //    adminUser = item.Where(x => x.Description.Equals("ADUsername", StringComparison.CurrentCultureIgnoreCase)).Select(x => x.Value).FirstOrDefault().ToString();
                //    adminPassword = item.Where(x => x.Description.Equals("ADPassword", StringComparison.CurrentCultureIgnoreCase)).Select(x => x.Value).FirstOrDefault().ToString();
                //}

                DirectoryEntry directoryEntry = new DirectoryEntry(connStr, username, password, AuthenticationTypes.Secure);

                DirectorySearcher dSearch = new DirectorySearcher(directoryEntry);
                dSearch.Filter = "(samaccountname=" + username + ")";


                SearchResult result = dSearch.FindOne();
                if (null == result)
                {
                    Authenticated = "username is not found";
                }
                else
                {
                    Authenticated = "1";
                }

                return Authenticated;
            }
            catch (Exception e)
            {
                Authenticated = e.Message;
                return Authenticated;
            }
        }
        [SupportedOSPlatform("windows")]
        private static DirectoryEntry getAdminDirectoryEntry(string username, ref string domain)
        {
            try
            {
                //string connStr = string.Empty; //ConfigurationManager.AppSettings["ADConnectionString"];
                //string adminUser = string.Empty;//ConfigurationManager.AppSettings["ADUsername"];
                //string adminPassword = string.Empty;//ConfigurationManager.AppSettings["ADPassword"];

                //using (MainEntities ent = new MainEntities())
                //{
                //    var encrypt = ConfigurationManager.AppSettings["Connection"];
                //    var strConn = AESCryptoWithBCHelper.DecryptAESBounceCastle(encrypt);
                //    ent.Database.Connection.ConnectionString = strConn.Replace("\0", string.Empty);

                //    var query = @"Select 
                //                    Description,
                //                    Value
                //                   From TblConfigure";
                //    var item = ent.Database.SqlQuery<TblConfigureViewModel>(query).ToList();

                //    connStr = item.Where(x => x.Description.Equals("ADConnectionString", StringComparison.CurrentCultureIgnoreCase)).Select(x => x.Value).FirstOrDefault().ToString();
                //    adminUser = item.Where(x => x.Description.Equals("ADUsername", StringComparison.CurrentCultureIgnoreCase)).Select(x => x.Value).FirstOrDefault().ToString();
                //    adminPassword = item.Where(x => x.Description.Equals("ADPassword", StringComparison.CurrentCultureIgnoreCase)).Select(x => x.Value).FirstOrDefault().ToString();
                //}

                string domainName = username.Split('\\').First().ToLower();

                DirectoryEntry directoryEntry = new DirectoryEntry(connStr, adminUser, adminPassword, AuthenticationTypes.Secure);
                domain = domainName;
                return directoryEntry;
            }
            catch (Exception ex)
            {
                throw new Exception("getAdminDirectoryEntry " + ex.InnerException.Message);
            }
        }

    }
}
