namespace AccountManagement.Application.Contracts.Account
{
    public class ChangePassword
    {
        public long Id { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}
