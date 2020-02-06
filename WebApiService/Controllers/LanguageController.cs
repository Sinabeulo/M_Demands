using System.Threading.Tasks;
using BizCommon_Std.Models;
using CommonBizModule;
using Microsoft.AspNetCore.Mvc;
using WebApiService.Common;
using WebApiService.Data;

namespace WebApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly LanguageContext _context;

        public LanguageController(LanguageContext context)
        {
            _context = context;
        }

        // POST: api/Language
        [HttpPost]
        public async Task<IActionResult> PostSetLanguage([FromBody] LanguageControlModel conItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!(ConnectedDB.Instance.GetConnection(conItem.TargetTitle) is ConnectionModel con))
            {
                return BadRequest("연결된 항목 없음");
            }

            var retDt = LanguageBiz.BizInstance.SetLanguage(con, conItem);

            //if (!dBManager.DbConnection(conItem))
            //{
            //    return BadRequest("연결실패");
            //}

            return Ok(retDt);
        }
    }
}