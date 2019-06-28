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
    public class WongProductoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public WongProductoController (ApplicationDbContext context) {
            _context = context;
        }
        [HttpGet]
        public IEnumerable<WongProducto> GetProducto() {
            return _context.WongProductos;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducto([FromRoute] int id) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var currentProduct = await _context.WongProductos.SingleOrDefaultAsync(p => p.Id == id);

            if (currentProduct == null) {
                return NotFound();
            }
            return Ok(currentProduct);
        }

        [HttpPost]
        public async Task<IActionResult> PostProducto([FromBody] WongProducto producto) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            _context.WongProductos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducto", new {id = producto.Id}, producto);

        }

        private bool ExisteProducto(int id) {
            return _context.WongProductos.Any(p => p.Id == id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var currentProduct = await _context.WongProductos.SingleOrDefaultAsync(p=>p.Id ==id);
            
            if (currentProduct == null) {
                return NotFound();
            }

            _context.WongProductos.Remove(currentProduct);
            await _context.SaveChangesAsync();

            return Ok(currentProduct);
        }

    }
}
