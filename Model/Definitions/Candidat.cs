using System;
using System.Collections.Generic;
using System.Text;


namespace Model.Definitions
{
    public  class Candidat
    {
        //definition de la classe 

        public const String EntityName = "Candidat";
        public const String EntityCollectionName = "Candidats";

        //les entités


    


        public int id { get; set; }
            public string Nom { get; set; }
            public string prenom { get; set; }
            public string Nom_Complet { get; set; }
            public string civilite { get; set; }
        public string Etat_Civile { get; set; }
            public int Telephone { get; set; }
            public string Adresse { get; set; }
            public string Experience { get; set; }
            public string email { get; set; }
            public int ResultatTest { get; set; }
            public string statuscode { get; set; }

        // les relations
        public virtual Test Test { get; set; }

        public Candidat()
        {
                
        }
        public Candidat(string nom , string prenom , string nom_complet,  string civilite , string etat_civilite , int telephone , string adresse , string experience , 
            string email , int resultattest , string statuscode )
        {
            this.Adresse = adresse;
            this.civilite = civilite;
            this.email = email;
                this.Telephone = telephone;
            this.Nom = nom;
            this.prenom = prenom;
            this.Nom_Complet = nom_complet;
            this.ResultatTest = resultattest;
            this.statuscode = statuscode;
            this.Experience = experience;
            this.Etat_Civile = etat_civilite;

        }
      





















    }
}
