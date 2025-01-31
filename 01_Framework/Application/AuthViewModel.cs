namespace _01_Framework.Application;

public class AuthViewModel(long id, long roleId, string fullname, string username)
{
    public long Id { get; set; } = id;
    public long RoleId { get; set; } = roleId;
    //public string Role { get; set; }
    public string Fullname { get; set; } = fullname;
    public string Username { get; set; } = username;
}