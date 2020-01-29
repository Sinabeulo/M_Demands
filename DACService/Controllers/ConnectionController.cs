using DACService.Data;
using DACService.Models;
using DACService.Mssql;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DACService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionController : ControllerBase
    {
        private readonly ConnectionContext _context;

        public ConnectionController(ConnectionContext context)
        {
            _context = context;
        }

        //// GET: api/Connections
        //[HttpGet]
        //public IEnumerable<ConnectionModel> GetConnections()
        //{
        //    return _context.Connections;
        //}

        //// GET: api/Connections/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetConnections([FromRoute] long id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var conItem = await _context.Connections.FindAsync(id);

        //    if (conItem == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(conItem);
        //}

        //// PUT: api/Connections/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutConnections([FromRoute] long id, [FromBody] ConnectionModel conItem)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != conItem.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(conItem).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ConnectionItemExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Connections
        [HttpPost]
        public async Task<IActionResult> PostConnections([FromBody] ConnectionModel conItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DBManager dBManager = new DBManager();

            if(!dBManager.DbConnection(conItem))
            {
                return BadRequest("연결실패");
            }

            //_context.Connections.Add(conItem);
            //await _context.SaveChangesAsync();

            

            return CreatedAtAction(nameof(ReturnConnection), new { id = conItem.Id }, conItem);
        }

        //// DELETE: api/Connections/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteConnections([FromRoute] long id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var conItem = await _context.Connections.FindAsync(id);
        //    if (conItem == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Connections.Remove(conItem);
        //    await _context.SaveChangesAsync();

        //    return Ok(conItem);
        //}

        private bool ConnectionItemExists(long id)
        {
            return _context.Connections.Any(e => e.Id == id);
        }

        private async Task<ConnectionModel> ReturnConnection(long id)
        {
            var conItem = await _context.Connections.FindAsync(id);

            if (conItem == null)
            {
                return null;//NotFound();
            }

            return conItem;//Ok(conItem);
            //return _context.Connections[id];
        }
    }
}
