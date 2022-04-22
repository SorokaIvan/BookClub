using BookClub.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace BookClub.Controllers
{
    public class AccountController : Controller
    {
        private readonly UsersRepository _usersRepository;

        public AccountController(UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
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
                var allUsers = _usersRepository.UserList();
                AccountsRepository accountsRepository = new AccountsRepository();
                var value = accountsRepository.GetUserByLogin(allUsers, userModel);

                if (value == false)
                {
                    _usersRepository.AddUser(userModel);
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.Message = "Пользователь с таким логином уже существует!";
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
            var allUsers = _usersRepository.UserList();
            var user = allUsers.Where(u => u.UserLogin == loginModel.UserLogin && u.Password == loginModel.Password).FirstOrDefault();
            if (user != null)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.UserId)),
                    new Claim(ClaimTypes.Name, user.UserLogin),
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
                ViewBag.Message = "Неверный логин или пароль!";
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
