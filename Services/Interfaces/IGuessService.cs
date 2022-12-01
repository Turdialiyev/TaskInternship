using System.Collections;
using Task.Modeles;
using Task.Models;

namespace Task.Services;
public interface IGuessService
{
    public ValueTask<Result<Game>> PlayResult(int userId, int number);
    public ValueTask<Result<List<User>>> GameLeader(int min);
    public ValueTask<Result<User>> ExistUser(string name);
    public void GameLog(int gameId, int m, int p, int guessNumber);

}