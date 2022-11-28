namespace Task.Repositories;

using Task.Data;
using Task.Modeles;

public class AttemptRepository:GenericRepository<Attempt>, IAttemptRepository
{
    public AttemptRepository(AppDbContext dbContext):base(dbContext){}
}