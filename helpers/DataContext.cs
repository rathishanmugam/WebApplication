namespace WebApplication.Helpers;

using Microsoft.EntityFrameworkCore;
using WebApplication.Entities;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;
    public DbSet<User> Users => Set<User>();
    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to postgres with connection string from app settings
        options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
    }
    
    // public DbSet<User> Users { get; set; }
}