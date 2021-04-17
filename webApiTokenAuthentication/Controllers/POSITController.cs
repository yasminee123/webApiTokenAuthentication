using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webApiTokenAuthentication.Models;

namespace webApiTokenAuthentication.Controllers
{
    public class POSITController : ApiController
    {
        private COG_KSEntities db = new COG_KSEntities();

        [Authorize]
        [HttpPost]
        [Route("api/posAdd")]
        public IHttpActionResult PostF_poscom(F_POSITIONCOM position)
        {
            try
            {
                F_POSITIONCOM poscom = new F_POSITIONCOM();
                poscom.CO_No = position.CO_No;
                poscom.Longitude = position.Longitude;
                poscom.Latitude = position.Latitude;
                poscom.DateP = position.DateP;
                db.F_POSITIONCOM.Add(poscom);
                db.SaveChanges();
                return Json(poscom);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException.ToString());
            }
           
        }

        [Authorize]
        [HttpPost]
        [Route("api/posListAdd")]
        public IHttpActionResult PostlF_poscom(List<F_POSITIONCOM> lposition)
        {
            foreach (var item in lposition)
            {
                try
                {
                    F_POSITIONCOM poscom = new F_POSITIONCOM();
                    poscom.CO_No = item.CO_No;
                    poscom.Longitude = item.Longitude;
                    poscom.Latitude = item.Latitude;
                    poscom.DateP = item.DateP;
                    db.F_POSITIONCOM.Add(poscom);
                    db.SaveChanges();
                    return Json(poscom);
                }
                catch (Exception ex)
                {
                    return Ok(ex.InnerException.ToString());
                }
            }
            return Ok();

        }

        [Authorize]
        [HttpGet]
        [Route("api/Positions")]
        public IQueryable<F_POSITIONCOM> GetF_POSITIONCOM(int co_no)
        {
            return db.F_POSITIONCOM.Where(cc => cc.CO_No == co_no);
        }
    }
}
