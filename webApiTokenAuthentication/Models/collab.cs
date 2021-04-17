using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webApiTokenAuthentication.Models
{
    public class collab
    {
        public int id { get; set; }
        public int CO_NO { get; set; }
        public int PROFIL { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string CT_Num { get; set; }

        public int N_Cattarif { get; set; }

        public string AdressIp { get; set; }
        
    }
}