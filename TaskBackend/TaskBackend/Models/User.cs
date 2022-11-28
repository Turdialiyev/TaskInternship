using System.ComponentModel.DataAnnotations;

namespace Task.Modeles;

public class User
{
    public int Id { get; set; }
    [Required]
    public string? UserName { get; set; }
    public IEnumerable<Attempt>? Games { get; set; }
}