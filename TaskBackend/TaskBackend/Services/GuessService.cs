
using System.Collections;
using Task.Modeles;
using System.Linq;
using Task.Repositories;

namespace Task.Services;

public partial class GuessService : IGuessService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GuessService> _logger;

    public GuessService(IUnitOfWork unitOfWork, ILogger<GuessService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public IList PlayResult(int userId, int number)
    {

        var rendom = RandomNumbers();
        var guessResult = GamePlay(number, rendom);
        var attemptModel = new Game();

        attemptModel.UserId = userId;

        var existGame = _unitOfWork.Games.GetAll().Where(u => u.UserId == userId).FirstOrDefault(u => u.EGameState == EGameState.Continue);

        if (existGame != null && existGame!.Attempt == 7)
        {
            rendom = existGame.Number;
            guessResult = GamePlay(number, rendom);
            existGame.Attempt += 1;
            existGame.EGameState = EGameState.Ended;
            if (guessResult.Item2 == 4)
            {
                existGame.CheckNumber = true;
            }

            _unitOfWork.Games.Update(existGame);
        }

        if (existGame != null && existGame!.Attempt < 7)
        {
            rendom = existGame.Number;
            guessResult = GamePlay(number, rendom);
            if (guessResult.Item2 == 4)
            {
                existGame!.CheckNumber = true;
                existGame.EGameState = EGameState.Ended;
            }
            existGame!.Attempt += 1;
            _unitOfWork.Games.Update(existGame);
        }

        if (existGame == null)
        {
            attemptModel.Attempt = 1;
            attemptModel.Number = rendom;
            if (guessResult.Item2 == 4)
            {
                attemptModel.Number = rendom;
                attemptModel.CheckNumber = true;
                attemptModel.EGameState = EGameState.Ended;
            }
            else
            {
                attemptModel!.CheckNumber = false;
                attemptModel.EGameState = EGameState.Continue;
            }

            existGame = _unitOfWork.Games.AddAsync(attemptModel).Result;
        }
        GameLog(existGame!.Id, guessResult.Item1, guessResult.Item2, number);

        var gameLog = _unitOfWork.GameLogs.GetAll().Where(g => g.GameId == existGame.Id).Select(l => new { l.GuessNumber, l.M, l.P, l.Game!.CheckNumber, l.Game.Attempt, l.Game.Number });

        _logger.LogInformation($"===================> {rendom}");

        return gameLog.ToList();
    }

    public static Tuple<int, int> GamePlay(int gessNumber, int rendomeNumber)
    {
        int m = 0;
        int p = 0;
        var inputNumber = gessNumber.ToString();
        var rendom = rendomeNumber.ToString();

        for (int i = 0; i < rendom.Length; i++)
        {
            if (inputNumber[i] == rendom[i])
            {
                p += 1;
            }
            for (int j = 0; j < rendom.Length; j++)
            {
                if (rendom[i] == inputNumber[j])
                {
                    m += 1;
                }
            }
        }

        return Tuple.Create(m, p);
    }
    public static int RandomNumbers()
    {
        int randomDigits = 4;
        int _max = (int)Math.Pow(10, randomDigits);
        Random _rdm = new Random();
        int _out = _rdm.Next(0, _max);

        while (randomDigits != _out.ToString().ToArray().Distinct().Count())
        {
            _out = _rdm.Next(0, _max);
        }
        return _out;
    }

    public User ExistUser(string name)
    {
        var existUser = _unitOfWork.Users.GetAll().FirstOrDefault(u => u.UserName == name);
        if (existUser == null)
        {
            var userModel = new User()
            {
                UserName = name
            };

            _unitOfWork.Users.AddAsync(userModel);
        }
        existUser = _unitOfWork.Users.GetAll().FirstOrDefault(u => u.UserName == name);

        return existUser!;
    }

    public void GameLog(int gameId, int m, int p, int guessNumber)
    {
        var gamelogModel = new GameLog()
        {
            GameId = gameId,
            P = p,
            M = m,
            GuessNumber = guessNumber

        };

        _unitOfWork.GameLogs.AddAsync(gamelogModel);
    }

    public IList GameLeader(int min = 0)
    {
        var userWins = _unitOfWork.Users.GetAll().Where(x => x.Games!.Any(y => y.CheckNumber))
        .Select(x => new User
        {
            Id = x.Id,
            UserName = x.UserName,
            Games = x.Games!.Where(y => y.CheckNumber)
        }).Where(x => x.Games!.Count() >= min).OrderByDescending(x => x.Games!.Count());
       
        return userWins.ToList();
    }
}