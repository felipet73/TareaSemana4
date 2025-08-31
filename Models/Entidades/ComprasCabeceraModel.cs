using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TareaSemana4.Models.Entidades.Base;

namespace TareaSemana4.Models.Entidades
{
    [Table("ComprasCabecera")]
    public class ComprasCabeceraModel:BaseModel
    {
       
        [Required(ErrorMessage ="El campo es requerido")]
        [Display(Name ="Fecha de Venta")]
        public DateTime FechaCompra { get; set; }
        [Display(Name = "Codigo de venta")]
        public string Codigo_Compra { get; set; }
        public string Notas { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        public double Sub_Total_Compra { get; set; }
        public string Estado_Compra { get; set; }
        public double? Descuento { get; set; }
        public double Total_Compra { get; set; }

        public string Metodo_Pago { get; set; }
        [Display(Name ="ProveedorId")]
        [ForeignKey("ProveedoresModel")]
        public int ProveedorModelId { get; set; }
        public ClientesModel ProveedoresModel { get; set; }
        public ICollection<ComprasDetalleModel> Productos_Comprados { get; set; }

        public ComprasCabeceraModel()
        {
            Productos_Comprados = new List<ComprasDetalleModel>();
        }
    }
}
