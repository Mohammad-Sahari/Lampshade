using _01_Framework.Domain;
using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AccountManagement.Domain.RoleAgg
{
    public class Role(string name) : EntityBase
    {
        public string Name { get; private set; } = name;
        
        public List<Account> Accounts { get; private set; }


        public void Edit(string name)
        {
            Name = name;
        }
    }
}
