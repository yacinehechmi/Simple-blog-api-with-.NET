namespace Tidjma.Data;
using Microsoft.EntityFrameworkCore;
using Tidjma.Models;


public class TidjmaDbContext : DbContext 
{
    protected readonly IConfiguration Configuration;

    public TidjmaDbContext(IConfiguration configuration)
    {
        Configuration = configuration;

    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(Configuration.GetConnectionString("tidjma_dev")).EnableSensitiveDataLogging();
        base.OnConfiguring(options);
    }

    public DbSet<Article> Articles => Set<Article>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<User> Users => Set<User>();

    //public DbSet<T> Data { get; set; }
}
