using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiFranquicia.Domain;
using ApiFranquicia.Repository.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiFranquicia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TottusProductoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public TottusProductoController (ApplicationDbContext context) {
            _context = context;
        }
        [HttpGet]
        public IEnumerable<TottusProducto> GetProducto() {
            return _context.TottusProductos;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducto([FromRoute] int id) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var currentProduct = await _context.TottusProductos.SingleOrDefaultAsync(p => p.Id == id);

            if (currentProduct == null) {
                return NotFound();
            }
            return Ok(currentProduct);
        }

        [HttpPost]
        public async Task<IActionResult> PostProducto([FromBody] TottusProducto producto) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            _context.TottusProductos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducto", new {id = producto.Id}, producto);

        }

        private bool ExisteProducto(int id) {
            return _context.TottusProductos.Any(p => p.Id == id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var currentProduct = await _context.TottusProductos.SingleOrDefaultAsync(p=>p.Id ==id);
            
            if (currentProduct == null) {
                return NotFound();
            }

            _context.TottusProductos.Remove(currentProduct);
            await _context.SaveChangesAsync();

            return Ok(currentProduct);
        }

    }
}
