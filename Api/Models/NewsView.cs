namespace Api.Models;

/// <summary>
/// Представляет представление новости, содержащее информацию о новости для отображения в пользовательском интерфейсе.
/// </summary>
public class NewsView
{
    /// <summary>
    /// Получает или задает уникальный идентификатор новости.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Получает или задает текст новости.
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Получает или задает изображение, связанное с новостью. Может быть любого типа, например, URL или объект изображения.
    /// </summary>
    public object Image { get; set; }

    /// <summary>
    /// Получает или задает количество лайков, полученных новостью. Может быть <c>null</c>, если информация недоступна.
    /// </summary>
    public int? LikesCount { get; set; }

    /// <summary>
    /// Получает или задает дату и время публикации новости. Может быть <c>null</c>, если информация недоступна.
    /// </summary>
    public DateTime? PostDate { get; set; }
}