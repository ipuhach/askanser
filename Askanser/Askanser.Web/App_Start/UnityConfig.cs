using Askanser.Core.Entities;
using Askanser.Core.Interfaces;
using Askanser.Data.Connections;
using Askanser.Data.Repositories;
using Microsoft.AspNet.Identity;
using System.Configuration;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Mvc5;

namespace Askanser.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // install a named string that holds the connection string to use
            container.RegisterInstance<string>("connectionString", ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, new ContainerControlledLifetimeManager());

            // register the class that will use the connection string
            //container.RegisterType<MyNamespace.MyObjectContext, MyNamespace.MyObjectContext>(new InjectionConstructor(new ResolvedParameter<string>("MyConnectionString")));


            container.RegisterType<IConnectionFactory, SqlConnectionFactory>(new InjectionConstructor(new ResolvedParameter<string>("connectionString")));

            //.WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IUserStore<User>, UserRepository>();
            container.RegisterType<IUserLoginStore<User>, UserRepository>();
            container.RegisterType<IUserPasswordStore<User>, UserRepository>();
            container.RegisterType<IUserSecurityStampStore<User>, UserRepository>();
            container.RegisterType<IAskanserProvider, AskanserProvider>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}