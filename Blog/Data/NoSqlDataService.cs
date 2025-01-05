using LiteDB;

namespace Blog.Data;

public class NoSqlDataService
{
    private readonly string _connectionString = "NoSqlBlogData.db";

    private const string SUBS = "SubsCollection";
    private const string LIKES = "LikesCollection";

    public UserSubscribe GetUserSubscribes(int userId)
    {
        using var db = new LiteDatabase(_connectionString);

        var subs = db.GetCollection<UserSubscribe>(SUBS);
        var userSubs = subs.FindOne(s => s.UserId == userId);
        return userSubs;
    }

    public UserSubscribe SetUserSubscribes(int from, int to)
    {
        using var db = new LiteDatabase(_connectionString);

        var subs = db.GetCollection<UserSubscribe>(SUBS);
        var userSubs = subs.FindOne(s => s.UserId == from);

        if (userSubs != null)
        {
            if (userSubs.Users.Contains(to)) 
                return userSubs;

            userSubs.Users.Add(to);
            subs.Update(userSubs);
        }
        else
        {
            var newSub = new UserSubscribe
            {
                UserId = from,
                Users = new List<int>() {to}
            };
            subs.Insert(newSub);
            subs.EnsureIndex(x => x.UserId);
            userSubs = newSub;
        }

        return userSubs;
    }

    public NewsLike GetNewsLikes(int newsId)
    {
        using var db = new LiteDatabase(_connectionString);

        var likes = db.GetCollection<NewsLike>(LIKES);
        var newsLike = likes.FindOne(s => s.NewsId == newsId);
        return newsLike;
    }

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