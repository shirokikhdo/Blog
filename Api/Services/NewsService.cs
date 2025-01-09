using Api.Data;
using Api.Models;

namespace Api.Services;

public class NewsService
{
    private readonly BlogDbContext _dbContext;
    private readonly NoSqlDataService _noSqlDataService;
    private readonly ImageService _imageService;

    public NewsService(
        BlogDbContext dbContext, 
        NoSqlDataService noSqlDataService,
        ImageService imageService)
    {
        _dbContext = dbContext;
        _noSqlDataService = noSqlDataService;
        _imageService = imageService;
    }

    public List<NewsView> GetByAuthor(int userId) =>
        _dbContext.News.Where(x=>x.AuthorId == userId)
            .OrderBy(x=>x.PostDate)
            .Reverse()
            .Select(ToView)
            .ToList();

    public NewsModel Create(NewsModel newsModel, int userId)
    {
        var news = new News
        {
            AuthorId = userId,
            Text = newsModel.Text,
            Image = _imageService.GetPhoto(newsModel.Image),
            PostDate = DateTime.Now,
        };

        _dbContext.News.Add(news);
        _dbContext.SaveChanges();

        newsModel.Id = news.Id;
        newsModel.PostDate = news.PostDate;

        return newsModel;
    }

    public NewsView Update(NewsModel newsModel, int userId)
    {
        var news = _dbContext.News
            .FirstOrDefault(x => x.Id == newsModel.Id 
                                 && x.AuthorId == userId);

        if (news is null)
            return null;

        news.Text = newsModel.Text;
        var image = _imageService.GetPhoto(newsModel.Image);
        if (!(news.Image?.Length > 10 && image.Length < 10))
            news.Image = image;

        _dbContext.News.Update(news);
        _dbContext.SaveChanges();

        var newsView = ToView(news);

        return newsView;
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

    public List<NewsView> GetNewsForCurrentUser(int userId)
    {
        var subs = _noSqlDataService.GetUserSubscribes(userId).Users;
        var news = new List<NewsView>();

        foreach (var sub in subs)
        {
            var allNewsByAuthor = _dbContext.News
                .Where(x => x.AuthorId == sub.Id);
            news.AddRange(allNewsByAuthor.Select(ToView));
        }

        news.Sort(new NewsViewComparer());
        return news;
    }

    public void SetLike(int newsId, int userId) =>
        _noSqlDataService.SetNewsLikes(userId, newsId);

    private NewsView ToView(News news)
    {
        var likes = _noSqlDataService.GetNewsLikes(news.Id);
        var newsModel = new NewsView
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