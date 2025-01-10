namespace Api.Data;

/// <summary>
/// Представляет новость в блоге.
/// </summary>
public class News
{
    /// <summary>
    /// Получает или задает уникальный идентификатор новости.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Получает или задает идентификатор автора, написавшего новость.
    /// </summary>
    public int AuthorId { get; set; }

    /// <summary>
    /// Получает или задает текст новости.
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Получает или задает изображение, связанное с новостью, в виде массива байтов.
    /// </summary>
    public byte[]? Image { get; set; }

    /// <summary>
    /// Получает или задает дату и время публикации новости.
    /// </summary>
    public DateTime PostDate { get; set; }
}