namespace Task.Repositories;

using Microsoft.EntityFrameworkCore;
using Task.Data;
using Task.Modeles;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext dbContext) : base(dbContext) { }

    public override IQueryable<User> GetAll(){
        return base.GetAll().Include(x => x.Games);
    }

}