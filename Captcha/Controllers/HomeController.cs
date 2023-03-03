using Captcha.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Captcha.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        //[AllowAnonymous]
        public IActionResult GetAuthCode()
        {
            string code = "";
            MemoryStream ms = new CaptchaHelper().Create(out code);
            string captcha = code;

            HttpContext.Session.SetString("cap", captcha);
            Response.Body.Dispose();
            return File(ms.ToArray(), @"image/png");
            //return File(new VerifyCode().GetVerifyCode(), @"image/Gif");
        }

    }
}
