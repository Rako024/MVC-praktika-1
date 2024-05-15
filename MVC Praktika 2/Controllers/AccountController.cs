using Core.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Praktika_2.DTOs.AccountDto;

namespace MVC_Praktika_2.Controllers
{
    public class AccountController : Controller
    {
        UserManager<AppUser> _userManager;
        SignInManager<AppUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = new AppUser() 
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                Surname = registerDto.Surname,
                UserName = registerDto.UserName,
            };
            var result = await _userManager.CreateAsync(user,registerDto.Password);
            if(!result.Succeeded) 
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("",error.ToString());
                }
                return View();
            }
            await _userManager.AddToRoleAsync(user, "Member");
            return RedirectToAction(nameof(Login));
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if(user == null)
            {
                ModelState.AddModelError("", "Username or Password is not valid");
                return View();
            }
           var result = await _signInManager.PasswordSignInAsync(user,loginDto.Password, loginDto.RememberMe,false);

            if(!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or Password is not valid");
                return View();
            }
            return RedirectToAction("Index","Home");
        }
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
        //public async Task<IActionResult> CreateRole() 
        //{
        //    IdentityRole role1 = new IdentityRole("Admin");
        //    IdentityRole role2 = new IdentityRole("Member");
        //    await _roleManager.CreateAsync(role1);
        //    await _roleManager.CreateAsync(role2);
        //    return Ok("Created Roles");
        //}

        //public async Task<IActionResult> CreateAdmin()
        //{
        //    AppUser user = new AppUser()
        //    {
        //        Name = "Rashid",
        //        Surname = "Babazada",
        //        UserName = "Admin",
        //        Email = "resid@gmail.com"
        //    };
        //    var result = await _userManager.CreateAsync(user,"Admin123@");
        //    await _userManager.AddToRoleAsync(user, "Admin");
        //    return Ok(user);
        //}
    }
}
