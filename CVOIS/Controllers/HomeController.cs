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
            _context = context;
            _captchaService = captchaService;
            _user = user;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            TempData.Peek("ErrorMessage");
            return View();
        }

        [HttpPost]
        public IActionResult UserLogin(UserLoginModel Model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["IndexError"] = "Unexpected Error";
                    return Redirect("Index");
                }
                else
                {
                    // Retrieve the CAPTCHA value from session
                    string sessionCaptcha = HttpContext.Session.GetString("CaptchaCode");

                    if (string.IsNullOrEmpty(sessionCaptcha) || !sessionCaptcha.Equals(Model.Captchacode, StringComparison.OrdinalIgnoreCase))
                    {
                        TempData["ErrorMessage"] = "Invalid Captcha!";
                        TempData.Keep("ErrorMessage");
                        return Redirect("Index");
                    }
                    else
                    {
                        var _Userid = _context.loginModel.Where(m => m.UserName == Model.Username && m.User_Status == "ACTIVE" && m.ISLOCKEDCNT < 3).FirstOrDefault();
                        if (_Userid != null)
                        {
                            var pass = _Userid.Password;
                            var Role = _Userid.Role;
                            var UserId= _Userid.Id;

                            if (pass == Model.Password)
                            {
                                HttpContext.Session.SetString("Username", _Userid.UserName);
                                HttpContext.Session.SetInt32("UserId", _Userid.Id);
                                HttpContext.Session.SetString("Role", _Userid.Role);
                                HttpContext.Session.SetString("Fullname", _Userid.Fullname);

                                string username = HttpContext.Session.GetString("Username");
                                int? userId = HttpContext.Session.GetInt32("UserId");

                                switch (_Userid.Role.ToUpper())
                                {
                                    case "ADMIN":
                                        return Redirect("~/Admin/AdminDashboard");
                                    case "VIEWER":
                                        return Redirect("~/Viewer/ViewerDashboard");
                                    case "SUPERADMIN":
                                        return Redirect("~/SuperAdmin/SuperAdminDashboard");
                                    case "USER":
                                        return RedirectToAction("UserDashboard");
                                    default:
                                        return RedirectToAction("AccessDenied");
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
                                    return Redirect("Index");
                                }
                                catch (Exception ex)
                                {
                                    TempData["ErrorMessage"] = "Invalid Password";
                                    return Redirect("Index");
                                }
                            }
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Invalid User Name";
                            return Redirect("Index");
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
        public IActionResult ResetPassword(string id)
        {
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

            bool isOldPasswordValid = _user.CheckOldPassword(userid, model.OldPassword);
            if (!isOldPasswordValid)
            {
                return BadRequest("Old password is incorrect.");
            }

            bool isUpdated = _user.UpdateUserPassword(userid, model.NewPassword);
            if (isUpdated)
            {
                return Ok(new { message = "Password updated successfully." });
            }

            return StatusCode(500, "An unexpected error occurred.");
        }





        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }


        [Route("/StatusCodeError/{StatusCode}")]
        public ActionResult Error(int StatusCode)
        {
            Response.StatusCode = StatusCode;
            return View();
        }
    }
}
