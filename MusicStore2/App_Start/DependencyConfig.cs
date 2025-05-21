// using System.Reflection;
// using System.Web.Mvc;
// using Autofac;
// using Autofac.Integration.Mvc;
// using MusicStore.BusinessLogic;
// using MusicStore.BusinessLogic.Data;
// using MusicStore.BusinessLogic.Data.DataInterfaces;
// using MusicStore.BusinessLogic.Data.Repositories;
// using MusicStore.BusinessLogic.Interfaces;
// using MusicStore.BusinessLogic.Services;
// using MusicStore.BusinessLogic.Services.Interfaces;
//
// namespace MusicStore2
// {
//     public class DependencyConfig
//     {
//         public static void RegisterDependencies()
//         {
//             var builder = new ContainerBuilder();
//
//             // Registrare controllere
//             builder.RegisterControllers(Assembly.GetExecutingAssembly());
//
//             // Registrare DbContext
//             builder.RegisterType<AppDbContext>().AsSelf().InstancePerRequest();
//
//             // Registrare repository-uri concrete
//             builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerRequest();
//             builder.RegisterType<ProductRepository>().As<IProductRepository>().InstancePerRequest();
//
//             // ✅ Registrare generic repository
//             builder.RegisterGeneric(typeof(GenericRepository<>))
//                 .As(typeof(IGenericRepository<>))
//                 .InstancePerRequest();
//
//             // Registrare servicii
//             builder.RegisterType<AuthService>().As<IAuthService>().InstancePerRequest();
//             builder.RegisterType<ProductService>().As<IProductService>().InstancePerRequest();
//
//             // Construiește containerul
//             var container = builder.Build();
//
//             // Configurare pentru MVC
//             DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
//         }
//     }
// }