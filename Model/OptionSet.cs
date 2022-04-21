namespace Model.Definitions
{
    internal class OptionSet
    {
        //definition candidat.civilité 
        public enum civilite
        {
            Monsieur = 0,
            Madame = 1
        }

        public enum Etat_Civile
        {
            Celibataire = 0,
            Marié = 1,
            Divorcé = 2
        }

        public enum Experience
        {
            Stagiaire = 914320000,
            deuxans = 914320000,
            cinqans = 914320003,
            Plusde5ans = 914320002
        }

        public enum Categorie
        {
            Ingénierie = 914320001,
            Junior = 914320002,
            Senior = 914320003
        }
        public enum Niveau_Dificullte
        {
            Niveau1 = 914320000,
            Niveau2 = 914320001,
            Niveau3 = 914320002
        }


        public enum Niveau_difficulte_moyenne
        {
            Niveau1 = 914320000,
            Niveau2 = 914320001,
            Niveau3 = 914320002
        }

        public enum Module
        {
            C = 914320000,
            logique = 914320001,
            algorithme = 914320002,
            Html = 914320003,
            CSS = 914320004,
            JS = 914320005,
            CRM = 914320006

        }

        public enum Valiadtion
        {
            Vrai = 0,
            Faux = 1
        }

    }
}
