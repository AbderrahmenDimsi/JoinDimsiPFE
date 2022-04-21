using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Definitions
{
   public  class Question

    {
        // definition de la classe
        public const String EntityName = "Question";
        public const String EntityCollectionName = "Questions";

        // definition des champs 
       
            public int id { get; set; }
            public string Enonce { get; set; }
            public string Niveau_Dificullte { get; set; }
            public string Module { get; set; }


         //les relations
        
             public virtual ICollection<Test> Tests { get; set; }
             public virtual ICollection<Repons> Reponss { get; set; }

        public Question()
        {

        }
        public Question(string Enonce, string Niveau_Dificullte , string Module )
        {
            this.Enonce = Enonce;
            this.Niveau_Dificullte = Niveau_Dificullte;
            this.Module = Module; 
        }
        
     


    }
}
