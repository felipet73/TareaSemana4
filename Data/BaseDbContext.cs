using TareaSemana4.Models.Entidades;
using Microsoft.EntityFrameworkCore;

namespace TareaSemana4.Data
{    
    public class BaseDbContext:DbContext
    {
        public BaseDbContext(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<ClientesModel> Clientes { get; set; }
        public DbSet<ProveedoresModel> Proveedores { get; set; }
        public DbSet<ProductosModel> Productos { get; set; }
        public DbSet<ComprasCabeceraModel> ComprasCabecera { get; set; }
        public DbSet<ComprasDetalleModel> ComprasDetalle { get; set; }
        public DbSet<VentasCabeceraModel> VentasCabecera { get; set; }
        public DbSet<VentasDetalleModel> VentasDetalle { get; set; }
    }
}
