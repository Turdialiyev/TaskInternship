namespace Task.Repositories;

using Task.Data;
using Task.Modeles;

public class GuessRepository:GenericRepository<Guess>, IGuessRepository
{
    public GuessRepository(AppDbContext dbContext):base(dbContext){}
}