namespace Task.Repositories;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; set; }
    IGameRepository Games { get; set; }
    IGameLogRepository GameLogs { get; set; }
    int Save();
}