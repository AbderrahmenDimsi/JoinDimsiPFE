namespace Model.Definitions
{
    public class TestDefinition
    {

        //definition de la class
        public const string EntityName = "dim_test";
        public const string EntityCollectionName = "dim_tests";

        //definition des entités
        public static class column
        {
            public const string Id = "dim_testid";
            public const string Titre = "dim_titre";
            public const string Categorie = "dim_categorie";
            public const string Module = "dim_module";
            public const string Niveau_difficulte_moyenne = "dim_niveau_difficulte_moyenne";

            public const string TestQuestion = "dim_dim_question_dim_test";

        }
    }
}

