using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace DotNet.Core.SignalR
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .AddJsonOptions
                (
                    options =>
                    {
                        /*
                            CAMEL CASE ISSUES AND CUSTOM CONTRACT RESOLVERS

                            At this moment, MVC uses camel case notation to pass JSON to clients.
                            That means that even if on the server you write your class properties with Pascal
                            case notation (as you should!), JavaScript clients would get Camel cased objects.

                            This behavior is new to ASP.NET Core and was not present in the older versions
                            when SignalR was used and built, so SignalR and its clients all expect Pascal case
                            objects, while the objects passed between MVC and its clients are camel cased.
                            This means it is not possible to reuse metods across the JavaScript client, thing
                            that cannot be cannot tolerated.

                            So, user should make SignalR pass objects in camel case. Basically, idea is to
                            “recycle” this old SignalR GitHub issue and adapt it to modified versions of
                            ASP.NET and SignalR.

                            ADD A CUSTOM CONTRACT RESOLVERS

                            A first try could be to change the default contract resolver to a
                            CamelCasePropertyNamesContractResolver() inside the ConfigureServices method:
                        */
                        var resolver =
                                // new Newtonsoft.Json.Serialization.DefaultContractResolver();
                                // new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                                new DotNet.Core.SignalR.Models.SignalRContractResolver()
                                ;

                        options.SerializerSettings.ContractResolver = resolver;
                        var settings = new Newtonsoft.Json.JsonSerializerSettings();
                        settings.ContractResolver = resolver;

                        var serializer = Newtonsoft.Json.JsonSerializer.Create(settings);

                        services.Add
                            (
                                new ServiceDescriptor
                                        (
                                            typeof(Newtonsoft.Json.JsonSerializer),
                                            provider => serializer,
                                            ServiceLifetime.Transient
                                        )
                            );
                    }
                );

            /*
            CALLING CLIENT METHODS OUTSIDE HUBS

            In the previous versions of SignalR, in order to call client methods and manage groups
            outside a hub you would need to make use of the GlobalHost, which is no longer available
            in the new version.

            So we will use an instance of ConnectionManager, specifically the GetHubContext<>()
            method. We need to register this service so we can use it in the controller:

            _connectionManager.GetHubContext<PostsHub>().someClientMethod(parameters)

            */
            services.AddSingleton
                        <
                           DotNet.Core.SignalR.Models.IPostRepository,
                           DotNet.Core.SignalR.Models.PostRepository
                        >();

            services.AddSignalR
                (
                    options
                        =>
                        {
                            options.Hubs.EnableDetailedErrors = true;
                        }
                );

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            /*
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            */

            app.UseStaticFiles();

            app.UseMvc
            (
                routes =>
                {
                    routes.MapRoute
                    (
                        name: "default",
                        template: "api/{controller}/{action}/{id?}"
                    );
                }
            );

            app.UseWebSockets();
            app.UseSignalR();

            /*
            app.Run
                (
                    async (context)
                        =>
                        {
                            await context.Response.WriteAsync("SignalR Sample!");
                        }
                );
            */
            return;
        }
    }
}