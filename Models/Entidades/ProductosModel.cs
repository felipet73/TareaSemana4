using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TareaSemana4.Models.Entidades.Base;

namespace TareaSemana4.Models.Entidades
{
    [Table("Productos")]
    public class ProductosModel:BaseModel
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        [Display(Name ="Codigo de Barras")]
        public string Codigo_Barras { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public double Precio { get; set; }
        [Display(Name ="Descripción")]
        public string Descripcion { get; set; }
        public string Notas { get; set; }

    }
}
