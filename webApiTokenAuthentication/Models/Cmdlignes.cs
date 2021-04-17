using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webApiTokenAuthentication.Models
{

    public class Cmdentete
    {
        public int Profil { get; set; }
        public int id { get; set; }
        public int DO_Type { get; set; }
        public string DO_Piece { get; set; }
        public DateTime DO_Date { get; set; }
        public string DO_Tiers { get; set; }

        public string CT_Intitule { get; set; }
        public string DO_Ref { get; set; }
        public int CO_No { get; set; }
        public decimal dl_montantTTC { get; set; }

        public string BCVALIDE { get; set; }

        public int n_cattarif { get; set; }
        public IEnumerable<Cmdlignes> lignes { get; set; }

    }

    public class Cmdlignes
    {
            public int do_type { get; set; }
            public string do_piece { get; set; }
            public string ar_ref { get; set; }
            public string dl_design { get; set; }
            public decimal qte_piece { get; set; }
            public decimal qte_colisee { get; set; }
            public decimal pu { get; set; }
            public decimal remise { get; set; }
            public string DO_Ref { get; set; }
            public string ct_num { get; set; }
            public DateTime DO_Date { get; set; }
            public decimal dl_montantHT { get; set; }
            public decimal dl_montantTTC { get; set; }
            public int CO_No { get; set; }
            public string EU_Enumere{ get; set; }

            public decimal Dl_qte { get; set; }
        
    }
}