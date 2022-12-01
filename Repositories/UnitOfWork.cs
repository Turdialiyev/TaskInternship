using Task.Data;

namespace Task.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IUserRepository Users { get; set; }
    public IGameRepository Games { get; set; }
    public IGameLogRepository GameLogs { get; set; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Users = new UserRepository(context);
        Games = new GameRepository(context);
        GameLogs= new GameLogRepository(context);
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    public int Save() => _context.SaveChanges();
}