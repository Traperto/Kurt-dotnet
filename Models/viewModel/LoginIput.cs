using System.ComponentModel.DataAnnotations;

public class LoginInput
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}