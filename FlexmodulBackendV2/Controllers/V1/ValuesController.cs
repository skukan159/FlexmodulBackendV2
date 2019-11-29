using System.Collections.Generic;
using FlexmodulBackendV2.Contracts.V1;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FlexmodulBackendV2.Controllers.V1
{
    //[Authorize]
    [EnableCors]
    [ApiController]
    public class ValuesController : Controller
    {
        [HttpGet(ApiRoutes.Values.GetAll)]
        public ActionResult<IEnumerable<string>> Get()
        {
            return base.Ok(new [] { "value1", "value2" });
        }


        [HttpGet(ApiRoutes.Values.Get)]
        public ActionResult<string> Get([FromRoute] int id)
        {
            return base.Ok("value " + id);
        }
    }
}
