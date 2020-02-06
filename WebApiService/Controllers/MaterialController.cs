using BizCommon_Std.Interface;
using BizCommon_Std.MATR;
using BizCommon_Std.RequestModel;
using CommonBizModule.MATRBizModule;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiService.Common;
using WebApiService.Data;

namespace WebApiService.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        private readonly MaterialContext _context;

        public MaterialController(MaterialContext context)
        {
            _context = context;
        }

        // POST: api/Material
        [HttpPost]
        public async Task<IActionResult> PostMaterial([FromBody] MATRRequestModel item)
        {
            try
            {
                object result = null;
                IFeatureExecute ret = null;
                switch (item.TargetFeature.ToUpper())
                {
                    case "ETC_CANCEL":
                        if (!(item.DataElement is MATR_ETCCancelModel model))
                            break;
                        ret = new MATR_ETCCancel(model);
                        break;
                    default:
                        result = "실행할 기능 미입력";
                        break;
                }
                ret.Target = ConnectedDB.Instance.GetConnection(item.TargetTitle);
                ret.Execute();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
