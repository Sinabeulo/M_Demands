using BizCommon_Std.Enums;
using BizCommon_Std.Models;
using CommonBizModule;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiService.Common;
using WebApiService.Data;

namespace WebApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestDataMakerController : ControllerBase
    {
        private readonly TestDataContext _context;

        public TestDataMakerController(TestDataContext context)
        {
            _context = context;
        }


        // POST: api/Language
        [HttpPost]
        public async Task<IActionResult> PostSetTestData([FromBody] TestDataMakerModel item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!(ConnectedDB.Instance.GetConnection(item.TargetTitle) is ConnectionModel con))
            {
                return BadRequest("연결된 항목 없음");
            }

            string dataElement = item.DataElement.ToString();

            if (string.IsNullOrEmpty(dataElement))
                return BadRequest("DataElement null");

            object result = null;
            switch (item.TargetFeature)
            {
                case Features.USER_CODE:
                    result = TestDataMakerBiz.BizInstance.GetMakerQuery(con, dataElement);
                    break;

                case Features.PO_COPY:
                    result = POTestDataMakerBiz.BizInstance.GetMakerQuery(con, dataElement);
                    break;

                case Features.BL_COPY:
                    result = BLTestDataMakerBiz.BizInstance.GetMakerQuery(con, dataElement);
                    break;
            }

            //if (!dBManager.DbConnection(conItem))
            //{
            //    return BadRequest("연결실패");
            //}

            return Ok(result);
        }
    }
}
