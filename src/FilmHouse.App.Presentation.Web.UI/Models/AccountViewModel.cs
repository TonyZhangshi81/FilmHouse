using System.ComponentModel.DataAnnotations;
using Microsoft.Identity.Client;
using FilmHouse.Core.ValueObjects;

namespace FilmHouse.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "请输入 用户名。")]
        [Display(Name = "用户名")]
        public AccountNameVO Account { get; set; }

        [Required(ErrorMessage = "请输入 密码。")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public PasswordVO Password { get; set; }

        public IsAdminVO IsAdmin { get; set; }

        public UserIdVO Id { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "请输入 用户名。")]
        [Display(Name = "用户名")]
        [StringLength(20, ErrorMessage = "{0} 必须少于 {1} 个字符。")]
        public AccountNameVO Account { get; set; }

        [Required(ErrorMessage = "请输入 密码。")]
        [RegularExpression(@"^(?=.*\d.*)(?=.*[a-zA-Z].*).{6,}$", ErrorMessage = "密码 必须包括字符和数字，且长度不小于6")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public PasswordVO Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码 和 确认密码 不匹配。")]
        public ConfirmPasswordVO ConfirmPassword { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "请输入 用户名。")]
        [Display(Name = "用户名")]
        public AccountNameVO Account { get; set; }

        [Required(ErrorMessage = "请输入 电子邮件。")]
        [EmailAddress(ErrorMessage = "请输入正确的 电子邮件。")]
        [Display(Name = "电子邮件")]
        public EmailAddressVO Email { get; set; }
    }

    public class ResetPasswordViewModel
    {
        //[Required(ErrorMessage = "请输入 用户名。")]
        [Display(Name = "用户名")]
        public AccountNameVO Account { get; set; }

        [Required(ErrorMessage = "请输入 新密码。")]
        [RegularExpression(@"^(?=.*\d.*)(?=.*[a-zA-Z].*).{6,}$", ErrorMessage = "密码 必须包括字符和数字，且长度不小于6")]
        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        public PasswordVO Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码和确认密码不匹配。")]
        public ConfirmPasswordVO ConfirmPassword { get; set; }
    }

    //
    // 摘要:
    //     登录尝试的可能结果
    public enum SignInStatus
    {
        //
        // 摘要:
        //     登陆成功
        Success = 0,
        //
        // 摘要:
        //     用户名不存在
        UndefinedAccount = 1,
        //
        // 摘要:
        //     登陆失败
        Failure = 2
    }
}