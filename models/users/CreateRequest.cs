namespace WebApplication.Models.Users;

using System.ComponentModel.DataAnnotations;
using WebApplication.Entities;

public class CreateRequest
{
    [Required]
    public string Title { get; set; }= string.Empty;

    [Required]
    public string FirstName { get; set; }= string.Empty;

    [Required]
    public string LastName { get; set; }= string.Empty;

    [Required]
    [EnumDataType(typeof(Role))]
    public string Role { get; set; }= string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; }= string.Empty;

    [Required]
    [MinLength(6)]
    public string Password { get; set; }= string.Empty;

    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }= string.Empty;
}