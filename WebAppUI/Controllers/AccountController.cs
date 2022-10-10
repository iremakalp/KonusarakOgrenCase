using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAppUI.Identity;
using WebAppUI.Models;

namespace WebAppUI.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationIdentityDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationIdentityDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        public IActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Bu email ile daha önce hesap oluşturulmamış.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

            if (result.Succeeded)
            {
            var userRole=GetRole(model);
                if(userRole=="Admin")
                {
                   return RedirectToAction("Admin","Home");
                }
                else if (userRole=="SysAdmin")
                {
                    return RedirectToAction("SysAdmin", "Home");
                }
                return RedirectToAction("Customer", "Home");
            }

            ModelState.AddModelError("", "Email veya parola yanlış");
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Accessdenied()
        {
            return View();
        }

        private string GetRole(LoginModel loginModel)
        {
          
            var result = from ur in _context.UserRoles
                         join r in _context.Roles
                         on ur.RoleId equals r.Id
                         join u in _context.Users
                         on ur.UserId equals u.Id
                         select new UserRoleModel
                         {
                             UserName= u.UserName,
                             RoleName=r.Name
                         }.RoleName;
            
            return result.ToString();
        }
    }
}
