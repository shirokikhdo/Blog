using Blog.Data;
using Blog.Models;

namespace Blog.Services;

public class NewsService
{
    private readonly BlogDbContext _dbContext;

    public NewsService(BlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<News> GetByAuthor(int userId) =>
        _dbContext.News.Where(x=>x.AuthorId == userId)
            .Reverse()
            .ToList();

    public NewsModel Create(NewsModel newsModel, int userId)
    {
        var news = new News
        {
            AuthorId = userId,
            Text = newsModel.Text,
            Image = newsModel.Image
        };

        _dbContext.News.Add(news);
        _dbContext.SaveChanges();

        newsModel.Id = news.Id;

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

    }
}