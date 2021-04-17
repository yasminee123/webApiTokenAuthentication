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
    public class ClientsController : ApiController
    {
        private COG_KSEntities db = new COG_KSEntities();
        private DEMO_NEGOCEEntities dbProd = new DEMO_NEGOCEEntities();


        [Authorize]
        [HttpGet]
        [Route("api/Clients")]
        public IQueryable<F_COMPTETV> GetF_COMPTETV(int co_no)
        {
            return dbProd.F_COMPTETV.Where(cc => cc.CO_No == co_no);
        }


        [Authorize]
        [HttpGet]
        [Route("api/Clientsall")]
        public IQueryable<F_COMPTETV> GetF_COMPTETAll()
        {
            return dbProd.F_COMPTETV;
        }

        [Authorize]
        [HttpGet]
        [Route("api/Commall")]
        public IQueryable<F_COLLABORATEUR> GetF_COMMAll()
        {
            return dbProd.F_COLLABORATEUR;
        }

       

    }
}