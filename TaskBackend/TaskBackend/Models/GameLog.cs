namespace Task.Modeles;

public class GameLog
{
    public int Id { get; set; }
    public int GuessNumber { get; set; }
    public int M { get; set; }
    public int P { get; set; }
    public int GameId { get; set; }
    public Game? Game { get; set; }
}