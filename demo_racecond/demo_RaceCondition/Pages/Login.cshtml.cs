using demo_RaceCondition.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace demo_RaceCondition.Pages
{
    public class LoginModel : PageModel
    {
        private readonly UserService _userService;

        private readonly demo_bypassContext _demo_bypassContext;


        private const int timeLoginLimit = 10;

        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public LoginModel(demo_bypassContext demo_bypassContext)
        {
            _userService = new UserService(demo_bypassContext);
        }

        public IActionResult OnPost()
        {
            var user = _userService.Login(Email, Password);
            if (user == null)
            {
                ViewData["Message"] = "Invalid username or password";
                UpdateLoginTimeAttemp();
                return Page();
            }

            HttpContext.Session.SetInt32("CurrentUserId", user.Id);
            HttpContext.Session.SetString("CurrentUserName", user.Name.ToString());

            return RedirectToPage("/Index");
        }

        public void UpdateLoginTimeAttemp()
        {
            int? timeLogin = HttpContext.Session.GetInt32("LoginTimeAttemp");
            if (!timeLogin.HasValue)
            {
                HttpContext.Session.SetInt32("LoginTimeAttemp", 1);

            }
            else if (timeLogin.Value < 10)
            {
                HttpContext.Session.SetInt32("LoginTimeAttemp", timeLogin.Value + 1);
            }
            else
            {
                var startTimeRaw = HttpContext.Session.GetString("StartTime");
                if (startTimeRaw == null)
                {
                    HttpContext.Session.SetString("StartTime", DateTime.Now.ToString());
                    ViewData["Message"] = "You have login fail many time, try again after 60s";
                }
                else
                {
                    int remainTime = (DateTime.Now - DateTime.Parse(startTimeRaw)).Seconds;
                    if (remainTime > 60)
                    {
                        HttpContext.Session.Remove("LoginTimeAttemp");
                        HttpContext.Session.Remove("StartTime");
                        ViewData["Message"] = $"You have login fail many time, try again after {60 - remainTime}s";

                    }
                }
            }
        }
    }
}
