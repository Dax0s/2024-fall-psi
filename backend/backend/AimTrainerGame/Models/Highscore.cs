using System.ComponentModel.DataAnnotations;

namespace backend.AimTrainerGame.Models;

public class Highscore
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string? Username { get; set; }

    [Range(0, int.MaxValue)]
    public int Score { get; set; }

    public DateTime Date { get; set; }
}
