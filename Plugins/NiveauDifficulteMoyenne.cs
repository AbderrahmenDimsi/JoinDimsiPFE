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





                
            }






            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}


