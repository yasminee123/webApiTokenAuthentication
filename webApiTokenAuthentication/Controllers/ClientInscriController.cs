using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace webApiTokenAuthentication.Controllers
{
    public class ClientInscriController : ApiController
    {
        private COG_KSEntities db = new COG_KSEntities();
        [HttpPost]
        [Route("api/cltinscriadd")]
        public IHttpActionResult Postclt_inscri(VM_ClientInscri cltinscri)
        {
            VM_ClientInscri cltinscr = new VM_ClientInscri();
            cltinscr.Nom = cltinscri.Nom;
            cltinscr.Prenom = cltinscri.Prenom;
            cltinscr.Mail = cltinscri.Mail;
            cltinscr.Telephone = cltinscri.Telephone;
            cltinscr.Longitude = cltinscri.Longitude;
            cltinscr.Latitude = cltinscri.Latitude;
            cltinscr.Adresse = cltinscri.Adresse;
            db.VM_ClientInscri.Add(cltinscr);
            db.SaveChanges();
            return Json(cltinscri);
        }
    }
}
