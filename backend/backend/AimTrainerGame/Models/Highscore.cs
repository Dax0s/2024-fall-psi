using System.ComponentModel.DataAnnotations;

namespace backend.AimTrainerGame.Models;

public class Highscore
{
    [Key]
    public Guid Id { get; set; }
    public string Username { get; set; }
    public int Score { get; set; }
    public DateTime Date { get; set; }
}
