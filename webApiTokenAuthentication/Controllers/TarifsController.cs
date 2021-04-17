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
    public class TarifsController : ApiController
    {
        private DEMO_NEGOCEEntities dbprod = new DEMO_NEGOCEEntities();

        [Authorize]
        [HttpGet]
        [Route("api/Tarifs")]
        public List<art_tarifs_globales> Getart_tarif_globale()
        {
            return dbprod.art_tarifs_globales.ToList() ;
        }


     
        [HttpGet]
        [Route("api/TarifsArt")]
        public decimal Getart_tarifs_details(string ar_ref, string EC_Enumere, int n_cattarif)
        {
            return dbprod.art_tarifs_globales.FirstOrDefault(cc => cc.AR_Ref == ar_ref && cc.EC_Enumere == EC_Enumere && cc.cat == n_cattarif).prix_veNte.Value;
        }
        
    }
}