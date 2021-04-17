using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace webApiTokenAuthentication.Controllers
{
    public class P_REGLEMENTController : ApiController
    {
        private DEMO_NEGOCEEntities dbProd = new DEMO_NEGOCEEntities();
        
        [Authorize]
        [HttpGet]
        [Route("api/preglement")]
        public IQueryable<P_REGLEMENT> GetP_REGLEMENT()
        {
            return dbProd.P_REGLEMENT.Where(cc=> cc.R_Intitule != "");
        }
    }
}
