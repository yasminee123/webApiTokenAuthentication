using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using webApiTokenAuthentication;

namespace webApiTokenAuthentication.Controllers
{
    public class CataloguesController : ApiController
    {
        private COG_KSEntities db = new COG_KSEntities();

        [Authorize]
        [HttpGet]
        [Route("api/Catalogues")]
        public IQueryable<F_CATALOGUEV> GetF_CATALOGUEV()
        {
            return db.F_CATALOGUEV;
        }
    }
}