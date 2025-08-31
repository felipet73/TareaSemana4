using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TareaSemana4.Data;
using TareaSemana4.Models.Entidades;

namespace TareaSemana4.Controllers
{
    public class VentasCabeceraController : Controller
    {
        private readonly BaseDbContext _context;

        public VentasCabeceraController(BaseDbContext context)
        {
            _context = context;
        }

        // GET: VentasCabecera
        public async Task<IActionResult> Index()
        {
            var baseDbContext = _context.VentasCabecera.Include(v => v.ClientesModel);
            return View(await baseDbContext.ToListAsync());
        }

        // GET: VentasCabecera/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventasCabeceraModel = await _context.VentasCabecera
                .Include(v => v.ClientesModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ventasCabeceraModel == null)
            {
                return NotFound();
            }

            return View(ventasCabeceraModel);
        }

        // GET: VentasCabecera/Create
        public IActionResult Create()
        {
            ViewData["ClientesModelId"] = new SelectList(_context.Clientes, "Id", "Cedula_RUC");
            return View();
        }

        // POST: VentasCabecera/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FechaVenta,Codigo_Venta,Notas,Sub_Total_Venta,Estado_Venta,Descuento,Total_Venta,Metodo_Pago,ClientesModelId,Id,Create_At,Update_At,isDelete")] VentasCabeceraModel ventasCabeceraModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ventasCabeceraModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientesModelId"] = new SelectList(_context.Clientes, "Id", "Cedula_RUC", ventasCabeceraModel.ClientesModelId);
            return View(ventasCabeceraModel);
        }

        // GET: VentasCabecera/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventasCabeceraModel = await _context.VentasCabecera.FindAsync(id);
            if (ventasCabeceraModel == null)
            {
                return NotFound();
            }
            ViewData["ClientesModelId"] = new SelectList(_context.Clientes, "Id", "Cedula_RUC", ventasCabeceraModel.ClientesModelId);
            return View(ventasCabeceraModel);
        }

        // POST: VentasCabecera/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FechaVenta,Codigo_Venta,Notas,Sub_Total_Venta,Estado_Venta,Descuento,Total_Venta,Metodo_Pago,ClientesModelId,Id,Create_At,Update_At,isDelete")] VentasCabeceraModel ventasCabeceraModel)
        {
            if (id != ventasCabeceraModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ventasCabeceraModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentasCabeceraModelExists(ventasCabeceraModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientesModelId"] = new SelectList(_context.Clientes, "Id", "Cedula_RUC", ventasCabeceraModel.ClientesModelId);
            return View(ventasCabeceraModel);
        }

        // GET: VentasCabecera/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventasCabeceraModel = await _context.VentasCabecera
                .Include(v => v.ClientesModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ventasCabeceraModel == null)
            {
                return NotFound();
            }

            return View(ventasCabeceraModel);
        }

        // POST: VentasCabecera/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ventasCabeceraModel = await _context.VentasCabecera.FindAsync(id);
            if (ventasCabeceraModel != null)
            {
                _context.VentasCabecera.Remove(ventasCabeceraModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentasCabeceraModelExists(int id)
        {
            return _context.VentasCabecera.Any(e => e.Id == id);
        }
    }
}
