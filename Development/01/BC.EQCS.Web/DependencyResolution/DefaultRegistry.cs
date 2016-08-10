// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using BC.EQCS.ActivityLog.Logger;
using BC.EQCS.Contracts;
using BC.EQCS.Domain;
using BC.EQCS.Domain.Incident;
using BC.EQCS.Domain.Incident.Validation;
using BC.EQCS.Domain.Schema;
using BC.EQCS.Entities;
using BC.EQCS.Models;
using BC.EQCS.Repositories;
using BC.EQCS.Repositories.Security;
using BC.EQCS.Security.Models;
using BC.EQCS.Security.Repository;
using BC.EQCS.Security.Service;
using BC.EQCS.Web.Infrastructure.Logging;
using BC.EQCS.Web.Utils;
using BC.EQCS.Workflow;
using BC.Security.Internal;
using BC.Security.Internal.Contracts;
using Microsoft.Owin;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace BC.EQCS.Web.DependencyResolution
{
    public class SecurityRegistryWebApi : Registry
    {
        public SecurityRegistryWebApi()
        {
            For<IContextResolver>().Use(context => CreateContextResolver(context));
        }

        private static IContextResolver CreateContextResolver(IContext context)
        {
            var claimsPrinciple = HttpContext.Current.User as ClaimsPrincipal;
          
            var repo = context.GetInstance<ISecurityUserRepository>();
            var securityUserModel = repo.GetUserIdentity(claimsPrinciple.Identity).Result;
            
            return new ContextResolver(securityUserModel);
        }
    }

    public class SecurityRegistryWebApiIntegrationTesting : Registry
    {
        public SecurityRegistryWebApiIntegrationTesting()
        {
            For<IContextResolver>().Use(context => CreateContextResolver(context));
        }

        private static IContextResolver CreateContextResolver(IContext context)
        {
            var owinContext = context.GetInstance<IOwinContext>();
            var user = owinContext.Request.Get<SecurityUserModel>(WindowsPrincipalHandler.UserRequestKey);
            return new ContextResolver(user);
        }
    }


    public class SecurityRegistryMvc : Registry
    {
        public SecurityRegistryMvc()
        {
            For<IContextResolver>().Use(context => CreateContextResolver());
        }

        private static IContextResolver CreateContextResolver()
        {
            var claimsPrinciple = HttpContext.Current.User as ClaimsPrincipal;
            var repo = ObjectFactory.GetInstance<ISecurityUserRepository>();
            var securityUserModel = repo.GetUserIdentity(claimsPrinciple.Identity).Result;
            return new ContextResolver(securityUserModel);
        }
    }

    public class SecurityRegistryMvcIntegrationTesting : Registry
    {
        public SecurityRegistryMvcIntegrationTesting()
        {
            For<IContextResolver>().Use(context => CreateContextResolver());
        }

        private static IContextResolver CreateContextResolver()
        {
            var context = HttpContext.Current.GetOwinContext();
            var claimsPrincipal = context.Request.Get<SecurityUserModel>(WindowsPrincipalHandler.UserRequestKey);
            return new ContextResolver(claimsPrincipal);
        }
    }


    public class DefaultRegistry : Registry
    {
        #region Constructors and Destructors

        public DefaultRegistry()
        {
            Scan(
                scan =>
                {
                    scan.TheCallingAssembly();

                    scan.With(new ControllerConvention());

                    scan.WithDefaultConventions();

                    scan.SingleImplementationsOfInterface();
                });

            ScanSolutionAssemblies();
        }

        #endregion

        private void ScanSolutionAssemblies()
        {
            Scan(scan =>
            {
                scan.WithDefaultConventions();

                scan.Assembly("BC.EQCS.Repositories");
                scan.ConnectImplementationsToTypesClosing(typeof (IRepository<>));
                scan.ConnectImplementationsToTypesClosing(typeof (ITreeRepository<>));
                scan.ConnectImplementationsToTypesClosing(typeof (IOdataRepository<>));
                scan.ConnectImplementationsToTypesClosing(typeof (IAsyncRepository<>));
                scan.ConnectImplementationsToTypesClosing(typeof (IAspectRepository<,>));
                scan.ConnectImplementationsToTypesClosing(typeof (ISchemaKeyRepository<>));
                scan.ConnectImplementationsToTypesClosing(typeof (IDocumentRepository<,>));
                scan.ConnectImplementationsToTypesClosing(typeof (INotificationRepository<,>));
                scan.ConnectImplementationsToTypesClosing(typeof (INotificationTemplateRepository<>));

                scan.Assembly("BC.EQCS.Domain");
                scan.ConnectImplementationsToTypesClosing(typeof (IModelValidator<>));
                scan.ConnectImplementationsToTypesClosing(typeof (IModelSchemata<>));
                scan.ConnectImplementationsToTypesClosing(typeof (ICommandAvailabilityManager<>));
                scan.ConnectImplementationsToTypesClosing(typeof (IDictionary<,>));
                scan.ConnectImplementationsToTypesClosing(typeof (ILookup<,>));
                scan.ConnectImplementationsToTypesClosing(typeof (ICommandTransitionMaps<,>));
                scan.ConnectImplementationsToTypesClosing(typeof (IValidationBuilderFactory<,,>));
                scan.ConnectImplementationsToTypesClosing(typeof (IAspectValidationBuilderFactory<,,,>));
                scan.ConnectImplementationsToTypesClosing(typeof (ISchemaAggregator<,,,>));
                scan.ConnectImplementationsToTypesClosing(typeof (ISchemaBuilderFactory<,,>));
                scan.ConnectImplementationsToTypesClosing(typeof (ISchemaBuildDirector<,>));
                scan.ConnectImplementationsToTypesClosing(typeof (IModelUpdaterFactory<,,,>));
                scan.ConnectImplementationsToTypesClosing(typeof (IModelUpdateStrategy<,>));

                scan.ConnectImplementationsToTypesClosing(typeof (IIncidentAttributeMapping<>));

                scan.Assembly("BC.EQCS.Workflow");
                scan.AddAllTypesOf<IWorkflow>();
                scan.ConnectImplementationsToTypesClosing(typeof (IWorkflowActivityLogger<,>));

                scan.Assembly("BC.EQCS.Security");


                scan.Assembly("BC.EQCS.ActivityLog");
                scan.AddAllTypesOf(typeof (IActivityLogger<,>));
                scan.ConnectImplementationsToTypesClosing(typeof (IActivityLogger<,>));
            });

            For<IEntityFactory>().Use<EntityFactory>().SetHttpLifeCycleIfIis();

            For<ISecurityUserRepository>().Use<UserRepository>();

            For<ISecurityIncidentRepository>().Use<SecurityIncidentRepository>();

            For<IWorkflowFactory>().Use<WorkflowFactory>();

            For<IUserContext>().Use<UserContext>();

            For<IAssetAuthoriser>().Use<Authoriser>();

            For<IPermissionsRepository>().Use<PermissionsRepository>();

            For<IModelValidator<IncidentActionModel, IncidentActionRuleSet>>().Use<IncidentActionModelValidator>();

            For<ILogger>().AlwaysUnique().Use(c => new NLogLogger(c.ParentType));

            //For<ILogger>().Use(c => new NLogLogger(c.ParentType)).AlwaysUnique();
        }
    }
}