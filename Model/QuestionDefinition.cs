using System.Diagnostics.CodeAnalysis;

namespace Model
{
    public class QuestionDefinition

    {
        public const string EntityName = "dim_question";
        public const string EntityCollectionName = "dim_questions";

        [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public static class Columns
        {

            public const string Id = "dim_questionid"; 
            public const string nom = "dim_name";
            public const string Enonce = "dim_enonce";
            public const string Module = "dim_module";
            public const string Niveau_difficulte = "dim_niveau_difficulte";

            public const string QuestionReponse = "dim_question_reponseid";
            public const string QuestionTest = "dim_dim_question_dim_test";

        }
    }
}