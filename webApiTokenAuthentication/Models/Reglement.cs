using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace webApiTokenAuthentication.Models
{
    public class Reglement
    {
        public int id { get; set; }
        public string CT_NumPayeur { get; set; }
        public string RG_Date { get; set; }
        public string RG_Reference { get; set; }
        public string RG_Libelle { get; set; }
        public decimal RG_Montant { get; set; }
        public int N_Reglement { get; set; }

    }
}