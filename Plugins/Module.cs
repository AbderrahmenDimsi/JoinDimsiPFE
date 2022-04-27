using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Model;
using Model.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Plugins
{
    public class Module : IPlugin
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
                    var target = (EntityReference)context.InputParameters["Target"];
                    var relatedEntities = context.InputParameters["RelatedEntities"] as EntityReferenceCollection;

                    var relatedEntityId = relatedEntities[0].Id;
                    if (target.LogicalName == "dim_test")
                    {
                        var Test = service.Retrieve(TestDefinition.EntityName, target.Id, new ColumnSet(TestDefinition.column.Module));

                        var query = new QueryExpression(QuestionDefinition.EntityName);
                        query.ColumnSet.AddColumns(QuestionDefinition.Columns.Id,QuestionDefinition.Columns.Enonce, QuestionDefinition.Columns.Module);
                        var query_dim_dim_question_dim_test = query.AddLink(QuestionDefinition.Columns.QuestionTest,QuestionDefinition.Columns.Id, QuestionDefinition.Columns.Id);
                        var ab = query_dim_dim_question_dim_test.AddLink(TestDefinition.EntityName, TestDefinition.column.Id, TestDefinition.column.Id);
                        ab.LinkCriteria.AddCondition(TestDefinition.column.Id, ConditionOperator.Equal, Test.Id);



                        string ch = "";
                        var questions = service.RetrieveMultiple(query).Entities.ToList();
                        var nums = new List<string>();
                        List<string> nn = new List<string>();

                     
                        foreach (var q in questions)
                        {
                            nums.Add(q.FormattedValues[QuestionDefinition.Columns.Module]);
                          
                        }

                        
                       var m= nums.Distinct().Select(s => Tuple.Create(s, nums.Count(s2 => s2==s))).ToList();
                 
                        

                        // var questionsDistinct = questions.GroupBy(x => x.GetAttributeValue<OptionSetValue>(QuestionDefinition.Columns.Module)).Select(y => y.First()).ToList();

                        foreach (var mm in m)
                        {
                            if (questions.Count() > 1)
                                ch = ch + "  " + mm;
                           else ch = ch + mm;
                         
                        }

                      
                        Test[TestDefinition.column.Module] = ch;

                        service.Update(Test);
                    }
                    if (target.LogicalName == "dim_question")
                    {

                        var Test = service.Retrieve(TestDefinition.EntityName, relatedEntityId, new ColumnSet(TestDefinition.column.Module));


                        var query = new QueryExpression(QuestionDefinition.EntityName);
                        query.ColumnSet.AddColumns(QuestionDefinition.Columns.Id, QuestionDefinition.Columns.Enonce, QuestionDefinition.Columns.Module);
                        var query_dim_dim_question_dim_test = query.AddLink(QuestionDefinition.Columns.QuestionTest, QuestionDefinition.Columns.Id, QuestionDefinition.Columns.Id);
                        var ab = query_dim_dim_question_dim_test.AddLink(TestDefinition.EntityName, TestDefinition.column.Id, TestDefinition.column.Id);
                        ab.LinkCriteria.AddCondition(TestDefinition.column.Id, ConditionOperator.Equal, Test.Id);


                        string ch = "";
                        var questions = service.RetrieveMultiple(query).Entities.ToList();

                        var nums = new List<string>();
                        List<string> nn = new List<string>();


                        foreach (var q in questions)
                        {
                            nums.Add(q.FormattedValues[QuestionDefinition.Columns.Module]);

                        }


                        var m = nums.Distinct().Select(s => Tuple.Create(s, nums.Count(s2 => s2 == s))).ToList();



                        // var questionsDistinct = questions.GroupBy(x => x.GetAttributeValue<OptionSetValue>(QuestionDefinition.Columns.Module)).Select(y => y.First()).ToList();

                        foreach (var mm in m)
                        {
                            if (questions.Count() > 1)
                                ch = ch + "  " + mm;
                            else ch = ch + mm;

                        }

                        Test[TestDefinition.column.Module] = ch;

                        service.Update(Test);
                    }
                    calculNiveauDiffMoyenne(context, service);
                }


            }
            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException(ex.Message);
            }
        }
        private void calculNiveauDiffMoyenne(IPluginExecutionContext context, IOrganizationService service)
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

}

















