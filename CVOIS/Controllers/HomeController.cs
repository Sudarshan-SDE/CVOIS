using CVOIS.Models;
using CVOIS.Models.Home;
using CVOIS.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using CVOIS.Interfaces;
using CVOIS.Interfaces.ISuperAdmin;
using CVOIS.Models.SuperAdmin;
using CVOIS.DataAccessLayer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;


namespace CVOIS.Controllers
{

    public class HomeController : Controller
    {
        private readonly cvois_context _context;
        private readonly CaptchaService _captchaService;
        private readonly IUser _user;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(cvois_context context, CaptchaService captchaService, IUser user, IHttpContextAccessor httpContextAccessor)
        {
            this._context = context;
            this._captchaService = captchaService;
            this._user = user;
            this._httpContextAccessor = httpContextAccessor;
        }

        [Route("Login")]
        public IActionResult Login()
        {
            TempData.Peek("ErrorMessage");
            return View();
        }
        public IActionResult UserLogin(UserLoginModel Model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["IndexError"] = "Unexpected Error";
                    return Redirect("Login");
                }
                else
                {
                    // Retrieve the CAPTCHA value from session
                    string sessionCaptcha = _httpContextAccessor.HttpContext.Session.GetString("CaptchaCode");

                    if (string.IsNullOrEmpty(sessionCaptcha) || !sessionCaptcha.Equals(Model.Captchacode, StringComparison.OrdinalIgnoreCase))
                    {
                        TempData["ErrorMessage"] = "Invalid Captcha!";
                        TempData.Keep("ErrorMessage");
                        return Redirect("Login");
                    }
                    else
                    {
                        var _Userid = _context.loginModel.Where(m => m.UserName == Model.Username && m.User_Status == "ACTIVE" && m.ISLOCKEDCNT < 3).FirstOrDefault();
                        if (_Userid != null)
                        {
                            var pass = _Userid.Password;
                            var Role = _Userid.Role;
                            var UserId = _Userid.Id;

                            if (pass == Model.Password)
                            {
                                _httpContextAccessor.HttpContext.Session.SetString("Username", _Userid.UserName);
                                _httpContextAccessor.HttpContext.Session.SetInt32("UserId", _Userid.Id);
                                _httpContextAccessor.HttpContext.Session.SetString("Role", _Userid.Role);
                                _httpContextAccessor.HttpContext.Session.SetString("Fullname", _Userid.Fullname);

                                string username = HttpContext.Session.GetString("Username");
                                int? userId = HttpContext.Session.GetInt32("UserId");

                                switch (_Userid.Role.ToUpper())
                                {
                                    case "ADMIN": return Redirect("~/Admin/AdminDashboard");
                                    case "VIEWER": return Redirect("~/Viewer/ViewerDashboard");
                                    case "SUPERADMIN": return Redirect("~/SuperAdmin/SuperAdminDashboard");
                                    case "USER": return RedirectToAction("UserDashboard");
                                    default: return RedirectToAction("AccessDenied");
                                }
                            }
                            else
                            {
                                try
                                {
                                    var entity = new LoginModel { Id = _Userid.Id }; // Only primary key is needed
                                    int iscount = _Userid.ISLOCKEDCNT + 1;
                                    _context.loginModel.Where(e => e.Id == entity.Id)
                                        .ExecuteUpdate(setters => setters.SetProperty(e => e.ISLOCKEDCNT, iscount));

                                    TempData["ErrorMessage"] = "Invalid Password";
                                    return Redirect("Login");
                                }
                                catch (Exception ex)
                                {
                                    TempData["ErrorMessage"] = "Invalid Password";
                                    return Redirect("Login");
                                }
                            }
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Invalid User Name";
                            return Redirect("Login");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }


        [HttpGet]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
            }

            return View(model);
        }


        [HttpGet]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(string id)
        {
            ViewBag.title = "ResetPassword";
            string Username = HttpContext.Session.GetString("Username");
            string Fullname = HttpContext.Session.GetString("Fullname");

            TempData["Userid"] = id;
            TempData.Keep("Userid");
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordModel model)
        {
            ViewBag.title = "ResetPassword";
            if (TempData.Peek("Userid") == null)
            {
                return BadRequest("Session expired. Please try again.");
            }
            string userid = TempData.Peek("Userid").ToString();
            model.SessionID = HttpContext.Session.Id;
            model.UserId = userid;


            if (string.IsNullOrWhiteSpace(model.OldPassword) || string.IsNullOrWhiteSpace(model.NewPassword))
            {
                return BadRequest("Please fill all the fields.");
            }

            bool isOldPasswordValid = _user.CheckOldPassword(userid, model.OldPassword);
            if (!isOldPasswordValid)
            {
                return BadRequest("Old password is incorrect.");
            }

            bool isUpdated = _user.UpdateUserPassword(userid, model.NewPassword);
            if (isUpdated)
            {
                TempData.Remove("Userid");
                return Ok(new { message = "Password updated successfully." });
            }

            return StatusCode(500, "An unexpected error occurred while updating the password.");
        }





        public IActionResult Logout()
        {
            HttpContext.SignOutAsync("MyCookieAuth");
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Home");
        }


        [Route("/StatusCodeError/{StatusCode}")]
        public ActionResult Error(int StatusCode)
        {
            Response.StatusCode = StatusCode;
            return View();
        }
    }
}
