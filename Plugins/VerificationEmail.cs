using Microsoft.Xrm.Sdk;
using Model.Definitions;
using System;


namespace Plugins
{
    public class VerificationEmail : IPlugin
    {


        void IPlugin.Execute(IServiceProvider serviceProvider)
        {


            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
            {

                //Trace
                ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
                //execute context
                IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

                Entity candidat = (Entity)context.InputParameters["Target"];
                if (candidat.Contains(CandidatDefinition.Columns.email))
                {
                    var email = candidat.GetAttributeValue<string>(CandidatDefinition.Columns.courrier);
                    var Isvalid = Model.Serviceplugin.IsValidEmail(email);


                }

            }
        }

    }
}



