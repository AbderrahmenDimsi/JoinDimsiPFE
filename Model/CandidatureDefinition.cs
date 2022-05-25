using System;


namespace Model.Definitions
{
    public class CandidatureDefinition
    {
        //definition de la classe 

        public const String EntityName = "dim_affectation";
        public const String EntityCollectionName = "dim_affectations";

        //les entités




        public static class Columns
        {
            public const string Id = "dim_affectationid";
            public const string Nom = "dim_name";
            public const string date_examen = "dim_date_examen";
            public const string resultat_test = "dim_resultat_test";
            public const string creer_le = "createdon";
            public const string statut = "statecode";
            public const string raison_statut = "statuscode";
            public const string candidat = "dim_candidataffid";


        }

    }
}

