using System.ComponentModel.DataAnnotations;

namespace PeaceApi.DTOs
{
 
        public class RegisterNutricionistaDTO
        {
            [Required(ErrorMessage = "O nome é obrigatório.")]
            public string? NomeCompleto { get; set; }

            [Required(ErrorMessage = "O e-mail é obrigatório.")]
            [EmailAddress]
            public string? Email { get; set; }

            [Required(ErrorMessage = "A senha é obrigatória.")]
            [MinLength(6)]
            public string? Password { get; set; }

            [Required(ErrorMessage = "O CRN é obrigatório.")]
            public string? CRN { get; set; }


        }
    }
