using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Model;
using Model.Definitions;
using System;
using System.Linq;

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
                    var test = 0;
                    var target = (EntityReference)context.InputParameters["Target"];
                    var relatedEntities = context.InputParameters["RelatedEntities"] as EntityReferenceCollection;
                    var testId = relatedEntities[0].Id;
                    if (target.LogicalName == "dim_test")
                    {
                        var Test = service.Retrieve(TestDefinition.EntityName, target.Id, new ColumnSet(TestDefinition.column.Module));


                        var query = new QueryExpression(QuestionDefinition.EntityName);
                        query.ColumnSet.AddColumns(QuestionDefinition.Columns.Id, QuestionDefinition.Columns.nom, QuestionDefinition.Columns.Module);
                        query.AddOrder(QuestionDefinition.Columns.nom, OrderType.Ascending);
                        var queryQuestionTest = query.AddLink(QuestionDefinition.Columns.QuestionTest, QuestionDefinition.Columns.Id, QuestionDefinition.Columns.Id);
                        var aa = queryQuestionTest.AddLink(TestDefinition.column.TestQuestion, TestDefinition.column.Id, TestDefinition.column.Id);
                        aa.LinkCriteria.AddCondition(TestDefinition.column.Id, ConditionOperator.Equal, Test.Id);

                        string ch = "";
                        var questions = service.RetrieveMultiple(query).Entities.ToList();

                        var questionsDistinct = questions.GroupBy(x => x.GetAttributeValue<OptionSetValue>(QuestionDefinition.Columns.Module)).Select(y => y.First()).ToList();

                        foreach (var q in questionsDistinct)
                        {
                            if (questions.Count() > 1)
                                ch = ch + " ," + q.FormattedValues[QuestionDefinition.Columns.Module];
                            else ch = ch + q.FormattedValues[QuestionDefinition.Columns.Module];
                        }

                        Test[TestDefinition.column.Module] = ch;

                        service.Update(Test);
                    }
                    if (target.LogicalName == "dim_question")
                    {

                        var Test = service.Retrieve(TestDefinition.EntityName, testId, new ColumnSet(TestDefinition.column.Module));


                        var query = new QueryExpression(QuestionDefinition.EntityName);
                        query.ColumnSet.AddColumns(QuestionDefinition.Columns.Id, QuestionDefinition.Columns.nom, QuestionDefinition.Columns.Module);
                        query.AddOrder(QuestionDefinition.Columns.nom, OrderType.Ascending);
                        var queryQuestionTest = query.AddLink(QuestionDefinition.Columns.QuestionTest, QuestionDefinition.Columns.Id, QuestionDefinition.Columns.Id);
                        var aa = queryQuestionTest.AddLink(TestDefinition.column.TestQuestion, TestDefinition.column.Id, TestDefinition.column.Id);
                        aa.LinkCriteria.AddCondition(TestDefinition.column.Id, ConditionOperator.Equal, Test.Id);

                        string ch = "";
                        var questions = service.RetrieveMultiple(query).Entities.ToList();

                        var questionsDistinct = questions.GroupBy(x => x.GetAttributeValue<OptionSetValue>(QuestionDefinition.Columns.Module)).Select(y => y.First()).ToList();

                        foreach (var q in questionsDistinct)
                        {
                            if (questions.Count() > 1)
                                ch = ch + " ," + q.FormattedValues[QuestionDefinition.Columns.Module];
                            else ch = ch + q.FormattedValues[QuestionDefinition.Columns.Module];
                        }

                        Test[TestDefinition.column.Module] = ch;

                        service.Update(Test);
                    }
                }


            }
            catch (Exception ex)
            {

            }
        }


    }

}

















