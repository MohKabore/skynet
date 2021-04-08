using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }


        [Required]
        public string Email { get; set; }


        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$",
        ErrorMessage = "Cette correspondance d'expression régulière peut être utilisée pour valider un mot de passe fort. Il attend au" +
        " moins 1 lettre minuscule, 1 lettre majuscule, 1 chiffre, 1 caractère spécial et la longueur doit être comprise entre 6 et 10" +
        " caractères. L'ordre des caractères n'est pas important. Cette expression suit les 4 normes ci-dessus spécifiées par Microsoft" +
        " pour un mot de passe fort.")]
        public string Password { get; set; }
    }
}