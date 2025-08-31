using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TareaSemana4.Data;
using TareaSemana4.Models.Entidades;

namespace TareaSemana4.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasCabeceraApiController : ControllerBase
    {
        private readonly BaseDbContext _context;

        public VentasCabeceraApiController(BaseDbContext context)
        {
            _context = context;
        }

        // GET: api/VentasCabeceraApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VentasCabeceraModel>>> GetVentasCabecera()
        {
            return await _context.VentasCabecera.ToListAsync();
        }

        // GET: api/VentasCabeceraApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VentasCabeceraModel>> GetVentasCabeceraModel(int id)
        {
            var ventasCabeceraModel = await _context.VentasCabecera.FindAsync(id);

            if (ventasCabeceraModel == null)
            {
                return NotFound();
            }

            return ventasCabeceraModel;
        }

        // PUT: api/VentasCabeceraApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVentasCabeceraModel(int id, VentasCabeceraModel ventasCabeceraModel)
        {
            if (id != ventasCabeceraModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(ventasCabeceraModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VentasCabeceraModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/VentasCabeceraApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VentasCabeceraModel>> PostVentasCabeceraModel(VentasCabeceraModel ventasCabeceraModel)
        {
            _context.VentasCabecera.Add(ventasCabeceraModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVentasCabeceraModel", new { id = ventasCabeceraModel.Id }, ventasCabeceraModel);
        }

        // DELETE: api/VentasCabeceraApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVentasCabeceraModel(int id)
        {
            var ventasCabeceraModel = await _context.VentasCabecera.FindAsync(id);
            if (ventasCabeceraModel == null)
            {
                return NotFound();
            }

            _context.VentasCabecera.Remove(ventasCabeceraModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VentasCabeceraModelExists(int id)
        {
            return _context.VentasCabecera.Any(e => e.Id == id);
        }
    }
}
