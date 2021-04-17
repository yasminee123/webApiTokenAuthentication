using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;
using webApiTokenAuthentication.Models;
using System.Transactions;
using System.Data.EntityClient;

namespace webApiTokenAuthentication.Controllers
{
    public class CMDController : ApiController
    {
        string do_pieceaff;
        string do_piece_origine;
        private COG_KSEntities db = new COG_KSEntities();
        private DEMO_NEGOCEEntities dbProd = new DEMO_NEGOCEEntities();
        private PCM_KS_BOEntities dbBO = new PCM_KS_BOEntities();


        [Authorize]
        [HttpGet]
        [Route("api/cmd")]
        //toutes les cmd de PCM_KS (BD inter)
        public IEnumerable<Cmdentete> GetTeamsAndPlayers()
        {
           
            var teams = db.F_DOCENTETE.Where(cc => cc.DO_Type == 1)
                .Select(t => new Cmdentete
                {
                    DO_Type = t.DO_Type.Value,
                    DO_Piece = t.DO_Piece,
                    DO_Date = t.DO_Date.Value,
                    DO_Ref = t.DO_Ref,
                    DO_Tiers = t.DO_Tiers,
                    CT_Intitule = db.F_COMPTETV.FirstOrDefault(cc => cc.CT_Num == t.DO_Tiers).CT_Intitule,
                    CO_No = t.CO_No.Value,
                    dl_montantTTC = t.DO_TotalHT.Value,
                    BCVALIDE = t.BCVALIDE,
                    n_cattarif = db.F_COMPTETV.FirstOrDefault(cc => cc.CT_Num == t.DO_Tiers).N_CatTarif.Value,
                    Profil = t.PROFIL.Value,
                    lignes = db.F_DOCLIGNE.Where(cc => cc.DO_Piece == t.DO_Piece && cc.DO_Type == t.DO_Type).Select(p => new Cmdlignes
                    {
                        do_piece = p.DO_Piece,
                        do_type = p.DO_Type,
                        ar_ref = p.AR_Ref,
                        dl_design = p.DL_Design,
                        qte_piece = p.DL_Qte.Value,
                        qte_colisee = p.EU_Qte.Value,
                        pu = p.DL_PrixUnitaire.Value,
                        remise = 0,
                        ct_num = p.CT_Num,
                        dl_montantHT = p.DL_MontantHT.Value,
                        dl_montantTTC = p.DL_MontantTTC.Value,
                        DO_Date = p.DO_Date.Value,
                        DO_Ref = p.DO_Ref,
                        EU_Enumere = p.EU_Enumere,
                        CO_No = p.CO_No.Value,
                        Dl_qte = p.DL_Qte.Value
                    })
                }).ToList();

            return teams;
        }


        [Authorize]
        [HttpGet]
        [Route("api/factures")]
        public IEnumerable<Cmdentete> GetTeamsAndPlayersf()
        {
            var teams = db.F_DOCENTETE.Where(cc => cc.DO_Type == 6 || cc.DO_Type == 7)
                .Select(t => new Cmdentete
                {

                    DO_Type = t.DO_Type.Value,
                    DO_Piece = t.DO_Piece,
                    DO_Date = t.DO_Date.Value,
                    DO_Ref = t.DO_Ref,
                    DO_Tiers = t.DO_Tiers,
                    CT_Intitule = db.F_COMPTETV.FirstOrDefault(cc => cc.CT_Num == t.DO_Tiers).CT_Intitule,
                    CO_No = t.CO_No.Value,
                    lignes = db.F_DOCLIGNE.Where(cc => cc.DO_Piece == t.DO_Piece && cc.DO_Type == t.DO_Type).Select(p => new Cmdlignes
                    {
                        do_piece = p.DO_Piece,
                        do_type = p.DO_Type,
                        ar_ref = p.AR_Ref,
                        dl_design = p.DL_Design,
                        qte_piece = p.DL_Qte.Value,
                        qte_colisee = p.EU_Qte.Value,
                        pu = p.DL_PrixUnitaire.Value,
                        remise = 0,
                        ct_num = p.CT_Num,
                        dl_montantHT = p.DL_MontantHT.Value,
                        dl_montantTTC = p.DL_MontantTTC.Value,
                        DO_Date = p.DO_Date.Value,
                        DO_Ref = p.DO_Ref,
                        EU_Enumere = p.EU_Enumere,
                        CO_No = p.CO_No.Value,
                        Dl_qte = p.DL_Qte.Value
                    })
                }).ToList();

            return teams;
        }

        [Authorize]
        [HttpGet]
        //cmd d'un commercial particulier
        [Route("api/cmdcono")]
        public IEnumerable<Cmdentete> GetTeamsAndPlayers(int co_no)
        {
            var teams = db.F_DOCENTETE.Where(cc => cc.DO_Type == 1 && cc.CO_No == co_no)
                .Select(t => new Cmdentete
                {

                    DO_Type = t.DO_Type.Value,
                    DO_Piece = t.DO_Piece,
                    DO_Date = t.DO_Date.Value,
                    DO_Ref = t.DO_Ref,
                    DO_Tiers = t.DO_Tiers,
                    CT_Intitule = db.F_COMPTETV.FirstOrDefault(cc => cc.CT_Num == t.DO_Tiers).CT_Intitule,
                    CO_No = t.CO_No.Value,
                    dl_montantTTC = t.DO_TotalHT.Value,
                    BCVALIDE = t.BCVALIDE,
                    n_cattarif = db.F_COMPTETV.FirstOrDefault(cc => cc.CT_Num == t.DO_Tiers).N_CatTarif.Value,
                    Profil = t.PROFIL.Value,
                    lignes = db.F_DOCLIGNE.Where(cc => cc.DO_Piece == t.DO_Piece && cc.DO_Type == t.DO_Type).Select(p => new Cmdlignes
                    {
                        do_piece = p.DO_Piece,
                        do_type = p.DO_Type,
                        ar_ref = p.AR_Ref,
                        dl_design = p.DL_Design,
                        qte_piece = p.DL_Qte.Value,
                        qte_colisee = p.EU_Qte.Value,
                        pu = p.DL_PrixUnitaire.Value,
                        remise = 0,
                        ct_num = p.CT_Num,
                        dl_montantHT = p.DL_MontantHT.Value,
                        dl_montantTTC = p.DL_MontantTTC.Value,
                        DO_Date = p.DO_Date.Value,
                        DO_Ref = p.DO_Ref,
                        EU_Enumere = p.EU_Enumere,
                        CO_No = p.CO_No.Value,
                        Dl_qte = p.DL_Qte.Value
                    })
                }).ToList();

            return teams;
        }

        [Authorize]
        [HttpGet]
        [Route("api/cmdclt")]
        //cmd par clt
        public IEnumerable<Cmdentete> GetTeamsAndPlayersclt(string DO_Tiers)
        {
            var teams = db.F_DOCENTETE.Where(cc => cc.DO_Type == 1 && cc.DO_Tiers == DO_Tiers)
                .Select(t => new Cmdentete
                {

                    DO_Type = t.DO_Type.Value,
                    DO_Piece = t.DO_Piece,
                    DO_Date = t.DO_Date.Value,
                    DO_Ref = t.DO_Ref,
                    DO_Tiers = t.DO_Tiers,
                    CT_Intitule = db.F_COMPTETV.FirstOrDefault(cc => cc.CT_Num == t.DO_Tiers).CT_Intitule,
                    CO_No = t.CO_No.Value,
                    dl_montantTTC = t.DO_TotalHT.Value,
                    BCVALIDE = t.BCVALIDE,
                    n_cattarif = db.F_COMPTETV.FirstOrDefault(cc => cc.CT_Num == t.DO_Tiers).N_CatTarif.Value,
                    Profil = t.PROFIL.Value,
                    lignes = db.F_DOCLIGNE.Where(cc => cc.DO_Piece == t.DO_Piece && cc.DO_Type == t.DO_Type).Select(p => new Cmdlignes
                    {
                        do_piece = p.DO_Piece,
                        do_type = p.DO_Type,
                        ar_ref = p.AR_Ref,
                        dl_design = p.DL_Design,
                        qte_piece = p.DL_Qte.Value,
                        qte_colisee = p.EU_Qte.Value,
                        pu = p.DL_PrixUnitaire.Value,
                        remise = 0,
                        ct_num = p.CT_Num,
                        dl_montantHT = p.DL_MontantHT.Value,
                        dl_montantTTC = p.DL_MontantTTC.Value,
                        DO_Date = p.DO_Date.Value,
                        DO_Ref = p.DO_Ref,
                        EU_Enumere = p.EU_Enumere,
                        CO_No = p.CO_No.Value,
                        Dl_qte = p.DL_Qte.Value
                    })
                }).ToList();

            return teams;
        }

        [Authorize]
        [HttpGet]
        //cmd par id
        [Route("api/cmdid")]
        public Cmdentete GetTeamsAndPlayers(string do_piecev)
        {
            F_DOCENTETE teams = db.F_DOCENTETE.FirstOrDefault(cc => cc.DO_Type == 1 && cc.DO_Piece == do_piecev);
            Cmdentete cmdid = new Cmdentete();
            cmdid.DO_Type = teams.DO_Type.Value;
                    cmdid.DO_Piece = teams.DO_Piece;
                    cmdid.DO_Date = teams.DO_Date.Value;
                    cmdid.DO_Ref = teams.DO_Ref;
                    cmdid.DO_Tiers = teams.DO_Tiers;
                    cmdid.CT_Intitule = db.F_COMPTETV.FirstOrDefault(cc => cc.CT_Num == cmdid.DO_Tiers).CT_Intitule;
                    cmdid.CO_No = teams.CO_No.Value;
                    cmdid.dl_montantTTC = teams.DO_TotalHT.Value;
                    cmdid.BCVALIDE = teams.BCVALIDE;
                    cmdid.n_cattarif = db.F_COMPTETV.FirstOrDefault(cc => cc.CT_Num == cmdid.DO_Tiers).N_CatTarif.Value;
                    cmdid.Profil = teams.PROFIL.Value;
            List<F_DOCLIGNE> lignesdoc = db.F_DOCLIGNE.Where(cc => cc.DO_Piece == teams.DO_Piece && cc.DO_Type == teams.DO_Type).ToList();
            List<Cmdlignes> lignesliste = new List<Cmdlignes>();      
            foreach(var item in lignesdoc)
                    {
                       Cmdlignes ligne = new Cmdlignes();
                        ligne.do_piece = item.DO_Piece;
                        ligne.do_type = item.DO_Type;
                        ligne.ar_ref = item.AR_Ref;
                        ligne.dl_design = item.DL_Design;
                        ligne.qte_piece = item.DL_Qte.Value;
                        ligne.qte_colisee = item.EU_Qte.Value;
                        ligne.pu = item.DL_PrixUnitaire.Value;
                        ligne.remise = item.DL_Remise01REM_Valeur.Value;
                        ligne.ct_num = item.CT_Num;
                        ligne.dl_montantHT = item.DL_MontantHT.Value;
                        ligne.dl_montantTTC = item.DL_MontantTTC.Value;
                        ligne.DO_Date = item.DO_Date.Value;
                        ligne.DO_Ref = item.DO_Ref;
                        ligne.EU_Enumere = item.EU_Enumere;
                        ligne.CO_No = item.CO_No.Value;
                        ligne.Dl_qte = item.DL_Qte.Value;
                        lignesliste.Add(ligne);
                    }
            cmdid.lignes = lignesliste;

             return cmdid;
        }

        //Insertion d'une commande 
        [Authorize]
        [HttpPost]
        [Route("api/cmdAdd")]
        public IHttpActionResult PostF_cmd(Cmdentete commande)
        {
            if (commande.Profil == 0)
            { //Commande validée par un commercial 
                try
                {
                    /*CreerBL(commande);
                    add_base_prod(commande);*/
                        CreerBLBO(commande);
                        cmd cmdcobj = new cmd();
                        cmdcobj.DO_Piece = do_pieceaff;
                        cmdcobj.id = commande.id;
                        return Json(cmdcobj);
                }
                catch (Exception ex)
                {
                    return Ok(ex.InnerException.ToString());
                }
            }

            /*else //la cmd venant du client
            {
                try
                {
                    F_DOCENTETE cmd = new F_DOCENTETE();
                    cmd = db.F_DOCENTETE.FirstOrDefault(cc => cc.DO_Piece == commande.DO_Piece && cc.DO_Type == 1);
                    cmd.BCVALIDE = commande.BCVALIDE;
                    if (commande.BCVALIDE == "Valide") //Normalment pour que la commande du client soit validée elle doit etre passée par le comm alors pourquoi on vérifie la validité de la cmd dans ce cas?
                    {
                        update_ligne(commande);
                        // add_base_prod(commande);   
                        CreerBLBO(commande);
                    }
                    cmd cmdcobj = new cmd();
                    cmdcobj.DO_Piece = do_pieceaff;
                    cmdcobj.id = commande.id;
                    db.SaveChanges();
                    return Json(cmdcobj);
                }
                catch
                {
                    try
                    {

                        //CreerBL(commande);
                        CreerBLBO(commande);
                        cmd cmdcobj = new cmd();
                        cmdcobj.DO_Piece = do_pieceaff;
                        cmdcobj.id = commande.id;
                        try
                        {
                            string registrationid = db.VM_Collaborateur.FirstOrDefault(cc => cc.CO_NO == commande.CO_No).RegistrationID;
                            string ct_intitule = db.F_COMPTETV.FirstOrDefault(cc => cc.CT_Num == commande.DO_Tiers).CT_Intitule;
                            SendNotif(ct_intitule, registrationid, do_pieceaff);
                        }
                        catch
                        {

                        }
                        return Json(cmdcobj);

                    }
                    catch (Exception ex)
                    {
                        return Ok(ex.InnerException.ToString());
                    }
                }
            }*/
            return null;
        }

        private void add_base_prod(Cmdentete commande)
        {
            using (var transaction = new System.Transactions.TransactionScope())
            {
                using (EntityConnection conn = new EntityConnection("name=DEMO_NEGOCEEntities"))
                {
                    conn.Open();
                    do_piece_origine = dbProd.F_DOCCURRENTPIECE.FirstOrDefault(cc => cc.DC_Domaine == 0 && cc.DC_IdCol == 1 && cc.DC_Souche == 0).DC_Piece;
                    do_pieceaff = do_piece_origine;
                    string do_piececmd = do_piece_origine;

                    #region docentet
                    F_DOCENTETE docentet = new F_DOCENTETE();
                    docentet.DO_Domaine = 0;
                    docentet.DO_Type = 1;
                    docentet.DO_Devise = 0;
                    docentet.DO_Period = 1;
                    docentet.DO_Expedit = 1;
                    docentet.DO_Souche = 0;
                    docentet.DO_Piece = do_piececmd;
                    docentet.DO_Tiers = commande.DO_Tiers;
                    docentet.CT_NumPayeur = commande.DO_Tiers;
                    docentet.DO_Date = commande.DO_Date;
                    docentet.DO_Statut = 2;
                    docentet.CO_No = commande.CO_No;
                    int de_no;
                    try
                    {
                        de_no = dbProd.F_COMPTET.FirstOrDefault(cc => cc.CT_Num == commande.DO_Tiers).DE_No.Value;
                        if (de_no == 0)
                            de_no = 4;
                    }
                    catch
                    {
                        de_no = 4;
                    }
                    docentet.DE_No = de_no;
                    docentet.DO_Reliquat = 0;
                    docentet.DO_Imprim = 0;
                    docentet.DO_Ventile = 0;
                    docentet.AB_No = 0;
                    docentet.CG_Num = "411000";
                    docentet.CA_No = 0;
                    docentet.DO_Transfere = 0;
                    docentet.DO_Cloture = 0;
                    docentet.DO_NoWeb = "";
                    docentet.DO_Provenance = 0;
                    docentet.CA_NumIFRS = "";
                    docentet.DO_TypeFrais = 0;
                    docentet.DO_Motif = "";
                    docentet.DO_Contact = "";
                    docentet.DO_Condition = 1;
                    docentet.DO_TypeFranco = 0;
                    docentet.DO_Taxe1 = 0;
                    docentet.DO_Taxe2 = 0;
                    docentet.DO_Taxe3 = 0;
                    docentet.DO_TypeTaux1 = 0;
                    docentet.DO_TypeTaux2 = 0;
                    docentet.DO_TypeTaux3 = 0;
                    docentet.DO_TypeTaxe1 = 0;
                    docentet.DO_TypeTaxe2 = 0;
                    docentet.DO_TypeTaxe3 = 0;
                    docentet.DO_DateLivrRealisee = Convert.ToDateTime("1900-01-01").Date;
                    docentet.DO_DateExpedition = Convert.ToDateTime("1900-01-01").Date;
                    docentet.DO_FactureFrs = "";
                    docentet.DO_PieceOrig = "";
                 
                    docentet.DO_DemandeRegul = 0;
                    docentet.ET_No = 0;
                    docentet.DO_Valide = 0;
                    docentet.DO_Coffre = 0;
                    int lino;
                    try
                    {
                        lino = dbProd.F_LIVRAISON.FirstOrDefault(l => l.CT_Num == commande.DO_Tiers).LI_No;
                        docentet.LI_No = lino;
                        docentet.cbLI_No = lino;
                    }
                    catch
                    {
                        docentet.LI_No = null;
                        docentet.cbLI_No = null;
                    }
                    docentet.CA_Num = "";
                    docentet.DO_Ecart = 0;
                    docentet.DO_Regime = 21;
                    docentet.DO_Transaction = 11;
                    docentet.DO_Cours = 0;
                    docentet.DO_NbFacture = 1;
                    docentet.DO_BLFact = 0;
                    docentet.DO_TxEscompte = 0;
                    docentet.DO_Coord01 = "";
                    docentet.DO_Coord02 = "";
                    docentet.DO_Coord03 = "";
                    docentet.DO_Coord04 = "";
                    docentet.DO_DateLivr = Convert.ToDateTime("1900-01-01").Date;
                    docentet.DO_Tarif = 1;
                    docentet.DO_Colisage = 1;
                    docentet.DO_TypeColis = 1;
                    docentet.DO_Langue = 0;
                    docentet.N_CatCompta = 1;
                    docentet.DO_DebutAbo = Convert.ToDateTime("1900-01-01").Date;
                    docentet.DO_FinAbo = Convert.ToDateTime("1900-01-01").Date;
                    docentet.DO_DebutPeriod = Convert.ToDateTime("1900-01-01").Date;
                    docentet.DO_FinPeriod = Convert.ToDateTime("1900-01-01").Date;
                    docentet.DO_Heure = "000112441";
                    docentet.CO_NoCaissier = 0;
                    docentet.DO_Attente = 0;
                    docentet.MR_No = 0;
                    docentet.DO_ValFrais = 0;
                    docentet.DO_TypeLigneFrais = 1;
                    docentet.DO_TypeLigneFranco = 0;
                    docentet.DO_ValFranco = 0;
                    docentet.DO_MajCpta = 0;
                    docentet.DO_FactureElec = 0;
                    docentet.DO_TypeTransac = 0;
                    docentet.DO_Ref = "";
                    docentet.DO_TotalHT = commande.dl_montantTTC;
                    docentet.cbProt = 0;
                    docentet.cbFlag = 0;
                    docentet.cbCreateur = "ERP1";
                    docentet.PROFIL = commande.Profil;
                    docentet.BCVALIDE = commande.BCVALIDE;
                    try
                    {
                        docentet.DO_Tarif = short.Parse(commande.n_cattarif.ToString());
                    }
                    catch
                    {
                        docentet.DO_Tarif = 0;
                    }
                    dbProd.F_DOCENTETE.Add(docentet);
                    dbProd.SaveChanges();
                    #endregion

                    #region DOCCURRENTPIECE

                    F_DOCCURRENTPIECE currentpiece = new F_DOCCURRENTPIECE();
                    currentpiece = dbProd.F_DOCCURRENTPIECE.FirstOrDefault(cc => cc.DC_Domaine == 0 && cc.DC_IdCol == 1 && cc.DC_Souche == 0);
                    string num_Piece = do_piece_origine;
                    int length_numpiece = num_Piece.Length - 3;
                    string prefixe = num_Piece.Substring(3, length_numpiece);
                    string suffixe = num_Piece.Substring(0, 3);
                    int npiece = int.Parse(prefixe);
                    npiece = npiece + 1;
                    string do_pieceS = suffixe + npiece.ToString();
                    currentpiece.DC_Piece = do_pieceS;
                    dbProd.SaveChanges();
                    #endregion

                    #region docregl

                    F_DOCREGL docregl = new F_DOCREGL();
                    docregl.DR_Date = commande.DO_Date;
                    docregl.DO_Domaine = 0;
                    docregl.DO_Type = 1;
                    docregl.DO_Piece = do_piececmd;
                    docregl.DR_TypeRegl = 2;
                    docregl.DR_Equil = 1;
                    docregl.EC_No = 0;
                    docregl.DR_Regle = 0;
                    docregl.N_Reglement = 1;
                    docregl.DR_Libelle = "";
                    docregl.DR_Pourcent = 0;
                    docregl.DR_Montant = 0;
                    docregl.DR_MontantDev = 0;
                    docregl.cbProt = 0;
                    dbProd.F_DOCREGL.Add(docregl);
                    dbProd.SaveChanges();
                    #endregion

                    #region docligne

                    foreach (var item in commande.lignes)
                    {
                        decimal cmup;
                        try
                        {
                            decimal montant = dbProd.F_ARTSTOCK.Where(cc => cc.AR_Ref == item.ar_ref).Sum(cc => cc.AS_MontSto).Value;
                            decimal Qte = dbProd.F_ARTSTOCK.Where(cc => cc.AR_Ref == item.ar_ref).Sum(cc => cc.AS_QteSto).Value;

                            cmup = montant / Qte;
                        }
                        catch
                        {
                            cmup = item.pu;
                        }

                        decimal taxe;
                        string code_taxe;
                        string code_famille;
                        try
                        {
                            try
                            {
                                code_taxe = dbProd.F_ARTCOMPTA.FirstOrDefault(cc => cc.AR_Ref == item.ar_ref && cc.ACP_Type == 0 && cc.ACP_Champ == 1).ACP_ComptaCPT_Taxe1;
                                taxe = dbProd.F_TAXE.FirstOrDefault(cc => cc.TA_Code == code_taxe).TA_Taux.Value;
                            }
                            catch
                            {
                                code_famille = dbProd.F_ARTICLE.FirstOrDefault(cc => cc.AR_Ref == item.ar_ref).FA_CodeFamille;
                                code_taxe = dbProd.F_FAMCOMPTA.FirstOrDefault(cc => cc.FA_CodeFamille == code_famille && cc.FCP_Type == 0 && cc.FCP_Champ == 1).FCP_ComptaCPT_Taxe1;
                                taxe = dbProd.F_TAXE.FirstOrDefault(cc => cc.TA_Code == code_taxe).TA_Taux.Value;
                            }
                        }
                        catch
                        {
                            taxe = -1;
                            code_taxe = null;
                        }

                        F_DOCLIGNE docligne = new F_DOCLIGNE();
                        docligne.DO_Domaine = 0;
                        docligne.DO_Type = 1;
                        docligne.CT_Num = item.ct_num;
                        docligne.PF_Num = "";
                        docligne.DO_Piece = do_piececmd;
                        docligne.DO_Date = item.DO_Date;
                        docligne.AR_Ref = item.ar_ref;
                        docligne.DL_Design = item.dl_design;
                        docligne.DL_Qte = item.Dl_qte;
                        docligne.EU_Qte = item.qte_colisee;
                        docligne.DL_MvtStock = 0;
                        try
                        {
                            de_no = dbProd.F_COMPTET.FirstOrDefault(cc => cc.CT_Num == item.ct_num).DE_No.Value;
                            if (de_no == 0)
                                de_no = 4;
                        }
                        catch
                        {
                            de_no = 4;
                        }
                        docligne.DE_No = de_no;
                        docligne.cbDE_No = de_no;
                        docligne.DL_No = dbProd.F_DOCLIGNE.Max(l => l.DL_No) + 1;
                        docligne.DL_PieceBC = "";
                        docligne.DL_PieceBL = "";
                        docligne.DL_DateBL = item.DO_Date;
                        docligne.DL_DateBC = item.DO_Date;
                        docligne.DO_Ref = "";
                        docligne.DL_TNomencl = 0;
                        docligne.DL_TRemExep = 0;
                        docligne.DL_TRemPied = 0;
                        docligne.DL_QteBC = docligne.DL_Qte;
                        docligne.DL_QteBL = 0;
                        docligne.DL_PoidsBrut = 0;
                        docligne.DL_PoidsNet = 0;
                        docligne.DL_PUBC = 0;
                        docligne.DL_Remise02REM_Type = 0;
                        docligne.DL_Remise02REM_Valeur = 0;
                        docligne.DL_Remise03REM_Type = 0;
                        docligne.DL_Remise03REM_Valeur = 0;
                        docligne.CO_No = commande.CO_No;
                        docligne.AG_No1 = 0;
                        docligne.AG_No2 = 0;
                        docligne.DL_PrixRU = cmup;
                        docligne.DL_CMUP = cmup;
                        docligne.DT_No = 0;
                        docligne.AF_RefFourniss = "";
                        docligne.DL_TTC = 1;
                        docligne.DL_NoRef = 1;
                        docligne.DL_TypePL = 0;
                        docligne.DL_PUDevise = 0;
                        docligne.DL_NonLivre = 0;
                        docligne.DO_DateLivr = Convert.ToDateTime("1900-01-01").Date;
                        docligne.DL_DatePL = item.DO_Date; ;
                        docligne.DL_QteRessource = 0;
                        docligne.DL_DateAvancement = Convert.ToDateTime("1900-01-01").Date;
                        docligne.CA_Num = "";
                        docligne.DL_Frais = 0;
                        docligne.DL_Valorise = 1;
                        docligne.AC_RefClient = "";
                        docligne.DL_FactPoids = 0;
                        docligne.DL_Escompte = 0;
                        docligne.DL_PiecePL = "";
                        docligne.DL_NoColis = "";
                        docligne.DL_NoLink = 0;
                        docligne.DL_Ligne = 1000;
                        docligne.cbProt = 0;
                        docligne.cbFlag = 0;
                       
                        docligne.DL_QtePL = item.Dl_qte;
                        docligne.EU_Enumere = item.EU_Enumere;
                        docentet.DO_Ref = "";
                        decimal remise = 0;
                        decimal remise_valeur;
                        int cat_tarif = 0;
                        try
                        {
                            cat_tarif = dbProd.F_COMPTET.FirstOrDefault(cc => cc.CT_Num == commande.DO_Tiers).N_CatTarif.Value;
                            remise = dbProd.F_ARTCLIENT.FirstOrDefault(cc => cc.AR_Ref == item.ar_ref && cc.AC_Categorie == cat_tarif).AC_Remise.Value;
                            remise_valeur = remise / 100;
                        }
                        catch
                        {
                            remise = 0;
                            remise_valeur = 0;
                        }

                        if (taxe != -1 && remise == 0)
                        {
                            docligne.DL_Taxe1 = taxe;
                            docligne.DL_TypeTaux1 = 0;
                            docligne.DL_TypeTaxe1 = 0;
                            docligne.DL_Taxe2 = 0;
                            docligne.DL_TypeTaux2 = 0;
                            docligne.DL_TypeTaxe2 = 0;
                            docligne.DL_Taxe3 = 0;
                            docligne.DL_TypeTaux3 = 0;
                            docligne.DL_TypeTaxe3 = 0;
                            docligne.DL_CodeTaxe1 = code_taxe;
                            docligne.DL_MontantHT = item.dl_montantTTC / (1 + (taxe / 100));
                            docligne.DL_PrixUnitaire = item.pu / (1 + (taxe / 100));
                        }
                        if (taxe == -1 && remise != 0)
                        {
                            decimal prixunitaireRemise = item.pu / (1 - remise_valeur);
                            docligne.DL_Taxe1 = 0;
                            docligne.DL_TypeTaux1 = 0;
                            docligne.DL_TypeTaxe1 = 0;
                            docligne.DL_Taxe2 = 0;
                            docligne.DL_TypeTaux2 = 0;
                            docligne.DL_TypeTaxe2 = 0;
                            docligne.DL_Taxe3 = 0;
                            docligne.DL_TypeTaux3 = 0;
                            docligne.DL_TypeTaxe3 = 0;
                            docligne.DL_Remise01REM_Valeur = remise;
                            docligne.DL_Remise01REM_Type = 1;
                            docligne.DL_PrixUnitaire = prixunitaireRemise;
                            docligne.DL_PUTTC = prixunitaireRemise;
                            docligne.DL_MontantHT = item.dl_montantHT;
                            docligne.DL_MontantTTC = item.dl_montantTTC;
                        }

                        if (taxe == -1 && remise == 0)
                        {
                            docligne.DL_Taxe1 = 0;
                            docligne.DL_TypeTaux1 = 0;
                            docligne.DL_TypeTaxe1 = 0;
                            docligne.DL_Taxe2 = 0;
                            docligne.DL_TypeTaux2 = 0;
                            docligne.DL_TypeTaxe2 = 0;
                            docligne.DL_Taxe3 = 0;
                            docligne.DL_TypeTaux3 = 0;
                            docligne.DL_TypeTaxe3 = 0;
                            docligne.DL_Remise01REM_Valeur = 0;
                            docligne.DL_Remise01REM_Type = 0;
                            docligne.DL_PrixUnitaire = item.pu;
                            docligne.DL_PUTTC = item.pu;
                            docligne.DL_MontantHT = item.dl_montantHT;
                            docligne.DL_MontantTTC = item.dl_montantTTC;
                        }

                        if (taxe != -1 && remise != 0)
                        {
                            decimal prix_unitaires;
                            decimal mtht;

                            docligne.DL_Taxe1 = taxe;
                            docligne.DL_TypeTaux1 = 0;
                            docligne.DL_TypeTaxe1 = 0;
                            docligne.DL_CodeTaxe1 = code_taxe;
                            docligne.DL_Taxe2 = 0;
                            docligne.DL_TypeTaux2 = 0;
                            docligne.DL_TypeTaxe2 = 0;
                            docligne.DL_Taxe3 = 0;
                            docligne.DL_TypeTaux3 = 0;
                            docligne.DL_TypeTaxe3 = 0;
                            mtht = item.dl_montantTTC / (1 + (taxe / 100));
                            docligne.DL_PUTTC = item.pu / (1 - remise_valeur);
                            prix_unitaires = item.pu / (1 + (taxe / 100));
                            prix_unitaires = prix_unitaires / (1 - remise_valeur);
                            docligne.DL_PrixUnitaire = prix_unitaires;
                            docligne.DL_MontantHT = mtht;
                            docligne.DL_Remise01REM_Valeur = remise;
                            docligne.DL_Remise01REM_Type = 1;
                        }
                        try
                        {
                            dbProd.F_DOCLIGNE.Add(docligne);
                            dbProd.SaveChanges();
                        }
                        catch
                        {

                        }
                    #endregion

                    }
                    conn.Close();
                }
                transaction.Complete();
            }

        }

        private void update_ligne(Cmdentete commande)
        {
            using (var transaction = new System.Transactions.TransactionScope())
            {
                using (EntityConnection conn = new EntityConnection("name=COG_KSEntities"))
                {
                    conn.Open();
                    List<F_DOCLIGNE> ligne_supp = new List<F_DOCLIGNE>();
                    ligne_supp = db.F_DOCLIGNE.Where(cc => cc.DO_Piece == commande.DO_Piece).ToList();
                    foreach (var item in ligne_supp)
                    {
                        db.F_DOCLIGNE.Remove(item);
                    }
                    db.SaveChanges();
                    #region docligne

                    foreach (var item in commande.lignes)
                    {
                        decimal cmup;
                        try
                        {
                            decimal montant = db.F_ARTSTOCK.Where(cc => cc.AR_Ref == item.ar_ref).Sum(cc => cc.AS_MontSto).Value;
                            decimal Qte = db.F_ARTSTOCK.Where(cc => cc.AR_Ref == item.ar_ref).Sum(cc => cc.AS_QteSto).Value;

                            cmup = montant / Qte;
                        }
                        catch
                        {
                            cmup = item.pu;
                        }

                        decimal taxe;
                        string code_taxe;
                        string code_famille;
                        try
                        {
                            try
                            {
                                code_taxe = db.F_ARTCOMPTA.FirstOrDefault(cc => cc.AR_Ref == item.ar_ref && cc.ACP_Type == 0 && cc.ACP_Champ == 1).ACP_ComptaCPT_Taxe1;
                                taxe = db.F_TAXE.FirstOrDefault(cc => cc.TA_Code == code_taxe).TA_Taux.Value;
                            }
                            catch
                            {
                                code_famille = db.F_ARTICLE.FirstOrDefault(cc => cc.AR_Ref == item.ar_ref).FA_CodeFamille;
                                code_taxe = db.F_FAMCOMPTA.FirstOrDefault(cc => cc.FA_CodeFamille == code_famille && cc.FCP_Type == 0 && cc.FCP_Champ == 1).FCP_ComptaCPT_Taxe1;
                                taxe = db.F_TAXE.FirstOrDefault(cc => cc.TA_Code == code_taxe).TA_Taux.Value;
                            }
                        }
                        catch
                        {
                            taxe = -1;
                            code_taxe = null;
                        }


                        F_DOCLIGNE docligne = new F_DOCLIGNE();
                        docligne.DO_Domaine = 0;
                        docligne.DO_Type = 1;
                        docligne.CT_Num = item.ct_num;
                        docligne.PF_Num = "";
                        docligne.DO_Piece = commande.DO_Piece;
                        docligne.DO_Date = item.DO_Date;
                        docligne.AR_Ref = item.ar_ref;
                        docligne.DL_Design = item.dl_design;
                        docligne.DL_Qte = item.Dl_qte;
                        docligne.EU_Qte = item.qte_colisee;
                        docligne.DL_PrixUnitaire = item.pu;
                        docligne.DL_MvtStock = 0;
                        int de_no;
                        try
                        {
                            de_no = dbProd.F_COMPTET.FirstOrDefault(cc => cc.CT_Num == item.ct_num).DE_No.Value;
                            if (de_no == 0)
                                de_no = 4;
                        }
                        catch
                        {
                            de_no = 4;
                        }
                        docligne.DE_No = de_no;
                        docligne.cbDE_No = de_no;
                        docligne.DL_No = db.F_DOCLIGNE.Max(l => l.DL_No) + 1;
                        docligne.DL_PieceBC = "";
                        docligne.DL_PieceBL = "";
                        docligne.DL_DateBL = item.DO_Date;
                        docligne.DL_DateBC = item.DO_Date;
                        docligne.DO_Ref = "";
                        docligne.DL_TNomencl = 0;
                        docligne.DL_TRemExep = 0;
                        docligne.DL_TRemPied = 0;
                        docligne.DL_QteBC = docligne.DL_Qte;
                        docligne.DL_QteBL = 0;
                        docligne.DL_PoidsBrut = 0;
                        docligne.DL_PoidsNet = 0;
                        docligne.DL_PUBC = 0;
                        docligne.DL_Remise01REM_Type = 0;
                        docligne.DL_Remise02REM_Type = 0;
                        docligne.DL_Remise02REM_Valeur = 0;
                        docligne.DL_Remise03REM_Type = 0;
                        docligne.DL_Remise03REM_Valeur = 0;
                        docligne.CO_No = commande.CO_No;
                        docligne.AG_No1 = 0;
                        docligne.AG_No2 = 0;
                        docligne.DL_PrixRU = cmup;
                        docligne.DL_CMUP = cmup;
                        docligne.DT_No = 0;
                        docligne.AF_RefFourniss = "";
                        docligne.DL_TTC = 1;
                        docligne.DL_NoRef = 1;
                        docligne.DL_TypePL = 0;
                        docligne.DL_PUDevise = 0;
                        docligne.DL_NonLivre = 0;
                        docligne.DO_DateLivr = Convert.ToDateTime("1900-01-01").Date;
                        docligne.DL_DatePL = item.DO_Date; ;
                        docligne.DL_QteRessource = 0;
                        docligne.DL_DateAvancement = Convert.ToDateTime("1900-01-01").Date;
                        docligne.DL_PUTTC = item.pu;
                        docligne.CA_Num = "";
                        docligne.DL_Frais = 0;
                        docligne.DL_Valorise = 1;
                        docligne.AC_RefClient = "";
                        docligne.DL_MontantHT = item.dl_montantHT;
                        docligne.DL_MontantTTC = item.dl_montantTTC;
                        docligne.DL_FactPoids = 0;
                        docligne.DL_Escompte = 0;
                        docligne.DL_PiecePL = "";
                        docligne.DL_NoColis = "";
                        docligne.DL_NoLink = 0;
                        docligne.DL_Ligne = 1000;
                        docligne.cbProt = 0;
                        docligne.cbFlag = 0;
                       
                        docligne.DL_QtePL = item.Dl_qte;
                        docligne.EU_Enumere = item.EU_Enumere;
                        docligne.DO_Ref = "";
                        decimal remise = 0;
                        decimal remise_valeur;
                        int cat_tarif = 0;
                        try
                        {
                            cat_tarif = dbProd.F_COMPTET.FirstOrDefault(cc => cc.CT_Num == commande.DO_Tiers).N_CatTarif.Value;
                            remise = dbProd.F_ARTCLIENT.FirstOrDefault(cc => cc.AR_Ref == item.ar_ref && cc.AC_Categorie == cat_tarif).AC_Remise.Value;
                            remise_valeur = remise / 100;
                        }
                        catch
                        {
                            remise = 0;
                            remise_valeur = 0;
                        }


                        if (taxe != -1 && remise == 0)
                        {
                            docligne.DL_Taxe1 = taxe;
                            docligne.DL_TypeTaux1 = 0;
                            docligne.DL_TypeTaxe1 = 0;
                            docligne.DL_Taxe2 = 0;
                            docligne.DL_TypeTaux2 = 0;
                            docligne.DL_TypeTaxe2 = 0;
                            docligne.DL_Taxe3 = 0;
                            docligne.DL_TypeTaux3 = 0;
                            docligne.DL_TypeTaxe3 = 0;
                            docligne.DL_CodeTaxe1 = code_taxe;
                            docligne.DL_MontantHT = item.dl_montantTTC / (1 + (taxe / 100));
                            docligne.DL_PrixUnitaire = item.pu / (1 + (taxe / 100));
                        }
                        if (taxe == -1 && remise != 0)
                        {
                            decimal prixunitaireRemise = item.pu / (1 - remise_valeur);
                            docligne.DL_Taxe1 = 0;
                            docligne.DL_TypeTaux1 = 0;
                            docligne.DL_TypeTaxe1 = 0;
                            docligne.DL_Taxe2 = 0;
                            docligne.DL_TypeTaux2 = 0;
                            docligne.DL_TypeTaxe2 = 0;
                            docligne.DL_Taxe3 = 0;
                            docligne.DL_TypeTaux3 = 0;
                            docligne.DL_TypeTaxe3 = 0;
                            docligne.DL_Remise01REM_Valeur = remise;
                            docligne.DL_Remise01REM_Type = 1;
                            docligne.DL_PrixUnitaire = prixunitaireRemise;
                            docligne.DL_PUTTC = prixunitaireRemise;
                            docligne.DL_MontantHT = item.dl_montantHT;
                            docligne.DL_MontantTTC = item.dl_montantTTC;
                        }

                        if (taxe == -1 && remise == 0)
                        {
                            docligne.DL_Taxe1 = 0;
                            docligne.DL_TypeTaux1 = 0;
                            docligne.DL_TypeTaxe1 = 0;
                            docligne.DL_Taxe2 = 0;
                            docligne.DL_TypeTaux2 = 0;
                            docligne.DL_TypeTaxe2 = 0;
                            docligne.DL_Taxe3 = 0;
                            docligne.DL_TypeTaux3 = 0;
                            docligne.DL_TypeTaxe3 = 0;
                            docligne.DL_Remise01REM_Valeur = 0;
                            docligne.DL_Remise01REM_Type = 0;
                            docligne.DL_PrixUnitaire = item.pu;
                            docligne.DL_PUTTC = item.pu;
                            docligne.DL_MontantHT = item.dl_montantHT;
                            docligne.DL_MontantTTC = item.dl_montantTTC;
                        }

                        if (taxe != -1 && remise != 0)
                        {
                            decimal prix_unitaires;
                            decimal mtht;

                            docligne.DL_Taxe1 = taxe;
                            docligne.DL_TypeTaux1 = 0;
                            docligne.DL_TypeTaxe1 = 0;
                            docligne.DL_CodeTaxe1 = code_taxe;
                            docligne.DL_Taxe2 = 0;
                            docligne.DL_TypeTaux2 = 0;
                            docligne.DL_TypeTaxe2 = 0;
                            docligne.DL_Taxe3 = 0;
                            docligne.DL_TypeTaux3 = 0;
                            docligne.DL_TypeTaxe3 = 0;
                            mtht = item.dl_montantTTC / (1 + (taxe / 100));
                            docligne.DL_PUTTC = item.pu / (1 - remise_valeur);
                            prix_unitaires = item.pu / (1 + (taxe / 100));
                            prix_unitaires = prix_unitaires / (1 - remise_valeur);
                            docligne.DL_PrixUnitaire = prix_unitaires;
                            docligne.DL_MontantHT = mtht;
                            docligne.DL_Remise01REM_Valeur = remise;
                            docligne.DL_Remise01REM_Type = 1;
                        }

                        try
                        {
                            db.F_DOCLIGNE.Add(docligne);
                            db.SaveChanges();
                        }
                        catch
                        {

                        }
                    #endregion
                    }
                    conn.Close();
                }
                transaction.Complete();
            }
        }

        public void CreerBL(Cmdentete commande)
        {
            using (var transaction = new System.Transactions.TransactionScope())
            {
                using (EntityConnection conn = new EntityConnection("name=COG_KSEntities"))
                {
                    conn.Open();
                    do_piece_origine = db.F_DOCCURRENTPIECE.FirstOrDefault(cc => cc.DC_Domaine == 0 && cc.DC_IdCol == 1 && cc.DC_Souche == 0).DC_Piece;
                    do_pieceaff = do_piece_origine;
                    string do_piececmd = do_piece_origine;

                    #region docentet
                    F_DOCENTETE docentet = new F_DOCENTETE();
                    docentet.DO_Domaine = 0;
                    docentet.DO_Type = 1;
                    docentet.DO_Devise = 0;
                    docentet.DO_Period = 1;
                    docentet.DO_Expedit = 1;
                    docentet.DO_Souche = 0;
                    docentet.DO_Piece = do_piececmd;
                    docentet.DO_Tiers = commande.DO_Tiers;
                    docentet.CT_NumPayeur = commande.DO_Tiers;
                    docentet.DO_Date = commande.DO_Date;
                    docentet.DO_Statut = 2;
                    docentet.CO_No = commande.CO_No;
                    int de_no;
                    try
                    {
                        de_no = dbProd.F_COMPTET.FirstOrDefault(cc => cc.CT_Num == commande.DO_Tiers).DE_No.Value;
                        if (de_no == 0)
                            de_no = 4;
                    }
                    catch
                    {
                        de_no = 4;
                    }
                    docentet.DE_No = de_no;
                    docentet.DO_Reliquat = 0;
                    docentet.DO_Imprim = 0;
                    docentet.DO_Ventile = 0;
                    docentet.AB_No = 0;
                    docentet.CG_Num = "411000";
                    docentet.CA_No = 0;
                    docentet.DO_Transfere = 0;
                    docentet.DO_Cloture = 0;
                    docentet.DO_NoWeb = "";
                    docentet.DO_Provenance = 0;
                    docentet.CA_NumIFRS = "";
                    docentet.DO_TypeFrais = 0;
                    docentet.DO_Motif = "";
                    docentet.DO_Contact = "";
                    docentet.DO_Condition = 1;
                    docentet.DO_TypeFranco = 0;
                    docentet.DO_Taxe1 = 0;
                    docentet.DO_Taxe2 = 0;
                    docentet.DO_Taxe3 = 0;
                    docentet.DO_TypeTaux1 = 0;
                    docentet.DO_TypeTaux2 = 0;
                    docentet.DO_TypeTaux3 = 0;
                    docentet.DO_TypeTaxe1 = 0;
                    docentet.DO_TypeTaxe2 = 0;
                    docentet.DO_TypeTaxe3 = 0;
                    docentet.DO_DateLivrRealisee = Convert.ToDateTime("1900-01-01").Date;
                    docentet.DO_DateExpedition = Convert.ToDateTime("1900-01-01").Date;
                    docentet.DO_FactureFrs = "";
                    docentet.DO_PieceOrig = "";
                 
                    docentet.DO_DemandeRegul = 0;
                    docentet.ET_No = 0;
                    docentet.DO_Valide = 0;
                    docentet.DO_Coffre = 0;
                    int lino;
                    try
                    {
                        lino = db.F_LIVRAISON.FirstOrDefault(l => l.CT_Num == commande.DO_Tiers).LI_No;
                        docentet.LI_No = lino;
                        docentet.cbLI_No = lino;
                    }
                    catch
                    {
                        docentet.LI_No = null;
                        docentet.cbLI_No = null;
                    }
                    docentet.CA_Num = "";
                    docentet.DO_Ecart = 0;
                    docentet.DO_Regime = 21;
                    docentet.DO_Transaction = 11;
                    docentet.DO_Cours = 0;
                    docentet.DO_NbFacture = 1;
                    docentet.DO_BLFact = 0;
                    docentet.DO_TxEscompte = 0;
                    docentet.DO_Coord01 = "";
                    docentet.DO_Coord02 = "";
                    docentet.DO_Coord03 = "";
                    docentet.DO_Coord04 = "";
                    docentet.DO_DateLivr = Convert.ToDateTime("1900-01-01").Date;
                    docentet.DO_Tarif = 1;
                    docentet.DO_Colisage = 1;
                    docentet.DO_TypeColis = 1;
                    docentet.DO_Langue = 0;
                    docentet.N_CatCompta = 1;
                    docentet.DO_DebutAbo = Convert.ToDateTime("1900-01-01").Date;
                    docentet.DO_FinAbo = Convert.ToDateTime("1900-01-01").Date;
                    docentet.DO_DebutPeriod = Convert.ToDateTime("1900-01-01").Date;
                    docentet.DO_FinPeriod = Convert.ToDateTime("1900-01-01").Date;
                    docentet.DO_Heure = "000112441";
                    docentet.CO_NoCaissier = 0;
                    docentet.DO_Attente = 0;
                    docentet.MR_No = 0;
                    docentet.DO_ValFrais = 0;
                    docentet.DO_TypeLigneFrais = 1;
                    docentet.DO_TypeLigneFranco = 0;
                    docentet.DO_ValFranco = 0;
                    docentet.DO_MajCpta = 0;
                    docentet.DO_FactureElec = 0;
                    docentet.DO_TypeTransac = 0;
                    docentet.DO_Ref = "";
                    docentet.DO_TotalHT = commande.dl_montantTTC;
                    docentet.cbProt = 0;
                    docentet.cbFlag = 0;
                    docentet.cbCreateur = "ERP1";
                    docentet.PROFIL = commande.Profil;
                    docentet.BCVALIDE = commande.BCVALIDE;
                    try
                    {
                        docentet.DO_Tarif = short.Parse(commande.n_cattarif.ToString());
                    }
                    catch
                    {
                        docentet.DO_Tarif = 0;
                    }
                    db.F_DOCENTETE.Add(docentet);
                    db.SaveChanges();
                    #endregion

                    #region DOCCURRENTPIECE

                    F_DOCCURRENTPIECE currentpiece = new F_DOCCURRENTPIECE();
                    currentpiece = db.F_DOCCURRENTPIECE.FirstOrDefault(cc => cc.DC_Domaine == 0 && cc.DC_IdCol == 1 && cc.DC_Souche == 0);
                    string num_Piece = do_piece_origine;
                    int length_numpiece = num_Piece.Length - 3;
                    string prefixe = num_Piece.Substring(3, length_numpiece);
                    string suffixe = num_Piece.Substring(0, 3);
                    int npiece = int.Parse(prefixe);
                    npiece = npiece + 1;
                    string do_pieceS = suffixe + npiece.ToString();
                    currentpiece.DC_Piece = do_pieceS;
                    db.SaveChanges();
                    #endregion

                    #region docregl

                    F_DOCREGL docregl = new F_DOCREGL();
                    docregl.DR_Date = commande.DO_Date;
                    docregl.DO_Domaine = 0;
                    docregl.DO_Type = 1;
                    docregl.DO_Piece = do_piececmd;
                    docregl.DR_TypeRegl = 2;
                    docregl.DR_Equil = 1;
                    docregl.EC_No = 0;
                    docregl.DR_Regle = 0;
                    docregl.N_Reglement = 1;
                    docregl.DR_Libelle = "";
                    docregl.DR_Pourcent = 0;
                    docregl.DR_Montant = 0;
                    docregl.DR_MontantDev = 0;
                    docregl.cbProt = 0;
                    db.F_DOCREGL.Add(docregl);
                    db.SaveChanges();
                    #endregion

                    #region docligne

                    foreach (var item in commande.lignes)
                    {
                        decimal cmup;
                        try
                        {
                            decimal montant = db.F_ARTSTOCK.Where(cc => cc.AR_Ref == item.ar_ref).Sum(cc => cc.AS_MontSto).Value;
                            decimal Qte = db.F_ARTSTOCK.Where(cc => cc.AR_Ref == item.ar_ref).Sum(cc => cc.AS_QteSto).Value;

                            cmup = montant / Qte;
                        }
                        catch
                        {
                            cmup = item.pu;
                        }

                        decimal taxe;
                        string code_taxe;
                        string code_famille;
                        try
                        {
                            try
                            {
                                code_taxe = db.F_ARTCOMPTA.FirstOrDefault(cc => cc.AR_Ref == item.ar_ref && cc.ACP_Type == 0 && cc.ACP_Champ == 1).ACP_ComptaCPT_Taxe1;
                                taxe = db.F_TAXE.FirstOrDefault(cc => cc.TA_Code == code_taxe).TA_Taux.Value;
                            }
                            catch
                            {
                                code_famille = db.F_ARTICLE.FirstOrDefault(cc => cc.AR_Ref == item.ar_ref).FA_CodeFamille;
                                code_taxe = db.F_FAMCOMPTA.FirstOrDefault(cc => cc.FA_CodeFamille == code_famille && cc.FCP_Type == 0 && cc.FCP_Champ == 1).FCP_ComptaCPT_Taxe1;
                                taxe = db.F_TAXE.FirstOrDefault(cc => cc.TA_Code == code_taxe).TA_Taux.Value;
                            }
                        }
                        catch
                        {
                            taxe = -1;
                            code_taxe = null;
                        }


                        F_DOCLIGNE docligne = new F_DOCLIGNE();
                        docligne.DO_Domaine = 0;
                        docligne.DO_Type = 1;
                        docligne.CT_Num = item.ct_num;
                        docligne.PF_Num = "";
                        docligne.DO_Piece = do_piececmd;
                        docligne.DO_Date = item.DO_Date;
                        docligne.AR_Ref = item.ar_ref;
                        docligne.DL_Design = item.dl_design;
                        docligne.DL_Qte = item.Dl_qte;
                        docligne.EU_Qte = item.qte_colisee;
                        docligne.DL_PrixUnitaire = item.pu;
                        docligne.DL_MvtStock = 0;
                        try
                        {
                            de_no = dbProd.F_COMPTET.FirstOrDefault(cc => cc.CT_Num == item.ct_num).DE_No.Value;
                            if (de_no == 0)
                                de_no = 4;
                        }
                        catch
                        {
                            de_no = 4;
                        }
                        docligne.DE_No = de_no;
                        docligne.cbDE_No = de_no;
                        docligne.DL_No = db.F_DOCLIGNE.Max(l => l.DL_No) + 1;
                        docligne.DL_PieceBC = "";
                        docligne.DL_PieceBL = "";
                        docligne.DL_DateBL = item.DO_Date;
                        docligne.DL_DateBC = item.DO_Date;
                        docligne.DO_Ref = "";
                        docligne.DL_TNomencl = 0;
                        docligne.DL_TRemExep = 0;
                        docligne.DL_TRemPied = 0;
                        docligne.DL_QteBC = docligne.DL_Qte;
                        docligne.DL_QteBL = 0;
                        docligne.DL_PoidsBrut = 0;
                        docligne.DL_PoidsNet = 0;
                        docligne.DL_PUBC = 0;
                        docligne.DL_Remise01REM_Type = 0;
                        docligne.DL_Remise02REM_Type = 0;
                        docligne.DL_Remise02REM_Valeur = 0;
                        docligne.DL_Remise03REM_Type = 0;
                        docligne.DL_Remise03REM_Valeur = 0;
                        docligne.CO_No = commande.CO_No;
                        docligne.AG_No1 = 0;
                        docligne.AG_No2 = 0;
                        docligne.DL_PrixRU = cmup;
                        docligne.DL_CMUP = cmup;
                        docligne.DT_No = 0;
                        docligne.AF_RefFourniss = "";
                        docligne.DL_TTC = 1;
                        docligne.DL_NoRef = 1;
                        docligne.DL_TypePL = 0;
                        docligne.DL_PUDevise = 0;
                        docligne.DL_NonLivre = 0;
                        docligne.DO_DateLivr = Convert.ToDateTime("1900-01-01").Date;
                        docligne.DL_DatePL = item.DO_Date; ;
                        docligne.DL_QteRessource = 0;
                        docligne.DL_DateAvancement = Convert.ToDateTime("1900-01-01").Date;
                        docligne.DL_PUTTC = item.pu;
                        docligne.CA_Num = "";
                        docligne.DL_Frais = 0;
                        docligne.DL_Valorise = 1;
                        docligne.AC_RefClient = "";
                        docligne.DL_MontantHT = item.dl_montantHT;
                        docligne.DL_MontantTTC = item.dl_montantTTC;
                        docligne.DL_FactPoids = 0;
                        docligne.DL_Escompte = 0;
                        docligne.DL_PiecePL = "";
                        docligne.DL_NoColis = "";
                        docligne.DL_NoLink = 0;
                        docligne.DL_Ligne = 1000;
                        docligne.cbProt = 0;
                        docligne.cbFlag = 0;
                       
                        docligne.DL_QtePL = item.Dl_qte;
                        docligne.EU_Enumere = item.EU_Enumere;
                        docligne.DO_Ref = "";
                        decimal remise = 0;
                        decimal remise_valeur;
                        int cat_tarif = 0;
                        try
                        {
                            cat_tarif = dbProd.F_COMPTET.FirstOrDefault(cc => cc.CT_Num == commande.DO_Tiers).N_CatTarif.Value;
                            remise = dbProd.F_ARTCLIENT.FirstOrDefault(cc => cc.AR_Ref == item.ar_ref && cc.AC_Categorie == cat_tarif).AC_Remise.Value;
                            remise_valeur = remise / 100;
                        }
                        catch
                        {
                            remise = 0;
                            remise_valeur = 0;
                        }


                        if (taxe != -1 && remise == 0)
                        {
                            docligne.DL_Taxe1 = taxe;
                            docligne.DL_TypeTaux1 = 0;
                            docligne.DL_TypeTaxe1 = 0;
                            docligne.DL_Taxe2 = 0;
                            docligne.DL_TypeTaux2 = 0;
                            docligne.DL_TypeTaxe2 = 0;
                            docligne.DL_Taxe3 = 0;
                            docligne.DL_TypeTaux3 = 0;
                            docligne.DL_TypeTaxe3 = 0;
                            docligne.DL_CodeTaxe1 = code_taxe;
                            docligne.DL_MontantHT = item.dl_montantTTC / (1 + (taxe / 100));
                            docligne.DL_PrixUnitaire = item.pu / (1 + (taxe / 100));
                        }
                        if (taxe == -1 && remise != 0)
                        {
                            decimal prixunitaireRemise = item.pu / (1 - remise_valeur);
                            docligne.DL_Taxe1 = 0;
                            docligne.DL_TypeTaux1 = 0;
                            docligne.DL_TypeTaxe1 = 0;
                            docligne.DL_Taxe2 = 0;
                            docligne.DL_TypeTaux2 = 0;
                            docligne.DL_TypeTaxe2 = 0;
                            docligne.DL_Taxe3 = 0;
                            docligne.DL_TypeTaux3 = 0;
                            docligne.DL_TypeTaxe3 = 0;
                            docligne.DL_Remise01REM_Valeur = remise;
                            docligne.DL_Remise01REM_Type = 1;
                            docligne.DL_PrixUnitaire = prixunitaireRemise;
                            docligne.DL_PUTTC = prixunitaireRemise;
                            docligne.DL_MontantHT = item.dl_montantHT;
                            docligne.DL_MontantTTC = item.dl_montantTTC;
                        }

                        if (taxe == -1 && remise == 0)
                        {
                            docligne.DL_Taxe1 = 0;
                            docligne.DL_TypeTaux1 = 0;
                            docligne.DL_TypeTaxe1 = 0;
                            docligne.DL_Taxe2 = 0;
                            docligne.DL_TypeTaux2 = 0;
                            docligne.DL_TypeTaxe2 = 0;
                            docligne.DL_Taxe3 = 0;
                            docligne.DL_TypeTaux3 = 0;
                            docligne.DL_TypeTaxe3 = 0;
                            docligne.DL_Remise01REM_Valeur = 0;
                            docligne.DL_Remise01REM_Type = 0;
                            docligne.DL_PrixUnitaire = item.pu;
                            docligne.DL_PUTTC = item.pu;
                            docligne.DL_MontantHT = item.dl_montantHT;
                            docligne.DL_MontantTTC = item.dl_montantTTC;
                        }

                        if (taxe != -1 && remise != 0)
                        {
                            decimal prix_unitaires;
                            decimal mtht;

                            docligne.DL_Taxe1 = taxe;
                            docligne.DL_TypeTaux1 = 0;
                            docligne.DL_TypeTaxe1 = 0;
                            docligne.DL_CodeTaxe1 = code_taxe;
                            docligne.DL_Taxe2 = 0;
                            docligne.DL_TypeTaux2 = 0;
                            docligne.DL_TypeTaxe2 = 0;
                            docligne.DL_Taxe3 = 0;
                            docligne.DL_TypeTaux3 = 0;
                            docligne.DL_TypeTaxe3 = 0;
                            mtht = item.dl_montantTTC / (1 + (taxe / 100));
                            docligne.DL_PUTTC = item.pu / (1 - remise_valeur);
                            prix_unitaires = item.pu / (1 + (taxe / 100));
                            prix_unitaires = prix_unitaires / (1 - remise_valeur);
                            docligne.DL_PrixUnitaire = prix_unitaires;
                            docligne.DL_MontantHT = mtht;
                            docligne.DL_Remise01REM_Valeur = remise;
                            docligne.DL_Remise01REM_Type = 1;
                        }

                        try
                        {
                            db.F_DOCLIGNE.Add(docligne);
                            db.SaveChanges();
                        }
                        catch
                        {

                        }
                    #endregion



                    }
                    conn.Close();
                }
                transaction.Complete();
            }
        }

        //Insérer la commande dans le BO
        public void CreerBLBO(Cmdentete commande)
        {
            using (var transaction = new System.Transactions.TransactionScope())
            {
                using (EntityConnection conn = new EntityConnection("name=PCM_KS_BOEntities"))
                {
                    conn.Open();
                    do_piece_origine = dbBO.F_DOCCURRENTPIECE.FirstOrDefault(cc => cc.DC_Domaine == 0 && cc.DC_IdCol == 1 && cc.DC_Souche == 0).DC_Piece;
                    do_pieceaff = do_piece_origine;
                    string do_piececmd = do_piece_origine;

                    #region docentet
                    F_DOCENTETE docentet = new F_DOCENTETE();
                    docentet.DO_Domaine = 0;
                    docentet.DO_Type = 1;
                    docentet.DO_Devise = 0;
                    docentet.DO_Period = 1;
                    docentet.DO_Expedit = 1;
                    docentet.DO_Souche = 0;
                    docentet.DO_Piece = do_piececmd;
                    docentet.DO_Tiers = commande.DO_Tiers;
                    docentet.CT_NumPayeur = commande.DO_Tiers;
                    docentet.DO_Date = commande.DO_Date;
                    docentet.DO_Statut = 2;
                    docentet.CO_No = commande.CO_No;
                    int de_no;
                    try
                    {
                        de_no = dbBO.F_COMPTET.FirstOrDefault(cc => cc.CT_Num == commande.DO_Tiers).DE_No.Value;
                        if (de_no == 0)
                            de_no = 4;
                    }
                    catch
                    {
                        de_no = 4;
                    }
                    docentet.DE_No = de_no;
                    docentet.DO_Reliquat = 0;
                    docentet.DO_Imprim = 0;
                    docentet.DO_Ventile = 0;
                    docentet.AB_No = 0;
                    docentet.CG_Num = "411000";
                    docentet.CA_No = 0;
                    docentet.DO_Transfere = 0;
                    docentet.DO_Cloture = 0;
                    docentet.DO_NoWeb = "";
                    docentet.DO_Provenance = 0;
                    docentet.CA_NumIFRS = "";
                    docentet.DO_TypeFrais = 0;
                    docentet.DO_Motif = "";
                    docentet.DO_Contact = "";
                    docentet.DO_Condition = 1;
                    docentet.DO_TypeFranco = 0;
                    docentet.DO_Taxe1 = 0;
                    docentet.DO_Taxe2 = 0;
                    docentet.DO_Taxe3 = 0;
                    docentet.DO_TypeTaux1 = 0;
                    docentet.DO_TypeTaux2 = 0;
                    docentet.DO_TypeTaux3 = 0;
                    docentet.DO_TypeTaxe1 = 0;
                    docentet.DO_TypeTaxe2 = 0;
                    docentet.DO_TypeTaxe3 = 0;
                    docentet.DO_DateLivrRealisee = Convert.ToDateTime("1900-01-01").Date;
                    docentet.DO_DateExpedition = Convert.ToDateTime("1900-01-01").Date;
                    docentet.DO_FactureFrs = "";
                    docentet.DO_PieceOrig = "";

                    docentet.DO_DemandeRegul = 0;
                    docentet.ET_No = 0;
                    docentet.DO_Valide = 0;
                    docentet.DO_Coffre = 0;
                    int lino;
                    try
                    {
                        lino = dbBO.F_LIVRAISON.FirstOrDefault(l => l.CT_Num == commande.DO_Tiers).LI_No;
                        docentet.LI_No = lino;
                        docentet.cbLI_No = lino;
                    }
                    catch
                    {
                        docentet.LI_No = null;
                        docentet.cbLI_No = null;
                    }
                    docentet.CA_Num = "";
                    docentet.DO_Ecart = 0;
                    docentet.DO_Regime = 21;
                    docentet.DO_Transaction = 11;
                    docentet.DO_Cours = 0;
                    docentet.DO_NbFacture = 1;
                    docentet.DO_BLFact = 0;
                    docentet.DO_TxEscompte = 0;
                    docentet.DO_Coord01 = "";
                    docentet.DO_Coord02 = "";
                    docentet.DO_Coord03 = "";
                    docentet.DO_Coord04 = "";
                    docentet.DO_DateLivr = Convert.ToDateTime("1900-01-01").Date;
                    docentet.DO_Tarif = 1;
                    docentet.DO_Colisage = 1;
                    docentet.DO_TypeColis = 1;
                    docentet.DO_Langue = 0;
                    docentet.N_CatCompta = 1;
                    docentet.DO_DebutAbo = Convert.ToDateTime("1900-01-01").Date;
                    docentet.DO_FinAbo = Convert.ToDateTime("1900-01-01").Date;
                    docentet.DO_DebutPeriod = Convert.ToDateTime("1900-01-01").Date;
                    docentet.DO_FinPeriod = Convert.ToDateTime("1900-01-01").Date;
                    docentet.DO_Heure = "000112441";
                    docentet.CO_NoCaissier = 0;
                    docentet.DO_Attente = 0;
                    docentet.MR_No = 0;
                    docentet.DO_ValFrais = 0;
                    docentet.DO_TypeLigneFrais = 1;
                    docentet.DO_TypeLigneFranco = 0;
                    docentet.DO_ValFranco = 0;
                    docentet.DO_MajCpta = 0;
                    docentet.DO_FactureElec = 0;
                    docentet.DO_TypeTransac = 0;
                    docentet.DO_Ref = "";
                    docentet.DO_TotalHT = commande.dl_montantTTC;
                    docentet.cbProt = 0;
                    docentet.cbFlag = 0;
                    docentet.cbCreateur = "ERP1";
                    docentet.PROFIL = commande.Profil;
                    docentet.BCVALIDE = commande.BCVALIDE;
                    try
                    {
                        docentet.DO_Tarif = short.Parse(commande.n_cattarif.ToString());
                    }
                    catch
                    {
                        docentet.DO_Tarif = 0;
                    }
                    dbBO.F_DOCENTETE.Add(docentet);
                    dbBO.SaveChanges();
                    #endregion

                    #region DOCCURRENTPIECE

                    F_DOCCURRENTPIECE currentpiece = new F_DOCCURRENTPIECE();
                    currentpiece = dbBO.F_DOCCURRENTPIECE.FirstOrDefault(cc => cc.DC_Domaine == 0 && cc.DC_IdCol == 1 && cc.DC_Souche == 0);
                    string num_Piece = do_piece_origine;
                    int length_numpiece = num_Piece.Length - 3;
                    string prefixe = num_Piece.Substring(3, length_numpiece);
                    string suffixe = num_Piece.Substring(0, 3);
                    int npiece = int.Parse(prefixe);
                    npiece = npiece + 1;
                    string do_pieceS = suffixe + npiece.ToString();
                    currentpiece.DC_Piece = do_pieceS;
                    dbBO.SaveChanges();
                    #endregion

                    #region docregl

                    F_DOCREGL docregl = new F_DOCREGL();
                    docregl.DR_Date = commande.DO_Date;
                    docregl.DO_Domaine = 0;
                    docregl.DO_Type = 1;
                    docregl.DO_Piece = do_piececmd;
                    docregl.DR_TypeRegl = 2;
                    docregl.DR_Equil = 1;
                    docregl.EC_No = 0;
                    docregl.DR_Regle = 0;
                    docregl.N_Reglement = 1;
                    docregl.DR_Libelle = "";
                    docregl.DR_Pourcent = 0;
                    docregl.DR_Montant = 0;
                    docregl.DR_MontantDev = 0;
                    docregl.cbProt = 0;
                    dbBO.F_DOCREGL.Add(docregl);
                    dbBO.SaveChanges();
                    #endregion

                    #region docligne

                    foreach (var item in commande.lignes)
                    {
                        decimal cmup;
                        try
                        {
                            decimal montant = dbBO.F_ARTSTOCK.Where(cc => cc.AR_Ref == item.ar_ref).Sum(cc => cc.AS_MontSto).Value;
                            decimal Qte = dbBO.F_ARTSTOCK.Where(cc => cc.AR_Ref == item.ar_ref).Sum(cc => cc.AS_QteSto).Value;

                            cmup = montant / Qte;
                        }
                        catch
                        {
                            cmup = item.pu;
                        }

                        decimal taxe;
                        string code_taxe;
                        string code_famille;
                        try
                        {
                            try
                            {
                                code_famille = dbBO.F_ARTICLE.FirstOrDefault(cc => cc.AR_Ref == item.ar_ref).FA_CodeFamille;
                                code_taxe = "18";
                               //dbBO.F_FAMCOMPTA.FirstOrDefault(cc => cc.FA_CodeFamille == code_famille && cc.FCP_Type == 0 && cc.FCP_Champ == 1).FCP_ComptaCPT_Taxe1;
                                taxe = dbBO.F_TAXE.FirstOrDefault(cc => cc.TA_Code == code_taxe).TA_Taux.Value;
                            }
                            catch
                            {
                                code_taxe = dbBO.F_ARTCOMPTA.FirstOrDefault(cc => cc.AR_Ref == item.ar_ref && cc.ACP_Type == 0 && cc.ACP_Champ == 1).ACP_ComptaCPT_Taxe1;
                                taxe = dbBO.F_TAXE.FirstOrDefault(cc => cc.TA_Code == code_taxe).TA_Taux.Value;
                            }
                        }
                        catch
                        {
                            taxe = -1;
                            code_taxe = null;
                        }


                        F_DOCLIGNE docligne = new F_DOCLIGNE();
                        docligne.DO_Domaine = 0;
                        docligne.DO_Type = 1;
                        docligne.CT_Num = item.ct_num;
                        docligne.PF_Num = "";
                        docligne.DO_Piece = do_piececmd;
                        docligne.DO_Date = item.DO_Date;
                        docligne.AR_Ref = item.ar_ref;
                        docligne.DL_Design = item.dl_design;
                        docligne.DL_Qte = item.Dl_qte;
                        docligne.EU_Qte = item.qte_colisee;
                        docligne.DL_PrixUnitaire = item.pu;
                        docligne.DL_MvtStock = 0;
                        try
                        {
                            de_no = dbBO.F_COMPTET.FirstOrDefault(cc => cc.CT_Num == item.ct_num).DE_No.Value;
                            if (de_no == 0)
                                de_no = 4;
                        }
                        catch
                        {
                            de_no = 4;
                        }
                        docligne.DE_No = de_no;
                        docligne.cbDE_No = de_no;
                        docligne.DL_No = db.F_DOCLIGNE.Max(l => l.DL_No) + 1;
                        docligne.DL_PieceBC = "";
                        docligne.DL_PieceBL = "";
                        docligne.DL_DateBL = item.DO_Date;
                        docligne.DL_DateBC = item.DO_Date;
                        docligne.DO_Ref = "";
                        docligne.DL_TNomencl = 0;
                        docligne.DL_TRemExep = 0;
                        docligne.DL_TRemPied = 0;
                        docligne.DL_QteBC = docligne.DL_Qte;
                        docligne.DL_QteBL = 0;
                        docligne.DL_PoidsBrut = 0;
                        docligne.DL_PoidsNet = 0;
                        docligne.DL_PUBC = 0;
                        docligne.DL_Remise01REM_Type = 0;
                        docligne.DL_Remise02REM_Type = 0;
                        docligne.DL_Remise02REM_Valeur = 0;
                        docligne.DL_Remise03REM_Type = 0;
                        docligne.DL_Remise03REM_Valeur = 0;
                        docligne.CO_No = commande.CO_No;
                        docligne.AG_No1 = 0;
                        docligne.AG_No2 = 0;
                        docligne.DL_PrixRU = cmup;
                        docligne.DL_CMUP = cmup;
                        docligne.DT_No = 0;
                        docligne.AF_RefFourniss = "";
                        docligne.DL_TTC = 1;
                        docligne.DL_NoRef = 1;
                        docligne.DL_TypePL = 0;
                        docligne.DL_PUDevise = 0;
                        docligne.DL_NonLivre = 0;
                        docligne.DO_DateLivr = Convert.ToDateTime("1900-01-01").Date;
                        docligne.DL_DatePL = item.DO_Date; ;
                        docligne.DL_QteRessource = 0;
                        docligne.DL_DateAvancement = Convert.ToDateTime("1900-01-01").Date;
                        docligne.DL_PUTTC = item.pu;
                        docligne.CA_Num = "";
                        docligne.DL_Frais = 0;
                        docligne.DL_Valorise = 1;
                        docligne.AC_RefClient = "";
                        docligne.DL_MontantHT = item.dl_montantHT;
                        docligne.DL_MontantTTC = item.dl_montantTTC;
                        docligne.DL_FactPoids = 0;
                        docligne.DL_Escompte = 0;
                        docligne.DL_PiecePL = "";
                        docligne.DL_NoColis = "";
                        docligne.DL_NoLink = 0;
                        docligne.DL_Ligne = 1000;
                        docligne.cbProt = 0;
                        docligne.cbFlag = 0;

                        docligne.DL_QtePL = item.Dl_qte;
                        docligne.EU_Enumere = item.EU_Enumere;
                        docligne.DO_Ref = "";
                        decimal remise = 0;
                        decimal remise_valeur;
                        int cat_tarif = 0;
                        try
                        {
                            cat_tarif = dbBO.F_COMPTET.FirstOrDefault(cc => cc.CT_Num == commande.DO_Tiers).N_CatTarif.Value;
                            remise = dbBO.F_ARTCLIENT.FirstOrDefault(cc => cc.AR_Ref == item.ar_ref && cc.AC_Categorie == cat_tarif).AC_Remise.Value;
                            remise_valeur = remise / 100;
                        }
                        catch
                        {
                            remise = 0;
                            remise_valeur = 0;
                        }


                        if (taxe != -1 && remise == 0)
                        {
                            docligne.DL_Taxe1 = taxe;
                            docligne.DL_TypeTaux1 = 0;
                            docligne.DL_TypeTaxe1 = 0;
                            docligne.DL_Taxe2 = 0;
                            docligne.DL_TypeTaux2 = 0;
                            docligne.DL_TypeTaxe2 = 0;
                            docligne.DL_Taxe3 = 0;
                            docligne.DL_TypeTaux3 = 0;
                            docligne.DL_TypeTaxe3 = 0;
                            docligne.DL_CodeTaxe1 = code_taxe;
                            docligne.DL_MontantHT = item.dl_montantTTC / (1 + (taxe / 100));
                            docligne.DL_PrixUnitaire = item.pu / (1 + (taxe / 100));
                        }
                        if (taxe == -1 && remise != 0)
                        {
                            decimal prixunitaireRemise = item.pu / (1 - remise_valeur);
                            docligne.DL_Taxe1 = 0;
                            docligne.DL_TypeTaux1 = 0;
                            docligne.DL_TypeTaxe1 = 0;
                            docligne.DL_Taxe2 = 0;
                            docligne.DL_TypeTaux2 = 0;
                            docligne.DL_TypeTaxe2 = 0;
                            docligne.DL_Taxe3 = 0;
                            docligne.DL_TypeTaux3 = 0;
                            docligne.DL_TypeTaxe3 = 0;
                            docligne.DL_Remise01REM_Valeur = remise;
                            docligne.DL_Remise01REM_Type = 1;
                            docligne.DL_PrixUnitaire = prixunitaireRemise;
                            docligne.DL_PUTTC = prixunitaireRemise;
                            docligne.DL_MontantHT = item.dl_montantHT;
                            docligne.DL_MontantTTC = item.dl_montantTTC;
                        }

                        if (taxe == -1 && remise == 0)
                        {
                            docligne.DL_Taxe1 = 0;
                            docligne.DL_TypeTaux1 = 0;
                            docligne.DL_TypeTaxe1 = 0;
                            docligne.DL_Taxe2 = 0;
                            docligne.DL_TypeTaux2 = 0;
                            docligne.DL_TypeTaxe2 = 0;
                            docligne.DL_Taxe3 = 0;
                            docligne.DL_TypeTaux3 = 0;
                            docligne.DL_TypeTaxe3 = 0;
                            docligne.DL_Remise01REM_Valeur = 0;
                            docligne.DL_Remise01REM_Type = 0;
                            docligne.DL_PrixUnitaire = item.pu;
                            docligne.DL_PUTTC = item.pu;
                            docligne.DL_MontantHT = item.dl_montantHT;
                            docligne.DL_MontantTTC = item.dl_montantTTC;
                        }

                        if (taxe != -1 && remise != 0)
                        {
                            decimal prix_unitaires;
                            decimal mtht;

                            docligne.DL_Taxe1 = taxe;
                            docligne.DL_TypeTaux1 = 0;
                            docligne.DL_TypeTaxe1 = 0;
                            docligne.DL_CodeTaxe1 = code_taxe;
                            docligne.DL_Taxe2 = 0;
                            docligne.DL_TypeTaux2 = 0;
                            docligne.DL_TypeTaxe2 = 0;
                            docligne.DL_Taxe3 = 0;
                            docligne.DL_TypeTaux3 = 0;
                            docligne.DL_TypeTaxe3 = 0;
                            mtht = item.dl_montantTTC / (1 + (taxe / 100));
                            docligne.DL_PUTTC = item.pu / (1 - remise_valeur);
                            prix_unitaires = item.pu / (1 + (taxe / 100));
                            prix_unitaires = prix_unitaires / (1 - remise_valeur);
                            docligne.DL_PrixUnitaire = prix_unitaires;
                            docligne.DL_MontantHT = mtht;
                            docligne.DL_Remise01REM_Valeur = remise;
                            docligne.DL_Remise01REM_Type = 1;
                        }

                        try
                        {
                            dbBO.F_DOCLIGNE.Add(docligne);
                            dbBO.SaveChanges();
                        }
                        catch
                        {

                        }
                        #endregion



                    }
                    conn.Close();
                }
                transaction.Complete();
            }
        }
        
        [Authorize]
        [HttpPost]
        [Route("api/cmdlistAdd")]
        public IHttpActionResult PostF_lcmd(List<Cmdentete> liste_commande)
        {
                List<cmd> liste_cmdobj = new List<cmd>();
                foreach (var item in liste_commande)
                {
                    if (item.Profil == 0)
                    {
                        try
                        {
                            CreerBL(item);
                            add_base_prod(item);
                            cmd cmdcobj = new cmd();
                            cmdcobj.DO_Piece = do_pieceaff;
                            cmdcobj.id = item.id;
                            liste_cmdobj.Add(cmdcobj);
                           
                        }
                        catch (Exception ex)
                        {
                            return Ok(ex.InnerException.ToString());
                        }
                    }
                    else
                    {
                        try
                        {
                            F_DOCENTETE cmd = new F_DOCENTETE();
                            cmd = db.F_DOCENTETE.FirstOrDefault(cc => cc.DO_Piece == item.DO_Piece && cc.DO_Type == 1);
                            cmd.BCVALIDE = item.BCVALIDE;
                            if (item.BCVALIDE == "Valide")
                            {
                                update_ligne(item);
                                add_base_prod(item);
                            }
                            cmd cmdcobj = new cmd();
                            cmdcobj.DO_Piece = do_pieceaff;
                            cmdcobj.id = item.id;
                            liste_cmdobj.Add(cmdcobj);
                            db.SaveChanges();
                         
                        }
                        catch
                        {
                            try
                            {
                                CreerBL(item);
                                cmd cmdcobj = new cmd();
                                cmdcobj.DO_Piece = do_pieceaff;
                                cmdcobj.id = item.id;
                                liste_cmdobj.Add(cmdcobj);
                                try
                                {
                                    string registrationid = db.VM_Collaborateur.FirstOrDefault(cc => cc.CO_NO == item.CO_No).RegistrationID;
                                    string ct_intitule = db.F_COMPTETV.FirstOrDefault(cc => cc.CT_Num == item.DO_Tiers).CT_Intitule;
                                    SendNotif(ct_intitule, registrationid, do_pieceaff);
                                }
                                catch
                                {

                                }
                              

                            }
                            catch (Exception ex)
                            {
                                return Ok(ex.InnerException.ToString());
                            }
                        }
                    }
                }

                return Json(liste_cmdobj);
            
        }

        public string SendNotif(string ct_intitule, string RegistrationID, string Message)
        {
            try
            {
                string applicationID = "AAAAfe3fn4E:APA91bFN-YjbIyvy9-rkE07c7JY_8vPp9XSC1UK4_TpZfg19wnPWyjoAwfGidw_0k95wnIxz-8bjjIfKPjBQERkFVxxhix_M1yiOqJS8N_j_7o28wyORqR30Vb5ff23vRsl9T_HCjAXy";

                string senderId = "540861767553";

                string deviceId = RegistrationID;

                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentType = "application/json";
                var data = new
                {
                    to = deviceId,
                    data = new {
                        type = 10,
                        do_piece = Message,
                        client = ct_intitule
                       
                    }
                };
                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.ContentLength = byteArray.Length;

        Stream dataStream = tRequest.GetRequestStream();
        dataStream.Write(byteArray, 0, byteArray.Length);
        dataStream.Close();

        WebResponse tResponse = tRequest.GetResponse();

        dataStream = tResponse.GetResponseStream();

        StreamReader tReader = new StreamReader(dataStream);

        String sResponseFromServer = tReader.ReadToEnd();


        tReader.Close();
        dataStream.Close();
        tResponse.Close();
        return sResponseFromServer;
           
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                return str;
            }
        }
 
    }
}
