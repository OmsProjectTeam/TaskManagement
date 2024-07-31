using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Yara.Models;

namespace Yara.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		UserManager<ApplicationUser> _userManager;

		public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager)
		{
			_logger = logger;
			_userManager= userManager;
		}

		public async Task<IActionResult> Index()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
				return View();
			// الحصول على دور المستخدم
			var role = await _userManager.GetRolesAsync(user);
			ViewBag.UserRole = role.FirstOrDefault();
		
			
			 return View(user);
		}
		public IActionResult IndexAr()
		{
			return View();
		}
		public IActionResult Privacy()
		{
			return View();
		}

		//[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		//public IActionResult Error()
		//{
		//	return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		//}

		// comment from ali
		// test from ali
	}
}
