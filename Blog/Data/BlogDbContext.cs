using Microsoft.EntityFrameworkCore;

namespace Blog.Data;

public class BlogDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
    public DbSet<News> News { get; set; }

    public DbSet<UserSubscribes> UserSubscribes { get; set; }

    public DbSet<NewsLikes> NewsLikes { get; set; }

    public BlogDbContext(DbContextOptions options)
        : base(options)
    {
        Database.EnsureCreated();
    }
}