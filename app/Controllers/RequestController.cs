using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace app.Controllers
{
    public class RequestController : ApiController
    {
        // קונטרול היחיד בפרויקט
        [HttpPost]
        [Route("GetData")]
        public DataSet GetData(RequestBL RequestBL)
        {
            return RequestBL.GetData();
        }
    }
}

