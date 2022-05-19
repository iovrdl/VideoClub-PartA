using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using VideoClub.Data.Context;
using VideoClub.Data.Services;

namespace VideoClub.Web
{
    public class ContainerConfig
    {
        public static void RegisterContainer(HttpConfiguration httpConfiguration)
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<SqlCustomerRepository>()
                .As<ICustomerRepository>()
                .InstancePerRequest();

            builder.RegisterType<SqlFilmRepository>()
                .As<IFilmRepository>()
                .InstancePerRequest();

            builder.RegisterType<SqlCopyRepository>()
                .As<ICopyRepository>()
                .InstancePerRequest();

            builder.RegisterType<SqlRentalRepository>()
                .As<IRentalRepository>()
                .InstancePerRequest();

            builder.RegisterType<ApplicationDbContext>()
                .InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}