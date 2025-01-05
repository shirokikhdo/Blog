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
        var userSubs = subs.FindOne(s => s.Id == userId);
        return userSubs;
    }

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