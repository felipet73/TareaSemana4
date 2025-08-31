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
    public class ComprasCabeceraController : Controller
    {
        private readonly BaseDbContext _context;

        public ComprasCabeceraController(BaseDbContext context)
        {
            _context = context;
        }

        // GET: ComprasCabecera
        public async Task<IActionResult> Index()
        {
            var baseDbContext = _context.ComprasCabecera.Include(c => c.ProveedoresModel);
            return View(await baseDbContext.ToListAsync());
        }

        // GET: ComprasCabecera/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comprasCabeceraModel = await _context.ComprasCabecera
                .Include(c => c.ProveedoresModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comprasCabeceraModel == null)
            {
                return NotFound();
            }

            return View(comprasCabeceraModel);
        }

        // GET: ComprasCabecera/Create
        public IActionResult Create()
        {
            ViewData["ProveedorModelId"] = new SelectList(_context.Clientes, "Id", "Cedula_RUC");
            return View();
        }

        // POST: ComprasCabecera/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FechaCompra,Codigo_Compra,Notas,Sub_Total_Compra,Estado_Compra,Descuento,Total_Compra,Metodo_Pago,ProveedorModelId,Id,Create_At,Update_At,isDelete")] ComprasCabeceraModel comprasCabeceraModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comprasCabeceraModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProveedorModelId"] = new SelectList(_context.Clientes, "Id", "Cedula_RUC", comprasCabeceraModel.ProveedorModelId);
            return View(comprasCabeceraModel);
        }

        // GET: ComprasCabecera/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comprasCabeceraModel = await _context.ComprasCabecera.FindAsync(id);
            if (comprasCabeceraModel == null)
            {
                return NotFound();
            }
            ViewData["ProveedorModelId"] = new SelectList(_context.Clientes, "Id", "Cedula_RUC", comprasCabeceraModel.ProveedorModelId);
            return View(comprasCabeceraModel);
        }

        // POST: ComprasCabecera/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FechaCompra,Codigo_Compra,Notas,Sub_Total_Compra,Estado_Compra,Descuento,Total_Compra,Metodo_Pago,ProveedorModelId,Id,Create_At,Update_At,isDelete")] ComprasCabeceraModel comprasCabeceraModel)
        {
            if (id != comprasCabeceraModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comprasCabeceraModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComprasCabeceraModelExists(comprasCabeceraModel.Id))
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
            ViewData["ProveedorModelId"] = new SelectList(_context.Clientes, "Id", "Cedula_RUC", comprasCabeceraModel.ProveedorModelId);
            return View(comprasCabeceraModel);
        }

        // GET: ComprasCabecera/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comprasCabeceraModel = await _context.ComprasCabecera
                .Include(c => c.ProveedoresModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comprasCabeceraModel == null)
            {
                return NotFound();
            }

            return View(comprasCabeceraModel);
        }

        // POST: ComprasCabecera/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comprasCabeceraModel = await _context.ComprasCabecera.FindAsync(id);
            if (comprasCabeceraModel != null)
            {
                _context.ComprasCabecera.Remove(comprasCabeceraModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComprasCabeceraModelExists(int id)
        {
            return _context.ComprasCabecera.Any(e => e.Id == id);
        }
    }
}
