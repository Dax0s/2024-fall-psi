using System.ComponentModel.DataAnnotations;

namespace backend.ReactionTimeGame.Models;

public class ReactionTimeGameScore
{
    [Key]
    public required Guid Id { get; set; }

    [Required(ErrorMessage = "Score username is required")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Invalid username length")]
    public required string Username { get; set; }

    [Required(ErrorMessage = "Score value is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Invalid score value")]
    public required int Value { get; set; }

    [Required(ErrorMessage = "Score date is required")]
    public required DateTime Date { get; set; }
}
