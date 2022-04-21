using System;
using System.Collections.Generic;
using System.Text;


namespace Model.Definitions
{
    public class ReponsDefinition
    {


        // definition de la classe
        public const String EntityName = "dim_reponse";
        public const String EntityCollectionName = "dim_reponses";



        //definiion des champs
        public static  class Columns {

        public const string Id = "dim_reponseid";
        public const string Reponse = "dim_reponse";
        public const string Validation = "dim_validation";

        public const string ReponseQuetion = "dim_question_reponseid";
    }
    }



    }

