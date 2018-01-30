using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using PeoplePetWebApp.Models;
using PeoplePetUtility;
using PeoplePetUtility.Interface;
using PeoplePetUtility.Api;

namespace PeoplePetWebApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            //services.AddSingleton<IConfiguration>(Configuration);
            services.Configure<PeopleApiSettings>(Configuration.GetSection("PeopleApiSettings"));

            services.AddSingleton<IApiRequestBuilder, ApiRequestBuilder>();

            services.AddSingleton<IPeoplePetRepository, PeoplePetRepository>((ctx) =>
            {
                IApiRequestBuilder apiBuiler = ctx.GetRequiredService<IApiRequestBuilder>();
                return new PeoplePetRepository(apiBuiler);
            });

            services.AddSingleton<IPeoplePetService, PeoplePetservice>((ctx) =>
            {
                //IPeoplePetRepository repo = ctx.GetService<IPeoplePetRepository>();
                IPeoplePetRepository repo = ctx.GetRequiredService<IPeoplePetRepository>();
                return new PeoplePetservice(repo);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            //loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/PeoplePet/Error");
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=PeoplePet}/{action=Index}/{id?}");
            });

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
