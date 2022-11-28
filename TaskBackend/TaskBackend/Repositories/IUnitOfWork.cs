namespace Task.Repositories;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; set; }
    IAttemptRepository Attempts { get; set; }
    IGuessRepository Guesses { get; set; }
    int Save();
}