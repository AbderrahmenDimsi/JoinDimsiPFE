using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Model;
using Model.Definitions;
using System;
using System.Linq;

namespace Plugins
{
    public class NiveauDifficulteMoyenne : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));


            //Trace
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            //execute context
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

            try

            {





                if (context.MessageName.ToLower() == "associate" || context.MessageName.ToLower() == "disassociate")
                {
                    var relatedEntities = context.InputParameters["RelatedEntities"] as EntityReferenceCollection;
                    var testId = relatedEntities[0].Id;
                    var target = (EntityReference)context.InputParameters["Target"];
                    if (target.LogicalName == "dim_test")
                    {
                        var Test = service.Retrieve(TestDefinition.EntityName, target.Id, new ColumnSet(TestDefinition.column.Module));





                        var query = new QueryExpression(QuestionDefinition.EntityName);
                        query.ColumnSet.AddColumns(QuestionDefinition.Columns.Id, QuestionDefinition.Columns.nom, QuestionDefinition.Columns.Niveau_difficulte);
                        query.AddOrder(QuestionDefinition.Columns.nom, OrderType.Ascending);
                        var queryQuestionTest = query.AddLink(QuestionDefinition.Columns.QuestionTest, QuestionDefinition.Columns.Id, QuestionDefinition.Columns.Id);
                        var aa = queryQuestionTest.AddLink(TestDefinition.column.TestQuestion, TestDefinition.column.Id, TestDefinition.column.Id);
                        aa.LinkCriteria.AddCondition(TestDefinition.column.Id, ConditionOperator.Equal, Test.Id);






                        int N = 0;
                        double moyenne;
                        var questions = service.RetrieveMultiple(query).Entities.ToList();


                        foreach (var Q in questions)
                        {
                            if (Q.Contains(QuestionDefinition.Columns.Niveau_difficulte))
                            {

                                var niveau = Q.GetAttributeValue<OptionSetValue>(QuestionDefinition.Columns.Niveau_difficulte).Value;

                                if (niveau == 914320000)
                                    N = N + 1;

                                else if (niveau == 914320001)
                                    N = N + 2;

                                else
                                    N = N + 3;

                            }

                        }
                        if (questions.Count() >= 1)
                        {
                            moyenne = N / questions.Count();
                        }
                        else
                            moyenne = 0;


                        Test[TestDefinition.column.Niveau_difficulte_moyenne] = moyenne;





                        service.Update(Test);
                    }
                    if (target.LogicalName == "dim_question")
                    {
                        var Test = service.Retrieve(TestDefinition.EntityName, testId, new ColumnSet(TestDefinition.column.Module));

                        var query = new QueryExpression(QuestionDefinition.EntityName);
                        query.ColumnSet.AddColumns(QuestionDefinition.Columns.Id, QuestionDefinition.Columns.nom, QuestionDefinition.Columns.Niveau_difficulte);
                        query.AddOrder(QuestionDefinition.Columns.nom, OrderType.Ascending);
                        var queryQuestionTest = query.AddLink(QuestionDefinition.Columns.QuestionTest, QuestionDefinition.Columns.Id, QuestionDefinition.Columns.Id);
                        var aa = queryQuestionTest.AddLink(TestDefinition.column.TestQuestion, TestDefinition.column.Id, TestDefinition.column.Id);
                        aa.LinkCriteria.AddCondition(TestDefinition.column.Id, ConditionOperator.Equal, Test.Id);






                        int N = 0;
                        double moyenne;
                        var questions = service.RetrieveMultiple(query).Entities.ToList();


                        foreach (var Q in questions)
                        {
                            if (Q.Contains(QuestionDefinition.Columns.Niveau_difficulte))
                            {

                                var niveau = Q.GetAttributeValue<OptionSetValue>(QuestionDefinition.Columns.Niveau_difficulte).Value;

                                if (niveau == 914320000)
                                    N = N + 1;

                                else if (niveau == 914320001)
                                    N = N + 2;

                                else
                                    N = N + 3;

                            }

                        }
                        if (questions.Count() >= 1)
                        {
                            moyenne = N / questions.Count();
                        }
                        else
                            moyenne = 0;


                        Test[TestDefinition.column.Niveau_difficulte_moyenne] = moyenne;





                        service.Update(Test);

                    }
                }
            }






            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}


