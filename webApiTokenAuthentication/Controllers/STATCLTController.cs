using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace webApiTokenAuthentication.Controllers
{
    public class STATCLTController : ApiController
    {
        private DEMO_NEGOCEEntities db = new DEMO_NEGOCEEntities();

        [Authorize]
        [HttpGet]
        [Route("api/statclt")]
        public IQueryable<STAT_CLT> GetF_STATCLTV(int co_no)
        {
            return db.STAT_CLT.Where(cc => cc.CO_No == co_no);
        }
    }
}
