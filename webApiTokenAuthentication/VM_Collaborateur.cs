//------------------------------------------------------------------------------
// <auto-generated>
//    Ce code a été généré à partir d'un modèle.
//
//    Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//    Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace webApiTokenAuthentication
{
    using System;
    using System.Collections.Generic;
    
    public partial class VM_Collaborateur
    {
        public int id { get; set; }
        public string Login { get; set; }
        public string Psw { get; set; }
        public Nullable<int> CO_NO { get; set; }
        public Nullable<int> PROFIL { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string CT_Num { get; set; }
        public string RegistrationID { get; set; }
        public Nullable<int> N_cattarif { get; set; }
    }
}