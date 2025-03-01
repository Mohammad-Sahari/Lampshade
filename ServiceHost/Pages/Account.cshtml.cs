using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class AccountModel(IAccountApplication accountApplication) : PageModel
    {
        [TempData]
        public string LoginMessage { get; set; }
        public string RegisterMessage { get; set; }

        private readonly IAccountApplication _accountApplication = accountApplication;
        public void OnGet()
        {
        }

        public IActionResult OnPostLogin(Login command)
        {
            var result = _accountApplication.Login(command);
            if (result.IsSucceeded)
               return RedirectToPage("/Index");
            LoginMessage = result.Message;
            return RedirectToPage("/Login");
        }
        public IActionResult OnPostRegister(RegisterAccount command)
        {
            var result = _accountApplication.Register(command);
            if (result.IsSucceeded)
                return RedirectToPage("/Account");

            RegisterMessage = result.Message;
            return RedirectToPage("/Account");
        }


    }
}
