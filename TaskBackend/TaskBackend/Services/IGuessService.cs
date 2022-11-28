using Task.Modeles;

namespace Task.Services;
public interface IGuessService
{
    public Tuple<int, int, IQueryable<GameLog>> PlayResult(string userName, int number);
    public User ExistUser(string name);
    public void GameLog(int gameId, int m, int p, int guessNumber);

}