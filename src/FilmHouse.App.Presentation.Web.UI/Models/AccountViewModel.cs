using FilmHouse.Core.DataAnnotations;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Localization;
using SysDataAnnotations = System.ComponentModel.DataAnnotations;

namespace FilmHouse.App.Presentation.Web.UI.Models;

/// <summary>
/// 登錄頁面
/// </summary>
public class LoginViewModel
{
    [SysDataAnnotations.Display(Name = nameof(Resources.UserName), ResourceType = typeof(Resources))]
    [Required]
    public AccountNameVO Account { get; set; }

    [SysDataAnnotations.Display(Name = nameof(Resources.Password), ResourceType = typeof(Resources))]
    [SysDataAnnotations.DataType(SysDataAnnotations.DataType.Password)]
    [Required]
    public PasswordHashVO Password { get; set; }

    public IsAdminVO IsAdmin { get; set; }

    public UserIdVO Id { get; set; }
}

/// <summary>
/// 註冊頁面
/// </summary>
public class RegisterViewModel
{
    [SysDataAnnotations.Display(Name = nameof(Resources.UserName), ResourceType = typeof(Resources))]
    [StringLength(20)]
    [Required]
    public AccountNameVO Account { get; set; }

    [SysDataAnnotations.Display(Name = nameof(Resources.Password), ResourceType = typeof(Resources))]
    [SysDataAnnotations.DataType(SysDataAnnotations.DataType.Password)]
    [InputPasswordVO]
    [Required]
    public PasswordHashVO Password { get; set; }

    [SysDataAnnotations.Display(Name = nameof(Resources.ConfirmPassword), ResourceType = typeof(Resources))]
    [SysDataAnnotations.DataType(SysDataAnnotations.DataType.Password)]
    [PasswordCompare("Password")]
    public ConfirmPasswordVO ConfirmPassword { get; set; }
}

/// <summary>
/// 忘記密碼
/// </summary>
public class ForgotPasswordViewModel
{
    [SysDataAnnotations.Display(Name = nameof(Resources.UserName), ResourceType = typeof(Resources))]
    [Required]
    public AccountNameVO Account { get; set; }

    [SysDataAnnotations.Display(Name = nameof(Resources.EMail), ResourceType = typeof(Resources))]
    [MailAddressVO]
    [Required]
    public EmailAddressVO Email { get; set; }
}

/// <summary>
/// 密碼重置頁面
/// </summary>
public class ResetPasswordViewModel
{
    [SysDataAnnotations.Display(Name = nameof(Resources.UserName), ResourceType = typeof(Resources))]
    [Required]
    public AccountNameVO Account { get; set; }

    [SysDataAnnotations.Display(Name = nameof(Resources.NewPassword), ResourceType = typeof(Resources))]
    [SysDataAnnotations.DataType(SysDataAnnotations.DataType.Password)]
    [InputPasswordVO]
    [Required]
    public PasswordHashVO Password { get; set; }

    [SysDataAnnotations.Display(Name = nameof(Resources.ConfirmPassword), ResourceType = typeof(Resources))]
    [SysDataAnnotations.DataType(SysDataAnnotations.DataType.Password)]
    [SysDataAnnotations.Compare("Password", ErrorMessageResourceName = nameof(Resources.ValidationConfirmPassword), ErrorMessageResourceType = typeof(Resources))]
    public ConfirmPasswordVO ConfirmPassword { get; set; }
}