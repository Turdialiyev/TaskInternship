
using System.Collections;
using Task.Modeles;
using System.Linq;
using Task.Repositories;
using Task.Models;
using Microsoft.EntityFrameworkCore;

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

    public async ValueTask<Result<Game>> PlayResult(int userId, int number)
    {
        var checkedUser = _unitOfWork.Users.GetById(userId);
        if (checkedUser is null)
            return new(false, "User with this id was not found");

        if (number.ToString().Length != 4)
            return new(false, "You entered an incorrect number, please enter only 4 digits");

        var rendom = RandomNumbers();
        var guessResult = GamePlay(number, rendom);
        var attemptModel = new Game();

        attemptModel.UserId = checkedUser.Id;

        var existGame = _unitOfWork.Games.GetAll().Where(u => u.UserId == userId).FirstOrDefault(u => u.EGameState == EGameState.Continue);

        if (existGame != null && existGame!.Attempt == 7)
        {
            rendom = existGame.Number;
            guessResult = GamePlay(number, rendom);
            existGame.Attempt += 1;
            existGame.EGameState = EGameState.Ended;
            if (guessResult.Item2 == 4)
                existGame.CheckNumber = true;

            await _unitOfWork.Games.Update(existGame);
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

            await _unitOfWork.Games.Update(existGame);
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

            existGame = await _unitOfWork.Games.AddAsync(attemptModel);
        }

        GameLog(existGame!.Id, guessResult.Item1, guessResult.Item2, number);

        var gameLog = await _unitOfWork.GameLogs.GetAll().Where(g => g.GameId == existGame.Id).Select(l => new { l.GuessNumber, l.M, l.P, l.Game!.CheckNumber, l.Game.Attempt, l.Game.Number }).ToListAsync();

        _logger.LogInformation($"===================> {rendom}");

        return new(true) {Data = existGame};
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

    public async ValueTask<Result<User>> ExistUser(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return new(false, "Name invalid");

        var existUser = _unitOfWork.Users.GetAll().FirstOrDefault(u => u.UserName == name);

        if (existUser == null)
        {
            var userModel = new User()
            {
                UserName = name
            };

            existUser = await _unitOfWork.Users.AddAsync(userModel);
        }

        return new(true) { Data = existUser };
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

    public async ValueTask<Result<List<User>>> GameLeader(int min = 0)
    {
        var users = await _unitOfWork.Users.GetAll()
        .Where(u => u.Games!.Any(g => g.CheckNumber))
        .Where(x => x.Games!.Count() >= min).OrderByDescending(g => g.Games!.Count()).ToListAsync();

        return new(true) { Data = users };
    }
}