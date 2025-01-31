using _01_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;

namespace AccountManagement.Application
{
    public class AccountApplication(IAuthHelper authHelper, IAccountRepository accountRepository, IPasswordHasher passwordHasher, IFileUploader fileUploader) : IAccountApplication
    {
        private readonly IAccountRepository _accountRepository= accountRepository;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;
        private readonly IFileUploader _fileUploader = fileUploader;
        private readonly IAuthHelper _authHelper = authHelper;
        //public AccountApplication(IAccountRepository accountRepository, IPasswordHasher passwordHasher, IFileUploader fileUploader, IAuthHelper authHelper)
        //{
        //    _accountRepository = accountRepository;
        //    _passwordHasher = passwordHasher;
        //    _fileUploader = fileUploader;
        //    _authHelper = authHelper;
        //}

        public OperationResult Register(RegisterAccount command)
        {
            var operation = new OperationResult();

            if (_accountRepository.Exists(x => x.UserName == command.UserName))
                return operation.Failed(ApplicationMessages.ExistingUser);

            if (_accountRepository.Exists(x => x.Mobile == command.Mobile))
                return operation.Failed(ApplicationMessages.ExistingMobileNumber);

            var path = $"ProfilePhotos";
            var picturePath = _fileUploader.Upload(command.ProfilePhoto, path);
            var password = _passwordHasher.Hash(command.Password);
            var account = new Account(command.FullName, command.UserName, password, command.Mobile, command.RoleId, picturePath);

            _accountRepository.Create(account);
            _accountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditAccount command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.Get(command.Id);
            if (account == null)
                return operation.Failed(ApplicationMessages.NotFound);

            if (_accountRepository.Exists(x=>(x.UserName == command.UserName || x.Mobile == command.Mobile)  && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicateRecord);

            var path = $"ProfilePhotos";
            var picturePath = _fileUploader.Upload(command.ProfilePhoto, path);
            account.Edit(command.FullName, command.UserName, command.Mobile, command.RoleId, picturePath);
            _accountRepository.SaveChanges();
            return operation.Succeeded();
        }

        //public OperationResult Login(Login command)
        //{
        //    throw new NotImplementedException();
        //}

        public OperationResult Login(Login command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetBy(command.Username);

            if (account is null)
                return operation.Failed(ApplicationMessages.InvalidCredential);

            (bool Verified, bool NeedsUpgrade) result = _passwordHasher.Check(account.Password, command.Password);
            
            if (!result.Verified)
                return operation.Failed(ApplicationMessages.InvalidCredential);

            var authViewModel = new AuthViewModel(account.Id, account.RoleId,account.FullName,account.UserName);
            _authHelper.Signin(authViewModel);
            return operation.Succeeded();
        }

        public OperationResult ChangePassword(ChangePassword command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.Get(command.Id);
            if (account is null)
                return operation.Failed(ApplicationMessages.NotFound);

            if (command.Password != command.PasswordConfirmation)
                return operation.Failed(ApplicationMessages.PasswordsNotMatch);

            var password = _passwordHasher.Hash(command.Password);
            account.ChangePassword(password);
            _accountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            return _accountRepository.Search(searchModel);
        }

        public EditAccount GetDetails(long id)
        {
            return _accountRepository.GetDetails(id);
        }
    }
}
