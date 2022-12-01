using Microsoft.EntityFrameworkCore;
using Task.Modeles;

namespace Task.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    public DbSet<User>? Users { get; set; }
    public DbSet<Game>? Games { get; set; }
    public DbSet<GameLog>? GameLogs { get; set; }
}
