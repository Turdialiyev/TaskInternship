namespace Task.Repositories;

using Task.Data;
using Task.Modeles;

public class GameRepository:GenericRepository<Game>, IGameRepository
{
    public GameRepository(AppDbContext dbContext):base(dbContext){}
}