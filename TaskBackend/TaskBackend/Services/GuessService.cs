
using Task.Modeles;
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

    public Tuple<int, int> PlayResult(string userName, int number)
    {
        var existUser = _unitOfWork.Users.GetAll().FirstOrDefault(u => u.UserName == userName);
        if (existUser == null)
        {
            var userModel = new User()
            {
                UserName = userName
            };

            _unitOfWork.Users.AddAsync(userModel);
        }

        var guessResult = GamePlay(number);
        var attemptModel = new Attempt();
        attemptModel.UserId = existUser!.Id;

        var existAttempt = _unitOfWork.Attempts.GetAll().FirstOrDefault(a => a.EGameState == EGameState.Continue);

        if (guessResult.Item2 == 4)
        {
            attemptModel.CheckNumber = true;
            attemptModel.EGameState = EGameState.Ended;
        }
        else
        {
            existAttempt!.CheckNumber = false;
            attemptModel.EGameState = EGameState.Continue;
        }

        if (existAttempt == null)
        {
            attemptModel.TryId = 1;

            _unitOfWork.Attempts.AddAsync(attemptModel);
        }
        if (existAttempt!.TryId == 7)
        {
            existAttempt.TryId += 1;
            attemptModel.EGameState = EGameState.Ended;
            _unitOfWork.Attempts.AddAsync(attemptModel);
        }
        else
        {
            existAttempt.TryId += 1;
            _unitOfWork.Attempts.AddAsync(attemptModel);
        }
        
        var GuessNumber = new Guess()
        {
            UserId = existUser.Id,
            GuessNumber = number,
            M = guessResult.Item1,
            P = guessResult.Item2,
        }; 
        
        return Tuple.Create(guessResult.Item1, guessResult.Item2);

    }

    public static Tuple<int, int> GamePlay(int number)
    {
        int m = 0;
        int p = 0;
        var inputNumber = number.ToString();
        var rendomeNumber = RandomNumbers().ToString();

        for (int i = 0; i < rendomeNumber.Length; i++)
        {
            if (inputNumber[i] == rendomeNumber[i])
            {
                p += 1;
            }
            for (int j = 0; j < rendomeNumber.Length; j++)
            {
                if (rendomeNumber[i] == inputNumber[j])
                {
                    m += 1;
                }
            }
        }
        // if (m == 4 && p == 4)
        // {
        //     message = $"rendome => {rendomeNumber} inputNumber => {inputNumber} you find it mos keladigan raqamlar soni => {m} , aniq joyda mos keladigan raqamlar soni => {p} ";
        // }
        // else
        // {
        //     message += $" rendome => {rendomeNumber} inputNumber => {inputNumber} mos keladigan raqamlar soni => {m} , aniq joyda mos keladigan raqamlar soni => {p} ";
        // }


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
}