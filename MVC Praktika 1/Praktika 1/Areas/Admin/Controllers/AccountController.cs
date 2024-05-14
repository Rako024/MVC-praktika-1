using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Praktika_1.Areas.Admin.ViewModels;

namespace Praktika_1.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class AccountController : Controller
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
		}

		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
        public async Task<IActionResult> Login(AdminLoginVM admin)
		{

			if(!ModelState.IsValid)
				return View();

			var user = await _userManager.FindByNameAsync(admin.Username);

			if(user == null)
			{
				ModelState.AddModelError("", "Username or password is not valid!");
				return View();
			}
			
			var result = await _signInManager.PasswordSignInAsync(user, admin.Password, admin.RememberMe,false);


			if (!result.Succeeded)
			{
                ModelState.AddModelError("", "Username or password is not valid!");
				return View();
            }

			return RedirectToAction("Index", "Dashboard");
		}

		public async Task<IActionResult> CreateAdmin()
		{
			User admin = new User()
			{
				Name = "Rashid",
				Surname = "Babazada",
				UserName = "Admin"
			};
			var res= await _userManager.CreateAsync(admin, "Admin123@");
			var role = await _userManager.AddToRoleAsync(admin, "SuperAdmin");
			return Ok("piuvvv 2");
		}

		public async Task<IActionResult> CreateRole()
		{
			IdentityRole identity = new IdentityRole("SuperAdmin");
			IdentityRole identity1 = new IdentityRole("Admin");
			IdentityRole identity2 = new IdentityRole("Member");
			await _roleManager.CreateAsync(identity);
			await _roleManager.CreateAsync(identity1);
			await _roleManager.CreateAsync(identity2);
			return Ok("Piuvv");
        }

    }
}
