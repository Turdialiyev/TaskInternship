using System.Collections;
using Task.Modeles;

namespace Task.Services;
public interface IGuessService
{
   public new object PlayResult(int userId, int number);
   public IList  GameLeader();
    public User ExistUser(string name);
    public void GameLog(int gameId, int m, int p, int guessNumber);

}