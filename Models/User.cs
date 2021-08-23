using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MinLength(3,ErrorMessage = "Este campo deve conter no mínimo 3 caracteres")]
        [MaxLength(20,ErrorMessage = "Este campo deve conter no máximo 20 caracteres")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MinLength(8,ErrorMessage = "Este campo deve conter no mínimo 8 caracteres")]
        [MaxLength(20,ErrorMessage = "Este campo deve conter no máximo 20 caracteres")]
        public string Password { get; set; }
        
        public string Role { get; set; }
    }
}