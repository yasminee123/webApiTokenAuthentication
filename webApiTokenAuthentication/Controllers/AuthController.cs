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
using webApiTokenAuthentication.Models;

namespace webApiTokenAuthentication.Controllers
{
    public class AuthController : ApiController
    {
        private COG_KSEntities db = new COG_KSEntities();


        [Authorize]
        [HttpGet]
        [Route("api/auth")]
        [ResponseType(typeof(VM_Collaborateur))]
        public collab GetVM_CollaborateurDR(string login, string password)
        {
            
            VM_Collaborateur vm_collaborateur = db.VM_Collaborateur.FirstOrDefault(cc=>cc.Login == login && cc.Psw == password);
            if (vm_collaborateur == null)
            {
                return null;
            }
            collab collaborateur = new collab();
            collaborateur.id = vm_collaborateur.id;
            collaborateur.CO_NO = vm_collaborateur.CO_NO.Value;
            collaborateur.PROFIL = vm_collaborateur.PROFIL.Value;
            collaborateur.Nom = vm_collaborateur.Nom;
            collaborateur.Prenom = vm_collaborateur.Prenom;
            collaborateur.CT_Num = vm_collaborateur.CT_Num;
            collaborateur.N_Cattarif = vm_collaborateur.N_cattarif.Value;

            return collaborateur;
        }

        [Authorize]
        [HttpPost]
        [Route("api/setregid")]
        [ResponseType(typeof(VM_Collaborateur))]
        public IHttpActionResult SetVM_CollaborateurRegid(int id, string registrationid)
        {

            VM_Collaborateur vm_collaborateur = db.VM_Collaborateur.FirstOrDefault(cc => cc.id == id);
            if (vm_collaborateur != null)
            {
                vm_collaborateur.RegistrationID = registrationid;
                db.SaveChanges();
                return Ok("OK");
            }
            else
            {
                return Ok("Not OK");
            }
            

      
        }
        
    }
}