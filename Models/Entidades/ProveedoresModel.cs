using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TareaSemana4.Models.Entidades.Base;

namespace TareaSemana4.Models.Entidades
{
    [Table("Proveedores")]
    public class ProveedoresModel:BaseModel
    {
        [Required(ErrorMessage = "Cammpo Requerido")]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "Cammpo Requerido")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Cammpo Requerido")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "Cammpo Requerido")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "Cammpo Requerido")]
        public string Cedula_RUC { get; set; }
    }
}
