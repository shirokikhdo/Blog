using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class BlogDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
    public DbSet<News> News { get; set; }

    public BlogDbContext(DbContextOptions options)
        : base(options)
    {
        Database.EnsureCreated();
    }
}