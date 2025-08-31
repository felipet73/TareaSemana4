using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TareaSemana4.Models.Entidades.Base;

namespace TareaSemana4.Models.Entidades
{
    [Table("VentasCabecera")]
    public class VentasCabeceraModel:BaseModel
    {
       
        [Required(ErrorMessage ="El campo es requerido")]
        [Display(Name ="Fecha de Venta")]
        public DateTime FechaVenta { get; set; }
        [Display(Name = "Codigo de venta")]
        public string Codigo_Venta { get; set; }
        public string Notas { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        public double Sub_Total_Venta { get; set; }
        public string Estado_Venta { get; set; }
        public double? Descuento { get; set; }
        public double Total_Venta { get; set; }

        public string Metodo_Pago { get; set; }
        [Display(Name ="ClienteId")]
        [ForeignKey("ClientesModel")]
        public int ClientesModelId { get; set; }
        public ClientesModel ClientesModel { get; set; }
        public ICollection<VentasDetalleModel> Productos_Vendidos { get; set; }

        public VentasCabeceraModel()
        {
            Productos_Vendidos = new List<VentasDetalleModel>();
        }
    }
}
