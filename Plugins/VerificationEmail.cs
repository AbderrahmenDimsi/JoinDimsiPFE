﻿using Microsoft.Xrm.Sdk;
using Model.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Web.Services.Description;


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
                    var email = candidat.GetAttributeValue<string>(CandidatDefinition.Columns.email);
                    var Isvalid = Model.Serviceplugin.IsValidEmail(email);

                    
                }
              
            }
        }

    }
}



