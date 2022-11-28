namespace Task.Modeles;

public class Guess
{
    public int Id { get; set; }
    public int GuessNumber { get; set; }
    public int M { get; set; }
    public int P { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
}