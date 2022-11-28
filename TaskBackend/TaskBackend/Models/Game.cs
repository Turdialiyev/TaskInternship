namespace Task.Modeles;

public class Game
{
    public int Id { get; set; }
    public bool CheckNumber { get; set; }
    public int Attempt { get; set; }
    public int Number { get; set; }
    public EGameState EGameState { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
}