using Microsoft.Xrm.Sdk;
using Model;
using System;

namespace Plugins
{
    public class NiveauDiff : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
            {

                //Trace
                ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
                //execute context
                IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

                Entity question = (Entity)context.InputParameters["Target"];

                if (question.Contains(QuestionDefinition.Columns.Module))
                {
                    var module = question.GetAttributeValue<OptionSetValue>(QuestionDefinition.Columns.Module).Value;


                    if (module == 914320002 || module == 914320001)
                    {
                        question[QuestionDefinition.Columns.Niveau_difficulte] = new OptionSetValue(914320000);

                    }
                    else if
                          (module == 914320003 || module == 914320004)
                    {
                        question[QuestionDefinition.Columns.Niveau_difficulte] = new OptionSetValue(914320001);
                    }
                    else
                        question[QuestionDefinition.Columns.Niveau_difficulte] = new OptionSetValue(914320002);


                }

            }
        }
    }
}


