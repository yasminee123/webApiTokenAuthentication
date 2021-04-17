using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace webApiTokenAuthentication.Controllers
{
    public class STATARTController : ApiController
    {
        private DEMO_NEGOCEEntities db = new DEMO_NEGOCEEntities();

        [Authorize]
        [HttpGet]
        [Route("api/statart")]
        public IQueryable<STAT_ART> GetF_STATARTV(int co_no)
        {
            return db.STAT_ART.Where(cc => cc.CO_No == co_no);
        }
    }
}
