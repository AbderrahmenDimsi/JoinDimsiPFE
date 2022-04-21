using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Definitions
{
   public   class Test
    {

        //definition de la class
        public const string EntityName = "Test";
        public const string EntityCollectionName = "Tests";

        //definition des entités
      
            public int Id { get; set; }
            public string Titre { get; set; }
            public string Categorie { get; set; }
            public string Module { get; set; }
            public int Niveau_difficulte_moyenne { get; set; }

        public Test()
        {

        }
        public Test(string titre ,string categorie,string module , int niveau_difficulte_moyenne)
        {
            this.Titre = titre;
            this.Categorie = categorie;
            this.Module = module;
            this.Niveau_difficulte_moyenne = niveau_difficulte_moyenne;

        }
          



        //les relations 
      
            public virtual ICollection<Question> Questions { get; set; }
            public virtual ICollection<Candidat> Candidats { get; set; }
        }
            


    }

