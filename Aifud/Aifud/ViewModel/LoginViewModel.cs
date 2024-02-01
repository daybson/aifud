using Microsoft.AspNetCore.Authentication;

using System.ComponentModel.DataAnnotations;

namespace Aifud.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Informe o email")]
        [Display(Name ="Email")]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set;}

        [Required(ErrorMessage ="Informe a senha")]
        [DataType (DataType.Password)]
        [Display(Name ="Senha")]
        public string Password { get; set;}

        public string? ReturnUrl { get; set;}

        public IList<AuthenticationScheme> ExternalLogins { get; set; }
    }
}
