using System.ComponentModel.DataAnnotations;

namespace Aifud.ViewModel
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "Informe o email")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirme a senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        [Compare("Password", ErrorMessage = "As senhas não são iguais")]
        public string PasswordConfirm { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
