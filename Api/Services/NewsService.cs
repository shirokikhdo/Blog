using Api.Data;
using Api.Models;

namespace Api.Services;

public class NewsService
{
    private readonly BlogDbContext _dbContext;
    private readonly NoSqlDataService _noSqlDataService;

    public NewsService(
        BlogDbContext dbContext, 
        NoSqlDataService noSqlDataService)
    {
        _dbContext = dbContext;
        _noSqlDataService = noSqlDataService;
    }

    public List<NewsModel> GetByAuthor(int userId) =>
        _dbContext.News.Where(x=>x.AuthorId == userId)
            .OrderBy(x=>x.PostDate)
            .Reverse()
            .Select(ToModel)
            .ToList();

    public NewsModel Create(NewsModel newsModel, int userId)
    {
        var news = new News
        {
            AuthorId = userId,
            Text = newsModel.Text,
            Image = newsModel.Image,
            PostDate = DateTime.Now,
        };

        _dbContext.News.Add(news);
        _dbContext.SaveChanges();

        newsModel.Id = news.Id;
        newsModel.PostDate = news.PostDate;

        return newsModel;
    }

    public NewsModel Update(NewsModel newsModel, int userId)
    {
        var news = _dbContext.News
            .FirstOrDefault(x => x.Id == newsModel.Id 
                                 && x.AuthorId == userId);

        if (news is null)
            return null;

        news.Text = newsModel.Text;
        news.Image = newsModel.Image;

        _dbContext.News.Update(news);
        _dbContext.SaveChanges();

        newsModel = ToModel(news);

        return newsModel;
    }

    public void Delete(int newsId, int userId)
    {
        var news = _dbContext.News
            .FirstOrDefault(x => x.Id == newsId 
                                 && x.AuthorId == userId);

        if(news is null)
            return;

        _dbContext.News.Remove(news);
        _dbContext.SaveChanges();
    }

    public List<NewsModel> GetNewsForCurrentUser(int userId)
    {
        var subs = _noSqlDataService.GetUserSubscribes(userId).Users;
        var news = new List<NewsModel>();

        foreach (var sub in subs)
        {
            var allNewsByAuthor = _dbContext.News
                .Where(x => x.AuthorId == sub.Id);
            news.AddRange(allNewsByAuthor.Select(ToModel));
        }

        news.Sort(new NewsModelComparer());
        return news;
    }

    public void SetLike(int newsId, int userId) =>
        _noSqlDataService.SetNewsLikes(userId, newsId);

    private NewsModel ToModel(News news)
    {
        var likes = _noSqlDataService.GetNewsLikes(news.Id);
        var newsModel = new NewsModel
        {
            Id = news.Id,
            Text = news.Text,
            Image = news.Image,
            PostDate = news.PostDate,
            LikesCount = likes is null 
                ? 0 
                : likes.Users.Count
        };
        return newsModel;
    }
}