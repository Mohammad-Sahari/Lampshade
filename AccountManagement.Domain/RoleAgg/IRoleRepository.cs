using _01_Framework.Domain;
using AccountManagement.Application.Contracts.Role;

namespace AccountManagement.Domain.RoleAgg
{
    public interface IRoleRepository : IRepository<Role, long>
    {
        EditRole GetDetails(long id);
        List<RoleViewModel> GetRoles();
    }
}
