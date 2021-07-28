using GraphQL.Server.Ui.Voyager;
using GraphQLDemo.API.GraphQL.DataLoaders;
using GraphQLDemo.API.GraphQL.Mutations;
using GraphQLDemo.API.GraphQL.Queries;
using GraphQLDemo.API.GraphQL.Subscriptions;
using GraphQLDemo.API.GraphQL.Types;
using GraphQLDemo.DataRepository;
using GraphQLDemo.DataRepository.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GraphQLDemo.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            services.AddPooledDbContextFactory<GraphQLDemoContext>(options =>
            {
                options.EnableSensitiveDataLogging(false)
                    .UseSqlServer(Configuration.GetConnectionString("GraphQLDemoContext"))
                    .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
            });
            services.TryAddTransient<IUnitOfWork, UnitOfWork>();
            services
                .AddGraphQLServer()
                .AddQueryType(q => q.Name("Query"))
                    .AddTypeExtension<DepartmentQueries>()
                    .AddTypeExtension<EmployeeQueries>()

                .AddMutationType(m => m.Name("Mutation"))
                    .AddTypeExtension<DepartmentMutations>()
                    .AddTypeExtension<EmployeeMutations>()

                .AddSubscriptionType(m => m.Name("Subscription"))
                    .AddTypeExtension<DepartmentSubscriptions>()
                    .AddTypeExtension<EmployeeSubscriptions>()
                .AddType<DepartmentType>()
                .AddType<EmployeeType>()

                .AddFiltering()
                .AddSorting()
                .AddInMemorySubscriptions()
                .AddDataLoader<DepartmentByIdDataLoader>()
                .AddDataLoader<EmployeesByDepartment_IdDataLoader>()
                .AddDataLoader<EmployeeByIdDataLoader>()
                .AddDataLoader<EmployeesByEmployee_IdDataLoader>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseWebSockets();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
            app.UseGraphQLVoyager(new VoyagerOptions()
            {
                GraphQLEndPoint = "/graphql",

            }, path: "/graphql-voyager");
        }
    }
}
