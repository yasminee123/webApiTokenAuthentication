using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace webApiTokenAuthentication.Controllers
{
    public class AssociationController : ApiController
    {
        private COG_KSEntities db = new COG_KSEntities();


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/association")]
        // GET: Association
        public IQueryable<AssClientArt> GetAllAssociation()
        {
            return db.AssClientArt;
        }
    }
}