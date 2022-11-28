namespace Task.Services;
public interface IGuessService
{
    public Tuple<int, int> PlayResult(string userName, int number);

}