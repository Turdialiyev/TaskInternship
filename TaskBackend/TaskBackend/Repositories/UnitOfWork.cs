using Task.Data;

namespace Task.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IUserRepository Users { get; set; }
    public IAttemptRepository Attempts { get; set; }
    public IGuessRepository Guesses { get; set; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Users = new UserRepository(context);
        Attempts = new AttemptRepository(context);
        Guesses= new GuessRepository(context);
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    public int Save() => _context.SaveChanges();
}