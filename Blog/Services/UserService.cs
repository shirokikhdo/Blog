using System.Security.Claims;
using System.Text;
using Blog.Data;

namespace Blog.Services;

public class UserService
{
    public (string login, string password) GetUserLoginPassFromBasicAuth(HttpRequest request)
    {
        var userName = string.Empty;
        var userPass = string.Empty;
        var authHeader = request.Headers["Authorization"].ToString();
        
        if (!authHeader.StartsWith("Basic")) 
            return (userName, userPass);

        var encodedUserNamePass = authHeader.Replace("Basic ", "");
        var encoding = Encoding.GetEncoding("iso-8859-1");
        var namePassArray = encoding.GetString(Convert.FromBase64String(encodedUserNamePass))
            .Split(':');
        userName = namePassArray[0];
        userPass = namePassArray[1];
        return (userName, userPass);
    }

    public (ClaimsIdentity identity, int id)? GetIdentity(string email, string password)
    {
        var currentUser = GetUserByLogin(email);

        if (currentUser == null 
            || !VerifyHashedPassword(currentUser.Password, password)) 
            return null;

        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, currentUser.Email)
        };

        var claimsIdentity = new ClaimsIdentity(
            claims,
            "Token",
            ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);

        return (claimsIdentity, currentUser.Id);
    }

    private User? GetUserByLogin(string email)
    {
        throw new NotImplementedException();
    }

    private bool VerifyHashedPassword(string password1, string password2) =>
        password1 == password2;
}