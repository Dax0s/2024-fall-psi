using System.ComponentModel.DataAnnotations;

namespace backend.MathGame.Models;

public class Puzzle
{
    [Key]
    public Guid Id { get; set; }
    public string? Content { get; set; }
}
