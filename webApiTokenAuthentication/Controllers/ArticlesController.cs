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
    public class ArticlesController : ApiController
    {//BD prod
        private DEMO_NEGOCEEntities dbProd = new DEMO_NEGOCEEntities();

        [Authorize]
        [HttpGet]
        [Route("api/Articles")]
        public IQueryable<F_ARTICLESALLPRICE> GetF_ARTICLESV()
        {
            return dbProd.F_ARTICLESALLPRICE;
        }
    }
}