using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using GraphQLGraphTypeFirstNestedTable.GraphQL;
using GraphQLGraphTypeFirstNestedTable.GraphQL.Clients;
using GraphQLGraphTypeFirstNestedTable.GraphQL.Queries;
using GraphQLGraphTypeFirstNestedTable.GraphQL.Schemas;
using GraphQLGraphTypeFirstNestedTable.GraphQL.Types;
using GraphQLGraphTypeFirstNestedTable.Models;
using GraphQLGraphTypeFirstNestedTable.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GraphQLGraphTypeFirstNestedTable
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
            
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ICertificationRepository, CertificationRepository>();
            
            services.AddDbContext<GraphQLDemoContext>(options =>  options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IServiceProvider>(provider => new FuncServiceProvider(provider.GetRequiredService));

            services.AddScoped<EmployeeSchema>();
            
            services.AddGraphQL(x => {
                    x.ExposeExceptions = true; //set true only in development mode. make it switchable.
                }).AddGraphTypes(ServiceLifetime.Scoped);


            services.AddScoped<EmployeeQuery>();
            services.AddScoped<EmployeeType>();
            services.AddScoped<EmployeeCertificationType>();
            // services.AddScoped<IDependencyResolver>(_ => new FuncDependencyResolver(_.GetRequiredService));
            //services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            //services.AddSingleton<IDocumentWriter, DocumentWriter>();
            //services.AddScoped<ISchema, EmployeeSchema>();

            //services.AddScoped<IEmployeeGraphqlClient, EmployeeGraphqlClient>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           // app.UseGraphQLAltair();// GraphQL.Server.Ui.Altair 
            app.UseGraphiQLServer(); // GraphQL.Server.Ui.GraphiQL
            app.UseGraphQL<EmployeeSchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
