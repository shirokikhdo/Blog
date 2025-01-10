namespace Api.Models;

/// <summary>
/// Представляет сокращенную модель пользователя, содержащую основные сведения о пользователе.
/// </summary>
public class UserShortModel
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
    /// Получает или задает краткое описание пользователя.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Получает или задает фотографию пользователя в виде массива байтов. 
    /// Может быть <c>null</c>, если фотография не задана.
    /// </summary>
    public byte[]? Photo { get; set; }
}