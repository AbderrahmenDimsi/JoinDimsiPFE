using System;


namespace Model.Definitions
{
    public class CandidatDefinition
    {
        //definition de la classe 

        public const String EntityName = "dim_candidat";
        public const String EntityCollectionName = "dim_candidats";

        //les entités




        public static class Columns
        {
            public const string Id = "dim_candidatid";
            public const string Nom = "dim_name";
            public const string prenom = "dim_prenom";
            public const string Nom_Complet = "dim_nom_complet";
            public const string civilite = "dim_civilite";
            public const string Etat_Civile = "dim_etat_civile";

            public const string Telephone = "dim_telephone";
            public const string Adresse = "dim_adresse";

            public const string Experience = "dim_experience";
            public const string email = "dim_email";
            public const string ResultatTest = "dim_resultattest";
            public const string statuscode = "statuscode ";
            public const string courrier = "dim_courrier";
            public const string numero = "dim_numro_telephone";
        }

    }
}
























