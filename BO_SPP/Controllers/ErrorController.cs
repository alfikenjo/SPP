using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BO_SPP.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index(int id = 500)
        {
            ViewData["CurrentControllerName"] = "Manajemen Delegator";
            ViewData["CurrentActionName"] = "Daftar Delegator";
            ViewData["Title"] = "Daftar Delegator";

            if (id == 400)
            {
                ViewBag.TitleError = "Bad Request";
            }
            else if (id == 401)
            {
                ViewBag.TitleError = "Unauthorized";
            }
            else if (id == 403)
            {
                ViewBag.TitleError = "Forbidden";
            }
            else if (id == 404)
            {
                ViewBag.TitleError = "Not Found";
            }
            else if (id == 503)
            {
                ViewBag.TitleError = "Service Unavailable";
            }
            else
            {
                ViewBag.TitleError = "Internal Server Error";
            }

            ViewBag.StatusCode = id;
            return View();
        }
    }
}
