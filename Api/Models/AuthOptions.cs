using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Api.Models;

public static class AuthOptions
{
    private const string KEY = "BlogSuperSecret_BlogSecretKey!123456789";

    public const string ISSUER = "BlogAuthServer";

    public const string AUDIENCE = "BlogAuthClient";

    public const int LIFETIME = 10;

    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
}