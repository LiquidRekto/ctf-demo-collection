using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web;

namespace demo_RaceCondition.Pages
{
    public class ConfirmModel : PageModel
    {
        private readonly UserService _userService;
        public bool IsSuccess { get; set; }

        public ConfirmModel(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult OnGet(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                IsSuccess = false;
                return Page();
            }

            // Decode the email from the query string
            var decodedEmail = HttpUtility.UrlDecode(email);

            // Find the user by email and confirm it
            var user = _userService.GetAllUsers().FirstOrDefault(u => u.Email == decodedEmail);
            if (user != null)
            {
                _userService.ConfirmEmail(user.Id); // Confirm the email in the database
                IsSuccess = true;
            }
            else
            {
                IsSuccess = false;
            }

            return Page();
        }
    }
}
