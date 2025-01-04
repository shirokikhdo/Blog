using Microsoft.EntityFrameworkCore;

namespace Blog.Data;

public class BlogDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public BlogDbContext(DbContextOptions options)
        : base(options)
    {
        Database.EnsureCreated();
    }
}