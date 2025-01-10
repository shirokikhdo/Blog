namespace Api.Models;

/// <summary>
/// Представляет профиль пользователя, содержащий основную информацию о пользователе.
/// </summary>
public class UserProfile
{
    /// <summary>
    /// Получает или задает уникальный идентификатор профиля пользователя.
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
    /// Получает или задает описание пользователя. Это может быть краткая биография или другая информация о пользователе.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Получает или задает фотографию пользователя. Может хранить путь к изображению или объект изображения.
    /// </summary>
    public object Photo { get; set; }

    /// <summary>
    /// Получает или задает количество подписчиков пользователя.
    /// </summary>
    public int SubsCount { get; set; }
}