using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TareaSemana4.Models.Entidades.Base;

namespace TareaSemana4.Models.Entidades
{
    [Table("VentasDetalle")]
    public class VentasDetalleModel: BaseModel
    {
        [Required]
        public int ProductosModelId { get; set; }
        public ProductosModel ProductosModel { get; set; }

        public string Nombre { get; set; }

        [Required]
        public double Precio { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public double Monto { get; set; }

        [Display(Name ="Ventas")]
        [ForeignKey("VentasModel")]
        public int VentasCabeceraModelId { get; set; }
        [JsonIgnore]
        public VentasCabeceraModel VentasCabeceraModel { get; set; }
    }
}
