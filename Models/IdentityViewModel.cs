using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace communicator.Models;

/// <summary>
/// The view model for the register model.
/// </summary>

public class RegisterViewModel
{
    [Required]
    [BindRequired]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required]
    [BindRequired]
    public string UserName { get; set; }
    [Required]
    [BindRequired]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }
    [Required]
    [BindingBehavior(BindingBehavior.Required)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; }

}

/// <summary>
/// This is loginViewModel which use in login page......
/// </summary>
public class LoginViewModel
{
    [Required]
    [BindRequired]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required]
    [BindRequired]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }
    [Required]
    [BindRequired]
    [Display(Name = "Remember me")]
    public bool RememberMe { get; set; }

}  