using System.ComponentModel.DataAnnotations;

namespace PEACE.api.Models;

public class LoginAttempt
{
    [Key]
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public int FailedCount { get; set; }
    public DateTime LastAttempt { get; set; }
    public DateTime? LockoutEnd { get; set; }

}