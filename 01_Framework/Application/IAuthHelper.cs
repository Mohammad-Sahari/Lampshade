namespace _01_Framework.Application
{
    public interface IAuthHelper
    {
        void Signin(AuthViewModel account);
        void SignOut();
        bool IsAuthenticated();
        void DeActiveUser(long id);
    }
}
