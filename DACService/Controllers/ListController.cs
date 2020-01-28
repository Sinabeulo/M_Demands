using DACService.Data;
using DACService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DACService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly ListContext _context;

        public ListController(ListContext context)
        {
            _context = context;
        }

        // GET: api/List
        [HttpGet]
        public IEnumerable<ConnectionModel> GetConnectionList()
        {
            return _context.ConnectionList;
        }

        // GET: api/List/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConnectionList([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var conItem = await _context.ConnectionList.FindAsync(id);

            if (conItem == null)
            {
                return NotFound();
            }

            return Ok(conItem);
        }

        // PUT: api/List/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConnectionList([FromRoute] long id, [FromBody] ConnectionModel conItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != conItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(conItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConnectionItemExists(id))
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

        // POST: api/List
        [HttpPost]
        public async Task<IActionResult> PostConnectionList([FromBody] ConnectionModel conItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (conItem != null) 
                conItem.Password = null;


            if(_context.ConnectionList.Any(a=>a.DataSource == conItem.DataSource))
            {
                return BadRequest("이미 있음");
            }
            _context.ConnectionList.Add(conItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetConnectionList), new { id = conItem.Id }, conItem);
        }

        // DELETE: api/List/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConnectionList([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var conItem = await _context.ConnectionList.FindAsync(id);
            if (conItem == null)
            {
                return NotFound();
            }

            _context.ConnectionList.Remove(conItem);
            await _context.SaveChangesAsync();

            return Ok(conItem);
        }

        private bool ConnectionItemExists(long id)
        {
            return _context.ConnectionList.Any(e => e.Id == id);
        }
    }
}
