namespace Task.Repositories;

using Task.Data;
using Task.Modeles;

public class GameLogRepository:GenericRepository<GameLog>, IGameLogRepository
{
    public GameLogRepository(AppDbContext dbContext):base(dbContext){}
}