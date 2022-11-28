namespace Task.Repositories;

using Task.Data;
using Task.Modeles;

public class UserRepository:GenericRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext dbContext):base(dbContext){}
}