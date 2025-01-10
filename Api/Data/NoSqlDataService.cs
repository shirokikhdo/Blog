using LiteDB;

namespace Api.Data;

/// <summary>
/// Сервис для работы с данными в NoSQL базе данных.
/// </summary>
public class NoSqlDataService
{
    private readonly string _connectionString = "NoSqlBlogData.db";

    private const string SUBS = "SubsCollection";
    private const string LIKES = "LikesCollection";

    /// <summary>
    /// Получает подписки пользователя по его идентификатору.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <returns>Объект <see cref="UserSubscribe"/>, содержащий подписки пользователя, или null, если подписок нет.</returns>
    public UserSubscribe GetUserSubscribes(int userId)
    {
        using var db = new LiteDatabase(_connectionString);

        var subs = db.GetCollection<UserSubscribe>(SUBS);
        var userSubs = subs.FindOne(s => s.Id == userId);
        return userSubs;
    }

    /// <summary>
    /// Устанавливает подписку одного пользователя на другого.
    /// </summary>
    /// <param name="from">Идентификатор пользователя, который подписывается.</param>
    /// <param name="to">Идентификатор пользователя, на которого осуществляется подписка.</param>
    /// <returns>Объект <see cref="UserSubscribe"/>, содержащий обновленные подписки пользователя.</returns>
    public UserSubscribe SetUserSubscribes(int from, int to)
    {
        using var db = new LiteDatabase(_connectionString);

        var subs = db.GetCollection<UserSubscribe>(SUBS);
        var userSubs = subs.FindOne(s => s.Id == from);
        var sub = new UserSub
        {
            Id = to,
            Date = DateTime.Now
        };

        if (userSubs != null)
        {
            if (userSubs.Users.Select(x=>x.Id).Contains(to)) 
                return userSubs;

            userSubs.Users.Add(sub);
            subs.Update(userSubs);
        }
        else
        {
            var newSub = new UserSubscribe
            {
                Id = from,
                Users = new List<UserSub>() { sub }
            };
            subs.Insert(newSub);
            subs.EnsureIndex(x => x.Id);
            userSubs = newSub;
        }

        return userSubs;
    }

    /// <summary>
    /// Получает лайки для новости по её идентификатору.
    /// </summary>
    /// <param name="newsId">Идентификатор новости.</param>
    /// <returns>Объект <see cref="NewsLike"/>, содержащий лайки для новости, или null, если лайков нет.</returns>
    public NewsLike GetNewsLikes(int newsId)
    {
        using var db = new LiteDatabase(_connectionString);

        var likes = db.GetCollection<NewsLike>(LIKES);
        var newsLike = likes.FindOne(s => s.NewsId == newsId);
        return newsLike;
    }

    /// <summary>
    /// Устанавливает лайк от пользователя на новость.
    /// </summary>
    /// <param name="from">Идентификатор пользователя, ставящего лайк.</param>
    /// <param name="newsId">Идентификатор новости, на которую ставится лайк.</param>
    /// <returns>Объект <see cref="NewsLike"/>, содержащий обновленные лайки для новости.</returns>
    public NewsLike SetNewsLikes(int from, int newsId)
    {
        using var db = new LiteDatabase(_connectionString);

        var likes = db.GetCollection<NewsLike>(LIKES);
        var newsLikes = likes.FindOne(s => s.NewsId == newsId);

        if (newsLikes != null)
        {
            if (newsLikes.Users.Contains(from))
                return newsLikes;

            newsLikes.Users.Add(from);
            likes.Update(newsLikes);
        }
        else
        {
            var newLike = new NewsLike
            {
                NewsId = newsId,
                Users = new List<int>() { from }
            };
            likes.Insert(newLike);
            likes.EnsureIndex(x => x.NewsId);
            newsLikes = newLike;
        }

        return newsLikes;
    }
}