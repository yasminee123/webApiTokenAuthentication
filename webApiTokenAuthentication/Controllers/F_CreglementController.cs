using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webApiTokenAuthentication.Models;

namespace webApiTokenAuthentication.Controllers
{
    public class F_CreglementController : ApiController
    {
        private COG_KSEntities db = new COG_KSEntities();

        [Authorize]
        [HttpPost]
        [Route("api/regAdd")]
        public IHttpActionResult PostF_creg(Reglement reglement)
        {
            int rg = 0;
            try
            {
                rg = db.F_CREGLEMENT.Max(cc => cc.RG_No).Value;
            }
            catch
            {
                rg = 0;
            }

            F_CREGLEMENT f_creglement = new F_CREGLEMENT();
            f_creglement.RG_No = rg + 1;
            f_creglement.CT_NumPayeur = reglement.CT_NumPayeur;
            f_creglement.RG_Date = System.DateTime.Now;
            f_creglement.RG_Reference = reglement.RG_Reference;
            f_creglement.RG_Libelle = reglement.RG_Libelle;
            f_creglement.RG_Montant = reglement.RG_Montant;
            f_creglement.RG_MontantDev = 0;
            try
            {
                string n_reglement = reglement.N_Reglement.ToString();
                f_creglement.N_Reglement = short.Parse(n_reglement);
            }
            catch
            {
                f_creglement.N_Reglement = 0;
            }

            f_creglement.RG_Impute = 0;
            f_creglement.RG_Compta = 0;
            f_creglement.RG_Type = 0;
            f_creglement.RG_Cours = 0;
            f_creglement.N_Devise = 0;
            f_creglement.JO_Num = "CR1";
            f_creglement.EC_No = 0;
            f_creglement.RG_Impaye = Convert.ToDateTime("1900-01-01").Date;
            f_creglement.CG_Num = "411000";
            f_creglement.RG_TypeReg = 0;
            f_creglement.CA_No = 0;
            f_creglement.CO_NoCaissier = 0;
            f_creglement.RG_Banque = 0;
            f_creglement.RG_Transfere = 0;
            f_creglement.RG_Cloture = 0;
            f_creglement.RG_Ticket = 0;
            f_creglement.RG_Souche = 0;
            f_creglement.CT_NumPayeurOrig = reglement.CT_NumPayeur;
            f_creglement.RG_MontantEcart = 0;
            f_creglement.cbProt = 0;

            try
            {
                db.F_CREGLEMENT.Add(f_creglement);
                db.SaveChanges();
                reg rel = new reg();
                rel.id = reglement.id;
                rel.rg_no = f_creglement.RG_No.Value;
                return Json(rel);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException.ToString());
            }
        }


        [Authorize]
        [HttpPost]
        [Route("api/regAddListe")]
        public IHttpActionResult PostF_creg(List<Reglement> reglement)
        {
            List<reg> liste_reg = new List<reg>();
            int rg = 0;
            try
            {
                rg = db.F_CREGLEMENT.Max(cc => cc.RG_No).Value;
            }
            catch
            {
                rg = 0;
            }
            foreach (var item in reglement)
            {
                

                F_CREGLEMENT f_creglement = new F_CREGLEMENT();
                rg = rg + 1;
                f_creglement.RG_No = rg;
                f_creglement.CT_NumPayeur = item.CT_NumPayeur;
                f_creglement.RG_Date = System.DateTime.Now;
                f_creglement.RG_Reference = item.RG_Reference;
                f_creglement.RG_Libelle = item.RG_Libelle;
                f_creglement.RG_Montant = item.RG_Montant;
                f_creglement.RG_MontantDev = 0;
                try
                {
                    string n_reglement = item.N_Reglement.ToString();
                    f_creglement.N_Reglement = short.Parse(n_reglement);
                }
                catch
                {
                    f_creglement.N_Reglement = 0;
                }

                f_creglement.RG_Impute = 0;
                f_creglement.RG_Compta = 0;
                f_creglement.RG_Type = 0;
                f_creglement.RG_Cours = 0;
                f_creglement.N_Devise = 0;
                f_creglement.JO_Num = "CR1";
                f_creglement.EC_No = 0;
                f_creglement.RG_Impaye = Convert.ToDateTime("1900-01-01").Date;
                f_creglement.CG_Num = "411000";
                f_creglement.RG_TypeReg = 0;
                f_creglement.CA_No = 0;
                f_creglement.CO_NoCaissier = 0;
                f_creglement.RG_Banque = 0;
                f_creglement.RG_Transfere = 0;
                f_creglement.RG_Cloture = 0;
                f_creglement.RG_Ticket = 0;
                f_creglement.RG_Souche = 0;
                f_creglement.CT_NumPayeurOrig = item.CT_NumPayeur;
                f_creglement.RG_MontantEcart = 0;
                f_creglement.cbProt = 0;

                try
                {
                    db.F_CREGLEMENT.Add(f_creglement);
                    reg regl = new reg();
                    regl.id = item.id;
                    regl.rg_no = f_creglement.RG_No.Value;
                    liste_reg.Add(regl);
                }
                catch (Exception ex)
                {
                    return Ok(ex.InnerException.ToString());
                }
            }
            db.SaveChanges();
            return Json(liste_reg);
        }

    }
}
