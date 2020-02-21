using BizCommon_Std.Interface;
using BizCommon_Std.Models;
using CommonBizModule.TestData;
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
    public class TestDataController : ControllerBase
    {
        private readonly TestDataContext _context;

        public TestDataController(TestDataContext context)
        {
            _context = context;
        }

        // POST: api/TestData
        [HttpPost]
        public async Task<IActionResult> PostMakeTestData([FromBody] TestDataModel conItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IFeatureExecute ret = null;
            switch (conItem.TargetFeature.ToUpper())
            {
                case "COPY_USERCODE":

                    break;
            }
            ret.Target = ConnectedDB.Instance.GetConnection(conItem.TargetTitle);

            string headerCode = conItem?.DataElement?.ToString();

            if (string.IsNullOrEmpty(headerCode))
                return BadRequest("전송받은 데이터 오류");

            var retDt = TestDataBiz.BizInstance.GetUserCodeData(ret.Target, headerCode);

            return Ok(retDt);
        }
    }
}
