using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Mail;
using System.Net;

namespace demo_RaceCondition.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UserService _userService;
        [BindProperty]

        public string Email { get; set; }
        public string CurrentUserName { get; set; }
        public IndexModel(ILogger<IndexModel> logger,UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        public IActionResult OnGet()
        {
            CurrentUserName = HttpContext.Session.GetString("CurrentUserName");
            if (string.IsNullOrEmpty(CurrentUserName))
            {
                return RedirectToPage("/Login");
            }

            ViewData["CurrentUserName"] = CurrentUserName;

            return Page();
        }
        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(Email))
            {
                ModelState.AddModelError("", "Email không được để trống.");
                return Page();
            }

            var userId = HttpContext.Session.GetInt32("CurrentUserId");
            if (userId.HasValue)
            {
                _userService.UpdateEmail(userId.Value, Email);
                SendConfirmationEmail(Email);
            }

            return RedirectToPage("/Index");
        }

        private void SendConfirmationEmail(string email)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("hieunguyenhuy2001@gmail.com", "urhefuvllahpqqdd"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("hieunguyenhuy2001@gmail.com"),
                Subject = "Email Update Confirmation",
                Body = $"Please confirm your email update by clicking the link: <a href='http://localhost:7212/Confirm?email={WebUtility.UrlEncode(email)}'>Confirm Email</a>",
                IsBodyHtml = true,
            };

            mailMessage.To.Add(email);

            smtpClient.Send(mailMessage);
        }

    }
}
