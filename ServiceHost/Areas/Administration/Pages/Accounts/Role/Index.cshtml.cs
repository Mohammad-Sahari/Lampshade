using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ServiceHost.Areas.Administration.Pages.Accounts.Role
{
    public class IndexModel(IRoleApplication roleApplication) : PageModel
    {
        [TempData] public string Message { get; set; }

        public List<RoleViewModel> Roles;

        private readonly IRoleApplication _roleApplication = roleApplication;

        public void OnGet(AccountSearchModel searchModel)
        {
            Roles = _roleApplication.GetRoles();
        }
    }
}
