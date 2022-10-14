namespace DotNetArchitecture3.JWTSettings
{
    public interface IJWTHelper
    {
        public string Authentication(string UserName, string Password);
        public string GetUserDetails();
    }
}
