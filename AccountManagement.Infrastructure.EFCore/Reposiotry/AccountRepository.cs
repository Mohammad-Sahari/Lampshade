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

        public Account? GetBy(string userName) => _context.Accounts.FirstOrDefault(x => x.UserName == userName);
                                                 

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            var query = _context.Accounts
                .Include(x=>x.Role)
                .Select(x => new AccountViewModel
            {
                Id = x.Id,
                FullName = x.FullName,
                UserName = x.UserName,
                Mobile = x.Mobile,
                RoleId = x.RoleId,
                Role = x.Role.Name,
                CreationDate = x.CreationDate.ToFarsi(),
                ProfilePhoto = x.ProfilePhoto
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
