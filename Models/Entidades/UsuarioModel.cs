using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Timers;
using TareaSemana4.Models.Entidades.Base;

namespace TareaSemana4.Models.Entidades
{
    [Table("Usuarios")]
    public class UsuarioModel: BaseModel
    {
        [EmailAddress(ErrorMessage ="El formato no de correo electronico")]
        [Required(ErrorMessage ="El campo es requerido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        public string Password { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        public  string ConfirmPassword { get; set; }
    }
}
