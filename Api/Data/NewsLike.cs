namespace Api.Data;

/// <summary>
/// Представляет лайки, связанные с новостью.
/// </summary>
public class NewsLike
{
    /// <summary>
    /// Получает или задает уникальный идентификатор новости, к которой относится лайк.
    /// </summary>
    public int NewsId { get; set; }

    /// <summary>
    /// Получает или задает список идентификаторов пользователей, которые поставили лайк на новость.
    /// </summary>
    public List<int> Users { get; set; }
}