namespace Task.Modeles;

public class Attempt
{
    public int Id { get; set; }
    public bool CheckNumber { get; set; }
    public int TryId { get; set; }
    public EGameState EGameState { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public IEnumerable<Guess>? Guess { get; set; }
}