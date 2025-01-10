using Api.Data;
using Api.Models;

namespace Api.Services;

/// <summary>
/// Сервис для управления новостями в блоге.
/// </summary>
public class NewsService
{
    private readonly BlogDbContext _dbContext;
    private readonly NoSqlDataService _noSqlDataService;
    private readonly ImageService _imageService;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="NewsService"/>.
    /// </summary>
    /// <param name="dbContext">Контекст базы данных для работы с новостями.</param>
    /// <param name="noSqlDataService">Сервис для работы с NoSQL данными.</param>
    /// <param name="imageService">Сервис для работы с изображениями.</param>
    public NewsService(
        BlogDbContext dbContext, 
        NoSqlDataService noSqlDataService,
        ImageService imageService)
    {
        _dbContext = dbContext;
        _noSqlDataService = noSqlDataService;
        _imageService = imageService;
    }

    /// <summary>
    /// Получает список новостей, написанных автором с указанным идентификатором.
    /// </summary>
    /// <param name="userId">Идентификатор автора.</param>
    /// <returns>Список представлений новостей, написанных автором.</returns>
    public List<NewsView> GetByAuthor(int userId) =>
        _dbContext.News.Where(x=>x.AuthorId == userId)
            .OrderBy(x=>x.PostDate)
            .Reverse()
            .Select(ToView)
            .ToList();

    /// <summary>
    /// Создает новую новость.
    /// </summary>
    /// <param name="newsModel">Модель новости, содержащая текст и изображение.</param>
    /// <param name="userId">Идентификатор автора новости.</param>
    /// <returns>Созданная модель новости с установленным идентификатором и датой публикации.</returns>
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

    /// <summary>
    /// Обновляет существующую новость.
    /// </summary>
    /// <param name="newsModel">Модель новости с обновленным текстом и изображением.</param>
    /// <param name="userId">Идентификатор автора новости.</param>
    /// <returns>Обновленная модель представления новости, или null, если новость не найдена.</returns>
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

    /// <summary>
    /// Удаляет новость по заданному идентификатору.
    /// </summary>
    /// <param name="newsId">Идентификатор удаляемой новости.</param>
    /// <param name="userId">Идентификатор автора новости.</param>
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

    /// <summary>
    /// Получает список новостей для текущего пользователя на основе его подписок.
    /// </summary>
    /// <param name="userId">Идентификатор текущего пользователя.</param>
    /// <returns>Список представлений новостей для текущего пользователя.</returns>
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

    /// <summary>
    /// Устанавливает лайк на новость от имени пользователя.
    /// </summary>
    /// <param name="newsId">Идентификатор новости, на которую ставится лайк.</param>
    /// <param name="userId">Идентификатор пользователя, ставящего лайк.</param>
    public void SetLike(int newsId, int userId) =>
        _noSqlDataService.SetNewsLikes(userId, newsId);

    /// <summary>
    /// Преобразует объект новости в представление новости.
    /// </summary>
    /// <param name="news">Объект новости для преобразования.</param>
    /// <returns>Представление новости.</returns>
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