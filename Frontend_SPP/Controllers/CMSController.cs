using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Frontend_SPP.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Frontend_SPP.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.Web;
//using Microsoft.Security.Application;
using Ganss.XSS;

namespace Frontend_SPP.Controllers
{
    public class CMSController : Controller
    {
        private HtmlSanitizer sani = new HtmlSanitizer();
    }
}
