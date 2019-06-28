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
    public class MetroProductoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public MetroProductoController (ApplicationDbContext context) {
            _context = context;
        }
        [HttpGet]
        public IEnumerable<MetroProducto> GetProducto() {
            return _context.MetroProductos;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducto([FromRoute] int id) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var currentProduct = await _context.MetroProductos.SingleOrDefaultAsync(p => p.Id == id);

            if (currentProduct == null) {
                return NotFound();
            }
            return Ok(currentProduct);
        }

        [HttpPost]
        public async Task<IActionResult> PostProducto([FromBody] MetroProducto producto) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            _context.MetroProductos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducto", new {id = producto.Id}, producto);

        }

        private bool ExisteProducto(int id) {
            return _context.MetroProductos.Any(p => p.Id == id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var currentProduct = await _context.MetroProductos.SingleOrDefaultAsync(p=>p.Id ==id);
            
            if (currentProduct == null) {
                return NotFound();
            }

            _context.MetroProductos.Remove(currentProduct);
            await _context.SaveChangesAsync();

            return Ok(currentProduct);
        }

    }
}
