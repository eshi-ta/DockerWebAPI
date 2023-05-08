using BookReadingEvent.Data;
using BookReadingEvent.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;



namespace BookReadingEvent.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private ApplicationDbcontext _db;



        public HomeController(ILogger<HomeController> logger, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, ApplicationDbcontext db)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _db = db;
        }



        public IActionResult Index()
        {
            List<Event> eventList = _db.Events.ToList();
            return View(eventList);
        }



        public IActionResult Privacy()
        {
            return View();
        }



        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(User obj)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(obj.Email, obj.Password, false, false);
                if (result.Succeeded)
                {
                    if (User.IsInRole("Admin"))
                    {
                        return RedirectToAction("ListAllEvents", "Event");
                    }
                    return RedirectToAction("MyInvitations", "Event");
                }
                ModelState.AddModelError("", "Invalid Credentials");
            }
            return View(obj);
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(User obj)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser() { UserName = obj.Email, Email = obj.Email };
                var result = await _userManager.CreateAsync(user, obj.Password); //bool result, identity result
                //if user created successfully
                if (result.Succeeded)
                {
                    //await _signInManager.CreateAsync(user, isPersistent: false);//session key using
                    //TempData["Success"] = "Registration Successfull";
                    return RedirectToAction("Login");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }






            }
            return View(obj);
        }



        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
