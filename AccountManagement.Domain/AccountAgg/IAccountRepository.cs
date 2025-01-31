using _01_Framework.Domain;
using AccountManagement.Application.Contracts.Account;

namespace AccountManagement.Domain.AccountAgg
{
    public interface IAccountRepository : IRepository<Account,long>
    {
        Account? GetBy(string userName);
        List<AccountViewModel> Search(AccountSearchModel command);
        EditAccount GetDetails(long id);
    }
}
