using _01_Framework.Application;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Application
{
    public class RoleApplication(IRoleRepository roleRepository) : IRoleApplication
    {
        private readonly IRoleRepository _roleRepository = roleRepository;

        public OperationResult Create(CreateRole command)
        {
            var operation = new OperationResult();

            if (_roleRepository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicateRecord);
            
            var role = new Role(command.Name, new List<Permission>());
            _roleRepository.Create(role);
            _roleRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditRole command)
        {
            var operation = new OperationResult();
            var role = _roleRepository.Get(command.Id);

            if (role is null)
                return operation.Failed(ApplicationMessages.NotFound);

            if (_roleRepository.Exists(x=>x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicateRecord);

            role.Edit(command.Name, new List<Permission>());
            _roleRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditRole GetDetails(long id)
        {
            return _roleRepository.GetDetails(id);
        }

        public List<RoleViewModel> GetRoles()
        {
            return _roleRepository.GetRoles();
        }
    }
}
