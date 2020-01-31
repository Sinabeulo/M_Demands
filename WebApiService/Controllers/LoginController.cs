using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiService.Data;
using WebApiService.Models;
using WebApiService.Mssql;

namespace WebApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginContext _context;

        public LoginController(LoginContext context)
        {
            _context = context;
        }

        // GET: api/Connections
        [HttpGet]
        public IEnumerable<ConnectionModel> GetConnections()
        {
            return _context.Connections.Select(s => new ConnectionModel
            {
                DataSource = s.DataSource,
                InitialCatalog = s.InitialCatalog,
                Id = s.Id,
                Title = s.Title,
                UserID = s.UserID
            });
        }

        // POST: api/Connections
        [HttpPost]
        public async Task<IActionResult> PostConnections([FromBody] ConnectionModel conItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DBManager dBManager = new DBManager();

            if (!dBManager.DbConnection(conItem))
            {
                return BadRequest("연결실패");
            }

            _context.Connections.Add(conItem);
            await _context.SaveChangesAsync();

            return Ok(conItem);
        }
    }
}