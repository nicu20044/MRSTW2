using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using MusicStore.BusinessLogic;
using MusicStore.BusinessLogic.Data;
using MusicStore.BusinessLogic.Data.DataInterfaces;
using MusicStore.BusinessLogic.Interfaces;
using MusicStore.BusinessLogic.Services;


namespace MusicStore2
{
    public class DependencyConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            // Registrare controllere
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            // Registrare DbContext
            builder.RegisterType<AppDbContext>().AsSelf().InstancePerRequest();

            // Registrare repository-uri
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerRequest();

            // Registrare servicii
            builder.RegisterType<SessionBL>().As<ISession>().SingleInstance();
            builder.RegisterType<AuthService>().AsSelf().InstancePerRequest();

            // Construiește containerul
            var container = builder.Build();

            // Configurare pentru MVC
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}