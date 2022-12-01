namespace Task.Repositories;

using Microsoft.EntityFrameworkCore;
using Task.Data;
using Task.Modeles;

public class GameRepository : GenericRepository<Game>, IGameRepository
{
    public GameRepository(AppDbContext dbContext) : base(dbContext) { }

    public override IQueryable<Game> GetAll()
    {
        return base.GetAll().Include(x => x.GameLogs);
    }
}