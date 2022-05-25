using Microsoft.Xrm.Sdk;
using Model;
using Model.Definitions;
using System;
using System.Collections.Generic;

namespace Plugins
{
    public class duree : IPlugin
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

                Entity target = (Entity)context.InputParameters["Target"];
            
                if (context.MessageName.ToLower() == "create" || context.MessageName.ToLower() == "update")
                {
                    if (target.LogicalName == "dim_test")
                    {
                        if (target.Contains(TestDefinition.column.dureetext))
                        {
                            var duree = target[TestDefinition.column.dureetext].ToString();
                            string ch = duree.Split(' ')[0];
                            target[TestDefinition.column.dureeInt] = (int.Parse(ch)*60);
                            //throw new InvalidPluginExecutionException(target.GetAttributeValue<int>(TestDefinition.column.dureeInt).ToString()
                               // + (target.GetAttributeValue<string>(TestDefinition.column.dureetext).ToString()));

                      
                        
                            
                        }
                    }
                }
            }
        }
    }
}
