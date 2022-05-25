using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Model.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugins
{
    internal class candidature : IPlugin
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
                    if(context.MessageName.ToLower()== "create"|| context.MessageName.ToLower()=="update")
                    {
                   
                    var target = (EntityReference)context.InputParameters["Target"];
                    if(target.LogicalName== "dim_candidat")
                    {
                    
                        var an_statuscode = 1;
                        var query = new QueryExpression(CandidatDefinition.EntityName);
                        query.Distinct = true;
                        query.ColumnSet.AddColumns(CandidatDefinition.Columns.Id, CandidatDefinition.Columns.Nom);
                        query.AddOrder(CandidatureDefinition.Columns.creer_le, OrderType.Descending);

                        var an = query.AddLink(CandidatureDefinition.EntityName, CandidatDefinition.Columns.Id,CandidatureDefinition.Columns.candidat);
                        an.EntityAlias = "an";
                        an.LinkCriteria.AddCondition(CandidatureDefinition.Columns.Id, ConditionOperator.NotNull);
                        an.LinkCriteria.AddCondition(CandidatureDefinition.Columns.raison_statut, ConditionOperator.Equal, an_statuscode);


                        var candidature =  service.RetrieveMultiple(query).Entities.ToList();
                        throw new InvalidPluginExecutionException(candidature[0].ToString());
                        for (int i =1;i<candidature.Count();i++)
                        {
                            
                            if (candidature[i].Contains(CandidatureDefinition.Columns.raison_statut))
                            {
                                var raisonS = candidature[i].GetAttributeValue<OptionSetValue>(CandidatureDefinition.Columns.raison_statut).Value;
                                if (raisonS == 1)
                                    candidature[i][CandidatureDefinition.Columns.raison_statut] = 587920001;
                            }
                        }

                      

                    }

                }
                
            }
            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException(ex.Message);
            }


        }
    }
}
