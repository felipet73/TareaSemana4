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
    public class UsuarioApiController : ControllerBase
    {
        private readonly BaseDbContext _context;

        public UsuarioApiController(BaseDbContext context)
        {
            _context = context;
        }

        // GET: api/UsuarioApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioModel>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/UsuarioApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioModel>> GetUsuarioModel(int id)
        {
            var usuarioModel = await _context.Usuarios.FindAsync(id);

            if (usuarioModel == null)
            {
                return NotFound();
            }

            return usuarioModel;
        }

        // PUT: api/UsuarioApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarioModel(int id, UsuarioModel usuarioModel)
        {
            if (id != usuarioModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(usuarioModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioModelExists(id))
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

        // POST: api/UsuarioApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UsuarioModel>> PostUsuarioModel(UsuarioModel usuarioModel)
        {
            _context.Usuarios.Add(usuarioModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuarioModel", new { id = usuarioModel.Id }, usuarioModel);
        }

        // DELETE: api/UsuarioApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuarioModel(int id)
        {
            var usuarioModel = await _context.Usuarios.FindAsync(id);
            if (usuarioModel == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuarioModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioModelExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
