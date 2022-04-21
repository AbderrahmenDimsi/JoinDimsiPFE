using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Definitions
{
  public   class Repons
    {


        // definition de la classe
        public const String EntityName = "Reponse";
        public const String EntityCollectionName = "Reponses";



        //definiion des champs
       
            public int id { get; set; }
            public string Reponse { get; set; }
            public Boolean Validation { get; set; }

           public virtual Question Question { get; set; }

        public Repons(String Reponse , bool Validation)
            {
            this.Reponse = Reponse;
            this.Validation = Validation;

            }

        public Repons()
        {

        }






    }
}
