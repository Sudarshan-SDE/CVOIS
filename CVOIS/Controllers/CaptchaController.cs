using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using CVOIS.Services;


namespace CVOIS.Controllers
{
    public class CaptchaController : Controller
    {
        private readonly CaptchaService _captchaService;

        public CaptchaController(CaptchaService captchaService)
        {
            this._captchaService = captchaService;
        }

        [HttpGet]
        public IActionResult GenerateCaptcha()
        {
            string captchaCode;
            byte[] captchaImage = _captchaService.GenerateCaptcha(out captchaCode);

            // Store CAPTCHA in session
            HttpContext.Session.SetString("CaptchaCode", captchaCode);

            return File(captchaImage, "image/png");
        }

        public bool ValidateCaptcha(string userInput)
        {
            string sessionCaptcha = HttpContext.Session.GetString("CaptchaCode");
            return sessionCaptcha != null && sessionCaptcha.Equals(userInput, StringComparison.OrdinalIgnoreCase);
        }


    }
}
