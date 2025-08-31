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
    public class ComprasCabeceraApiController : ControllerBase
    {
        private readonly BaseDbContext _context;

        public ComprasCabeceraApiController(BaseDbContext context)
        {
            _context = context;
        }

        // GET: api/ComprasCabeceraApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComprasCabeceraModel>>> GetComprasCabecera()
        {
            return await _context.ComprasCabecera.ToListAsync();
        }

        // GET: api/ComprasCabeceraApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ComprasCabeceraModel>> GetComprasCabeceraModel(int id)
        {
            var comprasCabeceraModel = await _context.ComprasCabecera.FindAsync(id);

            if (comprasCabeceraModel == null)
            {
                return NotFound();
            }

            return comprasCabeceraModel;
        }

        // PUT: api/ComprasCabeceraApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComprasCabeceraModel(int id, ComprasCabeceraModel comprasCabeceraModel)
        {
            if (id != comprasCabeceraModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(comprasCabeceraModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComprasCabeceraModelExists(id))
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

        // POST: api/ComprasCabeceraApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ComprasCabeceraModel>> PostComprasCabeceraModel(ComprasCabeceraModel comprasCabeceraModel)
        {
            _context.ComprasCabecera.Add(comprasCabeceraModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComprasCabeceraModel", new { id = comprasCabeceraModel.Id }, comprasCabeceraModel);
        }

        // DELETE: api/ComprasCabeceraApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComprasCabeceraModel(int id)
        {
            var comprasCabeceraModel = await _context.ComprasCabecera.FindAsync(id);
            if (comprasCabeceraModel == null)
            {
                return NotFound();
            }

            _context.ComprasCabecera.Remove(comprasCabeceraModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComprasCabeceraModelExists(int id)
        {
            return _context.ComprasCabecera.Any(e => e.Id == id);
        }
    }
}
