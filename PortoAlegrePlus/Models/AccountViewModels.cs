using System.ComponentModel.DataAnnotations;

namespace PortoAlegrePlus.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "E-mail")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name = "Lembrar-me")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [StringLength(15, ErrorMessage = "O campo {0} deve ser de pelo menos {2} e no máximo {1} caracteres.", MinimumLength = 4)]
        [Display(Name = "Nome")]
        public string NomeUsuario { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "O campo {0} deve ser de pelo menos {2} e no máximo {1} caracteres.", MinimumLength = 3)]
        [Display(Name = "Sobrenome")]
        public string SobrenomeUsuario { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O campo {0} deve ser de pelo menos {2} caracteres.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [Compare("Password", ErrorMessage = "O e-mail e a senha que você digitou não coincidem.")]
        public string ConfirmPassword { get; set; }
    }
}
