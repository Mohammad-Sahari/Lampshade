using _01_Framework.Application;
using _01_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore.Reposiotry
{
    public class AccountRepository : RepositoryBase<Account,long> , IAccountRepository
    {
        private readonly AccountContext _context;

        public AccountRepository(AccountContext context) :base(context)
        {
            _context = context;
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            var query = _context.Accounts.Select(x => new AccountViewModel
            {
                Id = x.Id,
                FullName = x.FullName,
                UserName = x.UserName,
                Mobile = x.Mobile,
                RoleId = 2,
                Role = "مدیر سیستم",
            });

            if (!string.IsNullOrWhiteSpace(searchModel.FullName))
                query = query.Where(x => x.FullName.Contains(searchModel.FullName));

            if (!string.IsNullOrWhiteSpace(searchModel.UserName))
                query = query.Where(x => x.UserName.Contains(searchModel.FullName));

            if (!string.IsNullOrWhiteSpace(searchModel.Mobile))
                query = query.Where(x => x.Mobile.Contains(searchModel.FullName));

            if (searchModel.RoleId > 0)
                query = query.Where(x => x.RoleId == searchModel.RoleId);

            return query.OrderByDescending(x => x.Id).ToList();
        }

        public EditAccount GetDetails(long id)
        {
            return _context.Accounts.Select(x => new EditAccount
            {
                Id= x.Id,
                FullName= x.FullName,
                UserName= x.UserName,
                Mobile= x.Mobile,
                RoleId= x.RoleId,
            }).FirstOrDefault(x => x.Id == id);
        }
    }
}
