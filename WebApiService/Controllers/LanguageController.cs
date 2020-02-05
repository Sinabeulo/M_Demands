using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BizCommon_Core.Models;
using CommonBizModule;
using Microsoft.AspNetCore.Http;
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

        // POST: api/Connections
        [HttpPost]
        public async Task<IActionResult> PostConnections([FromBody] LanguageControlModel conItem, [FromBody]string title)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!(ConnectedDB.Instance.GetConnection(title) is ConnectionModel con))
            {
                return BadRequest("연결된 항목 없음");
            }

            LanguageBiz.BizInstance.SetLanguage(con, conItem);

            //if (!dBManager.DbConnection(conItem))
            //{
            //    return BadRequest("연결실패");
            //}

            return Ok(conItem);
        }
    }
}