using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NOC_DKRKM.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Адреса електронної пошти")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Код")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Запам'ятати браузер?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Адреса електронної пошти")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Адреса електронної пошти")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запам'ятати мене")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        //МОИ ПОЛЯ
        [Display(Name="Ім'я")]
        public string name { get; set; }

        [Display(Name ="Прізвище")]
        public string sname { get; set; }

        [Display(Name ="По батькові")]
        public string lname { get; set; }

        public int GroupID { get; set; }

        public virtual Group Group { get; set; }


        //КОНЕЦ МОИХ ПОЛЕЙ
        [Required]
        [EmailAddress]
        [Display(Name = "Адреса електронної пошти")]
        public string Email { get; set; }

        //[Required]
        //[StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //[Display(Name = "Пароль")]
        //public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "Подтверждение пароля")]
        //[Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        //public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Адреса електронної пошти")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Значення {0} повинно містити не менше {2} символів.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Підтвердження пароля")]
        [Compare("Password", ErrorMessage = "Пароль і його підтвердження не збігаються.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Пошта")]
        public string Email { get; set; }
    }
}
