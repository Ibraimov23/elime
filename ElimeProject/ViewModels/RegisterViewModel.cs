using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ElimeProject.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Ф.И.О")]
        public string FullName { get; set; }
        [Required]
        [Display(Name = "Наименование компании")]
        public string CompanyName { get; set; }
        [Required]
        [Display(Name = "Ваш телефон")]
        public string Phone { get; set; }
        [Required]
        [Display(Name = "ИНН")]
        public long Inn { get; set; }
        [Required]
        [Display(Name = "ОГРН")]
        public long Ogrn { get; set; }
        [Required]
        [Remote(action: "CheckEmail", controller: "Account", ErrorMessage = "Такая почта уже используется")]
        [Display(Name = "Почта")]
        public string Email { get; set; }

        [StringLength(10, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть от 6 до 10 символов")]
        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
    }
}
