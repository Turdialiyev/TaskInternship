namespace Task.Modeles;

public class Attempt
{
    public int Id { get; set; }
    public bool Check { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public IEnumerable<Guess>? Guess { get; set; }
}