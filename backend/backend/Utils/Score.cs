using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace backend.Utils;

public class Score<T>
where T : notnull
{
    [Key]
    public required Guid Id { get; set; }
    public required string Username { get; set; }
    public required T Value { get; set; }
    public required DateTime Date { get; set; }

    [SetsRequiredMembers]
    public Score(string username, T score)
    {
        Id = Guid.NewGuid();
        Username = username;
        Value = score;
        Date = DateTime.UtcNow;
    }
}
