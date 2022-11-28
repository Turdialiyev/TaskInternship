using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Task.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GuessController : ControllerBase
{
    [HttpPost]
    public ActionResult StartPlay(int number)
    {
        var playResult = GamePlay(number);
        return Ok(playResult.Item3);

    }
    public static Tuple<int, int, string> GamePlay(int number)
    {
        string message = "try again";
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
        if (m == 4 && p == 4)
        {
            message = $"rendome => {rendomeNumber} inputNumber => {inputNumber} you find it mos keladigan raqamlar soni => {m} , aniq joyda mos keladigan raqamlar soni => {p} ";
        }
        else
        {
            message += $" rendome => {rendomeNumber} inputNumber => {inputNumber} mos keladigan raqamlar soni => {m} , aniq joyda mos keladigan raqamlar soni => {p} ";
        }


        return Tuple.Create(m, p, message);
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
