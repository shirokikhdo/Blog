using Microsoft.EntityFrameworkCore;

namespace Api.Data;

/// <summary>
/// Контекст базы данных для блога, унаследованный от <see cref="DbContext"/>.
/// </summary>
public class BlogDbContext : DbContext
{
    /// <summary>
    /// Получает или задает коллекцию пользователей в базе данных.
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Получает или задает коллекцию новостей в базе данных.
    /// </summary>
    public DbSet<News> News { get; set; }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="BlogDbContext"/>.
    /// </summary>
    /// <param name="options">Параметры конфигурации для контекста базы данных.</param>
    public BlogDbContext(DbContextOptions options)
        : base(options)
    {
        Database.EnsureCreated();
    }
}