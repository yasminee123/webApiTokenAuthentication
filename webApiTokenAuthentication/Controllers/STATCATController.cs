using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace webApiTokenAuthentication.Controllers
{
    public class STATCATController : ApiController
    {
        private DEMO_NEGOCEEntities db = new DEMO_NEGOCEEntities();

        [Authorize]
        [HttpGet]
        [Route("api/statcat")]
        public IQueryable<STAT_CAT> GetF_STATCATV(int co_no)
        {
            return db.STAT_CAT.Where(cc=> cc.CO_No == co_no);
        }
    }
}
