namespace Api.Data;

/// <summary>
/// Представляет пользователя в системе.
/// </summary>
public class User
{
    /// <summary>
    /// Получает или задает уникальный идентификатор пользователя.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Получает или задает имя пользователя.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Получает или задает адрес электронной почты пользователя.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Получает или задает пароль пользователя.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Получает или задает описание пользователя.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Получает или задает фотографию пользователя в виде массива байтов.
    /// </summary>
    public byte[]? Photo { get; set; }
}