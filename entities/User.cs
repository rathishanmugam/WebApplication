namespace WebApplication.Entities;

using System.Text.Json.Serialization;

public class User
{
    public int Id { get; set; }
    public required string Title { get; set; } 
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public Role Role { get; set; }

    [JsonIgnore]
    public string PasswordHash { get; set; }= string.Empty;
}