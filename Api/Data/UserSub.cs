namespace Api.Data;

/// <summary>
/// Представляет подписку пользователя.
/// </summary>
public class UserSub
{
    /// <summary>
    /// Получает или задает уникальный идентификатор подписки.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Получает или задает дату создания подписки.
    /// </summary>
    public DateTime Date { get; set; }
}