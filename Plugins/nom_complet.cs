using Microsoft.Xrm.Sdk;
using Model.Definitions;
using System;

namespace Plugins
{
    public class nom_complet : IPlugin
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

                Entity candidat = (Entity)context.InputParameters["Target"];

                if (candidat.Contains(CandidatDefinition.Columns.Nom) && candidat.Contains(CandidatDefinition.Columns.prenom))
                {
                    var nom = candidat.GetAttributeValue<string>(CandidatDefinition.Columns.Nom);
                    var prenom = candidat.GetAttributeValue<string>(CandidatDefinition.Columns.prenom);
                    candidat[CandidatDefinition.Columns.Nom_Complet] = nom + " " + prenom;
                }

                if (candidat.Contains(CandidatDefinition.Columns.courrier))
                {
                    var email = candidat.GetAttributeValue<string>(CandidatDefinition.Columns.courrier);
                    var Isvalid = Model.Serviceplugin.IsValidEmail(email);
                    if (!Isvalid)
                    {
                        throw new InvalidPluginExecutionException("L'email n'est pas valide");
                    }
                }
                if (candidat.Contains(CandidatDefinition.Columns.numero))
                {
                    var telephone = candidat.GetAttributeValue<string>(CandidatDefinition.Columns.numero);
                    var isvalidTelephone = Model.Serviceplugin.IsValidTelephone(telephone);
                    if (!isvalidTelephone)
                    {
                        throw new InvalidPluginExecutionException("Le telephone n'est pas valide");
                    }
                }
            }


        }
    }
}

