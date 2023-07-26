using JavascriptAspNetCore6RecaptchaV3.Models;
using JavascriptAspNetCore6RecaptchaV3.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JavascriptAspNetCore6RecaptchaV3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRecaptchaService _recaptchaService;
        private readonly IUserService _userService;
        public HomeController(ILogger<HomeController> logger, IRecaptchaService recaptchaService, IUserService userService)
        {
            _logger = logger;
            _recaptchaService = recaptchaService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View(new NewUser());
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromBody] NewUser newUser)
        {
            if (string.IsNullOrEmpty(newUser.RecaptchaToken))
                return BadRequest(new APIResponse() { Success = false, Error = "Recaptcha validation failed" });

            if (await _recaptchaService.ValidateRecaptcha(newUser.RecaptchaToken))
            {
                if (_userService.CreateUser(newUser))
                    return Ok(new APIResponse() { Success = true });
                else
                    return Ok(new APIResponse() { Success = false, Error = "Oops, there was an error. Please try again later" });
            }
            else return Ok(new APIResponse() { Success = false, Error = "Oops, there was an error. Please try again later" });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}