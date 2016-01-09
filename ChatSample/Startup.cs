using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin.Builder;
using Owin;

namespace ChatSample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseStatusCodePages();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new {controller = "Home", action = "Index"});
            });

            app.UseOwin(addToPipeline =>
            {
                addToPipeline(next =>
                {
                    var builder = new AppBuilder();
                    var hubConfig = new HubConfiguration { EnableDetailedErrors = true };

                    builder.MapSignalR(hubConfig);

                    var appFunc = builder.Build(typeof(Func<IDictionary<string, object>, Task>)) as Func<IDictionary<string, object>, Task>;

                    return appFunc;
                });
            });
        }
    }
}