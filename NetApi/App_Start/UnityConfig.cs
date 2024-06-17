using AutoMapper;
using NetApi.Context;
using NetApi.Interfaces;
using NetApi.Mapper;
using NetApi.Services;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace NetApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            // Crear contenedor de Unity
            var container = new UnityContainer();

            // Configurar AutoMapper
            var config = new MapperConfiguration(cfg => {
                 cfg.AddProfile<MappingProfile>();
            });

            // Crear IMapper
            IMapper mapper = config.CreateMapper();

            // Registrar instancias únicas
            container.RegisterInstance(mapper); // Registrar IMapper como una instancia única

            // Registrar contexto de base de datos
            container.RegisterType<ApiContext>(new HierarchicalLifetimeManager());

            // Registrar servicios
            container.RegisterType<ITestService, TestService>();
            container.RegisterType<IAuth, AuthService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IProductService, ProductService>();

            // Configurar Web API
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}