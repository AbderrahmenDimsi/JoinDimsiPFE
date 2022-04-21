using Microsoft.Xrm.Sdk;
using Model.Definitions;
using System;

namespace Plugins
{
    public class Nom_CompletUpdate : IPlugin
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
                //verification de l'entité et des champ 

                Entity candidat = (Entity)context.InputParameters["Target"];

                Entity preImageCandidat = (Entity)context.PreEntityImages["preimage"];
                var nom = "";
                var prenom = "";



                if (candidat.Contains(CandidatDefinition.Columns.Nom))
                    nom = candidat.GetAttributeValue<string>(CandidatDefinition.Columns.Nom);
                if (candidat.Contains(CandidatDefinition.Columns.prenom))
                    prenom = candidat.GetAttributeValue<string>(CandidatDefinition.Columns.prenom);

                if (nom == "" && preImageCandidat.Contains(CandidatDefinition.Columns.Nom))
                    nom = preImageCandidat.GetAttributeValue<string>(CandidatDefinition.Columns.Nom);
                if (prenom == "" && preImageCandidat.Contains(CandidatDefinition.Columns.prenom))
                    prenom = preImageCandidat.GetAttributeValue<string>(CandidatDefinition.Columns.prenom);



                candidat[CandidatDefinition.Columns.Nom_Complet] = nom + " " + prenom;

            }


        }
    }
}
