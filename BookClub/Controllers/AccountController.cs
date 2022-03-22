using BookClub.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Security.Claims;


namespace BookClub.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _config;
        public List<UserModel> users = null;

        public AccountController(IConfiguration config)
        {
            _config = config;
            users = GetConnection().UserList();
        }

        private DbContext GetConnection()
        {
            DbContext dbContext = new DbContext(new SqlConnection(_config.GetConnectionString("DefaultConnection")));
            return dbContext;
        }

        public IActionResult ErrorRegistration()
        {
            return View();
        }

        public IActionResult UserRegistration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserRegistration(UserModel userModel)
        {
            if (ModelState.IsValid)
            {


                bool value = false;
                foreach (var item in users)
                {
                    if (item.UserName == userModel.UserName)
                    {
                        value = true;
                    }
                }
                if (value == false)
                {
                    GetConnection().AddUser(userModel);
                    return RedirectToAction("Login");
                }
                else
                {
                    return RedirectToAction("ErrorRegistration");
                }
            }
            return View();
        }

        public IActionResult Login(string returnUrl = "/")
        {
            LoginModel loginModel = new LoginModel();
            loginModel.ReturnUrl = returnUrl;
            return View(loginModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var user = users.Where(u => u.UserName == loginModel.UserName && u.Password == loginModel.Password).FirstOrDefault();
            if (user != null)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.UserId)),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("DotNetMania", "Code")
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                    new AuthenticationProperties()
                    {
                        IsPersistent = loginModel.RememberLogin
                    });
                return LocalRedirect(loginModel.ReturnUrl);
            }
            else
            {
                ViewBag.Massage = "Invalid Credential";
                return View(loginModel);
            }
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/");
        }
    }
}
