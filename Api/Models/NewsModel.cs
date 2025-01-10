namespace Api.Models;

/// <summary>
/// Представляет модель новости, содержащую информацию о новости, включая текст, изображение и статистику.
/// </summary>
public class NewsModel
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
    /// Получает или задает URL изображения, связанного с новостью.
    /// </summary>
    public string Image { get; set; }

    /// <summary>
    /// Получает или задает количество лайков, полученных новостью. Может быть <c>null</c>, если информация недоступна.
    /// </summary>
    public int? LikesCount { get; set; }

    /// <summary>
    /// Получает или задает дату и время публикации новости. Может быть <c>null</c>, если информация недоступна.
    /// </summary>
    public DateTime? PostDate { get; set; }
}