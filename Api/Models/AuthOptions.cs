using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Api.Models;

/// <summary>
/// Класс, содержащий параметры аутентификации для приложения.
/// </summary>
public static class AuthOptions
{
    /// <summary>
    /// Секретный ключ для шифрования токенов.
    /// </summary>
    private const string KEY = "BlogSuperSecret_BlogSecretKey!123456789";

    /// <summary>
    /// Издатель токена (Auth Server).
    /// </summary>
    public const string ISSUER = "BlogAuthServer";

    /// <summary>
    /// Аудитория токена (Auth Client).
    /// </summary>
    public const string AUDIENCE = "BlogAuthClient";

    /// <summary>
    /// Время жизни токена в минутах.
    /// </summary>
    public const int LIFETIME = 10;

    /// <summary>
    /// Получает симметричный ключ безопасности на основе заданного секретного ключа.
    /// </summary>
    /// <returns>Симметричный ключ безопасности.</returns>
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
}