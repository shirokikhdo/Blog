namespace Api.Data;

/// <summary>
/// Представляет подписку пользователей.
/// </summary>
public class UserSubscribe
{
    /// <summary>
    /// Получает или задает уникальный идентификатор подписки.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Получает или задает список пользователей, подписанных на данную подписку.
    /// </summary>
    public List<UserSub> Users { get; set; }
}