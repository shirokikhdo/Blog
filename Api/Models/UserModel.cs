namespace Api.Models;

/// <summary>
/// Представляет модель пользователя, содержащую информацию о пользователе.
/// </summary>
public class UserModel
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
    /// Обратите внимание, что для безопасности пароли должны храниться в зашифрованном виде.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Получает или задает описание пользователя. Это может быть краткая биография или другая информация о пользователе.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Получает или задает путь к фотографии пользователя. Может быть URL или локальный путь к файлу изображения.
    /// </summary>
    public string Photo { get; set; }
}