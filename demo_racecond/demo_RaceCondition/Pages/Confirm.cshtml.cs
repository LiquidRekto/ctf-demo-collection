using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using demo_RaceCondition.Models;
using System.Net;

namespace demo_RaceCondition.Pages
{
    public class ConfirmModel : PageModel
    {
        private readonly UserService _userService;

        public ConfirmModel(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult OnGet(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ViewData["Message"] = "Invalid email confirmation request.";
                return Page();
            }
            var userId = HttpContext.Session.GetInt32("CurrentUserId");
            _userService.UpdateEmail(userId.Value, email);
            var user = _userService.GetAllUsers().FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                _userService.ConfirmEmail(user.Id);
                ViewData["Message"] = "Your email has been successfully confirmed!";
            }
            else
            {
                ViewData["Message"] = "User not found.";
            }

            return Page();
        }
    }
}