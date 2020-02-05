using WebApiService.Data;
using WebApiService.Mssql;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using BizCommon_Core.Models;

namespace WebApiService.Controllers
{
    /// <summary>
    /// 연결 확인용 컨트롤러였으나 Login Controller로 기능 이전
    /// 삭제 예정
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionController : ControllerBase
    {
        private readonly ConnectionContext _context;

        public ConnectionController(ConnectionContext context)
        {
            _context = context;
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

            return Ok(conItem);
        }

        private bool ConnectionItemExists(long id)
        {
            return _context.Connections.Any(e => e.Id == id);
        }

    }
}
