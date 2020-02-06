using BizCommon_Core.Models;
using CommonBizModule;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiService.Common;
using WebApiService.Data;

namespace WebApiService.Controllers
{
    /// <summary>
    /// Login 정보를 가져오거나 Login 기능
    /// </summary>
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

            if (!LoginBiz.Login.LoginToDatabase(conItem))
            {
                return BadRequest("연결실패");
            }

            _context.Connections.Add(conItem);

            await _context.SaveChangesAsync();

            ConnectedDB.Instance.SetConnection(conItem);

            //SaveToFile();

            return Ok(conItem);
        }
    }
}