namespace Api.Models;

/// <summary>
/// Представляет токен аутентификации, содержащий информацию о пользователе и сроке действия токена.
/// </summary>
public class AuthToken
{
    /// <summary>
    /// Получает количество минут, на которое выдан токен.
    /// </summary>
    public int Minutes { get; }

    /// <summary>
    /// Получает строку, представляющую сам токен доступа.
    /// </summary>
    public string AccessToken { get; }

    /// <summary>
    /// Получает имя пользователя, которому принадлежит токен.
    /// </summary>
    public string UserName { get; }

    /// <summary>
    /// Получает уникальный идентификатор пользователя, которому принадлежит токен.
    /// </summary>
    public int UserId { get; }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="AuthToken"/> с заданными параметрами.
    /// </summary>
    /// <param name="minutes">Количество минут, на которое выдан токен.</param>
    /// <param name="accessToken">Строка, представляющая токен доступа.</param>
    /// <param name="userName">Имя пользователя, которому принадлежит токен.</param>
    /// <param name="userId">Уникальный идентификатор пользователя.</param>
    public AuthToken(int minutes, string accessToken, string userName, int userId)
    {
        Minutes = minutes;
        AccessToken = accessToken;
        UserName = userName;
        UserId = userId;
    }
}