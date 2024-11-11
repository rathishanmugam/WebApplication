namespace WebApplication.Models.Users;

using System.ComponentModel.DataAnnotations;
using WebApplication.Entities;

public class UpdateRequest
{
    public string Title { get; set; }= string.Empty;
    public string FirstName { get; set; }= string.Empty;
    public string LastName { get; set; }= string.Empty;

    [EnumDataType(typeof(Role))]
    public string Role { get; set; }= string.Empty;

    [EmailAddress]
    public string Email { get; set; }= string.Empty;

    // treat empty string as null for password fields to 
    // make them optional in front end apps
    private string _password = string.Empty;
    [MinLength(6)]
    public string Password
    {
        get => _password;
        set => _password = replaceEmptyWithNull(value);
    }

    private string _confirmPassword= string.Empty;
    [Compare("Password")]
    public string ConfirmPassword 
    {
        get => _confirmPassword;
        set => _confirmPassword = replaceEmptyWithNull(value);
    }

    // helpers

    private string replaceEmptyWithNull(string value)
    {
        // replace empty string with null to make field optional
        return string.IsNullOrEmpty(value) ? string.Empty : value;
    }
}